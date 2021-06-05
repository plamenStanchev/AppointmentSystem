namespace AppointmentSystem.Infrastructure.Data.Config
{
    using AppointmentSystem.Core.Entities.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ApplicationRequestConfigurations : IEntityTypeConfiguration<ApplicationRequest>
    {
        public void Configure(EntityTypeBuilder<ApplicationRequest> builder)
        {
            builder.HasKey(i => i.Id);

            builder
                .Property(x => x.AccountId)
                .IsRequired();

            builder
                .Property(x => x.Data)
                .IsRequired();

            builder
                .Property(x => x.Status)
                .IsRequired();

            builder
                .Property(x => x.RequestType)
                .IsRequired();
        }
    }
}