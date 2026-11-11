# Taska — Engineering Deliverables & University Documentation

Welcome to the central documentation hub for **Taska** (**TASKA** / **taska.io**).

This directory houses all engineering specifications, system architecture documentation, project management backlogs, UI/UX interaction design maps, and repository setup instructions required for university and academic evaluation.

---

## Deliverables Directory Structure

```
documentation/
├── README.md                                                      # Central documentation index (this file)
│
├── SRS/                                                           # Deliverable 3: Software Requirements Specification (MVP)
│   └── Technician-Services-Platform-SRS-MVP.pdf                   # MVP SRS specification (42 active FRs, BRs, NFRs, Open Questions)
│
├── Classes/                                                       # Domain Model & Class Specifications
│   └── Technician-Services-Platform-Classes-Specifications.pdf    # 13 MVP classes, enums, attributes, relationships, FR/BR mapping
│
├── UI-UX/                                                         # Deliverable 4: UI/UX Design & Architecture
│   ├── UI-UX.md                                                   # Design tokens, color system, component catalog, screen specs
│   ├── User-Flows.md                                              # Complete sequence & interaction journey maps
│   └── Sitemap.md                                                 # Information architecture & route mapping
│
├── Project-Management/                                            # Deliverable 2: Agile Project Management (Jira / Trello)
│   ├── Trello-Jira-Backlog.md                                     # Epics, User Stories, Acceptance Criteria, Kanban Board
│   └── tasks.csv                                                  # Direct importable CSV for Jira / Trello / Linear
│
└── Repository/                                                    # Deliverable 1: Repository Technical Documentation
    └── Repository-Documentation.md                                # Codebase anatomy, environment, APIs, WebSockets, DB schema
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

### 3. Software Requirements Specification (SRS) & Classes Specifications
- **SRS Location (PDF):** [Technician-Services-Platform-SRS-MVP.pdf](SRS/Technician-Services-Platform-SRS-MVP.pdf)
- **Classes Specifications (PDF):** [Technician-Services-Platform-Classes-Specifications.pdf](Classes/Technician-Services-Platform-Classes-Specifications.pdf)
- **Covers:** MVP vs. Later scope, Actors (`Guest`, `Customer`, `Technician`, `Admin`), 42 active Functional Requirements (`FR-01` to `FR-43`, `FR-15` removed), Business Rules (`BR-01` to `BR-18`, `BR-06` removed), Non-Functional Requirements (`NFR-01` to `NFR-07` — ASP.NET Core MVC on .NET 8, N-Tier EF Core Code First, SQL Server, ASP.NET Core Identity), Out of Scope & Open Questions (`OQ-01` to `OQ-16`), and complete domain model specifications for the 13 MVP classes and 7 enums.

### 4. UI/UX Interaction & Information Architecture
- **Master UI/UX Screenshots Dossier (PDF):** [Taska_Complete_UI-UX_Screenshots.pdf](Taska_Complete_UI-UX_Screenshots.pdf) — Complete 41-page high-resolution dossier featuring the .NET 8 backend blueprint & 39 real Retina 2x screenshots across all 3 portals.
- **UI/UX Specification:** [UI-UX.md](UI-UX/UI-UX.md)
- **User Flows & Journeys:** [User-Flows.md](UI-UX/User-Flows.md)
- **System Sitemap:** [Sitemap.md](UI-UX/Sitemap.md)
- **Covers:** Color palettes, design tokens, typography, screen-by-screen breakdown across all 3 client applications, component catalog, responsive mobile shell behavior, interaction flows, sequence diagrams, and route-to-feature matrices.

---

## Primary Quick References

- **Root Project README:** [../../README.md](../../README.md)
- **Master Documentation Index:** [../../MASTER-DOCUMENTATION.md](../../MASTER-DOCUMENTATION.md)
- **Process Launch Commands:** [../../run.md](../../run.md)
