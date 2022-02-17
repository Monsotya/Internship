using Microsoft.EntityFrameworkCore;

namespace PlanetariumService.Models
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

            /*modelBuilder.Entity<Poster>()
                        .HasOne(p => p.Hall)
                        .WithMany(b => b.Posters)
                        .HasForeignKey(p => p.HallId);
            modelBuilder.Entity<Poster>()
                       .HasOne(p => p.Performance)
                       .WithMany(b => b.Posters)
                       .HasForeignKey(p => p.PerformanceId);

            modelBuilder.Entity<Ticket>()
                       .HasOne(p => p.Tier)
                       .WithMany(b => b.Tickets)
                       .HasForeignKey(p => p.TierId);
            modelBuilder.Entity<Ticket>()
                       .HasOne(p => p.Order)
                       .WithMany(b => b.Tickets)
                       .HasForeignKey(p => p.OrderId);
            modelBuilder.Entity<Ticket>()
                       .HasOne(p => p.Poster)
                       .WithMany(b => b.Tickets)
                       .HasForeignKey(p => p.PosterId);*/
        }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Poster> Posters { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Tier> Tiers { get; set; }
        public DbSet<Hall> Halls { get; set; }
                
    }
}
