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
    public class SearchModel : PageModel
    {
        public object MyBreweryAPI { get; private set; }

        public void OnGet()
        { 
        }
    }
}
