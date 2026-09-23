# TASKA — Project Management Documentation (Phased Development Roadmap)

**Project:** TASKA — Intelligent Compound Maintenance & Home Services Platform  
**Project Key:** `TASKA`  
**Methodology:** Scrum / Agile Kanban  
**Platform:** .NET 8.0 (C# 12) Web API + SignalR + React 18/19 Client PWAs  
**Current Milestone:** Midterm Progress Deliverable (In Progress)  
**Phasing Model:** **System Design & Specs Complete** · **Frontend in Phased Development** · **Backend in Phased Development**  

---

> [!IMPORTANT]
> ### 📌 Phased Implementation Roadmap & Current Status
> - **System Architecture & UI/UX Design:** ✅ **COMPLETED** (SRS, UI/UX flows, wireframes, and database models are fully finalized).
> - **Frontend Phase 1 (Foundations & Prototypes):** 🔄 **IN PROGRESS** (Navigation, RTL language context, core auth prototypes, and Offline Sandbox simulator).
> - **Frontend Phase 2 (Marketplace, Bidding & Live GPS):** 📋 **SCHEDULED (To Do)** (Technician bidding marketplace, real-time GPS tracking map, and in-order chat).
> - **Backend Phase 1 (Architecture & Auth API):** 🔄 **IN PROGRESS** (.NET 8 solution scaffolding, domain entities, and ASP.NET Core JWT Bearer authentication).
> - **Backend Phase 2 (SignalR & Services):** 📋 **SCHEDULED (To Do)** (SignalR real-time hubs, order workflow state machine, simulated payments, and ratings).

---

## 1. Workflow & Kanban Board

```
[ Backlog: 3 ] ──► [ To Do: 13 ] ──► [ In Progress: 6 ] ──► [ Testing / QA: 0 ] ──► [ Done: 6 ]
```

| Stage | Description | Count | Current Scope |
| :--- | :--- | :---: | :--- |
| **Backlog** | Secondary features not yet scheduled for active sprints. | 3 | Audio voice notes in chat, security gate pass QR generator, automated translation seeder. |
| **To Do** | Scheduled next-phase features for Frontend & .NET Backend. | 13 | Bidding marketplace, live GPS tracking, SignalR hubs, payment simulation, 5-star ratings. |
| **In Progress** | Actively being developed in the current sprint. | 6 | .NET JWT Bearer authentication, resident order submission UI, technician duty toggle, support widget. |
| **Testing / QA** | Verification stage. | 0 | Will receive features upon sprint completion. |
| **Done** | Verified foundational architecture and initial UI prototypes. | 6 | Compound schema, registration/OTP prototypes, RTL engine, admin dashboard layout, sandbox simulator. |

---

## 2. Status Distribution

> **Source:** `documentation/Project-Management/tasks.csv` (Derived strictly from active project tracking data)

| Status | Task Count | Percentage | Description |
| :--- | :---: | :---: | :--- |
| ✅ **Done (Foundations & Design)** | 6 | 21.4% | Initial UI prototypes, layout architecture, and client-side sandbox simulator |
| 🔄 **In Progress (Active Sprint)** | 6 | 21.4% | Core authentication, order request form, duty toggle, and support widget |
| 📋 **To Do (Next Phases)** | 13 | 46.4% | Reverse bidding marketplace, GPS tracking, SignalR hubs, payments, and ratings |
| 📦 **Backlog (Future Enhancements)** | 3 | 10.7% | Advanced features scheduled after core release |
| **TOTAL** | **28** | **100.0%** | **Phased Implementation in Active Progress** |

---

## 3. Priority Distribution

| Priority | Task Count | Percentage |
| :--- | :---: | :---: |
| 🔴 **Critical** | 9 | 32.1% |
| 🟠 **High** | 12 | 42.9% |
| 🟡 **Medium** | 5 | 17.9% |
| 🟢 **Low** | 2 | 7.1% |
| **TOTAL** | **28** | **100.0%** |
