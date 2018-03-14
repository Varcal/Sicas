CREATE TABLE [dbo].[DadosBancarios] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [BancoId]      INT           NOT NULL,
    [ContaTipo]    INT           NOT NULL,
    [Agencia]      CHAR (4)      NOT NULL,
    [Conta]        VARCHAR (20)  NOT NULL,
    [DataCadastro] DATETIME2 (3) NOT NULL,
    [Ativo]        BIT           NOT NULL,
    CONSTRAINT [PK_dbo.DadosBancarios] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.DadosBancarios_dbo.Banco_BancoId] FOREIGN KEY ([BancoId]) REFERENCES [dbo].[Banco] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_BancoId]
    ON [dbo].[DadosBancarios]([BancoId] ASC);

