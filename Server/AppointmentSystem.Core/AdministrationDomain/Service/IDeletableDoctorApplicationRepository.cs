namespace AppointmentSystem.Core.AdministrationDomain.Service
{
    using AppointmentSystem.Core.AdministrationDomain.Entety;
    using AppointmentSystem.Core.Interfaces.Repository;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDeletableDoctorApplicationRepository<TDoctorApplication> : IDeletableEntityRepository<TDoctorApplication>
        where TDoctorApplication : DoctorApplication
    {
        public Task<IEnumerable<TDoctorApplication>> GetAllPendingApplications(int pageSize,int pageNumber, CancellationToken cancellationToken);
    }
}
