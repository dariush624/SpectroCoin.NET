using SpectroCoin.NET.SendStrategy.Interface;
using SpectroCoin.NET.Model;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SpectroCoin.NET.WebUtility;
using Newtonsoft.Json;

namespace SpectroCoin.NET.SendStrategy
{
    class BitcoinStrategy : ISendStrategy
    {
        public async Task<Payment> Send(string receiver, double amount, string accessToken)
        {
            JArray sendRequests = new JArray();
            JObject request = new JObject
            {
                ["amount"] = amount,
                ["receiver"] = receiver
            };
            sendRequests.Add(request);
            string result = await "https://spectrocoin.com/api/r/wallet/send/BTC".Post(null, sendRequests.ToString(), accessToken);
            return JsonConvert.DeserializeObject<Payment>(result);
        }
    }
}
