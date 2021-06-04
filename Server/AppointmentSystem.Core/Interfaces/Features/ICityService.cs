namespace AppointmentSystem.Server.Features.Cities
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Infrastructure.Services;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ICityService
    {
        public Task<Result> CreateCityAsync(City city, CancellationToken cancellationToken);

        public Task<Result> CreateCityAsync(ICollection<City> cities, CancellationToken cancellationToken);

        public Task<Result> UpdateCityAsync(City city, CancellationToken cancellationToken);

        public Task<Result> DeleteCityAsync(City city, CancellationToken cancellationToken);

        public Task<IEnumerable<City>> GetAllCitiesAsync(CancellationToken cancellationToken);

        public Task<City> GetCityAsync(int cityId, CancellationToken cancellationToken);
    }
}
