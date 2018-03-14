using System.Linq;
using Domain.Core.Contracts.Repositories;
using Domain.Core.Entities.Enderecos;
using Infra.Data.Contexts;
using Infra.Data.Core.Repositories.Base;
using SharedKernel.Helpers;

namespace Infra.Data.Core.Repositories
{
    public class EnderecoRepository : RepositoryBase<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(EfContext context) : base(context)
        {
            Context = context;
        }

       
        public Endereco RetornaSeCadastrado(string cep, string logradouro)
        {
            var logradouroFonetizado = Soundex.FonetizarLogradouro(logradouro);

            return Context.Enderecos.SingleOrDefault(p => p.Cep == cep && p.LogradouroFonetizado == logradouroFonetizado);
        }

        public bool Existe(string cep, string logradouro)
        {
            var logradouroFonetizado = Soundex.FonetizarLogradouro(logradouro);
            return Context.Enderecos.Count(x => x.Cep == cep && x.LogradouroFonetizado == logradouroFonetizado) > 0;
        }
    }
}
