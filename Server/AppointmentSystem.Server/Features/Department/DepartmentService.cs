namespace AppointmentSystem.Server.Features.Department
{
    using AppointmentSystem.Infrastructure.Services;
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Core.Interfaces.Features;

    using System.Threading.Tasks;
    using AppointmentSystem.Core.Interfaces.Repository;
    using Microsoft.EntityFrameworkCore;

    public class DepartmentService : IDepartmentService
    {
        private readonly IDeletableEntityRepository<Department> repository;

        public DepartmentService(IDeletableEntityRepository<Department> repository)
        {
            this.repository = repository;
        }

        public async Task<Result> CreateDepartmentAsync(Department department)
        {
            await this.repository.AddAsync(department);
            var resuslt = await this.repository.SaveChangesAsync();
            if (resuslt == 0)
            {
                return "Problem";
            }
            return true;
        }

        public async Task<Result> DeleteDepartmentAsync(int departmentId)
        {
            var department = new Department()
            {
                Id = departmentId
            };
            this.repository.Delete(department);
            await this.repository.SaveChangesAsync();
            return true;
        }

        public async Task<Department> GetDepartmentAsync(int departmentId)
            => await this.repository.All()
                .FirstOrDefaultAsync(d => d.Id == departmentId);

        public async Task<Result> UpdateDepartmentAsync(Department department)
        {
            this.repository.Update(department);
            var result = await this.repository.SaveChangesAsync();
            if (result == 0)
            {
                return "Problem";
            }
            return true;
        }
    }
}
