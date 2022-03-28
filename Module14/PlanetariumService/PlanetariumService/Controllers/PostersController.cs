using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlanetariumModels;
using PlanetariumServices;
using PlanetariumService.Models;

namespace PlanetariumService.Controllers
{
    public class PostersController : Controller
    {
        private readonly IPosterService posterService;
        private readonly IHallService hallService;
        private readonly IPerformanceService performanceService;
        private readonly ITicketService ticketService;
        private readonly IMapper mapper;
        public PostersController(IPosterService posterService, IHallService hallService,
            ITicketService ticketService, IPerformanceService performanceService, IMapper mapper)
        {
            this.posterService = posterService;
            this.hallService = hallService;
            this.performanceService = performanceService;
            this.ticketService = ticketService;
            this.mapper = mapper;
        }
        public ViewResult Posters(DateTime? dateFrom = null, DateTime? dateTo = null)
        {


            if (dateFrom == null || dateTo == null)
            {
                dateFrom = DateTime.Now;
                dateTo = DateTime.Now.AddDays(7);
            }
            List<Poster> posters = posterService.Posters((DateTime)dateFrom, (DateTime)dateTo);
            List<PosterUI> result = mapper.Map<List<PosterUI>>(posters);

            return View(result);
        }
        public IActionResult AddPosters()
        {
            return View(posterService.GetAll());
        }
        public IActionResult Create()
        {
            ViewData["HallId"] = new SelectList(hallService.GetAll(), "Id", "HallName");
            ViewData["PerformanceId"] = new SelectList(performanceService.GetAll(), "Id", "Title");
            return View();
        }

        public IPosterService GetPosterService()
        {
            return posterService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync([Bind("Id,DateOfEvent,Price,PerformanceId,HallId")] Poster poster, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            if (ModelState.IsValid)
            {
                var pos = posterService.Add(poster);
                await CreateTickets(poster);
                return RedirectToAction(nameof(AddPosters));
            }
            ViewData["HallId"] = new SelectList(hallService.GetAll(), "Id", "Id", poster.HallId);
            ViewData["PerformanceId"] = new SelectList(performanceService.GetAll(), "Id", "Id", poster.PerformanceId);
            return View(poster);
        }

        public async Task CreateTickets(Poster poster)
        {
            for (int i = 1; i <= (int)hallService.GetById(poster.HallId).Capacity; i++)
            {
                Ticket ticket = new() { Place = (byte)i, TicketStatus = "available", TierId = 1, PosterId = poster.Id };
                await ticketService.Add(ticket);
            }
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poster = posterService.GetById((int)id);
            if (poster == null)
            {
                return NotFound();
            }
            ViewData["HallId"] = new SelectList(hallService.GetAll(), "Id", "HallName", poster.HallId);
            ViewData["PerformanceId"] = new SelectList(performanceService.GetAll(), "Id", "Title", poster.PerformanceId);
            return View(poster);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,DateOfEvent,Price,PerformanceId,HallId")] Poster poster)
        {
            if (id != poster.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    posterService.Update(poster);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(AddPosters));
            }
            ViewData["HallId"] = new SelectList(hallService.GetAll(), "Id", "Id", poster.HallId);
            ViewData["PerformanceId"] = new SelectList(performanceService.GetAll(), "Id", "Id", poster.PerformanceId);
            return View(poster);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(posterService.GetById((int)id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var poster = posterService.GetById(id);
            posterService.Delete(poster);
            return RedirectToAction(nameof(AddPosters));
        }

    }
}
