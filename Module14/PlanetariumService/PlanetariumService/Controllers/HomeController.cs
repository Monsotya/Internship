using Microsoft.AspNetCore.Mvc;
using PlanetariumService.Models;
using System.Data.Entity;
using System.Diagnostics;

namespace PlanetariumService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private PlanetariumServiceContext db = new PlanetariumServiceContext();

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
        public ViewResult Posters(DateTime? dateFrom=null, DateTime? dateTo=null)
        {
            var posters = db.Posters.OrderBy(s => s.Id).Select(s => s);
            List<Poster> result = new List<Poster>();
            if (dateFrom == null)
            {
                dateFrom = DateTime.Now;
                dateTo = DateTime.Now.AddDays(7);
            }
            
            foreach (Poster poster in posters)
            {
                if (poster.DateOfEvent.CompareTo(dateFrom) >= 0 && poster.DateOfEvent.CompareTo(dateTo) <= 0)
                {
                    result.Add(poster);
                }
            }
            
            return View(result);
        }
        public IActionResult AddPosters()
        {
            var posters = from e in db.Posters
                          orderby e.Id
                          select e;
            return View(posters);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}