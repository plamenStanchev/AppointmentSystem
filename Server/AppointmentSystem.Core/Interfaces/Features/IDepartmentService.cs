namespace AppointmentSystem.Core.Interfaces.Features
{
    using AppointmentSystem.Infrastructure.Services;
    using System.Threading.Tasks;
    using AppointmentSystem.Core.Entities.Models;
    using System.Collections.Generic;
    using System.Threading;

    public interface IDepartmentService
    {
        public Task<Result> CreateDepartmentAsync(Department department, CancellationToken cancellationToken);

        public Task<Result> DeleteDepartmentAsync(int departmentId, CancellationToken cancellationToken);

        public Task<Result> UpdateDepartmentAsync(Department department, CancellationToken cancellationToken);

        public Task<Department> GetDepartmentAsync(int departmentId, CancellationToken cancellationToken);

        public Task<IEnumerable<Department>> GetAllDepartmentsAsync( CancellationToken cancellationToken );
    }
}
