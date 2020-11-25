namespace AppointmentSystem.Core.Interfaces.Features
{
    using AppointmentSystem.Infrastructure.Services;
    using System.Threading.Tasks;
    using AppointmentSystem.Core.Entities.Models;

    public interface IDepartmentService
    {
        public Task<Result> CreateDepartmentAsync(Department department);

        public Task<Result> DeleteDepartmentAsync(int departmentId);

        public Task<Result> UpdateDepartmentAsync(Department department);

        public Task<Department> GetDepartmentAsync(int departmentId);
    }
}
