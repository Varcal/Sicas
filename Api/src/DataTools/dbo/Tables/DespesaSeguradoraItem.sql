CREATE TABLE [dbo].[DespesaSeguradoraItem] (
    [Id]                  INT             IDENTITY (1, 1) NOT NULL,
    [DespesaSeguradoraId] INT             NOT NULL,
    [Data]                DATE            NOT NULL,
    [Descricao]           NVARCHAR (MAX)  NULL,
    [Endereco]            NVARCHAR (200)  NOT NULL,
    [KmPercorrido]        DECIMAL (18, 2) NOT NULL,
    [ValorKmPercorrido]   DECIMAL (18, 2) NOT NULL,
    [TransportePedagio]   DECIMAL (18, 2) NOT NULL,
    [HospedagemRefeicao]  DECIMAL (18, 2) NOT NULL,
    [Total]               DECIMAL (18, 2) NOT NULL,
    [DataCadastro]        DATETIME2 (3)   NOT NULL,
    [Ativo]               BIT             NOT NULL,
    CONSTRAINT [PK_dbo.DespesaSeguradoraItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.DespesaSeguradoraItem_dbo.DespesaSeguradora_DespesaSeguradoraId] FOREIGN KEY ([DespesaSeguradoraId]) REFERENCES [dbo].[DespesaSeguradora] ([NumeroRecibo]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_DespesaSeguradoraId]
    ON [dbo].[DespesaSeguradoraItem]([DespesaSeguradoraId] ASC);

