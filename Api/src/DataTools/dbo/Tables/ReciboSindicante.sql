CREATE TABLE [dbo].[ReciboSindicante] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [SindicanteId]   INT            NOT NULL,
    [Sindicante]     NVARCHAR (MAX) NULL,
    [Cpf]            NVARCHAR (MAX) NULL,
    [DadosBancarios] NVARCHAR (MAX) NULL,
    [DataCadastro]   DATETIME       NOT NULL,
    [Ativo]          BIT            NOT NULL,
    [DataInicio]     DATETIME       DEFAULT ('1900-01-01T00:00:00.000') NOT NULL,
    [DataFim]        DATETIME       DEFAULT ('1900-01-01T00:00:00.000') NOT NULL,
    CONSTRAINT [PK_dbo.ReciboSindicante] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.ReciboSindicante_dbo.Sindicante_SindicanteId] FOREIGN KEY ([SindicanteId]) REFERENCES [dbo].[Sindicante] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_SindicanteId]
    ON [dbo].[ReciboSindicante]([SindicanteId] ASC);

