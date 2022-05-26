using Microsoft.AspNetCore.SignalR;


namespace WebAPI.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string username, string con)
        {
            await Clients.All.SendAsync("ReceiveMessage", username, con);
        }
    }
}
