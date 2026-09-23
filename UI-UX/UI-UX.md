# Taska — UI/UX Specification & Design System Documentation

---

## 1. Design System Overview & Global Aesthetics

Taska features a modern, mobile-first design language for resident and technician portals, complemented by a sleek, dark-themed operations dashboard for compound administrators.

### 1.1 Visual Identity & Color Palette

The color system is defined via CSS design tokens across both light and dark themes:

```css
:root {
  /* Brand Primary Colors */
  --primary: #385270;          /* Deep Slate Teal — Core Brand Accent */
  --primary-light: #4A6B92;    /* Slate Blue Hover & Gradients */
  --primary-dark: #273A50;     /* Focused & Active State */
  
  /* Status Colors */
  --success: #10B981;          /* Emerald Green — Completed, Online, Paid */
  --warning: #F59E0B;          /* Amber — Pending Orders, In Review */
  --danger: #EF4444;           /* Coral Red — Cancelled, Failed, Delete */
  --info: #3B82F6;             /* Royal Blue — Accepted, Live Chat */
  --accent-purple: #8B5CF6;    /* Violet — In Progress Jobs, Analytics */
  --accent-cyan: #06B6D4;      /* Cyan — Arrived Status, HVAC */
  
  /* Surface & Background */
  --background: #F8FAFC;       /* Off-White / Canvas */
  --surface: #FFFFFF;          /* Pure White Container */
  --surface-alt: #F1F5F9;      /* Slate Tinted Section Background */
  --border: #E2E8F0;           /* Subtle Dividers */
  --text-main: #0F172A;        /* Slate 900 — Main Heading Typography */
  --text-muted: #64748B;       /* Slate 500 — Captions & Secondary Details */
}

/* Admin Dashboard Theme */
.app-layout {
  background-color: #0F172A;   /* Slate 900 Dark Base */
  --card-bg: #1A2235;          /* Elevated Card Surface */
  --border-dark: rgba(255, 255, 255, 0.07);
}
```

### 1.2 Typography & Iconography
- **Primary Typefaces:** Inter, Outfit, system-ui sans-serif.
- **Monospace Elements (Order IDs, Codes, Coordinates):** JetBrains Mono / SFMono.
- **Icon Libraries:** 
  - `lucide-react` (Resident & Technician apps: Bolt, Droplet, Snowflake, Paintbrush, Hammer, MapPin, Star, Send, X, Phone, etc.)
  - Google Material Icons Round (Admin Dashboard navigation and stats).

### 1.3 Viewport Shell & Responsiveness
- **Resident & Technician Portals:** Implemented with `ResponsiveLayout.tsx`. On desktop viewports (>768px), the interface centers within an ultra-clean, mobile smartphone frame container (max-width: 480px) with drop shadows, mimicking native iOS/Android experience.
- **Admin Console:** Full-width responsive desktop layout with persistent collapsible sidebar, high-density data tables, and flexbox grid charts.

---

## 2. Screen-by-Screen Detailed Specifications

### 2.1 Resident Portal Screens (`taska-resident`)

---

#### Screen R-01: Resident Login (`/login`)
- **Screen Name:** Resident Login Screen
- **Purpose:** Authenticate registered residents into their compound maintenance session.
- **Target User:** Gated compound residents.
- **Main Components:**
  - Header brand badge with Taska logo and tagline.
  - Role switcher toggle showing active "Resident" mode.
  - Input field for Email (`type="email"`).
  - Input field for Password (`type="password"` with show/hide toggle).
  - Primary button: "Sign In".
  - Secondary navigation link: "Don't have an account? Register".
- **User Actions:** Enter credentials, submit form, navigate to registration.
- **Navigation:**
  - On submit success: Navigate to `/resident-home`.
  - On register click: Navigate to `/register`.
- **Validation:** Email format validation (`email`), password required (`min:6`).
- **States:** Default, loading spinner inside button, error banner on invalid credentials.
- **Error Handling:** 401 Unauthorized displays red toast `"Invalid email or password."`.

---

#### Screen R-02: Resident Registration (`/register`)
- **Screen Name:** Compound Resident Registration
- **Purpose:** Onboard residents by validating identity against their physical compound.
- **Target User:** Prospective compound residents.
- **Main Components:**
  - Form fields:
    1. Compound Code (e.g. `PHO01` — required, validated against database).
    2. Full Name.
    3. Email Address.
    4. Mobile Phone (`+20...`).
    5. Apartment / Villa Code (e.g. `A1-402`).
    6. Password & Confirmation.
  - Primary button: "Create Account".
  - Link to login screen.
- **User Actions:** Fill compound and personal details, submit registration.
- **Navigation:** On success, transfers email state to `/verify-otp`.
- **Validation:**
  - Compound code verified against active compound records.
  - Email unique validation.
  - Phone unique validation.
  - Password strength validation.
- **States:** Default, submitting state, validation errors displayed per input field.
- **Error Handling:** Shows validation toast if compound code does not exist.

---

#### Screen R-03: Email OTP Verification (`/verify-otp`)
- **Screen Name:** Two-Step OTP Verification
- **Purpose:** Confirm resident email ownership before issuing access token.
- **Target User:** Newly registered residents.
- **Main Components:**
  - Informational prompt displaying the destination email.
  - 6-digit numeric verification input box with auto-focus.
  - Countdown timer (10 minutes remaining).
  - Primary button: "Verify & Enter".
  - Resend OTP trigger link.
- **User Actions:** Enter 6-digit code, click verify.
- **Navigation:** On successful verification, receives JWT Bearer token and redirects to `/resident-home`.
- **Validation:** Requires exact 6 numeric characters.
- **States:** Ready, verifying, expired OTP state.
- **Error Handling:** Returns toast `"Invalid or expired OTP code."` on mismatch.

---

#### Screen R-04: Resident Home & Service Catalog (`/resident-home`)
- **Screen Name:** Resident Home Dashboard
- **Purpose:** Primary hub for browsing compound services, seeing online technicians, and viewing active orders.
- **Target User:** Compound residents.
- **Main Components:**
  - **Top App Header:** Compound name badge (`Palm Hills October`), search bar, notifications bell with unread count badge.
  - **Category Quick Grid:** Visual cards for Electricity, Plumbing, Air Conditioning, Gas, and Carpentry with base rates and icons.
  - **Interactive Compound Map:** Google Maps showing nearby verified technicians currently online within compound fences.
  - **Active Orders Card / Banner:** Highlights any ongoing order with real-time status chip and direct link to track.
  - **Notifications Slide-over Panel:** Drawer displaying live alerts (new bids, tech arrival).
  - **Floating Support Chat Button:** Direct access to `SupportChatWidget.tsx`.
  - **Bottom Navigation Bar:** Tabs for Home, Active Orders, Profile.
- **User Actions:** Select maintenance category, inspect active order, view notifications, open support.
- **Navigation:**
  - Tapping category card opens `/create-order` with category state.
  - Tapping active order card opens `/order-tracking` or `/order-offers`.
- **States:** Default, loading skeleton, empty active orders state.
- **Error Handling:** Auto-reconnects to WebSocket channels if connection drops.

---

#### Screen R-05: Create Service Request (`/create-order`)
- **Screen Name:** Maintenance Request Creation
- **Purpose:** Specify problem details, attach photo, select urgency and target coordinates.
- **Target User:** Compound residents requesting repairs.
- **Main Components:**
  - Category header with base fee preview.
  - Problem description multi-line textarea with placeholder hints.
  - Service Urgency Level selector:
    - *Standard:* 20–30 min queue (Base price).
    - *Express:* 10–15 min priority (+ surcharge).
    - *Premium:* 5–10 min top-rated technician assignment.
  - Price suggestion input field (defaults to category base price).
  - Payment method toggle: Cash / Visa Card.
  - Photo attachment button with camera icon, file picker, and thumbnail preview with remove button.
  - Interactive Google Maps mini-picker with crosshair pin for apartment coordinates.
  - Fixed bottom CTA button: "Submit Request".
- **User Actions:** Describe issue, take/upload photo, set price, submit order.
- **Navigation:** On submit, redirects to `/order-offers` with newly created order ID.
- **Validation:** Description cannot be empty; checks `User::hasActiveOrder()` on server.
- **States:** Ready, uploading image, submitting, submission success popup.
- **Error Handling:** If user already has an active order, blocks submission with error modal: `"You already have an active order. Please wait for it to complete."`.

---

#### Screen R-06: Offers & Bidding Marketplace (`/order-offers`)
- **Screen Name:** Technician Offers & Bidding Screen
- **Purpose:** Review competitive price quotes from online technicians and choose an offer.
- **Target User:** Resident waiting for technician assignment.
- **Main Components:**
  - Gradient header displaying Order ID, Category, and Target Price.
  - Pulsing hourglass radar animation indicating live broadcasting to technicians.
  - Incoming Offers Feed:
    - Technician card with avatar initials.
    - Full technician name and verified badge.
    - 5-star rating display (e.g. `★ 4.8`) and completed jobs count.
    - Quoted amount in EGP (highlighted badge).
    - Technician explanatory notes quote bubble.
    - "Accept Offer" primary action button.
- **User Actions:** Review incoming offers, tap "Accept Offer".
- **Navigation:**
  - Once an offer is accepted: Transitions to `/order-tracking`.
  - If a technician directly accepts at base price: Automatically transitions to `/order-tracking` via WebSocket listener.
- **States:** Waiting for bids (radar animation), multiple bids populated, bid accepting loader.
- **Error Handling:** Prevents double acceptance if offer expires or order was cancelled.

---

#### Screen R-07: Live Order Tracking & Execution (`/order-tracking`)
- **Screen Name:** Live Tracking & In-Service Screen
- **Purpose:** Monitor assigned technician's movement on map, communicate in real time, settle payment, and rate service.
- **Target User:** Resident awaiting and receiving service.
- **Main Components:**
  - **Full Google Map View:** Shows resident's apartment pin, technician's vehicle marker, and animated DirectionsRenderer route.
  - **Trip Information Pill:** Live distance (e.g., `450 m`) and duration (e.g., `3 mins`).
  - **Technician Profile Bar:** Avatar, technician name, phone dialer button, and chat toggle button with unread message badge.
  - **Status Stepper Bar:** Horizontal timeline: `Accepted` → `Arrived` → `In Progress` → `Completed`.
  - **Live In-Order Chat Modal:** Flyout drawer for two-way messaging with technician.
  - **Cancel Order Button & Modal:** Allows cancellation with dropdown reason before work starts.
  - **Payment Settlement Modal:** Pops up on completion; supports Cash confirmation or simulated Visa card payment.
  - **5-Star Rating Modal:** Pops up after payment; contains 5 clickable stars, text feedback textarea, and submit button.
- **User Actions:** Watch live vehicle moving on map, call/chat technician, pay invoice, rate technician.
- **Navigation:** On rating submission, navigates back to `/resident-home`.
- **States:**
  - `accepted`: Showing technician moving towards compound/apartment.
  - `arrived`: Green banner `"Technician has arrived at your door!"`.
  - `in_progress`: Status chip `"Work in progress"`.
  - `completed`: Triggers payment and rating modals.
- **Error Handling:** SignalR connection reconnects automatically via `@microsoft/signalr` with exponential backoff on network interruption.

---

#### Screen R-08: Resident Profile & Settings (`/profile`)
- **Screen Name:** Resident Profile Screen
- **Purpose:** Manage account details, compound affiliation, notification toggles, language preferences, and logout.
- **Target User:** Resident.
- **Main Components:**
  - Hero Profile Card with user initials avatar, phone number, and apartment code.
  - Metric counters: Total Services Completed, Rating, Compound Code (`PHO01`).
  - Settings list:
    - Push notifications toggle switch.
    - Language selector dropdown (English / العربية with RTL toggle).
    - Dark mode / Light mode toggle.
    - Terms and Support link.
  - Destructive button: "Sign Out".
- **User Actions:** Switch language, toggle dark mode, log out.
- **Navigation:** Sign out clears `localStorage` and routes to `/login`.

---

#### Component R-09: Live Support Chat Widget (`SupportChatWidget.tsx`)
- **Component Name:** Floating Support Helpdesk Widget
- **Purpose:** On-demand customer service directly connected to compound administrators.
- **Target User:** Resident experiencing issues or needing help.
- **Main Components:**
  - Floating circular action button with message icon and unread badge.
  - Expandable modal with ticket subject, linked order selector, and initial message input.
  - Chat thread rendering system notices, resident messages (right/blue), and admin replies (left/dark).
  - Send message input field.
  - Resolution satisfaction rating prompt (1–5 stars) when ticket is closed by admin.
- **User Actions:** Raise ticket, chat with admin, submit support review.
- **States:** Minimized bubble, launcher form, active conversation, resolved/rating state.

---

### 2.2 Technician Operations Screens (`taska-technician`)

---

#### Screen T-01: Technician Home & Radar (`/technician-home`)
- **Screen Name:** Technician Job Radar & Duty Cockpit
- **Purpose:** Control duty status, view earnings, and discover maintenance requests in the compound.
- **Target User:** Stationed compound technicians.
- **Main Components:**
  - **Duty Status Bar:** Compound badge (`Palm Hills October`), Online/Offline toggle switch with pulsing status dot.
  - **Daily Performance Counters:**
    - Today's Earnings (e.g. `EGP 450`).
    - Completed Jobs Count.
    - Average Rating (e.g. `★ 4.9`).
  - **Compound Order Radar Feed:** Cards for all pending requests in the compound:
    - Category tag with color chip.
    - Resident apartment code (e.g., `Villa 12B`).
    - Problem description text.
    - Proposed price badge.
    - Two action triggers:
      1. "Accept at EGP {Price}" (Direct instant acceptance).
      2. "Place Custom Bid" (Opens bidding drawer).
  - **Bidding Bottom Sheet Modal:** Numeric input for custom offer amount, notes textarea, and submit button.
- **User Actions:** Toggle duty, place price quote, accept job directly.
- **Navigation:** On order assignment, navigates immediately to `/tech-execution`.
- **Validation:** Bidding amount must be greater than 0; cannot accept if technician already has an active order.
- **States:** Offline state (radar dormant), Online searching state, Active job banner.

---

#### Screen T-02: Technician Job Execution & Tracking (`/tech-execution`)
- **Screen Name:** Job Navigation & Execution Screen
- **Purpose:** Navigate to resident's unit, stream GPS, communicate with resident, and progress job status.
- **Target User:** Active technician on duty.
- **Main Components:**
  - **Full Google Navigation Map:** Shows route from technician's live GPS position to resident's apartment.
  - **Turn-by-turn ETA Pill:** Dynamic distance and arrival time.
  - **Resident Coordination Card:**
    - Resident name and apartment code.
    - Direct phone call button (`tel:` link).
    - In-app chat toggle button with unread counter.
  - **Lifecycle Action CTA Button:**
    - State 1 (`accepted`): Large button **"I Have Arrived"** (sets status to `arrived`).
    - State 2 (`arrived`): Large button **"Start Working"** (sets status to `in_progress`).
    - State 3 (`in_progress`): Large button **"Complete Job"** (sets status to `completed`).
  - **Job Finished Confirmation Card:** Displays completed service summary and earnings earned.
- **User Actions:** Tap progression button at each stage, chat with resident, view customer phone number.
- **Navigation:** On completion, returns to `/technician-home`.
- **States:** Step 1 (En route), Step 2 (At customer door), Step 3 (Working), Step 4 (Complete).

---

### 2.3 Administrative Console Screens (`admin-react`)

---

#### Screen A-01: Executive Operations Dashboard (`/`)
- **Screen Name:** Central Management Dashboard
- **Purpose:** High-level operational oversight across all compounds.
- **Target User:** Platform administrators and compound facility managers.
- **Main Components:**
  - **Top KPI Metric Cards:**
    - Total Registered Residents.
    - Active Technicians (with count of currently online).
    - Total Orders & Pending Orders.
    - Total Paid Revenue (in EGP).
  - **Live WebSocket Activity Indicator:** Green pulsing dot indicating live sync.
  - **Monthly Revenue Area Chart (Recharts):** Trajectory of paid revenue over time.
  - **Order Status Distribution Pie Chart:** Proportional breakdown of pending, active, completed, and cancelled requests.
  - **Recent Orders Table:** Top 10 latest requests with resident name, category, price, and status chips.
- **User Actions:** Audit system velocity, drill into detailed tables.

---

#### Screen A-02: Live Orders Kanban Board (`/board`)
- **Screen Name:** Real-Time Orders Kanban Dispatch
- **Purpose:** Visual drag-and-drop operational board tracking all compound service requests.
- **Target User:** Dispatchers and compound operations managers.
- **Main Components:**
  - Four status columns:
    1. `Pending` (Amber) — Unassigned requests awaiting bids/acceptance.
    2. `Accepted` (Blue) — Technician assigned, en route to apartment.
    3. `In Progress` (Purple) — Work underway on-site.
    4. `Completed` (Green) — Work finalized, invoice settled.
  - Draggable Order Cards: Shows Order ID, Category icon, Resident name, Technician name, Price, and timestamp.
  - WebSocket auto-sort: Cards automatically move to their new column when technicians update status on mobile.
  - Click-to-inspect modal: Full order audit trail, status history, and payment details.
- **User Actions:** Drag cards between lanes, click card to view full timeline.

---

#### Screen A-03: Support Ticket Desk (`/support`)
- **Screen Name:** Customer Service Helpdesk
- **Purpose:** Resolve resident support cases in real-time.
- **Target User:** Administrative support agents.
- **Main Components:**
  - Left pane: Ticket queue listing open cases with resident name, order link, status, and creation time.
  - Right pane: Active chat dialogue showing full message history.
  - Administrative message composer with send button.
  - "Resolve & Close Ticket" action button.
- **User Actions:** Select ticket, chat with resident, resolve issue.

---

#### Screen A-04: Multi-Language Translation Manager (`/translations`)
- **Screen Name:** Bilingual Localization Console
- **Purpose:** Manage UI text strings for English and Arabic dynamically.
- **Target User:** Platform administrators.
- **Main Components:**
  - Language tab switcher (`English` vs `العربية`).
  - Searchable key-value table: `Translation Key` (code reference) and `Translated Value` (display string).
  - Inline input fields for modifying text.
  - Save Changes primary button (syncs changes to database).
  - "Add Language" modal for provisioning additional locales.
- **User Actions:** Search strings, update translations, add languages.

---

## 3. UI Component Catalog

| Component | Found In | Description |
| :--- | :--- | :--- |
| `AppButton` | Resident / Tech | Primary, secondary, and danger button with loading spinner state and tactile feedback. |
| `AppInput` | Resident / Tech | Floating label input field with validation error indicators. |
| `Avatar` | Resident / Tech | Circular avatar rendering user initials or image URL with colored status badge. |
| `StarRating` | Resident / Admin | Interactive 1-5 clickable stars for review submission and static fractional rating display. |
| `TrackingMap` | Resident / Tech | Google Maps JavaScript API container with custom markers, polyline routing, and DirectionsRenderer. |
| `BottomSheet` | Resident / Tech | Framer Motion draggable bottom slide-up drawer for offers and bidding. |
| `AlertModal` | Resident / Tech | Global modal for critical notifications and confirmations. |
| `SupportChatWidget` | Resident | Floating expandable support chat widget. |
| `Sidebar` | Admin Console | Collapsible dark navigation drawer with Material Icons. |
| `StatCard` | Admin Console | Elevated metric card with colored icon, stat value, and comparison percentage. |

---

## 4. Responsive Behavior & Viewport Matrix

| Breakpoint | Target Device | Layout Behavior |
| :--- | :--- | :--- |
| `< 768px` | Smartphones (Mobile Web / PWA) | Native full-screen view, bottom tab navigation bar, touch-friendly tap targets (minimum 44x44px). |
| `768px - 1024px` | Tablets | Resident/Tech apps center within framed smartphone shell. Admin dashboard wraps metric cards into 2 columns. |
| `> 1024px` | Desktop Laptops & Monitors | Admin dashboard displays full 4-column metric grid and side-by-side Kanban board. Resident/Tech apps remain centered in mobile preview shell for optimal ergonomics. |
