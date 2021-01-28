using EmploymentApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmploymentApp.Infrastructure.Data.Configurations
{
    class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("CategoryId");
                
            builder.Property(e => e.Name)
                     .IsRequired()
                     .HasMaxLength(60)
                     .IsUnicode(false);
        }
    }
}
