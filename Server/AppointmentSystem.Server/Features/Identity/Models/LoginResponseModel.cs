namespace AppointmentSystem.Server.Features.Identity.Models
{
    public class LoginResponseModel
    {
        public string Token { get; set; }

        public bool HasRole { get; set; }

        public string Role { get; set; }
    }
}
