using System;

namespace PlanetariumServiceModules
{
    public class AvailablePlaces
    {
        private DateTime performanceDate;
        private decimal priceFrom;
        private string hallName;
        private int availablePlacesNumber;
        public AvailablePlaces() { }
        public AvailablePlaces(DateTime performanceDate, decimal priceFrom, string hallName, int avaliablePlacesNumber) {
            this.performanceDate = performanceDate;
            this.priceFrom = priceFrom;
            this.hallName = hallName;
            this.availablePlacesNumber = avaliablePlacesNumber;
        }
        public DateTime PerformanceDate { get => performanceDate; set => performanceDate = value; }
        public decimal PriceFrom { get => priceFrom; set => priceFrom = value; }
        public string HallName { get => hallName; set => hallName = value; }
        public int AvailablePlacesNumber { get => availablePlacesNumber; set => availablePlacesNumber = value; }
    }
}
