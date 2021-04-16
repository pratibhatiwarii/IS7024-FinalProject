using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JustBrewItGoogle;
using Newtonsoft.Json.Linq;
using static Microsoft.AspNetCore.Http.HttpRequest;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Schema;
using WeatherAPI;

namespace JustBrewIt.Pages
{
    public class ViewDetailsModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public ViewDetailsModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public GoogleRecords GoogleAPI { get; set; }
        public new string Url { get; set; }
        public bool IsSearchValid = true;
        public void OnGet()
        {
            using (var webClient = new WebClient())
            {
                var googleBaseUri = _configuration["GoogleBaseUri"];
                string input = Request.Query["input"];
                string city = Request.Query["city"];

                //Consuming the Weather API from World Heritage Group

                string weatherString = webClient.DownloadString("https://worldheritage2021.azurewebsites.net/WeatherShare?city=" + city);
                WeatherAPI.Weather weatherDetails = WeatherAPI.Weather.FromJson(weatherString);
                List<Datum> cityData = weatherDetails.Data;
                List<Datum> cityList = new List<Datum>();
                foreach (var record in cityData)
                {
                    cityList.Add(record);
                }
                ViewData["WeatherRecords"] = cityList;

                //Consuming the Google Places API
                string key = System.IO.File.ReadAllText("GoogleAPIKey.txt");
                string fields = "&fields=business_status,formatted_address,icon,name,price_level,rating,user_ratings_total&key=";
                string inputType = "&inputtype=textquery";
                Url = googleBaseUri + input + inputType + fields + key;
                string googleString = webClient.DownloadString(Url);

                //Schema Validation for the Google Places API
                JSchema googleSchema = JSchema.Parse(System.IO.File.ReadAllText("GoogleSchema.json"));
                IList<string> validationEvents = new List<string>();
                JObject googleObject = JObject.Parse(googleString);
                if (googleObject.IsValid(googleSchema, out validationEvents))
                {
                    GoogleRecords google = GoogleRecords.FromJson(googleString);
                    if(google.Candidates.Count != 0) //Code below works when the output object is not empty
                    {
                        List<Candidate> candidates = google.Candidates;
                        List<Candidate> recordList = new List<Candidate>();

                        foreach (var record in candidates)
                        {
                            recordList.Add(record);
                        }
                        ViewData["GoogleRecords"] = recordList;
                        
                    }
                    else
                    {
                        IsSearchValid = false;
                    }
                    
                }
                else
                {
                    foreach(string invalidEvent in validationEvents)
                    {
                        Console.WriteLine(invalidEvent);
                    }
                    ViewData["GoogleRecords"] = new List<Candidate>();
                    IsSearchValid = false;
                }
            }
        }
    }
}
