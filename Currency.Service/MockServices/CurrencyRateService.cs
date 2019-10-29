using System;
using System.Threading.Tasks;
using Common.Enum;
using Currency.Model;

namespace Currency.Service.MockServices
{
    public class CurrencyRateService
    {
        // async for the case of a request to the database or file
        public async Task<CurrencyRateModel> GetCurrencyInfoAsync(CurrencyEnum currencyFrom, CurrencyEnum currencyTo)
        {
            var currencyRate = new CurrencyRateModel
            {
                CurrencyFrom = currencyFrom,
                CurrencyTo = currencyTo
            };

            if (currencyFrom == CurrencyEnum.USD)
            {
                currencyRate.Rate = GetRateToUsd(currencyTo);
            }
            else if (currencyTo == CurrencyEnum.USD)
            {
                currencyRate.Rate = 1 / GetRateToUsd(currencyFrom);
            }
            else if (currencyFrom != CurrencyEnum.USD && currencyTo != CurrencyEnum.USD)
            {
                currencyRate.Rate = GetRateToUsd(currencyFrom) / GetRateToUsd(currencyTo);
            }
            else
            {
                throw new ArgumentException("Invalid argument CurrencyEnum passed to GetCurrencyRate method");
            }

            return currencyRate;
        }

        private decimal GetRateToUsd(CurrencyEnum currencyTo)
        {
            switch (currencyTo)
            {
                case CurrencyEnum.Gold:
                    return 1000M;
                case CurrencyEnum.Diamond:
                    return 10M;
                case CurrencyEnum.LC:
                    return 1M;
                case CurrencyEnum.EUR:
                    return 0.91M;
                case CurrencyEnum.GBP:
                    return 0.83M;
                case CurrencyEnum.RUB:
                    return 70M;
                default:
                    throw new ArgumentException("Invalid argument CurrencyEnum passed");
            }
        }
    }
}
