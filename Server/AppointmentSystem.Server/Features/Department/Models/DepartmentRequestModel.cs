namespace AppointmentSystem.Server.Features.Department.Models
{
    using AppointmentSystem.Mapper;
    using AppointmentSystem.Core.Entities.Models;
    public class DepartmentRequestModel : IMapTo<Department>
    {
        public string Name { get; set; }
    }
}
