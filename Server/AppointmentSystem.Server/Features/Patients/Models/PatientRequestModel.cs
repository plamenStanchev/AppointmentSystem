namespace AppointmentSystem.Server.Features.Patients.Models
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Mapper;
    using System.ComponentModel.DataAnnotations;
    public class PatientRequestModel : IMapTo<Patient>
    {
        [Required]
        [MaxLength(30)]
        public string FistName { get; set; }

        [Required]
        [MaxLength(30)]
        public string SecondName { get; set; }

        [Required]
        [MaxLength(30)]
        public string SurName { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [Required]
        [MaxLength(10)]
        public string PIN { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        public string AccountId { get; set; }
    }
}
