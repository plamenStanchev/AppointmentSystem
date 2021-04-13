namespace AppointmentSystem.Server.Features.Doctors.Models
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Mapper;
    using AppointmentSystem.Server.Features.BaseFeatures.Models;
    using AutoMapper;

    public class DoctorDetailsResponseModel : BaseResponseModel, IMapFrom<Doctor>, IHaveCustomMappings
    {
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string SurName { get; set; }

        public string PIN { get; set; }

        public string CityName { get; set; }

        public string Department { get; set; }

        public string Description { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Doctor, DoctorDetailsResponseModel>()
                .ForMember(d => d.Department,
                    opt => opt.MapFrom(d => d.Department.Name));
        }
    }
}
