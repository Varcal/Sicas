CREATE TABLE [dbo].[SindicanteTipo] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [SindicanteTipo] VARCHAR (20)  NOT NULL,
    [DataCadastro]   DATETIME2 (3) NOT NULL,
    [Ativo]          BIT           NOT NULL,
    CONSTRAINT [PK_dbo.SindicanteTipo] PRIMARY KEY CLUSTERED ([Id] ASC)
);

