using System;
using System.Collections.Generic;
using PlanetariumServiceModules;

namespace PlanetariumServiceInterface
{
    public interface IPlanetariumService
    {
        bool BuyTicket(int ticketId);
        int CreatePerformance(CreatePerformanceInfo info);
        void CreatePoster(List<DateTime> dateOfEvent, CreatePosterInfo infoPoster);
        void CreatePosterPerformance(List<DateTime> dateOfEvent, CreatePosterInfo infoPoster, CreatePerformanceInfo infoPerformance);
        List<PerformanceInfo> GetAvailablePerformances(DateTime from, DateTime to);
        string GetInfoMessage(string ClientName, string Place, string Price);
        List<RevokeInfo> RevokeOrders(DateTime from, DateTime to, int hallid, List<int> places);
    }
}