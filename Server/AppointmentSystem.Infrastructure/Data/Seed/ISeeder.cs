namespace AppointmentSystem.Infrastructure.Data.Seed
{
    using System;
    using System.Threading.Tasks;
    public interface ISeeder
    {
        Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider);
    }
}
