using System.ComponentModel.DataAnnotations;

namespace TechnicianPlatform.ViewModels
{
    /// <summary>
    /// Form ViewModel for completing a service request with its final price.
    /// </summary>
    public class CompleteServiceRequestViewModel
    {
        public int ServiceRequestId { get; set; }

        [Display(Name = "Final Price")]
        public decimal? FinalPrice { get; set; }
    }
}
