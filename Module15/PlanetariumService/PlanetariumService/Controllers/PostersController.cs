using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlanetariumModels;
using PlanetariumServices;
using PlanetariumService.Models;

namespace PlanetariumService.Controllers
{
    [ApiController]
    public class PostersController : ControllerBase
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

        [Route("Posters/Posters")]
        [HttpGet]
        public ActionResult<List<PosterUI>> Posters(DateTime? dateFrom = null, DateTime? dateTo = null)
        {


            if (dateFrom == null || dateTo == null)
            {
                dateFrom = DateTime.Now;
                dateTo = DateTime.Now.AddDays(7);
            }
            List<Poster> posters = posterService.Posters((DateTime)dateFrom, (DateTime)dateTo);
            List<PosterUI> result = mapper.Map<List<PosterUI>>(posters);

            return result;
        }

        [Route("Posters/AddPosters")]
        [HttpGet]
        public ActionResult<List<PosterUI>> AddPosters()
        {
            return mapper.Map<List<PosterUI>>(posterService.GetAll());
        }

        [Route("Posters/Create")]
        [HttpPost]
        public ActionResult<Poster> Create([FromQuery][Bind("Id,DateOfEvent,Price,PerformanceId,HallId")] Poster poster, IPosterService posterService, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            if (ModelState.IsValid)
            {
                var pos = posterService.Add(poster);
                CreateTickets(poster);
                return RedirectToAction(nameof(AddPosters));
            }
            return poster;
        }

        [Route("Posters/CreateTickets")]
        [HttpPost]
        public void CreateTickets([FromQuery] Poster poster)
        {
            for (int i = 1; i <= (int)hallService.GetById(poster.HallId).Capacity; i++)
            {
                Ticket ticket = new() { Place = (byte)i, TicketStatus = "available", TierId = 1, PosterId = poster.Id };
                ticketService.Add(ticket);
            }
        }

        [Route("Posters/Edit")]
        [HttpPost]
        public ActionResult<Poster> Edit([FromQuery] int id, Poster poster)
        {
            if (id != poster.Id)
            {
                return NotFound();
            }

            posterService.Update(poster);            
            return poster;
        }

        [Route("Posters/Delete")]
        [HttpPost]
        public ActionResult<int> Delete([FromQuery] int id)
        {
            var poster = posterService.GetById(id);
            posterService.Delete(poster);
            return id;
        }

    }
}
