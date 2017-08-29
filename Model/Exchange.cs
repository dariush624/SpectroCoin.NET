using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SpectroCoin.NET.SendStrategy.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpectroCoin.NET.Model
{
    public class Exchange
    {
        [JsonProperty("exchangeId")]
        public int exchangeId { get; set; }
        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Status status { get; set; }
        [JsonProperty("payAmount")]
        public double payAmount { get; set; }
        [JsonProperty("payCurrency")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Currency payCurrency { get; set; }
        [JsonProperty("receiveCurrency")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Currency receiveCurrency { get; set; }
        [JsonProperty("receiveAmount")]
        public double receiveAmount { get; set; }
    }
}
