using System.Collections.Generic;
using System.Linq;
using Domain.Account.Entities;
using Domain.Administrativo.Entities;
using Domain.Core.Entities.Enderecos;
using Infra.Data.Contexts;

namespace Infra.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<EfContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EfContext context)
        {

            if (!context.EnderecosTipos.Any())
            {
                var enderecoTipo = new EnderecoTipo("Residencial");
                var enderecoTipo2 = new EnderecoTipo("Comercial");


                context.EnderecosTipos.Add(enderecoTipo);
                context.EnderecosTipos.Add(enderecoTipo2);

                context.SaveChanges();
            }

            if (!context.EventosTipos.Any())
            {
                context.EventosTipos.Add(new EventoTipo("Colisão"));
                context.EventosTipos.Add(new EventoTipo("Incêndio"));
                context.EventosTipos.Add(new EventoTipo("Roubo"));
                context.EventosTipos.Add(new EventoTipo("Levantamento Documento Vida"));
                context.SaveChanges();
            }

            if (!context.ServicosSeguradoras.Any())
            {
                var eventoTipoList = context.EventosTipos.ToList();
                var tipoServico = new ServicoSeguradora("Auto", eventoTipoList);
                var tipoServico2 = new ServicoSeguradora("Bens RE", eventoTipoList);
                var tipoServico3 = new ServicoSeguradora("Vida", eventoTipoList);

                context.ServicosSeguradoras.Add(tipoServico);
                context.ServicosSeguradoras.Add(tipoServico2);
                context.ServicosSeguradoras.Add(tipoServico3);

                context.SaveChanges();
            }

            if (!context.Perfis.Any())
            {
                var admin = new Perfil("Administrador");
                var gestor = new Perfil("Gestor");
                var financeiro = new Perfil("Financeiro");
                var administativo = new Perfil("Administrativo");
                var analista = new Perfil("Analista");

                context.Perfis.Add(admin);
                context.Perfis.Add(gestor);
                context.Perfis.Add(financeiro);
                context.Perfis.Add(administativo);
                context.Perfis.Add(analista);
                context.SaveChanges();
            }


            if (!context.Usuarios.Any())
            {
                var perfil = context.Perfis.Find(1);
                var perfis = new List<Perfil>() { perfil };
                var usuario = new Usuario("Administrador Master", "admin@admin.com", perfis);

                context.Usuarios.Add(usuario);
                context.SaveChanges(usuario);
            }

            if (!context.SindicantesTipos.Any())
            {
                var sindicanteTipo = new SindicanteTipo("Interno");
                var sindicanteTipo2 = new SindicanteTipo("Externo");

                context.SindicantesTipos.Add(sindicanteTipo);
                context.SindicantesTipos.Add(sindicanteTipo2);
                context.SaveChanges();
            }

            if (!context.Status.Any())
            {
                var status = new Status("Em análise");
                var status2 = new Status("Aguardando Emissão de NF");
                var status3 = new Status("Enviado para seguradora");
                var status4 = new Status("Finalizado");
                var status5 = new Status("Análise Finalizada");
                var status6 = new Status("Cancelado");
                var status7 = new Status("Enviado Sindicante Externo");
                var status8 = new Status("Enviado Financeiro");


                context.Status.Add(status);
                context.Status.Add(status2);
                context.Status.Add(status3);
                context.Status.Add(status4);
                context.Status.Add(status5);
                context.Status.Add(status6);
                context.Status.Add(status7);
                context.Status.Add(status8);
                context.SaveChanges();
            }

            
            if (!context.ServicosSindicantes.Any())
            {
                var sc = new ServicoSindicante("Sindicância Completa");
                var msc = new ServicoSindicante("Meia Sindicância");
                var lbo = new ServicoSindicante("Levantamento de B.O.");

                context.ServicosSindicantes.Add(sc);
                context.ServicosSindicantes.Add(msc);
                context.ServicosSindicantes.Add(lbo);

                context.SaveChanges();
            }
        }
    }
}
