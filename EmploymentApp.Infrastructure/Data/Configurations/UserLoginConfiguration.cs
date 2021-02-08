using EmploymentApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmploymentApp.Infrastructure.Data.Configurations
{
    class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("UserLoginId");

            builder.HasIndex(e => e.Email)
                   .HasName("idx_email")
                   .IsUnique();

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(60)
                .IsUnicode(false);

            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(300)
                .IsUnicode(false);

            builder.HasOne(d => d.Role)
                .WithMany(p => p.UserLogin)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_USERLOGIN_ROLE");

            builder.HasOne(d => d.User)
                .WithMany(p => p.UserLogin)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_USERLOGIN_USER");
        }
    }
}
