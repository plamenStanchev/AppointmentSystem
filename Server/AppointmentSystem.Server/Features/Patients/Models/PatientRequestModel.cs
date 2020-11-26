namespace AppointmentSystem.Server.Features.Patients.Models
{
    using System.ComponentModel.DataAnnotations;
    public class PatientRequestModel
    {
        [Required]
        public string FistName { get; set; }

        [Required]
        public string SecondName { get; set; }

        [Required]
        public string SurName { get; set; }

        public string Address { get; set; }

        [Required]
        public string PIN { get; set; }

        [Required]
        public int CityId { get; set; }
    }
}
