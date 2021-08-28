namespace AppointmentSystem.Infrastructure.Data.Config
{
    using AppointmentSystem.Core.AdministrationDomain.Entety;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class DoctorApplicationConfigurations : IEntityTypeConfiguration<DoctorApplication>
    {
       
        public void Configure(EntityTypeBuilder<DoctorApplication> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(DoctorApplication.AdministationInformationEntryCatalog));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(d => d.PIN)
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(d => d.FirstName)
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
