namespace AppointmentSystem.Core.AdministrationDomain.Service
{
    using AppointmentSystem.Core.AdministrationDomain.Entety;
    using System.Threading.Tasks;
    using System.Threading;
    using AppointmentSystem.Infrastructure.Services;

    public interface IApplicationService<TApplication>
        where TApplication : ApplicationBase
    {
        public Task<Result> ApproveApplication(int applicationId,
            AdministationInformationEntry administationInformationEntry,
            CancellationToken cancellationToken);
        public Task<Result> Rejected(int applicationId,
            AdministationInformationEntry administationInformationEntry,
            CancellationToken cancellationToken);
    }
}
