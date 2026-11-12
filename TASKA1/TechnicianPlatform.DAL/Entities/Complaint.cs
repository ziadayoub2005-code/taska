using TechnicianPlatform.DAL.Enums;

namespace TechnicianPlatform.DAL.Entities
{
    public class Complaint : BaseEntity
    {
        public int SubmittedByUserId { get; set; }
        public int? ServiceRequestId { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public ComplaintStatus Status { get; set; } = ComplaintStatus.Open;
        public string? AdminResponse { get; set; }
        public string? ActionTaken { get; set; }
        public int? HandledByAdminId { get; set; }

        public User SubmittedByUser { get; set; } = null!;
        public ServiceRequest? ServiceRequest { get; set; }
        public User? HandledByAdmin { get; set; }
    }
}