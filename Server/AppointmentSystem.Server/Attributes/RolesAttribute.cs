namespace AppointmentSystem.Server.Attributes
{
	using Microsoft.AspNetCore.Authorization;

	public class RolesAttribute : AuthorizeAttribute
	{
		public RolesAttribute(params string[] roles)
			=> this.Roles = string.Join(",", roles);
	}
}