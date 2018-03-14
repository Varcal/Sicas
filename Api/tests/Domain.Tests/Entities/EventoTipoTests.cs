using System;
using Domain.Administrativo.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Tests.Entities
{
    [TestClass]
    public class EventoTipoTests
    {
        [TestCategory("EventoTipo - Registrar")]
        [TestMethod]
        public void Criar_EventoTipo_Se_Valido()
        {
            const string nome = "Nome do Serviço";

            var eventoTipo = new EventoTipo(nome);

            Assert.IsNotNull(eventoTipo);
            Assert.AreEqual(nome, eventoTipo.Nome);
            Assert.AreNotEqual(DateTime.MinValue, eventoTipo.DataCadastro);
            Assert.IsTrue(eventoTipo.Ativo);
            Assert.IsTrue(eventoTipo.IsValid());
        }

        [TestCategory("EventoTipo - Registrar")]
        [TestMethod]
        public void Nao_Criar_EventoTipo_Se_Nome_Nao_Informado()
        {
            const string nome = "";

            var eventoTipo = new EventoTipo(nome);

            Assert.IsFalse(eventoTipo.IsValid());
        }

        [TestCategory("EventoTipo - Registrar")]
        [TestMethod]
        public void Nao_Criar_EventoTipo_Se_Nome_Invalido()
        {
            const string nome = "Nome do Evento 2";

            var eventoTipo = new EventoTipo(nome);

            Assert.IsFalse(eventoTipo.IsValid());
        }
    }
}
