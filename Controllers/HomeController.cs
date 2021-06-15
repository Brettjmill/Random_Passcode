using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Random_Passcode.Models;

namespace Random_Passcode.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("PassNum") == null)
            {
                HttpContext.Session.SetInt32("PassNum", 0);
            }
            
            ViewBag.Blurb = HttpContext.Session.GetString("RandBlurb");
            ViewBag.Num = HttpContext.Session.GetInt32("PassNum");

            return View("Index");
        }

        [HttpPost("generate")]
        public IActionResult Generate()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random rand = new Random();

            string result = "";

            for(int i = 0; i < 14; i++)
            {
                int charsIdx = rand.Next(0,36);
                result += chars[charsIdx];
            }

            HttpContext.Session.SetString("RandBlurb", result);
            
            int? passNum = HttpContext.Session.GetInt32("PassNum");
            int passNum2 = passNum.Value;

            passNum2 += 1;

            HttpContext.Session.SetInt32("PassNum", passNum2);

            return RedirectToAction("Index");
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
