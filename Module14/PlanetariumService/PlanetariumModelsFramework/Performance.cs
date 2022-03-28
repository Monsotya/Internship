using System;
using System.Collections.Generic;

namespace PlanetariumModelsFramework
{
    public class Performance
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string EventDescription { get; set; }
        public TimeSpan Duration { get; set; }                
        public virtual ICollection<Poster> Posters { get; set; }
    }
}
