using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeathersApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await GetWeathersData();
        }

        private async Task GetWeathersData()
        {
            var data = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (data != PermissionStatus.Granted)
            {
                var newdata = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }  
            var location = await Geolocation.GetLocationAsync();
            var latitude = location.Latitude;
            var longitude = location.Longitude;
         
           
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
           
           
           
            string url = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&units=metric&appid=2ba935debc178e093e33fc31bab59018";
            var response = await client.GetStringAsync(url);
            
            var weathersData = JsonConvert.DeserializeObject<WeatherData>(response);
            BindingContext = weathersData;
            
        }
    }
}
