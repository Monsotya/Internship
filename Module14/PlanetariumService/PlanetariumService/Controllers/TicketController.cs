using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlanetariumServices;
using PlanetariumService.Models;
using PlanetariumServiceGRPC;
using Grpc.Net.Client;
using PlanetariumModels;
using System.Linq;

namespace PlanetariumService.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService ticketService;
        private readonly IOrderService orderService;
        private readonly IMapper mapper;
        public TicketController(ITicketService ticketService, IOrderService orderService, IMapper mapper)
        {
            this.ticketService = ticketService;
            this.orderService = orderService;
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

        public async Task<IActionResult> Buy(int[] tickets, string clientName, string clientSurname, string email)
        {
            if (tickets.Length == 0)
            {
                return RedirectToAction("Posters", "Posters");
            }

            var r = await Confirm(clientName, tickets);

            Orders order = orderService.Add(new Orders() { Email = email, ClientSurname = clientSurname,
                ClientName = clientName, DateOfOrder = DateTime.Now}).Result;
            ticketService.BuyTickets(tickets, order);
            return RedirectToAction("Posters", "Posters");
        }

        public async Task<int> Confirm(string name, int[] tickets)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:7130");
            var client = new Planetarium.PlanetariumClient(channel);
            var reply = await client.SendEmailAsync(
                  new EmailInfo { Name = name, Seats = String.Join(", ", tickets) });
            Console.WriteLine(reply.Message);
            return 1;
        }
    }
}
