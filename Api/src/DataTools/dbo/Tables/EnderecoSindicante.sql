CREATE TABLE [dbo].[EnderecoSindicante] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [EnderecoTipoId] INT           NOT NULL,
    [EnderecoId]     INT           NOT NULL,
    [Numero]         CHAR (6)      NOT NULL,
    [SindicanteId]   INT           NOT NULL,
    [Complemento]    VARCHAR (200) NULL,
    [DataCadastro]   DATETIME2 (3) NOT NULL,
    [Ativo]          BIT           NOT NULL,
    CONSTRAINT [PK_dbo.EnderecoSindicante] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.EnderecoSindicante_dbo.Endereco_EnderecoId] FOREIGN KEY ([EnderecoId]) REFERENCES [dbo].[Endereco] ([Id]),
    CONSTRAINT [FK_dbo.EnderecoSindicante_dbo.EnderecoTipo_EnderecoTipoId] FOREIGN KEY ([EnderecoTipoId]) REFERENCES [dbo].[EnderecoTipo] ([Id]),
    CONSTRAINT [FK_dbo.EnderecoSindicante_dbo.Sindicante_SindicanteId] FOREIGN KEY ([SindicanteId]) REFERENCES [dbo].[Sindicante] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_EnderecoTipoId]
    ON [dbo].[EnderecoSindicante]([EnderecoTipoId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_EnderecoId]
    ON [dbo].[EnderecoSindicante]([EnderecoId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SindicanteId]
    ON [dbo].[EnderecoSindicante]([SindicanteId] ASC);

