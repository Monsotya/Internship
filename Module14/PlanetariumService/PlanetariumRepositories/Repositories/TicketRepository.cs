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
        public async Task BuyTickets(int[]? tickets, Orders order)
        {
            foreach (int ticket in tickets)
            {
                var result = GetByIdAsync(ticket).Result;
                result.TicketStatus = "bought";
                result.OrderId = order.Id;
                result.Order = order;
                await UpdateAsync(result);
            }
        }

        public async Task BuyTicketsWithoutOrder(int[]? ids)
        {
            List<Ticket> tickets = GetAll().ToList<Ticket>();
            var k = GetAll();
            foreach (var ticket in k)
            {
                if (ids.Contains(ticket.Id))
                {
                    ticket.TicketStatus = "bought";
                }
            }
            RepositoryPlanetarium.Update(k);
            object p = await RepositoryPlanetarium.SaveChangesAsync();
        }

        public List<Ticket> GetTicketsByPoster(int id)
        {
            return GetAll().Where(x => x.PosterId == id).Include(x => x.Poster).Include(x => x.Poster.Performance).ToList<Ticket>();
        }
    }
}
