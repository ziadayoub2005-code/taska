# TASKA — Project Management Documentation

**Project:** TASKA — Intelligent Compound Maintenance & Home Services Platform  
**Project Key:** `TASKA`  
**Methodology:** Scrum / Agile Kanban  
**Platform:** .NET 8.0 (C# 12) + ASP.NET Core Web API + SignalR + React 18/19  
**Sprint Cadence:** 2-Week Sprints  
**Target Release:** Version 1.0.0 Production Release  

---

## 1. Workflow

```
[ Backlog ] ──► [ To Do ] ──► [ In Progress ] ──► [ Testing / QA ] ──► [ Done ]
```

| Stage | Description | Policy & Transition Rule |
| :--- | :--- | :--- |
| **Backlog** | Planned features & user stories not yet scheduled for an active sprint. | Product backlog grooming and prioritization. |
| **To Do** | Scheduled sprint backlog items prioritized and ready for active development. | Requirements clarified, dependencies mapped. |
| **In Progress** | Tasks actively being developed by frontend or backend engineers. | Limit WIP, feature branch created in Git. |
| **Testing / QA** | Code complete; under unit, integration, SignalR, or manual QA verification. | Automated tests pass, acceptance criteria verified. |
| **Done** | Fully tested, verified, and accepted into the release candidate baseline. | Deployed to testing/staging environment. |

---

## 2. Epics Overview

| Epic ID | Epic Name | Module / Subsystem | Tasks | Status Breakdown |
| :--- | :--- | :--- | :---: | :--- |
| **EPC-01** | Role-Based JWT Bearer Authentication & Verification | `taska-api` / `taska-resident` / `taska-technician` | 4 | 4 Done (100%) |
| **EPC-02** | Compound & Service Catalog Infrastructure | `taska-api` / `admin-react` | 2 | 2 Done (100%) |
| **EPC-03** | Resident Order Creation & Bidding Marketplace | `taska-api` / `taska-resident` / `taska-technician` | 4 | 3 Done · 1 Testing / QA |
| **EPC-04** | Technician Operations & Live GPS Tracking | `taska-api` / `taska-technician` | 4 | 3 Done · 1 Backlog |
| **EPC-05** | Real-Time SignalR Hubs Infrastructure | `taska-api` / All Clients | 3 | 2 Done · 1 Backlog |
| **EPC-06** | Billing, Simulated Payments & Rating Engine | `taska-api` / `taska-resident` / `admin-react` | 2 | 1 Done · 1 In Progress |
| **EPC-07** | Admin Operations Console & Kanban Dispatch | `admin-react` / `taska-api` | 3 | 2 Done · 1 Testing / QA |
| **EPC-08** | In-App Customer Support Helpdesk | `taska-resident` / `admin-react` / `taska-api` | 2 | 1 Done · 1 Testing / QA |
| **EPC-09** | Dynamic Bilingual Localization (EN/AR) | `taska-api` / All Clients | 3 | 2 Done · 1 To Do |
| **EPC-10** | System Resilience & Offline Sandbox Mode | `taska-resident` / `taska-technician` | 1 | 1 Done (100%) |
| **TOTAL** | | | **28** | **21 Done · 3 QA · 1 In Progress · 1 To Do · 2 Backlog** |

---

## 3. Task Structure

Each task follows this standard project-management hierarchy:

```
TASKA (Project)
└── Epic (EPC-01 → EPC-10)
    └── Task / Story (TSK-101 → TSK-1102)
        ├── Summary (Title)
        ├── Description
        ├── Module(s)
        ├── Priority (Critical / High / Medium / Low)
        ├── Status (Backlog / To Do / In Progress / Testing / QA / Done)
        ├── Acceptance Criteria (Numbered validation items)
        └── Dependencies (Prerequisite Issue Keys)
```

---

## 4. Status Distribution

> **Source:** `documentation/Project-Management/tasks.csv` (Derived strictly from source data without estimation)

| Status | Task Count | Percentage | Tasks List |
| :--- | :---: | :---: | :--- |
| ✅ **Done** | 21 | 75.0% | TSK-101, TSK-102, TSK-103, TSK-104, TSK-201, TSK-202, TSK-301, TSK-302, TSK-303, TSK-401, TSK-402, TSK-403, TSK-501, TSK-502, TSK-602, TSK-701, TSK-703, TSK-801, TSK-901, TSK-902, TSK-1001 |
| 🔬 **Testing / QA** | 3 | 10.7% | TSK-304, TSK-702, TSK-802 |
| 🔄 **In Progress** | 1 | 3.6% | TSK-601 |
| 📋 **To Do** | 1 | 3.6% | TSK-903 |
| 📦 **Backlog** | 2 | 7.1% | TSK-1101, TSK-1102 |
| **TOTAL** | **28** | **100.0%** | **All 28 Tasks Accounted For** |

---

## 5. Priority Distribution

| Priority | Task Count | Percentage | Focus Area |
| :--- | :---: | :---: | :--- |
| 🔴 **Critical** | 9 | 32.1% | Core authentication, order flow, live GPS, reverse bidding, SignalR hubs |
| 🟠 **High** | 12 | 42.9% | Catalogs, duty toggles, payments, admin dashboard, support desk, localization |
| 🟡 **Medium** | 5 | 17.9% | Order cancellation, ratings, user directory, offline sandbox, security QR pass |
| 🟢 **Low** | 2 | 7.1% | Automated key seeder, audio voice notes in chat |
| **TOTAL** | **28** | **100.0%** | |

---

## 6. Dependencies Architecture

```
TSK-101 (Resident Reg) ──► TSK-102 (OTP Verify) ──► TSK-301 (Submit Order) ──► TSK-302 (Bidding) ──► TSK-303 (Accept Bid) ──► TSK-403 (Execution) ──► TSK-601 (Billing)
                                               └──► TSK-801 (Support Widget) ──► TSK-802 (Helpdesk)  └──► TSK-304 (Cancel)                         └──► TSK-602 (Rating)
TSK-103 (JWT Config)   ──► TSK-104 (Tech Login) ──► TSK-401 (Duty Toggle) ──► TSK-402 (Live GPS)
TSK-202 (Service Cat)  ──► TSK-301 (Submit Order)
TSK-501 (SignalR Hubs) ──► TSK-502 (In-Order Chat) ──► TSK-1101 (Voice Notes)
                       └──► TSK-702 (Kanban Live)
TSK-701 (Admin KPIs)   ──► TSK-702 (Kanban Board)
TSK-901 (I18n API)     ──► TSK-902 (RTL Engine) & TSK-903 (Auto Sync)
TSK-403 (Execution)    ──► TSK-1102 (Gate Pass QR)
```

---

## 7. Deliverable Artifacts

### Trello Workspace Package (`documentation/Project-Management/Trello/`)
- `Trello-Board-Structure.md`: Board configuration, list schemas, and label taxonomy.
- `Trello-Cards.csv`: CSV import format tailored for Trello board import tools.
- `Trello-Labels.csv`: Complete label color definitions (Epics, Priorities, Modules).
- `Trello-Import-Instructions.md`: Detailed setup guide for importing into Trello.
- `TASKA-Trello-Board.html`: Fully interactive HTML Trello Board with drag-and-drop, filters, and card detail modals.

### Jira Workspace Package (`documentation/Project-Management/Jira/`)
- `Jira-Epics.csv`: Jira-ready CSV file defining all 10 project Epics.
- `Jira-Tasks.csv`: Jira-ready CSV file defining all 28 Tasks with Issue Keys, Epics, AC, and Dependencies.
- `Jira-Import.csv`: Combined Jira Cloud / Server import file with Epic-Task link hierarchy.
- `Jira-Import-Instructions.md`: Step-by-step wizard guide for Jira issue import.

### Visual Project Management Dashboard
- `TASKA-Project-Dashboard.html`: Executive engineering dashboard featuring KPI stat cards, status & priority distribution rings, epic progress bars, interactive Kanban dispatch lanes, and searchable task matrix.

---

## 8. Verification & Integrity Checklist

- [x] All 28 tasks verified against `tasks.csv` without omissions or modifications.
- [x] All 10 Epics verified against `Trello-Jira-Backlog.md`.
- [x] Exact Issue Keys preserved (`TSK-101` through `TSK-1102`).
- [x] Exact Statuses preserved (`Done: 21`, `Testing / QA: 3`, `In Progress: 1`, `To Do: 1`, `Backlog: 2`).
- [x] Exact Priorities preserved (`Critical: 9`, `High: 12`, `Medium: 5`, `Low: 2`).
- [x] All Acceptance Criteria and Dependencies mapped accurately.
- [x] Zero tasks invented; zero tasks removed.
