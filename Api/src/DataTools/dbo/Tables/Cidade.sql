CREATE TABLE [dbo].[Cidade] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Nome]         VARCHAR (100) NOT NULL,
    [EstadoId]     INT           NOT NULL,
    [DataCadastro] DATETIME2 (3) NOT NULL,
    [Ativo]        BIT           NOT NULL,
    CONSTRAINT [PK_dbo.Cidade] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Cidade_dbo.Estado_EstadoId] FOREIGN KEY ([EstadoId]) REFERENCES [dbo].[Estado] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_EstadoId]
    ON [dbo].[Cidade]([EstadoId] ASC);

