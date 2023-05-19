using Microsoft.AspNetCore.SignalR;

namespace InvoiceGeneratorAPI.Hubs
{
    public class InvoiceGeneratorHub : Hub
    {
        public async Task NotifyCompletion()
        {
            await Clients.All.SendAsync("notification");
        }
    }
}
