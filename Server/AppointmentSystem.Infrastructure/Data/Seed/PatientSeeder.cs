namespace AppointmentSystem.Infrastructure.Data.Seed
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Infrastructure.Constants;
    using AppointmentSystem.Infrastructure.Data.Identity;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    internal class PatientSeeder : ISeeder
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
            if (dbContext.Patients.Any())
            {
                return;
            }
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            await this.SeedPatients(
                dbContext,
                userManager,
                "SomeShmukPatiient",
                "SoSecondName",
                "SMAKDMAI",
                "158cfb78-1376-4901-ab8c-270aaa7e5f03",
                2,
                "1444444444",
                "SomeADddersOFSomeShmuk");
            await this.SeedPatients(
                dbContext,
                userManager,
                "SomeShmukPatiient21",
                "SoSecondName32",
                "SMAKDMA131I",
                "8a1fd999-fba0-4661-9af8-2c1651f94ab3",
                1,
                "1444467544",
                "SomeAddres");
        }
        private async Task SeedPatients(
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            string firstName,
            string secondName,
            string surName,
            string accountId,
            int cityId,
            string PIN,
            string address)
        {
            await dbContext.Patients.AddAsync(new Patient()
            {
                FirstName = firstName,
                SecondName = secondName,
                SurName = surName,
                AccountId = accountId,
                CityId = cityId,
                PIN = PIN,
                Address = address
            });
            var user = await userManager.FindByIdAsync(accountId);
            await userManager.AddToRoleAsync(user, RolesNames.Patient);
        }
    }
}
