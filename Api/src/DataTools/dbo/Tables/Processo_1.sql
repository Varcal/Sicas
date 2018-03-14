CREATE TABLE [dbo].[Processo] (
    [Id]                   INT           IDENTITY (1, 1) NOT NULL,
    [SeguradoraId]         INT           NOT NULL,
    [SeguradoraServicoId]  INT           NOT NULL,
    [EventoTipoId]         INT           NOT NULL,
    [UsuarioResponsavelId] INT           NULL,
    [SindicanteServicoId]  INT           NOT NULL,
    [NumeroSinistro]       VARCHAR (30)  NOT NULL,
    [NumeroReferencia]     VARCHAR (30)  NULL,
    [Analista]             VARCHAR (100) NOT NULL,
    [Placa]                CHAR (8)      NULL,
    [CidadeId]             INT           NOT NULL,
    [SeguradoId]           INT           NOT NULL,
    [Situacao]             INT           NOT NULL,
    [Status]               INT           NOT NULL,
    [FinalizadaAnaliseEm]  DATETIME2 (3) NULL,
    [EmitidoParecerEm]     DATETIME2 (3) NULL,
    [FinalizadoEm]         DATETIME2 (3) NULL,
    [Comentario]           VARCHAR (500) NULL,
    [AcionamentoId]        INT           NULL,
    [NumeroNF]             INT           NULL,
    [DataEmissaoNF]        DATETIME2 (3) NULL,
    [DataCadastro]         DATETIME2 (3) NOT NULL,
    [Ativo]                BIT           NOT NULL,
    CONSTRAINT [PK_dbo.Processo] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Processo_dbo.Acionamento_AcionamentoId] FOREIGN KEY ([AcionamentoId]) REFERENCES [dbo].[Acionamento] ([Id]),
    CONSTRAINT [FK_dbo.Processo_dbo.Cidade_CidadeId] FOREIGN KEY ([CidadeId]) REFERENCES [dbo].[Cidade] ([Id]),
    CONSTRAINT [FK_dbo.Processo_dbo.EventoTipo_EventoTipoId] FOREIGN KEY ([EventoTipoId]) REFERENCES [dbo].[EventoTipo] ([Id]),
    CONSTRAINT [FK_dbo.Processo_dbo.Segurado_SeguradoId] FOREIGN KEY ([SeguradoId]) REFERENCES [dbo].[Segurado] ([Id]),
    CONSTRAINT [FK_dbo.Processo_dbo.Seguradora_SeguradoraId] FOREIGN KEY ([SeguradoraId]) REFERENCES [dbo].[Seguradora] ([Id]),
    CONSTRAINT [FK_dbo.Processo_dbo.ServicoSeguradora_SeguradoraServicoId] FOREIGN KEY ([SeguradoraServicoId]) REFERENCES [dbo].[ServicoSeguradora] ([Id]),
    CONSTRAINT [FK_dbo.Processo_dbo.ServicoSindicante_SindicanteServicoId] FOREIGN KEY ([SindicanteServicoId]) REFERENCES [dbo].[ServicoSindicante] ([Id]),
    CONSTRAINT [FK_dbo.Processo_dbo.Status_Status] FOREIGN KEY ([Status]) REFERENCES [dbo].[Status] ([Id]),
    CONSTRAINT [FK_dbo.Processo_dbo.Usuario_UsuarioResponsavelId] FOREIGN KEY ([UsuarioResponsavelId]) REFERENCES [dbo].[Usuario] ([Id])
);














GO
CREATE NONCLUSTERED INDEX [IX_SeguradoraId]
    ON [dbo].[Processo]([SeguradoraId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SeguradoraServicoId]
    ON [dbo].[Processo]([SeguradoraServicoId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_EventoTipoId]
    ON [dbo].[Processo]([EventoTipoId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UsuarioResponsavelId]
    ON [dbo].[Processo]([UsuarioResponsavelId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SindicanteServicoId]
    ON [dbo].[Processo]([SindicanteServicoId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Status]
    ON [dbo].[Processo]([Status] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SeguradoId]
    ON [dbo].[Processo]([SeguradoId] ASC);


GO



GO
CREATE NONCLUSTERED INDEX [IX_CidadeId]
    ON [dbo].[Processo]([CidadeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AcionamentoId]
    ON [dbo].[Processo]([AcionamentoId] ASC);

