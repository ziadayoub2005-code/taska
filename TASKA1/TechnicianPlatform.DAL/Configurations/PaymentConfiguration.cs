using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnicianPlatform.DAL.Entities;

namespace TechnicianPlatform.DAL.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments", t =>
            {
                t.HasCheckConstraint("CK_Payments_Amount", "[Amount] >= 0");
            });

            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.ServiceRequestId).IsUnique();

            builder.Property(p => p.Method)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.Property(p => p.Amount).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.PaidAt).IsRequired();
            builder.Property(p => p.CreatedAt).IsRequired();

            builder.HasOne(p => p.ServiceRequest)
                .WithOne(sr => sr.Payment)
                .HasForeignKey<Payment>(p => p.ServiceRequestId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}