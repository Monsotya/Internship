using Microsoft.EntityFrameworkCore;

namespace PlanetariumModels
{
    public class PlanetariumServiceContext : DbContext
    {
        public PlanetariumServiceContext(DbContextOptions<PlanetariumServiceContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {


            optionsBuilder.UseSqlServer("data source=.; database=Planetarium; integrated security=SSPI");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Performance>().ToTable("Performance");
            modelBuilder.Entity<Poster>().ToTable("Poster");
            modelBuilder.Entity<Ticket>().ToTable("Ticket");
            modelBuilder.Entity<Tier>().ToTable("Tier");
            modelBuilder.Entity<Orders>().ToTable("Orders");
            modelBuilder.Entity<Hall>().ToTable("Hall");
            modelBuilder.Entity<Users>().ToTable("Users");
        }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Poster> Posters { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Tier> Tiers { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Users> Users { get; set; }
                
    }
}
