using Domain.Financeiro.Entities.RecibosSindicantes;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Financeiro.Mappings
{
    public class ReciboSindicanteItemMap: BaseMap<ReciboSindicanteItem>
    {
        public ReciboSindicanteItemMap()
        {
            ToTable("ReciboSindicanteItem");

            
        }
    }
}
