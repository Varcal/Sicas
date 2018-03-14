CREATE TABLE [dbo].[DadosPessoaJuridica] (
    [Id]           INT           NOT NULL,
    [RazaoSocial]  VARCHAR (200) NOT NULL,
    [NomeFantasia] VARCHAR (200) NULL,
    [Cnpj]         CHAR (14)     NOT NULL,
    [InscEst]      CHAR (20)     NULL,
    CONSTRAINT [PK_dbo.DadosPessoaJuridica] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.DadosPessoaJuridica_dbo.DadosCadastrais_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[DadosCadastrais] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Cnpj_Unique]
    ON [dbo].[DadosPessoaJuridica]([Cnpj] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Id]
    ON [dbo].[DadosPessoaJuridica]([Id] ASC);

