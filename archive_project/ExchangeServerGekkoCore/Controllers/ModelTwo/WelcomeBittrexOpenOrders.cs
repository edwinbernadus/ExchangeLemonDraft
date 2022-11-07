using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
//using gekko_bot.BittrexTrade;
//using gekko_bot.WelcomeBittrexTicker;

namespace BlueLight.Main {
    public partial class WelcomeBittrexOpenOrders {
        [JsonProperty ("success")]
        public bool Success { get; set; }

        [JsonProperty ("message")]
        public string Message { get; set; }

        [JsonProperty ("result")]
        public Result[] Result { get; set; }
    }

    public partial class Result {
        [JsonProperty ("Uuid")]
        public object Uuid { get; set; }

        [JsonProperty ("OrderUuid")]
        public string OrderUuid { get; set; }

        [JsonProperty ("Exchange")]
        public string Exchange { get; set; }

        [JsonProperty ("OrderType")]
        public string OrderType { get; set; }

        [JsonProperty ("Quantity")]
        public double Quantity { get; set; }

        [JsonProperty ("QuantityRemaining")]
        public double QuantityRemaining { get; set; }

        [JsonProperty ("Limit")]
        public double Limit { get; set; }

        [JsonProperty ("CommissionPaid")]
        public long CommissionPaid { get; set; }

        [JsonProperty ("Price")]
        public double Price { get; set; }

        [JsonProperty ("PricePerUnit")]
        public object PricePerUnit { get; set; }

        [JsonProperty ("Opened")]
        public System.DateTimeOffset Opened { get; set; }

        [JsonProperty ("Closed")]
        public object Closed { get; set; }

        [JsonProperty ("CancelInitiated")]
        public bool CancelInitiated { get; set; }

        [JsonProperty ("ImmediateOrCancel")]
        public bool ImmediateOrCancel { get; set; }

        [JsonProperty ("IsConditional")]
        public bool IsConditional { get; set; }

        [JsonProperty ("Condition")]
        public object Condition { get; set; }

        [JsonProperty ("ConditionTarget")]
        public object ConditionTarget { get; set; }
    }

    public partial class WelcomeBittrexOpenOrders {
        public static WelcomeBittrexOpenOrders FromJson (string json) => JsonConvert.DeserializeObject<WelcomeBittrexOpenOrders> (json, Converter.Settings);
    }

    public static class Serialize {
        public static string ToJson (this WelcomeBittrexOpenOrders self) => JsonConvert.SerializeObject (self, Converter.Settings);
    }

    internal class Converter {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}