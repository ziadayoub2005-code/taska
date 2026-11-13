
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechnicianPlatform.DAL.Context;
using TechnicianPlatform.DAL.Enums;
using TechnicianPlatform.Models.ViewModels;

namespace TechnicianPlatform.Controllers
{
    public class TechnicianController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TechnicianController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            [FromQuery] TechnicianFilterViewModel model)
        {
            // Keep the first page as the minimum page number.
            model.Page = Math.Max(1, model.Page);
            model.PageSize = 10;

            // Load active categories for the dropdown.
            model.Categories = await _context.ServiceCategories
                .AsNoTracking()
                .Where(c => c.IsActive)
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToListAsync();

            // Start with approved technicians only.
            var query = _context.TechnicianProfiles
                .AsNoTracking()
                .Where(t => t.ApprovalStatus == ApprovalStatus.Approved);

            // 1. FILTER
            if (model.CategoryId.HasValue)
            {
                query = query.Where(t =>
                    t.CategoryId == model.CategoryId.Value);
            }

            // 2. SEARCH
            if (!string.IsNullOrWhiteSpace(model.SearchTerm))
            {
                var searchTerm = model.SearchTerm.Trim();

                query = query.Where(t =>
                    t.DisplayName.Contains(searchTerm) ||
                    t.Services.Any(s => s.Name.Contains(searchTerm)));
            }

            // Load the matching technicians and their related data.
            var technicians = await query
                .Include(t => t.Category)
                .Include(t => t.Services)
                .ToListAsync();

            // 3. SORTING
            var results = technicians.Select(t =>
            {
                double? distance = null;

                if (model.Latitude.HasValue &&
                    model.Longitude.HasValue &&
                    t.Latitude.HasValue &&
                    t.Longitude.HasValue)
                {
                    distance = CalculateDistance(
                        model.Latitude.Value,
                        model.Longitude.Value,
                        t.Latitude.Value,
                        t.Longitude.Value);
                }

                return new TechnicianFilterViewModel.TechnicianResultItem
                {
                    Id = t.Id,
                    DisplayName = t.DisplayName,
                    WorkArea = t.WorkArea,
                    ContactPhone = t.ContactPhone,
                    CategoryName = t.Category.Name,
                    Services = t.Services
                        .Select(s => s.Name)
                        .ToList(),
                    DistanceKm = distance
                };
            });

            List<TechnicianFilterViewModel.TechnicianResultItem> sortedResults;

            // If user location is available, sort nearest first.
            if (model.Latitude.HasValue && model.Longitude.HasValue)
            {
                sortedResults = results
                    .OrderBy(t => t.DistanceKm.HasValue ? 0 : 1)
                    .ThenBy(t => t.DistanceKm ?? double.MaxValue)
                    .ToList();
            }
            else
            {
                // Fallback sorting when location is unavailable.
                sortedResults = results
                    .OrderBy(t => t.DisplayName)
                    .ToList();
            }

            // 4. PAGINATION
            model.TotalCount = sortedResults.Count;

            if (model.TotalPages > 0)
            {
                model.Page = Math.Min(model.Page, model.TotalPages);
            }
            else
            {
                model.Page = 1;
            }

            model.Technicians = sortedResults
                .Skip((model.Page - 1) * model.PageSize)
                .Take(model.PageSize)
                .ToList();

            return View(model);
        }

        // Calculate the distance between two geographic coordinates.
        private static double CalculateDistance(
            double latitude1,
            double longitude1,
            double latitude2,
            double longitude2)
        {
            const double earthRadiusKm = 6371;

            double lat1 = ToRadians(latitude1);
            double lat2 = ToRadians(latitude2);

            double deltaLat = ToRadians(latitude2 - latitude1);
            double deltaLon = ToRadians(longitude2 - longitude1);

            double a =
                Math.Pow(Math.Sin(deltaLat / 2), 2) +
                Math.Cos(lat1) * Math.Cos(lat2) *
                Math.Pow(Math.Sin(deltaLon / 2), 2);

            a = Math.Clamp(a, 0, 1);

            double c = 2 * Math.Atan2(
                Math.Sqrt(a),
                Math.Sqrt(1 - a));

            return earthRadiusKm * c;
        }

        private static double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
    }
}
