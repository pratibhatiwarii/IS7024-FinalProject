using System;
using System.Net;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JustBrewIt.Pages
{
    public class WeatherModel : PageModel
    {
        private string city;

        public void OnGet()
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    string city = HttpContext.Request.Query["city"];
                    double t = 0;
                  
                    

                    string weatherString = webClient.DownloadString("https://api.weatherbit.io/v2.0/current?city=" + city + "&key=ccda8d9e0748480f8dfebed5538fae9d");
                    //JSchema schema = JSchema.Parse(System.IO.File.ReadAllText("unesco.json"));
                    //JObject jsonObject = JObject.Parse(jsonString);
                    WeatherAPI.Weather weatherDetails = WeatherAPI.Weather.FromJson(weatherString);

                    t = weatherDetails.Data[0].Temp;
                    ViewData["WeatherAPI"] = t;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error");
            }
        }
        
      
    }
}
