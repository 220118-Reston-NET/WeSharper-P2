using Microsoft.AspNetCore.SignalR;

namespace WeSharper.APIPortal.Hubs
{
    public class MessageHub : Hub
    {
        public async Task askServer(string someText)
        {
            string result;

            if (someText == "hey")
            {
                result = "Test connection Succeed!";
            }
            else
            {
                result = "OKKKKK!";
            }

            await Clients.Clients(this.Context.ConnectionId).SendAsync("askServerRespone", result);
        }
    }
}