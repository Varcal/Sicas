using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SharedKernel.DomainEvents;
using SharedKernel.DomainEvents.Events.DomainNotifications;
using SharedKernel.DomainEvents.Handlers.Base;

namespace Api.Core.Controllers.Base
{
    public class BaseController : ApiController
    {
        public Handler<DomainNotification> Notifications;
#pragma warning disable 108,114
        public HttpResponseMessage ResponseMessage;
#pragma warning restore 108,114
        protected string UsuarioLogado;
        
        public BaseController()
        { 
            Notifications = DomainEvent.Container.GetService<Handler<DomainNotification>>();
            ResponseMessage = new HttpResponseMessage();
            UsuarioLogado = User.Identity.Name;
        }

        public Task<HttpResponseMessage> CreateResponse(HttpStatusCode code, object result)
        {
            ResponseMessage = Notifications.HasNotification()
                ? Request.CreateResponse(HttpStatusCode.BadRequest, new {errors = Notifications.Notify()})
                : Request.CreateResponse(code, result);

            return Task.FromResult(ResponseMessage);
        }

        protected override void Dispose(bool disposing)
        {
            Notifications.Dispose();
        }
    }
}