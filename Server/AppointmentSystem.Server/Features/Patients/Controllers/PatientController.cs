namespace AppointmentSystem.Server.Features.Patients.Controllers
{
	using System.Threading.Tasks;
	using AppointmentSystem.Core.Entities.Models;
	using AppointmentSystem.Core.Interfaces.Features;
	using AppointmentSystem.Infrastructure.Constants;
	using AppointmentSystem.Infrastructure.Data.Identity;
	using AppointmentSystem.Infrastructure.Services;
	using AppointmentSystem.Server.Features.BaseFeatures.Controllers;
	using AppointmentSystem.Server.Features.Patients.Models;
	using AutoMapper;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;

	public class PatientController : ApiAccountController
	{
		private readonly IPatientService patientService;
		private readonly IMapper mapper;

		public PatientController(
			IPatientService patientService,
			UserManager<ApplicationUser> userManager,
			IMapper mapper)
			: base(userManager)
		{
			this.patientService = patientService;
			this.mapper = mapper;
		}

		[HttpPost(nameof(Create))]
		public async Task<ActionResult<Result>> Create(PatientRequestModel patientModel)
		{
			var validatioResult = await base.ValidateAccountId(patientModel.AccountId);
			if (!validatioResult)
			{
				return base.GenerateResultResponse(false);
			}
			var patient = this.mapper.Map<Patient>(patientModel);
			var result = await this.patientService.CreatePatientAsync(patient);
			return base.GenerateResultResponse(result);
		}

		[HttpGet(nameof(Get))]
		[Authorize(Roles = RolesNames.Patient)]
		public async Task<ActionResult<PatientDetailsResponseModel>> Get(string accountId)
		{
			var validationResult = await base.ValidateAccountId(accountId);
			if (!validationResult)
			{
				return this.BadRequest("Problem with Authentication");
			}
			var patient = await this.patientService.GetPatientAsync(accountId);
			if (patient is null)
			{
				return this.BadRequest("There isn't a patient with this Id");
			}
			var patientDto = this.mapper.Map<PatientDetailsResponseModel>(patient);

			return this.Ok(patientDto);
		}

		[HttpGet(nameof(Delete))]
		[Authorize(Roles = RolesNames.Patient)]
		public async Task<ActionResult<Result>> Delete(string accountId)
		{
			var validateResult = await base.ValidateAccountId(accountId);
			if (!validateResult)
			{
				base.GenerateResultResponse(validateResult);
			}
			var user = await this.userManager.GetUserAsync(this.User);
			var result = await this.patientService.DeletePatientAsync(accountId);
			await base.userManager.RemoveFromRoleAsync(user, RolesNames.Patient);
			return base.GenerateResultResponse(result);
		}

		[HttpPost(nameof(Update))]
		[Authorize(Roles = RolesNames.Patient)]
		public async Task<ActionResult<Result>> Update(PatientRequestModel patientModel)
		{
			var validateResult = await base.ValidateAccountId(patientModel.AccountId);
			if (!validateResult)
			{
				return base.GenerateResultResponse(validateResult);
			}
			var patient = this.mapper.Map<Patient>(patientModel);
			var result = await this.patientService.UpdatePatientAsync(patient);
			return base.GenerateResultResponse(result);
		}
	}
}
