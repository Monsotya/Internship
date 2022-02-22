
namespace PlanetariumService.Models
{
    public class TierUI
    {
        public int Id { get; set; }
        public string? TierName { get; set; }
        public decimal Surcharge { get; set; } 
        
        public virtual ICollection<TicketUI>? Tickets { get; set; }
    }
}
