namespace AppointmentSystem.Infrastructure.Data.Seed
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Infrastructure.Constants;
    using AppointmentSystem.Infrastructure.Data.Identity;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    public class DoctorSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }
            if (dbContext.Doctors.Any())
            {
                return;
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            await this.SeedDoctor(dbContext,
                userManager,
                "SomeShmuk",
                "SomeShmukSecondName",
                "SomeSHmukSurName",
                "37a71afc-58dd-4a37-bbfc-77c954336a62",
                1,
                2,
                "121436299");
            await this.SeedDoctor(dbContext,
                userManager,
                "SomeShmuk2",
                "SomeShmukSecondName2",
                "SomeSHmukSurName2",
                "cae52c34-a121-432f-a8d9-e74d988e5812",
                2,
                3,
                "123456799");
        }
        private async Task SeedDoctor(
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            string firstName,
            string secondName,
            string surName,
            string accountId,
            int cityId,
            int departmentId,
            string PIN)
        {
            dbContext.Doctors.Add(new Doctor()
            {
                FirstName = firstName,
                SecondName = secondName,
                SurName = surName,
                AccountId = accountId,
                CityId = cityId,
                DepartmentId = departmentId,
                PIN = PIN
            });
            var user = await userManager.FindByIdAsync(accountId);
            await userManager.AddToRoleAsync(user, RolesNames.Doctor);
        }
    }
}
