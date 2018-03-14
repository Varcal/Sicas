CREATE TABLE [dbo].[LancamentoDespesa] (
    [Id]        INT NOT NULL,
    [DespesaId] INT NOT NULL,
    CONSTRAINT [PK_dbo.LancamentoDespesa] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.LancamentoDespesa_dbo.Despesa_DespesaId] FOREIGN KEY ([DespesaId]) REFERENCES [dbo].[Despesa] ([Id]),
    CONSTRAINT [FK_dbo.LancamentoDespesa_dbo.Lancamento_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[Lancamento] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_DespesaId]
    ON [dbo].[LancamentoDespesa]([DespesaId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Id]
    ON [dbo].[LancamentoDespesa]([Id] ASC);

