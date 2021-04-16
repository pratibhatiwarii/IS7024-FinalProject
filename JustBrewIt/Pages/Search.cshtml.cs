using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JustBrewItType;
using Microsoft.Extensions.Configuration;

namespace JustBrewIt.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public SearchModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [BindProperty]
        public string BreweryType { get; set; }
        [BindProperty]
        public string BreweryCity { get; set; }
        public bool IsSearchCity { get; set; }
        public bool IsSearchType { get; set; }
        public new string Url { get; set; }
        public bool IsSearchValid = true;
        public bool IsSearchEmpty = false;

        public void OnGet()
        {
            IsSearchCity = false;
            IsSearchType = false;
        }

        public void OnPost()
        {
            try
            {
                if (string.IsNullOrEmpty(BreweryCity) && string.IsNullOrEmpty(BreweryType))
                {
                    IsSearchEmpty = true;
                }
                else
                {
                    string city = BreweryCity;
                    string type = BreweryType;
                    var baseUri = _configuration["BreweryBaseUri"];

                    if (city == null && type != null)
                    {

                        Url = baseUri + "?by_state=ohio&by_type=" + BreweryType;
                    }
                    else if (city != null && type != null)
                    {
                        Url = baseUri + "?by_type=" + type + "&by_city=" + city;
                    }
                    else
                    {
                        Url = baseUri + "?by_city=" + city;
                    }

                    using (var webClient = new WebClient())
                    {
                        string breweryString = webClient.DownloadString(Url);
                        BreweryAPI[] breweries = JustBrewItType.BreweryAPI.FromJson(breweryString);
                        if (breweries.Length != 0)
                            ViewData["Breweries"] = breweries;
                        else
                            IsSearchValid = false;
                    }

                    IsSearchType = true;
                    IsSearchCity = true;
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Exception occured while searching the brewery");
            }
        }
    }
}

