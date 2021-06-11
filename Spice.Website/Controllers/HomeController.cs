using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Spice.Website.Models;
using Spice.Website.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISpiceService _serv;

        public HomeController(ILogger<HomeController> logger, ISpiceService serv)
        {
            _serv = serv;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Spices()
        {
            ViewBag.Spices = (await _serv.GetSpicesAsync());
            return View();
        }
        public IActionResult AddSpice()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSpice(string name, decimal price, double weight)
        {
            var s = new Spice.Website.Models.Spice();
            s.Name = name;
            s.Price = price;
            s.Weight = weight;
            _serv.AddSpiceAsync(s);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
