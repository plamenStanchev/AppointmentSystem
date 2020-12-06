namespace AppointmentSystem.Server.Features.Patients.Controllers
{
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
    using System.Threading.Tasks;

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

        [HttpPost]
        [Route(nameof(Create))]
        public async Task<ActionResult<Result>> Create(PatientRequestModel patientModel)
        {
            var validatioResult = await base.ValidaiteAccountId(patientModel.AccountId);
            if (!validatioResult)
            {
                return base.GenerateResultResponse(false);
            }
            var patient = this.mapper.Map<Patient>(patientModel);
            var result = await this.patientService.CreatePatientAsync(patient);
            return base.GenerateResultResponse(result);
        }

        [HttpGet]
        [Authorize(Roles = RolesNames.PatientRoleName)]
        [Route(nameof(Get))]
        public async Task<ActionResult<PatientDetailsResponseModel>> Get(string accountId)
        {
            var validationResult = await base.ValidaiteAccountId(accountId);
            if (!validationResult)
            {
                return this.BadRequest("Problem with Authentication");
            }
            var patient = await this.patientService.GetPatientAsync(accountId);
            if (patient is null)
            {
                return this.BadRequest(new PatientDetailsResponseModel()
                {
                    ErrorMesage = "Problem"
                });
            }
            var patientDto = this.mapper.Map<PatientDetailsResponseModel>(patient);
            patientDto.Succeeded = true;
            return this.Ok(patientDto);
        }

        [HttpGet]
        [Authorize(Roles =RolesNames.PatientRoleName)]
        [Route(nameof(Delete))]
        public async Task<ActionResult<Result>> Delete(string accountId) 
        {
            var validateResult = await base.ValidaiteAccountId(accountId);
            if (!validateResult)
            {
                base.GenerateResultResponse(validateResult);
            }
            var user = await this.userManager.GetUserAsync(this.User);
            var result = await this.patientService.DeletePatientAsync(accountId);
            await base.userManager.RemoveFromRoleAsync(user, RolesNames.PatientRoleName);
            return base.GenerateResultResponse(result);
        }

        [HttpPost]
        [Authorize(Roles = RolesNames.PatientRoleName)]
        [Route(nameof(Update))]
        public async Task<ActionResult<Result>> Update(PatientRequestModel patientModel)
        {
            var validateResult = await base.ValidaiteAccountId(patientModel.AccountId);
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
