CREATE TABLE [dbo].[Honorario] (
    [SindicanteId]        INT            NOT NULL,
    [ServicoSeguradoraId] INT            NOT NULL,
    [EventoTipoId]        INT            NOT NULL,
    [ServicoSindicanteId] INT            NOT NULL,
    [Valor]               DECIMAL (5, 2) NOT NULL,
    [DataCadastro]        DATETIME2 (3)  NOT NULL,
    [Ativo]               BIT            NOT NULL,
    CONSTRAINT [PK_dbo.Honorario] PRIMARY KEY CLUSTERED ([SindicanteId] ASC, [ServicoSeguradoraId] ASC, [EventoTipoId] ASC, [ServicoSindicanteId] ASC),
    CONSTRAINT [FK_dbo.Honorario_dbo.EventoTipo_EventoTipoId] FOREIGN KEY ([EventoTipoId]) REFERENCES [dbo].[EventoTipo] ([Id]),
    CONSTRAINT [FK_dbo.Honorario_dbo.ServicoSeguradora_ServicoSeguradoraId] FOREIGN KEY ([ServicoSeguradoraId]) REFERENCES [dbo].[ServicoSeguradora] ([Id]),
    CONSTRAINT [FK_dbo.Honorario_dbo.ServicoSindicante_ServicoSindicanteId] FOREIGN KEY ([ServicoSindicanteId]) REFERENCES [dbo].[ServicoSindicante] ([Id]),
    CONSTRAINT [FK_dbo.Honorario_dbo.Sindicante_SindicanteId] FOREIGN KEY ([SindicanteId]) REFERENCES [dbo].[Sindicante] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_EventoTipoId]
    ON [dbo].[Honorario]([EventoTipoId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ServicoSeguradoraId]
    ON [dbo].[Honorario]([ServicoSeguradoraId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ServicoSindicanteId]
    ON [dbo].[Honorario]([ServicoSindicanteId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SindicanteId]
    ON [dbo].[Honorario]([SindicanteId] ASC);

