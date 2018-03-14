using System;
using Domain.Administrativo.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Tests.Entities
{
    [TestClass]
    public class EventoTests
    {
        [TestCategory("Evento - Registrar")]
        [TestMethod]
        public void Criar_Evento_Se_Valido()
        {
            const int tipoSevicoId = 1;
            const int eventoTipoId = 1;
            const int kmFranquia = 100;
            const decimal valor = 400.99M;

            var evento = new Evento(tipoSevicoId, eventoTipoId, kmFranquia, valor);

            Assert.IsNotNull(evento);
            Assert.AreEqual(tipoSevicoId, evento.ServicoSeguradoraId);
            Assert.AreEqual(eventoTipoId, evento.EventoTipoId);
            Assert.AreEqual(kmFranquia, evento.FranquiaKm);
            Assert.AreEqual(valor, evento.Honorario);
            Assert.AreNotEqual(DateTime.MinValue, evento.DataCadastro);
            Assert.IsTrue(evento.Ativo);
            Assert.IsTrue(evento.IsValid());
        }
    }
}
