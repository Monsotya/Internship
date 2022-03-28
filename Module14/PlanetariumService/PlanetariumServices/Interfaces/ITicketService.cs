using PlanetariumModels;

namespace PlanetariumServices
{
    public interface ITicketService
    {
        Task<Ticket> Add(Ticket ticket);
        public Task BuyTickets(int[] tickets, Orders order);
        public Task BuyTicketsWithoutOrder(int[]? tickets);
        public List<Ticket> GetTicketsByPoster(int id);
    }
}