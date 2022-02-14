using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlanetariumService.Models;

namespace PlanetariumService.Controllers
{
    public class PostersController : Controller
    {
        private PlanetariumServiceContext db = new PlanetariumServiceContext();
        private readonly ILogger<HomeController> _logger;

        public PostersController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Order(int? id)
        {
            var tickets = from e in db.Tickets
                          where e.PosterId == id
                          select e;
            return View(tickets);
        }

        public IActionResult Buy(int?[] tickets)
        {
            foreach (var ticket in tickets)
            {
                var result = db.Tickets.SingleOrDefault(t => t.Id == ticket);
                result.TicketStatus = "bought";
            }
            db.SaveChanges();
            return View();
        }
        public async Task<IActionResult> Index()
        {
            var planetariumServiceContext = db.Posters.Include(p => p.Hall).Include(p => p.Performance);
            return View(planetariumServiceContext.ToList());
        }
        public IActionResult Create()
        {
            ViewData["HallId"] = new SelectList(db.Set<Hall>(), "Id", "HallName");
            ViewData["PerformanceId"] = new SelectList(db.Set<Performance>(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateOfEvent,Price,PerformanceId,HallId")] Poster poster, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            if (ModelState.IsValid)
            {
                db.Posters.Add(poster);
                for (int i = 1; i <= (int)db.Halls.Find(poster.HallId).Capacity; i++)
                {
                    Ticket ticket = new Ticket() { Place = (byte)i, TicketStatus = "available", TierId = 1, PosterId = poster.Id };
                    db.Tickets.Add(ticket);
                }
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(AddPosters));
            }
            ViewData["HallId"] = new SelectList(db.Set<Hall>(), "Id", "Id", poster.HallId);
            ViewData["PerformanceId"] = new SelectList(db.Set<Performance>(), "Id", "Id", poster.PerformanceId);
            return View(poster);
        }
        public IActionResult AddPosters()
        {
            var posters = from e in db.Posters
                          orderby e.Id
                          select e;
            return View(posters);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poster = await db.Posters.FindAsync(id);
            if (poster == null)
            {
                return NotFound();
            }
            ViewData["HallId"] = new SelectList(db.Set<Hall>(), "Id", "HallName", poster.HallId);
            ViewData["PerformanceId"] = new SelectList(db.Set<Performance>(), "Id", "Title", poster.PerformanceId);
            return View(poster);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateOfEvent,Price,PerformanceId,HallId")] Poster poster)
        {
            if (id != poster.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var posterChanged = db.Posters.Find(poster.Id);
                    posterChanged.HallId = poster.HallId;
                    posterChanged.Price = poster.Price;
                    posterChanged.PerformanceId = poster.PerformanceId;
                    posterChanged.DateOfEvent = poster.DateOfEvent;
                    db.SaveChanges();
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PosterExists(poster.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AddPosters));
            }
            ViewData["HallId"] = new SelectList(db.Set<Hall>(), "Id", "Id", poster.HallId);
            ViewData["PerformanceId"] = new SelectList(db.Set<Performance>(), "Id", "Id", poster.PerformanceId);
            return View(poster);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Poster poster = db.Posters
                .Include(p => p.Hall)
                .Include(p => p.Performance)
                .FirstOrDefault(m => m.Id == id);
            if (poster == null)
            {
                return NotFound();
            }

            return View(poster);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var poster = await db.Posters.FindAsync(id);
            db.Posters.Remove(poster);
            db.SaveChanges();
            return RedirectToAction(nameof(AddPosters));
        }

        private bool PosterExists(int id)
        {
            return db.Posters.Any(e => e.Id == id);
        }
    }
}
