CREATE TABLE [dbo].[ReciboSindicanteItem] (
    [Id]                  INT             IDENTITY (1, 1) NOT NULL,
    [ReciboSindicanteId]  INT             NOT NULL,
    [Segurado]            VARCHAR (50)    NULL,
    [Sinistro]            VARCHAR (50)    NULL,
    [Seguradora]          VARCHAR (50)    NULL,
    [Despesa]             DECIMAL (18, 2) NOT NULL,
    [Honorario]           DECIMAL (18, 2) NOT NULL,
    [Desconto]            DECIMAL (18, 2) NOT NULL,
    [OutrosCreditos]      DECIMAL (18, 2) NOT NULL,
    [DataTerminoTrabalho] DATETIME        NOT NULL,
    [DataCadastro]        DATETIME2 (3)   NOT NULL,
    [Ativo]               BIT             NOT NULL,
    CONSTRAINT [PK_dbo.ReciboSindicanteItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.ReciboSindicanteItem_dbo.ReciboSindicante_ReciboSindicanteId] FOREIGN KEY ([ReciboSindicanteId]) REFERENCES [dbo].[ReciboSindicante] ([NumeroRecibo])
);








GO
CREATE NONCLUSTERED INDEX [IX_ReciboSindicanteId]
    ON [dbo].[ReciboSindicanteItem]([ReciboSindicanteId] ASC);

