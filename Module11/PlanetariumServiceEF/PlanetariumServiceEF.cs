using PlanetariumServiceModules;
using PlanetariumServiceInterface;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace PlanetariumServiceEF
{

    public class PlanetariumServiceEF : IPlanetariumService
    {
        public void CreatePoster(List<DateTime> dateOfEvent, CreatePosterInfo infoPoster)
        {
            if (dateOfEvent.Count == 0)
            {
                throw new Exception("Date list must not be null!");
            }

            var context = new PlanetariumServiceContext();

            foreach (DateTime date in dateOfEvent)
            {
                context.Posters.Add(new Modules.Poster()
                {
                    DateOfEvent = date,
                    Price = infoPoster.Price,
                    PerformanceId = infoPoster.PerformanceId,
                    HallId = infoPoster.HallId
                });
            }
            context.SaveChanges();
        }
        public void CreatePosterPerformance(List<DateTime> dateOfEvent, CreatePosterInfo infoPoster, CreatePerformanceInfo infoPerformance)
        {

            if (dateOfEvent.Count == 0)
            {
                throw new Exception("Date list must not be null!");
            }
            infoPoster.PerformanceId = CreatePerformance(infoPerformance);

            if (infoPoster.PerformanceId == -1)
            {
                throw new Exception("Error creating performance");
            }

            var context = new PlanetariumServiceContext();

            foreach (DateTime date in dateOfEvent)
            {
                context.Posters.Add(new Modules.Poster()
                {
                    DateOfEvent = date,
                    Price = infoPoster.Price,
                    PerformanceId = infoPoster.PerformanceId,
                    HallId = infoPoster.HallId
                });
            }
            context.SaveChanges();
        }
        public int CreatePerformance(CreatePerformanceInfo info)
        {

            var context = new PlanetariumServiceContext();

            context.Performances.Add(new Modules.Performance()
            {
                Title = info.Title,
                EventDescription = info.EventDescription,
                Duration = info.Duration
            });

            context.SaveChanges();

            var id = context.Performances.Max(p => p.Id);
                        
            return id;

        }
        public bool BuyTicket(int ticketId)
        {
            var context = new PlanetariumServiceContext();

            var ticket = context.Tickets.FromSqlRaw($"BuyTicket {ticketId}").ToList();
            context.SaveChanges();

            if (ticket.Count == 1)
                return true;
            return false;

        }
        public List<PerformanceInfo> GetAvailablePerformances(DateTime from, DateTime to)
        {
            var context = new PlanetariumServiceContext();
            var performances = context.Posters.Where(poster => poster.DateOfEvent.CompareTo(from) >= 0 && poster.DateOfEvent.CompareTo(to) <= 0)
                .Include(poster => poster.Performance)
                .Include(poster => poster.Hall)
                .Select(poster => new
                {
                    DateOfEvent = poster.DateOfEvent,
                    HallName = poster.Hall.HallName,
                    Price = poster.Price,
                    Title = poster.Performance.Title,
                    Duration = poster.Performance.Duration,
                    EventDescription = poster.Performance.EventDescription

                })
                .Distinct();

            List<AvailablePlaces> availablePlaces = new List<AvailablePlaces>();
            List<PerformanceInfo> availablePerformances = new List<PerformanceInfo>();

            foreach (var performance in performances)
            {
                var places = context.Posters.Where(poster => poster.DateOfEvent.CompareTo(from) >= 0 && poster.DateOfEvent.CompareTo(to) <= 0)
                    .Include(poster => poster.Performance)
                    .Include(poster => poster.Hall)
                    .Include(poster => poster.Tickets)
                    .Select(poster => new
                    {
                        DateOfEvent = poster.DateOfEvent,
                        HallName = poster.Hall.HallName,
                        Price = poster.Price,
                        AvaliablePlacesNumber = poster.Tickets.Count
                    })
                    .Distinct();

                foreach (var place in places)
                {
                    availablePlaces.Add(new AvailablePlaces(place.DateOfEvent, place.Price, place.HallName, place.AvaliablePlacesNumber));
                }

                availablePerformances.Add(new PerformanceInfo(performance.Title, performance.EventDescription, new List<AvailablePlaces>(availablePlaces)));
                availablePlaces.Clear();
            }

            return availablePerformances;

        }
        public List<RevokeInfo> RevokeOrders(DateTime from, DateTime to, int hallid, List<int> places)
        {
            if (places.Count == 0)
            {
                throw new Exception("Places must not be NULL!");
            }

            String placesString = string.Join(",", places);

            List<RevokeInfo> result = new List<RevokeInfo>();

            var context = new PlanetariumServiceContext();
                        
            var orders = context.Tickets
                .Where(t => places.Contains(t.Place) && t.Poster.DateOfEvent.CompareTo(from) >= 0 && t.Poster.DateOfEvent.CompareTo(to) <= 0 && t.Poster.HallId == hallid)
                .Include(t => t.Poster)
                .Include(t => t.Order)
                .Include(t => t.Tier)
                .Select(t => new
                {
                    Id = t.OrderId,
                    Email = t.Order.Email,
                    ClientName = t.Order.ClientName,
                    Place = t.Place,
                    Price = t.Poster.Price * t.Tier.Surcharge,
                    TicketId = t.Id
                }
                );

            foreach (var order in orders)
            {
                result.Add(new RevokeInfo(order.Id, order.Email,
                    GetInfoMessage(order.ClientName, order.Place.ToString(), order.Price.ToString())));
                                
            }

            var param1 = new SqlParameter("@to", to);
            var param2 = new SqlParameter("@from", from);
            var param3 = new SqlParameter("@hallid", hallid);
            var param4 = new SqlParameter("@places", placesString);

            context.Database.ExecuteSqlRaw($"RevokeOrders @to, @from, @hallid, @places", parameters: new[] { param1, param2, param3, param4 });
            context.SaveChanges();

            return result;

        }
        public string GetInfoMessage(string ClientName, string Place, string Price)
        {
            return $"Dear client {ClientName}, we are sorry to inform that your order canceled due to maintance work on " +
                   $"ordered place number {Place}, payment in amount {Price} will be returned to your bank account soon";
        }
    }
}