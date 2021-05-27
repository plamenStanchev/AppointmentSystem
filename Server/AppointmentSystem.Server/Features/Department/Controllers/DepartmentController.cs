namespace AppointmentSystem.Server.Features.Department.Controllers
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using AppointmentSystem.Core.Entities.Models;
	using AppointmentSystem.Core.Interfaces.Features;
	using AppointmentSystem.Infrastructure.Services;
	using AppointmentSystem.Server.Features.BaseFeatures.Controllers;
	using AppointmentSystem.Server.Features.Department.Models;
	using AutoMapper;
	using Microsoft.AspNetCore.Mvc;

	public class DepartmentController : ApiController
	{
		private readonly IDepartmentService departmentService;
		private readonly IMapper mapper;

		public DepartmentController(
			IDepartmentService departmentService,
			IMapper mapper)
		{
			this.departmentService = departmentService;
			this.mapper = mapper;
		}

		[HttpPost(nameof(Create))]
		public async Task<ActionResult<Result>> Create(DepartmentRequestModel departmentModel)
		{
			var department = this.mapper.Map<Department>(departmentModel);
			var result = await this.departmentService.CreateDepartmentAsync(department);
			return base.GenerateResultResponse(result);
		}

		[HttpGet(nameof(Delete))]
		public async Task<ActionResult<Result>> Delete(int departmentId)
		{
			var result = await this.departmentService.DeleteDepartmentAsync(departmentId);
			return base.GenerateResultResponse(result);
		}

		[HttpPost(nameof(Update))]
		public async Task<ActionResult<Result>> Update(DepartmentRequestModel departmentModel)
		{
			var department = this.mapper.Map<Department>(departmentModel);
			var result = await this.departmentService.UpdateDepartmentAsync(department);
			return base.GenerateResultResponse(result);
		}

		[HttpGet(nameof(Get))]
		public async Task<ActionResult<DepartmentDetailsResponseModel>> Get(int departmentId)
		{
			var department = await this.departmentService.GetDepartmentAsync(departmentId);
			var departmentDto = this.mapper.Map<DepartmentDetailsResponseModel>(department);
			return departmentDto;
		}

		[HttpGet(nameof(All))]
		public async Task<ActionResult<IEnumerable<DepartmentDetailsResponseModel>>> All()
		{
			var departments = await this.departmentService.GetAllDepartmentsAsync();
			var departmentsDto = departments.Select(d => this.mapper.Map<DepartmentDetailsResponseModel>(d)).ToList();

			return this.Ok(departmentsDto);
		}
	}
}
