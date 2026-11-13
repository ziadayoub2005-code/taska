using System.ComponentModel.DataAnnotations;

namespace TechnicianPlatform.ViewModels
{
    /// <summary>
    /// Form ViewModel for adding a service to a technician's offered services.
    /// </summary>
    public class AddServiceViewModel
    {
        [Display(Name = "Service Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Price")]
        public decimal? Price { get; set; }
    }
}
