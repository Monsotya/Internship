using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlanetariumServices;
using PlanetariumService.Models;
using Microsoft.AspNetCore.Authorization;

namespace PlanetariumService.Controllers
{
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService ticketService;
        private readonly IMapper mapper;
        private readonly ILogger<TicketController> log;
        public TicketController(ITicketService ticketService, IMapper mapper, ILogger<TicketController> log)
        {
            this.log = log;
            this.ticketService = ticketService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Returns tickets of a poster by id
        /// </summary>
        [Route("Ticket/Order")]
        [HttpGet, AllowAnonymous]
        public ActionResult<List<TicketUI>> Order(int? id)
        {
            if (id == null)
            {
                log.LogError("Not found");
                throw new NullReferenceException();
            }
            try
            {
                List<TicketUI> tickets = mapper.Map<List<TicketUI>>(ticketService.GetTicketsByPoster((int)id));
                log.LogInformation("Got tickets for order");
                return tickets;
            }
            catch (Exception exception)
            {
                log.LogError(exception, "Something went wrong");
                throw;
            }            
        }

        /// <summary>
        /// Changes ticket status to "bought"
        /// </summary>
        [Route("Ticket/Buy")]
        [HttpPut, Authorize]
        public ActionResult<int> Buy([FromQuery] int[] tickets)
        {
            if (tickets.Length == 0)
            {
                log.LogError("Ticket was not found");
                return 0;
            }
            try
            {
                ticketService.BuyTickets(tickets);
                log.LogInformation("Buying tickets finished");
                return tickets.Count();
            }
            catch (Exception exception)
            {
                log.LogError(exception, "Buying tickets went wrong");
                throw;
            }            
        }
    }
}
