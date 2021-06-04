namespace AppointmentSystem.Server.Features.Department.Controllers
{
	using System.Collections.Generic;
	using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
	using AppointmentSystem.Core.Entities.Models;
	using AppointmentSystem.Core.Interfaces.Features;
	using AppointmentSystem.Infrastructure.Constants;
	using AppointmentSystem.Infrastructure.Services;
	using AppointmentSystem.Server.Attributes;
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

		[Roles(RolesNames.Admin)]
		[HttpPost(nameof(Create))]
		public async Task<ActionResult<Result>> Create(DepartmentRequestModel departmentModel, CancellationToken cancellationToken = default)
		{
			var department = this.mapper.Map<Department>(departmentModel);
			var result = await this.departmentService.CreateDepartmentAsync(department, cancellationToken);
			return base.GenerateResultResponse(result);
		}

		[Roles(RolesNames.Admin)]
		[HttpGet(nameof(Delete))]
		public async Task<ActionResult<Result>> Delete(int departmentId, CancellationToken cancellationToken = default)
		{
			var result = await this.departmentService.DeleteDepartmentAsync(departmentId, cancellationToken);
			return base.GenerateResultResponse(result);
		}

		[Roles(RolesNames.Admin)]
		[HttpPost(nameof(Update))]
		public async Task<ActionResult<Result>> Update(DepartmentRequestModel departmentModel, CancellationToken cancellationToken = default)
		{
			var department = this.mapper.Map<Department>(departmentModel);
			var result = await this.departmentService.UpdateDepartmentAsync(department,cancellationToken);
			return base.GenerateResultResponse(result);
		}

		[HttpGet(nameof(Get))]
		public async Task<ActionResult<DepartmentDetailsResponseModel>> Get(int departmentId, CancellationToken cancellationToken = default)
		{
			var department = await this.departmentService.GetDepartmentAsync(departmentId, cancellationToken);
			var departmentDto = this.mapper.Map<DepartmentDetailsResponseModel>(department);
			return departmentDto;
		}

		[HttpGet(nameof(All))]
		public async Task<ActionResult<IEnumerable<DepartmentDetailsResponseModel>>> All(CancellationToken cancellationToken = default)
		{
			var departments = await this.departmentService.GetAllDepartmentsAsync(cancellationToken);
			var departmentsDto = departments.Select(d => this.mapper.Map<DepartmentDetailsResponseModel>(d)).ToList();

			return this.Ok(departmentsDto);
		}
	}
}
