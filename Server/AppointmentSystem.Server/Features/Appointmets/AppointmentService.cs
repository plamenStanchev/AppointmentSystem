namespace AppointmentSystem.Server.Features.Appointmets
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Core.Interfaces.Features;
    using AppointmentSystem.Core.Interfaces.Repository;
    using AppointmentSystem.Infrastructure.Services;

    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    //TODO : Move validation in difrent methods
    public class AppointmentService : IAppointmentService
    {
        private readonly IDeletableEntityRepository<Appointment> appointmentRepository;
        private readonly IDeletableEntityRepository<Patient> patientRepository;
        private readonly IDeletableEntityRepository<Doctor> doctorRepository;

        public AppointmentService(
            IDeletableEntityRepository<Appointment> appointmentRepository,
            IDeletableEntityRepository<Patient> patientRepository,
            IDeletableEntityRepository<Doctor> doctorRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.patientRepository = patientRepository;
            this.doctorRepository = doctorRepository;
        }
        //TODO Move Validation Out OF Method
        public async Task<Result> CreateAppointmentAsync(Appointment appointment, string patientAccountId)
        {
            var patientChek = await this.patientRepository.All()
                .FirstOrDefaultAsync(p => p.AccountId == patientAccountId);
            if (patientChek?.Id != appointment.PatientId
                || patientChek is null)
            {
                return "Problem With Patient";
            }

            await this.appointmentRepository.AddAsync(appointment);
            var result = await this.appointmentRepository.SaveChangesAsync();
            if (result == 0)
            {
                return "Ther was a problem adding the the record";
            }
            return true;

        }

        public async Task<Result> DeleteAppointmentAsync(int appointmentId, string doctorAccountId)
        {
            var doctor = await this.doctorRepository.All()
                .FirstOrDefaultAsync(d => d.AccountId == doctorAccountId);

            var appointment = await this.appointmentRepository.All()
                .FirstOrDefaultAsync(a => a.Id == appointmentId);

            if (doctor?.Id != appointment?.DoctorId || appointment is null || doctor is null)
            {
                return "you dont have premision";
            }

            this.appointmentRepository.Delete(appointment);
            var result = await this.appointmentRepository.SaveChangesAsync();
            if (result == 0)
            {
                return "Problem during deleting";
            }
            return true;
        }

        public async Task<IEnumerable<Appointment>> GetDoctorsAppointmetsAsync(string accountId)
        {
            var doctor = await this.doctorRepository.All()
                .FirstOrDefaultAsync(d => d.AccountId == accountId);
            if (doctor is null)
            {
                throw new ArgumentException(message:"Invalid AccountId");
            }
            var appointmets = await this.appointmentRepository.All()
                .Where(a => a.DoctorId == doctor.Id)
                .Select(a => new
                {
                    Appointment = new Appointment()
                    {
                        AppointmentStart = a.AppointmentStart,
                        Department = a.Department,
                        AppointmentEnd = a.AppointmentEnd,
                        Doctor = a.Doctor,
                        Patient = a.Patient,
                    },
                    Patient = a.Patient.FirstName,
                    Department = a.Department.Name,
                    Doctor = a.Doctor.FirstName,
                }).ToListAsync();

            return appointmets?.Select(a => a.Appointment);
        }

        public async Task<IEnumerable<Appointment>> GetPatientAppointAsync(string accountId)
        {
            var patient = await this.patientRepository.All()
                .FirstOrDefaultAsync(p => p.AccountId == accountId);

            if (patient is  null)
            {
                throw new ArgumentException(message: "Invalid AccountId");
            }
            var appointmets = await this.appointmentRepository.All()
                    .Where(a => a.PatientId == patient.Id)
                    .Select(a => new
                    {
                        Appointment = new Appointment()
                        {
                            AppointmentStart = a.AppointmentStart,
                            Department = a.Department,
                            AppointmentEnd = a.AppointmentEnd,
                            Doctor = a.Doctor,
                            Patient = a.Patient,
                            PatientId = a.PatientId,
                            DoctorId = a.DoctorId
                        },
                        Patient = a.Patient.FirstName,
                        Department = a.Department.Name,
                        Doctor = a.Doctor.FirstName,
                    }).ToListAsync();

            return appointmets.Select(a => a.Appointment);
        }

        public async Task<Result> UpdateAppointmentAsync(Appointment appointment, string doctorAccountId)
        {
            var doctor = await this.doctorRepository.All()
                .FirstOrDefaultAsync(d => d.AccountId == doctorAccountId);

            if (doctor is null)
            {
                return "Invalid doctorAccountId";
            }
            this.appointmentRepository.Update(appointment);
            var result = await this.appointmentRepository.SaveChangesAsync();
            if (result == 0)
            {
                return "Ther was a problem Updating the Entity";
            }
            return true;
        }
    }
}
