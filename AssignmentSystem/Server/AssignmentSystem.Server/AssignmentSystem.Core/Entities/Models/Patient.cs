namespace AssignmentSystem.Core.Entities.Models
{
    using AssignmentSystem.Core.Entities.Base;
    using System.Collections.Generic;
    public class Patient : DeletableEntity
    {
        public int Id { get; set; }

        public string  FistName { get; set; }

        public string SurName { get; set; }

        public string  Address { get; set; }

        public City City { get; set; }

        public int CityId { get; set; }

        public string AccountId { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
    }
}
