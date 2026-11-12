using TechnicianPlatform.DAL.Enums;

namespace TechnicianPlatform.DAL.Entities
{
    public class TechnicianDocument : BaseEntity
    {
        public int TechnicianProfileId { get; set; }
        public DocumentType DocumentType { get; set; }
        public string FilePath { get; set; } = string.Empty;

        public TechnicianProfile TechnicianProfile { get; set; } = null!;
    }
}