using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpectroCoin.NET.Model
{
    public class Transaction
    {
        [JsonProperty("transactionNo")]
        public string Number { get; set; }
        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TransactionStatus status { get; set; }
        [JsonProperty("amount")]
        public double amount { get; set; }
        [JsonProperty("description")]
        public string description { get; set; }
        [JsonProperty("date")]
        public DateTime date { get; set; }
    }
}
