# Taska — Repository & Technical Infrastructure Documentation (.NET Architecture)

This document provides a comprehensive technical audit and architectural blueprint of the **Taska** platform under the **.NET 8 / ASP.NET Core** enterprise ecosystem, detailing the solution structure, NuGet dependencies, configuration schemas, Entity Framework Core 8 database layer, SignalR real-time hubs, REST API endpoints, and development workflows.

---

## 1. Project Identification & Executive Summary

- **Project Name:** Taska (TASKA / taska.io)
- **Primary Domain:** Smart Compound Facilities & On-Demand Residential Maintenance
- **Core Technology Stack:** ASP.NET Core 8 Web API (C# 12) + Entity Framework Core 8 + ASP.NET Core SignalR + React 18/19 Frontends
- **Architecture Model:** Clean Architecture / N-Tier Modular Monolith
- **Target Deployment:** Localized deployment across private residential compounds (e.g., Giza & New Cairo gated communities)

> [!IMPORTANT]
> ### 📌 Current Development Milestone & Implementation Status (Midterm Milestone)
> **The TASKA project is currently under active development (Work In Progress):**
> - **Phase 1 — Frontend Client Applications:** ✅ **COMPLETED** (`taska-resident`, `taska-technician`, `admin-react` are fully designed, developed, and running interactively using the built-in **Offline Sandbox Simulator** for UI validation).
> - **Phase 2 — Backend Architecture & Services:** 🚧 **IN PROGRESS / SCHEDULED** (The ASP.NET Core 8 Web API, EF Core 8 database schema, and SignalR Hub contracts are fully designed and architected in this document; C# server implementation and database migrations are scheduled for the upcoming development sprints).

---

## 2. Solution Structure & Repository Anatomy

The repository follows a clean, decoupled solution structure separating the .NET backend API from the client applications:

```
project/
├── Taska.sln                          # Master Visual Studio / .NET Solution
├── README.md                          # Primary GitHub repository documentation
├── MASTER-DOCUMENTATION.md            # Central navigation index for deliverables
│
├── taska-api/ (or Taska.Api/)         # ASP.NET Core 8.0 Web API & Real-Time Backend
│   ├── Controllers/                   # Versioned REST Controllers (/api/v1)
│   │   ├── AuthController.cs          # Resident, Technician, Admin JWT Authentication
│   │   ├── OrdersController.cs        # Maintenance Request Lifecycle & Bidding
│   │   ├── TechniciansController.cs   # Duty State, Live GPS Stream, Job Execution
│   │   ├── CompoundsController.cs     # Compound Verification & Management
│   │   ├── CategoriesController.cs    # Service Catalog & Base Rates
│   │   ├── ChatController.cs          # In-Order Message History & Dispatch
│   │   ├── SupportController.cs       # Resident Support Tickets & Resolution
│   │   ├── TranslationsController.cs  # Dynamic Bilingual Translation API
│   │   └── AdminController.cs         # Operations Dashboard, KPIs, Payments, Ratings
│   ├── Hubs/                          # ASP.NET Core SignalR Real-Time Hubs
│   │   ├── OrderHub.cs                # Order lifecycle, live bids, status transitions
│   │   ├── LocationHub.cs             # Live technician GPS coordinate streaming
│   │   ├── SupportHub.cs              # Real-time customer care dialogue
│   │   └── CompoundHub.cs             # Compound technician job broadcast radar
│   ├── Data/                          # Entity Framework Core 8 Data Layer
│   │   ├── TaskaDbContext.cs          # Master DbContext & Fluent API configurations
│   │   ├── Migrations/                # EF Core Code-First Migrations
│   │   └── DbInitializer.cs           # Database seeder (compounds, categories, users)
│   ├── Entities/                      # 15 Domain Models & Entities
│   │   ├── Compound.cs
│   │   ├── User.cs                    # Residents and Admins
│   │   ├── Technician.cs              # Verified Compound Technicians
│   │   ├── ServiceCategory.cs
│   │   ├── Order.cs
│   │   ├── OrderBid.cs
│   │   ├── OrderStatusHistory.cs
│   │   ├── Payment.cs
│   │   ├── Rating.cs
│   │   ├── TechnicianLocationLog.cs
│   │   ├── ChatMessage.cs
│   │   ├── SupportTicket.cs
│   │   ├── SupportMessage.cs
│   │   ├── Language.cs
│   │   └── Translation.cs
│   ├── Services/                      # Application Business Logic Layer
│   │   ├── AuthService.cs
│   │   ├── OrderService.cs
│   │   ├── LocationService.cs
│   │   ├── PaymentService.cs
│   │   └── RatingService.cs
│   ├── BackgroundServices/            # IHostedService Asynchronous Workers
│   │   └── PushNotificationWorker.cs  # Background worker for FCM mobile push alerts
│   ├── DTOs/                          # Data Transfer Objects (Requests & Responses)
│   ├── Middlewares/                   # Global Exception Handling & Logging Middlewares
│   ├── Program.cs                     # WebApplication host, DI services & pipeline setup
│   ├── appsettings.json               # Database connection strings, JWT, SignalR config
│   └── Taska.Api.csproj               # .NET 8 Project file with NuGet dependencies
│
├── taska-resident/                    # Resident Web Application & Mobile PWA
│   ├── src/
│   │   ├── components/                # Map, Layout, Buttons, Support Widget
│   │   ├── pages/                     # Login, Register, VerifyOtp, Home, CreateOrder, Tracking
│   │   ├── services/                  # Axios API client, SignalR Hub client (@microsoft/signalr)
│   │   └── store/                     # Zustand state management & bilingual context
│   └── package.json
│
├── taska-technician/                  # Technician Field Application
│   ├── src/
│   │   ├── components/                # Bottom sheets, Navigation map, Layout
│   │   ├── pages/                     # Login, TechHome, TechExecution, Profile
│   │   ├── services/                  # SignalR connection manager, Axios client
│   │   └── store/                     # Zustand technician state & GPS tracker
│   └── package.json
│
├── admin-react/                       # Operations & Administrative Console
│   ├── src/
│   │   ├── components/                # Dark-themed Sidebar, StatCards
│   │   ├── pages/                     # Dashboard, OrdersBoard (Kanban), Users, Technicians
│   │   └── services/                  # SignalR admin listener & API client
│   └── package.json
│
└── documentation/                     # Full University Specification Artifacts
    ├── README.md
    ├── SRS/
    │   └── SRS.md                     # IEEE 830 / ISO Software Requirements Specification
    ├── UI-UX/
    │   ├── UI-UX.md                   # UI design tokens, components, screen breakdown
    │   ├── User-Flows.md              # Mermaid sequence diagrams & user journeys
    │   └── Sitemap.md                 # Information architecture & route mapping
    ├── Project-Management/
    │   ├── Trello-Jira-Backlog.md     # Agile Epics, User Stories, Acceptance Criteria
    │   └── tasks.csv                  # Importable CSV for Jira / Trello / Linear
    └── Repository/
        └── Repository-Documentation.md# This document
```

---

## 3. Technology Inventory & Detailed Versions

### 3.1. Backend Subsystem (`Taska.Api`)
- **Runtime & SDK:** .NET 8.0 SDK (C# 12)
- **Web Framework:** ASP.NET Core 8.0 Web API
- **Data Access:** Entity Framework Core 8.0 (`Microsoft.EntityFrameworkCore`)
- **Database Providers:**
  - SQL Server: `Microsoft.EntityFrameworkCore.SqlServer`
  - MySQL Alternative: `Pomelo.EntityFrameworkCore.MySql`
- **Real-Time Communication:** ASP.NET Core SignalR (`Microsoft.AspNetCore.SignalR`)
- **Authentication & Security:**
  - `Microsoft.AspNetCore.Authentication.JwtBearer` (JWT Token Security)
  - `BCrypt.Net-Next` (Password hashing)
- **API Documentation & Exploration:** `Swashbuckle.AspNetCore` (Swagger / OpenAPI 3.0)
- **Validation:** `FluentValidation.AspNetCore`
- **Testing:** `xUnit`, `Moq`, `FluentAssertions`, `Microsoft.AspNetCore.Mvc.Testing`

### 3.2. Client Applications
- **Resident Portal:** React 19, TypeScript, Vite, Zustand, `@microsoft/signalr`, Google Maps JS API.
- **Technician Portal:** React 19, TypeScript, Vite, Zustand, `@microsoft/signalr`, HTML5 Geolocation API.
- **Admin Console:** React 18, Vite, Recharts, `@microsoft/signalr`, Lucide React, React Hot Toast.

---

## 4. Configuration & Environment Variables (`appsettings.json`)

The ASP.NET Core configuration file manages database strings, JWT parameters, and SignalR settings:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore.Database.Command": "Information"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=taska_db;User Id=sa;Password=YourSecurePassword!;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "TaskaSuperSecretEnterpriseKeyWith256BitsMinimumSecurity2026!",
    "Issuer": "TaskaApi",
    "Audience": "TaskaClients",
    "ExpiryMinutes": 1440
  },
  "SignalR": {
    "EnableDetailedErrors": true,
    "KeepAliveIntervalInSeconds": 15
  },
  "Firebase": {
    "CredentialsPath": "firebase-credentials.json",
    "ProjectId": "taska-firebase"
  },
  "Cors": {
    "AllowedOrigins": [
      "http://localhost:5173",
      "http://localhost:5174",
      "http://localhost:5175"
    ]
  }
}
```

---

## 5. Entity Framework Core 8 Database Architecture

The data tier is constructed via Entity Framework Core Code-First entity mappings:

### 5.1. Entity Configuration & Relationships

```csharp
public class TaskaDbContext : DbContext
{
    public TaskaDbContext(DbContextOptions<TaskaDbContext> options) : base(options) {}

    public DbSet<Compound> Compounds => Set<Compound>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Technician> Technicians => Set<Technician>();
    public DbSet<ServiceCategory> ServiceCategories => Set<ServiceCategory>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderBid> OrderBids => Set<OrderBid>();
    public DbSet<OrderStatusHistory> OrderStatusHistories => Set<OrderStatusHistory>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Rating> Ratings => Set<Rating>();
    public DbSet<TechnicianLocationLog> TechnicianLocationLogs => Set<TechnicianLocationLog>();
    public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();
    public DbSet<SupportTicket> SupportTickets => Set<SupportTicket>();
    public DbSet<SupportMessage> SupportMessages => Set<SupportMessage>();
    public DbSet<Language> Languages => Set<Language>();
    public DbSet<Translation> Translations => Set<Translation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Unique compound code
        modelBuilder.Entity<Compound>()
            .HasIndex(c => c.Code).IsUnique();

        // Unique user email and phone
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Phone).IsUnique();

        // Unique technician phone
        modelBuilder.Entity<Technician>()
            .HasIndex(t => t.Phone).IsUnique();

        // Enforce one bid per technician per order
        modelBuilder.Entity<OrderBid>()
            .HasIndex(b => new { b.OrderId, b.TechnicianId }).IsUnique();

        // Unique translation per key and language
        modelBuilder.Entity<Translation>()
            .HasIndex(t => new { t.LanguageCode, t.Key }).IsUnique();

        // 1-to-1 relationships for Order with Payment and Rating
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Payment)
            .WithOne(p => p.Order)
            .HasForeignKey<Payment>(p => p.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Rating)
            .WithOne(r => r.Order)
            .HasForeignKey<Rating>(r => r.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

---

## 6. Real-Time Hubs Architecture (ASP.NET Core SignalR)

Instead of third-party broadcasting servers, Taska utilizes high-performance **ASP.NET Core SignalR Hubs**:

### 6.1. Hub Mapping (`Program.cs`)
```csharp
app.MapHub<OrderHub>("/hubs/order");
app.MapHub<LocationHub>("/hubs/location");
app.MapHub<SupportHub>("/hubs/support");
app.MapHub<CompoundHub>("/hubs/compound");
```

### 6.2. SignalR Hub Definitions

1. **`OrderHub` (`/hubs/order`):**
   - Residents and assigned technicians join the order group `Group("order-{id}")`.
   - Methods:
     - `SubscribeToOrder(int orderId)`
     - `SendOrderMessage(int orderId, string message)`
     - `BroadcastOrderStatus(int orderId, string status)`
2. **`LocationHub` (`/hubs/location`):**
   - Technicians stream live GPS coordinates.
   - Broadcasts `TechnicianLocationUpdated(orderId, lat, lng, speed, heading)` to order watchers.
3. **`CompoundHub` (`/hubs/compound`):**
   - Technicians join compound group `Group("compound-{compoundId}")`.
   - Broadcasts `NewOrderAvailable(orderDto)` to online compound technicians.
4. **`SupportHub` (`/hubs/support`):**
   - Manages live customer service dialogue between residents and platform administrators.

---

## 7. REST API Endpoints Specification

### 7.1. Public Endpoints
- `GET /api/v1/compounds` — List registered compounds.
- `GET /api/v1/categories` — Browse service categories and base rates.
- `GET /api/v1/translations?lang={code}` — Fetch bilingual dictionary.
- `POST /api/v1/auth/register` — Resident registration & OTP email dispatch.
- `POST /api/v1/auth/verify-otp` — Validate OTP and return JWT Bearer token.
- `POST /api/v1/auth/login` — Resident and Admin JWT login.
- `POST /api/v1/auth/technician/login` — Technician login (Phone/Email + Password).

### 7.2. Resident Endpoints (`[Authorize(Roles = "Resident")]`)
- `GET /api/v1/user/me` — Authenticated resident profile.
- `GET /api/v1/user/orders` — History of resident maintenance orders.
- `POST /api/v1/user/orders` — Create new maintenance request.
- `GET /api/v1/user/orders/{id}` — Order tracking details & status.
- `POST /api/v1/user/orders/{id}/cancel` — Cancel pending/accepted request.
- `GET /api/v1/user/orders/{id}/bids` — Retrieve incoming technician offers.
- `POST /api/v1/user/orders/{id}/bids/{bidId}/accept` — Accept offer.
- `POST /api/v1/user/orders/{id}/pay` — Settle invoice (Cash or Visa simulation).
- `POST /api/v1/user/orders/{id}/rate` — Submit 1–5 star rating and feedback.
- `POST /api/v1/user/support/tickets` — Create customer support case.

### 7.3. Technician Endpoints (`[Authorize(Roles = "Technician")]`)
- `POST /api/v1/technician/toggle-status` — Toggle duty (online/offline).
- `POST /api/v1/technician/location` — Stream current GPS coordinates.
- `GET /api/v1/technician/orders` — Compound requests radar & active orders.
- `POST /api/v1/technician/orders/{id}/bid` — Submit reverse price bid.
- `POST /api/v1/technician/orders/{id}/accept` — Accept order at base price.
- `POST /api/v1/technician/orders/{id}/status` — Step execution (`arrived`, `in_progress`, `completed`).

### 7.4. Administrator Endpoints (`[Authorize(Roles = "Admin")]`)
- `GET /api/v1/admin/dashboard` — KPI metrics and operational stats.
- `GET /api/v1/admin/orders` — Full compound orders directory.
- `GET /api/v1/admin/payments` & `/refund` — Financial ledger and refund action.
- `GET /api/v1/admin/support/tickets` — Helpdesk management queue.
- `PUT /api/v1/admin/translations` — Update dynamic bilingual translation strings.

---

## 8. Build, Database Migration & Execution Instructions

> **Milestone Note:** The React client applications are currently executable immediately via `npm run dev` and operate in **Client-Side Sandbox Mode** for interactive evaluation. The `.NET 8` backend execution commands below represent the target deployment setup for Phase 2 implementation.

```bash
# Phase 2 — Target .NET 8 Backend Launch:
# 1. Restore dependencies
dotnet restore

# 2. Apply EF Core Code-First Migrations
dotnet ef database update

# 3. Launch .NET 8 Web API & SignalR Hubs
dotnet run --project taska-api

# Phase 1 — Currently Running Client Applications (Sandbox Demo):
cd taska-resident   ; npm run dev -- --host --port 5173
cd taska-technician ; npm run dev -- --host --port 5174
cd admin-react      ; npm run dev -- --host --port 5175
```
