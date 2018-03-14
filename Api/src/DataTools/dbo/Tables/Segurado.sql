CREATE TABLE [dbo].[Segurado] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [Nome]               VARCHAR (150) NOT NULL,
    [EnderecoSeguradoId] INT           NULL,
    [DataCadastro]       DATETIME2 (3) NOT NULL,
    [Ativo]              BIT           NOT NULL,
    CONSTRAINT [PK_dbo.Segurado] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Segurado_dbo.EnderecoSegurado_EnderecoSeguradoId] FOREIGN KEY ([EnderecoSeguradoId]) REFERENCES [dbo].[EnderecoSegurado] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_EnderecoSeguradoId]
    ON [dbo].[Segurado]([EnderecoSeguradoId] ASC);

