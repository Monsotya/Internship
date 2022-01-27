using System;

namespace PlanetariumServiceModules
{
    public class CreatePerformanceInfo
    {
        private string title;
        private string eventDescription;
        private TimeSpan duration;
        public CreatePerformanceInfo() { }
        public CreatePerformanceInfo(string title, string eventDescription, TimeSpan duration)
        {
            this.title = title;
            this.eventDescription = eventDescription;
            this.duration = duration;
        }
    
        public string Title { get => title; set => title = value; }
        public string EventDescription { get => eventDescription; set => eventDescription = value; }
        public TimeSpan Duration { get => duration; set => duration = value; }
    }
}
