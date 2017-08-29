using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SpectroCoin.NET.SendStrategy.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpectroCoin.NET.Model
{
    public class Account
    {
        [JsonProperty("accountId")]
        public int accountId { get; set; }
        [JsonProperty("balance")]
        public double balance { get; set; }
        [JsonProperty("currencyName")]
        public string currencyName { get; set; }
        [JsonProperty("currencyCode")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Currency currencyCode { get; set; }
        [JsonProperty("availableBalance")]
        public double availableBalance { get; set; }
        [JsonProperty("reservedAmount")]
        public double reservedAmount { get; set; }
    }
}
