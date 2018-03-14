CREATE TABLE [dbo].[Processo] (
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [SeguradoraId]         INT            NOT NULL,
    [SeguradoraServicoId]  INT            NOT NULL,
    [EventoTipoId]         INT            NOT NULL,
    [UsuarioResponsavelId] INT            NULL,
    [SindicanteServicoId]  INT            NOT NULL,
    [NumeroSinistro]       CHAR (12)      NOT NULL,
    [NumeroReferencia]     CHAR (12)      NULL,
    [Analista]             VARCHAR (100)  NOT NULL,
    [Segurado]             VARCHAR (100)  NOT NULL,
    [Situacao]             INT            NOT NULL,
    [Status]               INT            NOT NULL,
    [EmitidoParecerEm]     DATETIME       NULL,
    [FinalizadoEm]         DATETIME2 (3)  NULL,
    [Comentario]           NVARCHAR (MAX) NULL,
    [DataCadastro]         DATETIME2 (3)  NOT NULL,
    [Ativo]                BIT            NOT NULL,
    CONSTRAINT [PK_dbo.Processo] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Processo_dbo.EventoTipo_EventoTipoId] FOREIGN KEY ([EventoTipoId]) REFERENCES [dbo].[EventoTipo] ([Id]),
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

