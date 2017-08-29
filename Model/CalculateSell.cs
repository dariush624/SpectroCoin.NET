using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SpectroCoin.NET.SendStrategy.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpectroCoin.NET.Model
{
    public class CalculateSell
    {
        [JsonProperty("receiveAmount")]
        public double ReceiveAmount { get; set; }
        [JsonProperty("receiveCurrency")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Currency ReceiveCurrency { get; set; }
    }
}
