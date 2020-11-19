﻿namespace AssignmentSystem.Infrastructure.Data.Seed
{
    using AssignmentSystem.Infrastructure.Data.Identity;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    internal class RoleSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            await SeedRoleAsync(roleManager, Constants.RolesNames.AdministratorRoleName);
            await SeedRoleAsync(roleManager, Constants.RolesNames.DoctorRoleName);
            await SeedRoleAsync(roleManager, Constants.RolesNames.PatientRoleName);
        }

        private async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
