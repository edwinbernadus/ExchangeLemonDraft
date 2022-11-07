// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using BittrexSchema;
//
//    var tickerSchema = TickerSchema.FromJson(jsonString);
using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace BittrexTickerSchema
{
    using Newtonsoft.Json;

    public partial class Ticker
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("result")]
        public Result Result { get; set; }
    }


    public partial class Result
    {
        [JsonProperty("Bid")]
        public decimal Bid { get; set; }

        [JsonProperty("Ask")]
        public decimal Ask { get; set; }

        [JsonProperty("Last")]
        public decimal Last { get; set; }
    }

    public partial class Ticker
    {
        public static Ticker FromJson(string json) => JsonConvert.DeserializeObject<Ticker>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Ticker self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
