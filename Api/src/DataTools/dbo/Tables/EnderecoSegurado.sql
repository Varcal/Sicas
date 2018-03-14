CREATE TABLE [dbo].[EnderecoSegurado] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [EnderecoTipoId] INT           NOT NULL,
    [EnderecoId]     INT           NOT NULL,
    [Numero]         CHAR (6)      NOT NULL,
    [Complemento]    VARCHAR (200) NULL,
    [DataCadastro]   DATETIME2 (3) NOT NULL,
    [Ativo]          BIT           NOT NULL,
    CONSTRAINT [PK_dbo.EnderecoSegurado] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.EnderecoSegurado_dbo.Endereco_EnderecoId] FOREIGN KEY ([EnderecoId]) REFERENCES [dbo].[Endereco] ([Id]),
    CONSTRAINT [FK_dbo.EnderecoSegurado_dbo.EnderecoTipo_EnderecoTipoId] FOREIGN KEY ([EnderecoTipoId]) REFERENCES [dbo].[EnderecoTipo] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_EnderecoId]
    ON [dbo].[EnderecoSegurado]([EnderecoId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_EnderecoTipoId]
    ON [dbo].[EnderecoSegurado]([EnderecoTipoId] ASC);

