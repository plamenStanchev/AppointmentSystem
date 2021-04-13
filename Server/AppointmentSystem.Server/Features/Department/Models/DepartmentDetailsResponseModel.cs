namespace AppointmentSystem.Server.Features.Department.Models
{
    using AppointmentSystem.Mapper;
    using AppointmentSystem.Server.Features.BaseFeatures.Models;
    using AppointmentSystem.Core.Entities.Models;

    public class DepartmentDetailsResponseModel : BaseResponseModel, IMapFrom<Department>
    {
        public string Name { get; set; }

        public int Id { get; set; }
    }
}
