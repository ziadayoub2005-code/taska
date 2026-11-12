using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnicianPlatform.DAL.Entities;

namespace TechnicianPlatform.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", t =>
            {
                t.HasCheckConstraint("CK_Users_PhoneNumber_Digits", "[PhoneNumber] NOT LIKE '%[^0-9]%'");
            });

            builder.Property(u => u.FullName).IsRequired().HasMaxLength(150);

            builder.Property(u => u.Email).IsRequired();
            builder.HasIndex(u => u.NormalizedEmail)
                .IsUnique()
                .HasFilter("[NormalizedEmail] IS NOT NULL");

            builder.Property(u => u.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.HasIndex(u => u.PhoneNumber).IsUnique();

            builder.Property(u => u.CreatedAt).IsRequired();
        }
    }
}
