namespace AppointmentSystem.Core.Interfaces.Features
{
    using AppointmentSystem.Infrastructure.Services;
    using System.Threading.Tasks;
    using AppointmentSystem.Core.Entities.Models;
    using System.Collections.Generic;

    public interface IDepartmentService
    {
        public Task<Result> CreateDepartmentAsync(Department department);

        public Task<Result> DeleteDepartmentAsync(int departmentId);

        public Task<Result> UpdateDepartmentAsync(Department department);

        public Task<Department> GetDepartmentAsync(int departmentId);

        public Task<IEnumerable<Department>> GetAllDepartmentsAsync();
    }
}
