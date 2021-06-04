namespace AppointmentSystem.Server.Features.BaseFeatures.Controllers
{
	using System;
	using System.Threading.Tasks;
	using AppointmentSystem.Infrastructure.Constants;
	using AppointmentSystem.Infrastructure.Data.Identity;
	using Microsoft.AspNetCore.Identity;

	public abstract class ApiAccountController : ApiController
	{
		protected readonly UserManager<ApplicationUser> userManager;

		protected ApiAccountController(
			UserManager<ApplicationUser> userManager)
		{
			this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
		}

		protected async Task<bool> ValidateAccountId(string accountId)
		{
			if (this.User.IsInRole(RolesNames.Admin))
			{
				return true;
			}

			var user = await this.userManager.GetUserAsync(this.User);

			return user?.Id == accountId;
		}

		protected async Task<bool> ValidateExistAccount(string accountId)
		{
			var user = await this.userManager.FindByIdAsync(accountId);

			return user?.Id == accountId;
		}
	}
}
