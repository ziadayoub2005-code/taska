using System.ComponentModel.DataAnnotations;
using TechnicianPlatform.DAL.Enums;

namespace TechnicianPlatform.ViewModels
{
    /// <summary>
    /// Form ViewModel for user registration (Customer or Technician).
    /// Validation rules are separated in <c>RegisterViewModelValidator</c> using FluentValidation.
    /// </summary>
    public class RegisterViewModel
    {
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Display(Name = "Account Type")]
        public UserRole Role { get; set; } = UserRole.Customer;

        // Technician-only profile fields (validated when Role == UserRole.Technician)
        [Display(Name = "Service Category")]
        public int? CategoryId { get; set; }

        [Display(Name = "Display Name")]
        public string? DisplayName { get; set; }

        [Display(Name = "Work Area")]
        public string? WorkArea { get; set; }

        [Display(Name = "Contact Phone")]
        [DataType(DataType.PhoneNumber)]
        public string? ContactPhone { get; set; }

        [Display(Name = "Bio")]
        [DataType(DataType.MultilineText)]
        public string? Bio { get; set; }

        [Display(Name = "Latitude")]
        public double? Latitude { get; set; }

        [Display(Name = "Longitude")]
        public double? Longitude { get; set; }
    }
}
