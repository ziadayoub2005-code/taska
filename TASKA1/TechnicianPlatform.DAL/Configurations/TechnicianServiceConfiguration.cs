using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnicianPlatform.DAL.Entities;

namespace TechnicianPlatform.DAL.Configurations
{
    public class TechnicianServiceConfiguration : IEntityTypeConfiguration<TechnicianService>
    {
        public void Configure(EntityTypeBuilder<TechnicianService> builder)
        {
            builder.ToTable("TechnicianServices", t =>
            {
                t.HasCheckConstraint("CK_TechnicianServices_Price", "[Price] > 0");
            });

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name).IsRequired().HasMaxLength(150);
            builder.Property(s => s.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(s => s.CreatedAt).IsRequired();

            builder.HasOne(s => s.TechnicianProfile)
                .WithMany(tp => tp.Services)
                .HasForeignKey(s => s.TechnicianProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}