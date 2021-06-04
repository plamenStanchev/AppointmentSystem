namespace AppointmentSystem.Server.Features.Cities.Controllers
{
	using System.Collections.Generic;
	using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
	using AppointmentSystem.Core.Entities.Models;
	using AppointmentSystem.Infrastructure.Services;
	using AppointmentSystem.Server.Features.BaseFeatures.Controllers;
	using AppointmentSystem.Server.Features.Cities.Models;
	using AutoMapper;
	using Microsoft.AspNetCore.Mvc;

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

		[HttpPost(nameof(Create))]
		public async Task<ActionResult<Result>> Create(CityRequestModel cityModel, CancellationToken cancellationToken = default)
		{
			var city = this.mapper.Map<City>(cityModel);
			var result = await this.cityService.CreateCityAsync(city, cancellationToken);
			return base.GenerateResultResponse(result);
		}

		[HttpGet(nameof(Delete))]
		public async Task<ActionResult<Result>> Delete(int cityId, CancellationToken cancellationToken = default)
		{
			var city = await this.cityService.GetCityAsync(cityId,cancellationToken);
			if (city is null)
			{
				return this.BadRequest($"Invalid {nameof(cityId)}");
			}
			var result = await this.cityService.DeleteCityAsync(city, cancellationToken);
			return base.GenerateResultResponse(result);
		}

		[HttpPost(nameof(Update))]
		public async Task<ActionResult<Result>> Update(CityRequestModel cityModel, int cityId, CancellationToken cancellationToken = default)
		{
			var city = this.mapper.Map<City>(cityModel);
			city.Id = cityId;
			var result = await this.cityService.UpdateCityAsync(city, cancellationToken);
			return base.GenerateResultResponse(result);
		}

		[HttpGet(nameof(Get))]
		public async Task<ActionResult<CityDetailsResponseModel>> Get(int cityId, CancellationToken cancellationToken = default)
		{
			var city = await this.cityService.GetCityAsync(cityId, cancellationToken);

			if (city is null)
			{
				return this.BadRequest("There isn't a city with this Id");
			}
			var cityDto = this.mapper.Map<CityDetailsResponseModel>(city);
			return this.Ok(cityDto);
		}

		[HttpGet(nameof(All))]
		public async Task<ActionResult<IEnumerable<CityDetailsResponseModel>>> All(CancellationToken cancellationToken = default)
		{
			var cities = await this.cityService.GetAllCitiesAsync(cancellationToken);
			var citiesDto = cities.Select(c => this.mapper.Map<CityDetailsResponseModel>(c)).ToList();
			return this.Ok(citiesDto);
		}
	}
}
