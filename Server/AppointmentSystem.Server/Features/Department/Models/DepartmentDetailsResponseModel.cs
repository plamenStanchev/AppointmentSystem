namespace AppointmentSystem.Server.Features.Department.Models
{
	using AppointmentSystem.Core.Entities.Models;
	using AppointmentSystem.Mapper;

	public class DepartmentDetailsResponseModel : IMapFrom<Department>
	{
		public string Name { get; set; }

		public int Id { get; set; }
	}
}
