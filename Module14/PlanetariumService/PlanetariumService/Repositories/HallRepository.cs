﻿using Microsoft.EntityFrameworkCore;
using PlanetariumService.Models;

namespace PlanetariumService.Repositories
{
    public class HallRepository : Repository<Hall>, IHallRepository
    {
        public HallRepository(PlanetariumServiceContext repositoryPatternDemoContext) : base(repositoryPatternDemoContext)
        {
        }
        public Task<Hall> GetByIdAsync(int id)
        {
            return base.GetAll().FirstOrDefaultAsync(x => x.Id == id);
        }

        public List<Hall> GetAll()
        {
            return  base.GetAll().ToList<Hall>();
        }
    }
}
