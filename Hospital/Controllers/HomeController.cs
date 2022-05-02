using Hospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    public class HomeController : Controller
    {
       
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

     
    }
}
