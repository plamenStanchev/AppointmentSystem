namespace AppointmentSystem.Core.Interfaces.Features
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Infrastructure.Services;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAppointmentService
    {
        public Task<Result> CreateAppointmentAsync(Appointment appointment, string patientAccountId);

        public Task<Result> DeleteAppointmentAsync(int appointmentId, string doctorAccountId);

        public Task<Result> UpdateAppointmentAsync(Appointment appointment, string doctorAccountId);

        public Task<IEnumerable<Appointment>> GetDoctorsAppointmetsAsync(string accountId);

        public Task<IEnumerable<Appointment>> GetPatientAppointAsync(string accountId);
    }
}
