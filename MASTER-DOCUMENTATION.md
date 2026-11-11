# Taska — Master Documentation & Deliverables Index

**University Engineering Deliverables Submission**  
**Project:** Taska (Intelligent Compound Maintenance & Home Services Platform)  
**System Architecture:** Multi-Client Monorepo (.NET 8 ASP.NET Core Web API + SignalR Hubs + 3 React Frontends)  
**Academic Status:** Progress Milestone Deliverables (System Design & Specs Complete · Phased Frontend & .NET Backend Under Active Development)

---

## Deliverables Navigation

This master document links directly to all 4 deliverables required by the university doctor, fully audited and reconstructed exclusively from the existing codebase:

```
project/
├── MASTER-DOCUMENTATION.md                                    ← (You are here)
├── README.md                                                  ← Root Repository Documentation
└── documentation/
    ├── README.md                                              ← Deliverables Overview
    ├── Repository/
    │   └── Repository-Documentation.md                        ← DELIVERABLE 1
    ├── Project-Management/
    │   ├── Trello-Jira-Backlog.md                             ← DELIVERABLE 2 (Documentation)
    │   └── tasks.csv                                          ← DELIVERABLE 2 (CSV Import File)
    ├── SRS/
    │   └── Technician-Services-Platform-SRS-MVP.pdf           ← DELIVERABLE 3 (SRS MVP)
    ├── Classes/
    │   └── Technician-Services-Platform-Classes-Specifications.pdf ← DELIVERABLE 3 (13 MVP Classes)
    └── UI-UX/
        ├── UI-UX.md                                           ← DELIVERABLE 4 (Design & Specs)
        ├── User-Flows.md                                      ← DELIVERABLE 4 (User Flows & Journeys)
        └── Sitemap.md                                         ← DELIVERABLE 4 (Sitemap & IA)
```

---

## 1. Deliverable 1: Repository & Technical Documentation

- **Primary Repository README:** [`README.md`](README.md)
- **Technical Infrastructure Document:** [`documentation/Repository/Repository-Documentation.md`](documentation/Repository/Repository-Documentation.md)

### Key Content Covered:
- Full monorepo directory anatomy (`Taska.Api`, `taska-resident`, `taska-technician`, `admin-react`).
- Core objectives and problem space: solving gated compound security and unregulated maintenance contractor entry.
- Technology stack specifications: .NET 8.0 SDK (C# 12), ASP.NET Core Web API, Entity Framework Core 8, ASP.NET Core SignalR (unified Kestrel server on port 5000/5001), React 18 & 19, TypeScript 6.0, Vite, Zustand 5.0, MySQL / SQL Server (`taska_db`).
- Complete installation and step-by-step setup guides for backend (`dotnet restore`, `dotnet ef database update`, `dotnet run`), database seeders, and all three frontend apps.
- Comprehensive inventory of all environment and configuration variables (`appsettings.json`, `.env`).
- Database schema progression across EF Core 8 Code-First migrations.
- Complete SignalR real-time Hub definitions (`/hubs/order`, `/hubs/location`, `/hubs/compound`, `/hubs/support`) and real-time events.
- Test suites, verification endpoints, and contribution protocols.

---

## 2. Deliverable 2: Project Management Space (Trello / Jira)

- **Detailed Backlog & Epics Specification:** [`documentation/Project-Management/Trello-Jira-Backlog.md`](documentation/Project-Management/Trello-Jira-Backlog.md)
- **Importable CSV File:** [`documentation/Project-Management/tasks.csv`](documentation/Project-Management/tasks.csv)

### Key Content Covered:
- Project Definition: `TASKA` (Scrum / Agile Kanban).
- **10 Concrete Engineering Epics:**
  - `EPC-01`: Role-Based JWT Bearer Authentication & Verification
  - `EPC-02`: Compound & Service Catalog Infrastructure
  - `EPC-03`: Resident Order Creation & Bidding Marketplace
  - `EPC-04`: Technician Operations & Live GPS Tracking
  - `EPC-05`: Real-Time SignalR Hubs Infrastructure
  - `EPC-06`: Billing, Simulated Payments & Rating Engine
  - `EPC-07`: Admin Operations Console & Kanban Dispatch
  - `EPC-08`: In-App Customer Support Helpdesk
  - `EPC-09`: Dynamic Bilingual Localization (EN/AR)
  - `EPC-10`: System Resilience & Offline Sandbox Mode
- Detailed User Stories with IDs, priorities, dependencies, and verifiable acceptance criteria.
- Kanban work distribution (`Backlog`, `To Do`, `In Progress`, `Testing / QA`, `Done`).
- Ready-to-import CSV mapped to standard Jira/Trello fields (`Issue Key`, `Summary`, `Description`, `Epic`, `Module`, `Priority`, `Status`, `Acceptance Criteria`, `Dependencies`).

---

## 3. Deliverable 3: Software Requirements Specification (SRS) & Classes Specifications

- **Official SRS Document (MVP):** [`SRS/Technician-Services-Platform-SRS-MVP.pdf`](SRS/Technician-Services-Platform-SRS-MVP.pdf)
- **Classes Specifications Document:** [`Classes/Technician-Services-Platform-Classes-Specifications.pdf`](Classes/Technician-Services-Platform-Classes-Specifications.pdf)

### Key Content Covered:
- **MVP vs. Later Scope:** Defines the 3-week end-to-end Customer → Technician → Admin cycle (with simulated online payment page) vs. deferred features (real payment gateway, recorded voice calls, Google Maps API, SaaS multi-company accounts, subscriptions, advanced analytics).
- **Actors:** `Guest`, `Customer`, `Technician` (independent provider with one specialty), and `Admin` (supports multiple admin accounts).
- **Functional Requirements (42 active MVP requirements, `FR-01` to `FR-43` with `FR-15` removed):**
  - `FR-01` to `FR-07`: Accounts & access (email/phone registration, email OTP verification, login/logout, guest redirect, Arabic/English UI switch).
  - `FR-08` to `FR-17`: Independent technician profile, work location, services & prices, portfolio photos, working hours, National ID & certificate upload, account review status (`Pending`, `Approved`, `Rejected`, `Suspended`), basic-data edit review flow, and post-rejection re-application.
  - `FR-18` to `FR-25`: Customer category & location selection, nearest approved technicians search, profile viewing, request submission, request listing, cancellation, 1–5 star rating, and complaints.
  - `FR-26` to `FR-32`: Technician job handling (accept/reject, start, complete with final price, cancel, complaints) and in-request text chat.
  - `FR-33` to `FR-43`: Admin review & operations (approve/reject/suspend/reactivate technicians, categories, customers, requests, reviews, complaints with action taken, statistics, multiple admins) and cash / simulated online payment recording.
- **Business Rules (`BR-01` to `BR-18`, with `BR-06` removed):** Enforces OTP verification, approval gates, one specialty per technician, basic-data edit hold (`ProfileEditRequest`), straight-line distance sorting, request state transitions, technician post-Pending cancellation counter, single post-completion review, and simulated payment handling.
- **Non-Functional Requirements (`NFR-01` to `NFR-07`):** ASP.NET Core MVC on .NET 8 with SQL Server, N-Tier layers with Entity Framework Core Code First, ASP.NET Core Identity (cookie-based auth & hashed passwords), role-based access control, Arabic RTL / English localization, and browser geolocation with zero paid external API dependencies.
- **13 MVP Classes & 7 Enums:** Full attribute, validation, relationship, and FR/BR traceability specifications (`User`, `ServiceCategory`, `TechnicianProfile`, `ProfileEditRequest`, `TechnicianDocument`, `TechnicianService`, `PortfolioPhoto`, `WorkingHour`, `ServiceRequest`, `Review`, `Payment`, `Complaint`, `ChatMessage`) plus provisional open-question (`OQ-01` to `OQ-16`) notes.

---

## 4. Deliverable 4: UI/UX Documentation

- **Master UI/UX Screenshots Dossier (PDF):** [`Taska_Complete_UI-UX_Screenshots.pdf`](Taska_Complete_UI-UX_Screenshots.pdf) (Complete 41-page Retina 2x capture + .NET 8 Architecture Blueprint)
- **Visual Design & Screen Specifications:** [`documentation/UI-UX/UI-UX.md`](documentation/UI-UX/UI-UX.md)
- **User Interaction Flows & Journey Maps:** [`documentation/UI-UX/User-Flows.md`](documentation/UI-UX/User-Flows.md)
- **Information Architecture & Sitemap:** [`documentation/UI-UX/Sitemap.md`](documentation/UI-UX/Sitemap.md)

### Key Content Covered:
- **Design System:** Visual identity tokens (Deep Slate Teal `--primary: #385270`, emerald green `--success`, amber `--warning`, dark slate `#0F172A` admin theme), typography (Inter, Outfit, SFMono), and icon sets (`lucide-react`, Google Material Icons).
- **Screen-by-Screen Specifications:**
  - Resident: Login, Register with Compound Code, 6-digit OTP verification, Home catalog with map, Create Order with photo and pin picker, Offers Bidding screen, Live Order Tracking with route distance/ETA and in-order chat, Profile with bilingual switcher, Floating Support Chat Widget.
  - Technician: Login, Duty Cockpit & Radar feed with bidding drawer, Job Execution screen with live GPS routing, resident contact, and sequential status progression buttons.
  - Administrator: Executive KPI Dashboard with Recharts, Drag-and-drop live Kanban board, Orders table with audit history modal, Resident & Technician directories, Compounds and Categories CRUD, Payment ledger with refunds, Support helpdesk, Bilingual translation editor.
- **Interaction Journeys (Mermaid Sequence Diagrams):**
  - Resident Onboarding & Compound Verification Journey
  - Maintenance Order Placement & Reverse Bidding Journey
  - Live GPS Tracking & Job Execution Lifecycle Journey
  - In-Order Real-Time Chat Journey
  - Customer Support Helpdesk Journey
  - Administrative Real-Time Kanban Dispatch Flow
- **Information Architecture:** Route-by-route sitemap covering HashRouter and BrowserRouter configurations, permissions, and screen-to-feature mapping matrix.

---

## 5. Execution Verification

All services can be initiated following the standardized terminal launch sequence from [`run.md`](run.md):

```bash
# Terminal 1 - Resident Mobile Web Portal
cd taska-resident ; npm run dev -- --host --port 5173

# Terminal 2 - Technician Field Portal
cd taska-technician ; npm run dev -- --host --port 5174

# Terminal 3 - Administrator Management Dashboard
cd admin-react ; npm run dev -- --host

# Terminal 4 - Unified ASP.NET Core 8 Web API & SignalR Server
cd Taska.Api ; dotnet run --urls "http://0.0.0.0:5000"
```
