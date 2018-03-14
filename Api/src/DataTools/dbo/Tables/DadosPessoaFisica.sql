CREATE TABLE [dbo].[DadosPessoaFisica] (
    [Id]   INT           NOT NULL,
    [Nome] VARCHAR (150) NOT NULL,
    [Cpf]  CHAR (11)     NOT NULL,
    [Rg]   CHAR (20)     NULL,
    CONSTRAINT [PK_dbo.DadosPessoaFisica] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.DadosPessoaFisica_dbo.DadosCadastrais_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[DadosCadastrais] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Cpf_Unique]
    ON [dbo].[DadosPessoaFisica]([Cpf] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Id]
    ON [dbo].[DadosPessoaFisica]([Id] ASC);

