# Taska — Information Architecture & System Sitemap

This document defines the complete information architecture, page hierarchy, URL routing schemas, and screen-to-feature mapping across the three client applications of **Taska**.

---

## 1. Global System Sitemap

```mermaid
graph TD
    Root((Taska Platform))

    subgraph Resident Application [taska-resident :5173]
        R_Auth[Authentication Flow]
        R_Auth --> R_Login["/login (Login)"]
        R_Auth --> R_Reg["/register (Registration)"]
        R_Auth --> R_OTP["/verify-otp (OTP Verification)"]
        
        R_Portal[Resident Portal]
        R_Login --> R_Home["/resident-home (Catalog & Nearby Map)"]
        R_OTP --> R_Home
        
        R_Home --> R_Create["/create-order (Request Form & Pin Picker)"]
        R_Create --> R_Offers["/order-offers (Bidding Marketplace)"]
        R_Offers --> R_Track["/order-tracking (Live GPS, Chat, Pay, Rate)"]
        R_Home --> R_Track
        R_Home --> R_Profile["/profile (Settings, Lang, Theme)"]
        R_Home -.-> R_Support["SupportChatWidget (Floating Helpdesk)"]
    end

    subgraph Technician Application [taska-technician :5174]
        T_Auth[Authentication Flow]
        T_Auth --> T_Login["/login (Phone/Email Login)"]
        T_Auth --> T_OTP["/verify-otp (OTP Verification)"]
        
        T_Portal[Technician Cockpit]
        T_Login --> T_Home["/technician-home (Duty Switch, Radar Feed)"]
        T_Home --> T_Exec["/tech-execution (GPS Stream, State Engine, Chat)"]
        T_Home --> T_Profile["/profile (Performance, Settings)"]
    end

    subgraph Administrator Console [admin-react :5175]
        A_Auth["/login (Admin Sign In)"]
        A_Layout[Admin Portal Layout]
        A_Auth --> A_Layout
        
        A_Layout --> A_Dash["/ (Executive KPI Dashboard)"]
        A_Layout --> A_Board["/board (Live Drag-and-Drop Kanban)"]
        A_Layout --> A_Orders["/orders (Comprehensive Orders Directory)"]
        A_Layout --> A_Users["/users (Resident User Accounts)"]
        A_Layout --> A_Techs["/technicians (Technician Fleet Directory)"]
        A_Layout --> A_Compounds["/compounds (Gated Compounds Registry)"]
        A_Layout --> A_Cats["/categories (Service Catalog & Base Rates)"]
        A_Layout --> A_Pay["/payments (Financial Audit & Refunds)"]
        A_Layout --> A_Rate["/ratings (Satisfaction Reviews)"]
        A_Layout --> A_Supp["/support (Real-Time Customer Care Desk)"]
        A_Layout --> A_Trans["/translations (Bilingual Content Editor)"]
    end

    Root --> Resident Application
    Root --> Technician Application
    Root --> Administrator Console
```

---

## 2. Subsystem Routing Specifications

### 2.1 Resident Portal (`taska-resident`)
**Router Type:** `HashRouter` (PWA / Mobile Web Universal Compatibility)  
**Port:** `5173`

| Route Path | Page Component | Access Scope | Functional Capabilities |
| :--- | :--- | :--- | :--- |
| `#/login` | `Login.tsx` | Public | Email and password authentication; navigation to register. |
| `#/register` | `Register.tsx` | Public | Compound code validation, resident profile creation, triggers OTP. |
| `#/verify-otp` | `VerifyOtp.tsx` | Public | 6-digit numeric OTP validation; issues Bearer token. |
| `#/resident-home` | `Home.tsx` | Authenticated Resident | Category selector, online technician map, active order card, alerts. |
| `#/create-order` | `CreateOrder.tsx` | Authenticated Resident | Problem description, photo upload, map pin picking, urgency selector. |
| `#/order-offers` | `OrderOffers.tsx` | Authenticated Resident | Live bidding marketplace, technician comparisons, bid acceptance. |
| `#/order-tracking`| `OrderTracking.tsx`| Authenticated Resident | Live GPS vehicle tracking, in-order chat, payment modal, 5-star rating. |
| `#/profile` | `Profile.tsx` | Authenticated Resident | Account information, bilingual toggle (AR/EN), theme toggle, sign out. |
| *(Floating)* | `SupportChatWidget.tsx`| Authenticated Resident | In-app support ticketing and direct admin live messaging. |

---

### 2.2 Technician Field Portal (`taska-technician`)
**Router Type:** `HashRouter`  
**Port:** `5174`

| Route Path | Page Component | Access Scope | Functional Capabilities |
| :--- | :--- | :--- | :--- |
| `#/login` | `Login.tsx` | Public | Login via mobile phone number or email address + password. |
| `#/verify-otp` | `VerifyOtp.tsx` | Public | OTP verification screen. |
| `#/technician-home`| `TechHome.tsx` | Authenticated Technician | Duty online/offline switch, daily earnings, compound request radar feed, bidding drawer. |
| `#/tech-execution` | `TechExecution.tsx`| Authenticated Technician | Live GPS streaming, turn-by-turn routing, customer details, in-order chat, status progression buttons. |
| `#/profile` | `Profile.tsx` | Authenticated Technician | Trade specialization badge, rating stars, completed jobs history, logout. |

---

### 2.3 Administrator Console (`admin-react`)
**Router Type:** `BrowserRouter`  
**Port:** `5175`

| Route Path | Page Component | Access Scope | Functional Capabilities |
| :--- | :--- | :--- | :--- |
| `/login` | `Login.jsx` | Public | Administrator credentials authentication. |
| `/` | `Dashboard.jsx` | Authenticated Admin | KPI stat cards, Recharts revenue trajectories, order status pie chart. |
| `/board` | `OrdersBoard.jsx` | Authenticated Admin | Real-time drag-and-drop Kanban dispatch board across 4 status lanes. |
| `/orders` | `Orders.jsx` | Authenticated Admin | Searchable orders data table, compound filters, status history modal. |
| `/users` | `Users.jsx` | Authenticated Admin | Resident directory, apartment code registry, deletion action. |
| `/technicians` | `Technicians.jsx` | Authenticated Admin | Technician fleet manager, provisioning modal, duty status filters. |
| `/compounds` | `Compounds.jsx` | Authenticated Admin | Gated community registry, compound code CRUD, address and city fields. |
| `/categories` | `Categories.jsx` | Authenticated Admin | Service catalog grid, base price editor, icon and color assignment. |
| `/payments` | `Payments.jsx` | Authenticated Admin | Transaction ledger, revenue aggregates, payment refund action. |
| `/ratings` | `Ratings.jsx` | Authenticated Admin | Quality assurance audit, star distribution, customer feedback comments. |
| `/support` | `SupportDashboard.jsx`| Authenticated Admin | Central helpdesk queue, live two-way customer chat, ticket resolution. |
| `/translations` | `Translations.jsx` | Authenticated Admin | Dynamic bilingual dictionary editor, search and inline modification. |

---

## 3. Screen-to-Feature Mapping

| Feature Area | Resident Screen | Technician Screen | Admin Screen | Backend API Service (.NET 8) |
| :--- | :--- | :--- | :--- | :--- |
| **Authentication & Verification** | R-01, R-02, R-03 | T-01 (Login) | A-Auth | `AuthService.cs` / `AuthController.cs` |
| **Compound & Category Setup** | R-04 | T-01 | A-01, A-Cats, A-Compounds | `CompoundController.cs` |
| **Order Dispatch & Bidding** | R-05, R-06 | T-01 (Radar, Bid) | A-02 (Board), A-Orders | `OrderService.cs` / `OrderHub.cs` |
| **GPS Tracking & Navigation** | R-07 (TrackingMap) | T-02 (TrackingMap) | A-Orders (Inspection) | `LocationService.cs` / `LocationHub.cs` |
| **Job State Progression** | R-07 (Timeline) | T-02 (Action CTA) | A-02 (Live Lanes) | `OrderService.cs` / `OrderHub.cs` |
| **In-Order Messaging** | R-07 (Chat Modal) | T-02 (Chat Modal) | — | `ChatController.cs` / `OrderHub.cs` |
| **Payments & Invoicing** | R-07 (Pay Modal) | — | A-Pay (Ledger & Refund) | `PaymentService.cs` / `PaymentController.cs` |
| **Rating & Quality Control** | R-07 (Rate Modal) | T-01 (Star Display) | A-Rate (Reviews Table) | `RatingService.cs` / `RatingController.cs` |
| **Customer Support Helpdesk**| R-09 (Widget) | — | A-03 (Support Desk) | `SupportTicketController.cs` / `SupportHub.cs` |
| **Bilingual Localization** | R-08 (Lang Select)| T-03 (Lang Select) | A-04 (Translations) | `TranslationController.cs` |
