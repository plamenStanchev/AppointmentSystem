namespace AppointmentSystem.Core.Entities.Models
{
    using AppointmentSystem.Core.Entities.Base;
    using System.Collections.Generic;

    public class Patient : DeletableEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public string SurName { get; set; }

        public string Address { get; set; }

        public string PIN { get; set; }

        public City City { get; set; }

        public int CityId { get; set; }

        public string AccountId { get; set; }

        public IReadOnlyCollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
    }
}
