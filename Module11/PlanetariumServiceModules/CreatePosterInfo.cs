namespace PlanetariumServiceModules
{
    public class CreatePosterInfo
    {
        private int performanceId;
        private int hallId;
        private decimal price;
        public CreatePosterInfo() { }
        public CreatePosterInfo(int performanceId, int hallId, decimal price) {
            this.performanceId = performanceId;
            this.hallId = hallId;
            this.price = price;
    }
        public int PerformanceId { get => performanceId; set => performanceId = value; }
        public int HallId { get => hallId; set => hallId = value; }
        public decimal Price { get => price; set => price = value; }
    }
}
