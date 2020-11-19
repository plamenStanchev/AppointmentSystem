namespace AssignmentSystem.Core.Entities.Models
{
    using AssignmentSystem.Core.Entities.Base;
    using System;
    public class Appointment : DeletableEntity
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        public Doctor Doctor { get; set; }

        public int PatientId { get; set; }

        public Patient Patient { get; set; }

        public int  DepartmentId { get; set; }

        public Department Department { get; set; }

        public DateTime AppointmentStart { get; set; }

        public DateTime AppointmentEnd { get; set; }
    }
}
