using System;
using System.Threading.Tasks;
using Common.Enum;

namespace Currency.Service.MockServices
{
    public class CurrencyCultureService
    {
        // async for the case of a request to the database or file
        public async Task<string> GetCurrencyCultureAsync(CurrencyEnum currency)
        {
            switch (currency)
            {
                case CurrencyEnum.Gold:
                    return "en-us";
                case CurrencyEnum.Diamond:
                    return "en-us";
                case CurrencyEnum.LC:
                    return "en-us";
                case CurrencyEnum.USD:
                    return "en-us";
                case CurrencyEnum.EUR:
                    return "fr-fr";
                case CurrencyEnum.GBP:
                    return "en-gb";
                case CurrencyEnum.RUB:
                    return "ru-ru";
                default:
                    throw new ArgumentException("Invalid argument CurrencyEnum passed");
            }
        }
    }
}
