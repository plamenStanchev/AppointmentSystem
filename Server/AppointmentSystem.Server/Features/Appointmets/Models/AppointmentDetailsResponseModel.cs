namespace AppointmentSystem.Server.Features.Appointmets.Models
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Mapper;
    using AppointmentSystem.Server.Features.BaseFeatures.Models;
    using AutoMapper;
    using System;
    public class AppointmentDetailsResponseModel : BaseResponseModel, IMapFrom<Appointment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        public string DoctorName { get; set; }

        public int PatientId { get; set; }

        public string  PatientName { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public DateTime AppointmentStart { get; set; }

        public DateTime? AppointmentEnd { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Appointment, AppointmentDetailsResponseModel>()
                .ForMember(d => d.DepartmentName,
                opt => opt.MapFrom(x => x.Department.Name));

            configuration.CreateMap<Appointment, AppointmentDetailsResponseModel>()
                .ForMember(d => d.DoctorName,
                opt => opt.MapFrom(x => x.Doctor.FirstName + " " + x.Doctor.SurName));

            configuration.CreateMap<Appointment, AppointmentDetailsResponseModel>()
                .ForMember(p => p.PatientName,
                opt => opt.MapFrom(x => x.Patient.FirstName + " " + x.Patient.SurName));
        }
    }
}
