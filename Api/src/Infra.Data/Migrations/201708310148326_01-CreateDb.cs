namespace Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _01CreateDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditLog",
                c => new
                    {
                        AuditLogId = c.Long(nullable: false, identity: true),
                        UserName = c.String(),
                        EventDateUTC = c.DateTime(nullable: false),
                        EventType = c.Int(nullable: false),
                        TypeFullName = c.String(nullable: false, maxLength: 512),
                        RecordId = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.AuditLogId);
            
            CreateTable(
                "dbo.AuditLogDetail",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PropertyName = c.String(nullable: false, maxLength: 256),
                        OriginalValue = c.String(),
                        NewValue = c.String(),
                        AuditLogId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AuditLog", t => t.AuditLogId)
                .Index(t => t.AuditLogId);
            
            CreateTable(
                "dbo.LogMetadata",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AuditLogId = c.Long(nullable: false),
                        Key = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AuditLog", t => t.AuditLogId)
                .Index(t => t.AuditLogId);
            
            CreateTable(
                "dbo.Banco",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Numero = c.String(nullable: false, maxLength: 3, fixedLength: true, unicode: false),
                        Nome = c.String(nullable: false, maxLength: 200, unicode: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cidade",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        EstadoId = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estado", t => t.EstadoId)
                .Index(t => t.EstadoId);
            
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        Uf = c.String(nullable: false, maxLength: 2, fixedLength: true, unicode: false),
                        PaisId = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pais", t => t.PaisId)
                .Index(t => t.PaisId);
            
            CreateTable(
                "dbo.Pais",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        Sigla = c.String(nullable: false, maxLength: 2, fixedLength: true, unicode: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DadosBancarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BancoId = c.Int(nullable: false),
                        ContaTipo = c.Int(nullable: false),
                        Agencia = c.String(nullable: false, maxLength: 4, fixedLength: true, unicode: false),
                        Conta = c.String(nullable: false, maxLength: 20, unicode: false),
                        Digito = c.String(maxLength: 1, fixedLength: true, unicode: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Banco", t => t.BancoId)
                .Index(t => t.BancoId);
            
            CreateTable(
                "dbo.DadosCadastrais",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Despesa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcessoId = c.Int(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 50, unicode: false),
                        Data = c.DateTime(nullable: false, storeType: "date"),
                        EnderecoOrigemId = c.Int(nullable: false),
                        EnderecoDestinoId = c.Int(nullable: false),
                        Kms = c.Decimal(nullable: false, precision: 5, scale: 1),
                        ValorKm = c.Decimal(nullable: false, precision: 11, scale: 2),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EnderecoDespesa", t => t.EnderecoDestinoId)
                .ForeignKey("dbo.EnderecoDespesa", t => t.EnderecoOrigemId)
                .ForeignKey("dbo.Processo", t => t.ProcessoId)
                .Index(t => t.ProcessoId)
                .Index(t => t.EnderecoOrigemId)
                .Index(t => t.EnderecoDestinoId);
            
            CreateTable(
                "dbo.DespesaAdicional",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DespesaTipoId = c.Int(nullable: false),
                        DespesaId = c.Int(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 6, scale: 2),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Despesa", t => t.DespesaId, cascadeDelete: true)
                .Index(t => t.DespesaId);
            
            CreateTable(
                "dbo.EnderecoDespesa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EnderecoId = c.Int(nullable: false),
                        Numero = c.String(nullable: false, maxLength: 6, fixedLength: true, unicode: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Endereco", t => t.EnderecoId)
                .Index(t => t.EnderecoId);
            
            CreateTable(
                "dbo.Endereco",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Logradouro = c.String(nullable: false, maxLength: 200, unicode: false),
                        LogradouroFonetizado = c.String(nullable: false, maxLength: 200, unicode: false),
                        Cep = c.String(nullable: false, maxLength: 8, fixedLength: true, unicode: false),
                        Bairro = c.String(maxLength: 200, unicode: false),
                        CidadeId = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cidade", t => t.CidadeId)
                .Index(t => t.CidadeId);
            
            CreateTable(
                "dbo.DespesaSeguradora",
                c => new
                    {
                        NumeroRecibo = c.Int(nullable: false, identity: true),
                        ProcessoId = c.Int(nullable: false),
                        Cia = c.String(nullable: false, maxLength: 200, unicode: false),
                        Sinistro = c.String(nullable: false, maxLength: 50, unicode: false),
                        Segurado = c.String(nullable: false, maxLength: 200, unicode: false),
                        Analista = c.String(nullable: false, maxLength: 200, unicode: false),
                        DataConclusao = c.DateTime(nullable: false, storeType: "date"),
                        ValorKm = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.NumeroRecibo)
                .ForeignKey("dbo.Processo", t => t.ProcessoId)
                .Index(t => t.ProcessoId);
            
            CreateTable(
                "dbo.DespesaSeguradoraItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DespesaSeguradoraId = c.Int(nullable: false),
                        Data = c.DateTime(nullable: false, storeType: "date"),
                        Descricao = c.String(maxLength: 50, unicode: false),
                        Endereco = c.String(nullable: false, maxLength: 200, unicode: false),
                        KmPercorrido = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorKmPercorrido = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransportePedagio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HospedagemRefeicao = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DespesaSeguradora", t => t.DespesaSeguradoraId, cascadeDelete: true)
                .Index(t => t.DespesaSeguradoraId);
            
            CreateTable(
                "dbo.Processo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SeguradoraId = c.Int(nullable: false),
                        SeguradoraServicoId = c.Int(nullable: false),
                        EventoTipoId = c.Int(nullable: false),
                        UsuarioResponsavelId = c.Int(),
                        SindicanteServicoId = c.Int(nullable: false),
                        NumeroSinistro = c.String(nullable: false, maxLength: 30, unicode: false),
                        NumeroReferencia = c.String(maxLength: 30, unicode: false),
                        Analista = c.String(nullable: false, maxLength: 100, unicode: false),
                        Placa = c.String(maxLength: 8, fixedLength: true, unicode: false),
                        CidadeId = c.Int(nullable: false),
                        SeguradoId = c.Int(nullable: false),
                        Situacao = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        FinalizadaAnaliseEm = c.DateTime(precision: 3, storeType: "datetime2"),
                        EmitidoParecerEm = c.DateTime(precision: 3, storeType: "datetime2"),
                        FinalizadoEm = c.DateTime(precision: 3, storeType: "datetime2"),
                        Comentario = c.String(maxLength: 500, unicode: false),
                        AcionamentoId = c.Int(),
                        NumeroNF = c.Int(),
                        DataEmissaoNF = c.DateTime(precision: 3, storeType: "datetime2"),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Acionamento", t => t.AcionamentoId)
                .ForeignKey("dbo.Cidade", t => t.CidadeId)
                .ForeignKey("dbo.EventoTipo", t => t.EventoTipoId)
                .ForeignKey("dbo.Segurado", t => t.SeguradoId)
                .ForeignKey("dbo.Seguradora", t => t.SeguradoraId)
                .ForeignKey("dbo.ServicoSeguradora", t => t.SeguradoraServicoId)
                .ForeignKey("dbo.ServicoSindicante", t => t.SindicanteServicoId)
                .ForeignKey("dbo.Status", t => t.Status)
                .ForeignKey("dbo.Usuario", t => t.UsuarioResponsavelId)
                .Index(t => t.SeguradoraId)
                .Index(t => t.SeguradoraServicoId)
                .Index(t => t.EventoTipoId)
                .Index(t => t.UsuarioResponsavelId)
                .Index(t => t.SindicanteServicoId)
                .Index(t => t.CidadeId)
                .Index(t => t.SeguradoId)
                .Index(t => t.Status)
                .Index(t => t.AcionamentoId);
            
            CreateTable(
                "dbo.Acionamento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        Arquivo = c.Binary(nullable: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EventoTipo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServicoSeguradora",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProcessoHistorico",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SeguradoraId = c.Int(nullable: false),
                        ProcessoId = c.Int(nullable: false),
                        SindicanteId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        IniciadoEm = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        FinalizadoEm = c.DateTime(precision: 3, storeType: "datetime2"),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Seguradora", t => t.SeguradoraId)
                .ForeignKey("dbo.Sindicante", t => t.SindicanteId)
                .ForeignKey("dbo.Status", t => t.StatusId)
                .ForeignKey("dbo.Processo", t => t.ProcessoId)
                .Index(t => t.SeguradoraId)
                .Index(t => t.ProcessoId)
                .Index(t => t.SindicanteId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.Seguradora",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DadosPessoaJuridicaId = c.Int(nullable: false),
                        Telefone = c.String(nullable: false, maxLength: 10, fixedLength: true, unicode: false),
                        Contato = c.String(nullable: false, maxLength: 150, unicode: false),
                        Email = c.String(nullable: false, maxLength: 150, unicode: false),
                        ValorPorKm = c.Decimal(nullable: false, precision: 9, scale: 2),
                        Observacoes = c.String(maxLength: 500, unicode: false),
                        UrlAdministrativo = c.String(nullable: false, maxLength: 256, unicode: false),
                        UrlFinanceiro = c.String(nullable: false, maxLength: 256, unicode: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DadosPessoaJuridica", t => t.DadosPessoaJuridicaId)
                .Index(t => t.DadosPessoaJuridicaId)
                .Index(t => t.Telefone, unique: true, name: "IX_Telefone_Unique")
                .Index(t => t.Email, unique: true, name: "IX_Email_Unique");
            
            CreateTable(
                "dbo.Evento",
                c => new
                    {
                        SeguradoraId = c.Int(nullable: false),
                        ServicoSeguradoraId = c.Int(nullable: false),
                        EventoTipoId = c.Int(nullable: false),
                        FranquiaKm = c.Int(nullable: false),
                        Honorario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.SeguradoraId, t.ServicoSeguradoraId, t.EventoTipoId })
                .ForeignKey("dbo.EventoTipo", t => t.EventoTipoId)
                .ForeignKey("dbo.ServicoSeguradora", t => t.ServicoSeguradoraId)
                .ForeignKey("dbo.Seguradora", t => t.SeguradoraId)
                .Index(t => t.SeguradoraId)
                .Index(t => t.ServicoSeguradoraId)
                .Index(t => t.EventoTipoId);
            
            CreateTable(
                "dbo.Sindicante",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SindicanteTipoId = c.Int(nullable: false),
                        DadosBancariosId = c.Int(nullable: false),
                        DadosPessoaFisicaId = c.Int(nullable: false),
                        Telefone = c.String(nullable: false, maxLength: 10, fixedLength: true, unicode: false),
                        Celular = c.String(nullable: false, maxLength: 11, fixedLength: true, unicode: false),
                        Email = c.String(nullable: false, maxLength: 150, unicode: false),
                        ValorPorKm = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DadosBancarios", t => t.DadosBancariosId)
                .ForeignKey("dbo.DadosPessoaFisica", t => t.DadosPessoaFisicaId)
                .ForeignKey("dbo.SindicanteTipo", t => t.SindicanteTipoId)
                .Index(t => t.SindicanteTipoId)
                .Index(t => t.DadosBancariosId)
                .Index(t => t.DadosPessoaFisicaId)
                .Index(t => t.Telefone, unique: true, name: "IX_Telefone_Unique")
                .Index(t => t.Celular, unique: true, name: "IX_Celular_Unique")
                .Index(t => t.Email, unique: true, name: "IX_Email_Unique");
            
            CreateTable(
                "dbo.EnderecoSindicante",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EnderecoTipoId = c.Int(nullable: false),
                        EnderecoId = c.Int(nullable: false),
                        Numero = c.String(nullable: false, maxLength: 6, fixedLength: true, unicode: false),
                        SindicanteId = c.Int(nullable: false),
                        Complemento = c.String(maxLength: 200, unicode: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Endereco", t => t.EnderecoId)
                .ForeignKey("dbo.EnderecoTipo", t => t.EnderecoTipoId)
                .ForeignKey("dbo.Sindicante", t => t.SindicanteId)
                .Index(t => t.EnderecoTipoId)
                .Index(t => t.EnderecoId)
                .Index(t => t.SindicanteId);
            
            CreateTable(
                "dbo.EnderecoTipo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Honorario",
                c => new
                    {
                        SindicanteId = c.Int(nullable: false),
                        ServicoSeguradoraId = c.Int(nullable: false),
                        EventoTipoId = c.Int(nullable: false),
                        ServicoSindicanteId = c.Int(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 5, scale: 2),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.SindicanteId, t.ServicoSeguradoraId, t.EventoTipoId, t.ServicoSindicanteId })
                .ForeignKey("dbo.EventoTipo", t => t.EventoTipoId)
                .ForeignKey("dbo.ServicoSeguradora", t => t.ServicoSeguradoraId)
                .ForeignKey("dbo.ServicoSindicante", t => t.ServicoSindicanteId)
                .ForeignKey("dbo.Sindicante", t => t.SindicanteId)
                .Index(t => t.SindicanteId)
                .Index(t => t.ServicoSeguradoraId)
                .Index(t => t.EventoTipoId)
                .Index(t => t.ServicoSindicanteId);
            
            CreateTable(
                "dbo.ServicoSindicante",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SindicanteTipo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SindicanteTipo = c.String(nullable: false, maxLength: 20, unicode: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 150, unicode: false),
                        Login = c.String(nullable: false, maxLength: 50, unicode: false),
                        Senha = c.String(nullable: false, maxLength: 32, unicode: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Login, unique: true, name: "IX_Login_Unique");
            
            CreateTable(
                "dbo.Perfil",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Segurado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 150, unicode: false),
                        EnderecoSeguradoId = c.Int(),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EnderecoSegurado", t => t.EnderecoSeguradoId)
                .Index(t => t.EnderecoSeguradoId);
            
            CreateTable(
                "dbo.EnderecoSegurado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EnderecoTipoId = c.Int(nullable: false),
                        EnderecoId = c.Int(nullable: false),
                        Numero = c.String(nullable: false, maxLength: 6, fixedLength: true, unicode: false),
                        Complemento = c.String(maxLength: 200, unicode: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Endereco", t => t.EnderecoId)
                .ForeignKey("dbo.EnderecoTipo", t => t.EnderecoTipoId)
                .Index(t => t.EnderecoTipoId)
                .Index(t => t.EnderecoId);
            
            CreateTable(
                "dbo.Lancamento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReciboId = c.Int(nullable: false),
                        LancamentoTipo = c.Int(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 100, unicode: false),
                        Valor = c.Decimal(nullable: false, precision: 11, scale: 2),
                        Observacao = c.String(maxLength: 200, unicode: false),
                        TipoFinanceiro = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recibo", t => t.ReciboId)
                .Index(t => t.ReciboId);
            
            CreateTable(
                "dbo.Recibo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcessoId = c.Int(nullable: false),
                        SindicanteId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Processo", t => t.ProcessoId)
                .ForeignKey("dbo.Sindicante", t => t.SindicanteId)
                .Index(t => t.ProcessoId)
                .Index(t => t.SindicanteId);
            
            CreateTable(
                "dbo.ReciboSindicante",
                c => new
                    {
                        NumeroRecibo = c.Int(nullable: false, identity: true),
                        SindicanteId = c.Int(nullable: false),
                        Sindicante = c.String(nullable: false, maxLength: 200, unicode: false),
                        Cpf = c.String(nullable: false, maxLength: 11, unicode: false),
                        DadosBancarios = c.String(nullable: false, maxLength: 100, unicode: false),
                        DataInicio = c.DateTime(nullable: false, storeType: "date"),
                        DataFim = c.DateTime(nullable: false, storeType: "date"),
                        Emitido = c.Boolean(nullable: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.NumeroRecibo)
                .ForeignKey("dbo.Sindicante", t => t.SindicanteId)
                .Index(t => t.SindicanteId);
            
            CreateTable(
                "dbo.ReciboSindicanteItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReciboSindicanteId = c.Int(nullable: false),
                        Segurado = c.String(maxLength: 50, unicode: false),
                        Sinistro = c.String(maxLength: 50, unicode: false),
                        Seguradora = c.String(maxLength: 50, unicode: false),
                        Despesa = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Honorario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Desconto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OutrosCreditos = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataTerminoTrabalho = c.DateTime(nullable: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ReciboSindicante", t => t.ReciboSindicanteId)
                .Index(t => t.ReciboSindicanteId);
            
            CreateTable(
                "dbo.LocalFatos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EnderecoTipoId = c.Int(nullable: false),
                        EnderecoId = c.Int(nullable: false),
                        Numero = c.String(nullable: false, maxLength: 6, fixedLength: true, unicode: false),
                        Complemento = c.String(maxLength: 200, unicode: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Endereco", t => t.EnderecoId)
                .ForeignKey("dbo.EnderecoTipo", t => t.EnderecoTipoId)
                .Index(t => t.EnderecoTipoId)
                .Index(t => t.EnderecoId);
            
            CreateTable(
                "dbo.ServicoSeguradoraEventoTipo",
                c => new
                    {
                        ServicoSeguradoraId = c.Int(nullable: false),
                        EventoTipoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ServicoSeguradoraId, t.EventoTipoId })
                .ForeignKey("dbo.ServicoSeguradora", t => t.ServicoSeguradoraId)
                .ForeignKey("dbo.EventoTipo", t => t.EventoTipoId)
                .Index(t => t.ServicoSeguradoraId)
                .Index(t => t.EventoTipoId);
            
            CreateTable(
                "dbo.UsuarioPerfil",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false),
                        PerfilId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UsuarioId, t.PerfilId })
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .ForeignKey("dbo.Perfil", t => t.PerfilId)
                .Index(t => t.UsuarioId)
                .Index(t => t.PerfilId);
            
            CreateTable(
                "dbo.ProcessoSindicante",
                c => new
                    {
                        ProcessoId = c.Int(nullable: false),
                        SindicanteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProcessoId, t.SindicanteId })
                .ForeignKey("dbo.Processo", t => t.ProcessoId)
                .ForeignKey("dbo.Sindicante", t => t.SindicanteId)
                .Index(t => t.ProcessoId)
                .Index(t => t.SindicanteId);
            
            CreateTable(
                "dbo.DadosPessoaFisica",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 150, unicode: false),
                        Cpf = c.String(nullable: false, maxLength: 11, fixedLength: true, unicode: false),
                        Rg = c.String(maxLength: 20, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DadosCadastrais", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.Cpf, unique: true, name: "IX_Cpf_Unique");
            
            CreateTable(
                "dbo.DadosPessoaJuridica",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        RazaoSocial = c.String(nullable: false, maxLength: 200, unicode: false),
                        NomeFantasia = c.String(maxLength: 200, unicode: false),
                        Cnpj = c.String(nullable: false, maxLength: 14, fixedLength: true, unicode: false),
                        InscEst = c.String(maxLength: 20, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DadosCadastrais", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.Cnpj, unique: true, name: "IX_Cnpj_Unique");
            
            CreateTable(
                "dbo.SindicanteInterno",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sindicante", t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.Id)
                .Index(t => t.UsuarioId, unique: true);
            
            CreateTable(
                "dbo.LancamentoDespesa",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        DespesaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lancamento", t => t.Id)
                .ForeignKey("dbo.Despesa", t => t.DespesaId)
                .Index(t => t.Id)
                .Index(t => t.DespesaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LancamentoDespesa", "DespesaId", "dbo.Despesa");
            DropForeignKey("dbo.LancamentoDespesa", "Id", "dbo.Lancamento");
            DropForeignKey("dbo.SindicanteInterno", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.SindicanteInterno", "Id", "dbo.Sindicante");
            DropForeignKey("dbo.DadosPessoaJuridica", "Id", "dbo.DadosCadastrais");
            DropForeignKey("dbo.DadosPessoaFisica", "Id", "dbo.DadosCadastrais");
            DropForeignKey("dbo.LocalFatos", "EnderecoTipoId", "dbo.EnderecoTipo");
            DropForeignKey("dbo.LocalFatos", "EnderecoId", "dbo.Endereco");
            DropForeignKey("dbo.ReciboSindicante", "SindicanteId", "dbo.Sindicante");
            DropForeignKey("dbo.ReciboSindicanteItem", "ReciboSindicanteId", "dbo.ReciboSindicante");
            DropForeignKey("dbo.Recibo", "SindicanteId", "dbo.Sindicante");
            DropForeignKey("dbo.Recibo", "ProcessoId", "dbo.Processo");
            DropForeignKey("dbo.Lancamento", "ReciboId", "dbo.Recibo");
            DropForeignKey("dbo.DespesaSeguradora", "ProcessoId", "dbo.Processo");
            DropForeignKey("dbo.Processo", "UsuarioResponsavelId", "dbo.Usuario");
            DropForeignKey("dbo.Processo", "Status", "dbo.Status");
            DropForeignKey("dbo.ProcessoSindicante", "SindicanteId", "dbo.Sindicante");
            DropForeignKey("dbo.ProcessoSindicante", "ProcessoId", "dbo.Processo");
            DropForeignKey("dbo.Processo", "SindicanteServicoId", "dbo.ServicoSindicante");
            DropForeignKey("dbo.Processo", "SeguradoraServicoId", "dbo.ServicoSeguradora");
            DropForeignKey("dbo.Processo", "SeguradoraId", "dbo.Seguradora");
            DropForeignKey("dbo.Processo", "SeguradoId", "dbo.Segurado");
            DropForeignKey("dbo.Segurado", "EnderecoSeguradoId", "dbo.EnderecoSegurado");
            DropForeignKey("dbo.EnderecoSegurado", "EnderecoTipoId", "dbo.EnderecoTipo");
            DropForeignKey("dbo.EnderecoSegurado", "EnderecoId", "dbo.Endereco");
            DropForeignKey("dbo.ProcessoHistorico", "ProcessoId", "dbo.Processo");
            DropForeignKey("dbo.ProcessoHistorico", "StatusId", "dbo.Status");
            DropForeignKey("dbo.ProcessoHistorico", "SindicanteId", "dbo.Sindicante");
            DropForeignKey("dbo.UsuarioPerfil", "PerfilId", "dbo.Perfil");
            DropForeignKey("dbo.UsuarioPerfil", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.Sindicante", "SindicanteTipoId", "dbo.SindicanteTipo");
            DropForeignKey("dbo.Honorario", "SindicanteId", "dbo.Sindicante");
            DropForeignKey("dbo.Honorario", "ServicoSindicanteId", "dbo.ServicoSindicante");
            DropForeignKey("dbo.Honorario", "ServicoSeguradoraId", "dbo.ServicoSeguradora");
            DropForeignKey("dbo.Honorario", "EventoTipoId", "dbo.EventoTipo");
            DropForeignKey("dbo.EnderecoSindicante", "SindicanteId", "dbo.Sindicante");
            DropForeignKey("dbo.EnderecoSindicante", "EnderecoTipoId", "dbo.EnderecoTipo");
            DropForeignKey("dbo.EnderecoSindicante", "EnderecoId", "dbo.Endereco");
            DropForeignKey("dbo.Sindicante", "DadosPessoaFisicaId", "dbo.DadosPessoaFisica");
            DropForeignKey("dbo.Sindicante", "DadosBancariosId", "dbo.DadosBancarios");
            DropForeignKey("dbo.ProcessoHistorico", "SeguradoraId", "dbo.Seguradora");
            DropForeignKey("dbo.Evento", "SeguradoraId", "dbo.Seguradora");
            DropForeignKey("dbo.Evento", "ServicoSeguradoraId", "dbo.ServicoSeguradora");
            DropForeignKey("dbo.Evento", "EventoTipoId", "dbo.EventoTipo");
            DropForeignKey("dbo.Seguradora", "DadosPessoaJuridicaId", "dbo.DadosPessoaJuridica");
            DropForeignKey("dbo.Processo", "EventoTipoId", "dbo.EventoTipo");
            DropForeignKey("dbo.ServicoSeguradoraEventoTipo", "EventoTipoId", "dbo.EventoTipo");
            DropForeignKey("dbo.ServicoSeguradoraEventoTipo", "ServicoSeguradoraId", "dbo.ServicoSeguradora");
            DropForeignKey("dbo.Despesa", "ProcessoId", "dbo.Processo");
            DropForeignKey("dbo.Processo", "CidadeId", "dbo.Cidade");
            DropForeignKey("dbo.Processo", "AcionamentoId", "dbo.Acionamento");
            DropForeignKey("dbo.DespesaSeguradoraItem", "DespesaSeguradoraId", "dbo.DespesaSeguradora");
            DropForeignKey("dbo.Despesa", "EnderecoOrigemId", "dbo.EnderecoDespesa");
            DropForeignKey("dbo.Despesa", "EnderecoDestinoId", "dbo.EnderecoDespesa");
            DropForeignKey("dbo.EnderecoDespesa", "EnderecoId", "dbo.Endereco");
            DropForeignKey("dbo.Endereco", "CidadeId", "dbo.Cidade");
            DropForeignKey("dbo.DespesaAdicional", "DespesaId", "dbo.Despesa");
            DropForeignKey("dbo.DadosBancarios", "BancoId", "dbo.Banco");
            DropForeignKey("dbo.Cidade", "EstadoId", "dbo.Estado");
            DropForeignKey("dbo.Estado", "PaisId", "dbo.Pais");
            DropForeignKey("dbo.LogMetadata", "AuditLogId", "dbo.AuditLog");
            DropForeignKey("dbo.AuditLogDetail", "AuditLogId", "dbo.AuditLog");
            DropIndex("dbo.LancamentoDespesa", new[] { "DespesaId" });
            DropIndex("dbo.LancamentoDespesa", new[] { "Id" });
            DropIndex("dbo.SindicanteInterno", new[] { "UsuarioId" });
            DropIndex("dbo.SindicanteInterno", new[] { "Id" });
            DropIndex("dbo.DadosPessoaJuridica", "IX_Cnpj_Unique");
            DropIndex("dbo.DadosPessoaJuridica", new[] { "Id" });
            DropIndex("dbo.DadosPessoaFisica", "IX_Cpf_Unique");
            DropIndex("dbo.DadosPessoaFisica", new[] { "Id" });
            DropIndex("dbo.ProcessoSindicante", new[] { "SindicanteId" });
            DropIndex("dbo.ProcessoSindicante", new[] { "ProcessoId" });
            DropIndex("dbo.UsuarioPerfil", new[] { "PerfilId" });
            DropIndex("dbo.UsuarioPerfil", new[] { "UsuarioId" });
            DropIndex("dbo.ServicoSeguradoraEventoTipo", new[] { "EventoTipoId" });
            DropIndex("dbo.ServicoSeguradoraEventoTipo", new[] { "ServicoSeguradoraId" });
            DropIndex("dbo.LocalFatos", new[] { "EnderecoId" });
            DropIndex("dbo.LocalFatos", new[] { "EnderecoTipoId" });
            DropIndex("dbo.ReciboSindicanteItem", new[] { "ReciboSindicanteId" });
            DropIndex("dbo.ReciboSindicante", new[] { "SindicanteId" });
            DropIndex("dbo.Recibo", new[] { "SindicanteId" });
            DropIndex("dbo.Recibo", new[] { "ProcessoId" });
            DropIndex("dbo.Lancamento", new[] { "ReciboId" });
            DropIndex("dbo.EnderecoSegurado", new[] { "EnderecoId" });
            DropIndex("dbo.EnderecoSegurado", new[] { "EnderecoTipoId" });
            DropIndex("dbo.Segurado", new[] { "EnderecoSeguradoId" });
            DropIndex("dbo.Usuario", "IX_Login_Unique");
            DropIndex("dbo.Honorario", new[] { "ServicoSindicanteId" });
            DropIndex("dbo.Honorario", new[] { "EventoTipoId" });
            DropIndex("dbo.Honorario", new[] { "ServicoSeguradoraId" });
            DropIndex("dbo.Honorario", new[] { "SindicanteId" });
            DropIndex("dbo.EnderecoSindicante", new[] { "SindicanteId" });
            DropIndex("dbo.EnderecoSindicante", new[] { "EnderecoId" });
            DropIndex("dbo.EnderecoSindicante", new[] { "EnderecoTipoId" });
            DropIndex("dbo.Sindicante", "IX_Email_Unique");
            DropIndex("dbo.Sindicante", "IX_Celular_Unique");
            DropIndex("dbo.Sindicante", "IX_Telefone_Unique");
            DropIndex("dbo.Sindicante", new[] { "DadosPessoaFisicaId" });
            DropIndex("dbo.Sindicante", new[] { "DadosBancariosId" });
            DropIndex("dbo.Sindicante", new[] { "SindicanteTipoId" });
            DropIndex("dbo.Evento", new[] { "EventoTipoId" });
            DropIndex("dbo.Evento", new[] { "ServicoSeguradoraId" });
            DropIndex("dbo.Evento", new[] { "SeguradoraId" });
            DropIndex("dbo.Seguradora", "IX_Email_Unique");
            DropIndex("dbo.Seguradora", "IX_Telefone_Unique");
            DropIndex("dbo.Seguradora", new[] { "DadosPessoaJuridicaId" });
            DropIndex("dbo.ProcessoHistorico", new[] { "StatusId" });
            DropIndex("dbo.ProcessoHistorico", new[] { "SindicanteId" });
            DropIndex("dbo.ProcessoHistorico", new[] { "ProcessoId" });
            DropIndex("dbo.ProcessoHistorico", new[] { "SeguradoraId" });
            DropIndex("dbo.Processo", new[] { "AcionamentoId" });
            DropIndex("dbo.Processo", new[] { "Status" });
            DropIndex("dbo.Processo", new[] { "SeguradoId" });
            DropIndex("dbo.Processo", new[] { "CidadeId" });
            DropIndex("dbo.Processo", new[] { "SindicanteServicoId" });
            DropIndex("dbo.Processo", new[] { "UsuarioResponsavelId" });
            DropIndex("dbo.Processo", new[] { "EventoTipoId" });
            DropIndex("dbo.Processo", new[] { "SeguradoraServicoId" });
            DropIndex("dbo.Processo", new[] { "SeguradoraId" });
            DropIndex("dbo.DespesaSeguradoraItem", new[] { "DespesaSeguradoraId" });
            DropIndex("dbo.DespesaSeguradora", new[] { "ProcessoId" });
            DropIndex("dbo.Endereco", new[] { "CidadeId" });
            DropIndex("dbo.EnderecoDespesa", new[] { "EnderecoId" });
            DropIndex("dbo.DespesaAdicional", new[] { "DespesaId" });
            DropIndex("dbo.Despesa", new[] { "EnderecoDestinoId" });
            DropIndex("dbo.Despesa", new[] { "EnderecoOrigemId" });
            DropIndex("dbo.Despesa", new[] { "ProcessoId" });
            DropIndex("dbo.DadosBancarios", new[] { "BancoId" });
            DropIndex("dbo.Estado", new[] { "PaisId" });
            DropIndex("dbo.Cidade", new[] { "EstadoId" });
            DropIndex("dbo.LogMetadata", new[] { "AuditLogId" });
            DropIndex("dbo.AuditLogDetail", new[] { "AuditLogId" });
            DropTable("dbo.LancamentoDespesa");
            DropTable("dbo.SindicanteInterno");
            DropTable("dbo.DadosPessoaJuridica");
            DropTable("dbo.DadosPessoaFisica");
            DropTable("dbo.ProcessoSindicante");
            DropTable("dbo.UsuarioPerfil");
            DropTable("dbo.ServicoSeguradoraEventoTipo");
            DropTable("dbo.LocalFatos");
            DropTable("dbo.ReciboSindicanteItem");
            DropTable("dbo.ReciboSindicante");
            DropTable("dbo.Recibo");
            DropTable("dbo.Lancamento");
            DropTable("dbo.EnderecoSegurado");
            DropTable("dbo.Segurado");
            DropTable("dbo.Status");
            DropTable("dbo.Perfil");
            DropTable("dbo.Usuario");
            DropTable("dbo.SindicanteTipo");
            DropTable("dbo.ServicoSindicante");
            DropTable("dbo.Honorario");
            DropTable("dbo.EnderecoTipo");
            DropTable("dbo.EnderecoSindicante");
            DropTable("dbo.Sindicante");
            DropTable("dbo.Evento");
            DropTable("dbo.Seguradora");
            DropTable("dbo.ProcessoHistorico");
            DropTable("dbo.ServicoSeguradora");
            DropTable("dbo.EventoTipo");
            DropTable("dbo.Acionamento");
            DropTable("dbo.Processo");
            DropTable("dbo.DespesaSeguradoraItem");
            DropTable("dbo.DespesaSeguradora");
            DropTable("dbo.Endereco");
            DropTable("dbo.EnderecoDespesa");
            DropTable("dbo.DespesaAdicional");
            DropTable("dbo.Despesa");
            DropTable("dbo.DadosCadastrais");
            DropTable("dbo.DadosBancarios");
            DropTable("dbo.Pais");
            DropTable("dbo.Estado");
            DropTable("dbo.Cidade");
            DropTable("dbo.Banco");
            DropTable("dbo.LogMetadata");
            DropTable("dbo.AuditLogDetail");
            DropTable("dbo.AuditLog");
        }
    }
}
