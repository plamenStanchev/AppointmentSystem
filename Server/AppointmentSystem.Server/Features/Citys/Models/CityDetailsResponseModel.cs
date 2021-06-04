namespace AppointmentSystem.Server.Features.Cities.Models
{
	using AppointmentSystem.Core.Entities.Models;
	using AppointmentSystem.Mapper;

	public class CityDetailsResponseModel : IMapFrom<City>
	{
		public string Name { get; set; }

		public int Id { get; set; }
	}
}
