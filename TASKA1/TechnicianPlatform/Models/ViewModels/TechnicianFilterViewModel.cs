
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TechnicianPlatform.Models.ViewModels
{
    public class TechnicianFilterViewModel
    {
        // Filter
        public int? CategoryId { get; set; }

        // User location
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        // Search
        public string? SearchTerm { get; set; }

        // Pagination
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; }

        public int TotalPages =>
            (int)Math.Ceiling((double)TotalCount / PageSize);

        // Dropdown categories
        public IEnumerable<SelectListItem> Categories { get; set; }
            = new List<SelectListItem>();

        // Technicians displayed on the current page
        public List<TechnicianResultItem> Technicians { get; set; }
            = new List<TechnicianResultItem>();

        // Result item used by this page
        public class TechnicianResultItem
        {
            public int Id { get; set; }
            public string DisplayName { get; set; } = string.Empty;
            public string WorkArea { get; set; } = string.Empty;
            public string ContactPhone { get; set; } = string.Empty;
            public string CategoryName { get; set; } = string.Empty;
            public List<string> Services { get; set; } = new();
            public double? DistanceKm { get; set; }
        }
    }
}
