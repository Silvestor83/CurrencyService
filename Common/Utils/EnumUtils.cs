using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Common.Utils
{
    public static class EnumUtil
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return System.Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static string GetDescription<T>(this System.Enum value)
        {
            var type = value.GetType();
            
            // Get fieldinfo for this type
            var fieldInfo = type.GetField(value.ToString());

            // Get the stringValue attributes
            var attribs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            // Return the first if there was a match.
            return attribs.Length > 0 && attribs[0] is DescriptionAttribute atr
                ? atr.Description 
                : null;
        }
    }
}

