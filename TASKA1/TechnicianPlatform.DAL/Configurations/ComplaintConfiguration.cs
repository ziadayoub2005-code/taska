using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnicianPlatform.DAL.Entities;
using TechnicianPlatform.DAL.Enums;

namespace TechnicianPlatform.DAL.Configurations
{
    public class ComplaintConfiguration : IEntityTypeConfiguration<Complaint>
    {
        public void Configure(EntityTypeBuilder<Complaint> builder)
        {
            builder.ToTable("Complaints", t =>
            {
                t.HasCheckConstraint("CK_Complaints_ClosedRequiresResponse",
                    "[Status] <> 'Closed' OR ([AdminResponse] IS NOT NULL AND [ActionTaken] IS NOT NULL)");
            });
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Subject).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Details).IsRequired().HasMaxLength(2000);

            builder.Property(c => c.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(30)
                .HasDefaultValue(ComplaintStatus.Open);

            builder.Property(c => c.AdminResponse).HasMaxLength(2000);
            builder.Property(c => c.ActionTaken).HasMaxLength(500);
            builder.Property(c => c.CreatedAt).IsRequired();

            builder.HasOne(c => c.SubmittedByUser)
                .WithMany(u => u.SubmittedComplaints)
                .HasForeignKey(c => c.SubmittedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.ServiceRequest)
                .WithMany(sr => sr.Complaints)
                .HasForeignKey(c => c.ServiceRequestId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.HandledByAdmin)
                .WithMany(u => u.HandledComplaints)
                .HasForeignKey(c => c.HandledByAdminId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}