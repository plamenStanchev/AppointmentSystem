namespace AppointmentSystem.Server.Features.Department
{
    using AppointmentSystem.Infrastructure.Services;
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Core.Interfaces.Features;

    using System.Threading.Tasks;
    using System.Collections.Generic;
    using AppointmentSystem.Core.Interfaces.Repository;
    using Microsoft.EntityFrameworkCore;

    internal class DepartmentService : IDepartmentService
    {
        private readonly IDeletableEntityRepository<Department> repository;

        public DepartmentService(IDeletableEntityRepository<Department> repository)
        {
            this.repository = repository;
        }

        public async Task<Result> CreateDepartmentAsync(Department department)
        {
            await this.repository.AddAsync(department);

            return await this.SaveChangesAsync();
        }

        public async Task<Result> DeleteDepartmentAsync(int departmentId)
        {
            var departmentResult = await this.GetDepartmentAsync(departmentId);

            this.repository.Delete(departmentResult);

            return await this.SaveChangesAsync();
        }

        public async Task<Result> UpdateDepartmentAsync(Department department)
        {
            this.repository.Update(department);

            return await this.SaveChangesAsync();
        }

        public async Task<Department> GetDepartmentAsync(int departmentId)
            => await this.repository.All()
                .FirstOrDefaultAsync(d => d.Id == departmentId);

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
            => await this.repository.AllAsNoTracking()
                .ToListAsync();

        private async Task<Result> SaveChangesAsync()
            => await this.repository.SaveChangesAsync() switch
            {
                0 => "Problem",
                _ => true
            };
    }
}
