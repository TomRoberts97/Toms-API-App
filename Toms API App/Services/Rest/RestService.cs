using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Toms_API_App.Services.Rest
{
    class RestService : IRestService
    {
        private readonly HttpClient _httpClient;
        private readonly string _host;

        public RestService(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _host = baseUrl;
        }

        public async Task<(ResultStatus status, TResponse payload, string rawResponse)> GetAsync<TResponse>(string path)
        {
            string uri = Path.Combine(_host, path);

            HttpResponseMessage response = await _httpClient.GetAsync(Sanitise(uri));

            if (response.IsSuccessStatusCode == true)
            {
                string rawData = await response.Content.ReadAsStringAsync();

                // Turn our JSON string into a csharp object (or object-graph)
                var result = JsonConvert.DeserializeObject<TResponse>(rawData);

                // Return a response of type TResult.
                return (ResultStatus.Success, result, rawData);
            }
            else
            {
                return (ResultStatus.Other, default(TResponse), null);
            }
        }

        public async Task<(ResultStatus status, TResponse payload, string rawResponse)> PostAsync<TRequest, TResponse>(TRequest request, string path)
        {
            string uri = Path.Combine(_host, path);

            string json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(Sanitise(uri), data);

            if (response.IsSuccessStatusCode == true)
            {
                string rawData = await response.Content.ReadAsStringAsync();

                // Turn our JSON string into a csharp object (or object-graph)
                TResponse result = JsonConvert.DeserializeObject<TResponse>(rawData);

                // Return a response of type TResult.
                return (ResultStatus.Success, result, rawData);

            }
            else
            {
                return (ResultStatus.Other, default(TResponse), null);
            }
        }

        private string Sanitise(string uri)
        {
            return uri.Replace('\\', '/');
        }


        public async Task SendAsync()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://hotstoks-sql-finance.p.rapidapi.com/query"),
                Headers =
                        {
                            { "x-rapidapi-host", "hotstoks-sql-finance.p.rapidapi.com" },
                            { "x-rapidapi-key", "2e582ea511msh7c0f83b4e5c7b31p1c1690jsn2352215932d2" },
                        },
                Content = new StringContent("\"SELECT * FROM stocks WHERE symbol in ('FB', 'AMZN', 'AAPL', 'NFLX', 'GOOG') ORDER BY price_change_percent_1m DESC\"")
                {
                    Headers =
                            {
                                ContentType = new MediaTypeHeaderValue("text/plain")
                            }
                }
            };

            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }
        }
    }
}
