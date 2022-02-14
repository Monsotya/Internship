namespace PlanetariumService.Models
{
    public class Hall
    {
        public int Id { get; set; }
        public string? HallName { get; set; }
        public byte Capacity { get; set; }        
        public virtual IList<Poster>? Posters { get; set; }
    }
}
