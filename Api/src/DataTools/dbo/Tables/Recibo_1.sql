CREATE TABLE [dbo].[Recibo] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [ProcessoId]   INT           NOT NULL,
    [SindicanteId] INT           NOT NULL,
    [Status]       INT           NOT NULL,
    [DataCadastro] DATETIME2 (3) NOT NULL,
    [Ativo]        BIT           NOT NULL,
    CONSTRAINT [PK_dbo.Recibo] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Recibo_dbo.Processo_ProcessoId] FOREIGN KEY ([ProcessoId]) REFERENCES [dbo].[Processo] ([Id]),
    CONSTRAINT [FK_dbo.Recibo_dbo.Sindicante_SindicanteId] FOREIGN KEY ([SindicanteId]) REFERENCES [dbo].[Sindicante] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_SindicanteId]
    ON [dbo].[Recibo]([SindicanteId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ProcessoId]
    ON [dbo].[Recibo]([ProcessoId] ASC);

