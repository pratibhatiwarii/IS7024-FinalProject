using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JustBrewItGoogle;
using Newtonsoft.Json.Linq;

namespace JustBrewIt.Pages
{
    public class ViewDetailsModel : PageModel
    {
        public GoogleRecords GoogleAPI { get; set; }
        public string Url { get; set; }
        public void OnGet()
        {
            using (var webClient = new WebClient())
            {
                string key = System.IO.File.ReadAllText("GoogleAPIKey.txt");
                string googleString = webClient.DownloadString("https://api.openbrewerydb.org/breweries?by_city=cincinnati&brewery_type=regional"+key);
                string fields = "&fields=business_status,formatted_address,geometry,icon,name,photos,place_id,plus_code,types,price_level,rating,user_ratings_total&key=";
                string api = "https://maps.googleapis.com/maps/api/place/findplacefromtext/json?input=";
                string input = "Mad Tree Brewing";
                string inputType = "&inputtype=textquery";
                Url = api + input + inputType + fields + key;
                string jsonString = webClient.DownloadString(Url);
                JObject jsonObject = JObject.Parse(jsonString);
                GoogleRecords google = GoogleRecords.FromJson(jsonString);
                List<Candidate> candidates = google.Candidates;
                List<Candidate> newlist = new List<Candidate>();

                foreach (var x in candidates)
                {
                    newlist.Add(x);
                }
                ViewData["Candidates"] = newlist;
            }
        }
    }
}
