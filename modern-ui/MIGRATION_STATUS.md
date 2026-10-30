# SMARTCORE CBS Static UI Modernization — Migration Status & Inventory

> **Source Project Analysis & Master Checklist**  
> **Extracted Source:** `New_folder/New folder/` (370 ASPX files, 770 VB files)  
> **Total Connected ASPX Pages Discovered Recursively from `Main.aspx`:** **196 pages**  
> **Completed Screens (Batches 1 to 9):** **92 pages**  
> **Pending Connected Pages:** **104 pages**  
> **Status:** In Progress — Batches 1 to 9 Completed & Verified  

---

## 1. Executive Summary & Inventory Scope

| Metric | Count | Description |
|---|---|---|
| **Total ASPX Files in Source Archive** | 370 | Full repository extraction including archives and modules |
| **Total VB.NET Code-Behind Files** | 770 | Server-side business logic and ADO.NET data access |
| **Direct Menu Links in `Main.aspx`** | 151 | Directly clickable links in administrator & operator navigation |
| **Total Connected Pages (Recursive)** | **196** | Fully mapped reachable screens, dialogs, and reports |
| **Unconnected / Standalone Source Files** | 174 | Unlinked test prototypes, legacy backups, or disconnected files |
| **Redesigned Static Pages Completed** | **92** | Core Foundation, System Admin, Loans & Financial Accounting screens with full CSS/JS design system |
| **Pending Connected Pages** | **104** | Scheduled across structured operational batches |
| **Internal Navigation Links Verified** | **1,622** | 0 broken links across 37 HTML files (verified locally) |

---

## 2. Implementation Batches Status

- **Batch 1 (Completed): Core Foundation & CBS Lifecycle** (10 Screens)
  - `LoginPage.aspx` -> `login.html`
  - `Main.aspx` -> `dashboard.html` / `index.html`
  - `CustomerRegister.aspx` -> `pages/customer-register.html`
  - `AccOpen.aspx` -> `pages/account-open.html`
  - `Transaction.aspx` -> `pages/account-transaction.html`
  - `CashTr.aspx` -> `pages/cash-transfer.html`
  - `ManagerPassing.aspx` -> `pages/manager-passing.html`
  - `DepositMaster.aspx` -> `pages/deposit-master.html`
  - `LoanApplication.aspx` -> `pages/loan-application.html`
  - `DayBeginEnd.aspx` -> `pages/day-begin-end.html`

- **Batch 2 (Completed): System Administration, Masters & Parameters** (12 Screens)
  - `SchemeMaster.aspx` -> `pages/scheme-master.html` (Product schemes catalog)
  - `AccountMaster.aspx` -> `pages/account-master.html` (General Ledger heads)
  - `AccountGroupMaster.aspx` -> `pages/account-group-master.html` (GL asset/liability/income/expense groups)
  - `InterestRateMaster.aspx` -> `pages/interest-rate-master.html` (Tenure rate slabs & margins)
  - `BranchDetails.aspx` -> `pages/branch-details.html` (Branch directory & regional profiles)
  - `UserReg.aspx` -> `pages/user-reg.html` (User registration & branch mapping)
  - `GroupRights.aspx` -> `pages/group-rights.html` (Role permissions matrix)
  - `Userrights.aspx` -> `pages/user-rights.html` (User-level override rights)
  - `Deposit_Parameter.aspx` -> `pages/deposit-parameter.html` (28 deposit operational rules)
  - `LoanParameter.aspx` -> `pages/loan-parameter.html` (22 credit underwriting rules)
  - `AreaMaster.aspx` -> `pages/area-master.html` (Administrative area setup)
  - `RegionMaster.aspx` -> `pages/region-master.html` (Regional office zones)

- **Batch 3 (Completed): Loans & Credit Advances Workflow** (7 Screens)
  - `LoanApplicationForm.aspx` -> `pages/loan-application-form.html` (Application kit & fee vouchers)
  - `LoanCreditApproval.aspx` -> `pages/loan-credit-approval.html` (Credit Committee & valuation)
  - `LoanDisbursal.aspx` -> `pages/loan-disbursal.html` (Disbursement schedule & documents)
  - `GoldOrnamentsAppraiserDtlsEntry.aspx` -> `pages/gold-appraiser-entry.html` (Assayer purity & carat)
  - `DepositLoan.aspx` -> `pages/deposit-loan.html` (Lien marking & credit ceiling)
  - `LoanActionTaken.aspx` -> `pages/loan-action-taken.html` (Recovery tracking & legal notice)
  - `LoanLegalNotice.aspx` -> `pages/loan-legal-notice.html` (Overdue borrower postal manifests)

- **Batch 4 (Completed): Financial Accounting, Cash Book & General Ledger** (7 Screens)
  - `DailyReports.aspx` -> `pages/daily-reports.html` (Cash scroll, transfer list, daybook, receipts & payments)
  - `TrialBal.aspx` -> `pages/trial-balance.html` (Debit/Credit balance proof & audit summary)
  - `ProfitLoss.aspx` -> `pages/profit-loss.html` (Income vs Expenditure & operating profit)
  - `BalanceSheet.aspx` -> `pages/balance-sheet.html` (Statutory Balance Sheet schedules 1 to 11)
  - `BalanceSheetDrillingDown.aspx` -> `pages/balance-sheet-drilldown.html` (Multi-level drilldown: Schedule -> Month -> Day -> Voucher)
  - `BankBalance.aspx` -> `pages/bank-balance.html` (Commercial bank clearing accounts & liquidity)
  - `DailyCashRpt.aspx` -> `pages/daily-cash-report.html` (Vault physical cash & teller till reconciliation)

- **Batch 5 (Completed): CBS Core Reports & Enquiries** (8 Screens)
  - `DailyLoanReport.aspx` -> `pages/daily-loan-report.html` (Loan daily sanctions, disbursement & recovery)
  - `LoanDCBReport.aspx` -> `pages/loan-dcb-report.html` (Demand-Collection-Balance statement by scheme)
  - `Loan_NPA_Rpt.aspx` -> `pages/loan-npa-report.html` (Standard, Substandard, Doubtful, Loss & Provision)
  - `CibilReport.aspx` -> `pages/cibil-report.html` (CIBIL TUDF v2 monthly borrower bureau extract)
  - `DailyBusinessRpt.aspx` -> `pages/daily-business-report.html` (Day cash, transfer, clearing & turnover summary)
  - `PersonalLedger.aspx` -> `pages/personal-ledger.html` (Account statement, interest certificate & ledger scroll)
  - `CustDetRpt.aspx` -> `pages/customer-details-report.html` (Customer 360 portfolio, KYC status & active accounts)
  - `Remainder.aspx` -> `pages/remainder.html` (Overdue borrower reminder letters 1, 2, 3 & demand notices)

- **Batch 6 (Completed): Payroll & Human Resources** (8 Screens)
  - `EmpMaster.aspx` -> `pages/emp-master.html` (Employee master, designation, salary account, qualifications)
  - `EmpLeave.aspx` -> `pages/emp-leave.html` (Leave application, type CL/SL/EL, days, manager approval)
  - `EmpAttendence.aspx` -> `pages/emp-attendance.html` (Daily attendance marking, in/out timestamps, shift logs)
  - `Emp_PayScale.aspx` -> `pages/emp-payscale.html` (Pay scale configuration, increment steps, grade ceilings)
  - `BulkUploadPayrollProcess.aspx` -> `pages/salary-posting.html` (Salary batch calculation & GL expense voucher)
  - `Salarystmt.aspx` -> `pages/salary-statement.html` (Monthly payslip generation & deductions statement)
  - `AcquiantanceRpt.aspx` -> `pages/acquaintance-report.html` (Statutory acquaintance wage roll & disbursement register)
  - `EmpLeavesettings.aspx` -> `pages/emp-leave-settings.html` (Leave policy rules, accrual rates & encashment limits)

- **Batch 7 (Completed): Fixed Assets & Operational Utilities** (7 Screens)
  - `AssetMaster.aspx` -> `pages/asset-master.html` (Fixed asset registration, GL mapping, depreciation rates)
  - `AssetTransaction.aspx` -> `pages/asset-transaction.html` (Asset purchase, branch transfer & auction query modes `?RptType=PURCHASE`)
  - `AssetDepreciation_rpt.aspx` -> `pages/asset-depreciation.html` (Asset depreciation schedule calculation & GL posting)
  - `ChequeIssueMaster.aspx` -> `pages/cheque-issue.html` (Cheque book issuance, leaf range tracking & charges)
  - `Bank_Reconciliation.aspx` -> `pages/bank-reconciliation.html` (Bank reconciliation statement BRS & uncredited cheques)
  - `ChangePwd.aspx` -> `pages/change-password.html` (Operator 90-day password renewal & security guidelines)
  - `TranCancellation.aspx` -> `pages/transaction-cancellation.html` (Maker-checker voucher reversal & supervisor cancellation)

- **Batch 8 (Completed): Counter Transactions, Mandates & Clearing** (14 Screens)
  - `BankAccountMaster.aspx` -> `pages/bank-account-master.html` (Bank account master, tenure, interest rate, customer operators)
  - `BatchDtls.aspx` -> `pages/batch-details.html` (Dual debit/credit balanced tally sheet with diff ₹0.00 proof)
  - `Entry_change.aspx` -> `pages/entry-change.html` (Transaction entry mode reclassification Cash vs Transfer)
  - `TranDtls.aspx` -> `pages/transaction-details.html` (Transaction audit trail & comprehensive ledger drilldown)
  - `ChequeIssueDtls.aspx` -> `pages/cheque-issue-details.html` (Cheque leaf lifecycle audit, presented, cleared, stop payment)
  - `NEFT_IntPayment.aspx` -> `pages/neft-interest-payment.html` (Bulk deposit interest payout via NEFT / Clearing)
  - `JointAcc.aspx` -> `pages/joint-account.html` (Joint account multi-signatory mandate registry)
  - `NomDtls.aspx` -> `pages/nominee-details.html` (Nominee registration Form DA-1 & minor guardian details)
  - `GoldItems.aspx` -> `pages/gold-items.html` (Pledged gold ornaments itemized assay & karat valuation)
  - `OrnamentImg.aspx` -> `pages/ornament-image.html` (Gold vault photographic capture & security seal tag)
  - `TicketMaster.aspx` -> `pages/ticket-master.html` (Counter service ticket booking & voucher posting)
  - `ChiityDtls.aspx` -> `pages/chitty-details.html` (Chit fund auction discount schedule & prize disbursement)
  - `VerifyBnkDtls.aspx` -> `pages/verify-bank-details.html` (Penny drop ₹1 external bank verification & mandate)
  - `cust_submission.aspx` -> `pages/cust-submission.html` (Customer Form 15G / 15H TDS exemption certificate)

- **Batch 9 (Completed): Loan Portfolio, Delinquency, NPA & Executive MIS** (19 Screens)
  - `DailyCollectionReport.aspx` -> `pages/daily-collection-report.html` (Daily recovery agent scroll & collections proof)
  - `EMI_BktReport.aspx` -> `pages/emi-bucket-report.html` (SMA-0, SMA-1, SMA-2 and NPA overdue ageing buckets)
  - `EMI_DueList_Rpt.aspx` -> `pages/emi-due-list.html` (Daily installment due list & borrower SMS notifications)
  - `FluidResRpt.aspx` -> `pages/fluid-resources-report.html` (Fluid resources & Statutory Liquidity Ratio SLR proof 22.37%)
  - `GuarantorDetails_Rpt.aspx` -> `pages/guarantor-details-report.html` (Co-obligant & guarantor directory with net worth)
  - `InterestDemandRpt.aspx` -> `pages/interest-demand-report.html` (Interest demand vs collections vs recovery follow-up)
  - `InterestDueList.aspx` -> `pages/interest-due-list.html` (Statutory demand notices register & Section 13(2) dispatches)
  - `LoanOpenClose_Report.aspx` -> `pages/loan-open-close-report.html` (Loan sanction/disbursal vs closure/foreclosure register)
  - `Loan_BDR_Report.aspx` -> `pages/loan-bdr-report.html` (Bad Debt Reserve BDR & statutory provision calculation)
  - `NPARptRegionwise.aspx` -> `pages/npa-region-report.html` (NPA regional geographic distribution & branch risk grading)
  - `NPA_Prdwise_Report.aspx` -> `pages/npa-period-report.html` (NPA period-wise ageing statement & default duration)
  - `SchemeOutstandRpt.aspx` -> `pages/scheme-outstand-report.html` (Scheme-wise loan outstanding portfolio balances)
  - `GoldOrnamentsAppraiserDtlsReport.aspx` -> `pages/gold-appraiser-report.html` (Gold appraiser purity & daily bullion rate audit)
  - `GoldRedemptionReport.aspx` -> `pages/gold-redemption-report.html` (Gold redemption packet release & safe vault stock proof)
  - `LoanEMIChart.aspx` -> `pages/loan-emi-chart.html` (Amortization repayment schedule: principal, interest, balance)
  - `LoanPIForm.aspx` -> `pages/loan-pi-form.html` (Pre-inspection appraisal kit stage 2 with boundary & cash flow checks)
  - `LoanApproval.aspx` -> `pages/loan-approval.html` (Credit Committee Board sanction order resolution)
  - `CompanyPosition.aspx` -> `pages/company-position.html` (Overall bank financial position statement: CASA, CRAR, CDR)
  - `IncomeExpenseRpt.aspx` -> `pages/income-expense-report.html` (Management monthly income vs expenditure comparative MIS)

---

## 3. Complete Connected Page Inventory Checklist

### 📂 CBS Reports (2 Pages - 0 Completed)

| # | Original ASPX Page | Code-Behind (.vb) | Menu Label / Function | Static Page Target | Query Variants | Status |
|---|---|---|---|---|---|---|
| 1 | `RptPreview.aspx` | `RptPreview.aspx.vb` | RptPreview | `pages/rptpreview.html` | `RptPreView.aspx?Outputto=1`<br>`RptPreView.aspx?Outputto=2` | ⏳ Pending Batch |
| 2 | `RptView.aspx` | `RptView.aspx.vb` | RptView | `pages/rptview.html` | — | ⏳ Pending Batch |


### 📂 CBS Utilities & Tools (6 Pages - 2 Completed)

| # | Original ASPX Page | Code-Behind (.vb) | Menu Label / Function | Static Page Target | Query Variants | Status |
|---|---|---|---|---|---|---|
| 1 | `BondPrint.aspx` | `BondPrint.aspx.vb` | Duplicate Printing | `pages/bondprint.html` | — | ⏳ Pending Batch |
| 2 | `ChangePwd.aspx` | `ChangePwd.aspx.vb` | Change Password | [pages/change-password.html](pages/change-password.html) | — | ✅ **COMPLETED** |
| 3 | `ChittySuertySecurity.aspx` | `ChittySuertySecurity.aspx.vb` | Guarantor Details | `pages/chittysuertysecurity.html` | `ChittySuertySecurity.aspx?AccID=` | ⏳ Pending Batch |
| 4 | `ControlBalanceReport.aspx` | `ControlBalanceReport.aspx.vb` | Control Balance | `pages/controlbalancereport.html` | — | ⏳ Pending Batch |
| 5 | `LoginUserStatus.aspx` | `LoginUserStatus.aspx.vb` | Login User status | `pages/loginuserstatus.html` | — | ⏳ Pending Batch |
| 6 | `TranCancellation.aspx` | `TranCancellation.aspx.vb` | Transaction Cancellation | [pages/transaction-cancellation.html](pages/transaction-cancellation.html) | — | ✅ **COMPLETED** |


### 📂 Core Banking System (7 Pages - 2 Completed)

| # | Original ASPX Page | Code-Behind (.vb) | Menu Label / Function | Static Page Target | Query Variants | Status |
|---|---|---|---|---|---|---|
| 1 | `DayOpenProcess.aspx` | `DayOpenProcess.aspx.vb` | DayOpenProcess | `pages/dayopenprocess.html` | — | ⏳ Pending Batch |
| 2 | `Main.aspx` | `Main.aspx.vb` | Executive Dashboard / Main Navigation | [dashboard.html](dashboard.html) | `Main.aspx?UsrID=` | ✅ **COMPLETED** |
| 3 | `SmartLogin.aspx` | `SmartLogin.aspx.vb` | SmartLogin | `pages/smartlogin.html` | — | ⏳ Pending Batch |
| 4 | `Srch.aspx` | `Srch.aspx.vb` | Srch | `pages/srch.html` | `Srch.aspx?SrType=ACCOUNTNO`<br>`Srch.aspx?SrType=CUST` | ⏳ Pending Batch |
| 5 | `UnauthorisedAcces.aspx` | `UnauthorisedAcces.aspx.vb` | UnauthorisedAcces | `pages/unauthorisedacces.html` | — | ⏳ Pending Batch |
| 6 | `UserEmployee.aspx` | `UserEmployee.aspx.vb` | User Employee Registration | `pages/useremployee.html` | — | ⏳ Pending Batch |
| 7 | `UserReg.aspx` | `UserReg.aspx.vb` | User Creation | [pages/user-reg.html](pages/user-reg.html) | — | ✅ **COMPLETED** |


### 📂 Counter Transactions & Transfers (17 Pages - 17 Completed)

| # | Original ASPX Page | Code-Behind (.vb) | Menu Label / Function | Static Page Target | Query Variants | Status |
|---|---|---|---|---|---|---|
| 1 | `AccOpen.aspx` | `AccOpen.aspx.vb` | ACCOUNT OPEN | [pages/account-open.html](pages/account-open.html) | — | ✅ **COMPLETED** |
| 2 | `BankAccountMaster.aspx` | `BankAccountMaster.aspx.vb` | BankAccountMaster | [pages/bank-account-master.html](pages/bank-account-master.html) | — | ✅ **COMPLETED** |
| 3 | `Bank_Reconciliation.aspx` | `Bank_Reconciliation.aspx.vb` | BANK RECONCILIATION | [pages/bank-reconciliation.html](pages/bank-reconciliation.html) | — | ✅ **COMPLETED** |
| 4 | `BatchDtls.aspx` | `BatchDtls.aspx.vb` | BatchDtls | [pages/batch-details.html](pages/batch-details.html) | `BatchDtls.aspx?BatchNo=` | ✅ **COMPLETED** |
| 5 | `CashTr.aspx` | `CashTr.aspx.vb` | CASH/FUND TRANSFER | [pages/cash-transfer.html](pages/cash-transfer.html) | `CashTr.aspx?TRANID=` | ✅ **COMPLETED** |
| 6 | `ChequeIssueDtls.aspx` | `ChequeIssueDtls.aspx.vb` | ChequeIssueDtls | [pages/cheque-issue-details.html](pages/cheque-issue-details.html) | `ChequeIssueDtls.aspx?AccID=`<br>`ChequeIssueDtls.aspx?AccID=97` | ✅ **COMPLETED** |
| 7 | `ChiityDtls.aspx` | `ChiityDtls.aspx.vb` | ChiityDtls | [pages/chitty-details.html](pages/chitty-details.html) | `ChiityDtls.aspx?AccID=` | ✅ **COMPLETED** |
| 8 | `Entry_change.aspx` | `Entry_change.aspx.vb` | ENTRY MODE CHANGE | [pages/entry-change.html](pages/entry-change.html) | — | ✅ **COMPLETED** |
| 9 | `GoldItems.aspx` | `GoldItems.aspx.vb` | GoldItems | [pages/gold-items.html](pages/gold-items.html) | `GoldItems.aspx?AccID=` | ✅ **COMPLETED** |
| 10 | `JointAcc.aspx` | `JointAcc.aspx.vb` | JointAcc | [pages/joint-account.html](pages/joint-account.html) | `JointAcc.aspx?AccID=` | ✅ **COMPLETED** |
| 11 | `LoanActionTaken.aspx` | `LoanActionTaken.aspx.vb` | LOAN ACTION TAKEN ENTRY | [pages/loan-action-taken.html](pages/loan-action-taken.html) | — | ✅ **COMPLETED** |
| 12 | `NEFT_IntPayment.aspx` | `NEFT_IntPayment.aspx.vb` | DEPOSIT BULK INT.POST | [pages/neft-interest-payment.html](pages/neft-interest-payment.html) | — | ✅ **COMPLETED** |
| 13 | `NomDtls.aspx` | `NomDtls.aspx.vb` | NomDtls | [pages/nominee-details.html](pages/nominee-details.html) | `NomDtls.aspx?AccID=` | ✅ **COMPLETED** |
| 14 | `OrnamentImg.aspx` | `OrnamentImg.aspx.vb` | OrnamentImg | [pages/ornament-image.html](pages/ornament-image.html) | — | ✅ **COMPLETED** |
| 15 | `TicketMaster.aspx` | `TicketMaster.aspx.vb` | TICKET MASTER | [pages/ticket-master.html](pages/ticket-master.html) | — | ✅ **COMPLETED** |
| 16 | `TranDtls.aspx` | `TranDtls.aspx.vb` | TranDtls | [pages/transaction-details.html](pages/transaction-details.html) | `TranDtls.aspx?AccID=` | ✅ **COMPLETED** |
| 17 | `Transaction.aspx` | `Transaction.aspx.vb` | ACCOUNT TRANSACTION | [pages/account-transaction.html](pages/account-transaction.html) | `Transaction.aspx?TRANID=` | ✅ **COMPLETED** |


### 📂 Customer & Membership (8 Pages - 3 Completed)

| # | Original ASPX Page | Code-Behind (.vb) | Menu Label / Function | Static Page Target | Query Variants | Status |
|---|---|---|---|---|---|---|
| 1 | `CommonImage.aspx` | `CommonImage.aspx.vb` | CommonImage | `pages/commonimage.html` | — | ⏳ Pending Batch |
| 2 | `CustImage.aspx` | `CustImage.aspx.vb` | CustImage | `pages/custimage.html` | `CustImage.aspx?CustID=` | ⏳ Pending Batch |
| 3 | `CustImg.aspx` | `CustImg.aspx.vb` | CustImg | `pages/custimg.html` | — | ⏳ Pending Batch |
| 4 | `CustomerRegister.aspx` | `CustomerRegister.aspx.vb` | CUSTOMER REGISTER | [pages/customer-register.html](pages/customer-register.html) | — | ✅ **COMPLETED** |
| 5 | `IMG_Upload.aspx` | `IMG_Upload.aspx.vb` | IMG_Upload | `pages/img_upload.html` | — | ⏳ Pending Batch |
| 6 | `VerifyBnkDtls.aspx` | `VerifyBnkDtls.aspx.vb` | CUSTOMER BANK DETAILS | [pages/verify-bank-details.html](pages/verify-bank-details.html) | — | ✅ **COMPLETED** |
| 7 | `WebCame.aspx` | `WebCame.aspx.vb` | WebCame | `pages/webcame.html` | — | ⏳ Pending Batch |
| 8 | `cust_submission.aspx` | `cust_submission.aspx.vb` | CUSTOMER 15-H SUBMISSION | [pages/cust-submission.html](pages/cust-submission.html) | — | ✅ **COMPLETED** |


### 📂 Customer & Membership Reports (6 Pages - 2 Completed)

| # | Original ASPX Page | Code-Behind (.vb) | Menu Label / Function | Static Page Target | Query Variants | Status |
|---|---|---|---|---|---|---|
| 1 | `15H_Rpt.aspx` | `15H_Rpt.aspx.vb` | 15-G Report | `pages/15h_rpt.html` | — | ⏳ Pending Batch |
| 2 | `CustAssetLiabRpt.aspx` | `CustAssetLiabRpt.aspx.vb` | CustAssetLiabRpt | `pages/custassetliabrpt.html` | — | ⏳ Pending Batch |
| 3 | `CustDetRpt.aspx` | `CustDetRpt.aspx.vb` | Customer Details Report | [pages/customer-details-report.html](pages/customer-details-report.html) | — | ✅ **COMPLETED** |
| 4 | `NotSubmitPanCard.aspx` | `NotSubmitPanCard.aspx.vb` | Pan Card Report | `pages/notsubmitpancard.html` | — | ⏳ Pending Batch |
| 5 | `PersonalLedger.aspx` | `PersonalLedger.aspx.vb` | PERSONAL LEDGER | [pages/personal-ledger.html](pages/personal-ledger.html) | — | ✅ **COMPLETED** |
| 6 | `cust_reg_Report.aspx` | `cust_reg_Report.aspx.vb` | Customer Register Report | `pages/cust_reg_report.html` | — | ⏳ Pending Batch |


### 📂 Day Begin & Day End Settlement (4 Pages - 2 Completed)

| # | Original ASPX Page | Code-Behind (.vb) | Menu Label / Function | Static Page Target | Query Variants | Status |
|---|---|---|---|---|---|---|
| 1 | `DayBeginEnd.aspx` | `DayBeginEnd.aspx.vb` | Daybegin | [pages/day-begin-end.html](pages/day-begin-end.html) | — | ✅ **COMPLETED** |
| 2 | `DayEndStatus.aspx` | `DayEndStatus.aspx.vb` | Branch DayEnd Status | `pages/dayendstatus.html` | — | ⏳ Pending Batch |
| 3 | `EmpAttendence.aspx` | `EmpAttendence.aspx.vb` | Attendence Marking | [pages/emp-attendance.html](pages/emp-attendance.html) | — | ✅ **COMPLETED** |
| 4 | `NoticePrinting.aspx` | `NoticePrinting.aspx.vb` | Notice Printing | `pages/noticeprinting.html` | — | ⏳ Pending Batch |


### 📂 Debenture & Bonds Reports (3 Pages - 0 Completed)

| # | Original ASPX Page | Code-Behind (.vb) | Menu Label / Function | Static Page Target | Query Variants | Status |
|---|---|---|---|---|---|---|
| 1 | `DepositReports.aspx` | `DepositReports.aspx.vb` | Maturity Report | `pages/depositreports.html` | — | ⏳ Pending Batch |
| 2 | `DepsitSeriesOutstandRpt.aspx` | `DepsitSeriesOutstandRpt.aspx.vb` | Debenture Series Outstand | `pages/depsitseriesoutstandrpt.html` | — | ⏳ Pending Batch |
| 3 | `NominationRegisterRpt.aspx` | `NominationRegisterRpt.aspx.vb` | Nominee Details | `pages/nominationregisterrpt.html` | — | ⏳ Pending Batch |


### 📂 Deposit Products & Operations (3 Pages - 1 Completed)

| # | Original ASPX Page | Code-Behind (.vb) | Menu Label / Function | Static Page Target | Query Variants | Status |
|---|---|---|---|---|---|---|
| 1 | `DepositMaster.aspx` | `DepositMaster.aspx.vb` | DepositMaster | [pages/deposit-master.html](pages/deposit-master.html) | `DepositMaster.aspx?TRANID=` | ✅ **COMPLETED** |
| 2 | `GroupDeposit.aspx` | `GroupDeposit.aspx.vb` | GroupDeposit | `pages/groupdeposit.html` | — | ⏳ Pending Batch |
| 3 | `NomineeMaster.aspx` | `NomineeMaster.aspx.vb` | NomineeMaster | `pages/nomineemaster.html` | — | ⏳ Pending Batch |


### 📂 Executive MIS Reports (3 Pages - 0 Completed)

| # | Original ASPX Page | Code-Behind (.vb) | Menu Label / Function | Static Page Target | Query Variants | Status |
|---|---|---|---|---|---|---|
| 1 | `LoanRisk.aspx` | `LoanRisk.aspx.vb` | GOlD Analysis | `pages/loanrisk.html` | — | ⏳ Pending Batch |
| 2 | `MIS_Report.aspx` | `MIS_Report.aspx.vb` | MIS Analysis | `pages/mis_report.html` | `MIS_Report.aspx?Type=MisReport` | ⏳ Pending Batch |
| 3 | `MIS_setting.aspx` | `MIS_setting.aspx.vb` | MIS Settings | `pages/mis_setting.html` | — | ⏳ Pending Batch |


### 📂 Financial & GL Accounts Reports (11 Pages - 7 Completed)

| # | Original ASPX Page | Code-Behind (.vb) | Menu Label / Function | Static Page Target | Query Variants | Status |
|---|---|---|---|---|---|---|
| 1 | `BalanceSheet.aspx` | `BalanceSheet.aspx.vb` | Balalnce Sheet | [pages/balance-sheet.html](pages/balance-sheet.html) | — | ✅ **COMPLETED** |
| 2 | `BalanceSheetDrillingDown.aspx` | `BalanceSheetDrillingDown.aspx.vb` | Balalnce Drill Down | [pages/balance-sheet-drilldown.html](pages/balance-sheet-drilldown.html) | — | ✅ **COMPLETED** |
| 3 | `BankBalance.aspx` | `BankBalance.aspx.vb` | Bank Balance Report | [pages/bank-balance.html](pages/bank-balance.html) | — | ✅ **COMPLETED** |
| 4 | `CmpFinalAcc_Frm.aspx` | `CmpFinalAcc_Frm.aspx.vb` | Balalnce Sheet Comparison | `pages/cmpfinalacc_frm.html` | — | ⏳ Pending Batch |
| 5 | `DailyCashRpt.aspx` | `DailyCashRpt.aspx.vb` | Cash Balance Report | [pages/daily-cash-report.html](pages/daily-cash-report.html) | — | ✅ **COMPLETED** |
| 6 | `DailyReports.aspx` | `DailyReports.aspx.vb` | Cash Book | [pages/daily-reports.html](pages/daily-reports.html) | — | ✅ **COMPLETED** |
| 7 | `DayEndRep.aspx` | `DayEndRep.aspx.vb` | DayEndRep | `pages/dayendrep.html` | `DayEndRep.aspx?PageType=130` | ⏳ Pending Batch |
| 8 | `ProfitLoss.aspx` | `ProfitLoss.aspx.vb` | Profit & Loss | [pages/profit-loss.html](pages/profit-loss.html) | — | ✅ **COMPLETED** |
| 9 | `TranListGroupWise_Rpt.aspx` | `TranListGroupWise_Rpt.aspx.vb` | Transaction Report(Group) | `pages/tranlistgroupwise_rpt.html` | — | ⏳ Pending Batch |
| 10 | `Transactionlist.aspx` | `Transactionlist.aspx.vb` | Transaction report | `pages/transactionlist.html` | — | ⏳ Pending Batch |
| 11 | `TrialBal.aspx` | `TrialBal.aspx.vb` | Trial Balance | [pages/trial-balance.html](pages/trial-balance.html) | — | ✅ **COMPLETED** |


### 📂 Fixed Assets & Inventory (11 Pages - 4 Completed)

| # | Original ASPX Page | Code-Behind (.vb) | Menu Label / Function | Static Page Target | Query Variants | Status |
|---|---|---|---|---|---|---|
| 1 | `AssetDepreciation_rpt.aspx` | `AssetDepreciation_rpt.aspx.vb` | Depreciation | [pages/asset-depreciation.html](pages/asset-depreciation.html) | — | ✅ **COMPLETED** |
| 2 | `AssetGroup.aspx` | `AssetGroup.aspx.vb` | Asset Group | `pages/assetgroup.html` | — | ⏳ Pending Batch |
| 3 | `AssetLiab.aspx` | `AssetLiab.aspx.vb` | AssetLiab | `pages/assetliab.html` | `AssetLiab.aspx?CustID=`<br>`AssetLiab.aspx?CustID=<%HdnSurCustID.ClientID%>` | ⏳ Pending Batch |
| 4 | `AssetMaster.aspx` | `AssetMaster.aspx.vb` | Asset Master | [pages/asset-master.html](pages/asset-master.html) | — | ✅ **COMPLETED** |
| 5 | `AssetRegister_rpt1.aspx` | `AssetRegister_rpt1.aspx.vb` | Asset Register | `pages/assetregister_rpt1.html` | — | ⏳ Pending Batch |
| 6 | `AssetStock_rpt.aspx` | `AssetStock_rpt.aspx.vb` | Asset Stock | `pages/assetstock_rpt.html` | — | ⏳ Pending Batch |
| 7 | `AssetTransaction.aspx` | `AssetTransaction.aspx.vb` | PURCHASE | [pages/asset-transaction.html](pages/asset-transaction.html) | `AssetTransaction.aspx?RptType=AUCTION`<br>`AssetTransaction.aspx?RptType=DAMAGE` | ✅ **COMPLETED** |
| 8 | `Depreciation_Rpt.aspx` | `Depreciation_Rpt.aspx.vb` | Depreciation | `pages/depreciation_rpt.html` | — | ⏳ Pending Batch |
| 9 | `ManagerPassing.aspx` | `ManagerPassing.aspx.vb` | AUTHORISATION | [pages/manager-passing.html](pages/manager-passing.html) | `ManagerPassing.aspx?Passing=y` | ✅ **COMPLETED** |
| 10 | `PostDepreciation.aspx` | `PostDepreciation.aspx.vb` | Depreciation Post/Report | `pages/postdepreciation.html` | — | ⏳ Pending Batch |
| 11 | `ShareTransfer.aspx` | `ShareTransfer.aspx.vb` | ShareTransfer | `pages/sharetransfer.html` | `ShareTransfer.aspx?TRANID=` | ⏳ Pending Batch |


### 📂 Gold Loan Reports (8 Pages - 3 Completed)

| # | Original ASPX Page | Code-Behind (.vb) | Menu Label / Function | Static Page Target | Query Variants | Status |
|---|---|---|---|---|---|---|
| 1 | `GoldLoanOpenClose_Report.aspx.aspx` | `GoldLoanOpenClose_Report.aspx.aspx.vb` | Gold Opening Closing Report | `pages/goldloanopenclose_report.html.html` | — | ⏳ Pending Batch |
| 2 | `GoldOrnamentsAppraiserDtlsEntry.aspx` | `GoldOrnamentsAppraiserDtlsEntry.aspx.vb` | Gold Ornaments Details | [pages/gold-appraiser-entry.html](pages/gold-appraiser-entry.html) | — | ✅ **COMPLETED** |
| 3 | `GoldOrnamentsAppraiserDtlsReport.aspx` | `GoldOrnamentsAppraiserDtlsReport.aspx.vb` | Gold Ornaments Appraiser Report | [pages/gold-appraiser-report.html](pages/gold-appraiser-report.html) | — | ✅ **COMPLETED** |
| 4 | `GoldRedemptionReport.aspx` | `GoldRedemptionReport.aspx.vb` | Stock Report | [pages/gold-redemption-report.html](pages/gold-redemption-report.html) | — | ✅ **COMPLETED** |
| 5 | `InterestPayable.aspx` | `InterestPayable.aspx.vb` | Interest Receiavable Report | `pages/interestpayable.html` | `InterestPayable.aspx?RptType=Receivable` | ⏳ Pending Batch |
| 6 | `LoanEMIRpt.aspx` | `LoanEMIRpt.aspx.vb` | Loan EMI Report | `pages/loanemirpt.html` | — | ⏳ Pending Batch |
| 7 | `LoanReports.aspx` | `LoanReports.aspx.vb` | Overdue Report | `pages/loanreports.html` | — | ⏳ Pending Batch |
| 8 | `RptOutstanding.aspx` | `RptOutstanding.aspx.vb` | Outstanding Report | `pages/rptoutstanding.html` | — | ⏳ Pending Batch |


### 📂 Head Office & Management Reports (14 Pages - 6 Completed)

| # | Original ASPX Page | Code-Behind (.vb) | Menu Label / Function | Static Page Target | Query Variants | Status |
|---|---|---|---|---|---|---|
| 1 | `BhartiAxa.aspx` | `BhartiAxa.aspx.vb` | Bharti Axa-Insurance Report | `pages/bhartiaxa.html` | — | ⏳ Pending Batch |
| 2 | `CanvassedBy_Rpt.aspx` | `CanvassedBy_Rpt.aspx.vb` | Canvasser Report | `pages/canvassedby_rpt.html` | — | ⏳ Pending Batch |
| 3 | `ChequeIssueMaster.aspx` | `ChequeIssueMaster.aspx.vb` | Cheque Issue | [pages/cheque-issue.html](pages/cheque-issue.html) | — | ✅ **COMPLETED** |
| 4 | `ChequeReturn.aspx` | `ChequeReturn.aspx.vb` | Cheque Return | `pages/chequereturn.html` | — | ⏳ Pending Batch |
| 5 | `CompanyPosition.aspx` | `CompanyPosition.aspx.vb` | Company Position | [pages/company-position.html](pages/company-position.html) | — | ✅ **COMPLETED** |
| 6 | `DailyLoanReport.aspx` | `DailyLoanReport.aspx.vb` | Daily GL Report | [pages/daily-loan-report.html](pages/daily-loan-report.html) | — | ✅ **COMPLETED** |
| 7 | `IncomeExpenseRpt.aspx` | `IncomeExpenseRpt.aspx.vb` | MIS Reports | [pages/income-expense-report.html](pages/income-expense-report.html) | — | ✅ **COMPLETED** |
| 8 | `IntOutstandRpt.aspx` | `IntOutstandRpt.aspx.vb` | Interest Outstand Report | `pages/intoutstandrpt.html` | — | ⏳ Pending Batch |
| 9 | `IntRcvdPayableRpt.aspx` | `IntRcvdPayableRpt.aspx.vb` | Interest Report | `pages/intrcvdpayablerpt.html` | — | ⏳ Pending Batch |
| 10 | `LoanAnalysisRpt.aspx` | `LoanAnalysisRpt.aspx.vb` | Loan Analysis Report | `pages/loananalysisrpt.html` | — | ⏳ Pending Batch |
| 11 | `LoanApproval.aspx` | `LoanApproval.aspx.vb` | Loan Sanction | [pages/loan-approval.html](pages/loan-approval.html) | — | ✅ **COMPLETED** |
| 12 | `LoanLegalNotice.aspx` | `LoanLegalNotice.aspx.vb` | Legal Notice | [pages/loan-legal-notice.html](pages/loan-legal-notice.html) | — | ✅ **COMPLETED** |
| 13 | `LoanRIForm.aspx` | `LoanRIForm.aspx.vb` | Record Inspection | `pages/loanriform.html` | — | ⏳ Pending Batch |
| 14 | `OD_Int_Checking.aspx` | `OD_Int_Checking.aspx.vb` | Busines Loan OD | `pages/od_int_checking.html` | — | ⏳ Pending Batch |


### 📂 Loan & Credit Reports (17 Pages - 14 Completed)

| # | Original ASPX Page | Code-Behind (.vb) | Menu Label / Function | Static Page Target | Query Variants | Status |
|---|---|---|---|---|---|---|
| 1 | `DailyCollectionReport.aspx` | `DailyCollectionReport.aspx.vb` | Daily Collection Report | [pages/daily-collection-report.html](pages/daily-collection-report.html) | — | ✅ **COMPLETED** |
| 2 | `EMI_BktReport.aspx` | `EMI_BktReport.aspx.vb` | EMI Bucket Wise Rpt | [pages/emi-bucket-report.html](pages/emi-bucket-report.html) | — | ✅ **COMPLETED** |
| 3 | `EMI_DueList_Rpt.aspx` | `EMI_DueList_Rpt.aspx.vb` | Daily DueList | [pages/emi-due-list.html](pages/emi-due-list.html) | `EMI_DueList_Rpt.aspx?RptType=Daily` | ✅ **COMPLETED** |
| 4 | `FluidResRpt.aspx` | `FluidResRpt.aspx.vb` | Fluid Report | [pages/fluid-resources-report.html](pages/fluid-resources-report.html) | — | ✅ **COMPLETED** |
| 5 | `GuarantorDetails_Rpt.aspx` | `GuarantorDetails_Rpt.aspx.vb` | Guarantor Details | [pages/guarantor-details-report.html](pages/guarantor-details-report.html) | — | ✅ **COMPLETED** |
| 6 | `InterestDemandRpt.aspx` | `InterestDemandRpt.aspx.vb` | FollowUp List | [pages/interest-demand-report.html](pages/interest-demand-report.html) | — | ✅ **COMPLETED** |
| 7 | `InterestDueList.aspx` | `InterestDueList.aspx.vb` | Demand List | [pages/interest-due-list.html](pages/interest-due-list.html) | — | ✅ **COMPLETED** |
| 8 | `LoanDCBReport.aspx` | `LoanDCBReport.aspx.vb` | DCB | [pages/loan-dcb-report.html](pages/loan-dcb-report.html) | — | ✅ **COMPLETED** |
| 9 | `LoanOpenClose_Report.aspx` | `LoanOpenClose_Report.aspx.vb` | Opening Closing Report | [pages/loan-open-close-report.html](pages/loan-open-close-report.html) | — | ✅ **COMPLETED** |
| 10 | `Loan_BDR_Report.aspx` | `Loan_BDR_Report.aspx.vb` | BDR Statement | [pages/loan-bdr-report.html](pages/loan-bdr-report.html) | — | ✅ **COMPLETED** |
| 11 | `Loan_NPA_Rpt.aspx` | `Loan_NPA_Rpt.aspx.vb` | NPA Report | [pages/loan-npa-report.html](pages/loan-npa-report.html) | — | ✅ **COMPLETED** |
| 12 | `NPARptRegionwise.aspx` | `NPARptRegionwise.aspx.vb` | NPA AREA WISE Report | [pages/npa-region-report.html](pages/npa-region-report.html) | — | ✅ **COMPLETED** |
| 13 | `NPA_Prdwise_Report.aspx` | `NPA_Prdwise_Report.aspx.vb` | NPA Report | [pages/npa-period-report.html](pages/npa-period-report.html) | — | ✅ **COMPLETED** |
| 14 | `Outstand.aspx` | `Outstand.aspx.vb` | Outstanding Type2 | `pages/outstand.html` | — | ⏳ Pending Batch |
| 15 | `OutstandConsolidate.aspx` | `OutstandConsolidate.aspx.vb` | Outstanding Consolidate | `pages/outstandconsolidate.html` | — | ⏳ Pending Batch |
| 16 | `SchemeOutstandRpt.aspx` | `SchemeOutstandRpt.aspx.vb` | Scheme Wise Outstand | [pages/scheme-outstand-report.html](pages/scheme-outstand-report.html) | — | ✅ **COMPLETED** |
| 17 | `SuspenseCash.aspx` | `SuspenseCash.aspx.vb` | Suspense Balance Report | `pages/suspensecash.html` | — | ⏳ Pending Batch |


### 📂 Loans & Credit Advances (12 Pages - 7 Completed)

| # | Original ASPX Page | Code-Behind (.vb) | Menu Label / Function | Static Page Target | Query Variants | Status |
|---|---|---|---|---|---|---|
| 1 | `DepositLoan.aspx` | `DepositLoan.aspx.vb` | DepositLoan | [pages/deposit-loan.html](pages/deposit-loan.html) | `DepositLoan.aspx?TRANID=` | ✅ **COMPLETED** |
| 2 | `LoanAction.aspx` | `LoanAction.aspx.vb` | LoanAction | `pages/loanaction.html` | `LoanAction.aspx?AccID=` | ⏳ Pending Batch |
| 3 | `LoanApplication.aspx` | `LoanApplication.aspx.vb` | LOAN APPLICATION STAGE 1 | [pages/loan-application.html](pages/loan-application.html) | `LoanApplication.aspx?TRANID=` | ✅ **COMPLETED** |
| 4 | `LoanApplicationForm.aspx` | `LoanApplicationForm.aspx.vb` | LOAN APPLICATION | [pages/loan-application-form.html](pages/loan-application-form.html) | `LoanApplicationForm.aspx?TRANID=` | ✅ **COMPLETED** |
| 5 | `LoanCreditApproval.aspx` | `LoanCreditApproval.aspx.vb` | LOAN APPLICATION STAGE 3 | [pages/loan-credit-approval.html](pages/loan-credit-approval.html) | — | ✅ **COMPLETED** |
| 6 | `LoanDisbursal.aspx` | `LoanDisbursal.aspx.vb` | LOAN APPLICATION STAGE 2 | [pages/loan-disbursal.html](pages/loan-disbursal.html) | — | ✅ **COMPLETED** |
| 7 | `LoanEMIChart.aspx` | `LoanEMIChart.aspx.vb` | LoanEMIChart | [pages/loan-emi-chart.html](pages/loan-emi-chart.html) | `LoanEMIChart.aspx?AccID=999999999` | ✅ **COMPLETED** |
| 8 | `LoanMaster.aspx` | `LoanMaster.aspx.vb` | LoanMaster | `pages/loanmaster.html` | `LoanMaster.aspx?TRANID=` | ⏳ Pending Batch |
| 9 | `LoanPIForm.aspx` | `LoanPIForm.aspx.vb` | LOAN APPLICATION STAGE 2 | [pages/loan-pi-form.html](pages/loan-pi-form.html) | — | ✅ **COMPLETED** |
| 10 | `LoanTranDtls.aspx` | `LoanTranDtls.aspx.vb` | LoanTranDtls | `pages/loantrandtls.html` | `LoanTranDtls.aspx?AccID=` | ⏳ Pending Batch |
| 11 | `Loan_appDetail.aspx` | `Loan_appDetail.aspx.vb` | LOAN PROCESS REPORT | `pages/loan_appdetail.html` | — | ⏳ Pending Batch |
| 12 | `NewAccount.aspx` | `NewAccount.aspx.vb` | NewAccount | `pages/newaccount.html` | `NewAccount.aspx?AccNo=`<br>`NewAccount.aspx?AccNo= Loan Application Form No-` | ⏳ Pending Batch |


### 📂 Miscellaneous Reports & Inquiries (9 Pages - 2 Completed)

| # | Original ASPX Page | Code-Behind (.vb) | Menu Label / Function | Static Page Target | Query Variants | Status |
|---|---|---|---|---|---|---|
| 1 | `BulkUploadLoanAction.aspx` | `BulkUploadLoanAction.aspx.vb` | Loan Action Bulkupload | `pages/bulkuploadloanaction.html` | — | ⏳ Pending Batch |
| 2 | `CibilReport.aspx` | `CibilReport.aspx.vb` | Cibil Report | [pages/cibil-report.html](pages/cibil-report.html) | — | ✅ **COMPLETED** |
| 3 | `Customermodified_rpt.aspx` | `Customermodified_rpt.aspx.vb` | Customer Updation Log | `pages/customermodified_rpt.html` | — | ⏳ Pending Batch |
| 4 | `GuarantorCustomerDetails_Rpt.aspx` | `GuarantorCustomerDetails_Rpt.aspx.vb` | Customer guarantors Report | `pages/guarantorcustomerdetails_rpt.html` | — | ⏳ Pending Batch |
| 5 | `IncentiveCanvasserDetails_Rpt.aspx` | `IncentiveCanvasserDetails_Rpt.aspx.vb` | Incentive Canvasser Report | `pages/incentivecanvasserdetails_rpt.html` | — | ⏳ Pending Batch |
| 6 | `LoginPage.aspx` | `LoginPage.aspx.vb` | LoginPage | [login.html](login.html) | — | ✅ **COMPLETED** |
| 7 | `TicketMaster_Report.aspx` | `TicketMaster_Report.aspx.vb` | Ticket Register Report | `pages/ticketmaster_report.html` | — | ⏳ Pending Batch |
| 8 | `TranCancelRpt.aspx` | `TranCancelRpt.aspx.vb` | Cancellation Report | `pages/trancancelrpt.html` | — | ⏳ Pending Batch |
| 9 | `TransactionLogReport.aspx` | `TransactionLogReport.aspx.vb` | User Log Report | `pages/transactionlogreport.html` | — | ⏳ Pending Batch |


### 📂 Payroll & Human Resources (6 Pages - 4 Completed)

| # | Original ASPX Page | Code-Behind (.vb) | Menu Label / Function | Static Page Target | Query Variants | Status |
|---|---|---|---|---|---|---|
| 1 | `BulkUploadPayrollProcess.aspx` | `BulkUploadPayrollProcess.aspx.vb` | Salary Posting | [pages/salary-posting.html](pages/salary-posting.html) | — | ✅ **COMPLETED** |
| 2 | `EmpLeave.aspx` | `EmpLeave.aspx.vb` | Leave Master | [pages/emp-leave.html](pages/emp-leave.html) | — | ✅ **COMPLETED** |
| 3 | `EmpLeavesettings.aspx` | `EmpLeavesettings.aspx.vb` | Leave Settings | [pages/emp-leave-settings.html](pages/emp-leave-settings.html) | — | ✅ **COMPLETED** |
| 4 | `Emp_PayScale.aspx` | `Emp_PayScale.aspx.vb` | Pay Scale | [pages/emp-payscale.html](pages/emp-payscale.html) | — | ✅ **COMPLETED** |
| 5 | `LeaveBalance.aspx` | `LeaveBalance.aspx.vb` | Leave Balance | `pages/leavebalance.html` | — | ⏳ Pending Batch |
| 6 | `PaySettings.aspx` | `PaySettings.aspx.vb` | Pay Settings | `pages/paysettings.html` | — | ⏳ Pending Batch |


### 📂 Payroll Reports (4 Pages - 2 Completed)

| # | Original ASPX Page | Code-Behind (.vb) | Menu Label / Function | Static Page Target | Query Variants | Status |
|---|---|---|---|---|---|---|
| 1 | `AcquiantanceRpt.aspx` | `AcquiantanceRpt.aspx.vb` | Acquiantance Report | [pages/acquaintance-report.html](pages/acquaintance-report.html) | — | ✅ **COMPLETED** |
| 2 | `Grativity.aspx` | `Grativity.aspx.vb` | Gratuvity | `pages/grativity.html` | — | ⏳ Pending Batch |
| 3 | `SalaryRpt.aspx` | `SalaryRpt.aspx.vb` | Salary Type | `pages/salaryrpt.html` | — | ⏳ Pending Batch |
| 4 | `Salarystmt.aspx` | `Salarystmt.aspx.vb` | PaySlip | [pages/salary-statement.html](pages/salary-statement.html) | — | ✅ **COMPLETED** |


### 📂 Supervisor Authorisation & Audit (1 Pages - 0 Completed)

| # | Original ASPX Page | Code-Behind (.vb) | Menu Label / Function | Static Page Target | Query Variants | Status |
|---|---|---|---|---|---|---|
| 1 | `NEFT_PaymentApproval.aspx` | `NEFT_PaymentApproval.aspx.vb` | DEPOSIT BULK PAYMENT | `pages/neft_paymentapproval.html` | — | ⏳ Pending Batch |


### 📂 System Administration & Security (44 Pages - 14 Completed)

| # | Original ASPX Page | Code-Behind (.vb) | Menu Label / Function | Static Page Target | Query Variants | Status |
|---|---|---|---|---|---|---|
| 1 | `AccHeadTranEdit.aspx` | `AccHeadTranEdit.aspx.vb` | AccHeadTranEdit | `pages/accheadtranedit.html` | `AccHeadTranEdit.aspx?TRANID=` | ⏳ Pending Batch |
| 2 | `AccountGroupMaster.aspx` | `AccountGroupMaster.aspx.vb` | Account Group Creation | [pages/account-group-master.html](pages/account-group-master.html) | — | ✅ **COMPLETED** |
| 3 | `AccountMaster.aspx` | `AccountMaster.aspx.vb` | Account Head Creation | [pages/account-master.html](pages/account-master.html) | — | ✅ **COMPLETED** |
| 4 | `AreaMaster.aspx` | `AreaMaster.aspx.vb` | Area Master | [pages/area-master.html](pages/area-master.html) | — | ✅ **COMPLETED** |
| 5 | `BILLSREALISATION.aspx` | `BILLSREALISATION.aspx.vb` | BILLSREALISATION | `pages/billsrealisation.html` | `BILLSREALISATION.aspx?TRANID=` | ⏳ Pending Batch |
| 6 | `Bank_Account.aspx` | `Bank_Account.aspx.vb` | Bank Accounts | `pages/bank_account.html` | — | ⏳ Pending Batch |
| 7 | `Bills.aspx` | `Bills.aspx.vb` | Bills | `pages/bills.html` | `Bills.aspx?TRANID=` | ⏳ Pending Batch |
| 8 | `BranchAreaMapping.aspx` | `BranchAreaMapping.aspx.vb` | Branch Area Mapping | `pages/branchareamapping.html` | — | ⏳ Pending Batch |
| 9 | `BranchDetails.aspx` | `BranchDetails.aspx.vb` | Branch Details | [pages/branch-details.html](pages/branch-details.html) | — | ✅ **COMPLETED** |
| 10 | `Budget.aspx` | `Budget.aspx.vb` | Budget Settings | `pages/budget.html` | — | ⏳ Pending Batch |
| 11 | `BudgetRpt.aspx` | `BudgetRpt.aspx.vb` | Budget Settings | `pages/budgetrpt.html` | — | ⏳ Pending Batch |
| 12 | `BulkTransactionEntry.aspx` | `BulkTransactionEntry.aspx.vb` | BulkTransactionEntry | `pages/bulktransactionentry.html` | `BulkTransactionEntry.aspx?TRANID=` | ⏳ Pending Batch |
| 13 | `CanvasedBy.aspx` | `CanvasedBy.aspx.vb` | Canvased By | `pages/canvasedby.html` | — | ⏳ Pending Batch |
| 14 | `ChangeBranch.aspx` | `ChangeBranch.aspx.vb` | Change Branch / DeActive | `pages/changebranch.html` | — | ⏳ Pending Batch |
| 15 | `ChangeUserAuthorisation.aspx` | `ChangeUserAuthorisation.aspx.vb` | Transfer User Rights | `pages/changeuserauthorisation.html` | — | ⏳ Pending Batch |
| 16 | `ChittyAuction.aspx` | `ChittyAuction.aspx.vb` | ChittyAuction | `pages/chittyauction.html` | `ChittyAuction.aspx?TRANID=` | ⏳ Pending Batch |
| 17 | `DailyBusinessRpt.aspx` | `DailyBusinessRpt.aspx.vb` | Daily Business Report | [pages/daily-business-report.html](pages/daily-business-report.html) | — | ✅ **COMPLETED** |
| 18 | `DebRpt_Series.aspx` | `DebRpt_Series.aspx.vb` | Debenture Sub Series | `pages/debrpt_series.html` | — | ⏳ Pending Batch |
| 19 | `DebentureSeriesAdd.aspx` | `DebentureSeriesAdd.aspx.vb` | Debenture Series | `pages/debentureseriesadd.html` | — | ⏳ Pending Batch |
| 20 | `Deposit_Parameter.aspx` | `Deposit_Parameter.aspx.vb` | Deposit Parameter | [pages/deposit-parameter.html](pages/deposit-parameter.html) | — | ✅ **COMPLETED** |
| 21 | `DueDateExtender.aspx` | `DueDateExtender.aspx.vb` | Due Date Extender | `pages/duedateextender.html` | — | ⏳ Pending Batch |
| 22 | `EmpMaster.aspx` | `EmpMaster.aspx.vb` | Employee master | [pages/emp-master.html](pages/emp-master.html) | — | ✅ **COMPLETED** |
| 23 | `Goldopen.aspx` | `Goldopen.aspx.vb` | Goldopen | `pages/goldopen.html` | `GoldOpen.aspx?TRANID=` | ⏳ Pending Batch |
| 24 | `Gramrate.aspx` | `Gramrate.aspx.vb` | Gram Rate Settings | `pages/gramrate.html` | — | ⏳ Pending Batch |
| 25 | `GroupRights.aspx` | `GroupRights.aspx.vb` | Group Rights | [pages/group-rights.html](pages/group-rights.html) | — | ✅ **COMPLETED** |
| 26 | `InterestRateMaster.aspx` | `InterestRateMaster.aspx.vb` | Interest Rate Settings | [pages/interest-rate-master.html](pages/interest-rate-master.html) | — | ✅ **COMPLETED** |
| 27 | `InvestmentMaster.aspx` | `InvestmentMaster.aspx.vb` | InvestmentMaster | `pages/investmentmaster.html` | `InvestmentMaster.aspx?TRANID=` | ⏳ Pending Batch |
| 28 | `LoanParameter.aspx` | `LoanParameter.aspx.vb` | Loan Parameter | [pages/loan-parameter.html](pages/loan-parameter.html) | — | ✅ **COMPLETED** |
| 29 | `LoanRate.aspx` | `LoanRate.aspx.vb` | Loan Rate Settings | `pages/loanrate.html` | — | ⏳ Pending Batch |
| 30 | `LockerMaster.aspx` | `LockerMaster.aspx.vb` | LockerMaster | `pages/lockermaster.html` | `LockerMaster.aspx?TRANID=` | ⏳ Pending Batch |
| 31 | `ManagerPassingEdit.aspx` | `ManagerPassingEdit.aspx.vb` | Transaction Edit | `pages/managerpassingedit.html` | — | ⏳ Pending Batch |
| 32 | `MembershipParameter.aspx` | `MembershipParameter.aspx.vb` | Share Parameter | `pages/membershipparameter.html` | — | ⏳ Pending Batch |
| 33 | `PickUpMaster.aspx` | `PickUpMaster.aspx.vb` | Pickup Value Settings | `pages/pickupmaster.html` | — | ⏳ Pending Batch |
| 34 | `PrnView.aspx` | `PrnView.aspx.vb` | PrnView | `pages/prnview.html` | `PrnView.aspx?PageType=`<br>`PrnView.aspx?PageType=130` | ⏳ Pending Batch |
| 35 | `ProcessingFeeWaiver.aspx` | `ProcessingFeeWaiver.aspx.vb` | Processing Fee Waiver | `pages/processingfeewaiver.html` | — | ⏳ Pending Batch |
| 36 | `RegionMaster.aspx` | `RegionMaster.aspx.vb` | Region Master | [pages/region-master.html](pages/region-master.html) | — | ✅ **COMPLETED** |
| 37 | `Remainder.aspx` | `Remainder.aspx.vb` | Reminder Settings | [pages/remainder.html](pages/remainder.html) | — | ✅ **COMPLETED** |
| 38 | `RemainderRevoke.aspx` | `RemainderRevoke.aspx.vb` | Reminder Revoke | `pages/remainderrevoke.html` | — | ⏳ Pending Batch |
| 39 | `SchemeMaster.aspx` | `SchemeMaster.aspx.vb` | Scheme Creation | [pages/scheme-master.html](pages/scheme-master.html) | — | ✅ **COMPLETED** |
| 40 | `ShareMaster.aspx` | `ShareMaster.aspx.vb` | ShareMaster | `pages/sharemaster.html` | `ShareMaster.aspx?TRANID=` | ⏳ Pending Batch |
| 41 | `SuspenseMaster.aspx` | `SuspenseMaster.aspx.vb` | SuspenseMaster | `pages/suspensemaster.html` | `SuspenseMaster.aspx?TRANID=` | ⏳ Pending Batch |
| 42 | `TransfermodeSetting.aspx` | `TransfermodeSetting.aspx.vb` | TransfermodeSetting | `pages/transfermodesetting.html` | — | ⏳ Pending Batch |
| 43 | `UserChangeGroup.aspx` | `UserChangeGroup.aspx.vb` | User Change Group | `pages/userchangegroup.html` | — | ⏳ Pending Batch |
| 44 | `Userrights.aspx` | `Userrights.aspx.vb` | User Rights | [pages/user-rights.html](pages/user-rights.html) | — | ✅ **COMPLETED** |

