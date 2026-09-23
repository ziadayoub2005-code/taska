# Taska — Agile Project Management Space (Jira / Trello Backlog)
## Enterprise .NET 8 / ASP.NET Core Architecture

This document reconstructs the complete engineering and product management backlog for **Taska** (**TASKA**), derived from the enterprise **.NET 8 / ASP.NET Core** backend and React frontend client applications.

---

## 1. Project Identification

- **Project Title:** Taska — Intelligent Compound Maintenance & Home Services Platform
- **Project Key:** `TASKA`
- **Methodology:** Scrum / Agile Kanban
- **Target Platform:** .NET 8.0 (C# 12) + ASP.NET Core Web API + SignalR + React 18/19
- **Sprint Cadence:** 2-Week Sprints
- **Target Release:** Version 1.0.0 Production Release

---

## 2. Epics Overview

| Epic Key | Epic Title | Module / Subsystem | Primary Objective |
| :--- | :--- | :--- | :--- |
| **EPC-01** | Multi-Role JWT Authentication & Verification | `Taska.Api`, `taska-resident`, `taska-technician` | Compound-verified resident registration, OTP via email, technician credentials, JWT Bearer RBAC. |
| **EPC-02** | Compound & Service Catalog Infrastructure | `Taska.Api`, `admin-react` | EF Core 8 entities, gated compound records, service categories, base fees, and icon assignments. |
| **EPC-03** | Resident Order Creation & Bidding Marketplace | `Taska.Api`, `taska-resident` | Maintenance request creation, image attachments, coordinate selection, and reverse bidding. |
| **EPC-04** | Technician Operations & Live GPS Tracking | `Taska.Api`, `taska-technician` | Duty status toggle, GPS location streaming, and step-by-step order progression. |
| **EPC-05** | Real-Time SignalR WebSockets Infrastructure | `Taska.Api`, All Clients | ASP.NET Core SignalR Hubs (`OrderHub`, `LocationHub`, `SupportHub`, `CompoundHub`), status events, and chat. |
| **EPC-06** | Billing, Simulated Payments & Rating Engine | `Taska.Api`, `taska-resident`, `admin-react` | Cash/Visa payment processing, automated technician rating aggregation, and refund audit. |
| **EPC-07** | Admin Operations Console & Kanban Dispatch | `admin-react`, `Taska.Api` | Executive KPI dashboard, Recharts analytics, drag-and-drop live Kanban board, user administration. |
| **EPC-08** | In-App Customer Support Helpdesk | `Taska.Api`, `taska-resident`, `admin-react` | Dedicated support chat widget, admin dispatch desk, and ticket resolution rating. |
| **EPC-09** | Dynamic Bilingual Localization (EN/AR) | `Taska.Api`, All Clients | Database-driven key-value translations, RTL layout flip, and user language persistence. |
| **EPC-10** | System Resilience & Offline Sandbox Mode | `taska-resident`, `taska-technician` | Automatic API health check and localStorage sandbox simulator for reliable offline demos. |

---

## 3. Detailed Work Breakdown Structure (Tasks & User Stories)

### Epic 1: Multi-Role JWT Authentication & Verification (`EPC-01`)

#### `TSK-101`: Resident Registration with Compound Code Validation
- **Module:** `Taska.Api` / `taska-resident`
- **Priority:** Critical
- **Status:** Done
- **Description:** Implement resident registration requiring Name, Email, Phone, Apartment Code, Password, and a valid Compound Code (e.g., `PHO01`). `AuthService` verifies compound existence in EF Core `Compounds` DbSet and dispatches a 6-digit numeric OTP with 10-minute expiry to the resident's email.
- **Acceptance Criteria:**
  1. Rejects registration if compound code does not match an active record in `Compounds`.
  2. Generates a random 6-digit OTP code stored in `User.OtpCode` with `OtpExpiresAt`.
  3. Sends email via .NET SMTP / MailKit service.
  4. Returns `201 Created` with email confirmation payload.

#### `TSK-102`: Email OTP Verification & JWT Token Issuance
- **Module:** `Taska.Api` / `taska-resident`
- **Priority:** Critical
- **Status:** Done
- **Dependencies:** `TSK-101`
- **Description:** Provide OTP verification endpoint and 6-digit input screen. Validates that the submitted code matches `OtpCode` and is within the 10-minute window, clears the OTP, and issues an ASP.NET Core JWT Bearer token.
- **Acceptance Criteria:**
  1. Throws validation error if code is invalid or expired (`DateTime.UtcNow > user.OtpExpiresAt`).
  2. Nullifies `OtpCode` and `OtpExpiresAt` upon successful verification.
  3. Issues signed JWT containing claims: `Sub`, `Email`, `Role = "Resident"`, `CompoundId`.
  4. Stores token in client `localStorage` under `taska_token`.

#### `TSK-103`: ASP.NET Core JWT Bearer & Policy-Based Authorization Setup
- **Module:** `Taska.Api`
- **Priority:** Critical
- **Status:** Done
- **Dependencies:** None
- **Description:** Configure JWT Bearer authentication in `Program.cs` (`builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(...)`). Implement role-based and policy-based authorization (`[Authorize(Roles = "Resident")]`, `[Authorize(Roles = "Technician")]`, `[Authorize(Roles = "Admin")]`).
- **Acceptance Criteria:**
  1. Validates token signing key, issuer, audience, and lifetime.
  2. Requests without valid token return `401 Unauthorized`.
  3. Role mismatch returns `403 Forbidden`.

#### `TSK-104`: Technician Multi-Identifier Login
- **Module:** `Taska.Api` / `taska-technician`
- **Priority:** High
- **Status:** Done
- **Dependencies:** `TSK-103`
- **Description:** Implement technician login endpoint accepting either registered phone number or email address with password. Issues `Technician` JWT Bearer token upon success.
- **Acceptance Criteria:**
  1. Query matches `Technicians.Phone` or `Technicians.Email`.
  2. Password checked against BCrypt hash (`BCrypt.Net.Verify`).
  3. Technician portal redirects unauthorized users to `/login`.

---

### Epic 2: Compound & Service Catalog Infrastructure (`EPC-02`)

#### `TSK-201`: Residential Compound EF Core Schema & Management CRUD
- **Module:** `Taska.Api` / `admin-react`
- **Priority:** High
- **Status:** Done
- **Dependencies:** None
- **Description:** Create `Compound` entity (`Name`, `Code` [unique], `Address`, `Phone`, `City`, `Logo`, `IsActive`) and admin UI to manage gated communities.
- **Acceptance Criteria:**
  1. Unique compound code enforced via Fluent API in `TaskaDbContext`.
  2. Admin can list, create, edit address/city, and toggle active status.
  3. Cascading delete or soft delete prevents orphaned resident associations.

#### `TSK-202`: Service Categories Catalog & Dynamic Base Pricing
- **Module:** `Taska.Api` / `admin-react` / `taska-resident`
- **Priority:** High
- **Status:** Done
- **Dependencies:** None
- **Description:** Implement `ServiceCategory` entity (`Name`, `Icon`, `IconColor`, `BasePrice`, `IsActive`, `SortOrder`). Expose public API for client apps and management endpoints for admin.
- **Acceptance Criteria:**
  1. Resident app dynamically renders categories (Electricity, Plumbing, AC, Gas, Carpentry) with base prices.
  2. Admin can configure base fee and customize icon and hex color.
  3. `GET /api/v1/categories` public endpoint cached and ordered by `SortOrder`.

---

### Epic 3: Resident Order Lifecycle & Dynamic Bidding (`EPC-03`)

#### `TSK-301`: Maintenance Request Submission with Photo Upload & Geolocation
- **Module:** `Taska.Api` / `taska-resident`
- **Priority:** Critical
- **Status:** Done
- **Dependencies:** `TSK-102`, `TSK-202`
- **Description:** Implement `CreateOrder.tsx` screen enabling residents to describe fault, upload an image attachment, select service urgency (Standard, Express, Premium), pick map coordinates, and specify payment method.
- **Acceptance Criteria:**
  1. Enforces single active order policy: returns 400 if user already has an active request (`pending`, `accepted`, `in_progress`, `arrived`).
  2. Saves uploaded image to local storage directory.
  3. Creates order with status `pending`.
  4. Broadcasts `NewOrderAvailable` over `CompoundHub` to compound technicians.
  5. Enqueues background worker push notification via FCM.

#### `TSK-302`: Technician Reverse Bidding Marketplace
- **Module:** `Taska.Api` / `taska-technician` / `taska-resident`
- **Priority:** Critical
- **Status:** Done
- **Dependencies:** `TSK-301`
- **Description:** Allow technicians in the compound to submit competitive financial bids with explanatory notes on pending orders. Allow resident to view all received offers in real-time.
- **Acceptance Criteria:**
  1. Technicians submit bid with `Amount` and optional `Notes` via `POST /api/v1/technician/orders/{id}/bid`.
  2. Unique index `['OrderId', 'TechnicianId']` prevents duplicate bids by same technician.
  3. Dispatches `NewBidReceived` to resident via `OrderHub`.
  4. Dispatches FCM push notification: `"New Offer Received 💰"`.

#### `TSK-303`: Bid Acceptance & Automatic Competitor Dismissal
- **Module:** `Taska.Api` / `taska-resident`
- **Priority:** Critical
- **Status:** Done
- **Dependencies:** `TSK-302`
- **Description:** When resident accepts a technician's bid on `OrderOffers.tsx`, execute an EF Core transaction updating order status to `accepted`, set agreed price as `TotalPrice`, mark chosen bid as `accepted`, reject all competing bids, and broadcast accept event.
- **Acceptance Criteria:**
  1. Atomic `DbContext.Database.BeginTransactionAsync()` handles bid acceptance.
  2. All other bids for this order marked `Status = "rejected"`.
  3. Order assigned to `TechnicianId` with `AcceptedAt = DateTime.UtcNow`.
  4. Broadcasts `OrderAccepted` over `OrderHub` to resident and technician.

#### `TSK-304`: Order Cancellation Flow with Audit Trail
- **Module:** `Taska.Api` / `taska-resident`
- **Priority:** Medium
- **Status:** Done
- **Dependencies:** `TSK-301`
- **Description:** Allow residents to cancel orders with a stated cancellation reason, logging the cancellation reason in `OrderStatusHistories`.
- **Acceptance Criteria:**
  1. Verifies order is in cancellable state.
  2. Updates order status to `cancelled` and records `CancellationReason`.
  3. Emits `OrderStatusUpdated` over `OrderHub` to notify technician.

---

### Epic 4: Technician Operations & Live GPS Tracking (`EPC-04`)

#### `TSK-401`: Technician Online/Offline Duty Status Toggle
- **Module:** `Taska.Api` / `taska-technician`
- **Priority:** High
- **Status:** Done
- **Dependencies:** `TSK-104`
- **Description:** Implement duty status switch on `TechHome.tsx`. Online technicians receive compound orders and broadcast availability; offline technicians receive no dispatch alerts.
- **Acceptance Criteria:**
  1. Endpoint `POST /api/v1/technician/toggle-status` toggles status between `online` and `offline`.
  2. Only online technicians in the compound receive `CompoundHub` broadcasts.
  3. Nearby technician query `GET /api/v1/user/technicians/nearby` filters by `Status == "online"`.

#### `TSK-402`: Background GPS Geolocation Tracking & Movement Logging
- **Module:** `Taska.Api` / `taska-technician`
- **Priority:** Critical
- **Status:** Done
- **Dependencies:** `TSK-401`
- **Description:** Stream technician coordinates via HTML5 `navigator.geolocation.watchPosition` with high accuracy. Post updates to `LocationHub`, persist movement to `TechnicianLocationLogs`, and broadcast to active order subscribers.
- **Acceptance Criteria:**
  1. Updates `Technician.Lat` and `Technician.Lng`.
  2. Appends entry to `TechnicianLocationLogs` with speed and heading if available.
  3. If technician has an active order, broadcasts `TechnicianLocationUpdated` to `order-{orderId}` in SignalR.
  4. Resident map updates marker coordinates without page refresh.

#### `TSK-403`: Sequential Job Execution State Machine
- **Module:** `Taska.Api` / `taska-technician`
- **Priority:** Critical
- **Status:** Done
- **Dependencies:** `TSK-303`
- **Description:** Provide UI and API for order progression: `accepted` → `arrived` (gate/door check-in) → `in_progress` (service execution) → `completed` (job finalized).
- **Acceptance Criteria:**
  1. Validates legal state transitions via `OrderService.UpdateStatusAsync()`.
  2. Sets `ArrivedAt` timestamp on `arrived` status.
  3. Sets `CompletedAt` timestamp on `completed` status and increments `CompletedOrders`.
  4. Automatically generates a pending payment record upon completion.

---

### Epic 5: Real-Time SignalR WebSockets Infrastructure (`EPC-05`)

#### `TSK-501`: ASP.NET Core SignalR Server Hubs & Group Authorization
- **Module:** `Taska.Api`
- **Priority:** Critical
- **Status:** Done
- **Dependencies:** None
- **Description:** Implement SignalR hubs (`OrderHub`, `LocationHub`, `SupportHub`, `CompoundHub`) mapped in `Program.cs`. Provide JWT Bearer query-string token extraction for WebSocket protocol negotiation.
- **Acceptance Criteria:**
  1. Hub endpoints accessible at `/hubs/order`, `/hubs/location`, `/hubs/support`, `/hubs/compound`.
  2. JWT token validated from `access_token` query parameter during WebSocket handshake.
  3. Group-based isolation prevents unauthorized message eavesdropping.

#### `TSK-502`: In-Order Live Chat between Resident and Technician
- **Module:** `Taska.Api` / `taska-resident` / `taska-technician`
- **Priority:** High
- **Status:** Done
- **Dependencies:** `TSK-501`
- **Description:** Dedicated messaging interface within order tracking and execution screens. Messages persist in `ChatMessages` table and broadcast live via `OrderHub`.
- **Acceptance Criteria:**
  1. Access restricted to the order owner and assigned technician.
  2. Messages include `SenderType` (`User` or `Technician`), `SenderId`, and `Message`.
  3. Displays live desktop/browser notification if chat modal is closed when message arrives.

---

### Epic 6: Billing, Simulated Payments & Rating Engine (`EPC-06`)

#### `TSK-601`: Automated Invoice Generation & Payment Simulation (Cash & Visa)
- **Module:** `Taska.Api` / `taska-resident` / `admin-react`
- **Priority:** High
- **Status:** Done
- **Dependencies:** `TSK-403`
- **Description:** Automatically generate a payment record upon order completion. Resident can settle payment via cash or simulate card payment. Includes admin refund capability.
- **Acceptance Criteria:**
  1. Cash payment generates transaction ID formatted `CASH-XXXXXXXXXX`.
  2. Card payment validates card parameters and generates transaction ID formatted `VISA-XXXXXXXXXXXX`.
  3. Test card ending in `0000` triggers simulated failure response.
  4. Admin can refund paid transactions via `POST /api/v1/admin/payments/{id}/refund`.

#### `TSK-602`: Customer Satisfaction 5-Star Rating & Review Engine
- **Module:** `Taska.Api` / `taska-resident` / `admin-react`
- **Priority:** Medium
- **Status:** Done
- **Dependencies:** `TSK-601`
- **Description:** Present a rating dialog upon order completion (1 to 5 stars + feedback comment). Recalculates technician's overall average rating and total review count.
- **Acceptance Criteria:**
  1. Prevents duplicate rating submissions for the same order (`OrderId` is unique in `Ratings`).
  2. Enforces rating scale between 1 and 5 stars.
  3. Recalculates arithmetic mean rounded to 2 decimal places.
  4. Admin can view ratings table and star breakdown.

---

### Epic 7: Admin Operations Console & Kanban Dispatch (`EPC-07`)

#### `TSK-701`: Executive KPI Operations Dashboard
- **Module:** `admin-react` / `Taska.Api`
- **Priority:** High
- **Status:** Done
- **Dependencies:** None
- **Description:** Build dashboard displaying key metrics: total users, technicians, orders, active jobs, revenue, online technicians, plus visual charts for monthly revenue and status breakdown using Recharts.
- **Acceptance Criteria:**
  1. Stats cards fetch data from `GET /api/v1/admin/dashboard`.
  2. Listens to `OrderHub` to increment/decrement active counters in real-time.
  3. Visualizes revenue trajectory via AreaChart and category breakdown via PieChart.

#### `TSK-702`: Real-Time Drag-and-Drop Orders Kanban Board
- **Module:** `admin-react`
- **Priority:** High
- **Status:** Done
- **Dependencies:** `TSK-701`, `TSK-501`
- **Description:** Implement `OrdersBoard.jsx` displaying columns for `Pending`, `Accepted`, `In Progress`, and `Completed`. Cards update live over SignalR and can be dragged to transition statuses.
- **Acceptance Criteria:**
  1. Columns show live count badges and colored indicator dots.
  2. Real-time SignalR listener automatically moves cards when technician updates status in the field.
  3. Clicking a card opens detailed order inspection modal.

#### `TSK-703`: Technician & Resident Directory Management
- **Module:** `admin-react` / `Taska.Api`
- **Priority:** Medium
- **Status:** Done
- **Dependencies:** None
- **Description:** Provide full CRUD interfaces for managing technicians (credentials, specialization, assigned compound, trade category) and viewing compound residents.
- **Acceptance Criteria:**
  1. Search and compound filtering supported on resident and technician tables.
  2. Admin can create new technician with encrypted password.
  3. Deletion confirmation dialog prevents accidental record removal.

---

### Epic 8: In-App Customer Support Helpdesk (`EPC-08`)

#### `TSK-801`: Resident Support Widget & Ticket Creation
- **Module:** `taska-resident` / `Taska.Api`
- **Priority:** High
- **Status:** Done
- **Dependencies:** `TSK-102`
- **Description:** Floating support widget (`SupportChatWidget.tsx`) allowing residents to report an issue, optionally linked to an order. Enforces one active support ticket per resident.
- **Acceptance Criteria:**
  1. `POST /api/v1/user/support/tickets` validates single open ticket per resident.
  2. Broadcasts `NewSupportTicket` to admin over `SupportHub`.
  3. Automatically posts automated welcome message from system.

#### `TSK-802`: Admin Helpdesk Dispatch Desk & Ticket Resolution
- **Module:** `admin-react` / `Taska.Api`
- **Priority:** High
- **Status:** Done
- **Dependencies:** `TSK-801`
- **Description:** Admin support dashboard (`SupportDashboard.jsx`) listing active and closed tickets. Admin can select a ticket, view chat history, reply in real-time, and close the session.
- **Acceptance Criteria:**
  1. Displays toast notification when new ticket arrives via `SupportHub`.
  2. Real-time two-way messaging on `SupportHub`.
  3. Closing ticket triggers prompt for resident to submit a 1–5 star rating.

---

### Epic 9: Dynamic Bilingual Localization (EN/AR) (`EPC-09`)

#### `TSK-901`: Database-Driven Translation Schema & Public API
- **Module:** `Taska.Api`
- **Priority:** High
- **Status:** Done
- **Dependencies:** None
- **Description:** Implement `Languages` and `Translations` tables via EF Core. Provide `GET /api/v1/translations?lang={code}` to return a key-value dictionary for dynamic frontend consumption.
- **Acceptance Criteria:**
  1. `Languages` table contains default records for English (`en`) and Arabic (`ar`).
  2. `Translations` table enforces unique constraint on `['LanguageCode', 'Key']`.
  3. User language preference stored in `User.Language` and `Technician.Language`.

#### `TSK-902`: Client Bilingual Context & RTL Layout Engine
- **Module:** `taska-resident` / `taska-technician` / `admin-react`
- **Priority:** High
- **Status:** Done
- **Dependencies:** `TSK-901`
- **Description:** Implement `LanguageContext` hook with `t(key)` helper. Switches HTML `dir` attribute between `ltr` and `rtl` and flips layout padding, alignment, and navigation.
- **Acceptance Criteria:**
  1. Switching to Arabic applies `dir="rtl"` to root DOM element.
  2. Language changes persist in `localStorage` and sync to backend API.
  3. Admin console includes translation editor (`Translations.jsx`) to update string values without code deployment.

---

### Epic 10: System Resilience & Offline Sandbox Mode (`EPC-10`)

#### `TSK-1001`: Client Health Check & Automatic Offline Sandbox Mode
- **Module:** `taska-resident` / `taska-technician`
- **Priority:** Medium
- **Status:** Done
- **Dependencies:** None
- **Description:** Implement ping check to `${BASE_URL}/categories` with a 1.5s timeout. If backend is offline (e.g. during an offline university demo), engage `sandbox.ts` to simulate all API responses and SignalR events in `localStorage`.
- **Acceptance Criteria:**
  1. When API is reachable, `getSandboxMode()` returns `false` and all calls hit ASP.NET Core API.
  2. When API is unreachable, `getSandboxMode()` returns `true` and mock data is served seamlessly.
  3. No unhandled promise rejections or white-screen crashes occur when backend is stopped.

---

## 4. Kanban Work Distribution

```
┌─────────────────┐ ┌───────────────┐ ┌────────────────┐ ┌────────────────┐ ┌───────────────┐
│     Backlog     │ │     To Do     │ │  In Progress   │ │  Testing / QA  │ │     Done      │
├─────────────────┼─┼───────────────┼─┼────────────────┼─┼────────────────┼─┼───────────────┤
│ TSK-1101 (Voice)│ │ TSK-903 (Auto)│ │ TSK-601 (Pay)  │ │ TSK-304 (Audit)│ │ TSK-101 (OTP) │
│ TSK-1102 (Gate) │ │ TSK-803 (Canned)│ │                │ │ TSK-702 (Kanban)│ TSK-102 (Token)│
│                 │ │               │ │                │ │ TSK-802 (Help) │ │ TSK-103 (JWT)  │
│                 │ │               │ │                │ │                │ │ TSK-104 (Tech) │
│                 │ │               │ │                │ │                │ │ TSK-201 (Comp) │
│                 │ │               │ │                │ │                │ │ TSK-202 (Cat)  │
│                 │ │               │ │                │ │                │ │ TSK-301 (Order)│
│                 │ │               │ │                │ │                │ │ TSK-302 (Bid)  │
│                 │ │               │ │                │ │                │ │ TSK-303 (Accept│
│                 │ │               │ │                │ │                │ │ TSK-401 (Duty) │
│                 │ │               │ │                │ │                │ │ TSK-402 (GPS)  │
│                 │ │               │ │                │ │                │ │ TSK-403 (State)│
│                 │ │               │ │                │ │                │ │ TSK-501 (Signal│
│                 │ │               │ │                │ │                │ │ TSK-502 (Chat) │
│                 │ │               │ │                │ │                │ │ TSK-602 (Rate) │
│                 │ │               │ │                │ │                │ │ TSK-701 (KPI)  │
│                 │ │               │ │                │ │                │ │ TSK-703 (Users)│
│                 │ │               │ │                │ │                │ │ TSK-801 (Ticket│
│                 │ │               │ │                │ │                │ │ TSK-901 (I18n) │
│                 │ │               │ │                │ │                │ │ TSK-902 (RTL)  │
│                 │ │               │ │                │ │                │ │ TSK-1001 (Sand)│
└─────────────────┘ └───────────────┘ └────────────────┘ └────────────────┘ └───────────────┘
```
