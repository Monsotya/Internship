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
        public TicketController(ITicketService ticketService, IMapper mapper)
        {
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
                return NotFound();
            }
            List<TicketUI> tickets = mapper.Map<List<TicketUI>>(ticketService.GetTicketsByPoster((int)id));
            return tickets;
        }

        /// <summary>
        /// Changes ticket status to "bought"
        /// </summary>
        [Route("Ticket/Buy")]
        [HttpPut, Authorize]
        public ActionResult<int> Buy([FromQuery] int[]? tickets)
        {
            if (tickets.Length == 0)
            {
                return 0;
            }
            ticketService.BuyTickets(tickets);
            return tickets.Count();
        }
    }
}
