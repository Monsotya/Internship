using System;
using System.Collections.Generic;
using PlanetariumServiceDapper;
using PlanetariumServiceModules;

namespace Planetarium_Service
{
    class Program
    {
        static void Main(string[] args)
        {
            PlanetariumServiceDapper.PlanetariumServiceDapper obj = new PlanetariumServiceDapper.PlanetariumServiceDapper();
            PlanetariumService service = new(obj);
            service.Service.BuyTicket(16);
            DateTime from = new DateTime(2022, 1, 30);
            DateTime to = new DateTime(2022, 2, 15);
            service.Service.CreatePosterPerformance(new List<DateTime> { to, from}, new CreatePosterInfo(0, 1, 100), new CreatePerformanceInfo("aa", "bbbbb", new TimeSpan(100000000000)));
            var result = service.Service.GetAvailablePerformances(from, to);
            var res = service.Service.RevokeOrders(from, to, 1, new List<int> { 1, 2, 3 });
        }
    }
}
