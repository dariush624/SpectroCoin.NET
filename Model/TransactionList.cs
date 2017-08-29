using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpectroCoin.NET.Model
{
    public class TransactionList
    {
        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }
        [JsonProperty("history")]
        public List<Transaction> History { get; set; }
    }
}
