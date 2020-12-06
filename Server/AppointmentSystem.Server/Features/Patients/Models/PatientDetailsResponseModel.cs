namespace AppointmentSystem.Server.Features.Patients.Models
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Mapper;
    using AppointmentSystem.Server.Features.BaseFeatures.Models;

    public class PatientDetailsResponseModel :BaseResponseModel ,IMapFrom<Patient>
    {
        public int Id { get; set; }

        public string FistName { get; set; }
        public string SecondName { get; set; }

        public string SurName { get; set; }

        public string Address { get; set; }

        public string PIN { get; set; }

        public string CityName { get; set; }

        public int CityId { get; set; }
    }
}
