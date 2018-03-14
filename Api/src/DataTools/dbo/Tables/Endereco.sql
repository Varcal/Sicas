CREATE TABLE [dbo].[Endereco] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Logradouro]   VARCHAR (200) NOT NULL,
    [Cep]          CHAR (8)      NOT NULL,
    [Bairro]       VARCHAR (200) NULL,
    [CidadeId]     INT           NOT NULL,
    [DataCadastro] DATETIME2 (3) NOT NULL,
    [Ativo]        BIT           NOT NULL,
    CONSTRAINT [PK_dbo.Endereco] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Endereco_dbo.Cidade_CidadeId] FOREIGN KEY ([CidadeId]) REFERENCES [dbo].[Cidade] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Cep_Unique]
    ON [dbo].[Endereco]([Cep] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CidadeId]
    ON [dbo].[Endereco]([CidadeId] ASC);

