namespace AppointmentSystem.Core.Interfaces.Features
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Infrastructure.Services;
    using System.Threading.Tasks;
    public interface IPatientService
    {
        public Task<Result> CreatePatientAsync(Patient patient);

        public Task<Patient> GetPatientAsync(string accountId);

        public Task<Result> UpdatePatientAsync(Patient patient);

        public Task<Result> DeletePatientAsync(string accountId);


    }
}
