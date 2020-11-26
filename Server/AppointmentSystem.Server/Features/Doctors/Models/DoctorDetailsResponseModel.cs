namespace AppointmentSystem.Server.Features.Doctors.Models
{
    using AppointmentSystem.Server.Features.BaseFeatures.Models;
    public class DoctorDetailsResponseModel : BaseResponseModel
    {
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string SurName { get; set; }

        public string PIN { get; set; }

        public string CityName { get; set; }

        public string Department { get; set; }

        public string Description { get; set; }
    }
}
