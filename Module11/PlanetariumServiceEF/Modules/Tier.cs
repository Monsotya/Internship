using System.Collections.Generic;

namespace PlanetariumServiceEF.Modules
{
    public class Tier
    {
        public int Id { get; set; }
        public string TierName { get; set; }
        public decimal Surcharge { get; set; }
        
        public IList<Ticket> Tickets { get; set; }
    }
}
