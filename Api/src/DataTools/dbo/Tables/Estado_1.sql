CREATE TABLE [dbo].[Estado] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Nome]         VARCHAR (50)  NOT NULL,
    [Uf]           CHAR (2)      NOT NULL,
    [PaisId]       INT           NOT NULL,
    [DataCadastro] DATETIME2 (3) NOT NULL,
    [Ativo]        BIT           NOT NULL,
    CONSTRAINT [PK_dbo.Estado] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Estado_dbo.Pais_PaisId] FOREIGN KEY ([PaisId]) REFERENCES [dbo].[Pais] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_PaisId]
    ON [dbo].[Estado]([PaisId] ASC);

