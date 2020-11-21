namespace AppointmentSystem.Core.Interfaces.Infrastructure
{
    public interface ICurrentUserService
    {
        string GetUserName();

        string GetId();
    }
}
