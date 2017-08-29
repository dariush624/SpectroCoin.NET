using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SpectroCoin.NET.SendStrategy.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpectroCoin.NET.Model
{
    public class Address
    {
        [JsonProperty("btcAddress")]
        public string BitcoinAddress { get; set; }
        [JsonProperty("cryptoAddress")]
        public string CryptoAddress { get; set; }
        [JsonProperty("currency")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Currency currency { get; set; }
    }
}
