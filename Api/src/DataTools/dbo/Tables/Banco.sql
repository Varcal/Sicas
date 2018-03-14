CREATE TABLE [dbo].[Banco] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Numero]       CHAR (3)      NOT NULL,
    [Nome]         VARCHAR (200) NOT NULL,
    [DataCadastro] DATETIME2 (3) NOT NULL,
    [Ativo]        BIT           NOT NULL,
    CONSTRAINT [PK_dbo.Banco] PRIMARY KEY CLUSTERED ([Id] ASC)
);

