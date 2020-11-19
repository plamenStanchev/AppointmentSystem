namespace AssignmentSystem.Infrastructure.Services
{
    using AssignmentSystem.Core.Interfaces;
    using AssignmentSystem.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;

    public class CurrentUserService : ICurrentUserService
    {
        private readonly ClaimsPrincipal user;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
            => this.user = httpContextAccessor.HttpContext?.User;

        public string GetUserName()
            => this.user?.Identity?.Name;

        public string GetId()
            => this.user?.GetId();
    }
}
