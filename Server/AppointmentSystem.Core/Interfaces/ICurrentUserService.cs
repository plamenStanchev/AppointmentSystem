namespace AppointmentSystem.Core.Interfaces
{
    public interface ICurrentUserService
    {
        string GetUserName();

        string GetId();
    }
}
