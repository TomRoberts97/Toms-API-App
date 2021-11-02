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

        public async Task<(ResultStatus status, IEnumerable<StocksResponse> payload, string rawResponse)> GetApodAsync(DateTime pictureDate)
        {

            return await _restService.GetAsync<IEnumerable<StocksResponse>>(_apiPath + $"?start_date={pictureDate.ToString("yyyy-MM-dd")}&end_date={pictureDate.ToString("yyyy-MM-dd")}&api_key={_apiKey}");
        }
    }
}
