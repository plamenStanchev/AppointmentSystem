namespace AppointmentSystem.Server.Features.Appointments.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Core.Interfaces.Features;
    using AppointmentSystem.Infrastructure.Constants;
    using AppointmentSystem.Infrastructure.Data.Identity;
    using AppointmentSystem.Infrastructure.Services;
    using AppointmentSystem.Server.Attributes;
    using AppointmentSystem.Server.Features.Appointments.Models;
    using AppointmentSystem.Server.Features.BaseFeatures.Controllers;

    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AppointmentController : ApiController
    {
        private readonly IAppointmentService appointmentService;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public AppointmentController(
            IAppointmentService appointmentService,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            this.appointmentService = appointmentService;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [Roles(RolesNames.Patient)]
        [HttpGet(nameof(GetPatientAppointments))]
        public async Task<ActionResult<IEnumerable<AppointmentDetailsResponseModel>>> GetPatientAppointments(string accountId, CancellationToken cancellationToken = default)
        {
            var patientAppointments = await this.appointmentService.GetPatientAppointAsync(accountId, cancellationToken);
            var patientAppointmentDtos = patientAppointments
                .Select(a => this.mapper.Map<AppointmentDetailsResponseModel>(a)).ToList();

            return patientAppointmentDtos;
        }

        [Roles(RolesNames.Doctor)]
        [HttpGet(nameof(GetDoctorAppointments))]
        public async Task<ActionResult<IEnumerable<AppointmentDetailsResponseModel>>> GetDoctorAppointments(string accountId, CancellationToken cancellationToken = default)
        {
            var doctorAppointments = await this.appointmentService.GetDoctorsAppointmentsAsync(accountId, cancellationToken);
            var doctorAppointmetDtos = doctorAppointments
                .Select(a => this.mapper.Map<AppointmentDetailsResponseModel>(a)).ToList();

            return doctorAppointmetDtos;
        }

        [Roles(RolesNames.Patient)]
        [HttpPost(nameof(Create))]
        public async Task<ActionResult<Result>> Create(AppointmentRequestModel appointmentModel, CancellationToken cancellationToken = default)
        {
            var appointmet = this.mapper.Map<Appointment>(appointmentModel);
            var accountId = this.userManager.GetUserId(this.User);

            var result = await this.appointmentService.CreateAppointmentAsync(appointmet, accountId, cancellationToken);
            return base.GenerateResultResponse(result);
        }

        [Roles(RolesNames.Patient, RolesNames.Doctor)]
        [HttpPost(nameof(Update))]
        public async Task<ActionResult<Result>> Update(AppointmentRequestModel appointmentModel, CancellationToken cancellationToken = default)
        {
            var appointmet = this.mapper.Map<Appointment>(appointmentModel);
            var accontId = this.userManager.GetUserId(this.User);
            var result = await this.appointmentService.UpdateAppointmentAsync(appointmet, accontId, cancellationToken);

            return base.GenerateResultResponse(result);
        }

        [Roles(RolesNames.Patient, RolesNames.Doctor)]
        [HttpPost(nameof(Delete))]
        public async Task<ActionResult<Result>> Delete(string accountId, int appointId, CancellationToken cancellationToken = default)
        {
            var result = await this.appointmentService.DeleteAppointmentAsync(appointId, accountId, cancellationToken);
            return base.GenerateResultResponse(result);
        }
    }
}
