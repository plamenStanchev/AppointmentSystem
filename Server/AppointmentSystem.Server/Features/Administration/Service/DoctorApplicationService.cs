namespace AppointmentSystem.Core.AdministrationDomain.Service
{
    using AppointmentSystem.Core.AdministrationDomain.Entety;
    using AppointmentSystem.Core.Interfaces.Features;
    using AppointmentSystem.Core.Interfaces.Repository;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Infrastructure.Services;

    public class DoctorApplicationService : IApplicationService<DoctorApplication>
    {
        private readonly IDoctorService doctorService;
        private readonly IDeletableEntityRepository<DoctorApplication> doctorApplicationEntetyRepository;
        private readonly IMapper mapper;
        public DoctorApplicationService(IDoctorService doctorService,
            IDeletableEntityRepository<DoctorApplication> doctorApplicationEntetyRepository,
            IMapper mapper)
        {
            this.doctorService = doctorService;
            this.doctorApplicationEntetyRepository = doctorApplicationEntetyRepository;
            this.mapper = mapper;
        }
        public async Task<Result> ApproveApplication(int applicationId,
            AdministationInformationEntry administationInformationEntry,
            CancellationToken cancellationToken = default)
        {
            var doctorApplication = await this.doctorApplicationEntetyRepository.All()
                .FirstOrDefaultAsync(ai => ai.Id == applicationId);
            doctorApplication.Approve(administationInformationEntry);
            Doctor doctor = this.mapper.Map<Doctor>(doctorApplication);
            await this.doctorService.CreateDoctorAsync(doctor, cancellationToken);
            return true;
        }

        public async Task<Result> Rejected(int applicationId,
            AdministationInformationEntry administationInformationEntry, 
            CancellationToken cancellationToken)
        {
            var doctorApplication = await this.doctorApplicationEntetyRepository.All()
                .FirstOrDefaultAsync(ai => ai.Id == applicationId);
            doctorApplication.Rejected(administationInformationEntry);
            return true;
        }
    }
}
