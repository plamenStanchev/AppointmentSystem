namespace AppointmentSystem.Core.Interfaces.Infrastructure
{
    public interface ICurrentUserService
    {
        string GetUserEmail();

        string GetId();
    }
}
