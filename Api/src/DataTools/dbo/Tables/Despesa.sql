CREATE TABLE [dbo].[Despesa] (
    [Id]                INT             IDENTITY (1, 1) NOT NULL,
    [ProcessoId]        INT             NOT NULL,
    [Descricao]         VARCHAR (50)    NOT NULL,
    [Data]              DATE            NOT NULL,
    [Kms]               DECIMAL (5, 1)  NOT NULL,
    [ValorKm]           DECIMAL (11, 2) NOT NULL,
    [DataCadastro]      DATETIME2 (3)   NOT NULL,
    [Ativo]             BIT             NOT NULL,
    [EnderecoChegadaId] INT             NOT NULL,
    [EnderecoPartidaId] INT             NOT NULL,
    CONSTRAINT [PK_dbo.Despesa] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Despesa_dbo.EnderecoDespesa_EnderecoChegadaId] FOREIGN KEY ([EnderecoChegadaId]) REFERENCES [dbo].[EnderecoDespesa] ([Id]),
    CONSTRAINT [FK_dbo.Despesa_dbo.EnderecoDespesa_EnderecoPartidaId] FOREIGN KEY ([EnderecoPartidaId]) REFERENCES [dbo].[EnderecoDespesa] ([Id]),
    CONSTRAINT [FK_dbo.Despesa_dbo.Processo_ProcessoId] FOREIGN KEY ([ProcessoId]) REFERENCES [dbo].[Processo] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ProcessoId]
    ON [dbo].[Despesa]([ProcessoId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_EnderecoChegadaId]
    ON [dbo].[Despesa]([EnderecoChegadaId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_EnderecoPartidaId]
    ON [dbo].[Despesa]([EnderecoPartidaId] ASC);

