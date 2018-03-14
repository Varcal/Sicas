CREATE TABLE [dbo].[SindicanteInterno] (
    [Id]        INT NOT NULL,
    [UsuarioId] INT NOT NULL,
    CONSTRAINT [PK_dbo.SindicanteInterno] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.SindicanteInterno_dbo.Sindicante_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[Sindicante] ([Id]),
    CONSTRAINT [FK_dbo.SindicanteInterno_dbo.Usuario_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Usuario] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Id]
    ON [dbo].[SindicanteInterno]([Id] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UsuarioId]
    ON [dbo].[SindicanteInterno]([UsuarioId] ASC);

