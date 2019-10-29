using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces;

namespace Currency.Service
{
    public class BeautifyService
    {
        private IRoundable roundStrategy;

        public BeautifyService(IRoundable roundStrategy)
        {
            this.roundStrategy = roundStrategy;
        }

        public async Task<decimal> BeautifyRealCurrency(decimal value)
        {
            return await roundStrategy.RoundAsync(value) - 0.01M;
        }

        public async Task<decimal> BeautifyVirtualCurrency(decimal value)
        {
            return await roundStrategy.RoundAsync(value);
        }
    }
}
