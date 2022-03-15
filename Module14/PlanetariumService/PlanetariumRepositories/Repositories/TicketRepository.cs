using Microsoft.EntityFrameworkCore;
using PlanetariumModels;

namespace PlanetariumRepositories
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(PlanetariumServiceContext repositoryPatternDemoContext) : base(repositoryPatternDemoContext)
        {
        }

        public Task<Ticket> GetByIdAsync(int id) => GetAll().FirstOrDefaultAsync(x => x.Id == id);
        public async Task BuyTickets(int[]? tickets)
        {
            foreach (int ticket in tickets)
            {
                var result = GetByIdAsync(ticket).Result;
                result.TicketStatus = "bought";
                await UpdateAsync(result);
            }
        }

        public List<Ticket> GetTicketsByPoster(int id)
        {
            return GetAll().Where(x => x.PosterId == id).Include(x => x.Poster).Include(x => x.Poster.Performance).ToList<Ticket>();
        }
    }
}
