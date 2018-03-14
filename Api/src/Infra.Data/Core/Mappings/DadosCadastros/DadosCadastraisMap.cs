using Domain.Core.Entities.DadosCadastros;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Core.Mappings.DadosCadastros
{
    public class DadosCadastraisMap: BaseMap<DadosCadastrais>
    {
        public DadosCadastraisMap()
        {
            ToTable("DadosCadastrais");
        }
    }
}
