using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Common.Enum;
using Currency.Model;

namespace CurrencyService.Controllers
{
    [RoutePrefix("api/Currency")]
    public class CurrencyController : BaseApiController
    {
        [HttpGet]
        [Route("ConvertFromUsd")]
        public async Task<IEnumerable<BeautyCurrencyModel>> ConvertCurrencyFromUsd(string price, CurrencyEnum currencyTo = CurrencyEnum.None)
        {
            return await Services.ExchangeService.GetBeautyConvertedCurrencyAsync(price, CurrencyEnum.USD, currencyTo);
        }
    }
}
