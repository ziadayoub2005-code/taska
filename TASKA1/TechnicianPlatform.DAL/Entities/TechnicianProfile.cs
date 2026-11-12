using TechnicianPlatform.DAL.Enums;

namespace TechnicianPlatform.DAL.Entities
{
    public class TechnicianProfile : BaseEntity
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public string WorkArea { get; set; } = string.Empty;
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string ContactPhone { get; set; } = string.Empty;
        public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Pending;
        public DateTime? ReviewedAt { get; set; }
        public int CancellationsCount { get; set; } = 0;

        public User User { get; set; } = null!;
        public ServiceCategory Category { get; set; } = null!;
        public ICollection<ProfileEditRequest> EditRequests { get; set; } = new List<ProfileEditRequest>();
        public ICollection<TechnicianDocument> Documents { get; set; } = new List<TechnicianDocument>();
        public ICollection<TechnicianService> Services { get; set; } = new List<TechnicianService>();
        public ICollection<PortfolioPhoto> PortfolioPhotos { get; set; } = new List<PortfolioPhoto>();
        public ICollection<WorkingHour> WorkingHours { get; set; } = new List<WorkingHour>();
        public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}