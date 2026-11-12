using TechnicianPlatform.DAL.Enums;

namespace TechnicianPlatform.DAL.Entities
{
    public class ProfileEditRequest : BaseEntity
    {
        public int TechnicianProfileId { get; set; }
        public int CategoryId { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public string WorkArea { get; set; } = string.Empty;
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string ContactPhone { get; set; } = string.Empty;
        public EditRequestStatus Status { get; set; } = EditRequestStatus.Pending;
        public DateTime? ReviewedAt { get; set; }

        public TechnicianProfile TechnicianProfile { get; set; } = null!;
        public ServiceCategory Category { get; set; } = null!;
    }
}