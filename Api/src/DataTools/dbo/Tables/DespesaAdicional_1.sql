CREATE TABLE [dbo].[DespesaAdicional] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [DespesaTipoId] INT            NOT NULL,
    [DespesaId]     INT            NOT NULL,
    [Valor]         DECIMAL (6, 2) NOT NULL,
    [DataCadastro]  DATETIME2 (3)  NOT NULL,
    [Ativo]         BIT            NOT NULL,
    CONSTRAINT [PK_dbo.DespesaAdicional] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.DespesaAdicional_dbo.Despesa_DespesaId] FOREIGN KEY ([DespesaId]) REFERENCES [dbo].[Despesa] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_DespesaId]
    ON [dbo].[DespesaAdicional]([DespesaId] ASC);

