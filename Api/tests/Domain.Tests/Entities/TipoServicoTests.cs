using System;
using System.Collections.Generic;
using Domain.Administrativo.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Tests.Entities
{
    [TestClass]
    public class TipoServicoTests
    {

        IList<EventoTipo> eventoTipoList = new List<EventoTipo>() {new EventoTipo("Teste")};

        [TestCategory("TipoServico - Registrar")]
        [TestMethod]
        public void Criar_TipoServico_Se_Valido()
        {
            const string nome = "Nome do Serviço";
            

            var tipoServico = new ServicoSeguradora(nome, eventoTipoList);

            Assert.IsNotNull(tipoServico);
            Assert.AreEqual(nome, tipoServico.Nome);
            Assert.AreNotEqual(DateTime.MinValue, tipoServico.DataCadastro);
            Assert.IsTrue(tipoServico.Ativo);
            Assert.IsTrue(tipoServico.IsValid());
        }

        [TestCategory("TipoServico - Registrar")]
        [TestMethod]
        public void Nao_Criar_TipoServico_Se_Nome_Nao_Informado()
        {
            const string nome = "";

            var tipoServico = new ServicoSeguradora(nome, eventoTipoList);
         
            Assert.IsFalse(tipoServico.IsValid());
        }

        [TestCategory("TipoServico - Registrar")]
        [TestMethod]
        public void Nao_Criar_TipoServico_Se_Nome_Invalido()
        {
            const string nome = "Nome Serviço 2";

            var tipoServico = new ServicoSeguradora(nome, eventoTipoList);

            Assert.IsFalse(tipoServico.IsValid());
        }
    }

}
