namespace TechnicianPlatform.ViewModels
{
    /// <summary>
    /// Form ViewModel for submitting a technician's weekly working hours schedule.
    /// </summary>
    public class WorkingHoursViewModel
    {
        public List<WorkingHourViewModel> Days { get; set; } = new();
    }
}
