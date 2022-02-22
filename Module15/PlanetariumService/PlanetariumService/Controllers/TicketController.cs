using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlanetariumServices;
using PlanetariumService.Models;

namespace PlanetariumService.Controllers
{
    [ApiController]
    public class TicketController : Controller
    {
        private readonly ITicketService ticketService;
        private readonly IMapper mapper;
        public TicketController(ITicketService ticketService, IMapper mapper)
        {
            this.ticketService = ticketService;
            this.mapper = mapper;
        }
        [Route("Ticket/Order")]
        [HttpGet]
        public IActionResult Order(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tickets = mapper.Map<List<TicketUI>>(ticketService.GetTicketsByPoster((int)id));
            return View(tickets);
        }        
        [Route("Ticket/Buy")]
        [HttpGet]
        public IActionResult Buy(int[]? tickets)
        {
            if (tickets.Length == 0)
            {
                return RedirectToAction("Posters", "Posters");
            }
            ticketService.BuyTickets(tickets);
            return View();
        }
    }
}
