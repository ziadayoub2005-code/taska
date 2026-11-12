using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnicianPlatform.DAL.Entities;

namespace TechnicianPlatform.DAL.Configurations
{
    public class PortfolioPhotoConfiguration : IEntityTypeConfiguration<PortfolioPhoto>
    {
        public void Configure(EntityTypeBuilder<PortfolioPhoto> builder)
        {
            builder.ToTable("PortfolioPhotos");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.FilePath).IsRequired().HasMaxLength(500);
            builder.Property(p => p.CreatedAt).IsRequired();

            builder.HasOne(p => p.TechnicianProfile)
                .WithMany(tp => tp.PortfolioPhotos)
                .HasForeignKey(p => p.TechnicianProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}