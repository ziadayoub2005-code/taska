using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnicianPlatform.DAL.Entities;

namespace TechnicianPlatform.DAL.Configurations
{
    public class TechnicianDocumentConfiguration : IEntityTypeConfiguration<TechnicianDocument>
    {
        public void Configure(EntityTypeBuilder<TechnicianDocument> builder)
        {
            builder.ToTable("TechnicianDocuments");
            builder.HasKey(d => d.Id);

            builder.Property(d => d.DocumentType)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.Property(d => d.FilePath).IsRequired().HasMaxLength(500);
            builder.Property(d => d.CreatedAt).IsRequired();

            builder.HasOne(d => d.TechnicianProfile)
                .WithMany(tp => tp.Documents)
                .HasForeignKey(d => d.TechnicianProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}