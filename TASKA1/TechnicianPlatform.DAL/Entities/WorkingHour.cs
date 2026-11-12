namespace TechnicianPlatform.DAL.Entities
{
    public class WorkingHour : BaseEntity
    {
        public int TechnicianProfileId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public bool IsDayOff { get; set; } = false;
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }

        public TechnicianProfile TechnicianProfile { get; set; } = null!;
    }
}