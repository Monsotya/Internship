using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PlanetariumServiceWCF
{
    public class PlanetariumService : IPlanetariumService
    {
        public bool BuyTickets(int ticketId)
        {
            using (SqlConnection connection = new SqlConnection("data source=.; database=Planetarium; integrated security=SSPI"))
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
        public string SendEmail(string name, string seats)
        {
            string message = "Dear " + name + "! Your tickets: " + seats + " have been bought";
            /*MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("s61788466@gmail.com");
            message.To.Add(new MailAddress(request.Email));
            message.Subject = "Tickets";
            message.IsBodyHtml = true; 
            message.Body = message;
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";  
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("s61788466@gmail.com", "***********");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);*/
            return message;
        }
    }
}
