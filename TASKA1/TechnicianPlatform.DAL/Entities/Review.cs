namespace TechnicianPlatform.DAL.Entities
{
    public class Review : BaseEntity
    {
        public int ServiceRequestId { get; set; }
        public int CustomerId { get; set; }
        public int TechnicianProfileId { get; set; }
        public int Stars { get; set; }
        public string? Comment { get; set; }

        public ServiceRequest ServiceRequest { get; set; } = null!;
        public User Customer { get; set; } = null!;
        public TechnicianProfile TechnicianProfile { get; set; } = null!;
    }
}