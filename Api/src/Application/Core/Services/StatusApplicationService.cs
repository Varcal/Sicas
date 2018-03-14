using System.Collections.Generic;
using System.Linq;
using Application.Core.Contracts;
using Application.Core.Helpers;
using Application.Core.ViewModels;
using SharedKernel.Enums;

namespace Application.Core.Services
{
    public class StatusApplicationService: IStatusApplicationService
    {
        public IEnumerable<StatusVm> SelecionarTodos()
        {
            return Combo.CreateListByEnum<StatusEnum>().Select(s => new StatusVm(s.Id, s.Descricao)).ToList();
        }
    }
}
