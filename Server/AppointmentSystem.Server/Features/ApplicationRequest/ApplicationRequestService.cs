namespace AppointmentSystem.Server.Features.ApplicationRequest
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Core.Interfaces.Features;
    using AppointmentSystem.Core.Interfaces.Repository;
    using AppointmentSystem.Infrastructure.Services;
    using AppointmentSystem.Server.Features.ApplicationRequest.Hubs;

    using Microsoft.AspNetCore.SignalR;
    using Microsoft.EntityFrameworkCore;

    internal class ApplicationRequestService : IApplicationRequestService
    {
        private readonly IDeletableEntityRepository<ApplicationRequest> applicationRequestRepository;
        private readonly IHubContext<ApplicationRequestHub, IApplicationRequestHub> applicationRequestHub;

        public ApplicationRequestService(
            IDeletableEntityRepository<ApplicationRequest> applicationRequestRepository,
            IHubContext<ApplicationRequestHub, IApplicationRequestHub> applicationRequestHub)
        {
            this.applicationRequestRepository = applicationRequestRepository ?? throw new ArgumentNullException(nameof(applicationRequestRepository));
            this.applicationRequestHub = applicationRequestHub ?? throw new ArgumentNullException(nameof(applicationRequestHub));
        }

        public async Task<IEnumerable<ApplicationRequest>> GetRequests()
            => await this.applicationRequestRepository
                .All()
                .ToListAsync();

        public async Task<Result> CreateRequest(ApplicationRequest applicationRequest, CancellationToken cancellationToken = default)
        {
            await this.applicationRequestRepository.AddAsync(applicationRequest);

            var result = await this.applicationRequestRepository.SaveChangesAsync(cancellationToken) switch
            {
                0 => "Problem during adding",
                _ => "Request is sended"
            };

            await applicationRequestHub.Clients.All.ApplicationRequest(applicationRequest, cancellationToken);

            return result;
        }
    }
}