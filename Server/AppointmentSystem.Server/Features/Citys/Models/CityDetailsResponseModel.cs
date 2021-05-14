namespace AppointmentSystem.Server.Features.Cities.Models
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Mapper;
    using AppointmentSystem.Server.Features.BaseFeatures.Models;

    public class CityDetailsResponseModel : BaseResponseModel, IMapFrom<City>
    {
        public string Name { get; set; }

        public int Id { get; set; }
    }
}
