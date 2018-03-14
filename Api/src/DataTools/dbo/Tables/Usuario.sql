CREATE TABLE [dbo].[Usuario] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Nome]         VARCHAR (150) NOT NULL,
    [Login]        VARCHAR (50)  NOT NULL,
    [Senha]        VARCHAR (32)  NOT NULL,
    [DataCadastro] DATETIME2 (3) NOT NULL,
    [Ativo]        BIT           NOT NULL,
    CONSTRAINT [PK_dbo.Usuario] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Login_Unique]
    ON [dbo].[Usuario]([Login] ASC);

