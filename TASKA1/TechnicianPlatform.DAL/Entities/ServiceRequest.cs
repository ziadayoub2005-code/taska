using TechnicianPlatform.DAL.Enums;

namespace TechnicianPlatform.DAL.Entities
{
    public class ServiceRequest : BaseEntity
    {
        public int CustomerId { get; set; }
        public int TechnicianProfileId { get; set; }
        public string Description { get; set; } = string.Empty;
        public RequestStatus Status { get; set; } = RequestStatus.Pending;
        public int? CancelledByUserId { get; set; }
        public decimal? FinalPrice { get; set; }
        public string? CancellationReason { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public User Customer { get; set; } = null!;
        public User? CancelledByUser { get; set; }
        public TechnicianProfile TechnicianProfile { get; set; } = null!;
        public Review? Review { get; set; }
        public Payment? Payment { get; set; }
        public ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();
        public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
    }
}