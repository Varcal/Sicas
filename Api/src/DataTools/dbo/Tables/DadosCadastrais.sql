CREATE TABLE [dbo].[DadosCadastrais] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [DataCadastro] DATETIME2 (3) NOT NULL,
    [Ativo]        BIT           NOT NULL,
    CONSTRAINT [PK_dbo.DadosCadastrais] PRIMARY KEY CLUSTERED ([Id] ASC)
);

