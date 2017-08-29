using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SpectroCoin.NET.SendStrategy.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpectroCoin.NET.Model
{
    public class Payment
    {
        [JsonProperty("paymentId")]
        public string Id { get; set; }
        [JsonProperty("withdrawAmount")]
        public double WithdrawAmount { get; set; }
        [JsonProperty("receiveAmount")]
        public double ReceiveAmount { get; set; }
        [JsonProperty("currency")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Currency Currency { get; set; }
        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }
    }
}
