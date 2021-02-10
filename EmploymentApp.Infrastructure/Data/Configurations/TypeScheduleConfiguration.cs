using EmploymentApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmploymentApp.Infrastructure.Data.Configurations
{
    class TypeScheduleConfiguration : IEntityTypeConfiguration<TypeSchedule>
    {
        public void Configure(EntityTypeBuilder<TypeSchedule> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("TypeScheduleId");

            builder.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);
        }
    }
}
