using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PlanetariumService.Models
{
    public class PlanetariumServiceContext : DbContext
    {
        public PlanetariumServiceContext()
            : base("data source=.; database=Planetarium; integrated security=SSPI; MultipleActiveResultSets=true")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Poster> Posters { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Tier> Tiers { get; set; }
        public DbSet<Hall> Halls { get; set; }
                
    }
}
