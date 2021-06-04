namespace AppointmentSystem.Core.Interfaces.Features
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Infrastructure.Services;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDoctorService
    {
        public Task<Result> CreateDoctorAsync(Doctor doctors, CancellationToken cancellationToken);

        public Task<Result> DeleteDoctorAsync(string accountId, CancellationToken cancellationToken);

        public Task<Result> UpdateDoctorAsync(Doctor doctor, CancellationToken cancellationToken);

        public Task<Doctor> GetDoctorAsync(string accoutId, CancellationToken cancellationToken);

        public Task<IEnumerable<Doctor>> GetDoctorsInCity(int cityId, CancellationToken cancellationToken);
    }
}
