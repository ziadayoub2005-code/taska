# Taska — Engineering Deliverables & University Documentation

Welcome to the central documentation hub for **Taska** (**TASKA** / **taska.io**).

This directory houses all engineering specifications, system architecture documentation, project management backlogs, UI/UX interaction design maps, and repository setup instructions required for university and academic evaluation.

---

## Deliverables Directory Structure

```
documentation/
├── README.md                                    # Central documentation index (this file)
│
├── SRS/                                         # Deliverable 3: Software Requirements Specification
│   └── SRS.md                                   # Full academic IEEE/ISO-style SRS document
│
├── UI-UX/                                       # Deliverable 4: UI/UX Design & Architecture
│   ├── UI-UX.md                                 # Design tokens, color system, component catalog, screen specs
│   ├── User-Flows.md                            # Complete sequence & interaction journey maps
│   └── Sitemap.md                               # Information architecture & route mapping
│
├── Project-Management/                          # Deliverable 2: Agile Project Management (Jira / Trello)
│   ├── Trello-Jira-Backlog.md                   # Epics, User Stories, Acceptance Criteria, Kanban Board
│   └── tasks.csv                                # Direct importable CSV for Jira / Trello / Linear
│
└── Repository/                                  # Deliverable 1: Repository Technical Documentation
    └── Repository-Documentation.md              # Codebase anatomy, environment, APIs, WebSockets, DB schema
```

---

## Deliverables Summary

### 1. Repository & Technical Infrastructure Documentation
- **Location:** [Repository-Documentation.md](Repository/Repository-Documentation.md)
- **Covers:** Multi-client monorepo structure (`Taska.Api`, `taska-resident`, `taska-technician`, `admin-react`), .NET 8 & ASP.NET Core Web API backend (C# 12), Entity Framework Core 8 Code-First migrations, ASP.NET Core SignalR real-time hubs, React 18/19 & TypeScript frontends, MySQL/SQL Server database schema, environment configuration, and execution verification.

### 2. Project Management Space (Jira / Trello)
- **Documentation:** [Trello-Jira-Backlog.md](Project-Management/Trello-Jira-Backlog.md)
- **CSV Data File:** [tasks.csv](Project-Management/tasks.csv)
- **Covers:** 10 core engineering Epics (`EPC-01` to `EPC-10`), granular user stories and technical tasks, acceptance criteria, dependencies, priority classifications, and Kanban lifecycle distribution (`Backlog`, `To Do`, `In Progress`, `Testing / QA`, `Done`).

### 3. Software Requirements Specification (SRS)
- **Location:** [SRS.md](SRS/SRS.md)
- **Covers:** Formal 18-section specification detailing Introduction, Stakeholders, Roles Matrix, Functional Requirements (`FR-AUTH`, `FR-ORD`, `FR-TRACK`, `FR-MSG`, `FR-PAY`, `FR-RATE`, `FR-ADM`, `FR-SUPP`), Non-Functional Requirements, System Architecture, Database Schema with Entity-Relationship diagrams, API Endpoints, Business Rules, User Stories, Use Case Diagrams, and Future Improvements.

### 4. UI/UX Interaction & Information Architecture
- **UI/UX Specification:** [UI-UX.md](UI-UX/UI-UX.md)
- **User Flows & Journeys:** [User-Flows.md](UI-UX/User-Flows.md)
- **System Sitemap:** [Sitemap.md](UI-UX/Sitemap.md)
- **Covers:** Color palettes, design tokens, typography, screen-by-screen breakdown across all 3 client applications, component catalog, responsive mobile shell behavior, interaction flows, sequence diagrams, and route-to-feature matrices.

---

## Primary Quick References

- **Root Project README:** [../../README.md](../../README.md)
- **Master Documentation Index:** [../../MASTER-DOCUMENTATION.md](../../MASTER-DOCUMENTATION.md)
- **Process Launch Commands:** [../../run.md](../../run.md)
