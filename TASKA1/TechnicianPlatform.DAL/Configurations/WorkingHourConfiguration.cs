using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnicianPlatform.DAL.Entities;

namespace TechnicianPlatform.DAL.Configurations
{
    public class WorkingHourConfiguration : IEntityTypeConfiguration<WorkingHour>
    {
        public void Configure(EntityTypeBuilder<WorkingHour> builder)
        {
            builder.ToTable("WorkingHours", t =>
            {
                t.HasCheckConstraint("CK_WorkingHours_Times",
                    "[IsDayOff] = 1 OR ([StartTime] IS NOT NULL AND [EndTime] IS NOT NULL AND [EndTime] > [StartTime])");
            });
            builder.HasKey(w => w.Id);

            builder.HasIndex(w => new { w.TechnicianProfileId, w.DayOfWeek }).IsUnique();

            builder.Property(w => w.DayOfWeek).IsRequired();
            builder.Property(w => w.IsDayOff).HasDefaultValue(false);
            builder.Property(w => w.CreatedAt).IsRequired();

            builder.HasOne(w => w.TechnicianProfile)
                .WithMany(tp => tp.WorkingHours)
                .HasForeignKey(w => w.TechnicianProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}