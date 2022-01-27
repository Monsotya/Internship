using System.Collections.Generic;

namespace PlanetariumServiceModules
{
    public class PerformanceInfo
    {
        private string performanceName;
        private string performanceDetail;
        private List<AvailablePlaces> availablePlaces;
        public PerformanceInfo() { }
        public PerformanceInfo(string performanceName, string performanceDetail, List<AvailablePlaces> availablePlaces) {
            this.performanceName = performanceName;
            this.performanceDetail = performanceDetail;
            this.availablePlaces = availablePlaces;
    }
        public string PerformanceName { get => performanceName; set => performanceName = value; }
        public string PerformanceDetail { get => performanceDetail; set => performanceDetail = value; }
        internal List<AvailablePlaces> AvailablePlaces { get => availablePlaces; set => availablePlaces = value; }
    }
}
