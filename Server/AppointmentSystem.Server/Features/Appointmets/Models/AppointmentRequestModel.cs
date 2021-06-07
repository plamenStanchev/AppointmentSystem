namespace AppointmentSystem.Server.Features.Appointments.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Mapper;

    public class AppointmentRequestModel : IMapTo<Appointment>
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public DateTime AppointmentStart { get; set; }

        public DateTime? AppointmentEnd { get; set; }
    }
}
