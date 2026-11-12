using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnicianPlatform.DAL.Entities;
using TechnicianPlatform.DAL.Enums;

namespace TechnicianPlatform.DAL.Configurations
{
    public class ProfileEditRequestConfiguration : IEntityTypeConfiguration<ProfileEditRequest>
    {
        public void Configure(EntityTypeBuilder<ProfileEditRequest> builder)
        {
            builder.ToTable("ProfileEditRequests", t =>
            {
                t.HasCheckConstraint("CK_ProfileEditRequests_Latitude", "[Latitude] IS NULL OR ([Latitude] >= -90 AND [Latitude] <= 90)");
                t.HasCheckConstraint("CK_ProfileEditRequests_Longitude", "[Longitude] IS NULL OR ([Longitude] >= -180 AND [Longitude] <= 180)");
            });

            builder.HasKey(pe => pe.Id);

            builder.Property(pe => pe.DisplayName).IsRequired().HasMaxLength(150);
            builder.Property(pe => pe.Bio).HasMaxLength(1000);
            builder.Property(pe => pe.WorkArea).IsRequired().HasMaxLength(200);
            builder.Property(pe => pe.ContactPhone).IsRequired().HasMaxLength(20);

            builder.Property(pe => pe.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(30)
                .HasDefaultValue(EditRequestStatus.Pending);

            builder.Property(pe => pe.CreatedAt).IsRequired();

            builder.HasIndex(pe => pe.TechnicianProfileId)
                .IsUnique()
                .HasFilter("[Status] = 'Pending'");

            builder.HasOne(pe => pe.TechnicianProfile)
                .WithMany(tp => tp.EditRequests)
                .HasForeignKey(pe => pe.TechnicianProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pe => pe.Category)
                .WithMany(c => c.ProfileEditRequests)
                .HasForeignKey(pe => pe.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}