using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedKernel.Validations;

namespace SharedKernel.Tests.Validations
{
    [TestClass]
    public class AssertionConcernTests
    {
        [TestMethod]
        [TestCategory("AssertionConcern")]
        public void AssertRegexIsValid()
        {
            var emailRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            var res = AssertionConcern.AssertRegexMatch("cleber.varcal@varcalsys.com.br", emailRegex, "E-mail inválido");
            Assert.IsNull(res);
        }

        [TestMethod]
        [TestCategory("AssertionConcern")]
        public void AssertRegexIsInvalid()
        {
            var emailRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            var res = AssertionConcern.AssertRegexMatch("cleber.varcal[at]varcalsys[dot]com.br", emailRegex, "E-mail inválido");
            Assert.IsNotNull(res);
        }
               
        [TestMethod]
        [TestCategory("AssertionConcern")]
        public void AssertIsNull()
        {
            var res = AssertionConcern.AssertIsNull(DateTime.Now, "Well... it is not null!");
            Assert.IsNotNull(res);
        }

        [TestMethod]
        [TestCategory("AssertionConcern")]
        public void AssertEmailIsValid()
        {
            var res = AssertionConcern.AssertEmailIsValid("cleber.varcal@varcalsys.com.br", "E-mail inválido");
            Assert.IsNull(res);
        }

        [TestMethod]
        [TestCategory("AssertionConcern")]
        public void AssertEmailIsInvalid()
        {
            var res = AssertionConcern.AssertEmailIsValid("cleber.varcal[at]varcalsys[dot]com.br", "E-mail inválido");
            Assert.IsNotNull(res);
        }

        [TestMethod]
        [TestCategory("AssertionConcern")]
        public void AssertUrlIsValid()
        {
            var res = AssertionConcern.AssertUrlIsValid("http://varcalsys.com.br/", "URL inválida");
            Assert.IsNull(res);
        }

        [TestMethod]
        [TestCategory("AssertionConcern")]
        public void AssertUrlIsInvalid()
        {
            var res = AssertionConcern.AssertUrlIsValid("agá tê tê pê dois pontos barra barra andrebaltieri.net", "URL inválido");
            Assert.IsNotNull(res);
        }

        [TestMethod]
        [TestCategory("AssertionConcern")]
        public void AssertOnlyNumbersIsValid()
        {
            var res = AssertionConcern.AssertOnlyNumber("123456", "Permitido somente números");
            Assert.IsNull(res);
        }

        [TestMethod]
        [TestCategory("AssertionConcern")]
        public void AssertOnlyNumbersIsInvalid()
        {
            var res = AssertionConcern.AssertOnlyNumber("123456A", "Permitido somente números");
            Assert.IsNotNull(res);
        }

        [TestMethod]
        [TestCategory("AssertionConcern")]
        public void AssertOnlyNameIsValid()
        {
            var res = AssertionConcern.AssertNameIsValid("Cleber Eduardo Varçal", "Nome não pode conter números");
            Assert.IsNull(res);
        }

        [TestMethod]
        [TestCategory("AssertionConcern")]
        public void AssertOnlyNameIsInvalid()
        {
            var res = AssertionConcern.AssertNameIsValid("Cleber Eduardo3 Var4cal", "Nome não pode conter números");
            Assert.IsNotNull(res);
        }

        [TestMethod]
        [TestCategory("AssertionConcern")]
        public void AssertPhoneIsValid()
        {
            var res = AssertionConcern.AssertPhoneIsValid("1123456789", "Telefone inválido");
            Assert.IsNull(res);
        }

        [TestMethod]
        [TestCategory("AssertionConcern")]
        public void AssertPhoneIsInvalid()
        {
            var res = AssertionConcern.AssertPhoneIsValid("11234567890", "Telefone inválido");
            Assert.IsNotNull(res);
        }

        [TestMethod]
        [TestCategory("AssertionConcern")]
        public void AssertCNPJIsValid()
        {
            var res = AssertionConcern.AssertCnpjIsValid("11222333000199", "CNPJ inválido");
            Assert.IsNull(res);
        }

        [TestMethod]
        [TestCategory("AssertionConcern")]
        public void AssertCNPJIsInvalid()
        {
            var res = AssertionConcern.AssertCnpjIsValid("112223330001993", "CNPJ inválido");
            Assert.IsNotNull(res);
        }
    }
}
