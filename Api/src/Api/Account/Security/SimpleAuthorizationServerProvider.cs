using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Application.Account.Contracts;
using CrossCutting.Ioc.Configurations;
using Microsoft.Owin.Security.OAuth;

namespace Api.Account.Security
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUsuarioApplicationService _userService;
        public SimpleAuthorizationServerProvider()
        {
            _userService = (IUsuarioApplicationService)SimpleInjectorInitializer.GetInstance<IUsuarioApplicationService>();
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return base.ValidateClientAuthentication(context);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var headers = context.OwinContext.Response.Headers.SingleOrDefault(k => k.Key == "Access-Control-Allow-Origin");

            if(headers.Equals(default(KeyValuePair<string, string[]>)))
              context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] {"*"});


            var user = _userService.Autenticar(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "Usuário ou senha inválidos");
                return base.GrantResourceOwnerCredentials(context);
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Login));
            identity.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString()));

            var roles = user.Perfis.Select(x => x.Nome).ToArray();

            foreach (var role in roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var principal = new GenericPrincipal(identity, roles);
            Thread.CurrentPrincipal = principal;

             context.Validated(identity);

            return base.GrantResourceOwnerCredentials(context);
        }

        public class QueryStringOAuthBearerProvider : OAuthBearerAuthenticationProvider
        {
            public override Task RequestToken(OAuthRequestTokenContext context)
            {
                var value = context.Request.Query.Get("access_token");

                if (!string.IsNullOrEmpty(value))
                {
                    context.Token = value;
                }

                return Task.FromResult<object>(null);
            }
        }
    }
}