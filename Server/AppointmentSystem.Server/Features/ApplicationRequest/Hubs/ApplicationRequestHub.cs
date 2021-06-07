namespace AppointmentSystem.Server.Features.ApplicationRequest.Hubs
{
    using System.Threading;
    using System.Threading.Tasks;

    using AppointmentSystem.Core.Entities.Models;

    using Microsoft.AspNetCore.SignalR;

    public class ApplicationRequestHub : Hub<IApplicationRequestHub> { }

    public interface IApplicationRequestHub
    {
        Task ApplicationRequest(ApplicationRequest value, CancellationToken cancellationToken = default);
    }
}