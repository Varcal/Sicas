CREATE TABLE [dbo].[ProcessoHistorico] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [SeguradoraId] INT           NOT NULL,
    [ProcessoId]   INT           NOT NULL,
    [SindicanteId] INT           NOT NULL,
    [StatusId]     INT           NOT NULL,
    [IniciadoEm]   DATETIME2 (3) NOT NULL,
    [FinalizadoEm] DATETIME2 (3) NULL,
    [DataCadastro] DATETIME2 (3) NOT NULL,
    [Ativo]        BIT           NOT NULL,
    CONSTRAINT [PK_dbo.ProcessoHistorico] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.ProcessoHistorico_dbo.Processo_ProcessoId] FOREIGN KEY ([ProcessoId]) REFERENCES [dbo].[Processo] ([Id]),
    CONSTRAINT [FK_dbo.ProcessoHistorico_dbo.Seguradora_SeguradoraId] FOREIGN KEY ([SeguradoraId]) REFERENCES [dbo].[Seguradora] ([Id]),
    CONSTRAINT [FK_dbo.ProcessoHistorico_dbo.Sindicante_SindicanteId] FOREIGN KEY ([SindicanteId]) REFERENCES [dbo].[Sindicante] ([Id]),
    CONSTRAINT [FK_dbo.ProcessoHistorico_dbo.Status_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_SeguradoraId]
    ON [dbo].[ProcessoHistorico]([SeguradoraId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ProcessoId]
    ON [dbo].[ProcessoHistorico]([ProcessoId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SindicanteId]
    ON [dbo].[ProcessoHistorico]([SindicanteId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StatusId]
    ON [dbo].[ProcessoHistorico]([StatusId] ASC);

