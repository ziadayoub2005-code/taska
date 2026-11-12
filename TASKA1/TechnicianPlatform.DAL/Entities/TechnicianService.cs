namespace TechnicianPlatform.DAL.Entities
{
    public class TechnicianService : BaseEntity
    {
        public int TechnicianProfileId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public TechnicianProfile TechnicianProfile { get; set; } = null!;
    }
}