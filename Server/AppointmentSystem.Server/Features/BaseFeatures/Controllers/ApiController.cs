namespace AppointmentSystem.Server.Features.BaseFeatures.Controllers
{
    using AppointmentSystem.Infrastructure.Services;
    using AppointmentSystem.Server.Filters;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    [Authorize]
    [ApiController]
    [ApiResponseFilter]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
        protected const string Comma = ",";

        protected IMapper UseMapper => HttpContext.RequestServices.GetRequiredService<IMapper>();

        protected virtual ActionResult<Result> GenerateResultResponse(Result result)
            => result.Succeeded switch
            {
                true => this.Ok(result),
                _ => this.BadRequest(result.Error)
            };
    }
}
