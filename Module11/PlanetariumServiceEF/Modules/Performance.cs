using System.Collections.Generic;
using System;

namespace PlanetariumServiceEF.Modules
{
    public class Performance
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string EventDescription { get; set; }
        public TimeSpan Duration { get; set; }
                
        public IList<Poster> Posters { get; set; }
    }
}
