namespace AppointmentSystem.Server.Features.Citys
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Core.Interfaces.Repository;
    using AppointmentSystem.Infrastructure.Services;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class CityService : ICityService
    {
        private readonly IDeletableEntityRepository<City> repository;

        public CityService(IDeletableEntityRepository<City> repository)
        {
            this.repository = repository;
        }

        public async Task<Result> CreateCityAsync(City city)
        {
            await this.repository.AddAsync(city);

            return await this.repository.SaveChangesAsync() switch
            {
                0 => "No record wher added",
                _ => true
            };
        }

        public async Task<Result> CreateCityAsync(ICollection<City> cities)
        {
            foreach (var city in cities)
            {
                await this.repository.AddAsync(city);
            }

            return await this.repository.SaveChangesAsync() != default;
        }

        public async Task<Result> DeleteCityAsync(City city)
        {
            this.repository.Delete(city);

            return await this.repository.SaveChangesAsync() != default;
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

            return await this.repository.SaveChangesAsync() != default;
        }
    }
}
