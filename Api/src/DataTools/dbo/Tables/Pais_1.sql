CREATE TABLE [dbo].[Pais] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Nome]         VARCHAR (50)  NOT NULL,
    [Sigla]        CHAR (2)      NOT NULL,
    [DataCadastro] DATETIME2 (3) NOT NULL,
    [Ativo]        BIT           NOT NULL,
    CONSTRAINT [PK_dbo.Pais] PRIMARY KEY CLUSTERED ([Id] ASC)
);

