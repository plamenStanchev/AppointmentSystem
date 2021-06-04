namespace AppointmentSystem.Server.Features.Cities
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Core.Interfaces.Repository;
    using AppointmentSystem.Infrastructure.Services;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    internal class CityService : ICityService
    {
        private readonly IDeletableEntityRepository<City> repository;

        public CityService(IDeletableEntityRepository<City> repository, CancellationToken cancellationToken = default)
        {
            this.repository = repository;
        }

        public async Task<Result> CreateCityAsync(City city, CancellationToken cancellationToken = default)
        {
            await this.repository.AddAsync(city);

            return await this.repository.SaveChangesAsync(cancellationToken) switch
            {
                0 => "No record where added",
                _ => true
            };
        }

        public async Task<Result> CreateCityAsync(ICollection<City> cities, CancellationToken cancellationToken = default)
        {
            foreach (var city in cities)
            {
                await this.repository.AddAsync(city);
            }

            return await this.repository.SaveChangesAsync(cancellationToken) != default;
        }

        public async Task<Result> DeleteCityAsync(City city, CancellationToken cancellationToken = default)
        {
            this.repository.Delete(city);

            return await this.repository.SaveChangesAsync(cancellationToken) != default;
        }

        public async Task<City> GetCityAsync(int cityId, CancellationToken cancellationToken = default)
            => await this.repository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == cityId);

        public async Task<IEnumerable<City>> GetAllCitiesAsync(CancellationToken cancellationToken = default)
            => await this.repository.AllAsNoTracking()
                .ToListAsync(cancellationToken);

        public async Task<Result> UpdateCityAsync(City city, CancellationToken cancellationToken = default)
        {
            this.repository.Update(city);

            return await this.repository.SaveChangesAsync(cancellationToken) != default;
        }
    }
}
