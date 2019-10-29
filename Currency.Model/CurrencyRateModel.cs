using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enum;

namespace Currency.Model
{
    public class CurrencyRateModel
    {
        public CurrencyEnum CurrencyFrom { get; set; }
        public CurrencyEnum CurrencyTo { get; set; }
        public decimal Rate { get; set; }
    }
}
