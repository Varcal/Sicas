using System.Collections.Generic;
using Domain.Account.Scopes;
using Domain.Core.Entities;
using SharedKernel.Security;
using System.Configuration;

namespace Domain.Account.Entities
{
    public class Usuario: EntityId
    {
        public string Nome { get; private set; }
        public string Login { get; private set; }
        public string Senha { get; private set; }
        public ICollection<Perfil> Perfis { get; private set; }

        protected Usuario()
        {
            Perfis = new List<Perfil>();
        }

        public Usuario(string nome, string login, ICollection<Perfil> perfis)
        {
            Nome = nome;
            Login = login;
            // TODO: Criar senha inicial configuravel
            Senha = StringHelper.Encrypt(ConfigurationManager.AppSettings["senhaPadrao"]);
            //Senha = StringHelper.Encrypt("123456");
            Perfis = perfis;
        }

        public Usuario(string nome, string login, Perfil perfil)
            : this()
        {
            Nome = nome;
            Login = login;
            // TODO: Criar senha inicial configuravel
            Senha = StringHelper.Encrypt(ConfigurationManager.AppSettings["senhaPadrao"]);
            Perfis.Add(perfil);
        }

        public override bool IsValid()
        {
            return this.CriaUsuarioScopeSeValido();
        }

        public bool IsValidAlter(string nome, string login, ICollection<Perfil> perfis)
        {
            return this.AlterarUsuarioScopeSeValido(nome, login, perfis);
        }

        public void Alterar(string nome, string login, ICollection<Perfil> perfis)
        {
            if(!IsValidAlter(nome, login, perfis))
                return;

            Nome = nome;
            Login = login;
            Perfis = perfis;
        }

        public void AlterarSenha(string senha)
        {
            Senha = StringHelper.Encrypt(senha);
        }

        public void ResetarSenha()
        {
            var senha = ConfigurationManager.AppSettings["senhaPadrao"];
            AlterarSenha(senha);
        }
    }
}
