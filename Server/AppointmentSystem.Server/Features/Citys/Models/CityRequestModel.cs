namespace AppointmentSystem.Server.Features.Citys.Models
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Mapper;
    public class CityRequestModel : IMapTo<City>
    {
        public string Name { get; set; }
    }
}
