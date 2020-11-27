namespace AppointmentSystem.Infrastructure.Data.Seed
{
    using AppointmentSystem.Core.Entities.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    internal class CitySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext,IServiceProvider serviceProvider)
        {

            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            if (dbContext.Cities.Any())
            {
                return;
            }
            await this.SeedCitys(dbContext,"Burgas");
            await this.SeedCitys(dbContext,"Sofia");
            await this.SeedCitys(dbContext,"SomeShmukCity");
        }

        private async Task SeedCitys(ApplicationDbContext dbContext,string name)
        {
           await dbContext.Cities.AddAsync(new City() { Name = name });
        }
    }
}
