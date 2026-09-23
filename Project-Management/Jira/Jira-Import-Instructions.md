# Jira Import Instructions

## Prerequisites
- Jira Cloud or Jira Server account
- Project Administrator role

## Step 1 — Create Project
1. Go to your Jira instance
2. Click **Projects** → **Create project**
3. Select **Scrum** or **Kanban** template
4. Project Name: TASKA
5. Project Key: TSK
6. Click **Create**

## Step 2 — Configure Workflow
Set up the following statuses in the Workflow editor:
| Status | Category |
|--------|----------|
| Backlog | To Do |
| To Do | To Do |
| In Progress | In Progress |
| Testing / QA | In Progress |
| Done | Done |

## Step 3 — Import Epics
1. Go to **Backlog** view
2. Click **Create Epic** for each row in Jira-Epics.csv
3. Use the Epic Key column for naming (EPC-01 through EPC-10)

## Step 4 — Import Tasks (CSV)
**Via Jira CSV Import:**
1. Go to **Settings** → **System** → **External System Import** → **CSV**
2. Upload Jira-Import.csv
3. Map columns:
   - Issue Type → Issue Type
   - Summary → Summary
   - Description → Description
   - Priority → Priority
   - Status → Status
   - Component → Component
   - Epic Link → Epic Link
   - Labels → Labels
   - Acceptance Criteria → Custom Field (create if not exists)
   - Depends On → Linked Issues → Depends on

## Step 5 — Set Dependencies
After import, link issues:
- TSK-102 depends on TSK-101
- TSK-104 depends on TSK-103
- TSK-301 depends on TSK-102, TSK-202
- TSK-302 depends on TSK-301
- TSK-303 depends on TSK-302
- TSK-304 depends on TSK-301
- TSK-401 depends on TSK-104
- TSK-402 depends on TSK-401
- TSK-403 depends on TSK-303
- TSK-502 depends on TSK-501
- TSK-601 depends on TSK-403
- TSK-602 depends on TSK-601
- TSK-702 depends on TSK-701, TSK-501
- TSK-801 depends on TSK-102
- TSK-802 depends on TSK-801
- TSK-902 depends on TSK-901
- TSK-903 depends on TSK-901
- TSK-1101 depends on TSK-502
- TSK-1102 depends on TSK-403

## Step 6 — Configure Board
1. Go to **Board Settings** → **Columns**
2. Set columns to match: Backlog | To Do | In Progress | Testing/QA | Done
3. Map status categories to columns

## Verification Checklist
- [ ] 10 Epics created (EPC-01 to EPC-10)
- [ ] 28 Tasks imported
- [ ] Status distribution: Done=21, Testing/QA=3, In Progress=1, To Do=1, Backlog=2
- [ ] All dependencies linked
- [ ] Issue keys preserved (TSK-101 through TSK-1102)
