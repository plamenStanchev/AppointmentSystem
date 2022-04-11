namespace AppointmentSystem.Infrastructure.Data.Repositories
{
    using AppointmentSystem.Core.AdministrationDomain.Entety;
    using AppointmentSystem.Core.AdministrationDomain.Service;
    using AppointmentSystem.Infrastructure.Data.Helpers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class EfDeletableDoctorApplicationRepository : EfDeletableEntityRepository<DoctorApplication>, IDeletableDoctorApplicationRepository<DoctorApplication>
    {
        public EfDeletableDoctorApplicationRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<DoctorApplication>> GetAllPendingApplications(int pageSize, int pageNumber, CancellationToken cancellationToken)
        => await PagedEnumerable<DoctorApplication>.ToPagedListAsync(base.All().Where(i => i.CurentStatus == Status.pending), pageSize, pageNumber) ;

    }
}
