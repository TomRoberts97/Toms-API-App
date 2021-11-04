using System;
using Toms_API_App.Boilerplate;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Toms_API_App
{
    public partial class App : Application
    {

        public Locator Locator { get; private set; }
        public App()
        {
            InitializeComponent();

            // Create our Locator instance and tell it about the Application instance ...
            Locator = new Locator(this);

            // Ask the Locator to get us going ...
            _ = Locator.SetFirstPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
