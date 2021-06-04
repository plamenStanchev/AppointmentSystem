namespace AppointmentSystem.Infrastructure.Data.Seed
{
    using AppointmentSystem.Infrastructure.Data.Identity;
    using AppointmentSystem.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    internal class UserSeed : ISeeder
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
            if (dbContext.Users.Any())
            {
                return;
            }
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await this.SeedApplicationUser(userManager, "SomeUserShmuk", "123456", "SomeEmail@gmail.com", "37a71afc-58dd-4a37-bbfc-77c954336a62");
            await this.SeedApplicationUser(userManager, "SomeUserShmuk2", "1234561", "SomeEm1ail@abv.bg", "cae52c34-a121-432f-a8d9-e74d988e5812");
            await this.SeedApplicationUser(userManager, "SomeUserShmuk23", "1234561as", "SomeShmukEmail@abv.bg", "158cfb78-1376-4901-ab8c-270aaa7e5f03");
            await this.SeedApplicationUser(userManager, "SomeUserShuk2", "1234561", "SomeEma31il@abv.bg", "8a1fd999-fba0-4661-9af8-2c1651f94ab3");
        }
        private async Task SeedApplicationUser(
            UserManager<ApplicationUser> userManager,
            string userName,
            string password,
            string email,
            string id)
        {
            var result = await userManager.CreateAsync(new ApplicationUser() { Email = email, UserName = userName, Id = id }, password);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(Environment.NewLine, result.GetError()));
            }
        }
    }
}
