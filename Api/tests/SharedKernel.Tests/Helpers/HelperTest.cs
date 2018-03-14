using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedKernel.Helpers;

namespace SharedKernel.Tests.Helpers
{
    [TestClass]
    public class HelperTest
    {
        [TestMethod]
        [TestCategory("Helpers - Fonetização")]
        public void FonetizarLogradouro_Rua()
        {
            var logradouroFonetizado = "J135";

            var logradouro1 = "Rua João Betim";
            var logradouro2 = "R Joâo Betim";
            var logradouro3 = "R. Joáo Betim";

            var fonetizado1 = Soundex.FonetizarLogradouro(logradouro1);
            var fonetizado2 = Soundex.FonetizarLogradouro(logradouro2);
            var fonetizado3 = Soundex.FonetizarLogradouro(logradouro3);

            Assert.AreEqual(logradouroFonetizado, fonetizado1);
            Assert.AreEqual(logradouroFonetizado, fonetizado2);
            Assert.AreEqual(logradouroFonetizado, fonetizado3);
        }

        [TestMethod]
        [TestCategory("Helpers - Fonetização")]
        public void FonetizarLogradouro_Avenida()
        {
            var logradouroFonetizado = "J135";
            var logradouro1 = "Avenida João Betim Betim";
            var logradouro2 = "Av João Betim betim";
            var logradouro3 = "Av. João Betim BEtim";

            var fonetizado1 = Soundex.FonetizarLogradouro(logradouro1);
            var fonetizado2 = Soundex.FonetizarLogradouro(logradouro2);
            var fonetizado3 = Soundex.FonetizarLogradouro(logradouro3);

            Assert.AreEqual(logradouroFonetizado, fonetizado1);
            Assert.AreEqual(logradouroFonetizado, fonetizado2);
            Assert.AreEqual(logradouroFonetizado, fonetizado3);
        }

        [TestMethod]
        [TestCategory("Helpers - Fonetização")]
        public void FonetizarLogradouro_Praca()
        {
            var logradouroFonetizado = "J135";
            var logradouro1 = "Praça João Betim";
            var logradouro2 = "Pç João Betim";
            var logradouro3 = "Pç. João Betim";

            var fonetizado1 = Soundex.FonetizarLogradouro(logradouro1);
            var fonetizado2 = Soundex.FonetizarLogradouro(logradouro2);
            var fonetizado3 = Soundex.FonetizarLogradouro(logradouro3);

            Assert.AreEqual(logradouroFonetizado, fonetizado1);
            Assert.AreEqual(logradouroFonetizado, fonetizado2);
            Assert.AreEqual(logradouroFonetizado, fonetizado3);
        }       
    }
}
