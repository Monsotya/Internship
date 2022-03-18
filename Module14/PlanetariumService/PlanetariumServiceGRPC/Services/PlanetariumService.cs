using Grpc.Core;
using PlanetariumModels;
using PlanetariumServiceGRPC;
using PlanetariumServices;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace PlanetariumServiceGRPC.Services
{
    public class PlanetariumService : Planetarium.PlanetariumBase
    {
        private readonly ILogger<PlanetariumService> _logger;
        public PlanetariumService(ILogger<PlanetariumService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
        public override Task<TicketsMessage> BuyTickets(TicketsInfo request, ServerCallContext context)
        {
            List<Ticket> tickets = request.TicketService.GetTicketsById(request.PosterId).ToList<Ticket>();

            int counter = request.Quantity;
            foreach (Ticket ticket in tickets)
            {
                if (counter == 0)
                {
                    return Task.FromResult(new TicketsMessage
                    {
                        Message = "Buying was successful"
                    });
                }
                if (ticket.TicketStatus == "available")
                {
                    request.TicketService.BuyTickets(ticket.Id, request.Order);
                }
                counter--;
            }

            return Task.FromResult(new TicketsMessage
            {
                Message = "Something went wrong"
            });
        }
        public override Task<EmailMessage> SendEmail(EmailInfo request, ServerCallContext context)
        {
            string message = "Dear " + request.Name + "! Your tickets: " + request.Seats + " have been bought";
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
            return Task.FromResult(new EmailMessage
            {
                Message = message
            });
        }
    }
}