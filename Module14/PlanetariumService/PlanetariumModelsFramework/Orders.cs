using System;
using System.Collections.Generic;

namespace PlanetariumModelsFramework
{
    public class Orders
    {
        public int Id { get; set; }
        public DateTime DateOfOrder { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        public string Email { get; set; }
        
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
