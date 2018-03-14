CREATE TABLE [dbo].[Evento] (
    [SeguradoraId]        INT             NOT NULL,
    [ServicoSeguradoraId] INT             NOT NULL,
    [EventoTipoId]        INT             NOT NULL,
    [FranquiaKm]          INT             NOT NULL,
    [Honorario]           DECIMAL (18, 2) NOT NULL,
    [DataCadastro]        DATETIME2 (3)   NOT NULL,
    [Ativo]               BIT             NOT NULL,
    CONSTRAINT [PK_dbo.Evento] PRIMARY KEY CLUSTERED ([SeguradoraId] ASC, [ServicoSeguradoraId] ASC, [EventoTipoId] ASC),
    CONSTRAINT [FK_dbo.Evento_dbo.EventoTipo_EventoTipoId] FOREIGN KEY ([EventoTipoId]) REFERENCES [dbo].[EventoTipo] ([Id]),
    CONSTRAINT [FK_dbo.Evento_dbo.Seguradora_SeguradoraId] FOREIGN KEY ([SeguradoraId]) REFERENCES [dbo].[Seguradora] ([Id]),
    CONSTRAINT [FK_dbo.Evento_dbo.ServicoSeguradora_ServicoSeguradoraId] FOREIGN KEY ([ServicoSeguradoraId]) REFERENCES [dbo].[ServicoSeguradora] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_EventoTipoId]
    ON [dbo].[Evento]([EventoTipoId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SeguradoraId]
    ON [dbo].[Evento]([SeguradoraId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ServicoSeguradoraId]
    ON [dbo].[Evento]([ServicoSeguradoraId] ASC);

