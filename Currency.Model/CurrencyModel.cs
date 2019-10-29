using Common.Enum;

namespace Currency.Model
{
    public class CurrencyModel
    {
        public decimal Price { get; set; }
        public CurrencyEnum Currency { get; set; }
    }
}