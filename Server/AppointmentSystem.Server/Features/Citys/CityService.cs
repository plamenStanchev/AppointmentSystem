namespace AppointmentSystem.Server.Features.Cities
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Core.Interfaces.Repository;
    using AppointmentSystem.Infrastructure.Services;
    using AppointmentSystem.Server.ApplicationOptions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    internal class CityService : ICityService
    {
        private readonly IDeletableEntityRepository<City> repository;
        private readonly IMemoryCache cache;
        private readonly CacheKeys cacheKeys;

        public CityService(
            IDeletableEntityRepository<City> repository,
            CacheKeys cacheKeys,
            IMemoryCache cache)
        {
            this.repository = repository;
            this.cacheKeys = cacheKeys;
            this.cache = cache;
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

            var savachangesResult = await this.repository.SaveChangesAsync(cancellationToken) != default;
            if (savachangesResult)
            {
                await this.PopulateCache(cancellationToken);
            }
            return savachangesResult;
        }

        public async Task<Result> DeleteCityAsync(City city, CancellationToken cancellationToken = default)
        {
            this.repository.Delete(city);

            var savachangesResult = await this.repository.SaveChangesAsync(cancellationToken) != default;
            if (savachangesResult)
            {
                await this.PopulateCache(cancellationToken);
            }
            return savachangesResult;
        }

        public async Task<City> GetCityAsync(int cityId, CancellationToken cancellationToken = default)
            => await this.repository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == cityId);

        public async Task<IEnumerable<City>> GetAllCitiesAsync(CancellationToken cancellationToken = default)
        {
            
            return await this.cache.GetOrCreateAsync<IEnumerable<City>>(this.cacheKeys.GetAllCitys, async x =>
            {
                return await this.repository.AllAsNoTracking()
                .ToListAsync(cancellationToken);
            });
        }


        public async Task<Result> UpdateCityAsync(City city, CancellationToken cancellationToken = default)
        {
            this.repository.Update(city);
            var savachangesResult = await this.repository.SaveChangesAsync(cancellationToken) != default;
            if (savachangesResult)
            {
                await this.PopulateCache(cancellationToken);
            }
            return savachangesResult;
        }
        private async Task PopulateCache(CancellationToken cancellationToken = default)
        {
           var cities = await this.repository.AllAsNoTracking()
                .ToListAsync(cancellationToken);
            this.cache.Set(this.cacheKeys.GetAllCitys, cities);
        }
    }
}
