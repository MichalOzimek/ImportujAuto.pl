using ImportCars.Data;
using ImportCars.Models;
using ImportCars.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ImportCars.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Context _context;

        public HomeController(ILogger<HomeController> logger, Context contex)
        {
            _logger = logger;
            _context = contex;
        }

        
        public IActionResult Index()
        {
            var model = new HomePageModel
            {
                Auctions = _context.Auctions.Include(x => x.Images).Where(y => y.EndDate >= DateTime.Now).OrderByDescending(z => z.EndDate).ToList(),
                Questions = _context.Questions?.ToList(),
                IsHomePage = true
            };

            return View(model);
        }


        public IActionResult AboutUs()
        {
            var model = new HomePageModel
            {
                IsHomePage = false
            };

            return View(model);
        }

        public IActionResult Offer()
        {
            var model = new HomePageModel
            {
                IsHomePage = false
            };

            return View(model);
        }

        public IActionResult ImportSteps()
        {
            var model = new HomePageModel
            {
                IsHomePage = false
            };

            return View(model);
        }        
        
        public IActionResult Contact()
        {
            var model = new HomePageModel
            {
                IsHomePage = false
            };

            return View(model);
        }

        public IActionResult Blog()
        {
            var model = new HomePageModel
            {
                IsHomePage = false
            };

            return View(model);
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