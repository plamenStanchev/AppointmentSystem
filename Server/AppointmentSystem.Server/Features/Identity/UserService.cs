namespace AppointmentSystem.Server.Features.Identity
{
    using AppointmentSystem.Core.Interfaces.Features;
    using AppointmentSystem.Infrastructure.Data.Identity;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private UserManager<ApplicationUser> userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<string> GetTokenAsync(string secret, string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var user = await userManager.FindByIdAsync(userId);
            var roles = await userManager.GetRolesAsync(user);
            Claim roleClaim;

            if (roles.Count == 0)
            {
                roleClaim = new Claim(ClaimTypes.Role, "");

            }
            else
            {
                roleClaim = new Claim(ClaimTypes.Role, roles.FirstOrDefault());
            }

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email,user.Email ),
                    new Claim(ClaimTypes.NameIdentifier,userId),
                    roleClaim
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }

    }
}
