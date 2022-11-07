// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using BittrexSchema;
//
//    var marketHistorySchema = MarketHistorySchema.FromJson(jsonString);

namespace BittrexMarketHistorySchema {
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.Serialization;
    using System;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json;

    public partial class MarketHistory {
        [JsonProperty ("success")]
        public bool Success { get; set; }

        [JsonProperty ("message")]
        public string Message { get; set; }

        [JsonProperty ("result")]
        public List<Result> Result { get; set; }
    }

    public partial class Result {
        [JsonProperty ("Id")]
        public long Id { get; set; }

        [JsonProperty ("TimeStamp")]
        public System.DateTimeOffset TimeStamp { get; set; }

        [JsonProperty ("Quantity")]
        public double Quantity { get; set; }

        [JsonProperty ("Price")]
        public double Price { get; set; }

        [JsonProperty ("Total")]
        public double Total { get; set; }

        //[JsonProperty("FillType")]
        //[JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty ("FillType"), JsonConverter (typeof (StringEnumConverter))]
        public FillType FillType { get; set; }

        [JsonProperty ("OrderType"), JsonConverter (typeof (StringEnumConverter))]

        public OrderType OrderType { get; set; }
    }

    //public enum PermissionType
    //{
    //    [EnumMember(Value = "can_fly")]
    //    PermissionToFly,

    //    [EnumMember(Value = "can_swim")]
    //    PermissionToSwim
    //}
    //public enum FillType { Fill, PartialFill };

    public enum FillType {
        [EnumMember (Value = "FILL")]
        Fill,

        [EnumMember (Value = "PARTIAL_FILL")]
        PartialFill
    }

    //public enum OrderType { Buy, Sell };
    public enum OrderType {
        [EnumMember (Value = "BUY")]
        Buy,

        [EnumMember (Value = "SELL")]
        Sell
    }

    public partial class MarketHistory {
        public static MarketHistory FromJson (string json) => JsonConvert.DeserializeObject<MarketHistory> (json, Converter.Settings);
    }

    static class FillTypeExtensions {
        public static FillType? ValueForString (string str) {
            switch (str) {
                case "FILL":
                    return FillType.Fill;
                case "PARTIAL_FILL":
                    return FillType.PartialFill;
                default:
                    return null;
            }
        }

        public static FillType ReadJson (JsonReader reader, JsonSerializer serializer) {
            var str = serializer.Deserialize<string> (reader);
            var maybeValue = ValueForString (str);
            if (maybeValue.HasValue) return maybeValue.Value;
            throw new Exception ("Unknown enum case " + str);
        }

        public static void WriteJson (this FillType value, JsonWriter writer, JsonSerializer serializer) {
            switch (value) {
                case FillType.Fill:
                    serializer.Serialize (writer, "FILL");
                    break;
                case FillType.PartialFill:
                    serializer.Serialize (writer, "PARTIAL_FILL");
                    break;
            }
        }
    }

    static class OrderTypeExtensions {
        public static OrderType? ValueForString (string str) {
            switch (str) {
                case "BUY":
                    return OrderType.Buy;
                case "SELL":
                    return OrderType.Sell;
                default:
                    return null;
            }
        }

        public static OrderType ReadJson (JsonReader reader, JsonSerializer serializer) {
            var str = serializer.Deserialize<string> (reader);
            var maybeValue = ValueForString (str);
            if (maybeValue.HasValue) return maybeValue.Value;
            throw new Exception ("Unknown enum case " + str);
        }

        public static void WriteJson (this OrderType value, JsonWriter writer, JsonSerializer serializer) {
            switch (value) {
                case OrderType.Buy:
                    serializer.Serialize (writer, "BUY");
                    break;
                case OrderType.Sell:
                    serializer.Serialize (writer, "SELL");
                    break;
            }
        }
    }

    public static class Serialize {
        public static string ToJson (this MarketHistory self) => JsonConvert.SerializeObject (self, Converter.Settings);
    }

    internal class Converter : JsonConverter {
        public override bool CanConvert (Type t) => t == typeof (FillType) || t == typeof (OrderType) || t == typeof (FillType?) || t == typeof (OrderType?);

        public override object ReadJson (JsonReader reader, Type t, object existingValue, JsonSerializer serializer) {
            if (t == typeof (FillType))
                return FillTypeExtensions.ReadJson (reader, serializer);
            if (t == typeof (OrderType))
                return OrderTypeExtensions.ReadJson (reader, serializer);
            if (t == typeof (FillType?)) {
                if (reader.TokenType == JsonToken.Null) return null;
                return FillTypeExtensions.ReadJson (reader, serializer);
            }
            if (t == typeof (OrderType?)) {
                if (reader.TokenType == JsonToken.Null) return null;
                return OrderTypeExtensions.ReadJson (reader, serializer);
            }
            throw new Exception ("Unknown type");
        }

        public override void WriteJson (JsonWriter writer, object value, JsonSerializer serializer) {
            var t = value.GetType ();
            if (t == typeof (FillType)) {
                ((FillType) value).WriteJson (writer, serializer);
                return;
            }
            if (t == typeof (OrderType)) {
                ((OrderType) value).WriteJson (writer, serializer);
                return;
            }
            throw new Exception ("Unknown type");
        }

        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
            new Converter (),
            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}