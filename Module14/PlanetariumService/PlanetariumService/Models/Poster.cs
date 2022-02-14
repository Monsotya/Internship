namespace PlanetariumService.Models
{
    public class Poster
    {
        public int Id { get; set; }
        public DateTime DateOfEvent { get; set; }
        public decimal Price { get; set; }

        public int PerformanceId { get; set; }
        public virtual Performance? Performance { get; set; }

        public int HallId { get; set; }
        public virtual Hall? Hall { get; set; }

        public virtual IList<Ticket>? Tickets { get; set; }

    }
}
