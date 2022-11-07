// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using BittrexOpenOrderSchema;
//
//    var openOrder = OpenOrder.FromJson(jsonString);

namespace BittrexOpenOrderSchema
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class OpenOrder
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
        [JsonProperty("Uuid")]
        public object Uuid { get; set; }

        [JsonProperty("OrderUuid")]
        public string OrderUuid { get; set; }

        [JsonProperty("Exchange")]
        public string Exchange { get; set; }

        [JsonProperty("OrderType")]
        public string OrderType { get; set; }

        [JsonProperty("Quantity")]
        public decimal Quantity { get; set; }

        [JsonProperty("QuantityRemaining")]
        public decimal QuantityRemaining { get; set; }

        [JsonProperty("Limit")]
        public decimal Limit { get; set; }

        [JsonProperty("CommissionPaid")]
        public long CommissionPaid { get; set; }

        [JsonProperty("Price")]
        public long Price { get; set; }

        [JsonProperty("PricePerUnit")]
        public double? PricePerUnit { get; set; }

        [JsonProperty("Opened")]
        public System.DateTimeOffset Opened { get; set; }

        [JsonProperty("Closed")]
        public string Closed { get; set; }

        [JsonProperty("CancelInitiated")]
        public bool CancelInitiated { get; set; }

        [JsonProperty("ImmediateOrCancel")]
        public bool ImmediateOrCancel { get; set; }

        [JsonProperty("IsConditional")]
        public bool IsConditional { get; set; }

        [JsonProperty("Condition")]
        public string Condition { get; set; }

        [JsonProperty("ConditionTarget")]
        public string ConditionTarget { get; set; }
    }

    public partial class OpenOrder
    {
        public static OpenOrder FromJson(string json) => JsonConvert.DeserializeObject<OpenOrder>(json, BittrexOpenOrderSchema.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this OpenOrder self) => JsonConvert.SerializeObject(self, BittrexOpenOrderSchema.Converter.Settings);
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
