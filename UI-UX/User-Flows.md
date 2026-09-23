# Taska — User Flows & Interaction Journeys

This document details the step-by-step user interaction flows and system journeys across **Taska**, derived directly from the application code, route definitions, and WebSocket event sequences.

---

## 1. Primary User Journey Maps

### Journey 1: Resident Registration & Compound Verification Flow

This flow validates that only physical residents of authorized gated compounds can access the marketplace:

```mermaid
sequenceDiagram
    autonumber
    actor Resident as Resident User
    participant App as taska-resident
    participant API as taska-api
    participant Mail as Mail Server
    participant DB as MySQL DB

    Resident->>App: Opens App & Navigates to /register
    Resident->>App: Inputs Name, Email, Phone, Apt Code, Password, & Compound Code (e.g. PHO01)
    App->>API: POST /api/v1/auth/register
    API->>DB: Query Compound Code in `compounds`
    alt Compound Code Invalid
        API-->>App: 422 Unprocessable Entity ("Invalid Compound Code")
        App-->>Resident: Displays error banner
    else Compound Code Valid
        API->>DB: Insert User (otp_code=6-digit, expires_at=now+10m)
        API->>Mail: Queue & Send OTPMail with 6-digit Code
        API-->>App: 201 Created ("OTP sent to email")
        App->>Resident: Navigates to /verify-otp screen
    end

    Resident->>App: Checks Email & Inputs 6-digit OTP
    App->>API: POST /api/v1/auth/verify-otp
    API->>DB: Check otp_code match & expiration
    alt OTP Expired or Incorrect
        API-->>App: 422 ("Invalid or expired OTP.")
        App-->>Resident: Prompts re-entry or resend
    else OTP Verified
        API->>DB: Nullify otp_code & otp_expires_at
        API->>DB: Issue JWT Bearer Access Token ('taska-user')
        API-->>App: 200 OK (Token + User Profile)
        App->>App: Persist token to localStorage
        App-->>Resident: Enters /resident-home with active compound context
    end
```

---

### Journey 2: Maintenance Request Creation & Bidding Marketplace Flow

This journey illustrates the reverse bidding model where compound technicians submit monetary offers on a resident's request:

```mermaid
sequenceDiagram
    autonumber
    actor R as Resident
    participant RApp as taska-resident
    participant API as taska-api (ASP.NET Core)
    participant SignalR as ASP.NET Core SignalR (/hubs/order)
    participant TApp as taska-technician
    actor T as Technician

    R->>RApp: Taps Service Category (e.g. Plumbing, Base: EGP 120)
    RApp-->>R: Opens /create-order screen
    R->>RApp: Enters Fault Description, Attaches Photo, Picks Map Pin, Selects Urgency
    R->>RApp: Clicks "Submit Request"
    RApp->>API: POST /api/v1/user/orders
    API->>API: Check User::hasActiveOrder()
    alt Active Order Exists
        API-->>RApp: 422 ("You already have an active order.")
        RApp-->>R: Shows AlertModal preventing duplicate orders
    else No Active Order
        API->>API: Upload photo to /storage/orders
        API->>API: Save Order (status='pending')
        API->>SignalR: Broadcast NewOrder to group `compound_{compound_id}`
        API->>API: Queue SendPushNotificationJob (FCM to online techs)
        API-->>RApp: 201 Created (Order Object)
        RApp-->>R: Transitions to /order-offers (Pulsing radar animation)
        
        SignalR-->>TApp: Receives NewOrder on group `compound_{id}`
        TApp-->>T: Updates radar feed with sound / notification badge
        T->>TApp: Reviews fault description & taps "Place Custom Bid"
        T->>TApp: Enters Quote (e.g. EGP 150) and Note ("Includes valve replacement")
        TApp->>API: POST /api/v1/technician/orders/{id}/bid
        API->>API: Save OrderBid (status='pending')
        API->>SignalR: Broadcast NewBid to group `user_{resident_id}`
        
        SignalR-->>RApp: Receives NewBid on group `user_{id}`
        RApp-->>R: Renders new technician card in offers feed live
        R->>RApp: Reviews offers (Stars, reviews, price) & clicks "Accept Offer"
        RApp->>API: POST /api/v1/user/orders/{id}/bids/{bidId}/accept
        API->>API: In DB Transaction:
        API->>API: 1. Set chosen bid to 'accepted'
        API->>API: 2. Set competing bids to 'rejected'
        API->>API: 3. Set order status='accepted', technician_id=tech.id
        API->>SignalR: Broadcast OrderAccepted on order & technician groups
        
        SignalR-->>RApp: Receives OrderAccepted
        RApp-->>R: Redirects automatically to /order-tracking
        SignalR-->>TApp: Receives OrderAccepted
        TApp-->>T: Vibrates & redirects to /tech-execution
    end
```

---

### Journey 3: Real-Time GPS Tracking & Job Execution Lifecycle Flow

This flow tracks the physical movement and operational status progression from departure to job completion:

```mermaid
sequenceDiagram
    autonumber
    actor T as Technician
    participant TApp as taska-technician
    participant API as taska-api (ASP.NET Core)
    participant SignalR as ASP.NET Core SignalR (/hubs/location & /hubs/order)
    participant RApp as taska-resident
    actor R as Resident

    Note over TApp,API: Technician is En Route to Resident's Apartment
    loop Every 5 to 15 seconds / On Movement
        TApp->>TApp: Geolocation watchPosition() captures Lat, Lng
        TApp->>API: POST /api/v1/technician/location (lat, lng, speed, heading)
        API->>API: Update technicians table & log to technician_location_logs
        API->>SignalR: Broadcast LocationUpdated on `order_{id}`
        SignalR-->>RApp: Receives coordinates
        RApp->>RApp: Google Maps marker moves smoothly; updates distance & ETA
    end

    Note over T,TApp: Stage 1: Arrival at Gate / Door
    T->>TApp: Taps "I Have Arrived"
    TApp->>API: POST /api/v1/technician/orders/{id}/status (status='arrived')
    API->>API: Set status='arrived', arrived_at=now()
    API->>SignalR: Broadcast TechnicianArrived & OrderStatusUpdated
    SignalR-->>RApp: Shows green alert: "Technician has arrived at your door!"
    RApp-->>R: Stepper moves to "Arrived"

    Note over T,TApp: Stage 2: Service Execution
    T->>TApp: Taps "Start Working"
    TApp->>API: POST /api/v1/technician/orders/{id}/status (status='in_progress')
    API->>SignalR: Broadcast OrderStatusUpdated
    SignalR-->>RApp: Stepper moves to "In Progress"

    Note over T,TApp: Stage 3: Job Completion & Billing
    T->>TApp: Taps "Finish & Complete Job"
    TApp->>API: POST /api/v1/technician/orders/{id}/status (status='completed')
    API->>API: In DB Transaction:
    API->>API: 1. Set status='completed', completed_at=now()
    API->>API: 2. Increment technician.completed_orders
    API->>API: 3. Create pending Payment record
    API->>SignalR: Broadcast OrderCompleted on `order_{id}`
    SignalR-->>TApp: Shows Job Summary screen
    SignalR-->>RApp: Opens Payment Settlement Modal

    Note over R,RApp: Stage 4: Payment & Rating
    R->>RApp: Selects Payment Method (Cash or Visa Simulation)
    RApp->>API: POST /api/v1/user/orders/{id}/pay
    API->>API: Mark payment paid, generate transaction_id
    API-->>RApp: Payment confirmed
    RApp-->>R: Closes payment modal; opens 5-Star Rating Modal
    R->>RApp: Selects 5 Stars & Enters Comment
    RApp->>API: POST /api/v1/user/orders/{id}/rate
    API->>API: Insert rating, recalculate technician average rating
    API-->>RApp: Rating saved
    RApp-->>R: Navigates back to /resident-home
```

---

### Journey 4: In-Order Direct Real-Time Chat Flow

```mermaid
sequenceDiagram
    autonumber
    actor Resident as Resident
    participant RApp as taska-resident
    participant API as taska-api (ASP.NET Core)
    participant SignalR as ASP.NET Core SignalR (/hubs/order)
    participant TApp as taska-technician
    actor Technician as Technician

    Resident->>RApp: Taps Chat Icon on /order-tracking
    RApp-->>Resident: Opens Chat Flyout Drawer
    Resident->>RApp: Types: "Gate guard needs your ID badge at Entrance 2"
    RApp->>API: POST /api/v1/user/orders/{id}/messages
    API->>API: Verify user belongs to order
    API->>API: Save in `chat_messages`
    API->>SignalR: Broadcast ChatMessage on `order_{id}`
    SignalR-->>TApp: Delivers message payload
    TApp-->>Technician: Displays chat bubble / Notification toast
    Technician->>TApp: Opens Chat & Replies: "Understood, entering Gate 2 now."
    TApp->>API: POST /api/v1/technician/orders/{id}/messages
    API->>API: Save in `chat_messages`
    API->>SignalR: Broadcast ChatMessage on `order_{id}`
    SignalR-->>RApp: Renders technician message in active thread
```

---

### Journey 5: Customer Support Helpdesk Journey

```mermaid
sequenceDiagram
    autonumber
    actor R as Resident
    participant RApp as taska-resident
    participant API as taska-api (ASP.NET Core)
    participant SignalR as ASP.NET Core SignalR (/hubs/support)
    participant AApp as admin-react
    actor Admin as Support Admin

    R->>RApp: Clicks Floating Support Widget Button
    RApp-->>R: Displays Support Form (Subject & Message)
    R->>RApp: Submits: "Technician arrived 20 minutes late and broke water valve"
    RApp->>API: POST /api/v1/user/support/tickets
    API->>API: Verify single active ticket policy
    API->>API: Create `support_tickets` record (status='open')
    API->>API: Create initial system welcome message
    API->>SignalR: Broadcast SupportTicketCreated on `admin_support`
    API-->>RApp: Returns ticket object; opens live chat view
    
    SignalR-->>AApp: Plays chime & displays toast on Admin Console
    Admin->>AApp: Navigates to /support & selects ticket
    AApp->>API: GET /api/v1/admin/support/tickets/{id}/messages
    API-->>AApp: Returns message history
    Admin->>AApp: Types response: "We are reviewing this with compound security immediately."
    AApp->>API: POST /api/v1/admin/support/tickets/{id}/messages
    API->>API: Save support message
    API->>SignalR: Broadcast SupportMessage on `support_ticket_{id}`
    SignalR-->>RApp: Delivers message directly to resident widget
    
    Note over Admin,AApp: Once Issue is Handled
    Admin->>AApp: Clicks "Resolve & Close Ticket"
    AApp->>API: POST /api/v1/admin/support/tickets/{id}/close
    API->>API: Set status='closed'
    API->>SignalR: Broadcast SupportTicketClosed on `support_ticket_{id}`
    SignalR-->>RApp: Prompts resident with 5-star Support Rating dialog
    R->>RApp: Submits 5-star support feedback
    RApp->>API: POST /api/v1/user/support/tickets/{id}/rate
    API->>API: Persist support rating
    RApp-->>R: Closes widget smoothly
```

---

### Journey 6: Administrative Operations & Real-Time Kanban Dispatch

```mermaid
graph TD
    A[Admin Logs In] --> B[Views Executive Dashboard /]
    B --> C{Selects Activity}
    C -->|Real-Time Dispatch| D[Navigates to /board]
    C -->|Community Config| E[Navigates to /compounds]
    C -->|Service Catalog| F[Navigates to /categories]
    C -->|Finance Audit| G[Navigates to /payments]
    C -->|Content Localization| H[Navigates to /translations]

    subgraph Live Kanban Board
        D --> D1[Pending Column]
        D --> D2[Accepted Column]
        D --> D3[In Progress Column]
        D --> D4[Completed Column]
        D1 -.->|Tech Accepts on Phone| D2
        D2 -.->|Tech Arrives & Starts| D3
        D3 -.->|Tech Finishes Job| D4
        D --> D5[Click Card: View Full Audit History & Timeline]
    end

    subgraph Financial Control
        G --> G1[View Revenue Stats]
        G --> G2[Filter by Status: Paid, Pending, Refunded]
        G --> G3[Click Refund: Dispatches Refund to PaymentService]
    end

    subgraph Localization Console
        H --> H1[Select Language: English / Arabic]
        H --> H2[Inline String Search & Edit]
        H --> H3[Save Changes: Updates database translations table immediately]
    end
```
