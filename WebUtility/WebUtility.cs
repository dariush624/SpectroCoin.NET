using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SpectroCoin.NET.WebUtility
{
    static class WebUtility
    {
        private enum HttpVerb { GET, POST }

        private static HttpClient client = new HttpClient();

        private const string MEDIA_TYPE = "application/json";

        public static async Task<string> Post(this string url, Dictionary<string, string> queryParameters, string body, string authorization = null) => await ProcessRequest(url, HttpVerb.POST, GenerateQueryString(queryParameters), body, authorization);

        public static async Task<string> Get(this string url, Dictionary<string, string> queryParameters, string authorization = null) => await ProcessRequest(url, HttpVerb.GET, GenerateQueryString(queryParameters), String.Empty, authorization);

        private static async Task<string> ProcessRequest(string url, HttpVerb verb, string query, string body, string authorization)
        {
            if (authorization != null)
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + authorization);

            StringContent content = null;
            if (body != String.Empty)
                content = new StringContent(body, Encoding.UTF8, MEDIA_TYPE);

            HttpResponseMessage result = null;
            if (verb == HttpVerb.POST)          
                result = await client.PostAsync(url + query, content);         
            else
                result = await client.GetAsync(url + query);

            if(!result.IsSuccessStatusCode)
                throw new HttpRequestException(await result.Content.ReadAsStringAsync());

            return await result.Content.ReadAsStringAsync();
        }

        private static string GenerateQueryString(Dictionary<string, string> parameters) => (parameters != null) ? "?" + string.Join("&", parameters.Select((pair) => pair.Key + "=" + pair.Value)) : String.Empty;
    }
}
