using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EVEStandard;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Eve_Intel_Manager.Models;

namespace Eve_Intel_Manager.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

       
        public IActionResult Login()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
