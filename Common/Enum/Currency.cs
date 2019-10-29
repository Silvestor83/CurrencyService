using System.ComponentModel;

namespace Common.Enum
{
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum CurrencyEnum
    {
        None = 0,
        [Description("Gold")]
        Gold = 1,
        [Description("Diamond")]
        Diamond,
        [Description("Light crystal")]
        LC,
        //...For future in-game currency

        [Description("USA Dollar")]
        USD = 100,
        [Description("Euro")]
        EUR,
        [Description("Great Britain Pound")]
        GBP,
        [Description("Russian ruble")]
        RUB
    }
}