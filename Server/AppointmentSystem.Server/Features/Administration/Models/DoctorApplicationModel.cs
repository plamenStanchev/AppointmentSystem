namespace AppointmentSystem.Server.Features.Administration.Models
{
    using AppointmentSystem.Core.AdministrationDomain.Entety;
    using AppointmentSystem.Mapper;
    using System.ComponentModel.DataAnnotations;
    public class DoctorApplicationModel : IMapFrom<DoctorApplication>
    {
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string SecondName { get; set; }

        [Required]
        [MaxLength(30)]
        public string SurName { get; set; }

        [Required]
        [MaxLength(10)]
        public string PIN { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string AccountId { get; set; }
    }
}
