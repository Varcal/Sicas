using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Domain.Administrativo.Entities;
using Infra.Data.Contexts;

namespace Teste
{
    class Program
    {
        static void Main(string[] args)
        {
            //CriaNovaSeguradora();


            IList<Evento> e = new List<Evento>() {new Evento(1,1,109,112), new Evento(1,2,350,400), new Evento(1,3,44,34)};
            

           AlterarEvento(e);
        }

        private static void AlterarEvento(IList<Evento> model)
        {
            using (var ctx = new EfContext())
            {

                var seguradora = ctx.Seguradoras.Where(x=>x.Id == 2).Include(x => x.Eventos).SingleOrDefault();


                seguradora.Alterar(seguradora);
                
                ctx.Seguradoras.AddOrUpdate(seguradora);
                ctx.SaveChanges();
            }
        }

        private static void CriaNovaSeguradora()
        {
            using (var context = new EfContext())
            {
                var seguradora = new Seguradora("Nome", "!2345678000199", "1123456789", "Contato", "teste@teste.com.br", 0.60M);

                //var tipoServicos = context.TipoServicos.Include("Eventos").ToList();

                var eventos = new List<Evento>();



                /*-----AUTO-----*/
                //tipoServicos.SingleOrDefault(x => x.Id == 1).Eventos.Add(colisaoAuto);
                //tipoServicos.SingleOrDefault(x => x.Id == 1).Eventos.Add(incendioAuto);
                //tipoServicos.SingleOrDefault(x => x.Id == 1).Eventos.Add(rouboAuto);

                var colisaoAuto = new Evento(1, 1, 300, 500);
                var incendioAuto = new Evento(1, 2, 350, 400);
                var rouboAuto = new Evento(1, 3, 350, 400);

                eventos.Add(colisaoAuto);
                eventos.Add(incendioAuto);
                eventos.Add(rouboAuto);

                /*-----Bens RE-----*/
                //tipoServicos.SingleOrDefault(x => x.Id == 2).Eventos.Add(incendioBens);
                //tipoServicos.SingleOrDefault(x => x.Id == 2).Eventos.Add(rouboBens);

                var incendioBens = new Evento(2, 2, 350, 400);
                var rouboBens = new Evento(2, 3, 350, 400);

                eventos.Add(incendioBens);
                eventos.Add(rouboBens);



                /*-----Vida-----*/
                //tipoServicos.SingleOrDefault(x => x.Id == 3).Eventos.Add(docVida);
                var docVida = new Evento(3, 4, 150, 200);
                eventos.Add(docVida);

                seguradora.AdicionarServicos(eventos);


                context.Seguradoras.Add(seguradora);
                context.SaveChanges();
            }
        }
    }
}
