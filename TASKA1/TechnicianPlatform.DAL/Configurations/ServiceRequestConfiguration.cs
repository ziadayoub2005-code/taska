using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnicianPlatform.DAL.Entities;
using TechnicianPlatform.DAL.Enums;

namespace TechnicianPlatform.DAL.Configurations
{
    public class ServiceRequestConfiguration : IEntityTypeConfiguration<ServiceRequest>
    {
        public void Configure(EntityTypeBuilder<ServiceRequest> builder)
        {
            builder.ToTable("ServiceRequests", t =>
            {
                t.HasCheckConstraint("CK_ServiceRequests_FinalPrice", "[FinalPrice] IS NULL OR [FinalPrice] >= 0");
                t.HasCheckConstraint("CK_ServiceRequests_Latitude", "[Latitude] >= -90 AND [Latitude] <= 90");
                t.HasCheckConstraint("CK_ServiceRequests_Longitude", "[Longitude] >= -180 AND [Longitude] <= 180");
                t.HasCheckConstraint("CK_ServiceRequests_CancelledBy",
                    "[Status] <> 'Cancelled' OR [CancelledByUserId] IS NOT NULL");
                t.HasCheckConstraint("CK_ServiceRequests_CompletedPrice",
                    "[Status] <> 'Completed' OR [FinalPrice] IS NOT NULL");
            });

            builder.HasKey(sr => sr.Id);

            builder.Property(sr => sr.Description).IsRequired().HasMaxLength(2000);

            builder.Property(sr => sr.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(30)
                .HasDefaultValue(RequestStatus.Pending);

            builder.Property(sr => sr.FinalPrice).HasColumnType("decimal(18,2)");
            builder.Property(sr => sr.CancellationReason).HasMaxLength(500);
            builder.Property(sr => sr.Latitude).IsRequired();
            builder.Property(sr => sr.Longitude).IsRequired();
            builder.Property(sr => sr.CreatedAt).IsRequired();

            builder.HasOne(sr => sr.Customer)
                .WithMany(u => u.CustomerRequests)
                .HasForeignKey(sr => sr.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sr => sr.CancelledByUser)
                .WithMany()
                .HasForeignKey(sr => sr.CancelledByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sr => sr.TechnicianProfile)
                .WithMany(tp => tp.ServiceRequests)
                .HasForeignKey(sr => sr.TechnicianProfileId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}