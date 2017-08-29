using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpectroCoin.NET.Exceptions
{
    public class ValidationError
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
