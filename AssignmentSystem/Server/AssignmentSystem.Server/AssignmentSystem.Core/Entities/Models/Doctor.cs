namespace AssignmentSystem.Core.Entities.Models
{
    using AssignmentSystem.Core.Entities.Base;
    using System.Collections.Generic;
    public class Doctor : DeletableEntity
    {
        public int Id { get; set; }

        public string FistName { get; set; }

        public string SecondName { get; set; }

        public string SurName { get; set; }


        public string AccountId { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public string Description { get; set; }

        public Department Department { get; set; }

        public int DepartmentId { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
    }
}
