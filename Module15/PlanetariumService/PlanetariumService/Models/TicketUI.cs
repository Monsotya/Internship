namespace PlanetariumService.Models
{
    public class TicketUI
    {
        public int Id { get; set; }
        public string? TicketStatus { get; set; }
        public byte Place { get; set; }

        public int TierId { get; set; }
        public virtual TierUI? Tier { get; set; }

        public int PosterId { get; set; }
        public virtual PosterUI? Poster { get; set; }

        public int? OrderId { get; set; }
        public virtual OrdersUI? Order { get; set; }
    }
}
