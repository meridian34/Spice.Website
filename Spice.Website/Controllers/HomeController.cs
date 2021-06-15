using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Spice.Website.Models;
using Spice.Website.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Spice.Website.Controllers
{
    [EnableCors("AllowAllOrigin")]
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
            var httpMessage = new HttpRequestMessage();
            httpMessage.RequestUri = new Uri("https://172.16.0.245:5001/SpiceBFF");
            httpMessage.Method = HttpMethod.Get;
            

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://172.16.0.245:5001/SpiceBFF");
                var response2 = await httpClient.SendAsync(httpMessage);
                if (response != null)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<IEnumerable<Models.Spice>>(jsonString);
                    ViewBag.Spices = res;
                }
                
            }            
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
