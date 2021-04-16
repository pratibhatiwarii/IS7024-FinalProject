using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeatherAPI;

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
                    string weatherString = webClient.DownloadString("https://api.weatherbit.io/v2.0/current?city=" + city + "&key=ccda8d9e0748480f8dfebed5538fae9d");
                    //JSchema schema = JSchema.Parse(System.IO.File.ReadAllText("unesco.json"));
                    //JObject jsonObject = JObject.Parse(jsonString);
                    WeatherAPI.Weather weatherDetails = WeatherAPI.Weather.FromJson(weatherString);
                    List<Datum> cityData = weatherDetails.Data;
                    List<Datum> recordList = new List<Datum>();
                    foreach (var record in cityData)
                    {
                        recordList.Add(record);
                    }
                    ViewData["Records"] = recordList;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error");
            }
        }
        
      
    }
}
