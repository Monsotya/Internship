﻿using Microsoft.EntityFrameworkCore;
using PlanetariumService.Models;

namespace PlanetariumService.Repositories
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
            return GetAll().Where(x => x.PosterId == id).ToList<Ticket>();
        }
    }
}