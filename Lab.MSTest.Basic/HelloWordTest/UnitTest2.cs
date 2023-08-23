using HelloWordCore.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelloWordTest
{
    [TestClass]
    public class UnitTest2
    {
        private readonly PrimeService _primeService;

        public UnitTest2()
        {
            _primeService = new PrimeService();
        }

        [TestMethod]
        [DataRow(3)]
        public void IsPrime_InputIs1_ReturnFalse(int value)
        {
            bool result = _primeService.IsPrime(value);

            Assert.IsTrue(result, $"{value} không chia hết cho 2");
        }
    }
}
