using PlanetariumService.Models;

namespace PlanetariumService.Services
{
    public interface ITicketService
    {
        Task<Ticket> Add(Ticket ticket);
        public void BuyTickets(int[] tickets);

        public List<Ticket> GetTicketsByPoster(int id);
    }
}