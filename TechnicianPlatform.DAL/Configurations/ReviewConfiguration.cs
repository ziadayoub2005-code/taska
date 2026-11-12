using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnicianPlatform.DAL.Entities;

namespace TechnicianPlatform.DAL.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews", t =>
            {
                t.HasCheckConstraint("CK_Reviews_Stars", "[Stars] >= 1 AND [Stars] <= 5");
            });

            builder.HasKey(r => r.Id);
            builder.HasIndex(r => r.ServiceRequestId).IsUnique();

            builder.Property(r => r.Stars).IsRequired();
            builder.Property(r => r.Comment).HasMaxLength(1000);
            builder.Property(r => r.CreatedAt).IsRequired();

            builder.HasOne(r => r.ServiceRequest)
                .WithOne(sr => sr.Review)
                .HasForeignKey<Review>(r => r.ServiceRequestId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Customer)
                .WithMany(u => u.GivenReviews)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.TechnicianProfile)
                .WithMany(tp => tp.Reviews)
                .HasForeignKey(r => r.TechnicianProfileId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}