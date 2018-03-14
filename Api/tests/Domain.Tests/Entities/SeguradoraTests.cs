using System;
using System.Collections.Generic;
using Domain.Administrativo.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Tests.Entities
{
    [TestClass]
    public class SeguradoraTests
    {
        IList<Evento> eventos = new List<Evento>();

        [TestCategory("Seguradora - Registrar")]
        [TestMethod]
        public void Criar_Seguradora_Se_Valida()
        {
            const string nome = "Nome da Seguradora";
            const string cnpj = "11222333000199";
            const string telefone = "1123456789";
            const string email = "teste@teste.com.br";
            const string contato = "Nome do contato";
            const decimal valorPorKm = 0.72M;
            var evento = new Evento(1,1,50,150);
            eventos.Add(evento);

            var seguradora = new Seguradora(nome, cnpj, telefone, contato, email, valorPorKm,"","","", eventos);

            Assert.IsNotNull(seguradora);
            Assert.AreEqual(nome, seguradora.DadosPessoaJuridica.RazaoSocial);
            Assert.AreEqual(cnpj, seguradora.DadosPessoaJuridica.Cnpj);
            Assert.AreEqual(telefone, seguradora.Telefone);
            Assert.AreEqual(contato, seguradora.Contato);
            Assert.AreEqual(email, seguradora.Email);
            Assert.AreEqual(valorPorKm, seguradora.ValorPorKm);
            Assert.AreNotEqual(DateTime.MinValue, seguradora.DataCadastro);
            Assert.IsTrue(seguradora.Ativo);
            Assert.IsTrue(seguradora.IsValid());
        }

        [TestCategory("Seguradora - Registrar")]
        [TestMethod]
        public void Nao_Criar_Seguradora_Se_Nome_Nao_Informado()
        {
            const string nome = "";
            const string cnpj = "11222333000199";
            const string telefone = "1123456789";
            const string email = "teste@teste.com.br";
            const string contato = "Nome do contato";
            const decimal valorPorKm = 0.72M;

            var seguradora = new Seguradora(nome, cnpj, telefone, contato, email, valorPorKm, "", "", "", eventos);


            Assert.IsFalse(seguradora.IsValid());
        }

        [TestCategory("Seguradora - Registrar")]
        [TestMethod]
        public void Nao_Criar_Seguradora_Se_CNPJ_Nao_Informado()
        {
            const string nome = "Nome";
            const string cnpj = "";
            const string telefone = "1123456789";
            const string email = "teste@teste.com.br";
            const string contato = "Nome do contato";
            const decimal valorPorKm = 0.72M;

            var seguradora = new Seguradora(nome, cnpj, telefone, contato, email, valorPorKm, "", "", "", eventos);


            Assert.IsFalse(seguradora.IsValid());
        }

        [TestCategory("Seguradora - Registrar")]
        [TestMethod]
        public void Nao_Criar_Seguradora_Se_Telefone_Nao_Informado()
        {
            const string nome = "Nome da Seguradora";
            const string cnpj = "11222333000199";
            const string telefone = "";
            const string email = "teste@teste.com.br";
            const string contato = "Nome do contato";
            const decimal valorPorKm = 0.72M;

            var seguradora = new Seguradora(nome, cnpj, telefone, contato, email, valorPorKm, "", "", "", eventos);


            Assert.IsFalse(seguradora.IsValid());
        }

        [TestCategory("Seguradora - Registrar")]
        [TestMethod]
        public void Nao_Criar_Seguradora_Se_Telefone_Invalido_Mais_10_Caracteres()
        {
            const string nome = "Nome da Seguradora";
            const string cnpj = "11222333000199";
            const string telefone = "11234567890";
            const string email = "teste@teste.com.br";
            const string contato = "Nome do contato";
            const decimal valorPorKm = 0.72M;

            var seguradora = new Seguradora(nome, cnpj, telefone, contato, email, valorPorKm, "", "", "", eventos);


            Assert.IsFalse(seguradora.IsValid());
        }

        [TestCategory("Seguradora - Registrar")]
        [TestMethod]
        public void Nao_Criar_Seguradora_Se_Telefone_Invalido_Menos_10_Caracteres()
        {
            const string nome = "Nome da Seguradora";
            const string cnpj = "11222333000199";
            const string telefone = "112345678";
            const string email = "teste@teste.com.br";
            const string contato = "Nome do contato";
            const decimal valorPorKm = 0.72M;

            var seguradora = new Seguradora(nome, cnpj, telefone, contato, email, valorPorKm, "", "", "", eventos);


            Assert.IsFalse(seguradora.IsValid());
        }

        [TestCategory("Seguradora - Registrar")]
        [TestMethod]
        public void Nao_Criar_Seguradora_Se_Email_Nao_Informado()
        {
            const string nome = "Nome da Seguradora";
            const string cnpj = "11222333000199";
            const string telefone = "1123456789";
            const string email = "";
            const string contato = "Nome do contato";
            const decimal valorPorKm = 0.72M;

            var seguradora = new Seguradora(nome, cnpj, telefone, contato, email, valorPorKm, "", "", "", eventos);


            Assert.IsFalse(seguradora.IsValid());
        }

        [TestCategory("Seguradora - Registrar")]
        [TestMethod]
        public void Nao_Criar_Seguradora_Se_Email_Invalido()
        {
            const string nome = "Nome da Seguradora";
            const string cnpj = "11222333000199";
            const string telefone = "1123456789";
            const string email = "teste@teste.";
            const string contato = "Nome do contato";
            const decimal valorPorKm = 0.72M;

            var seguradora = new Seguradora(nome, cnpj, telefone, contato, email, valorPorKm, "", "", "", eventos);


            Assert.IsFalse(seguradora.IsValid());
        }


        [TestCategory("Seguradora - Registrar")]
        [TestMethod]
        public void Nao_Criar_Seguradora_Se_Contato_Nao_Informado()
        {
            const string nome = "Nome da Seguradora";
            const string cnpj = "11222333000199";
            const string telefone = "1123456789";
            const string email = "teste@teste.com.br";
            const string contato = "";
            const decimal valorPorKm = 0.72M;


            var seguradora = new Seguradora(nome, cnpj, telefone, contato, email, valorPorKm, "", "", "", eventos);


            Assert.IsFalse(seguradora.IsValid());
        }

        [TestCategory("Seguradora - Registrar")]
        [TestMethod]
        public void Nao_Criar_Seguradora_Se_Contato_Invalido()
        {
            const string nome = "Nome da Seguradora";
            const string cnpj = "11222333000199";
            const string telefone = "1123456789";
            const string email = "teste@teste.com.br";
            const string contato = "Nome23 do contato";
            const decimal valorPorKm = 0.72M;

            var seguradora = new Seguradora(nome, cnpj, telefone, contato, email, valorPorKm, "", "", "", eventos);


            Assert.IsFalse(seguradora.IsValid());
        }

        [TestCategory("Seguradora - Registrar")]
        [TestMethod]
        public void Nao_Criar_Seguradora_Se_ValorPorKm_Nao_Informado()
        {
            const string nome = "Nome da Seguradora";
            const string cnpj = "11222333000199";
            const string telefone = "1123456789";
            const string email = "teste@teste.com.br";
            const string contato = "Nome23 do contato";
            const decimal valorPorKm = 0;

            var seguradora = new Seguradora(nome, cnpj, telefone, contato, email, valorPorKm, "", "", "", eventos);


            Assert.IsFalse(seguradora.IsValid());
        }
    }
}
