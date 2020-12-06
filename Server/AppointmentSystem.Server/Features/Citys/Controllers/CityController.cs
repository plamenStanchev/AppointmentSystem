namespace AppointmentSystem.Server.Features.Citys.Controllers
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Infrastructure.Services;
    using AppointmentSystem.Server.Features.BaseFeatures.Controllers;
    using AppointmentSystem.Server.Features.Citys.Models;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CityController : ApiController
    {
        private readonly ICityService cityService;
        private readonly IMapper mapper;

        public CityController(
            ICityService cityService,
            IMapper mapper)
        {
            this.cityService = cityService;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route(nameof(CreateCity))]
        public async Task<ActionResult<Result>> CreateCity(CityRequestModel cityModel)
        {
            var city = this.mapper.Map<City>(cityModel);
            var result = await this.cityService.CreateCityAsync(city);
            return base.GenerateResultResponse(result);
        }
        [HttpGet]
        [Route(nameof(DeleteCity))]
        public async Task<ActionResult<Result>> DeleteCity(int cityId)
        {
            var city = await this.cityService.GetCityAsync(cityId);
            if (city is null)
            {
                return this.BadRequest("Invalid cityId");
            }
            var result = await this.cityService.DeleteCityAsync(city);
            return base.GenerateResultResponse(result);
        }

        [HttpPost]
        [Route(nameof(UpdateCity))]
        public async Task<ActionResult<Result>> UpdateCity(CityRequestModel cityModel,int cityId)
        {
            var city = this.mapper.Map<City>(cityModel);
            city.Id = cityId;
            var result = await this.cityService.UpdateCityAsync(city);
            return base.GenerateResultResponse(result);
        }

        [HttpGet]
        [Route(nameof(GetCity))]
        public async Task<ActionResult<CityDetailsResponseModel>> GetCity(int cityId)
        {
            var city = await this.cityService.GetCityAsync(cityId);
            
            if (city is null)
            {
                return this.BadRequest(new CityDetailsResponseModel() { ErrorMesage = "Ther isnt a city with this Id" });
            }
            var cityDto = this.mapper.Map<CityDetailsResponseModel>(city);
            cityDto.Succeeded = true;
            return this.Ok(cityDto);
        }

        [HttpPost]
        [Route(nameof(GetAllCities))]
        public async Task<ActionResult<IEnumerable<CityDetailsResponseModel>>> GetAllCities()
        {
            var cities = await this.cityService.GetAllCitiesAsync();
            var citiesDto = cities.Select(c => this.mapper.Map<CityDetailsResponseModel>(c)).Select(c => c.Succeeded);
            return this.Ok(citiesDto);
        }
    }
}
