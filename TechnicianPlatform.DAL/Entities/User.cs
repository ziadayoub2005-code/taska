using Microsoft.AspNetCore.Identity;

namespace TechnicianPlatform.DAL.Entities
{
    public class User : IdentityUser<int>
    {
        public string FullName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public TechnicianProfile? TechnicianProfile { get; set; }
        public ICollection<ServiceRequest> CustomerRequests { get; set; } = new List<ServiceRequest>();
        public ICollection<Review> GivenReviews { get; set; } = new List<Review>();
        public ICollection<Complaint> SubmittedComplaints { get; set; } = new List<Complaint>();
        public ICollection<Complaint> HandledComplaints { get; set; } = new List<Complaint>();
        public ICollection<ChatMessage> SentMessages { get; set; } = new List<ChatMessage>();
        public ICollection<ChatMessage> ReceivedMessages { get; set; } = new List<ChatMessage>();
    }
}