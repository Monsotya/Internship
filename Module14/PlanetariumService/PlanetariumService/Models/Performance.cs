﻿namespace PlanetariumService.Models
{
    public class Performance
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? EventDescription { get; set; }
        public TimeSpan Duration { get; set; }                
        public virtual IList<Poster>? Posters { get; set; }
    }
}
