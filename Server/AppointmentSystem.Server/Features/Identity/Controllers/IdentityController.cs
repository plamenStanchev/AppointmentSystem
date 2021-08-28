namespace AppointmentSystem.Server.Features.Identity.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using AppointmentSystem.Core.Interfaces.Features;
    using AppointmentSystem.Infrastructure.Data.Identity;
    using AppointmentSystem.Infrastructure.Extensions;
    using AppointmentSystem.Server.ApplicationOptions;
    using AppointmentSystem.Server.Features.BaseFeatures.Controllers;
    using AppointmentSystem.Server.Features.Identity.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

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

			var createUserResult = await this.userManager.CreateAsync(user, model.Password);
			if (!createUserResult.Succeeded)
			{
				return this.BadRequest(createUserResult.GetError());
			}

			user = await this.userManager.FindByEmailAsync(model.Email);

			var signInResult = await this.signInManager.PasswordSignInAsync(user, model.Password, false, false);
			if (!signInResult.Succeeded)
			{
				return this.Problem();
			}

			var token = await this.userService.GetTokenAsync(appSettings.Value.Secret, user.Id);

			return new LoginResponseModel
			{
				Token = token,
				HasRole = false,
				Role = string.Empty
			};
		}

		[AllowAnonymous]
		[HttpPost(nameof(Login))]
		public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model)
		{
			var user = await this.userManager.FindByEmailAsync(model.Email);
			if (user == null)
			{
				return this.Unauthorized();
			}
			var passWordValidation = await this.userManager.CheckPasswordAsync(user, model.Password);
			if (!passWordValidation)
			{
				return this.Unauthorized();
			}
			var result = await this.signInManager.PasswordSignInAsync(user, model.Password, false, false);

			if (!result.Succeeded)
			{
				return this.Problem();
			}

			var token = await this.userService.GetTokenAsync(appSettings.Value.Secret, user.Id);
			var roles = await this.userManager.GetRolesAsync(user);

			var responseModel = new LoginResponseModel()
			{
				Token = token,
				HasRole = false,
				Role = string.Empty,
			};

			if (roles.Count > 0)
			{
				responseModel.HasRole = true;
				responseModel.Role = roles.FirstOrDefault();
			}

			return this.Ok(responseModel);
		}
	}
}