using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace App1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
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
