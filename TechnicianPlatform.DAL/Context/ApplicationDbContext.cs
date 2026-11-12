using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechnicianPlatform.DAL.Entities;

namespace TechnicianPlatform.DAL.Context
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ServiceCategory> ServiceCategories => Set<ServiceCategory>();
        public DbSet<TechnicianProfile> TechnicianProfiles => Set<TechnicianProfile>();
        public DbSet<ProfileEditRequest> ProfileEditRequests => Set<ProfileEditRequest>();
        public DbSet<TechnicianDocument> TechnicianDocuments => Set<TechnicianDocument>();
        public DbSet<TechnicianService> TechnicianServices => Set<TechnicianService>();
        public DbSet<PortfolioPhoto> PortfolioPhotos => Set<PortfolioPhoto>();
        public DbSet<WorkingHour> WorkingHours => Set<WorkingHour>();
        public DbSet<ServiceRequest> ServiceRequests => Set<ServiceRequest>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Complaint> Complaints => Set<Complaint>();
        public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<IdentityRole<int>>().ToTable("Roles").HasData(
                new IdentityRole<int> { Id = 1, Name = "Customer",   NormalizedName = "CUSTOMER",   ConcurrencyStamp = "role-customer" },
                new IdentityRole<int> { Id = 2, Name = "Technician", NormalizedName = "TECHNICIAN", ConcurrencyStamp = "role-technician" },
                new IdentityRole<int> { Id = 3, Name = "Admin",      NormalizedName = "ADMIN",      ConcurrencyStamp = "role-admin" });
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
        }
    }
}