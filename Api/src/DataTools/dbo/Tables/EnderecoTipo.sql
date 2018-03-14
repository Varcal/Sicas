CREATE TABLE [dbo].[EnderecoTipo] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Nome]         VARCHAR (50)  NOT NULL,
    [DataCadastro] DATETIME2 (3) NOT NULL,
    [Ativo]        BIT           NOT NULL,
    CONSTRAINT [PK_dbo.EnderecoTipo] PRIMARY KEY CLUSTERED ([Id] ASC)
);

