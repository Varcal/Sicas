CREATE TABLE [dbo].[EnderecoDespesa] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [EnderecoId]   INT           NOT NULL,
    [Numero]       CHAR (6)      NOT NULL,
    [DataCadastro] DATETIME2 (3) NOT NULL,
    [Ativo]        BIT           NOT NULL,
    CONSTRAINT [PK_dbo.EnderecoDespesa] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.EnderecoDespesa_dbo.Endereco_EnderecoId] FOREIGN KEY ([EnderecoId]) REFERENCES [dbo].[Endereco] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_EnderecoId]
    ON [dbo].[EnderecoDespesa]([EnderecoId] ASC);

