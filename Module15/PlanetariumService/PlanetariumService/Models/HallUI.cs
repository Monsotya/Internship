namespace PlanetariumService.Models
{
    public class HallUI
    {
        public int Id { get; set; }
        public string? HallName { get; set; }
        public byte Capacity { get; set; }
        public IList<PosterUI>? Posters { get; set; }
    }
}
