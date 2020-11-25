namespace AppointmentSystem.Server.Features.Identity.Models
{
    using AppointmentSystem.Server.Features.BaseFeatures.Models;
    public class LoginResponseModel :BaseResponseModel
    {
        public string Token { get; set; }

        public bool HasRole { get; set; }

        public string Role { get; set; }

    }
}
