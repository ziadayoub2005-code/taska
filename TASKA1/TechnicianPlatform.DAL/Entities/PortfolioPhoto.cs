 namespace TechnicianPlatform.DAL.Entities
{
    public class PortfolioPhoto : BaseEntity
    {
        public int TechnicianProfileId { get; set; }
        public string FilePath { get; set; } = string.Empty;

        public TechnicianProfile TechnicianProfile { get; set; } = null!;
    }
}