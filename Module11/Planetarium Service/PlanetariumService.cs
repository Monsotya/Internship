using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using Dapper;

namespace Planetarium_Service
{
    class PlanetariumService
    {
        private string connectionString;
        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = $"data source =.; database = {value}; integrated security = SSPI";  }
        }

        public PlanetariumService()
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

                connection.Open();

                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "CreatePoster",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@dateOfEvent", dateOfEvent[0]);
                cmd.Parameters.AddWithValue("@price", infoPoster.Price);
                cmd.Parameters.AddWithValue("@performanceId", infoPoster.PerformanceId);
                cmd.Parameters.AddWithValue("@hallId", infoPoster.HallId);

                foreach(DateTime date in dateOfEvent)
                {
                    cmd.Parameters.Remove(cmd.Parameters["@dateOfEvent"]);
                    cmd.Parameters.AddWithValue("@dateOfEvent", date);
                    cmd.ExecuteNonQuery();
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

                connection.Open();

                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "CreatePoster",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@dateOfEvent", dateOfEvent[0]);
                cmd.Parameters.AddWithValue("@price", infoPoster.Price);
                cmd.Parameters.AddWithValue("@performanceId", infoPoster.PerformanceId);
                cmd.Parameters.AddWithValue("@hallId", infoPoster.HallId);

                foreach (DateTime date in dateOfEvent)
                {
                    cmd.Parameters.Remove(cmd.Parameters["@dateOfEvent"]);
                    cmd.Parameters.AddWithValue("@dateOfEvent", date);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public int CreatePerformance(CreatePerformanceInfo info)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var sql = "INSERT INTO Performance (Title, Duration, EventDescription) VALUES (@Title, @Duration, @EventDescription)";
                connection.Execute(sql, new { Title = info.Title, Duration = info.Duration, EventDescription = info.EventDescription });
                SqlCommand cmd = new SqlCommand("SELECT MAX(Id) AS Id FROM Performance", connection);
                connection.Open();
                SqlDataReader sd = cmd.ExecuteReader();
                while (sd.Read())
                {
                    return (int)sd["Id"];
                }
                return -1;
            }
        }
        public bool BuyTicket(int ticketId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "BuyTicket",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@TicketId", ticketId);
                if (cmd.ExecuteNonQuery() == 1)
                    return true;
                return false;

                /*SqlCommand cm = new SqlCommand($"select * from ticket where id = @TicketId", connection);
                cmd.Parameters.AddWithValue("@TicketId", ticketId);
                SqlDataReader sdr = cm.ExecuteReader();
                while (sdr.Read()) {
                    if((string)sdr["TicketStatus"] == "available")
                    {
                        sdr.Close();
                        SqlCommand cmd = new SqlCommand($"update ticket set ticketstatus = 'bought' where id = @ticketId", connection);
                        cmd.Parameters.AddWithValue("@TicketId", ticketId);
                        SqlDataReader sd = cmd.ExecuteReader();
                        return true;
                    }
                }
                return false;*/
            }
        }
        public List<PerformanceInfo> GetAvailablePerformances(DateTime from, DateTime to) 
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter("GetAvailablePerformances", connection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.AddWithValue("@from", from);
                da.SelectCommand.Parameters.AddWithValue("@to", to);
                DataTable performances = new DataTable();
                da.Fill(performances);

                SqlDataAdapter sda = new SqlDataAdapter("GetAvailablePlaces", connection);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;

                sda.SelectCommand.Parameters.AddWithValue("@from", from);
                sda.SelectCommand.Parameters.AddWithValue("@to", to);
                DataTable places = new DataTable();
                List<AvailablePlaces> availablePlaces = new List<AvailablePlaces>();
                List<PerformanceInfo> availablePerformances = new List<PerformanceInfo>();
                sda.SelectCommand.Parameters.AddWithValue("@performance", "");

                foreach (DataRow performance in performances.Rows)
                {
                    sda.SelectCommand.Parameters.Remove(sda.SelectCommand.Parameters["@performance"]);
                    sda.SelectCommand.Parameters.AddWithValue("@performance", performance["Title"]);
                    
                    sda.Fill(places);
                    var values = new DynamicParameters();
                    values.Add("to", to, dbType: DbType.DateTime);
                    values.Add("from", from, dbType: DbType.DateTime);
                    values.Add("performance", performance["Title"], dbType: DbType.String);
                    availablePlaces = connection.Query<AvailablePlaces>("GetAvailablePlaces", values, commandType: CommandType.StoredProcedure).ToList();

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

            String placesString = String.Join(',', places);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlDataAdapter da = new SqlDataAdapter("GetRevokedOrders", connection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                List<RevokeInfo> result = new List<RevokeInfo>();

                da.SelectCommand.Parameters.AddWithValue("@from", from);
                da.SelectCommand.Parameters.AddWithValue("@to", to);
                da.SelectCommand.Parameters.AddWithValue("@places", placesString);
                da.SelectCommand.Parameters.AddWithValue("@hallId", hallid);
                DataTable orders = new DataTable();

                da.Fill(orders);
                foreach (DataRow order in orders.Rows)
                {
                    result.Add(new RevokeInfo((int)order["Id"], (string)order["Email"], 
                        GetInfoMessage((string)order["ClientName"], order["Place"].ToString(), order["Price"].ToString())));
                }

                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "RevokeOrders",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);
                cmd.Parameters.AddWithValue("@places", placesString);
                cmd.Parameters.AddWithValue("@hallId", hallid);

                cmd.ExecuteNonQuery();

                return result;

                /*connection.Open();

                List<RevokeInfo> result =  new List<RevokeInfo>();
                SqlDataAdapter sda = new SqlDataAdapter($"SELECT Place, Price * Surcharge as Price, [Orders].Id, ClientName, " +
                    $"Email FROM Ticket INNER JOIN Orders ON([Ticket].OrderId = [Orders].Id) INNER JOIN Poster ON([Ticket].PosterId " +
                    $"= [Poster].Id) INNER JOIN Tier ON([Ticket].TierId = [Tier].Id) WHERE[Ticket].place in (SELECT CAST(value AS int) FROM STRING_SPLIT(@places, ',')) AND (SELECT HallId " +
                    $"FROM Poster WHERE Id = PosterId) = @hallId AND (SELECT DateOfEvent FROM Poster WHERE Id = PosterId) " +
                    $"BETWEEN @from AND @to", connection);

                sda.SelectCommand.Parameters.AddWithValue("@from", from);
                sda.SelectCommand.Parameters.AddWithValue("@to", to);
                sda.SelectCommand.Parameters.AddWithValue("@places", placesString);
                sda.SelectCommand.Parameters.AddWithValue("@hallId", hallid);

                DataTable orders = new DataTable();
     
                sda.Fill(orders);
                foreach (DataRow order in orders.Rows)
                {
                    result.Add(new RevokeInfo((int)order["Id"], (string)order["Email"], 
                        GetInfoMessage((string)order["ClientName"], order["Place"].ToString(), (string)order["Price"].ToString())));
                }
                SqlCommand cm = new SqlCommand($"UPDATE Ticket SET TicketStatus = 'unavailable', OrderId = NULL WHERE " +
                    $"[Ticket].place in (SELECT CAST(value AS int) FROM STRING_SPLIT(@places, ',')) AND (SELECT HallId FROM Poster WHERE Id = PosterId) = @hallId " +
                    $"AND (SELECT DateOfEvent FROM Poster WHERE Id = PosterId) BETWEEN @from " +
                    $"AND @to", connection);


                cm.Parameters.AddWithValue("@from", from);
                cm.Parameters.AddWithValue("@to", to);
                cm.Parameters.AddWithValue("@places", placesString);
                cm.Parameters.AddWithValue("@hallId", hallid);

                SqlDataReader sdr = cm.ExecuteReader();
                sdr.Close();
                return result;*/
            }
        }        
        public string GetInfoMessage(string ClientName, string Place, string Price)
        {
            return $"Dear client {ClientName}, we are sorry to inform that your order canceled due to maintance work on " +
                   $"ordered place number {Place}, payment in amount {Price} will be returned to your bank account soon";
        }
    }
}
