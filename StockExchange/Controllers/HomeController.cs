using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using StockExchange.Models;
using System.Diagnostics;

namespace StockExchange.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HubConnection _hubConnection;
        public HomeController(ILogger<HomeController> logger)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7160/chathub")
                .Build();

            _hubConnection.On<int[]>("UpdateStock", arr => 
            {
                ViewBag.DataArr=arr;
                View("Index");
            });
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            ViewBag.DataArr = new int[] { 300, 320, 330, 300, 300, 300 };
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