using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

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
        public void CreatePoster(DateTime dateOfEvent, float price, int performanceId, int hallId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "CreatePoster",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@dateOfEvent", dateOfEvent);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@performanceId", performanceId);
                cmd.Parameters.AddWithValue("@hallId", hallId);

                cmd.ExecuteNonQuery();
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

                /*SqlCommand cm = new SqlCommand($"select * from ticket where id = {ticketId}", connection);
                SqlDataReader sdr = cm.ExecuteReader();
                while (sdr.Read()) {
                    if((string)sdr["TicketStatus"] == "available")
                    {
                        sdr.Close();
                        SqlCommand cmd = new SqlCommand($"update ticket set ticketstatus = 'bought' where id = {ticketId}", connection);
                        SqlDataReader sd = cmd.ExecuteReader();
                        return true;
                    }
                }
                return false;*/
            }
        }
        public DataTable GetAvailablePerformances(DateTime from, DateTime to) 
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
                return performances;

                /*connection.Open();
                SqlDataAdapter sda = new SqlDataAdapter($"SELECT DISTINCT DateOfEvent, HallName, Price, Title, Duration " +
                    $"FROM Poster, Hall, Performance WHERE[Performance].Id = PerformanceId AND[Hall].Id = HallId " +
                    $"AND DateOfEvent BETWEEN '{from.ToString("yyyy-MM-dd HH:mm:ss")}' AND '{to.ToString("yyyy-MM-dd HH:mm:ss")}'", connection);
                DataTable performances = new DataTable();
                sda.Fill(performances);
                return performances;*/
            }
        }

        public List<Dictionary<string, object>> RevokeOrders(DateTime from, DateTime to, int hallid, List<int> places)
        {
            if (places.Count == 0)
            {
                throw new Exception("Places must not be NULL!");
            }

            StringBuilder placesString = new StringBuilder(places[0].ToString());
            for (int i = 1; i < places.Count; i++)
            {
                placesString.Append(",");
                placesString.Append(places[i]);
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlDataAdapter da = new SqlDataAdapter("GetRevokedOrders", connection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();

                da.SelectCommand.Parameters.AddWithValue("@from", from);
                da.SelectCommand.Parameters.AddWithValue("@to", to);
                da.SelectCommand.Parameters.AddWithValue("@places", placesString.ToString());
                da.SelectCommand.Parameters.AddWithValue("@hallid", hallid);
                DataTable orders = new DataTable();

                da.Fill(orders);
                foreach (DataRow order in orders.Rows)
                {
                    result.Add(new Dictionary<string, object> { { "OrderId", order["Id"] }, { "ClientEmail", order["Email"] }, { "InfoMessage",
                            $"Dear client {order["ClientName"]}, we are sorry to inform that your order canceled due to maintance work on " +
                            $"ordered place number {order["Place"]}, payment in amount {order["Price"]} will be returned to your bank account soon" } });
                }

                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "RevokeOrders",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);
                cmd.Parameters.AddWithValue("@places", places);
                cmd.Parameters.AddWithValue("@hallid", hallid);

                return result;

                /*connection.Open();

                List<Dictionary<string, object>> result =  new List<Dictionary<string, object>>();
                SqlDataAdapter sda = new SqlDataAdapter($"SELECT Place, Price * Surcharge as Price, [Orders].Id, ClientName, Email FROM Ticket INNER JOIN Orders ON([Ticket].OrderId = [Orders].Id) INNER JOIN Poster ON([Ticket].PosterId = [Poster].Id) INNER JOIN Tier ON([Ticket].TierId = [Tier].Id) WHERE[Ticket].place in ({placesString}) AND(SELECT HallId FROM Poster WHERE Id = PosterId) = {hallid} AND (SELECT DateOfEvent FROM Poster WHERE Id = PosterId) BETWEEN '{from.ToString("yyyy-MM-dd HH:mm:ss")}' AND '{to.ToString("yyyy-MM-dd HH:mm:ss")}'", connection);
                
                DataTable orders = new DataTable();
     
                sda.Fill(orders);
                foreach (DataRow order in orders.Rows)
                {
                    result.Add(new Dictionary<string, object> { { "OrderId", order["Id"] }, { "ClientEmail", order["Email"] }, { "InfoMessage", 
                            $"Dear client {order["ClientName"]}, we are sorry to inform that your order canceled due to maintance work on " +
                            $"ordered place number {order["Place"]}, payment in amount {order["Price"]} will be returned to your bank account soon" } });
                }
                SqlCommand cm = new SqlCommand($"UPDATE Ticket SET TicketStatus = 'unavailable', OrderId = NULL WHERE [Ticket].place in ({placesString}) AND (SELECT HallId FROM Poster WHERE Id = PosterId) = {hallid} AND (SELECT DateOfEvent FROM Poster WHERE Id = PosterId) BETWEEN '{from.ToString("yyyy-MM-dd HH:mm:ss")}' AND '{to.ToString("yyyy-MM-dd HH:mm:ss")}'", connection);
                SqlDataReader sdr = cm.ExecuteReader();
                sdr.Close();
                return result;*/
            }
}
    }
}
