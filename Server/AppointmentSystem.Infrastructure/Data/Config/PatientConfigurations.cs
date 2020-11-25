namespace AppointmentSystem.Infrastructure.Data.Config
{
    using AppointmentSystem.Core.Entities.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PatientConfigurations : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(p => p.FistName)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(p => p.SurName)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(p => p.SecondName)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(p => p.PIN)
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(p => p.Address)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.CityId)
                .IsRequired();

            builder.Property(p => p.AccountId)
                .IsRequired();
        }
    }
}
