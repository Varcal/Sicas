using System;
using System.Collections.Generic;
using Domain.Account.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedKernel.Security;

namespace Domain.Tests.Entities
{
    [TestClass]
    public class UsuarioTests
    {
        [TestCategory("Usuario - Registrar")]
        [TestMethod]
        public void Criar_Usuario_Se_Valido()
        {
            const string nome = "Nome do Usuario";
            const string login = "nome.sobrenome";
            string senha = "123456";
            var perfis = new List<Perfil>() {new Perfil("AdminMaster")};

            var usuario = new Usuario(nome, login, perfis);
           

            Assert.IsNotNull(usuario);
            Assert.AreEqual(nome, usuario.Nome);
            Assert.AreEqual(login, usuario.Login);
            Assert.AreEqual(StringHelper.Encrypt(senha), usuario.Senha);
            Assert.AreNotEqual(DateTime.MinValue, usuario.DataCadastro);
            Assert.IsTrue(usuario.Ativo);
            Assert.IsTrue(usuario.IsValid());
        }
    }
}
