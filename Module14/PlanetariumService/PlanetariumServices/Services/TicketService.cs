using PlanetariumModels;
using PlanetariumRepositories;

namespace PlanetariumServices
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository ticketRepository;
        public TicketService(ITicketRepository ticketRepository) => this.ticketRepository = ticketRepository;
        public async Task<Ticket> Add(Ticket ticket) => await ticketRepository.AddAsync(ticket);
        public void BuyTickets(int[]? tickets) => ticketRepository.BuyTickets(tickets);

        public List<Ticket> GetTicketsByPoster(int id) => ticketRepository.GetTicketsByPoster(id);
    }
}
