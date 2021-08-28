namespace AppointmentSystem.Infrastructure.Services
{
    using AppointmentSystem.Core.Interfaces.Infrastructure;
    using AppointmentSystem.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;

    public class CurrentUserService : ICurrentUserService
    {
        private readonly ClaimsPrincipal user;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
            => this.user = httpContextAccessor.HttpContext?.User;

        public string GetUserEmail()
            => this.user?.GetEmail();

        public string GetId()
            => this.user?.GetId();
    }
}
