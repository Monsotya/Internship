namespace PlanetariumService.Models
{
    public class PerformanceUI
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? EventDescription { get; set; }
        public TimeSpan Duration { get; set; }                
        public virtual ICollection<PosterUI>? Posters { get; set; }
    }
}
