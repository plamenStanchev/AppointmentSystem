namespace AppointmentSystem.Server.Features.Department.Controllers
{
    using AppointmentSystem.Core.Entities.Models;
    using AppointmentSystem.Core.Interfaces.Features;
    using AppointmentSystem.Server.Features.BaseFeatures.Controllers;
    using AppointmentSystem.Server.Features.Department.Models;
    using AppointmentSystem.Infrastructure.Services;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Collections.Generic;

    public class DepartmentController : ApiController
    {
        private readonly IDepartmentService departmentService;
        private readonly IMapper mapepr;

        public DepartmentController(
            IDepartmentService departmentService,
            IMapper mapepr)
        {
            this.departmentService = departmentService;
            this.mapepr = mapepr;
        }

        [HttpPost(nameof(Create))]
        public async Task<ActionResult<Result>> Create(DepartmentRequestModel departmentModel)
        {
            var department = this.mapepr.Map<Department>(departmentModel);
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
            var department = this.mapepr.Map<Department>(departmentModel);
            var result = await this.departmentService.UpdateDepartmentAsync(department);
            return base.GenerateResultResponse(result);
        }

        [HttpGet(nameof(Get))]
        public async Task<ActionResult<DepartmentDetailsResponseModel>> Get(int departmentId)
        {
            var department = await this.departmentService.GetDepartmentAsync(departmentId);
            var departmentDto = this.mapepr.Map<DepartmentDetailsResponseModel>(department);
            departmentDto.Succeeded = true;
            return departmentDto;
        }

        [HttpGet(nameof(All))]
        public async Task<ActionResult<IEnumerable<DepartmentDetailsResponseModel>>> All()
        {
            var departments = await this.departmentService.GetAllDepartmentsAsync();
            var departmentsDto = departments.Select(d => this.mapepr.Map<DepartmentDetailsResponseModel>(d)).ToList();
            departmentsDto.ForEach(d => d.Succeeded = true);

            return this.Ok(departmentsDto);
        }
    }
}
