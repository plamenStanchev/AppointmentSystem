namespace AppointmentSystem.Server.Features.Administration.Contollers
{
    using AppointmentSystem.Core.AdministrationDomain.Entety;
    using AppointmentSystem.Core.AdministrationDomain.Service;
    using AppointmentSystem.Core.Interfaces.Repository;
    using AppointmentSystem.Infrastructure.Constants;
    using AppointmentSystem.Infrastructure.Data.Identity;
    using AppointmentSystem.Infrastructure.Services;
    using AppointmentSystem.Server.Attributes;
    using AppointmentSystem.Server.Features.Administration.Models;
    using AppointmentSystem.Server.Features.BaseFeatures.Controllers;
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class ApplicationControlelr : ApiAccountController
    {
        private readonly IMapper mapper;
        private readonly IApplicationService<DoctorApplication> applicationService;
        private readonly IDeletableEntityRepository<DoctorApplication> docotrApplicatonDeletableEntityRepository;
        public ApplicationControlelr(
            IMapper mapper,
            IApplicationService<DoctorApplication> applicationService,
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<DoctorApplication> docotrApplicatonDeletableEntityRepository)
            : base(userManager)
        {
            this.mapper = mapper;
            this.applicationService = applicationService;
            this.docotrApplicatonDeletableEntityRepository = docotrApplicatonDeletableEntityRepository;
        }

        [Roles(RolesNames.Admin)]
        [HttpPost(nameof(RejectApplication))]
        public async Task<Result> RejectApplication([FromBody] int applicationId, CancellationToken cancellationToken = default)
        => await this.applicationService.Rejected(applicationId, new AdministationInformationEntry(status: Status.rejected), cancellationToken);
        
        [Roles(RolesNames.Admin)]
        [HttpPost(nameof(CreateApplication))]
        public async Task<Result> CreateApplication([FromBody] DoctorApplicationModel doctorApplicationequestModel, CancellationToken cancellationToken = default) 
        {
            var administationEntety = new AdministationInformationEntry(status: Status.pending);
            DoctorApplication doctorApplication = new DoctorApplication(
                doctorApplicationequestModel.AccountId,
                doctorApplicationequestModel.FirstName,
                doctorApplicationequestModel.SecondName,
                doctorApplicationequestModel.SurName,
                doctorApplicationequestModel.PIN,
                doctorApplicationequestModel.CityId,
                doctorApplicationequestModel.Description,
                doctorApplicationequestModel.DepartmentId,
                administationEntety);
            await this.docotrApplicatonDeletableEntityRepository.AddAsync(doctorApplication);
            if (await this.docotrApplicatonDeletableEntityRepository.SaveChangesAsync(cancellationToken) != 0)// TODO: put this outside theaction
            {
                return true;
            }
            return false;
        }
        [Roles(RolesNames.Admin)]
        [HttpPost(nameof(ApproveApplication))]
        public async Task<ActionResult<Result>> ApproveApplication([FromBody] int applicationId, CancellationToken cancellationToken = default)
        => await this.applicationService.ApproveApplication(applicationId, new AdministationInformationEntry(status: Status.approved), cancellationToken);

        

        [Roles(RolesNames.Admin)]
        public async Task<DoctorApplicationModel> GetApplication(int applicationId, CancellationToken cancellationToken = default)
        {
            var doctorApplication = await this.docotrApplicatonDeletableEntityRepository.All()
                .Where(da => da.Id == applicationId)
                .FirstOrDefaultAsync();
            if( doctorApplication is null)
            {
                return null;
            }

            return  this.mapper.Map<DoctorApplicationModel>(doctorApplication);
        }
    }
}
