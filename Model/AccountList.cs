using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpectroCoin.NET.Model
{
    public class AccountList
    {
        [JsonProperty("accounts")]
        public List<Account> Accounts { get; set; }
    }
}
