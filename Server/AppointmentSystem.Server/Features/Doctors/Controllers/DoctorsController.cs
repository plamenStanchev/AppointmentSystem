namespace AppointmentSystem.Server.Features.Doctors.Controllers
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
	using AppointmentSystem.Server.Features.BaseFeatures.Controllers;
	using AppointmentSystem.Server.Features.Doctors.Models;
	using AutoMapper;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;

	public class DoctorsController : ApiAccountController
	{
		private readonly IDoctorService doctorService;
		private readonly IMapper mapper;

		public DoctorsController(
			IDoctorService doctorService,
			UserManager<ApplicationUser> userManager,
			IMapper mapper) : base(userManager)
		{
			this.doctorService = doctorService;
			this.mapper = mapper;
		}

		[Roles(RolesNames.Doctor)]
		[HttpGet("{action}")]
		public async Task<ActionResult<DoctorDetailsResponseModel>> Get(string accountId, CancellationToken cancellationToken = default)
		{
			var validationResult = await base.ValidateAccountId(accountId);
			if (!validationResult)
			{
				return this.BadRequest("Invalid request");
			}
			var result = await this.doctorService.GetDoctorAsync(accountId, cancellationToken);

			if (result is null)
			{
				return this.BadRequest("There isn't a doctor with this Id");
			}

			var doctorResponse = this.mapper.Map<DoctorDetailsResponseModel>(result);

			return this.Ok(doctorResponse);
		}

		[HttpPost(nameof(Create))]
		public async Task<ActionResult<Result>> Create(DoctorRequestModel requestModel, CancellationToken cancellationToken = default)
		{
			var validationResult = await base.ValidateExistAccount(requestModel.AccountId);
			if (!validationResult)
			{
				return this.BadRequest("Problem with Authentication");
			}
			var doctor = this.mapper.Map<Doctor>(requestModel);
			var result = await this.doctorService.CreateDoctorAsync(doctor, cancellationToken);
			return base.GenerateResultResponse(result);
		}

		[Roles(RolesNames.Doctor)]
		[HttpGet(nameof(Update))]
		public async Task<ActionResult<Result>> Update(DoctorRequestModel requestModel, CancellationToken cancellationToken = default)
		{
			var validationResult = await base.ValidateAccountId(requestModel.AccountId);
			if (!validationResult)
			{
				return this.BadRequest("Problem with Authentication");
			}
			var doctor = this.mapper.Map<Doctor>(requestModel);
			var result = await this.doctorService.UpdateDoctorAsync(doctor, cancellationToken);
			return base.GenerateResultResponse(result);
		}

		[Roles(RolesNames.Doctor)]
		[HttpGet(nameof(Delete))]
		public async Task<ActionResult<Result>> Delete(string accountId, CancellationToken cancellationToken = default)
		{
			var validationResult = await base.ValidateAccountId(accountId);
			if (!validationResult)
			{
				return this.BadRequest("Problem with Authentication");
			}
			var result = await this.doctorService.DeleteDoctorAsync(accountId, cancellationToken);
			var user = await base.userManager.GetUserAsync(this.User);
			await base.userManager.RemoveFromRoleAsync(user, RolesNames.Doctor);
			return base.GenerateResultResponse(result);
		}

		[HttpGet(nameof(GetInCity))]
		public async Task<ActionResult<IEnumerable<DoctorDetailsResponseModel>>> GetInCity(int cityId, CancellationToken cancellationToken = default)
		{
			var result = await this.doctorService.GetDoctorsInCity(cityId, cancellationToken);
			var dtoResult = result.Select(d => this.mapper.Map<DoctorDetailsResponseModel>(d)).ToList();

			return this.Ok(dtoResult);
		}
	}
}
