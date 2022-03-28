using PlanetariumModels;
using PlanetariumRepositories;

namespace PlanetariumServices
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository ticketRepository;
        public TicketService(ITicketRepository ticketRepository) => this.ticketRepository = ticketRepository;
        public async Task<Ticket> Add(Ticket ticket) => await ticketRepository.AddAsync(ticket);
        public async Task BuyTickets(int[]? tickets, Orders order) => await ticketRepository.BuyTickets(tickets, order);
        public async Task BuyTicketsWithoutOrder(int[]? tickets) => await ticketRepository.BuyTicketsWithoutOrder(tickets);
        public List<Ticket> GetTicketsByPoster(int id) => ticketRepository.GetTicketsByPoster(id);
    }
}
