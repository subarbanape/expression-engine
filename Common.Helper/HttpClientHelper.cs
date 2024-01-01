using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public static class HttpClientHelper
    {
        public static async Task<string> DoGet(string url, string authHeader)
        {
            HttpClient client = new HttpClient() { Timeout = new TimeSpan(0, 0, 30) };
            client.DefaultRequestHeaders.Add("Authorization", $"Basic {authHeader}");
            HttpResponseMessage result = await client.GetAsync(url).ConfigureAwait(false);
            string response = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            return response;
        }

        public static async Task<string> DoPost(string url, string authHeader, string data = null)
        {
            using (StringContent content = new StringContent(data ?? "{}", Encoding.UTF8))
            {
                HttpClient client = new HttpClient() { Timeout = new TimeSpan(0, 0, 30) };
                content.Headers.Remove("Content-Type");
                content.Headers.Add("Content-Type", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Basic {authHeader}");
                HttpResponseMessage result = await client.PostAsync(url, content).ConfigureAwait(false);
                string response = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                return response;
            }
        }
    }
}
