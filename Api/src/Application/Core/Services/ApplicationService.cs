using Infra.Data.Core.UnitOfWork;
using SharedKernel.DomainEvents;
using SharedKernel.DomainEvents.Events.DomainNotifications;
using SharedKernel.DomainEvents.Handlers.Base;

namespace Application.Core.Services
{
    public abstract class ApplicationService
    {
        private readonly Handler<DomainNotification> _notifications;
        private readonly IUnitOfWork _uow;

        protected ApplicationService(IUnitOfWork uow)
        {
            _notifications = DomainEvent.Container.GetService<Handler<DomainNotification>>();
            _uow = uow;
        }

        public void BeginTran()
        {
            _uow.BeginTran();
        }

        public void Commit()
        {
            _uow.Commit();
        }

        public void RollBack()
        {
            _uow.Rollback();
        }

        public bool Save(string usuarioLogado)
        {           
            if (_notifications.HasNotification())
                return false;

            _uow.Save(usuarioLogado);
            return true;
        }

        public bool Save()
        {

            if (_notifications.HasNotification())
                return false;

            _uow.Save();
            return true;
        }
    }
}
