using System;
using System.Collections.Generic;

namespace Planetarium_Service
{
    class Program
    {
        static void Main(string[] args)
        {
            PlanetariumService obj = new PlanetariumService();
            obj.ConnectionString = "Planetarium";
            obj.BuyTicket(16);
            DateTime from = new DateTime(2022, 1, 30);
            DateTime to = new DateTime(2022, 2, 15);
            obj.CreatePoster(to, 120F, 3, 3);
            var result = obj.GetAvailablePerformances(from, to);
            var res = obj.RevokeOrders(from, to, 1, new List<int> { 1, 2, 3 });
        }
    }
}
