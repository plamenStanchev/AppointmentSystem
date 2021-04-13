namespace AppointmentSystem.Core.Interfaces.Features
{
    using System.Threading.Tasks;

    public interface IUserService
    {
        public Task<string> GetTokenAsync(string secret, string userId);
    }
}
