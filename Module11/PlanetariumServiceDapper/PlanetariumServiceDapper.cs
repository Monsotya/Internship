using System;
using System.Collections.Generic;
using System.Linq;
using PlanetariumServiceModules;
using PlanetariumServiceInterface;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using System.Configuration;

namespace PlanetariumServiceDapper
{
    public class PlanetariumServiceDapper : IPlanetariumService
    {
        private string connectionString;
        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = $"data source =.; database = {value}; integrated security = SSPI"; }
        }
        public PlanetariumServiceDapper()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        public void CreatePoster(List<DateTime> dateOfEvent, CreatePosterInfo infoPoster)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (dateOfEvent.Count == 0)
                {
                    throw new Exception("Date list must not be null!");
                }

                var values = new DynamicParameters();

                values.Add("dateOfEvent", dateOfEvent[0], dbType: DbType.DateTime);
                values.Add("price", infoPoster.Price, dbType: DbType.Decimal);
                values.Add("performanceId", infoPoster.PerformanceId, dbType: DbType.Int32);
                values.Add("hallId", infoPoster.HallId, dbType: DbType.Int32);

                foreach (DateTime date in dateOfEvent)
                {
                    values.Add("dateOfEvent", date, dbType: DbType.DateTime);
                    var cmd = connection.ExecuteReader("CreatePoster", values, commandType: CommandType.StoredProcedure);
                }
            }
        }

        public void CreatePosterPerformance(List<DateTime> dateOfEvent, CreatePosterInfo infoPoster, CreatePerformanceInfo infoPerformance)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
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

                var values = new DynamicParameters();

                values.Add("dateOfEvent", dateOfEvent[0], dbType: DbType.DateTime);
                values.Add("price", infoPoster.Price, dbType: DbType.Decimal);
                values.Add("performanceId", infoPoster.PerformanceId, dbType: DbType.Int32);
                values.Add("hallId", infoPoster.HallId, dbType: DbType.Int32);

                foreach (DateTime date in dateOfEvent)
                {                    
                    values.Add("dateOfEvent", date, dbType: DbType.DateTime);
                    var cmd = connection.ExecuteReader("CreatePoster", values, commandType: CommandType.StoredProcedure);
                }
            }
        }
        public int CreatePerformance(CreatePerformanceInfo info)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var sql = "INSERT INTO Performance (Title, Duration, EventDescription) VALUES (@Title, @Duration, @EventDescription)";
                var cmd = connection.ExecuteReader("SELECT MAX(Id) AS Id FROM Performance");
                DataTable ticketId = new DataTable();

                connection.Execute(sql, new { Title = info.Title, Duration = info.Duration, EventDescription = info.EventDescription });
                ticketId.Load(cmd);
                foreach(DataRow row in ticketId.Rows)
                {
                    return (int)row["Id"];
                }
                return -1;
            }
        }
        public bool BuyTicket(int ticketId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                if (connection.Execute("BuyTicket", new { TicketId = ticketId }, commandType: CommandType.StoredProcedure) == 1)
                    return true;

                return false;
            }
        }
        public List<PerformanceInfo> GetAvailablePerformances(DateTime from, DateTime to)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var values1 = new DynamicParameters();
                DataTable performances = new DataTable();
                List<AvailablePlaces> availablePlaces = new List<AvailablePlaces>();
                List<PerformanceInfo> availablePerformances = new List<PerformanceInfo>();

                values1.Add("to", to, dbType: DbType.DateTime);
                values1.Add("from", from, dbType: DbType.DateTime);

                var cmd = connection.ExecuteReader("GetAvailablePerformances", values1, commandType: CommandType.StoredProcedure);
                performances.Load(cmd);                         

                foreach (DataRow performance in performances.Rows)
                {
                    var values2 = new DynamicParameters();
                    values2.Add("to", to, dbType: DbType.DateTime);
                    values2.Add("from", from, dbType: DbType.DateTime);
                    values2.Add("performance", performance["Title"], dbType: DbType.String);
                    availablePlaces = connection.Query<AvailablePlaces>("GetAvailablePlaces", values2, commandType: CommandType.StoredProcedure).ToList();

                    availablePerformances.Add(new PerformanceInfo((string)performance["Title"], (string)performance["EventDescription"], new List<AvailablePlaces>(availablePlaces)));
                    availablePlaces.Clear();
                }

                return availablePerformances;
            }
        }
        public List<RevokeInfo> RevokeOrders(DateTime from, DateTime to, int hallid, List<int> places)
        {
            if (places.Count == 0)
            {
                throw new Exception("Places must not be NULL!");
            }

            String placesString = String.Join(",", places);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var values = new DynamicParameters();
                DataTable orders = new DataTable();
                List<RevokeInfo> result = new List<RevokeInfo>();

                values.Add("to", to, dbType: DbType.DateTime);
                values.Add("from", from, dbType: DbType.DateTime);
                values.Add("places", placesString, dbType: DbType.String);
                values.Add("hallId", hallid, dbType: DbType.Int32);

                var cmd = connection.ExecuteReader("GetRevokedOrders", values, commandType: CommandType.StoredProcedure);
                orders.Load(cmd);

                foreach (DataRow order in orders.Rows)
                {
                    result.Add(new RevokeInfo((int)order["Id"], (string)order["Email"],
                        GetInfoMessage((string)order["ClientName"], order["Place"].ToString(), order["Price"].ToString())));
                }

                connection.Execute("RevokeOrders", values, commandType: CommandType.StoredProcedure);

                return result;
            }
        }
        public string GetInfoMessage(string ClientName, string Place, string Price)
        {
            return $"Dear client {ClientName}, we are sorry to inform that your order canceled due to maintance work on " +
                   $"ordered place number {Place}, payment in amount {Price} will be returned to your bank account soon";
        }
    }
}
