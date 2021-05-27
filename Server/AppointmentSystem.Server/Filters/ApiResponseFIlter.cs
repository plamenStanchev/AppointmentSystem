namespace AppointmentSystem.Server.Filters
{
	using AppointmentSystem.Server.Features.BaseFeatures.Models;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Filters;

	internal class ApiResponseFilter : ActionFilterAttribute
	{
		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			if (filterContext.Result is ObjectResult)
			{
				var response = filterContext.Result as ObjectResult;
				response.Value = response.Value.ToApiResponse();
			}
		}
	}
}