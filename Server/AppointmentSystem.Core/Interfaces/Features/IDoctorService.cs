namespace AppointmentSystem.Core.Interfaces.Features
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Infrastructure.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDoctorService
    {
        public Task<Result> CreateDoctorAsync(Doctor doctors);

        public Task<Result> DeleteDoctorAsync(string accountId);

        public Task<Result> UpdateDoctorAsync(Doctor doctor);

        public Task<Doctor> GetDoctorAsync(string accoutId);

        public Task<IEnumerable<Doctor>> GetDoctorsInCity(int cityId);
    }
}
