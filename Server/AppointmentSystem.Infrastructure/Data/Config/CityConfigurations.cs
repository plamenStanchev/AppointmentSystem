namespace AppointmentSystem.Infrastructure.Data.Config
{
    using AppointmentSystem.Core.Entities.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CityConfigurations : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(60);
        }
    }
}
