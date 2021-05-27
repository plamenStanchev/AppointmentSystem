namespace AppointmentSystem.Server.Features.BaseFeatures.Controllers
{
	using AppointmentSystem.Infrastructure.Services;
	using AppointmentSystem.Server.Filters;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize]
	[ApiController]
	[ApiResponseFilter]
	[Route("api/[controller]")]
	public abstract class ApiController : ControllerBase
	{
		protected const string Comma = ",";

		protected virtual ActionResult<Result> GenerateResultResponse(Result result)
			=> result.Succeeded switch
			{
				true => this.Ok(result),
				_ => this.BadRequest(result.Error)
			};
	}
}
