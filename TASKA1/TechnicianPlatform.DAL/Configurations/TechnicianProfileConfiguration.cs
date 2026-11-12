using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnicianPlatform.DAL.Entities;
using TechnicianPlatform.DAL.Enums;

namespace TechnicianPlatform.DAL.Configurations
{
    public class TechnicianProfileConfiguration : IEntityTypeConfiguration<TechnicianProfile>
    {
        public void Configure(EntityTypeBuilder<TechnicianProfile> builder)
        {
            builder.ToTable("TechnicianProfiles", t =>
            {
                t.HasCheckConstraint("CK_TechnicianProfiles_CancellationsCount", "[CancellationsCount] >= 0");
                t.HasCheckConstraint("CK_TechnicianProfiles_Latitude", "[Latitude] IS NULL OR ([Latitude] >= -90 AND [Latitude] <= 90)");
                t.HasCheckConstraint("CK_TechnicianProfiles_Longitude", "[Longitude] IS NULL OR ([Longitude] >= -180 AND [Longitude] <= 180)");
            });

            builder.HasKey(tp => tp.Id);
            builder.HasIndex(tp => tp.UserId).IsUnique();

            builder.Property(tp => tp.DisplayName).IsRequired().HasMaxLength(150);
            builder.Property(tp => tp.Bio).HasMaxLength(1000);
            builder.Property(tp => tp.WorkArea).IsRequired().HasMaxLength(200);
            builder.Property(tp => tp.ContactPhone).IsRequired().HasMaxLength(20);

            builder.Property(tp => tp.ApprovalStatus)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(30)
                .HasDefaultValue(ApprovalStatus.Pending);

            builder.Property(tp => tp.CancellationsCount).HasDefaultValue(0);
            builder.Property(tp => tp.CreatedAt).IsRequired();

            builder.HasOne(tp => tp.User)
                .WithOne(u => u.TechnicianProfile)
                .HasForeignKey<TechnicianProfile>(tp => tp.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(tp => tp.Category)
                .WithMany(c => c.TechnicianProfiles)
                .HasForeignKey(tp => tp.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}