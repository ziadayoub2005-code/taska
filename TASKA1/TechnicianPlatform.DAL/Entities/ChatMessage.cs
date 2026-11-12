namespace TechnicianPlatform.DAL.Entities
{
    public class ChatMessage : BaseEntity
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int ServiceRequestId { get; set; }
        public string Body { get; set; } = string.Empty;

        public User Sender { get; set; } = null!;
        public User Receiver { get; set; } = null!;
        public ServiceRequest ServiceRequest { get; set; } = null!;
    }
}