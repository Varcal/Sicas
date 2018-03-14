CREATE TABLE [dbo].[DespesaSeguradora] (
    [NumeroRecibo]  INT             IDENTITY (1, 1) NOT NULL,
    [ProcessoId]    INT             NOT NULL,
    [Cia]           NVARCHAR (200)  NOT NULL,
    [Sinistro]      NVARCHAR (50)   NOT NULL,
    [Segurado]      NVARCHAR (200)  NOT NULL,
    [Analista]      NVARCHAR (200)  NOT NULL,
    [DataConclusao] DATE            NOT NULL,
    [ValorKm]       DECIMAL (18, 2) NOT NULL,
    [DataCadastro]  DATETIME2 (3)   NOT NULL,
    [Ativo]         BIT             NOT NULL,
    CONSTRAINT [PK_dbo.DespesaSeguradora] PRIMARY KEY CLUSTERED ([NumeroRecibo] ASC),
    CONSTRAINT [FK_dbo.DespesaSeguradora_dbo.Processo_ProcessoId] FOREIGN KEY ([ProcessoId]) REFERENCES [dbo].[Processo] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ProcessoId]
    ON [dbo].[DespesaSeguradora]([ProcessoId] ASC);

