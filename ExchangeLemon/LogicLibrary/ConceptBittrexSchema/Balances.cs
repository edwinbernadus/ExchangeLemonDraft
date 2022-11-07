// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using BittrexBalancesSchema;
//
//    var balances = Balances.FromJson(jsonString);

namespace BittrexBalancesSchema
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Balances
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("result")]
        public List<Result> Result { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("Currency")]
        public string Currency { get; set; }

        [JsonProperty("Balance")]
        public decimal Balance { get; set; }

        [JsonProperty("Available")]
        public decimal Available { get; set; }

        [JsonProperty("Pending")]
        public decimal Pending { get; set; }

        [JsonProperty("CryptoAddress")]
        public string CryptoAddress { get; set; }
    }

    public partial class Balances
    {
        public static Balances FromJson(string json) => JsonConvert.DeserializeObject<Balances>(json, BittrexBalancesSchema.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Balances self) => JsonConvert.SerializeObject(self, BittrexBalancesSchema.Converter.Settings);
    }

    internal class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
