# TASKA — Project Management Documentation (Midterm Milestone)

**Project:** TASKA — Intelligent Compound Maintenance & Home Services Platform  
**Project Key:** `TASKA`  
**Methodology:** Scrum / Agile Kanban  
**Platform:** .NET 8.0 (C# 12) Web API + SignalR + React 18/19 Client PWAs  
**Current Milestone:** Midterm Progress Deliverable (Work In Progress)  
**Implementation Phase:** **Phase 1: Frontend Clients Completed (57.1%)** · **Phase 2: .NET 8 Backend Under Construction**  

---

> [!IMPORTANT]
> ### 📌 Current Project Status
> - **Frontend Phase (16 Tasks):** ✅ **COMPLETED & VERIFIED** (`taska-resident`, `taska-technician`, and `admin-react` are fully designed, developed, and operable via **Client-Side Sandbox Simulator** for interactive UI validation and demonstration).
> - **Backend Phase (12 Tasks):** 🚧 **IN PROGRESS / SCHEDULED** (The ASP.NET Core 8 Web API, EF Core 8 models, and SignalR Hub contracts are architecturally specified; server implementation and database migrations are currently being built).

---

## 1. Workflow

```
[ Backlog: 3 ] ──► [ To Do: 7 ] ──► [ In Progress: 2 ] ──► [ Testing / QA: 0 ] ──► [ Done: 16 ]
```

| Stage | Description | Count | Focus Areas |
| :--- | :--- | :---: | :--- |
| **Backlog** | Secondary features not yet scheduled for active sprints. | 3 | Audio voice notes, QR gate pass, auto translation seeder. |
| **To Do** | Backend .NET 8 Web API endpoints & SignalR hubs ready for implementation. | 7 | SignalR OrderHub, payment simulation, categories DB, translation API. |
| **In Progress** | Core backend architecture actively being developed. | 2 | ASP.NET Core JWT Bearer authentication & sequential state machine validator. |
| **Testing / QA** | Code complete under verification. | 0 | Reserved for backend services upon sprint delivery. |
| **Done** | Fully built and verified client applications & workflows. | 16 | All 3 React frontend applications, UI screens, RTL engine, and Offline Sandbox. |

---

## 2. Status Distribution

> **Source:** `documentation/Project-Management/tasks.csv` (Derived strictly from active project tracking data)

| Status | Task Count | Percentage | Description |
| :--- | :---: | :---: | :--- |
| ✅ **Done (Frontend)** | 16 | 57.1% | Resident portal, technician PWA, admin console, UI flows, and sandbox simulator |
| 🔄 **In Progress (Backend Core)** | 2 | 7.1% | Core .NET 8 JWT Bearer auth (`TSK-103`) & Order State Machine (`TSK-403`) |
| 📋 **To Do (Backend APIs & DB)** | 7 | 25.0% | ASP.NET Core Web API endpoints, EF Core database tables, and SignalR hubs |
| 📦 **Backlog (Future Features)** | 3 | 10.7% | Gate pass QR code, audio chat notes, translation automated seeder |
| **TOTAL** | **28** | **100.0%** | **Midterm Milestone: Active Work In Progress** |

---

## 3. Epics Overview & Midterm Progress

| Epic ID | Epic Name | Module / Subsystem | Tasks | Status Breakdown |
| :--- | :--- | :--- | :---: | :--- |
| **EPC-01** | Role-Based JWT Bearer Authentication & Verification | `taska-api` / `taska-resident` / `taska-technician` | 4 | 3 Done (UI) · 1 In Progress (.NET Auth) |
| **EPC-02** | Compound & Service Catalog Infrastructure | `taska-api` / `admin-react` | 2 | 1 Done (Admin UI) · 1 To Do (Backend DB) |
| **EPC-03** | Resident Order Creation & Bidding Marketplace | `taska-api` / `taska-resident` / `taska-technician` | 4 | 3 Done (UI) · 1 To Do (Cancellation API) |
| **EPC-04** | Technician Operations & Live GPS Tracking | `taska-api` / `taska-technician` | 4 | 3 Done (UI/GPS) · 1 In Progress (Validator) |
| **EPC-05** | Real-Time SignalR Hubs Infrastructure | `taska-api` / All Clients | 3 | 2 To Do (SignalR API) · 1 Backlog |
| **EPC-06** | Billing, Simulated Payments & Rating Engine | `taska-api` / `taska-resident` / `admin-react` | 2 | 2 To Do (Backend Payment & Rating) |
| **EPC-07** | Admin Operations Console & Kanban Dispatch | `admin-react` / `taska-api` | 3 | 3 Done (Admin UI & Live Kanban) |
| **EPC-08** | In-App Customer Support Helpdesk | `taska-resident` / `admin-react` / `taska-api` | 2 | 2 Done (Resident & Admin Support UI) |
| **EPC-09** | Dynamic Bilingual Localization (EN/AR) | `taska-api` / All Clients | 3 | 1 Done (RTL Engine) · 1 To Do · 1 Backlog |
| **EPC-10** | System Resilience & Offline Sandbox Mode | `taska-resident` / `taska-technician` | 1 | 1 Done (Client-Side Simulator) |
| **TOTAL** | | | **28** | **16 Done · 2 In Progress · 7 To Do · 3 Backlog** |
