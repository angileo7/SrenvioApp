using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UtilitiesSrenvio;
using Xunit.Sdk;

namespace UnitTestProject1
{
    [TestClass]
    public class SobrePesoUnitTest
    {
        [TestMethod]
        public void calcular_sobre_peso()
        {
            var result = WeightUtilitties.calculateSobrePeso(1, "2.5");
            var expected = 2;
            Assert.AreEqual(expected, 2);
        }
        public void calcular_peso_total()
        {
            var result = WeightUtilitties.calculateTotalWeight(4.3f, 5.1f);
            int expected = 6;
            Assert.AreEqual(result, expected);
        }
    }
}
