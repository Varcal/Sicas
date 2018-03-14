CREATE TABLE [dbo].[Status] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Nome]         NVARCHAR (MAX) NULL,
    [DataCadastro] DATETIME2 (3)  NOT NULL,
    [Ativo]        BIT            NOT NULL,
    CONSTRAINT [PK_dbo.Status] PRIMARY KEY CLUSTERED ([Id] ASC)
);

