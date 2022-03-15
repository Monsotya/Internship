using Microsoft.AspNetCore.SignalR;

namespace PlanetariumService.Hubs
{
    public class BuyingTicketsHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
