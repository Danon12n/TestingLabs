using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pet_shop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using System.Data.Common;
using MySql.Data.MySqlClient;
using pet_shop.DB;
using pet_shop.MySQLRepository;

using static pet_shop.Globals;

namespace pet_shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //Console.WriteLine("Я в Index Homecontroller!");

            if (Globals.error_code == 0)
                ViewData["Message"] = "Everything all right!";
            if (Globals.error_code == 1)
                ViewData["Message"] = "You are not authorized!";
            if (Globals.error_code == 2)
                ViewData["Message"] = "Not enough rights to access!";
            Globals.error_code = 0;

            return View();
        }

        public IActionResult NotAuth()
        {
            return RedirectToAction("Index");
        }

        public IActionResult show_Registration()
        {
            return View("Registration");
        }

        public IActionResult show_SignIn()
        {
            return View("SignIn");
        }
        public IActionResult Registration()
        {
            
            return View();
        }

        public IActionResult Privacy()
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
