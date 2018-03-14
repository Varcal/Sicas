CREATE TABLE [dbo].[ProcessoSindicante] (
    [ProcessoId]   INT NOT NULL,
    [SindicanteId] INT NOT NULL,
    CONSTRAINT [PK_dbo.ProcessoSindicante] PRIMARY KEY CLUSTERED ([ProcessoId] ASC, [SindicanteId] ASC),
    CONSTRAINT [FK_dbo.ProcessoSindicante_dbo.Processo_ProcessoId] FOREIGN KEY ([ProcessoId]) REFERENCES [dbo].[Processo] ([Id]),
    CONSTRAINT [FK_dbo.ProcessoSindicante_dbo.Sindicante_SindicanteId] FOREIGN KEY ([SindicanteId]) REFERENCES [dbo].[Sindicante] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ProcessoId]
    ON [dbo].[ProcessoSindicante]([ProcessoId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SindicanteId]
    ON [dbo].[ProcessoSindicante]([SindicanteId] ASC);

