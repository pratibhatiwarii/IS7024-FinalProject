using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuickType;

namespace JustBrewIt.Pages
{
    public class BreweryModel : PageModel
    {
        public object MyBreweryAPI { get; set; }

        public string Error;
        public void OnGet()
        {
            using (var webClient = new WebClient())
            {
                try
                {
                    string breweryResponseString = webClient.DownloadString("https://api.openbrewerydb.org/breweries?by_city=cincinnati&brewery_type=regional");
                    Welcome[] welcome = Welcome.FromJson(breweryResponseString);
                    ViewData["MyBreweryAPI"] = welcome;
                }
                catch (Exception ex)
                {
                    Error = "Something went wrong! Unable to retrieve breweries in Cincinnati.";
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
 
}
