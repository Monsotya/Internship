using PlanetariumModels;

namespace PlanetariumServices
{
    public interface ITicketService
    {
        Task<Ticket> Add(Ticket ticket);
        public void BuyTickets(int[] tickets);

        public List<Ticket> GetTicketsByPoster(int id);
    }
}