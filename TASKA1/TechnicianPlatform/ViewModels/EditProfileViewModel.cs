using System.ComponentModel.DataAnnotations;

namespace TechnicianPlatform.ViewModels
{
    /// <summary>
    /// Form ViewModel for editing technician profile details (maps to ProfileEditRequest / TechnicianProfile).
    /// </summary>
    public class EditProfileViewModel
    {
        [Display(Name = "Service Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Display Name")]
        public string DisplayName { get; set; } = string.Empty;

        [Display(Name = "Bio")]
        [DataType(DataType.MultilineText)]
        public string? Bio { get; set; }

        [Display(Name = "Work Area")]
        public string WorkArea { get; set; } = string.Empty;

        [Display(Name = "Contact Phone")]
        [DataType(DataType.PhoneNumber)]
        public string ContactPhone { get; set; } = string.Empty;

        [Display(Name = "Latitude")]
        public double? Latitude { get; set; }

        [Display(Name = "Longitude")]
        public double? Longitude { get; set; }
    }
}
