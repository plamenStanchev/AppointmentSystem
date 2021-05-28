namespace AppointmentSystem.Server.Features.Appointments.Controllers
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using AppointmentSystem.Core.Entities.Models;
	using AppointmentSystem.Core.Interfaces.Features;
	using AppointmentSystem.Infrastructure.Constants;
	using AppointmentSystem.Infrastructure.Data.Identity;
	using AppointmentSystem.Infrastructure.Services;
	using AppointmentSystem.Server.Attributes;
	using AppointmentSystem.Server.Features.Appointments.Hubs;
	using AppointmentSystem.Server.Features.Appointments.Models;
	using AppointmentSystem.Server.Features.BaseFeatures.Controllers;
	using AutoMapper;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.SignalR;

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


		[Roles(RolesNames.Patient)]
		[HttpGet(nameof(GetPatientAppointments))]
		public async Task<ActionResult<IEnumerable<AppointmentDetailsResponseModel>>> GetPatientAppointments(string accountId)
		{
			var patientAppointments = await this.appointmentService.GetPatientAppointAsync(accountId);
			var patientAppointmentDtos = patientAppointments
				.Select(a => this.mapper.Map<AppointmentDetailsResponseModel>(a)).ToList();

			return patientAppointmentDtos;
		}

		[Roles(RolesNames.Doctor)]
		[HttpGet(nameof(GetDoctorAppointments))]
		public async Task<ActionResult<IEnumerable<AppointmentDetailsResponseModel>>> GetDoctorAppointments(string accountId)
		{
			var doctorAppointments = await this.appointmentService.GetDoctorsAppointmentsAsync(accountId);
			var doctorAppointmetDtos = doctorAppointments
				.Select(a => this.mapper.Map<AppointmentDetailsResponseModel>(a)).ToList();

			return doctorAppointmetDtos;
		}

		[Roles(RolesNames.Patient)]
		[HttpPost(nameof(Create))]
		public async Task<ActionResult<Result>> Create(AppointmentRequestModel appointmentModel)
		{
			var appointmet = this.mapper.Map<Appointment>(appointmentModel);
			var accountId = this.userManager.GetUserId(this.User);

			var result = await this.appointmentService.CreateAppointmentAsync(appointmet, accountId);
			return base.GenerateResultResponse(result);
		}

		[Roles(RolesNames.Patient, RolesNames.Doctor)]
		[HttpPost(nameof(Update))]
		public async Task<ActionResult<Result>> Update(AppointmentRequestModel appointmentModel)
		{
			var appointmet = this.mapper.Map<Appointment>(appointmentModel);
			var accontId = this.userManager.GetUserId(this.User);
			var result = await this.appointmentService.UpdateAppointmentAsync(appointmet, accontId);

			return base.GenerateResultResponse(result);
		}

		[Roles(RolesNames.Patient, RolesNames.Doctor)]
		[HttpPost(nameof(Delete))]
		public async Task<ActionResult<Result>> Delete(string accountId, int appointId)
		{
			var result = await this.appointmentService.DeleteAppointmentAsync(appointId, accountId);
			return base.GenerateResultResponse(result);
		}
	}
}
