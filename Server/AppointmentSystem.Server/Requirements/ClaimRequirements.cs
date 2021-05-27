namespace AppointmentSystem.Server.Requirements
{
	using System.Linq;
	using System.Threading.Tasks;
	using AppointmentSystem.Infrastructure.Constants;
	using Microsoft.AspNetCore.Authorization;

	public class ClaimRequirementsHandler : IAuthorizationHandler
	{
		public Task HandleAsync(AuthorizationHandlerContext context)
		{
			if (context.User.IsInRole(RolesNames.Admin))
			{
				while (context.PendingRequirements.Any())
				{
					context.Succeed(context.PendingRequirements.First());
				}
			}

			return Task.CompletedTask;
		}
	}
}