namespace PlanetariumServiceEF.Modules
{
    public class Ticket
    {
        public int Id { get; set; }
        public string TicketStatus { get; set; }
        public int Place { get; set; }

        public int TierId { get; set; }
        public Tier Tier { get; set; }

        public int PosterId { get; set; }
        public Poster Poster { get; set; }

        public int OrderId { get; set; }
        public Orders Order { get; set; }
    }
}
