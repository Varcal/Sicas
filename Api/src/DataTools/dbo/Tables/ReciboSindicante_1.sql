CREATE TABLE [dbo].[ReciboSindicante] (
    [NumeroRecibo]   INT           IDENTITY (1, 1) NOT NULL,
    [SindicanteId]   INT           NOT NULL,
    [Sindicante]     VARCHAR (200) NOT NULL,
    [Cpf]            VARCHAR (11)  NOT NULL,
    [DadosBancarios] VARCHAR (100) NOT NULL,
    [DataInicio]     DATE          NOT NULL,
    [DataFim]        DATE          NOT NULL,
    [DataCadastro]   DATETIME2 (3) NOT NULL,
    [Ativo]          BIT           NOT NULL,
    [Emitido]        BIT           DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.ReciboSindicante] PRIMARY KEY CLUSTERED ([NumeroRecibo] ASC),
    CONSTRAINT [FK_dbo.ReciboSindicante_dbo.Sindicante_SindicanteId] FOREIGN KEY ([SindicanteId]) REFERENCES [dbo].[Sindicante] ([Id])
);










GO
CREATE NONCLUSTERED INDEX [IX_SindicanteId]
    ON [dbo].[ReciboSindicante]([SindicanteId] ASC);

