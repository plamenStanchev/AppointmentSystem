namespace AppointmentSystem.Infrastructure.Data.Config
{
    using AppointmentSystem.Core.AdministrationDomain.Entety;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class AdministrationInformatonEntryConfigurations : IEntityTypeConfiguration<AdministationInformationEntry>
    {
        public void Configure(EntityTypeBuilder<AdministationInformationEntry> builder)
        {
            builder.Property(b => b.Status)
                .IsRequired();

            builder.HasIndex(b => b.ApplicationId);
        }
    }
}
