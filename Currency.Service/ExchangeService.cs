using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Common.Enum;
using Common.Utils;
using Currency.Model;
using Currency.Service.MockServices;
using Currency.Service.StrategyServices;

namespace Currency.Service
{
    public class ExchangeService
    {
        public async Task<IEnumerable<BeautyCurrencyModel>> GetBeautyConvertedCurrencyAsync(string priceWithoutDecimal, CurrencyEnum currencyFrom, CurrencyEnum currencyTo)
        {
            if (currencyTo == CurrencyEnum.None)
            {
                return await GetAllBeautyCurrenciesAsync(priceWithoutDecimal, currencyFrom);
            }

            var beautyCurrencyModel = await GetBeautyCurrencyAsync(priceWithoutDecimal, currencyFrom, currencyTo);

            return new List<BeautyCurrencyModel> {beautyCurrencyModel};
        }

        private decimal GetDecimalPrice(string priceWithoutDecimal)
        {
            if (!int.TryParse(priceWithoutDecimal, out var intPriceWithoutDecimal))
            {
                throw new FormatException($"Not possible to convert string: '{priceWithoutDecimal}' to int value.");
            }

            return intPriceWithoutDecimal / 100M;
        }

        private async Task<List<BeautyCurrencyModel>> GetAllBeautyCurrenciesAsync(string priceWithoutDecimal, CurrencyEnum currencyFrom)
        {
            List<BeautyCurrencyModel> list = new List<BeautyCurrencyModel>();
            var currencyEnums = EnumUtil.GetValues<CurrencyEnum>().Where(cur => cur != CurrencyEnum.None && cur != currencyFrom);

            foreach (var currencyTo in currencyEnums)
            {
                var currencyModel = await GetBeautyCurrencyAsync(priceWithoutDecimal, currencyFrom, currencyTo);
                list.Add(currencyModel);
            }

            return list;
        }

        private async Task<BeautyCurrencyModel> GetBeautyCurrencyAsync(string priceWithoutDecimal, CurrencyEnum currencyFrom, CurrencyEnum currencyTo)
        {
            var currencyModelFrom = new CurrencyModel
            {
                Currency = currencyFrom,
                Price = GetDecimalPrice(priceWithoutDecimal)
            };

            var convertedCurrency = await ConvertCurrencyAsync(currencyModelFrom, currencyTo);

            return await BeatifyCurrencyAsync(convertedCurrency);
        }

        private async Task<BeautyCurrencyModel> BeatifyCurrencyAsync(CurrencyModel currency)
        {
            var beautyService = new BeautifyService(new BeautifyRounding());
            var cultureService = new CurrencyCultureService();
            var culture = await cultureService.GetCurrencyCultureAsync(currency.Currency);

            int priceWithoutDecimal;
            string format;

            if (currency.Currency < CurrencyEnum.USD)
            {
                priceWithoutDecimal = (int)(await beautyService.BeautifyVirtualCurrency(currency.Price) * 100);
                format = "C0";
            }
            else
            {
                priceWithoutDecimal = (int)(await beautyService.BeautifyRealCurrency(currency.Price) * 100);
                format = "C";
            }

            var beautyCurrency = new BeautyCurrencyModel
            {
                PriceWithoutDecimal = priceWithoutDecimal,
                BeautyPrice = (priceWithoutDecimal / 100M).ToString(format, new CultureInfo(culture)),
                Currency = currency.Currency.ToString()
            };

            return beautyCurrency;
        }

        private async Task<CurrencyModel> ConvertCurrencyAsync(CurrencyModel currencyModelFrom, CurrencyEnum currencyTo)
        {
            var currency = new CurrencyModel();

            var currencyRateService = new CurrencyRateService();
            var currencyInfo = await currencyRateService.GetCurrencyInfoAsync(currencyModelFrom.Currency, currencyTo);

            currency.Currency = currencyTo;
            currency.Price = currencyModelFrom.Price * currencyInfo.Rate;
            
            return currency;
        }
    }
}
