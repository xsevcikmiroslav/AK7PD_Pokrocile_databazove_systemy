using BusinessLayer.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Models;
using System.Diagnostics;

namespace OnlineLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookManager _bookManager;

        public HomeController(ILogger<HomeController> logger, IBookManager bookManager)
        {
            _logger = logger;
            _bookManager = bookManager;
        }

        public IActionResult Index()
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