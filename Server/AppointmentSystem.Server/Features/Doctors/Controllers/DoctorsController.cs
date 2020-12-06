namespace AppointmentSystem.Server.Features.Doctors.Controllers
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Core.Interfaces.Features;
    using AppointmentSystem.Infrastructure.Constants;
    using AppointmentSystem.Infrastructure.Data.Identity;
    using AppointmentSystem.Infrastructure.Services;
    using AppointmentSystem.Server.Features.BaseFeatures.Controllers;
    using AppointmentSystem.Server.Features.Doctors.Models;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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

        [HttpGet]
        [Authorize(Roles = RolesNames.DoctorRoleName)]
        [Route(nameof(Get))]
        public async Task<ActionResult<DoctorDetailsResponseModel>> Get(string accountId)
        {
            var validationResult = await base.ValidaiteAccountId(accountId);
            if (!validationResult)
            {
                return this.BadRequest(new DoctorDetailsResponseModel()
                { Succeeded = false, ErrorMesage = "Problem with Authentication" });
            }
            var result = await this.doctorService.GetDoctorAsync(accountId);
            var doctorResponse = this.mapper.Map<DoctorDetailsResponseModel>(result);
            doctorResponse.Succeeded = true;
            return this.Ok(doctorResponse);
        }

        [HttpPost]
        [Route(nameof(Create))]
        public async Task<ActionResult<Result>> Create(DoctorRequsetModel requsetModel)
        {
            var validationResult = await base.ValidaiteAccountId(requsetModel.AccountId);
            if (!validationResult)
            {
                return this.BadRequest("Problem with Authentication");
            }
            var doctor = this.mapper.Map<Doctor>(requsetModel);
            var result = await this.doctorService.CreateDoctorAsynch(doctor);
            return base.GenerateResultResponse(result);
        }

        [HttpGet]
        [Authorize(Roles = RolesNames.DoctorRoleName)]
        [Route(nameof(Update))]
        public async Task<ActionResult<Result>> Update(DoctorRequsetModel requsetModel)
        {

            var validationResult = await base.ValidaiteAccountId(requsetModel.AccountId);
            if (!validationResult)
            {
                return this.BadRequest("Problem with Authentication");
            }
            var doctor = this.mapper.Map<Doctor>(requsetModel);
            var result = await this.doctorService.UpdateDoctorAsync(doctor);
            return base.GenerateResultResponse(result);
        }

        [HttpGet]
        [Authorize(Roles = RolesNames.DoctorRoleName)]
        [Route(nameof(Delete))]
        public async Task<ActionResult<Result>> Delete(string accountId)
        {

            var validationResult = await base.ValidaiteAccountId(accountId);
            if (!validationResult)
            {
                return this.BadRequest("Problem with Authentication");
            }
            var result = await this.doctorService.DeleteDoctorAsync(accountId);
            var user = await base.userManager.GetUserAsync(this.User);
            await base.userManager.RemoveFromRoleAsync(user, RolesNames.DoctorRoleName);
            return base.GenerateResultResponse(result);
        }

        [HttpGet]
        [Route(nameof(GetInCity))]
        [Authorize(Roles = RolesNames.DoctorRoleName + "," + RolesNames.PatientRoleName)]
        public async Task<ActionResult<IEnumerable<DoctorDetailsResponseModel>>> GetInCity(int cityId)
        {
            var result = await this.doctorService.GetDoctorsInCity(cityId);
            var dtoResult = result.Select(d => this.mapper.Map<DoctorDetailsResponseModel>(d)).ToList();

                dtoResult.ForEach(c => c.Succeeded = true);
            return this.Ok(dtoResult);
        }
    }
}
