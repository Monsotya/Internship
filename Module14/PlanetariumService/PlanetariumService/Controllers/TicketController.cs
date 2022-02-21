using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlanetariumServices;
using PlanetariumServices.Models;

namespace PlanetariumService.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService ticketService;
        private readonly IMapper mapper;
        public TicketController(ITicketService ticketService, IMapper mapper)
        {
            this.ticketService = ticketService;
            this.mapper = mapper;
        }
        public IActionResult Order(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tickets = mapper.Map<List<TicketUI>>(ticketService.GetTicketsByPoster((int)id));
            return View(tickets);
        }

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
