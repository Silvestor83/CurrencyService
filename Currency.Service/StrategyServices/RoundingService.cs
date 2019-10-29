using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces;

namespace Currency.Service.StrategyServices
{
    public class BeautifyRounding : IRoundable
    {
        private readonly int[] array = new int[]
        {
            1, 2, 3, 4, 5, 6, 8, 10,
            12, 14, 15, 16, 18, 20,
            25, 30, 40, 50,
            60, 80, 100
        };

        // async for the case of a request to the database or file
        public async Task<decimal> RoundAsync(decimal value)
        {
            if (value < 0)
            {
                throw new ArgumentException("The value is less then 0");
            }

            var countDigits = (int)Math.Log10((int)value) + 1;
            var tempAdditionalCountDigits = 0d;

            if (countDigits > 2)
            {
                tempAdditionalCountDigits = countDigits - 2;
                value /= (decimal)Math.Pow(10, tempAdditionalCountDigits);
            }

            var intValue = (int)Math.Round(value, 0, MidpointRounding.AwayFromZero);

            int min = 0;
            int max = array.Length - 1;

            while (max - min > 1)
            {
                int mid = (max + min) / 2;

                if (array[mid] < intValue)
                {
                    min = mid;
                }
                else
                {
                    max = mid;
                }
            }
        
            int result = array[max] - value <= value - array[min] ? array[max] : array[min];

            return result * (int)Math.Pow(10, tempAdditionalCountDigits);
        }
    }
}
