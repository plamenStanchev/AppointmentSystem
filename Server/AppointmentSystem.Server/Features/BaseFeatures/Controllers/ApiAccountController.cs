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

        protected async Task<bool> ValidaiteAccountId(string accountId)
        {
            var curentUser = await this.userManager.GetUserAsync(this.User);
            if (curentUser is null || curentUser?.Id != accountId)
            {
                return false;
            }
            return true;
        }
    }
}
