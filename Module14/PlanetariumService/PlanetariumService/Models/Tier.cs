﻿namespace PlanetariumService.Models
{
    public class Tier
    {
        public int Id { get; set; }
        public string? TierName { get; set; }
        public decimal Surcharge { get; set; }
        
        public virtual IList<Ticket>? Tickets { get; set; }
    }
}
