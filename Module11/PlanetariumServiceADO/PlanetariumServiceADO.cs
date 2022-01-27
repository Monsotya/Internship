using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using PlanetariumServiceModules;
using PlanetariumServiceInterface;

namespace PlanetariumServiceADO
{
    public class PlanetariumServiceADO : IPlanetariumService
    {
            private string connectionString;
            public string ConnectionString
            {
                get { return connectionString; }
                set { connectionString = $"data source =.; database = {value}; integrated security = SSPI"; }
            }

            public PlanetariumServiceADO()
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

                    foreach (DateTime date in dateOfEvent)
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
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO Performance (Title, Duration, EventDescription) VALUES (@Title, @Duration, @EventDescription)", connection);

                    cmd.Parameters.AddWithValue("@Title", info.Title);
                    cmd.Parameters.AddWithValue("@Duration", info.Duration);
                    cmd.Parameters.AddWithValue("@EventDescription", info.EventDescription);

                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SELECT MAX(Id) AS Id FROM Performance", connection);

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

                        foreach (DataRow row in places.Rows)
                        {
                            availablePlaces.Add(new AvailablePlaces((DateTime)row["DateOfEvent"], (decimal)row["Price"], (string)row["HallName"], (int)row["AvaliablePlacesNumber"]));
                        }

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

                String placesString = string.Join(",", places);

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
                }
            }
            public string GetInfoMessage(string ClientName, string Place, string Price)
            {
                return $"Dear client {ClientName}, we are sorry to inform that your order canceled due to maintance work on " +
                       $"ordered place number {Place}, payment in amount {Price} will be returned to your bank account soon";
            }
    }
}
