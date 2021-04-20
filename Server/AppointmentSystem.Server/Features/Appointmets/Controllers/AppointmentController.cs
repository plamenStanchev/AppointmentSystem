namespace AppointmentSystem.Server.Features.Appointmets.Controllers
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Core.Interfaces.Features;
    using AppointmentSystem.Infrastructure.Constants;
    using AppointmentSystem.Infrastructure.Data.Identity;
    using AppointmentSystem.Infrastructure.Services;
    using AppointmentSystem.Server.Features.Appointmets.Hubs;
    using AppointmentSystem.Server.Features.Appointmets.Models;
    using AppointmentSystem.Server.Features.BaseFeatures.Controllers;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MessagePost
    {
        public virtual string Message { get; set; }
    }

    public class AppointmentController : ApiController
    {
        private readonly IAppointmentService appointmentService;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHubContext<NotifyHub> hub;

        public AppointmentController(
            IAppointmentService appointmentService,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            IHubContext<NotifyHub> hub)
        {
            this.appointmentService = appointmentService;
            this.mapper = mapper;
            this.userManager = userManager;
            this.hub = hub;
        }

        [HttpPost]
        public async Task<IActionResult> Create(MessagePost messagePost)
        {
            // await this.hub.Clients.All.SendAsync("test", "The message '" + messagePost.Message + "' has been received");
            await this.hub.Clients.All.SendAsync("rest", messagePost.Message);
            return Ok();
        }


        [HttpGet(nameof(GetPatientAppointmets))]
        [Authorize(Roles = RolesNames.Patient)]
        public async Task<ActionResult<IEnumerable<AppointmentDetailsResponseModel>>> GetPatientAppointmets(string accountId)
        {
            var patientAppointments = await this.appointmentService.GetPatientAppointAsync(accountId);
            var patientAppointmentDtos = patientAppointments
                .Select(a => this.mapper.Map<AppointmentDetailsResponseModel>(a)).ToList();

            patientAppointmentDtos.ForEach(a => a.Succeeded = true);

            return patientAppointmentDtos;
        }

        [HttpGet(nameof(GetDoctorAppointmets))]
        [Authorize(Roles = RolesNames.Doctor)]
        public async Task<ActionResult<IEnumerable<AppointmentDetailsResponseModel>>> GetDoctorAppointmets(string accountId)
        {
            var doctorAppointments = await this.appointmentService.GetDoctorsAppointmetsAsync(accountId);
            var doctorAppointmetDtos = doctorAppointments
                .Select(a => this.mapper.Map<AppointmentDetailsResponseModel>(a)).ToList();
            doctorAppointmetDtos.ForEach(a => a.Succeeded = true);

            return doctorAppointmetDtos;
        }

        [HttpPost(nameof(Create))]
        [Authorize(Roles = RolesNames.Patient)]
        public async Task<ActionResult<Result>> Create(AppointmentRequestModel appointmentModel)
        {
            var appointmet = this.mapper.Map<Appointment>(appointmentModel);
            var accountId = this.userManager.GetUserId(this.User);

            var result = await this.appointmentService.CreateAppointmentAsync(appointmet, accountId);
            return base.GenerateResultResponse(result);
        }

        [HttpPost(nameof(Update))]
        [Authorize(Roles = RolesNames.Patient + Comma + RolesNames.Doctor)]
        public async Task<ActionResult<Result>> Update(AppointmentRequestModel appointmentModel)
        {
            var appointmet = this.mapper.Map<Appointment>(appointmentModel);
            var accontId = this.userManager.GetUserId(this.User);
            var result = await this.appointmentService.UpdateAppointmentAsync(appointmet, accontId);

            return base.GenerateResultResponse(result);
        }

        [HttpPost(nameof(Delete))]
        [Authorize(Roles = RolesNames.Doctor + Comma + RolesNames.Patient + Comma + RolesNames.Admin)]
        public async Task<ActionResult<Result>> Delete(string accountId, int appointId)
        {
            var result = await this.appointmentService.DeleteAppointmentAsync(appointId, accountId);
            return base.GenerateResultResponse(result);
        }
    }
}
