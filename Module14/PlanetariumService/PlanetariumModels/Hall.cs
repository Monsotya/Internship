namespace PlanetariumModels
{
    public class Hall
    {
        public int Id { get; set; }
        public string? HallName { get; set; }
        public byte Capacity { get; set; }
        public IList<Poster>? Posters { get; set; }
    }
}
