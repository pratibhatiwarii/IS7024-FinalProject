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
       // public string Name = { get; set;  }
        public void OnGet()
        {
            using (var webClient = new WebClient())
            {
                var googleBaseUri = _configuration["GoogleBaseUri"];
                string input = Request.Query["input"];
                string key = System.IO.File.ReadAllText("GoogleAPIKey.txt");
                string fields = "&fields=business_status,formatted_address,geometry,icon,name,photos,place_id,plus_code,types,price_level,rating,user_ratings_total&key=";
                // string input = "Mad Tree Brewing";
                string inputType = "&inputtype=textquery";
                Url = googleBaseUri + input + inputType + fields + key;
                string googleString = webClient.DownloadString(Url);
                JSchema googleSchema = JSchema.Parse(System.IO.File.ReadAllText("GoogleSchema.json"));
                IList<string> validationEvents = new List<string>();
                JObject googleObject = JObject.Parse(googleString);
                if (googleObject.IsValid(googleSchema, out validationEvents))
                {
                    GoogleRecords google = GoogleRecords.FromJson(googleString);
                    List<Candidate> candidates = google.Candidates;
                    List<Candidate> recordList = new List<Candidate>();

                    foreach (var record in candidates)
                    {
                        recordList.Add(record);
                    }
                    ViewData["Records"] = recordList;
                }
                else
                {
                    foreach(string invalidEvent in validationEvents)
                    {
                        Console.WriteLine(invalidEvent);
                    }
                    ViewData["Records"] = new List<Candidate>();
                }
            }
        }
    }
}
