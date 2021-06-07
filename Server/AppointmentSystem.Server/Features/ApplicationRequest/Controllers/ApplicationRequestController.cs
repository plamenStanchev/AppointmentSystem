namespace AppointmentSystem.Server.Features.ApplicationRequest.Controllers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Core.Interfaces.Features;
    using AppointmentSystem.Server.Features.ApplicationRequest.Models;
    using AppointmentSystem.Server.Features.BaseFeatures.Controllers;

    using Microsoft.AspNetCore.Mvc;

    public class ApplicationRequestController : ApiController
    {
        private readonly IApplicationRequestService applicationRequestService;

        public ApplicationRequestController(IApplicationRequestService applicationRequestService)
        {
            this.applicationRequestService = applicationRequestService ?? throw new ArgumentNullException(nameof(applicationRequestService));
        }

        [HttpPost]
        public async Task<IActionResult> DoctorRequest(ApplicationRequestModel applicationRequest, CancellationToken cancellationToken = default)
        {
            var request = this.UseMapper.Map<ApplicationRequest>(applicationRequest);
            await this.applicationRequestService.CreateRequest(request, cancellationToken);

            return this.Ok("Request is sended");
        }
    }
}