namespace AppointmentSystem.Server.Features.Appointments
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Core.Interfaces.Features;
    using AppointmentSystem.Core.Interfaces.Repository;
    using AppointmentSystem.Infrastructure.Services;

    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    //TODO : Move validation in different methods
    internal class AppointmentService : IAppointmentService
    {
        private readonly IDeletableEntityRepository<Appointment> appointmentRepository;

        private readonly IDoctorService doctorService;

        private readonly IPatientService patientService;

        public AppointmentService(
            IDeletableEntityRepository<Appointment> appointmentRepository,
            IPatientService patientService,
            IDoctorService doctorService)
        {
            this.appointmentRepository = appointmentRepository;
            this.patientService = patientService;
            this.doctorService = doctorService;
        }

        //TODO Move Validation Out OF Method
        public async Task<Result> CreateAppointmentAsync(Appointment appointment, string patientAccountId, CancellationToken cancellationToken = default)
        {
            var patientCheck = await this.patientService.GetPatientAsync(patientAccountId, cancellationToken);

            if (patientCheck?.Id != appointment.PatientId || patientCheck is null)
            {
                return "Problem with Patient";
            }

            await this.appointmentRepository.AddAsync(appointment);

            return await this.appointmentRepository.SaveChangesAsync(cancellationToken) switch
            {
                0 => "There was a problem adding the the record",
                _ => true
            };
        }

        public async Task<Result> DeleteAppointmentAsync(int appointmentId, string doctorAccountId, CancellationToken cancellationToken = default)
        {
            var doctor = await this.doctorService.GetDoctorAsync(doctorAccountId, cancellationToken);

            var appointment = await this.GetAppointmentAsync(appointmentId, cancellationToken);

            if (doctor?.Id != appointment?.DoctorId || appointment is null || doctor is null) // ??
            {
                return "you don't have permission";
            }

            this.appointmentRepository.Delete(appointment);

            return await this.appointmentRepository.SaveChangesAsync(cancellationToken) switch
            {
                0 => "Problem during deleting",
                _ => true
            };
        }

        public async Task<IEnumerable<Appointment>> GetDoctorsAppointmentsAsync(string accountId, CancellationToken cancellationToken = default)
        {
            var doctor = await this.doctorService.GetDoctorAsync(accountId, cancellationToken);

            if (doctor is null)
            {
                throw new ArgumentException(message: "Invalid AccountId");
            }

            var appointments = await this.appointmentRepository.All()
                .Where(a => a.DoctorId == doctor.Id)
                .Select(a => new Appointment()
                {
                    AppointmentStart = a.AppointmentStart,
                    Department = a.Department,
                    AppointmentEnd = a.AppointmentEnd,
                    Doctor = a.Doctor,
                    Patient = a.Patient,
                })
                .ToListAsync(cancellationToken);

            return appointments;
        }

        public async Task<IEnumerable<Appointment>> GetPatientAppointAsync(string accountId, CancellationToken cancellationToken = default)
        {
            var patient = await this.patientService.GetPatientAsync(accountId, cancellationToken);

            if (patient is null)
            {
                throw new ArgumentException(message: "Invalid AccountId");
            }

            var appointments = await this.appointmentRepository.All()
                    .Where(a => a.PatientId == patient.Id)
                    .Select(a => new Appointment()
                    {
                        AppointmentStart = a.AppointmentStart,
                        Department = a.Department,
                        AppointmentEnd = a.AppointmentEnd,
                        Doctor = a.Doctor,
                        Patient = a.Patient,
                        PatientId = a.PatientId,
                        DoctorId = a.DoctorId
                    })
                    .ToListAsync(cancellationToken);

            return appointments;
        }

        public async Task<Result> UpdateAppointmentAsync(Appointment appointment, string doctorAccountId, CancellationToken cancellationToken = default)
        {
            var doctor = await this.doctorService.GetDoctorAsync(doctorAccountId, cancellationToken);

            if (doctor is null)
            {
                return "Invalid doctorAccountId";
            }

            this.appointmentRepository.Update(appointment);

            return await this.appointmentRepository.SaveChangesAsync(cancellationToken) switch
            {
                0 => "There was a problem Updating the Entity",
                _ => true
            };
        }

        public async Task<Appointment> GetAppointmentAsync(int appointmentId, CancellationToken cancellationToken = default)
            => await this.appointmentRepository.All()
                .FirstOrDefaultAsync(a => a.Id == appointmentId, cancellationToken);
    }
}
