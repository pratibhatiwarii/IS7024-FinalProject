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
        public bool isSearchCity = false;

        public bool isSearchType = false;
        public string Url { get; set; }
        public string Error;
        
        public void OnPost()
        {
            string city = BreweryCity;
            string type = BreweryType;
            Url = "https://api.openbrewerydb.org/breweries?by_city=" + city;
            if (String.IsNullOrEmpty(city) && !String.IsNullOrEmpty(type)) {
                Url = "https://api.openbrewerydb.org/breweries?by_state=ohio&by_type=" + BreweryType;
            }
            else if(!String.IsNullOrEmpty(city) && !String.IsNullOrEmpty(city))
            {
                Url = "https://api.openbrewerydb.org/breweries?by_type=" + type + "&by_city=" + city;
            }
            
            using (var webClient = new WebClient())
            {
                try
                {
                    string breweriesResponseString = webClient.DownloadString(Url);
                    Welcome[] welcome = Welcome.FromJson(breweriesResponseString);
                    ViewData["BreweryAPI"] = welcome;
                } 
                catch (Exception ex)
                {
                    Error = "Something went wrong! Unable to retrieve list of breweries.";
                    Console.WriteLine(ex.Message);
                }
            }

            isSearchType = isSearchCity = true;
        }
    }
}
