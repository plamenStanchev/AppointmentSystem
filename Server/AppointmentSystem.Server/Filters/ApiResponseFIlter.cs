namespace AppointmentSystem.Server.Filters
{
    using AppointmentSystem.Server.Features.BaseFeatures.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Filters;

    internal class ApiResponseFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Result is ObjectResult)
            {
                var response = filterContext.Result as ObjectResult;

                response.Value = response switch
                {
                    BadRequestObjectResult => response.Value.ToApiError((filterContext.ActionDescriptor as ControllerActionDescriptor).ActionName),
                    _ => response.Value.ToApiResponse(),
                };

            }
        }
    }
}