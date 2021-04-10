namespace AppointmentSystem.Server.Features.Appointmets.Controllers
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Core.Interfaces.Features;
    using AppointmentSystem.Infrastructure.Constants;
    using AppointmentSystem.Infrastructure.Data.Identity;
    using AppointmentSystem.Infrastructure.Services;
    using AppointmentSystem.Server.Features.Appointmets.Models;
    using AppointmentSystem.Server.Features.BaseFeatures.Controllers;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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
        [Authorize(Roles = RolesNames.Doctor + Comma + RolesNames.Patient)]
        public async Task<ActionResult<Result>> Delete(string accountId, int appointId)
        {
            var result = await this.appointmentService.DeleteAppointmentAsync(appointId, accountId);
            return base.GenerateResultResponse(result);
        }
    }
}
