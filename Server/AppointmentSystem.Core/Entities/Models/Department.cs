namespace AppointmentSystem.Core.Entities.Models
{
    using AppointmentSystem.Core.Entities.Base;
    using System.Collections.Generic;

    public class Department : DeletableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IReadOnlyCollection<Doctor> Doctors { get; set; }
            = new HashSet<Doctor>();

        public IReadOnlyCollection<Appointment> Appointments { get; set; }
            = new HashSet<Appointment>();
    }
}
