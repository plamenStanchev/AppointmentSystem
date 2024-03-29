﻿namespace AppointmentSystem.Infrastructure.Data.Config
{
    using AppointmentSystem.Core.Entities.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(60);
        }
    }
}
