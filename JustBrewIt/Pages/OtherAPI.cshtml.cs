using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;



namespace JustBrewIt.Pages
{
    public class OtherAPIModel : PageModel
    {
        public JsonResult OnGet()
        {



            string url = "https://api.openbrewerydb.org/breweries";

            string breweryDetails = getData(url);



            JustBrewItType.BreweryAPI[] array = JustBrewItType.BreweryAPI.FromJson(breweryDetails);



            return new JsonResult(array);
        }



        private string getData(string url)
        {
            using (WebClient webClient = new WebClient())
            {
                return webClient.DownloadString(url);
            }
        }
    }
}