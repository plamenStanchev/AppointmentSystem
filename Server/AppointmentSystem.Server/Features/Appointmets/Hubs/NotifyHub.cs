namespace AppointmentSystem.Server.Features.Appointments.Hubs
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;

    public class NotifyHub : Hub
    {
        // public override async Task OnConnectedAsync()
        // {
        //     await Clients.Client(Context.ConnectionId).SendAsync("test", "Connected");
        // }

        // public override async Task OnConnectedAsync()
        // {
        //     await Clients.Client(Context.ConnectionId).SendAsync("rest", "Connected");
        // }

        public async Task Notify(string value)
        {
            await Task.Delay(3000);

            for (int i = 0; i < 12; i++)
            {
                await Task.Delay(500);
                await this.Clients.All.SendAsync("rest", value);
            }
        }
    }
}