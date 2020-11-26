namespace AppointmentSystem.Server.Features.BaseFeatures.Models
{
    public abstract class BaseResponseModel
    {
        public bool Succeeded { get; set; }

        public string ErrorMesage { get; set; }
    }
}
