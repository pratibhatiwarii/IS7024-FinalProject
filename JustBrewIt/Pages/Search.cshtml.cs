using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JustBrewItQuickType;
using Microsoft.Extensions.Configuration;
using System;

namespace JustBrewIt.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public SearchModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public object BreweryAPI { get; private set; }
        [BindProperty]
        public string BreweryType { get; set; }
        [BindProperty]
        public string BreweryCity { get; set; }
        public bool IsSearchCity { get; set; }

        public bool IsSearchType { get; set; }
        public string Url { get; set; }
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
                        string jsonString = webClient.DownloadString(Url);
                        Welcome[] welcome = Welcome.FromJson(jsonString);
                        if (welcome.Length != 0)
                            ViewData["BreweryAPI"] = welcome;
                        else
                            IsSearchValid = false;

                    }

                    IsSearchType = true;
                    IsSearchCity = true;
                }
            }
            catch(Exception ex)
            {
                //this should be handled through application's exception handling mechanism or redirect to error page
                Console.WriteLine("Exception occured while searching the brewery");
            }
        }
    }
}
