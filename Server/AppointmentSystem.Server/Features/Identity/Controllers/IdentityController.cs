namespace AppointmentSystem.Server.Features.Identity.Controllers
{
    using AppointmentSystem.Core.Interfaces.Features;
    using AppointmentSystem.Infrastructure.Data.Identity;
    using AppointmentSystem.Server.Features.BaseFeatures.Controllers;
    using AppointmentSystem.Server.Features.Identity.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using System.Linq;
    using System.Threading.Tasks;

    public class IdentityController : ApiController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;
        private readonly IOptions<AppSettings> appSettings;
        private readonly SignInManager<ApplicationUser> signInManager;

        public IdentityController(
            UserManager<ApplicationUser> userManager,
            IUserService userService,
            IOptions<AppSettings> appSettings,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.appSettings = appSettings;
            this.signInManager = signInManager;
        }

        [HttpPost(nameof(Register))]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponseModel>> Register([FromBody] RegisterRequestModel model)
        {
            var user = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.UserName,
            };

            var createUserresult = await userManager.CreateAsync(user, model.Password);
            if (!createUserresult.Succeeded)
            {
                return BadRequest(createUserresult.Errors);
            }

            user = await userManager.FindByEmailAsync(model.Email);

            var signInresult = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!signInresult.Succeeded)
            {
                return Problem();
            }
            var token = await userService.GetTokenAsync(appSettings.Value.Secret, user.Id);

            return new LoginResponseModel
            {
                Token = token,
                HasRole = false,
                Role = string.Empty
            };
        }
        
        [HttpPost(nameof(Login))]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized();
            }
            var passWordValidation = await userManager.CheckPasswordAsync(user, model.Password);
            if (!passWordValidation)
            {
                return Unauthorized();
            }
            var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (!result.Succeeded)
            {
                return Problem();
            }

            var token = await userService.GetTokenAsync(appSettings.Value.Secret, user.Id);
            var roles = await userManager.GetRolesAsync(user);

            var responseModel = new LoginResponseModel()
            {
                Token = token,
                HasRole = false,
                Role = string.Empty,
                Succeeded = true
            };

            if (roles.Count > 0)
            {
                responseModel.HasRole = true;
                responseModel.Role = roles.FirstOrDefault();
            }

            return responseModel;
        }
    }
}