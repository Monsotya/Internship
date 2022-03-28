using PlanetariumModels;

namespace PlanetariumRepositories
{
    public interface ITicketRepository
    {
        public Task BuyTickets(int[]? tickets, Orders order);
        public Task BuyTicketsWithoutOrder(int[]? tickets);
        Task<Ticket> GetByIdAsync(int id);
        public Task<Ticket> AddAsync(Ticket ticket);
        public List<Ticket> GetTicketsByPoster(int id);
    }
}