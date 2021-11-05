using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Toms_API_App.MVVM.Models;
using Toms_API_App.Services.Rest;

namespace Toms_API_App.Services
{
    public class StocksService
    {
        private readonly IRestService _restService;
        private readonly string _apiPath;
        private readonly string _apiKey;

        public StocksService(IRestService restService, string apiPath, string apiKey)
        {
            _restService = restService;
            _apiPath = apiPath;
            _apiKey = apiKey;
        }

        // just get the data showing with the given SQL
        // then I can start passing in the dates and stocks names from the UI

        //"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol=IBM&apikey=demo"

        public async Task<(ResultStatus status, IEnumerable<Root> payload, string rawResponse)> GetStocksAsync()
        {

            return await _restService.GetAsync<IEnumerable<Root>>(_apiPath + $"?function=GLOBAL_QUOTE&symbol=IBM&apikey={_apiKey}");
            Console.WriteLine("aaa");

           // return await _restService.GetAsync<IEnumerable<StocksResponse>>(_apiPath + $"?start_date={pictureDate.ToString("yyyy-MM-dd")}&end_date={pictureDate.ToString("yyyy-MM-dd")}&api_key={_apiKey}");
        }

    }
}
