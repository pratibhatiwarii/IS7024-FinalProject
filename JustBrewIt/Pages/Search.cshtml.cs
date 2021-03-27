using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JustBrewItQuickType;

namespace JustBrewIt.Pages
{
    public class SearchModel : PageModel
    {
        public object BreweryAPI { get; private set; }
        [BindProperty]
        public string BreweryType { get; set; }
        [BindProperty]
        public string BreweryCity { get; set; }
        public bool isSearchCity { get; set; }

        public bool isSearchType { get; set; }
        public string Url { get; set; }

        public void OnGet()
        {
            isSearchCity = false;
            isSearchType = false;
        }
        [HttpPost]
        public void OnPost()
        {
            string city = BreweryCity;
            string type = BreweryType;

            if(BreweryCity == null && BreweryType != null) { 

            Url = "https://api.openbrewerydb.org/breweries?by_state=ohio&by_type=" + BreweryType;
           }
           else if(BreweryCity != null && BreweryType != null)
            {
                Url = "https://api.openbrewerydb.org/breweries?by_type=" + type + "&by_city" + city;
            }
            else if(BreweryCity != null && BreweryType == null)
            {
                Url = "https://api.openbrewerydb.org/breweries?by_city=" + city;
            }
            
            using (var webClient = new WebClient())
            {
                string jsonString = webClient.DownloadString(Url);
                Welcome[] welcome = Welcome.FromJson(jsonString);
                ViewData["BreweryAPI"] = welcome;
            }

            isSearchType = true;
            isSearchCity = true;
        }
    }
}
