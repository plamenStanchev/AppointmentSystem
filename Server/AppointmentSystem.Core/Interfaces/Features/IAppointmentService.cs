namespace AppointmentSystem.Core.Interfaces.Features
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Infrastructure.Services;

    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IAppointmentService
    {
        public Task<Result> CreateAppointmentAsync(Appointment appointment, string patientAccountId, CancellationToken cancellationToken);

        public Task<Result> DeleteAppointmentAsync(int appointmentId, string doctorAccountId, CancellationToken cancellationToken);

        public Task<Result> UpdateAppointmentAsync(Appointment appointment, string doctorAccountId, CancellationToken cancellationToken);

        public Task<IEnumerable<Appointment>> GetDoctorsAppointmentsAsync(string accountId,CancellationToken cancellationToken);

        public Task<IEnumerable<Appointment>> GetPatientAppointAsync(string accountId, CancellationToken cancellationToken);

        public Task<Appointment> GetAppointmentAsync(int appointmentId, CancellationToken cancellationToken);
    }
}
