
using SharedKernel.DomainEvents.Events.Emails;

namespace CrossCutting.Utils.Services
{
    public interface IEmailService
    {
        void Send(Email email);
    }
}
