﻿using EmploymentApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmploymentApp.Infrastructure.Data.Configurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("UserId")
                .ValueGeneratedNever();

            builder.Property(e => e.Bithdate).HasColumnType("date");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Surnames)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}