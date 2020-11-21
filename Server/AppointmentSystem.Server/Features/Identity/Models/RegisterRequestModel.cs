namespace AppointmentSystem.Server.Features.Identity.Models
{
    using System.ComponentModel.DataAnnotations;
    public class RegisterRequestModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [EmailAddress]
        [Compare(nameof(Email))]
        public string ConfirmEmail { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
