using Microsoft.EntityFrameworkCore;
using PlanetariumServiceEF.Modules;

namespace PlanetariumServiceEF
{
    public class PlanetariumServiceContext : DbContext
    {
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Poster> Posters { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Tier> Tiers { get; set; }
        public DbSet<Hall> Halls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Planetarium;Trusted_Connection=True;");
        }

    }
}
