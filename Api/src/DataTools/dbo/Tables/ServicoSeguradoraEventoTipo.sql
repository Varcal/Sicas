CREATE TABLE [dbo].[ServicoSeguradoraEventoTipo] (
    [ServicoSeguradoraId] INT NOT NULL,
    [EventoTipoId]        INT NOT NULL,
    CONSTRAINT [PK_dbo.ServicoSeguradoraEventoTipo] PRIMARY KEY CLUSTERED ([ServicoSeguradoraId] ASC, [EventoTipoId] ASC),
    CONSTRAINT [FK_dbo.ServicoSeguradoraEventoTipo_dbo.EventoTipo_EventoTipoId] FOREIGN KEY ([EventoTipoId]) REFERENCES [dbo].[EventoTipo] ([Id]),
    CONSTRAINT [FK_dbo.ServicoSeguradoraEventoTipo_dbo.ServicoSeguradora_ServicoSeguradoraId] FOREIGN KEY ([ServicoSeguradoraId]) REFERENCES [dbo].[ServicoSeguradora] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_EventoTipoId]
    ON [dbo].[ServicoSeguradoraEventoTipo]([EventoTipoId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ServicoSeguradoraId]
    ON [dbo].[ServicoSeguradoraEventoTipo]([ServicoSeguradoraId] ASC);

