using Microsoft.AspNetCore.Mvc;
using PlanetariumService.Services;

namespace PlanetariumService.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService ticketService;
        public TicketController(ITicketService ticketService)
        {
            this.ticketService = ticketService;
        }
        public IActionResult Order(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tickets = ticketService.GetTicketsByPoster((int)id);
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
