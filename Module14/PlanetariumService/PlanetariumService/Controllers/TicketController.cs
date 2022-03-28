using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlanetariumServices;
using PlanetariumService.Models;
using PlanetariumServiceGRPC;
using Grpc.Net.Client;
using PlanetariumModels;
using PlanetariumService.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNet.SignalR;
using Microsoft.VisualStudio.OLE.Interop;
using ServiceReference;

namespace PlanetariumService.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService ticketService;
        private readonly IOrderService orderService;
        private readonly IMapper mapper;
        //private IHubContext<BuyingTicketsHub> HubContext
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

        public async Task<IActionResult> Buy(int[] tickets, string clientName, string clientSurname, string email, int posterId)
        {
            if (tickets.Length == 0)
            {
                //await this.HubContext.Clients.All.InvokeAsync("Completed", posterId);
                /*var hubContext = GlobalHost.ConnectionManager.GetHubContext<BuyingTicketsHub>();
                hubContext.Clients.All.foo(MSG);*/
                await BuyTickets(2, posterId);
                return RedirectToAction("Posters", "Posters");
            }

            //var r = await Confirm(clientName, tickets);
            PlanetariumServiceClient client = new PlanetariumServiceClient();
            var result = await client.SendEmailAsync(clientName, String.Join(", ", tickets));
            var result2 = await client.BuyTicketsAsync(tickets[0]);

            Orders order = orderService.Add(new Orders() { Email = email, ClientSurname = clientSurname,
                ClientName = clientName, DateOfOrder = DateTime.Now}).Result;
            //var tickets1 = mapper.Map<List<TicketUI>>(ticketService.GetTicketsByPoster(2));

            await ticketService.BuyTickets(tickets, order);
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

        public async Task BuyTickets(int quantity, int posterId)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:7130");
            var client = new Planetarium.PlanetariumClient(channel);
            var reply = await client.BuyTicketsAsync(
                  new TicketsInfo { Quantity = quantity, PosterId = posterId });
            //Console.WriteLine(reply.Message);
        }
    }
}
