using System;
using System.Collections.Generic;
using System.Net;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public partial class BitcoinSchema
{
    [JsonProperty("block_height")]
    public long BlockHeight { get; set; }

    [JsonProperty("block_index")]
    public long BlockIndex { get; set; }

    [JsonProperty("hash")]
    public string Hash { get; set; }

    [JsonProperty("addresses")]
    public string[] Addresses { get; set; }

    [JsonProperty("total")]
    public long Total { get; set; }

    [JsonProperty("fees")]
    public long Fees { get; set; }

    [JsonProperty("size")]
    public long Size { get; set; }

    [JsonProperty("preference")]
    public string Preference { get; set; }

    [JsonProperty("relayed_by")]
    public string RelayedBy { get; set; }

    [JsonProperty("received")]
    public System.DateTimeOffset Received { get; set; }

    [JsonProperty("ver")]
    public long Ver { get; set; }

    [JsonProperty("double_spend")]
    public bool DoubleSpend { get; set; }

    [JsonProperty("vin_sz")]
    public long VinSz { get; set; }

    [JsonProperty("vout_sz")]
    public long VoutSz { get; set; }

    [JsonProperty("confirmations")]
    public long Confirmations { get; set; }

    [JsonProperty("inputs")]
    public Input[] Inputs { get; set; }

    [JsonProperty("outputs")]
    public Output[] Outputs { get; set; }
}

public partial class Input
{
    [JsonProperty("prev_hash")]
    public string PrevHash { get; set; }

    [JsonProperty("output_index")]
    public long OutputIndex { get; set; }

    [JsonProperty("script")]
    public string Script { get; set; }

    [JsonProperty("output_value")]
    public long OutputValue { get; set; }

    [JsonProperty("sequence")]
    public long Sequence { get; set; }

    [JsonProperty("addresses")]
    public string[] Addresses { get; set; }

    [JsonProperty("script_type")]
    public string ScriptType { get; set; }

    [JsonProperty("age")]
    public long Age { get; set; }
}

public partial class Output
{
    [JsonProperty("value")]
    public long Value { get; set; }

    [JsonProperty("script")]
    public string Script { get; set; }

    [JsonProperty("addresses")]
    public string[] Addresses { get; set; }

    [JsonProperty("script_type")]
    public string ScriptType { get; set; }
}

public partial class BitcoinSchema
{
    public static BitcoinSchema FromJson(string json) => JsonConvert.DeserializeObject<BitcoinSchema>(json, Converter.Settings);
}

public static class Serialize
{
    public static string ToJson(this BitcoinSchema self) => JsonConvert.SerializeObject(self, Converter.Settings);
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