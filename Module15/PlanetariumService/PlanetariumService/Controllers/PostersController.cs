using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlanetariumModels;
using PlanetariumServices;
using PlanetariumService.Models;
using Microsoft.AspNetCore.Authorization;
namespace PlanetariumService.Controllers
{
    [ApiController]
    public class PostersController : ControllerBase, IPostersController
    {
        private readonly IPosterService posterService;
        private readonly IHallService hallService;
        private readonly IPerformanceService performanceService;
        private readonly ITicketService ticketService;
        private readonly IMapper mapper;
        private readonly ILogger<PostersController> log;
        public PostersController(IPosterService posterService, IHallService hallService,
            ITicketService ticketService, IPerformanceService performanceService, IMapper mapper, ILogger<PostersController> log)
        {
            this.log = log;
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
        [HttpGet, AllowAnonymous]
        public ActionResult<PosterUI> PosterDetails(int id)
        {
            try
            {
                PosterUI poster = mapper.Map<PosterUI>(posterService.GetById(id));
                log.LogInformation("Got poster detailes by id");
                return poster;
            }
            catch (Exception exception)
            {
                log.LogError(exception, "Buying tickets went wrong");
                throw;
            }
        }

        /// <summary>
        /// Returns posters of in a chosen time interval
        /// </summary>
        [Route("Posters/Posters")]
        [HttpGet, AllowAnonymous]
        public ActionResult<List<PosterUI>> Posters(DateTime? dateFrom = null, DateTime? dateTo = null)
        {


            if (dateFrom == null || dateTo == null)
            {
                dateFrom = DateTime.Now;
                dateTo = DateTime.Now.AddDays(7);
            }

            try
            {
                List<Poster> posters = posterService.Posters((DateTime)dateFrom, (DateTime)dateTo);
                List<PosterUI> result = mapper.Map<List<PosterUI>>(posters);
                log.LogInformation("Got posters");

                return result;
            }
            catch (Exception exception)
            {
                log.LogError(exception, "Something went wrong");
                throw;
            }

        }

        /// <summary>
        /// Returns all posters
        /// </summary>
        [Route("Posters/AddPosters")]
        [HttpGet, AllowAnonymous]
        public ActionResult<List<PosterUI>> AddPosters()
        {
            try
            {
                log.LogInformation("Got all the posters");
                return mapper.Map<List<PosterUI>>(posterService.GetAll());
            }
            catch (Exception exception)
            {
                log.LogError(exception, "Something went wrong");
                throw;
            }
        }

        /// <summary>
        /// Creates a poster
        /// </summary>
        [Route("Posters/Create")]
        [HttpPost, Authorize]
        public ActionResult<Poster> Create([FromQuery][Bind("Id,DateOfEvent,Price,PerformanceId,HallId")] Poster poster)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var pos = posterService.Add(poster);
                    CreateTickets(poster.Id);
                    log.LogInformation("Poster was created");

                    return poster;
                }
                catch (Exception exception)
                {
                    log.LogError(exception, "Creating poster went wrong");
                    throw;
                }
            }
            return poster;
        }

        /// <summary>
        /// Creates tickets to a poster
        /// </summary>
        [Route("Posters/CreateTickets")]
        [HttpPost, Authorize]
        public void CreateTickets([FromQuery] int id)
        {
            try
            {
                Poster poster = posterService.GetById(id);
                for (int i = 1; i <= (int)hallService.GetById(poster.HallId).Capacity; i++)
                {
                    Ticket ticket = new() { Place = (byte)i, TicketStatus = "available", TierId = 1, PosterId = poster.Id };
                    ticketService.Add(ticket);
                }
                log.LogInformation("Tickets were created");
            }
            catch (Exception exception)
            {
                log.LogError(exception, "Something went wrong");
                throw;
            }

        }
        /// <summary>
        /// Changes poster by id
        /// </summary>
        [Route("Posters/Edit")]
        [HttpPut, Authorize]
        public ActionResult<Poster> Edit([FromQuery] int id, [Bind("Id,DateOfEvent,Price,PerformanceId,HallId")] Poster poster)
        {
            if (id != poster.Id)
            {
                log.LogError("Not found");
                return NotFound();
            }
            log.LogWarning("Risk of damaging sensative data");

            try
            {
                posterService.Update(poster);
                log.LogInformation("Poster was edited");
                return poster;
            }
            catch (Exception exception)
            {
                log.LogError(exception, "Something went wrong");
                throw;
            }
        }

        /// <summary>
        /// Deletes a poster by id
        /// </summary>
        [Route("Posters/Delete")]
        [HttpDelete, Authorize(Roles = "Admin")]
        public ActionResult<int> Delete([FromQuery] int id)
        {
            log.LogWarning("Risk of damaging sensative data");
            try
            {

                var poster = posterService.GetById(id);
                posterService.Delete(poster);
                log.LogInformation("Poster was deleted");

                return id;
            }
            catch (Exception exception)
            {
                log.LogError(exception, "Something went wrong");
                throw;
            }
        }

    }
}
