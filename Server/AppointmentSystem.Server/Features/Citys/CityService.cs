namespace AppointmentSystem.Server.Features.Citys
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Core.Interfaces.Repository;
    using AppointmentSystem.Infrastructure.Services;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CityService : ICityService
    {
        private readonly IDeletableEntityRepository<City> repository;

        public CityService(IDeletableEntityRepository<City> repository)
        {
            this.repository = repository;
        }

        public async Task<Result> CreateCityAsync(City city)
        {
            await this.repository.AddAsync(city);
            var result = await this.repository.SaveChangesAsync();
            if (result == 0)
            {
                return "No record wher added";
            }
            return true;
        }

        public async Task<Result> CreateCityAsync(ICollection<City> cities)
        {
            foreach (var city in cities)
            {
                await this.repository.AddAsync(city);
            }

            await this.repository.SaveChangesAsync();
            return true;
        }

        public async Task<Result> DeleteCityAsync(City city)
        {
            this.repository.Delete(city);
            await this.repository.SaveChangesAsync();

            return true;
        }

        public async Task<City> GetCityAsync(int cityId)
            => await this.repository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == cityId);

        public async Task<IEnumerable<City>> GetAllCitiesAsync()
            => await this.repository.AllAsNoTracking()
                .ToListAsync();

        public async Task<Result> UpdateCityAsync(City city)
        {
            this.repository.Update(city);
            await this.repository.SaveChangesAsync();
            return true;
        }
    }
}
