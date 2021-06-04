namespace AppointmentSystem.Server.Features.Department
{
    using AppointmentSystem.Infrastructure.Services;
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Core.Interfaces.Features;

    using System.Threading.Tasks;
    using System.Collections.Generic;
    using AppointmentSystem.Core.Interfaces.Repository;
    using Microsoft.EntityFrameworkCore;
    using System.Threading;

    internal class DepartmentService : IDepartmentService
    {
        private readonly IDeletableEntityRepository<Department> repository;

        public DepartmentService(IDeletableEntityRepository<Department> repository, CancellationToken cancellationToken = default)
        {
            this.repository = repository;
        }

        public async Task<Result> CreateDepartmentAsync(Department department, CancellationToken cancellationToken = default)
        {
            await this.repository.AddAsync(department);

            return await this.SaveChangesAsync(cancellationToken);
        }

        public async Task<Result> DeleteDepartmentAsync(int departmentId, CancellationToken cancellationToken = default)
        {
            var departmentResult = await this.GetDepartmentAsync(departmentId, cancellationToken);

            this.repository.Delete(departmentResult);

            return await this.SaveChangesAsync(cancellationToken);
        }

        public async Task<Result> UpdateDepartmentAsync(Department department, CancellationToken cancellationToken = default)
        {
            this.repository.Update(department);

            return await this.SaveChangesAsync(cancellationToken);
        }

        public async Task<Department> GetDepartmentAsync(int departmentId, CancellationToken cancellationToken = default)
            => await this.repository.All()
                .FirstOrDefaultAsync(d => d.Id == departmentId, cancellationToken);

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync(CancellationToken cancellationToken = default)
            => await this.repository.AllAsNoTracking()
                .ToListAsync(cancellationToken);

        private async Task<Result> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await this.repository.SaveChangesAsync(cancellationToken) switch
            {
                0 => "Problem",
                _ => true
            };
    }
}
