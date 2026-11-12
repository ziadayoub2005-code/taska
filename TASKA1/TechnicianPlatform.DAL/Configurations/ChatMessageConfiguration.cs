using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnicianPlatform.DAL.Entities;

namespace TechnicianPlatform.DAL.Configurations
{
    public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.ToTable("ChatMessages", t =>
            {
                t.HasCheckConstraint("CK_ChatMessages_DifferentUsers", "[SenderId] <> [ReceiverId]");
            });

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Body).IsRequired().HasMaxLength(2000);
            builder.Property(m => m.CreatedAt).IsRequired();

            builder.HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.ServiceRequest)
                .WithMany(sr => sr.ChatMessages)
                .HasForeignKey(m => m.ServiceRequestId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}