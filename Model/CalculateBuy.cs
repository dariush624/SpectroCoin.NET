using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SpectroCoin.NET.SendStrategy.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpectroCoin.NET.Model
{
    public class CalculateBuy
    {
        [JsonProperty("payAmount")]
        public double PayAmount { get; set; }
        [JsonProperty("payCurrency")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Currency PayCurrency { get; set; }
    }
}
