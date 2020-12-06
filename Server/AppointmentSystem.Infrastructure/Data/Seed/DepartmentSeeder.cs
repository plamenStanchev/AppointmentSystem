namespace AppointmentSystem.Infrastructure.Data.Seed
{
    using AppointmentSystem.Core.Entities.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    internal class DepartmentSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            if (dbContext.Departments.Any())
            {
                return;
            }
            await this.SeedDepartment(dbContext, "Surgical");
            await this.SeedDepartment(dbContext, "Respiratory");
            await this.SeedDepartment(dbContext, "Radiology");
            await this.SeedDepartment(dbContext, "Physical Therapy"); 
            await this.SeedDepartment(dbContext, "Rehabilitation");
        }
        private async Task SeedDepartment(ApplicationDbContext dbContext,string name)
        {
            await dbContext.Departments.AddAsync(new Department() { Name = name });
        }
    }
}
