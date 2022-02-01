using System.Collections.Generic;

namespace PlanetariumServiceEF.Modules
{
    public class Orders
    {
        public int Id { get; set; }
        public DateTime DateOfOrder { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        public string Email { get; set; }
        
        public IList<Ticket> Tickets { get; set; }
    }
}
