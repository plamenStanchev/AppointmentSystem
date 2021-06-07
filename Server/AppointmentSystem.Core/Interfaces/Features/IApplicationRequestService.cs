namespace AppointmentSystem.Core.Interfaces.Features
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Infrastructure.Services;

    public interface IApplicationRequestService
    {
        Task<Result> CreateRequest(ApplicationRequest applicationRequest, CancellationToken cancellationToken);

        Task<IEnumerable<ApplicationRequest>> GetRequests();
    }
}