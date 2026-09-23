# Trello Import Instructions

## Prerequisites
- Active Trello account
- Access to Trello.com

## Step 1 — Create Board
1. Go to trello.com → Click **Create Board**
2. Name: TASKA — Project Management
3. Visibility: Private (team) or Workspace
4. Background: Dark (recommended for professional look)

## Step 2 — Create Lists
Create the following lists in order:
1. Backlog
2. To Do
3. In Progress
4. Testing / QA
5. Done

## Step 3 — Create Labels
Create labels from Trello-Labels.csv:
- Open any card → **Labels** → **Create new label**
- Create all 18 labels with their names and colors

## Step 4 — Import Cards
**Option A — Manual (Recommended for accuracy):**
For each row in Trello-Cards.csv:
1. Create card in the list matching the **List (Status)** column
2. Set card title to: [Issue Key] Card Title
3. Open card → Add description
4. Add labels matching Epic + Priority
5. Create checklist **Acceptance Criteria** and add each criterion
6. Add **Dependencies** to card description

**Option B — CSV Import Tool:**
Use [trello-csv-importer](https://github.com/natterstefan/trello-json-to-csv) or
the official Trello CSV import via Power-Up.

## Step 5 — Organize Cards by Epic
Within each list, group cards using the ordering:
- EPC-01 group → EPC-02 group → ... → EPC-10 group

## Verification
After import, verify:
- [ ] 28 total cards across all lists
- [ ] 2 cards in Backlog (TSK-1101, TSK-1102)
- [ ] 1 card in To Do (TSK-903)
- [ ] 1 card in In Progress (TSK-601)
- [ ] 3 cards in Testing / QA (TSK-304, TSK-702, TSK-802)
- [ ] 21 cards in Done
