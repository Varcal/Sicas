CREATE TABLE [dbo].[Sindicante] (
    [Id]                  INT             IDENTITY (1, 1) NOT NULL,
    [SindicanteTipoId]    INT             NOT NULL,
    [DadosBancariosId]    INT             NOT NULL,
    [DadosPessoaFisicaId] INT             NOT NULL,
    [Telefone]            CHAR (10)       NOT NULL,
    [Celular]             CHAR (11)       NOT NULL,
    [Email]               VARCHAR (150)   NOT NULL,
    [ValorPorKm]          DECIMAL (18, 2) NOT NULL,
    [DataCadastro]        DATETIME2 (3)   NOT NULL,
    [Ativo]               BIT             NOT NULL,
    CONSTRAINT [PK_dbo.Sindicante] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Sindicante_dbo.DadosBancarios_DadosBancariosId] FOREIGN KEY ([DadosBancariosId]) REFERENCES [dbo].[DadosBancarios] ([Id]),
    CONSTRAINT [FK_dbo.Sindicante_dbo.DadosPessoaFisica_DadosPessoaFisicaId] FOREIGN KEY ([DadosPessoaFisicaId]) REFERENCES [dbo].[DadosPessoaFisica] ([Id]),
    CONSTRAINT [FK_dbo.Sindicante_dbo.SindicanteTipo_SindicanteTipoId] FOREIGN KEY ([SindicanteTipoId]) REFERENCES [dbo].[SindicanteTipo] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_SindicanteTipoId]
    ON [dbo].[Sindicante]([SindicanteTipoId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_DadosBancariosId]
    ON [dbo].[Sindicante]([DadosBancariosId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_DadosPessoaFisicaId]
    ON [dbo].[Sindicante]([DadosPessoaFisicaId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Telefone_Unique]
    ON [dbo].[Sindicante]([Telefone] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Celular_Unique]
    ON [dbo].[Sindicante]([Celular] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Email_Unique]
    ON [dbo].[Sindicante]([Email] ASC);

