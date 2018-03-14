CREATE TABLE [dbo].[Lancamento] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL,
    [ReciboId]       INT             NOT NULL,
    [LancamentoTipo] INT             NOT NULL,
    [Descricao]      VARCHAR (100)   NOT NULL,
    [Valor]          DECIMAL (11, 2) NOT NULL,
    [Observacao]     VARCHAR (200)   NULL,
    [TipoFinanceiro] INT             NOT NULL,
    [DataCadastro]   DATETIME2 (3)   NOT NULL,
    [Ativo]          BIT             NOT NULL,
    CONSTRAINT [PK_dbo.Lancamento] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Lancamento_dbo.Recibo_ReciboId] FOREIGN KEY ([ReciboId]) REFERENCES [dbo].[Recibo] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ReciboId]
    ON [dbo].[Lancamento]([ReciboId] ASC);

