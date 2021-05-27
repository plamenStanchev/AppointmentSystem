namespace AppointmentSystem.Server.Features.BaseFeatures.Controllers
{
    using AppointmentSystem.Infrastructure.Data.Identity;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public abstract class ApiAccountController : ApiController
    {
        protected readonly UserManager<ApplicationUser> userManager;

        protected ApiAccountController(
            UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        protected async Task<bool> ValidateAccountId(string accountId)
        {
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
