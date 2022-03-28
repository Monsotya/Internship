using System;
using System.Collections.Generic;

namespace PlanetariumModelsFramework
{
    public class Poster
    {
        public int Id { get; set; }
        public DateTime DateOfEvent { get; set; }
        public decimal Price { get; set; }

        public int PerformanceId { get; set; }
        public Performance Performance { get; set; }

        public int HallId { get; set; }
        public Hall Hall { get; set; }

        public IList<Ticket> Tickets { get; set; }

    }
}
