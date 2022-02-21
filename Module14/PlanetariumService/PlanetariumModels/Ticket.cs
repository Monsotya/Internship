namespace PlanetariumModels
{
    public class Ticket
    {
        public int Id { get; set; }
        public string? TicketStatus { get; set; }
        public byte Place { get; set; }

        public int TierId { get; set; }
        public virtual Tier? Tier { get; set; }

        public int PosterId { get; set; }
        public virtual Poster? Poster { get; set; }

        public int? OrderId { get; set; }
        public virtual Orders? Order { get; set; }
    }
}
