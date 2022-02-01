using System.Collections.Generic;

namespace PlanetariumServiceEF.Modules
{
    public class Hall
    {
        public int Id { get; set; }
        public string HallName { get; set; }
        public int Capacity { get; set; }
        
        public IList<Poster> Posters { get; set; }
    }
}
