using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Returns a poster
        /// </summary>
        [Route("Posters/PosterDetails")]
        [HttpGet]
        public ActionResult<PosterUI> PosterDetails(int id)
        {
            PosterUI poster = mapper.Map<PosterUI>(posterService.GetById(id));
            return poster;
        }

        /// <summary>
        /// Returns posters of in a chosen time interval
        /// </summary>
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

        /// <summary>
        /// Returns all posters
        /// </summary>
        [Route("Posters/AddPosters")]
        [HttpGet]
        public ActionResult<List<PosterUI>> AddPosters()
        {
            return mapper.Map<List<PosterUI>>(posterService.GetAll());
        }

        /// <summary>
        /// Creates a poster
        /// </summary>
        [Route("Posters/Create")]
        [HttpPut]
        public ActionResult<Poster> Create([FromQuery][Bind("Id,DateOfEvent,Price,PerformanceId,HallId")] Poster poster, IPosterService posterService)
        {
            if (ModelState.IsValid)
            {
                var pos = posterService.Add(poster);
                CreateTickets(poster.Id);
                return RedirectToAction(nameof(AddPosters));
            }
            return poster;
        }

        /// <summary>
        /// Creates tickets to a poster
        /// </summary>
        [Route("Posters/CreateTickets")]
        [HttpPut]
        public void CreateTickets([FromQuery] int id)
        {
            Poster poster = posterService.GetById(id);
            for (int i = 1; i <= (int)hallService.GetById(poster.HallId).Capacity; i++)
            {
                Ticket ticket = new() { Place = (byte)i, TicketStatus = "available", TierId = 1, PosterId = poster.Id };
                ticketService.Add(ticket);
            }
        }
        /// <summary>
        /// Changes poster by id
        /// </summary>
        [Route("Posters/Edit")]
        [HttpPost]
        public ActionResult<Poster> Edit([FromQuery] int id, [Bind("Id,DateOfEvent,Price,PerformanceId,HallId")] Poster poster)
        {
            if (id != poster.Id)
            {
                return NotFound();
            }

            posterService.Update(poster);
            return poster;
        }

        /// <summary>
        /// Deletes a poster by id
        /// </summary>
        [Route("Posters/Delete")]
        [HttpDelete]
        public ActionResult<int> Delete([FromQuery] int id)
        {
            var poster = posterService.GetById(id);
            posterService.Delete(poster);
            return id;
        }

    }
}
