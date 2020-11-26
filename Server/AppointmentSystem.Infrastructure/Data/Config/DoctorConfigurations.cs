 namespace AppointmentSystem.Infrastructure.Data.Config
{
    using AppointmentSystem.Core.Entities.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class DoctorConfigurations : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(d => d.Id);

            builder
                .HasOne(d => d.City)
                .WithMany(c => c.Doctors)
                .HasForeignKey(c => c.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(d => d.Department)
                .WithMany(d => d.Doctors)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(d => d.PIN)
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(d => d.FistName)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(d => d.SecondName)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(d => d.SurName)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(d => d.AccountId)
                .IsRequired();

            builder.Property(d => d.DepartmentId)
                .IsRequired();

            builder.Property(d => d.CityId)
                .IsRequired();

            builder.Property(d => d.Description)
                .HasMaxLength(500);
        }
    }
}
