namespace AppointmentSystem.Core.Interfaces.Features
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Infrastructure.Services;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IPatientService
    {
        public Task<Result> CreatePatientAsync(Patient patient,CancellationToken cancellationToken);

        public Task<Patient> GetPatientAsync(string accountId, CancellationToken cancellationToken);

        public Task<Result> UpdatePatientAsync(Patient patient, CancellationToken cancellationToken);

        public Task<Result> DeletePatientAsync(string accountId, CancellationToken cancellationToken);
    }
}
