using Currency.Service;

namespace CurrencyService.Common
{
    public class ServiceProvider
    {
        private ExchangeService _exchangeService;
        public ExchangeService ExchangeService
        {
            get
            {
                if (_exchangeService == null)
                {
                    _exchangeService = new ExchangeService();
                }

                return _exchangeService;
            } 
        }
    }
}