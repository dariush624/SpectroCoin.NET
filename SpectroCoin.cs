using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpectroCoin.NET.Exceptions;
using SpectroCoin.NET.Model;
using SpectroCoin.NET.SendStrategy.Factory;
using SpectroCoin.NET.WebUtility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// C# Library for SpectroCoin API
/// Author: Dariush Moshiri
/// E-mail: dariush.moshiri@outlook.com
/// 2017
/// </summary>
namespace SpectroCoin.NET
{
    public class SpectroCoin
    {
        private string clientId;
        private string clientSecret;
        private string version;
        private string scopes;

        private const string HOST = "https://spectrocoin.com/api/r";
        private const string OAUTH = HOST + "/oauth2/auth";
        private const string OAUTH_REFRESH = HOST + "/oauth2/refresh";
        private const string CALCULATE_BUY = HOST + "/wallet/exchange/calculate/buy";
        private const string CALCULATE_SELL = HOST + "/wallet/exchange/calculate/sell";
        private const string EXCHANGE_BUY = HOST + "/wallet/exchange/buy";
        private const string EXCHANGE_SELL = HOST + "/wallet/exchange/sell";
        private const string ACCOUNTS = HOST + "/wallet/accounts";
        private const string ACCOUNT = HOST + "/wallet/account/";
        private const string LAST_ADDRESS = HOST + "/wallet/deposit/{0}/last";
        private const string NEW_ADDRESS = HOST + "/wallet/deposit/{0}/fresh";

        public SpectroCoin(string clientId, string clientSecret, string version = "1.0", string scopes = "send_currency currency_exchange user_account")
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            this.version = version;
            this.scopes = scopes;
        }

        public async Task<Authenticator> Login()
        {
            try
            {
                string content = JObject.FromObject(new
                {
                    client_id = this.clientId,
                    client_secret = this.clientSecret,
                    version = this.version,
                    scope = this.scopes
                }).ToString();

                return JsonConvert.DeserializeObject<Authenticator>(await OAUTH.Post(null, content));
            } catch(HttpRequestException e)
            {
                throw new ValidationException(JsonConvert.DeserializeObject<List<ValidationError>>(e.Message));
            }
        }

        public async Task<Authenticator> Refresh(string refreshToken)
        {
            try
            {
                string content = JObject.FromObject(new
                {
                    client_id = this.clientId,
                    client_secret = this.clientSecret,
                    version = this.version,
                    refresh_token = refreshToken
                }).ToString();

                return JsonConvert.DeserializeObject<Authenticator>(await OAUTH_REFRESH.Post(null, content));
            }
            catch (HttpRequestException e)
            {
                throw new ValidationException(JsonConvert.DeserializeObject<List<ValidationError>>(e.Message));
            }
        }

        public async Task<Payment> SendBTC(string receiver, double amount, string accessToken)
        {
            try
            {
                return await SendStrategies.Instance[Currency.BTC].Send(receiver, amount, accessToken);
            } catch(HttpRequestException e)
            {
                throw new ValidationException(JsonConvert.DeserializeObject<List<ValidationError>>(e.Message));
            }
        }

        public async Task<CalculateBuy> CalculateBuy(double receiveAmount, Currency receiveCurrency, Currency payCurrency, string accessToken)
        {
            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>()
                {
                    {"receiveAmount", receiveAmount.ToString() },
                    {"receiveCurrency", receiveCurrency.ToString() },
                    {"payCurrency", payCurrency.ToString() }
                };
                Debug.WriteLine(await CALCULATE_BUY.Get(parameters, accessToken));
                return JsonConvert.DeserializeObject<CalculateBuy>(await CALCULATE_BUY.Get(parameters, accessToken));
            }
            catch (HttpRequestException e)
            {
                throw new ValidationException(JsonConvert.DeserializeObject<List<ValidationError>>(e.Message));
            }
        }

        public async Task<CalculateSell> CalculateSell(double payAmount, Currency receiveCurrency, Currency payCurrency, string accessToken)
        {
            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>()
                {
                    {"payAmount", payAmount.ToString() },
                    {"receiveCurrency", receiveCurrency.ToString() },
                    {"payCurrency", payCurrency.ToString() }
                };
                return JsonConvert.DeserializeObject<CalculateSell>(await CALCULATE_BUY.Get(parameters, accessToken));
            }
            catch (HttpRequestException e)
            {
                throw new ValidationException(JsonConvert.DeserializeObject<List<ValidationError>>(e.Message));
            }
        }

        public async Task<Exchange> ExchangeBuy(double receiveAmount, Currency receiveCurrency, Currency payCurrency, string accessToken)
        {
            try
            {
                string content = JObject.FromObject(new
                {
                    receiveAmount = receiveAmount,
                    receiveCurrency = receiveCurrency.ToString(),
                    payCurrency = payCurrency.ToString(),
                }).ToString();

                return JsonConvert.DeserializeObject<Exchange>(await EXCHANGE_BUY.Post(null, content, accessToken));
            } catch(HttpRequestException e)
            {
                throw new ValidationException(JsonConvert.DeserializeObject<List<ValidationError>>(e.Message));
            }
        }

        public async Task<Exchange> ExchangeSell(double payAmount, Currency receiveCurrency, Currency payCurrency, string accessToken)
        {
            try
            {
                string content = JObject.FromObject(new
                {
                    payAmount = payAmount,
                    receiveCurrency = receiveCurrency.ToString(),
                    payCurrency = payCurrency.ToString(),
                }).ToString();

                return JsonConvert.DeserializeObject<Exchange>(await EXCHANGE_SELL.Post(null, content, accessToken));
            }
            catch (HttpRequestException e)
            {
                throw new ValidationException(JsonConvert.DeserializeObject<List<ValidationError>>(e.Message));
            }
        }

        public async Task<AccountList> GetAccounts(string accessToken)
        {
            try
            {
                return JsonConvert.DeserializeObject<AccountList>(await ACCOUNTS.Get(null, accessToken));
            } catch(HttpRequestException e)
            {
                throw new ValidationException(JsonConvert.DeserializeObject<List<ValidationError>>(e.Message));
            }
        }

        public async Task<TransactionList> GetAccount(long accountId, string accessToken, int page = 1, int size = 4)
        {
            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>()
                {
                    {"page", page.ToString() },
                    {"size", size.ToString() }
                };
                return JsonConvert.DeserializeObject<TransactionList>(await (ACCOUNT + accountId).Get(parameters, accessToken));
            }
            catch (HttpRequestException e)
            {
                throw new ValidationException(JsonConvert.DeserializeObject<List<ValidationError>>(e.Message));
            }
        }

        public async Task<Address> GetLastAddress(Currency currency, string accessToken) => await GetAddress(currency, accessToken);

        public async Task<Address> GetNewAddress(Currency currency, string accessToken) => await GetAddress(currency, accessToken, true);

        private async Task<Address> GetAddress(Currency currency, string accessToken, bool newAddress = false)
        {
            try
            {
                return JsonConvert.DeserializeObject<Address>(await String.Format((newAddress) ? NEW_ADDRESS : LAST_ADDRESS, currency.ToString()).Get(null, accessToken));
            }
            catch (HttpRequestException e)
            {
                throw new ValidationException(JsonConvert.DeserializeObject<List<ValidationError>>(e.Message));
            }
        }



    }
}
