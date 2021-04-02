using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BreweryQuickType;

namespace JustBrewIt.Pages
{
    public class BreweryModel : PageModel
    {
        public object MyBreweryAPI { get; set; }

        public void OnGet()
        {

            using (var webClient = new WebClient())
            {
                string jsonString = webClient.DownloadString("https://api.openbrewerydb.org/breweries?by_city=cincinnati&brewery_type=regional");
                BreweryAPI[] welcome = BreweryAPI.FromJson(jsonString);
                ViewData["MyBreweryAPI"] = welcome;
            }
        }
    }
 
}
