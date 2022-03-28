using System.Collections.Generic;

namespace PlanetariumModelsFramework
{
    public class Tier
    {
        public int Id { get; set; }
        public string TierName { get; set; }
        public decimal Surcharge { get; set; }
        
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
