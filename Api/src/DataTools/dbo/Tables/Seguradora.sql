CREATE TABLE [dbo].[Seguradora] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [DadosPessoaJuridicaId] INT            NOT NULL,
    [Telefone]              CHAR (10)      NOT NULL,
    [Contato]               VARCHAR (150)  NOT NULL,
    [Email]                 VARCHAR (150)  NOT NULL,
    [ValorPorKm]            DECIMAL (9, 2) NOT NULL,
    [UrlAdministrativo]     VARCHAR (256)  NOT NULL,
    [UrlFinanceiro]         VARCHAR (256)  NOT NULL,
    [DataCadastro]          DATETIME2 (3)  NOT NULL,
    [Ativo]                 BIT            NOT NULL,
    CONSTRAINT [PK_dbo.Seguradora] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Seguradora_dbo.DadosPessoaJuridica_DadosPessoaJuridicaId] FOREIGN KEY ([DadosPessoaJuridicaId]) REFERENCES [dbo].[DadosPessoaJuridica] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_DadosPessoaJuridicaId]
    ON [dbo].[Seguradora]([DadosPessoaJuridicaId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Telefone_Unique]
    ON [dbo].[Seguradora]([Telefone] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Email_Unique]
    ON [dbo].[Seguradora]([Email] ASC);

