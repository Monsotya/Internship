using System.ComponentModel.DataAnnotations.Schema;

namespace PlanetariumService.Models
{
    public class PosterUI
    {
        public int Id { get; set; }
        public DateTime DateOfEvent { get; set; }
        public decimal Price { get; set; }

        public int PerformanceId { get; set; }
        public PerformanceUI? Performance { get; set; }

        public int HallId { get; set; }
        public HallUI? Hall { get; set; }

        public IList<TicketUI>? Tickets { get; set; }

    }
}
