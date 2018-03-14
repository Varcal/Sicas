CREATE TABLE [dbo].[Despesa] (
    [Id]                INT             IDENTITY (1, 1) NOT NULL,
    [ProcessoId]        INT             NOT NULL,
    [Descricao]         VARCHAR (50)    NOT NULL,
    [Data]              DATE            NOT NULL,
    [EnderecoOrigemId]  INT             NOT NULL,
    [EnderecoDestinoId] INT             NOT NULL,
    [Kms]               DECIMAL (5, 1)  NOT NULL,
    [ValorKm]           DECIMAL (11, 2) NOT NULL,
    [DataCadastro]      DATETIME2 (3)   NOT NULL,
    [Ativo]             BIT             NOT NULL,
    CONSTRAINT [PK_dbo.Despesa] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Despesa_dbo.EnderecoDespesa_EnderecoDestinoId] FOREIGN KEY ([EnderecoDestinoId]) REFERENCES [dbo].[EnderecoDespesa] ([Id]),
    CONSTRAINT [FK_dbo.Despesa_dbo.EnderecoDespesa_EnderecoOrigemId] FOREIGN KEY ([EnderecoOrigemId]) REFERENCES [dbo].[EnderecoDespesa] ([Id]),
    CONSTRAINT [FK_dbo.Despesa_dbo.Processo_ProcessoId] FOREIGN KEY ([ProcessoId]) REFERENCES [dbo].[Processo] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_ProcessoId]
    ON [dbo].[Despesa]([ProcessoId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_EnderecoDestinoId]
    ON [dbo].[Despesa]([EnderecoDestinoId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_EnderecoOrigemId]
    ON [dbo].[Despesa]([EnderecoOrigemId] ASC);

