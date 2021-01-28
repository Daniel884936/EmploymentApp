using EmploymentApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmploymentApp.Infrastructure.Data.Configurations
{
     class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("JobId");
                

            builder.Property(e => e.Company)
                    .HasMaxLength(80)
                    .IsUnicode(false);

            builder.Property(e => e.Date).HasColumnType("date");

            builder.Property(e => e.Description)
                .IsRequired()
                .HasColumnType("text");

            builder.Property(e => e.Img).HasColumnType("text");

            builder.Property(e => e.Lat).HasColumnType("decimal(18, 0)");

            builder.Property(e => e.Long).HasColumnType("decimal(18, 0)");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasColumnName("title")
                .HasMaxLength(60)
                .IsUnicode(false);

            builder.HasOne(d => d.Category)
                .WithMany(p => p.Job)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_JOB_CATEGORY");

            builder.HasOne(d => d.Status)
                .WithMany(p => p.Job)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_JOB_STATUS");

            builder.HasOne(d => d.TypeSchedule)
                .WithMany(p => p.Job)
                .HasForeignKey(d => d.TypeScheduleId)
                .HasConstraintName("FK_JOB_TYPESHEDULE");
        }
    }
}
