namespace TechnicianPlatform.DAL.Entities
{
    public class ServiceCategory : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<TechnicianProfile> TechnicianProfiles { get; set; } = new List<TechnicianProfile>();
        public ICollection<ProfileEditRequest> ProfileEditRequests { get; set; } = new List<ProfileEditRequest>();
    }
}