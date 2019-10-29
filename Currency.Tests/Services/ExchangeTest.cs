using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enum;
using Currency.Model;
using Currency.Service;
using NUnit.Framework;

namespace CurrencyService.Tests.Services
{
    [TestFixture]
    public class ExchangeTest
    {
        public ExchangeService ExchangeService { get; set; }

        public ExchangeTest()
        {
            ExchangeService = new ExchangeService();
        }

        [Test]
        public async Task GetBeautyCurrency_GetOneRealCurrency()
        {
            // Arrange
            BeautyCurrencyModel beautyCurrency;

            // Act
            var result = await ExchangeService.GetBeautyConvertedCurrencyAsync("750", CurrencyEnum.USD, CurrencyEnum.RUB);
            beautyCurrency = result.Single();

            // Assert
            Assert.IsTrue(beautyCurrency.Currency == "RUB");
            Assert.IsTrue(beautyCurrency.BeautyPrice == "499,99 ₽");
            Assert.IsTrue(beautyCurrency.PriceWithoutDecimal == 49999);
        }

        [Test]
        public async Task GetBeautyCurrency_GetOneVirtualCurrency()
        {
            // Arrange
            BeautyCurrencyModel beautyCurrency;

            // Act
            var result = await ExchangeService.GetBeautyConvertedCurrencyAsync("1499", CurrencyEnum.USD, CurrencyEnum.Gold);
            beautyCurrency = result.Single();

            // Assert
            Assert.IsTrue(beautyCurrency.Currency == "Gold");
            Assert.IsTrue(beautyCurrency.BeautyPrice == "$15,000");
            Assert.IsTrue(beautyCurrency.PriceWithoutDecimal == 1500000);
        }

        [Test]
        public async Task GetBeautyCurrencies_GetMoreThanOne()
        {
            // Act
            var result = (await ExchangeService.GetBeautyConvertedCurrencyAsync("750", CurrencyEnum.USD, CurrencyEnum.None)).ToList();
            
            // Assert
            Assert.IsTrue(result.Count > 1);
        }

        [Test]
        [TestCase("750.15")]
        [TestCase("750,15")]
        [TestCase("750M")]
        [TestCase("750D")]
        [TestCase("750 ")]
        public async Task GetBeautyCurrencies_ThrowExceptionIfNotParsed(string value)
        {
            try
            {
                // Act
                var result = (await ExchangeService.GetBeautyConvertedCurrencyAsync("750.00", CurrencyEnum.USD, CurrencyEnum.None)).ToList();
            }
            catch (Exception e)
            {
                // Assert
                Assert.IsInstanceOf<FormatException>(e);
                return;
            }
            
            Assert.Fail();
        }
    }
}
