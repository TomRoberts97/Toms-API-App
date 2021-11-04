using FunctionZero.CommandZero;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Toms_API_App.MVVM.ViewModels;
using Toms_API_App.Services;

namespace Toms_API_App.MVVM.PageViewModels
{

    // Uncle Bob is watching
    // Try unit testing
    // think about SOLID when writing 
    // make sure code it clean
    // take your time , understand everything


    public class HomePageVm : BaseVm
    {

        private readonly StocksService _stocksService;

        public ICommand ApiCommand { get; }
        public HomePageVm(StocksService stocksService)
        {
            _stocksService = stocksService;

            ApiCommand = new CommandBuilder().AddGuard(this).SetExecuteAsync(ApiCommandExecute).Build();
        }

        private async Task ApiCommandExecute()
        {
            await _stocksService.SendAsync();
        }
    }
}
