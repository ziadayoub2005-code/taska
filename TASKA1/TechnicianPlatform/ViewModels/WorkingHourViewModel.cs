using System.ComponentModel.DataAnnotations;

namespace TechnicianPlatform.ViewModels
{
    /// <summary>
    /// Form ViewModel representing a single day's working hours for a technician.
    /// </summary>
    public class WorkingHourViewModel
    {
        [Display(Name = "Day of Week")]
        public DayOfWeek DayOfWeek { get; set; }

        [Display(Name = "Day Off")]
        public bool IsDayOff { get; set; }

        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        public TimeOnly? StartTime { get; set; }

        [Display(Name = "End Time")]
        [DataType(DataType.Time)]
        public TimeOnly? EndTime { get; set; }
    }
}
