using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Spice.Website.Models;
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
        
        public HomeController(ILogger<HomeController> logger)
        {
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

            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    var response = await httpClient.SendAsync(httpMessage);
                    if (response != null)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var res = JsonConvert.DeserializeObject<IEnumerable<Models.Spice>>(jsonString);
                        ViewBag.Spices = res;
                    }

                }
            }

            return View();
        }
        public IActionResult AddSpice()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSpice(string name, decimal price, double weight)
        {
            var s = new Spice.Website.Models.Spice();
            s.Name = name;
            s.Price = price;
            s.Weight = weight;


            var httpMessage = new HttpRequestMessage();
            httpMessage.RequestUri = new Uri("https://172.16.0.245:5001/SpiceBFF");
            httpMessage.Method = HttpMethod.Post;
            httpMessage.Content = new StringContent(JsonConvert.SerializeObject(s), System.Text.Encoding.UTF8, "application/json");

            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    var response = await httpClient.SendAsync(httpMessage);
                }
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
