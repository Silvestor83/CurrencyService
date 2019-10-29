using System;
using System.Threading.Tasks;
using Common.Interfaces;
using Currency.Service.StrategyServices;
using NUnit.Framework;

namespace CurrencyService.Tests.Services
{
    [TestFixture]
    public class RoundTest
    {
        private IRoundable Roundable { get; set; }
        public RoundTest()
        {
            Roundable = new BeautifyRounding();
        } 

        [Test]
        [TestCase(15.45, 15)]
        [TestCase(15.5, 16)]
        [TestCase(0, 1)]
        [TestCase(45, 50)]
        [TestCase(90, 100)]
        [TestCase(499, 500)]
        public async Task RoundDecimalToBeautyValue(decimal value, decimal awaitValue)
        {
            // Act
            var result = await Roundable.RoundAsync(value);

            // Assert
            Assert.AreEqual(awaitValue, result);
        }

        [Test]
        [TestCase(-1)]
        public async Task RoundDecimal_ThrowException(decimal value)
        {
            try
            {
                // Act
                var result = await Roundable.RoundAsync(value);
            }
            catch (Exception e)
            {
                // Assert
                Assert.IsInstanceOf<ArgumentException>(e);
                return;
            }

            Assert.Fail();
        }
    }
}
