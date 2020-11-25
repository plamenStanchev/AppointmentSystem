namespace AppointmentSystem.Server.Features.Citys
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Infrastructure.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface ICityService
    {
        public Task<Result> CreateCityAsync(City city);

        public Task<Result> CreateCityAsync(ICollection<City> cities);

        public Task<Result> UpdateCityAsync(City city);

        public Task<Result> DeleteCityAsync(City city);

        public Task<City> GetCityAsync(int cityId);
    }
}
