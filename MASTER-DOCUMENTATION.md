# Taska — Master Documentation & Deliverables Index

**University Engineering Deliverables Submission**  
**Project:** Taska (Intelligent Compound Maintenance & Home Services Platform)  
**System Architecture:** Multi-Client Monorepo (.NET 8 ASP.NET Core Web API + SignalR Hubs + 3 React Frontends)  
**Academic Status:** Midterm Milestone Deliverables (Phase 1: Frontend Complete · Phase 2: .NET Backend In Active Development)  

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
    │   └── SRS.md                                             ← DELIVERABLE 3
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

## 3. Deliverable 3: Software Requirements Specification (SRS)

- **Official SRS Document:** [`documentation/SRS/SRS.md`](documentation/SRS/SRS.md)

### Key Content Covered:
- Formal 18-section specification adhering to international software engineering standards.
- System Overview, Stakeholder Analysis, and Role-Based Permissions Matrix (Resident vs Technician vs Administrator).
- **Functional Requirements:**
  - `FR-AUTH-01` to `03`: Resident compound registration, 6-digit email OTP verification, technician multi-identifier login.
  - `FR-ORD-01` to `04`: Order creation with active-order mutex, photo upload, coordinate selection, reverse bidding, bid acceptance, direct acceptance.
  - `FR-TRACK-01` to `03`: Online/offline duty toggle, GPS geolocation streaming (`watchPosition`), movement logging, order state transitions.
  - `FR-MSG-01`: In-order real-time chat between resident and technician.
  - `FR-SUPP-01`: In-app customer support widget, admin dispatch, and resolution satisfaction rating.
  - `FR-PAY-01`: Automated invoice creation, simulated Cash and Visa payment processing.
  - `FR-RATE-01`: 5-star customer review system with automatic technician average rating recalculation.
  - `FR-ADM-01`: Real-time drag-and-drop orders Kanban board.
- **Non-Functional Requirements:** Performance, Security (JWT Bearer role policies, BCrypt password hashing, SignalR connection handshake authorization), Reliability (EF Core database execution transactions, offline sandbox fallback), Scalability, Usability (bilingual RTL/LTR layout).
- **System Architecture & Data Persistence:** Layered architecture diagram, Entity-Relationship diagram, comprehensive data dictionary for all 15 models/tables.
- **Interface Contracts:** Complete API endpoint documentation across public, resident, technician, and admin routes.
- **Business Rules (BR-01 to BR-08):** Formal rule constraints strictly extracted from code.
- **Use Cases & User Stories:** Complete Mermaid use case diagrams and detailed interaction scenarios.
- **Future Improvements:** Gate pass QR codes, audio voice clips in chat, payment gateway webhooks.

---

## 4. Deliverable 4: UI/UX Documentation

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
