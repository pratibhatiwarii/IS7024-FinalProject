using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JustBrewItQuickType;
using JustBrewItWeatherApi;

namespace JustBrewIt.Pages
{
    public class WeatherModel : PageModel
    { 

        public void OnGet()
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    string jsonString = webClient.DownloadString("https://api.weatherbit.io/v2.0/current?lat=39.1031&lon=84.5120&key=ccda8d9e0748480f8dfebed5538fae9d"); //Check again with team
                    Weather response= Weather.FromJson(jsonString);
                    ViewData["WeatherAPI"] = response;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error");
            }
        }
        
      
    }
}
