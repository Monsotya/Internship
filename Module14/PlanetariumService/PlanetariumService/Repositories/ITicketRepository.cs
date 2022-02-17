﻿using PlanetariumService.Models;

namespace PlanetariumService.Repositories
{
    public interface ITicketRepository
    {
        public Task BuyTickets(int[]? tickets);
        Task<Ticket> GetByIdAsync(int id);
        public Task<Ticket> AddAsync(Ticket ticket);
        public List<Ticket> GetTicketsByPoster(int id);
    }
}