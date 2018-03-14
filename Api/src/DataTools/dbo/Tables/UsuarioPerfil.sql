CREATE TABLE [dbo].[UsuarioPerfil] (
    [UsuarioId] INT NOT NULL,
    [PerfilId]  INT NOT NULL,
    CONSTRAINT [PK_dbo.UsuarioPerfil] PRIMARY KEY CLUSTERED ([UsuarioId] ASC, [PerfilId] ASC),
    CONSTRAINT [FK_dbo.UsuarioPerfil_dbo.Perfil_PerfilId] FOREIGN KEY ([PerfilId]) REFERENCES [dbo].[Perfil] ([Id]),
    CONSTRAINT [FK_dbo.UsuarioPerfil_dbo.Usuario_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Usuario] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_PerfilId]
    ON [dbo].[UsuarioPerfil]([PerfilId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UsuarioId]
    ON [dbo].[UsuarioPerfil]([UsuarioId] ASC);

