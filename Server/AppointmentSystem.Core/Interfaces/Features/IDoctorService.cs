namespace AppointmentSystem.Core.Interfaces.Features
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Infrastructure.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IDoctorService
    {
        public Task<Result> CreateDoctorAsynch(Doctor doctors);

        public Task<Result> DeleteDoctorAsync(int doctorId, string accountId);

        public Task<Result> UpdateDoctorAsync(Doctor doctor);

        public Task<Doctor> GetDoctorAsync(int doctorId, string accoutId);

        public Task<IEnumerable<Doctor>> GetDoctorsInCity(int cityId);
    }
}
