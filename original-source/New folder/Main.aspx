
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="Main.aspx.vb" Inherits="SMARTCORE_UI.Main" 
    title="Account Open" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
    <link type="text/css" href="menu.css" rel="stylesheet" />
  

    <script type="text/javascript">
        var safe = function (id) {
            return document.getElementById(id);
        }
        function show(id)
        { safe(id).style.display = "Block"; }
        function hide(id)
        { safe(id).style.display = "none"; }
        function setclass(show, hide) {
            safe(show).className = 'Mainheadshow';
            safe(hide).className = 'Mainheadcss';
        }
    </script>

    <style type="text/css" >
        .Mainheadcss {  cursor:pointer;
             float:left;   height:30px; padding:10px 0px 0px 5px; width:150px; border-radius:5px;
             background:#2063b7;
   background: -webkit-linear-gradient(top, #2063b7, #6b9fe1);
    background: -moz-linear-gradient(top, #2063b7, #6b9fe1);
  background: -ms-linear-gradient(top, #2063b7, #6b9fe1);
  background: -o-linear-gradient(top, #2063b7, #6b9fe1);
  color:#FFF;
        }

        .Mainheadshow { cursor:pointer;border-radius:5px;
             float:left;   height:30px; padding:10px 0px 0px 5px; width:150px; color:#2063b7; border:0px 1px 1px 1px  solid #2063b7;
              background:#e6ecf7;
   /*background: -webkit-linear-gradient(top, #083d7e, #6b9fe1);
    background: -moz-linear-gradient(top, #083d7e, #6b9fe1);
  background: -ms-linear-gradient(top, #083d7e, #6b9fe1);
  background: -o-linear-gradient(top, #083d7e, #6b9fe1);*/
        }
            .Mainheadshow:after {
                
                content: "";
    border: 12px solid;
    border-color:#2063b7 transparent transparent transparent ;
    /*position: absolute;*/
 float:left; margin-left:40%; margin-top:10px;  
            }
            
            .charthead
       {width:100%; float:left; height:25px; padding-top:5px; color:#FFF;font-family:Arial,Calibri; font-size:80%;font-weight:800; background:#f28838;
        text-align:center; text-transform:uppercase;
       }
       .chartsubhead
       {width:100%;float:left; height:20px;padding-top:5px;color:#FFF;font-family:Arial,Calibri; font-size:80%; font-weight:800; background:#fbaf37; text-align:center; text-transform:uppercase;
       }
       .charttablehead
       { float:left; height:15px; padding-top:2px; font-family:Arial,Calibri; font-size:70%; font-weight:800;  
       }
       .charttable
       {width:100%; float:left; margin-top:5px; box-shadow:2px #e0dbdb; 
       }
            
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager> 

<div style="width:100%; float:left;">
<table><tr><td style="width:15%;background:#79a5d3; vertical-align:top;">
<div style="width:100%;float:left; " >
<asp:Panel runat="server" ID="SysAdminMenu" Visible="false" >
<div class="Menuparent" >    
        <div class="dropleft">
        <ul>
        <li><div class="menuleft"></div><a href="UserReg.aspx"><span>User Creation</span></a></li>
        <li><div class="menuleft"></div><a href="UserEmployee.aspx"><span>User Employee Registration</span></a></li>
       <ul>
<li><div class="menuleft"></div><a href="#"><span>Group Creation</span></a></li>
<li><div class="menuleft"></div><a href="GroupRights.aspx"><span>Group Rights</span></a></li>
<li><div class="menuleft"></div><a href="Userrights.aspx"><span>User Rights</span></a></li>
<li><div class="menuleft"></div><a href="SchemeMaster.aspx"><span>Scheme Creation</span></a></li>
<li><div class="menuleft"></div><a href="AccountMaster.aspx"><span>Account Head Creation</span></a></li>
<li><div class="menuleft"></div><a href="AccountGroupMaster.aspx"><span>Account Group Creation</span></a></li>
<li><div class="menuleft"></div><a href="InterestRateMaster.aspx"><span>Interest Rate Settings</span></a></li>
<li><div class="menuleft"></div><a href="PickUpMaster.aspx"><span>Pickup Value Settings</span></a></li>
<li><div class="menuleft"></div><a href="EmpMaster.aspx"><span>Employee master</span></a></li>
<li><div class="menuleft"></div><a href="ChangeBranch.aspx"><span>Change Branch / DeActive </span></a></li>
<li><div class="menuleft"></div><a href="ChangeUserAuthorisation.aspx"><span>Transfer User Rights</span></a></li>
<li><div class="menuleft"></div><a href="RegionMaster.aspx"><span>Region Master</span></a></li>
<li><div class="menuleft"></div><a href="AreaMaster.aspx"><span>Area Master</span></a></li>
<li><div class="menuleft"></div><a href="BranchAreaMapping.aspx"><span>Branch Area Mapping</span></a></li>
<li><div class="menuleft"></div><a href="BranchDetails.aspx"><span>Branch Details</span></a></li>
</ul>
<li><div class="menuleft"></div><a href="Deposit_Parameter.aspx"><span>Deposit Parameter</span></a></li>
<li><div class="menuleft"></div><a href="LoanParameter.aspx"><span>Loan Parameter</span></a></li>
<li><div class="menuleft"></div><a href="MembershipParameter.aspx" ><span>Share Parameter</span></a></li>
<li><div class="menuleft"></div><a href="DailyBusinessRpt.aspx" ><span>Daily Business Report</span></a></li>
<li><div class="menuleft"></div><a href="PortFolioRpt.aspx" ><span>Port Folio Rpt</span></a></li>
<li><div class="menuleft"></div><a href="ProcessingFeeWaiver.aspx" ><span>Processing Fee Waiver</span></a></li>
<li><div class="menuleft"></div><a href="ManagerPassingEdit.aspx" ><span>Transaction Edit</span></a></li>
<li><div class="menuleft"></div><a href="DueDateExtender.aspx" ><span>Due Date Extender</span></a></li>
<li><div class="menuleft"></div><a href="Budget.aspx" ><span>Budget Settings</span></a></li>
<li><div class="menuleft"></div><a href="CanvasedBy.aspx" ><span>Canvased By</span></a></li>
<li><div class="menuleft"></div><a href="Bank_Account.aspx" ><span>Bank Accounts</span></a></li>
<li><div class="menuleft"></div><a href="BudgetRpt.aspx" ><span>Budget Settings</span></a></li>
<li><div class="menuleft"></div><a href="Remainder.aspx" ><span>Reminder Settings</span></a></li>
<li><div class="menuleft"></div><a href="RemainderRevoke.aspx" ><span>Reminder Revoke</span></a></li>
<li><div class="menuleft"></div><a href="DebentureSeriesAdd.aspx" ><span>Debenture Series</span></a></li>
<li><div class="menuleft"></div><a href="DebRpt_Series.aspx" ><span>Debenture Sub Series</span></a></li>
<li><div class="menuleft"></div><a href="UserChangeGroup.aspx" ><span>User Change Group</span></a></li>
        </ul>
        </div>
        </div>
</asp:Panel>
<asp:Panel ID= "PnlMenu" runat="server"  >
    <div class="Menuparent">    
        <div class="dropleft">
<ul>
<li><div class="menuleft"></div><a href='#'><span>CUSTOMER</span></a>
    <ul>
       <li><a href="CustomerRegister.aspx"><span>CUSTOMER REGISTER</span></a></li>
       <li><a href="cust_submission.aspx" ><span>CUSTOMER 15-H SUBMISSION</span></a></li>
       <li><a href="VerifyBnkDtls.aspx" ><span>CUSTOMER BANK DETAILS</span></a></li>

</ul>
</li>

<%--<li><div class="menuleft"></div><a href='#'><span>LOAN PROCESS</span></a>
    <ul>
       <li><a href="LoanApplicationForm.aspx"><span>LOAN APPLICATION</span></a></li>
       <li><a href="LoanApplication.aspx"><span>LOAN APPLICATION STAGE 1</span></a></li>
       <li><a href="LoanPIForm.aspx"><span>LOAN APPLICATION STAGE 2</span></a></li>
       <li><a href="LoanCreditApproval.aspx"><span>LOAN APPLICATION STAGE 3</span></a></li>
       <li><a href="LoanDisbursal.aspx"><span>LOAN APPLICATION STAGE 2</span></a></li>
       

</ul>
</li>--%>

<li><div class="menuleft"></div><a href='#'><span>TRANSACTION</span></a>
<ul>
    <li><a href="AccOpen.aspx"><span>ACCOUNT OPEN</span></a></li>
<li><a href="Transaction.aspx"><span>ACCOUNT TRANSACTION</span></a></li>
<li><a href="CashTr.aspx"><span>CASH/FUND TRANSFER</span></a></li>
<li><a href="LoanActionTaken.aspx"><span>LOAN ACTION TAKEN ENTRY</span></a></li>
<li><a href="NEFT_IntPayment.aspx"><span>DEPOSIT BULK INT.POST</span></a></li>
<%--<li><a href="Entry_change.aspx"><span>ENTRY MODE CHANGE</span></a></li>--%>
<%--<li><a href="Bank_Reconciliation.aspx"><span>BANK RECONCILIATION</span></a></li>--%>
<li><a href="TicketMaster.aspx"><span>TICKET MASTER</span></a></li>

                     <li><a href="#" class="parent"><span>Fixed Asset</span></a>
                            <div><ul>
                            <li><a href="AssetTransaction.aspx?RptType=PURCHASE"  ><span>PURCHASE</span></a></li>
                            <li><a href="AssetTransaction.aspx?RptType=SALES"  ><span>SALES</span></a></li>
                            <li><a href="AssetTransaction.aspx?RptType=TRANSFER"  ><span>TRANSFER</span></a></li>
                            <li><a href="AssetTransaction.aspx?RptType=AUCTION"  ><span>AUCTION</span></a></li>
                            <li><a href="AssetTransaction.aspx?RptType=DAMAGE"  ><span>DAMAGE</span></a></li>
                            <li><a href="AssetGroup.aspx" ><span>Asset Group</span></a></li>
                            <li><a href="AssetMaster.aspx" ><span>Asset Master</span></a></li>
                             </ul></div>
                        </li>
</ul>
</li>

<%--<li><div class="menuleft"></div><a href="ManagerPassing.aspx"><span>AUTHORISATION</span></a></li>--%>
<li><div class="menuleft"></div><a href='#'><span>AUTHORISATION</span></a>
        <ul>
            <li><a href="ManagerPassing.aspx"><span>AUTHORISATION</span></a></li>
            <li><a href="NEFT_PaymentApproval.aspx"><span>DEPOSIT BULK PAYMENT</span></a></li>
        </ul>
    </li>
    <li><div class="menuleft"></div><a href='#'><span>Utilities</span></a>
        <ul>
            <li><a href="TranCancellation.aspx"><span>Transaction Cancellation</span></a></li>
<li><a href="BondPrint.aspx"><span>Duplicate Printing</span></a></li>
<li><a href="ControlBalanceReport.aspx"><span>Control Balance</span></a></li>
<li><a href="ChangePwd.aspx"><span>Change Password</span></a></li>
<li><a href="LoginUserStatus.aspx"><span>Login User status</span></a></li>
<li><a href="ChittySuertySecurity.aspx"><span>Guarantor Details</span></a></li>

        </ul>
    </li>
    <li><div class="menuleft"></div><a href='#'><span>Dayend</span></a>
        <ul>
            <li><a href="DayBeginEnd.aspx"><span>Daybegin</span></a></li>
            <li><a href="DayBeginEnd.aspx"><span>DayEnd</span></a></li>
            <li><a href="LoginUserStatus.aspx" ><span>Login User Status</span></a></li>
            <li><a href="EmpAttendence.aspx" ><span>Attendence Marking</span></a></li>
            <li><a href="DayEndStatus.aspx" ><span>Branch DayEnd Status</span></a></li>
<li><a href="NoticePrinting.aspx"><span>Notice Printing</span></a></li>
        </ul>
    </li>
<li><div class="menuleft"></div><a href='#'><span>LOAN PROCESS</span></a>
    <ul>
       <li><a href="LoanApplicationForm.aspx"><span>LOAN APPLICATION</span></a></li>
       <li><a href="LoanApplication.aspx"><span>LOAN APPLICATION STAGE 1</span></a></li>
       <li><a href="LoanPIForm.aspx"><span>LOAN APPLICATION STAGE 2</span></a></li>
       <li><a href="LoanCreditApproval.aspx"><span>LOAN APPLICATION STAGE 3</span></a></li>
       <li><a href="LoanDisbursal.aspx"><span>LOAN APPLICATION STAGE 4</span></a></li>
       <li><a href="Loan_appDetail.aspx"><span>LOAN PROCESS REPORT</span></a></li>
       

</ul>
</li>
    
    <li><div class="menuleft"></div><a href='#'><span>Customer  REPORTS</span></a>
        <ul>
     <%--   <li><a href="CustAssetLiabRpt.aspx"></a><span>Assets and Liabilities</span></li>--%>
        <li><a href="CustAssetLiabRpt.aspx"><span>Assets and Liabilities</span></a></li>
            <li><a href="PersonalLedger.aspx"><span>PERSONAL LEDGER</span></a></li>
<li><a href="cust_reg_Report.aspx"><span>Customer Register Report</span></a></li>
<li><a href="CustDetRpt.aspx"><span>Customer Details Report</span></a></li>
<li><a href="NotSubmitPanCard.aspx"><span>Pan Card Report</span></a></li>
<li><a href="15H_Rpt.aspx"><span>15-G Report</span></a></li>

        </ul>
    </li>
    
    <li><div class="menuleft"></div><a href='#'><span>Gold REPORTS</span></a>
        <ul>
        <li><a href="PersonalLedger.aspx"><span>Personal Ledger</span></a></li>
            <li><a href="GoldLoanOpenClose_Report.aspx.aspx"><span>Gold Opening Closing Report</span></a></li>
<li><a href="GoldRedemptionReport.aspx"><span>Stock Report</span></a></li>
<li><a href="GoldOrnamentsAppraiserDtlsEntry.aspx"><span>Gold Ornaments Details</span></a></li>
<li><a href="GoldOrnamentsAppraiserDtlsReport.aspx"><span>Gold Ornaments Appraiser Report</span></a></li>
<li><a href="RptOutstanding.aspx"><span>Outstanding Report</span></a></li>

<li><a href="LoanReports.aspx"><span>Overdue Report</span></a></li>
<li><a href="InterestPayable.aspx"><span>Interest Receiavable Report</span></a></li>
<li><a href="LoanEMIRpt.aspx"><span>Loan EMI Report</span></a></li>
<li><a href="NoticePrinting.aspx"><span>Notice Printing</span></a></li>
        </ul>
    </li>
    <li><div class="menuleft"></div><a href='#'><span>Loan  REPORTS</span></a>
        <ul>
<li><a href="PersonalLedger.aspx"><span>Personal Ledger</span></a>

<ul>
 <%--<li><a href="EMI_DueList_Rpt.aspx?RptType=Daily"><span>Daily DueList</span></a></li>--%>
 <li><a href="EMI_DueList_Rpt.aspx"><span>EMI DueList</span></a></li>
 <li><a href="DailyCollectionReport.aspx"><span>Daily Collection Report</span></a></li>
 <li><a href="EMI_BktReport.aspx"><span>EMI Bucket Wise Rpt</span></a></li>
<li><a href="InterestDueList.aspx"><span>Demand List</span></a></li>
<li><a href='Loan_BDR_Report.aspx'><span>BDR Statement</span></a></li>
<li><a href="LoanDCBReport.aspx"><span>DCB</span></a></li>
<li><a href= "InterestPayable.aspx?RptType=Receivable" ><span>Interest Receivable</span></a></li>
<li><a href="FluidResRpt.aspx"><span>Fluid Report</span></a></li>
<li><a href="NoticePrinting.aspx"><span>Notice Printing</span></a></li>
<li><a href="InterestDemandRpt.aspx"><span>FollowUp List</span></a></li>
<li><a href="GuarantorDetails_Rpt.aspx"><span>Guarantor Details</span></a></li> 
</ul>
</li>
<li><a href="LoanOpenClose_Report.aspx"><span>Opening Closing Report</span></a></li>
<li><a href="LoanEMIRpt.aspx"><span>Loan EMI Report</span></a></li>              
<li><a href="RptOutstanding.aspx"><span>Outstanding Report</span></a></li>
<li><a href="LoanReports.aspx"><span>Overdue Report</span></a></li>
<%--<li><a href="Loan_NPA_Rpt.aspx"><span>NPA Report</span></a></li>--%>
<li><a href="NPA_Prdwise_Report.aspx"><span>NPA Report</span></a></li>
<li><a href="NPARptRegionwise.aspx"><span>NPA AREA WISE Report</span></a></li>
<li><a href="SuspenseCash.aspx"><span>Suspense Balance Report</span></a></li>
<li><a href="Outstand.aspx"><span>Outstanding Type2</span></a></li>
<li><a href="OutstandConsolidate.aspx"><span>Outstanding Consolidate</span></a></li>
<li><a href="SchemeOutstandRpt.aspx"><span>Scheme Wise Outstand</span></a></li>

                                    
        </ul>
    </li>
    
    <li><div class="menuleft"></div><a href='#'><span>DEBENTURE  REPORTS</span></a>
        <ul>
        <li><a href="DepositReports.aspx"><span>Maturity Report</span></a></li>
        <li><a href="DepositReports.aspx"><span>Opening Register</span></a></li>
        <li><a href="DepositReports.aspx"><span>Closing Register</span></a></li>
        <li><a href="DepsitSeriesOutstandRpt.aspx"><span>Debenture Series Outstand</span></a></li>
         <li><a href="NominationRegisterRpt.aspx"><span>Nominee Details</span></a></li>  
        </ul>
    </li>
    
    
    <li><div class="menuleft"></div><a href='#'><span>MIS  REPORTS</span></a>
        <ul>
        <li><a href="LoanRisk.aspx"><span>GOlD Analysis</span></a></li>
        <li><a href="MIS_setting.aspx"><span>MIS Settings</span></a></li>
        <li><a href="MIS_Report.aspx?Type=MisReport"><span>MIS Analysis</span></a></li>
        <li><a href="BudgetRpt.aspx" ><span>Budget Report</span></a></li>
                        <li><a href="#" class="parent"><span>Fixed Asset </span></a>
                            <div><ul>
                            <li><a href="AssetRegister_rpt1.aspx"  ><span>Asset Register</span></a></li>
                            <li><a href="AssetStock_rpt.aspx"  ><span>Asset Stock</span></a></li>
                            <li><a href="AssetDepreciation_rpt.aspx"   ><span>Depreciation</span></a></li>
                             <li><a href="PostDepreciation.aspx"><span>Depreciation Post/Report </span></a></li>
                             </ul></div>
                        </li>  
           
        </ul>
    </li>
    <li><div class="menuleft"></div><a href='#'><span>Pay Roll</span></a>
        <ul>
        <li><a href="EmpMaster.aspx"><span>Employee Master</span></a></li>
        <li><a href="EmpLeave.aspx"><span>Leave Master</span></a></li>
        <li><a href="LeaveBalance.aspx" ><span>Leave Balance</span></a></li>
        <li><a href="EmpLeavesettings.aspx"><span>Leave Settings</span></a></li>
        <li><a href="Emp_PayScale.aspx" ><span>Pay Scale</span></a></li>
        
        <li><a href="PaySettings.aspx" ><span>Pay Settings</span></a></li>
        <li><a href="BulkUploadPayrollProcess.aspx" ><span>Salary Posting</span></a></li>
           <li><a href="#" class="parent"><span>Salary Reports</span></a>
                    <div><ul>
                    	<li><a href="AcquiantanceRpt.aspx"><span>Acquiantance Report</span></a></li> 
                    	<li><a href='SalaryRpt.aspx'><span>Salary Type</span></a></li>
                    		<li><a href="Salarystmt.aspx"><span>PaySlip</span></a></li>
                    		<li><a href="Grativity.aspx"><span>Gratuvity</span></a></li>
                        </ul>
                    </div>
           </li>
        
        
           
        </ul>
    </li>
    
    
    <li><div class="menuleft"></div><a href='#'><span>ACCOUNTS REPORTS</span></a>
        <ul>
        <li><a href="DailyReports.aspx"><span>Cash Book</span></a>
        <ul>
        <li><a href="BalanceSheetDrillingDown.aspx"><span>Balalnce Drill Down</span></a></li>
        <li><a href="DailyCashRpt.aspx"><span>Cash Balance Report</span></a></li>
        <li><a href="BankBalance.aspx"><span>Bank Balance Report</span></a></li> 
        <li><a href="CmpFinalAcc_Frm.aspx"><span>Balalnce Sheet Comparison</span></a></li>       
        <li><a href="Transactionlist.aspx"><span>Transaction report</span></a></li>
        <li><a href="TranListGroupWise_Rpt.aspx"><span>Transaction Report(Group)</span></a></li>
        </ul>
        </li>
        <li><a href="DailyReports.aspx"><span>Daybook</span></a></li>
        <li><a href="DailyReports.aspx"><span>Receipts& Disbursement</span></a></li>
        <li><a href="DailyReports.aspx"><span>General Ledger</span></a></li>
        <li><a href="TrialBal.aspx"><span>Trial Balance</span></a></li>
        <li><a href="ProfitLoss.aspx"><span>Profit & Loss</span></a></li>
        <li><a href="BalanceSheet.aspx"><span>Balalnce Sheet</span></a></li>
    

        </ul>
    </li>
    <li><div class="menuleft"></div><a href='#'><span>Others </span></a>
        <ul>
            <li><a href="BondPrint.aspx"><span>Cancellation Report</span></a></li>
            <li><a href="TransactionLogReport.aspx"><span>User Log Report</span></a></li>
            <li><a href="Customermodified_rpt.aspx"><span>Customer Updation Log</span></a></li>
            <%--<li><a href="DailyBusinessRpt.aspx"><span>Daily Business Report</span></a></li>--%>
              <li><a href="CibilReport.aspx"><span>Cibil Report</span></a></li>
 <li><a href="IncentiveCanvasserDetails_Rpt.aspx"><span>Incentive Canvasser Report</span></a></li>
 <li><a href="GuarantorCustomerDetails_Rpt.aspx"><span>Customer guarantors Report</span></a></li>
            
            
        </ul>
    </li>
</ul>
</div>
    </div>
    </asp:Panel>

<asp:Panel ID= "AdminMenu" runat="server" style="float:left;" Visible="false" >
    <div class="Menuparent">    
        <div class="dropleft">
<ul>
<li><div class="menuleft"></div><a href='#'><span>System Admin</span></a>
    <ul>
<li><a href="UserReg.aspx"><span>User Creation</span></a></li>
<ul>
<li><a href="UserEmployee.aspx"><span>User Employee Registration</span></a></li>
<%--<li><a href="#"><span>Group Creation</span></a></li>--%>
<li><a href="GroupRights.aspx"><span>Group Rights</span></a></li>
<li><a href="Userrights.aspx"><span>User Rights</span></a></li>
<li><a href="SchemeMaster.aspx"><span>Scheme Creation</span></a></li>
<li><a href="Gramrate.aspx"><span>Gram Rate Settings</span></a></li>
<li><a href="LoanRate.aspx"><span>Loan Rate Settings</span></a></li>
<li><a href="AccountMaster.aspx"><span>Account Head Creation</span></a></li>
<li><a href="AccountGroupMaster.aspx"><span>Account Group Creation</span></a></li>
<li><a href="InterestRateMaster.aspx"><span>Interest Rate Settings</span></a></li>
<li><a href="PickUpMaster.aspx"><span>Pickup Value Settings</span></a></li>
<li><a href="EmpMaster.aspx"><span>Employee master</span></a></li>
<li><a href="ChangeBranch.aspx"><span>Change Branch / DeActive</span></a></li>
<li><a href="ChangeUserAuthorisation.aspx"><span>Transfer User Rights</span></a></li>
<li><a href="RegionMaster.aspx"><span>Region Master</span></a></li>
<li><a href="AreaMaster.aspx"><span>Area Master</span></a></li>
<li><a href="BranchAreaMapping.aspx"><span>Branch Area Mapping</span></a></li>
<li><a href="BranchDetails.aspx"><span>Branch Details</span></a></li>
</ul>
<li><a href="Deposit_Parameter.aspx"><span>Deposit Parameter</span></a></li>
<li><a href="LoanParameter.aspx" ><span>Loan Parameter</span></a></li>
<li><a href="MembershipParameter.aspx" ><span>Share Parameter</span></a></li>
<li><a href="CanvasedBy.aspx" ><span>Canvased By</span></a></li>
<li><a href="Bank_Account.aspx" ><span>Bank Accounts</span></a></li>
<li><a href="ManagerPassingEdit.aspx"><span>Transaction Edit </span></a></li>
<li><a href="ProcessingFeeWaiver.aspx"><span>Processing Fee Waiver </span></a></li>
<li><a href="DueDateExtender.aspx"><span>Due Date Extender </span></a></li>
<li><a href="Budget.aspx" ><span>Budget Settings</span></a></li>
<li><a href="BudgetRpt.aspx" ><span>Budget Report</span></a></li>
<li><a href="Remainder.aspx" ><span>Reminder Settings</span></a></li>
<li><a href="DebentureSeriesAdd.aspx" ><span>Debenture Series</span></a></li>
<li><a href="DebRpt_Series.aspx" ><span>Debenture Sub Series</span></a></li>
<li><a href="UserChangeGroup.aspx" ><span>User Change Group</span></a></li>
</ul>
</li>
<li><div class="menuleft"></div><a href='#'><span>CUSTOMER</span></a>
    <ul>
       <li><a href="CustomerRegister.aspx"><span>CUSTOMER REGISTER</span></a></li>
       <li><a href="cust_submission.aspx" ><span>CUSTOMER 15-H SUBMISSION</span></a></li>
       <li><a href="VerifyBnkDtls.aspx" ><span>CUSTOMER BANK DETAILS</span></a></li>
</ul>
</li>

<li><div class="menuleft"></div><a href='#'><span>LOAN PROCESS</span></a>
    <ul>
       <li><a href="LoanApplicationForm.aspx"><span>LOAN APPLICATION</span></a></li>
       <li><a href="LoanApplication.aspx"><span>LOAN APPLICATION STAGE 1</span></a></li>
       <li><a href="LoanPIForm.aspx"><span>LOAN APPLICATION STAGE 2</span></a></li>
       <li><a href="LoanCreditApproval.aspx"><span>LOAN APPLICATION STAGE 3</span></a></li>
       <li><a href="LoanDisbursal.aspx"><span>LOAN APPLICATION STAGE 4</span></a></li>
       <li><a href="Loan_appDetail.aspx"><span>LOAN PROCESS REPORT</span></a></li>
       

</ul>
</li>

<li><div class="menuleft"></div><a href='#'><span>TRANSACTION</span></a>
<ul>
    <li><a href="AccOpen.aspx"><span>ACCOUNT OPEN</span></a></li>
<li><a href="Transaction.aspx"><span>ACCOUNT TRANSACTION</span></a></li>
<li><a href="CashTr.aspx"><span>CASH/FUND TRANSFER</span></a></li>
<li><a href="LoanActionTaken.aspx"><span>LOAN ACTION TAKEN ENTRY</span></a></li>
<li><a href="NEFT_IntPayment.aspx"><span>DEPOSIT BULK INT.POST</span></a></li>
<%--<li><a href="Entry_change.aspx"><span>ENTRY MODE CHANGE</span></a></li>--%>
<%--<li><a href="Bank_Reconciliation.aspx"><span>BANK RECONCILIATION</span></a></li>--%>
<li><a href="TicketMaster.aspx"><span>TICKET MASTER</span></a></li>
                     <li><a href="#" class="parent"><span>Fixed Asset</span></a>
                            <div><ul>
                            <li><a href="AssetTransaction.aspx?RptType=PURCHASE"  ><span>PURCHASE</span></a></li>
                            <li><a href="AssetTransaction.aspx?RptType=SALES"  ><span>SALES</span></a></li>
                            <li><a href="AssetTransaction.aspx?RptType=TRANSFER"  ><span>TRANSFER</span></a></li>
                            <li><a href="AssetTransaction.aspx?RptType=AUCTION"  ><span>AUCTION</span></a></li>
                            <li><a href="AssetTransaction.aspx?RptType=DAMAGE"  ><span>DAMAGE</span></a></li>
                            <li><a href="AssetGroup.aspx" ><span>Asset Group</span></a></li>
                            <li><a href="AssetMaster.aspx" ><span>Asset Master</span></a></li>
                             </ul></div>
                        </li>

</ul>
</li>

<%--<li><div class="menuleft"></div><a href="ManagerPassing.aspx"><span>AUTHORISATION</span></a></li>--%>
    <li><div class="menuleft"></div><a href='#'><span>AUTHORISATION</span></a>
        <ul>
            <li><a href="ManagerPassing.aspx"><span>AUTHORISATION</span></a></li>
            <li><a href="NEFT_PaymentApproval.aspx"><span>DEPOSIT BULK PAYMENT</span></a></li>
        </ul>
    </li>
    <li><div class="menuleft"></div><a href='#'><span>Utilities</span></a>
        <ul>
            <li><a href="TranCancellation.aspx"><span>Transaction Cancellation</span></a></li>
<li><a href="BondPrint.aspx"><span>Duplicate Printing</span></a></li>
<li><a href="ControlBalanceReport.aspx"><span>Control Balance</span></a></li>
<li><a href="ChangePwd.aspx"><span>Change Password</span></a></li>
<li><a href="LoginUserStatus.aspx"><span>Login User status</span></a></li>
<li><a href="ChittySuertySecurity.aspx"><span>Guarantor Details</span></a></li>
        </ul>
    </li>
    <li><div class="menuleft"></div><a href='#'><span>Dayend</span></a>
        <ul>
            <li><a href="DayBeginEnd.aspx"><span>Daybegin</span></a></li>
            <li><a href="DayBeginEnd.aspx"><span>Dayend</span></a></li>
            <li><a href="EmpAttendence.aspx" ><span>Attendence Marking</span></a></li>
<li><a href="NoticePrinting.aspx"><span>Notice Printing</span></a></li>
<li><a href="DayEndStatus.aspx" ><span>Branch DayEnd Status</span></a></li>
        </ul>
    </li>
    <li><div class="menuleft"></div><a href='#'><span>Customer  REPORTS</span></a>
        <ul>
        <li><a href="CustAssetLiabRpt.aspx"><span>Assets and Liabilities</span></a></li>
            <li><a href="PersonalLedger.aspx"><span>PERSONAL LEDGER</span></a></li>
            
<li><a href="cust_reg_Report.aspx"><span>Customer Register Report</span></a></li>
<li><a href="CustDetRpt.aspx"><span>Customer Details Report</span></a></li>
<li><a href="15H_Rpt.aspx"><span>15-G Submit Report</span></a></li>
<li><a href="NotSubmitPanCard.aspx"><span>Pan Card Report</span></a></li>

        </ul>
    </li>
    
    <li><div class="menuleft"></div><a href='#'><span>Gold REPORTS</span></a>
        <ul>
        <li><a href="PersonalLedger.aspx"><span>Personal Ledger</span></a></li>
            <li><a href="GoldLoanOpenClose_Report.aspx.aspx"><span>Opening Closing Report</span></a></li>
<li><a href="GoldRedemptionReport.aspx"><span>Stock Report</span></a></li>
<li><a href="GoldOrnamentsAppraiserDtlsEntry.aspx"><span>Gold Ornaments Details</span></a></li>
<li><a href="GoldOrnamentsAppraiserDtlsReport.aspx"><span>Gold Ornaments Appraiser Report</span></a></li>
<li><a href="RptOutstanding.aspx"><span>Outstanding Report</span></a></li>
<li><a href="LoanReports.aspx"><span>Overdue Report</span></a></li>
<li><a href="InterestPayable.aspx"><span>Interest Receivable Report</span></a></li>
<li><a href="LoanEMIRpt.aspx"><span>Loan EMI Report</span></a></li>
<li><a href="NoticePrinting.aspx"><span>Notice Printing</span></a></li>

        </ul>
    </li>
    <li><div class="menuleft"></div><a href='#'><span>Loan  REPORTS</span></a>
        <ul>
        <li><a href="PersonalLedger.aspx"><span>Personal Ledger</span></a>
        <ul>
            <%--<li><a href="EMI_DueList_Rpt.aspx?RptType=Daily"><span>Daily DueList</span></a></li>--%>
            <li><a href="EMI_DueList_Rpt.aspx"><span>EMI DueList</span></a></li>
            <li><a href="DailyCollectionReport.aspx"><span>Daily Collection Report</span></a></li>
            <li><a href="EMI_BktReport.aspx"><span>EMI Bucket Wise Rpt</span></a></li>
            <li><a href="InterestDueList.aspx"><span>Demand List</span></a></li>
            <li><a href='Loan_BDR_Report.aspx'><span>BDR Statement</span></a></li>
            <li><a href="LoanDCBReport.aspx"><span>DCB</span></a></li>
            <li><a href= "InterestPayable.aspx?RptType=Receivable" ><span>Interest Receivable</span></a></li>
            <li><a href="FluidResRpt.aspx"><span>Fluid Report</span></a></li>
            <li><a href="NoticePrinting.aspx"><span>Notice Printing</span></a></li>
            <li><a href="InterestDemandRpt.aspx"><span>FollowUp List</span></a></li>
            <li><a href="GuarantorDetails_Rpt.aspx"><span>Guarantor Details</span></a></li>
        </ul>
        </li>
            <li><a href="LoanEMIRpt.aspx"><span>Loan EMI Report</span></a></li>
            <li><a href="LoanOpenClose_Report.aspx"><span>Opening Closing Report</span></a></li>
<%--<li><a href='#'><span>Stock Report</span></a></li>--%>
            <li><a href="RptOutstanding.aspx"><span>Outstanding Report</span></a></li>
            <li><a href="LoanReports.aspx"><span>Overdue Report</span></a></li>
            <%--<li><a href="Loan_NPA_Rpt.aspx"><span>NPA Report</span></a></li>--%>
            <li><a href="NPA_Prdwise_Report.aspx"><span>NPA Report</span></a></li>
 	    <li><a href="NPARptRegionwise.aspx"><span>NPA AREA WISE Report</span></a></li>
            <li><a href="SuspenseCash.aspx"><span>Suspense Balance Report</span></a></li>
            <li><a href="Outstand.aspx"><span>Outstand Type2</span></a></li>
            <li><a href="OutstandConsolidate.aspx"><span>Outstanding Consolidate</span></a></li>
            <li><a href="SchemeOutstandRpt.aspx"><span>Scheme Wise Outstand</span></a></li>

                                    
                                    
        </ul>
    </li>
    
    
     <li><div class="menuleft"></div><a href='#'><span>DEBENTURE  REPORTS</span></a>
        <ul>
        <li><a href="DepositReports.aspx"><span>Maturity Report</span></a></li>
        <li><a href="DepositReports.aspx"><span>Opening Register</span></a></li>
        <li><a href="DepositReports.aspx"><span>Closing Register</span></a></li>
        <li><a href="DepsitSeriesOutstandRpt.aspx"><span>Debenture Series Outstand</span></a></li>
        <li><a href="NominationRegisterRpt.aspx"><span>Nominee Details</span></a></li>
           
        </ul>
    </li>
    
     <li><div class="menuleft"></div><a href='#'><span>MIS  REPORTS</span></a>
        <ul>
        <li><a href="LoanRisk.aspx"><span>GOlD Analysis</span></a></li>
        <li><a href="BudgetRpt.aspx" ><span>Budget Report</span></a></li>
        <li><a href="MIS_setting.aspx"><span>MIS Settings</span></a></li>
        <li><a href="MIS_Report.aspx?Type=MisReport"><span>MIS Analysis</span></a></li>
        
                        <li><a href="#" class="parent"><span>Fixed Asset </span></a>
                            <div><ul>
                            <li><a href="AssetRegister_rpt1.aspx"  ><span>Asset Register</span></a></li>
                            <li><a href="AssetStock_rpt.aspx"  ><span>Asset Stock</span></a></li>
                            <%--<li><a href="AssetDepreciation_rpt.aspx"   ><span>Depreciation</span></a></li>--%>
                            <li><a href="Depreciation_Rpt.aspx" ><span>Depreciation</span></a></li>
                            <li><a href="PostDepreciation.aspx"><span>Depreciation Post/Report </span></a></li>                            
                             </ul></div>
                        </li>  
           
        </ul>
        
     
    </li>
    <li><div class="menuleft"></div><a href='#'><span>Pay Roll</span></a>
        <ul>
        <li><a href="EmpMaster.aspx"><span>Employee Master</span></a></li>
        <li><a href="EmpLeave.aspx"><span>Leave Master</span></a></li>
        <li><a href="LeaveBalance.aspx" ><span>Leave Balance</span></a></li>
        <li><a href="EmpLeavesettings.aspx"><span>Leave Settings</span></a></li>
        <li><a href="Emp_PayScale.aspx" ><span>Pay Scale</span></a></li>
        <li><a href="PaySettings.aspx" ><span>Pay Settings</span></a></li>
        <li><a href="BulkUploadPayrollProcess.aspx" ><span>Salary Posting</span></a></li>
           <li><a href="#" class="parent"><span>Salary Reports</span></a>
                    <div><ul>
                    	<li><a href="AcquiantanceRpt.aspx"><span>Acquiantance Report</span></a></li> 
                    	<li><a href='SalaryRpt.aspx'><span>Salary Type</span></a></li>
                    		<li><a href="Salarystmt.aspx"><span>PaySlip</span></a></li>
                    		<li><a href="Grativity.aspx"><span>Gratuvity</span></a></li>
                        </ul>
                    </div>
           </li>
        
           
        </ul>
    </li>
    
    
    <li><div class="menuleft"></div><a href='#'><span>ACCOUNTS REPORTS</span></a>
        <ul>
        <li><a href="DailyReports.aspx"><span>Cash Book</span></a>
        <ul>
        <li><a href="ProfitLoss.aspx"><span>Profit & Loss</span></a></li>
        <li><a href="BalanceSheet.aspx"><span>Balalnce Sheet</span></a></li>
        <li><a href="BalanceSheetDrillingDown.aspx"><span>Balalnce Sheet DrillDown</span></a></li>
        <li><a href="CmpFinalAcc_Frm.aspx"><span>Balalnce Sheet Comparison</span></a></li>
        <li><a href="Transactionlist.aspx"><span>Transaction report</span></a></li>
        <li><a href="TranListGroupWise_Rpt.aspx"><span>Transaction Report(Group)</span></a></li>
        </ul>
        </li>
        <li><a href="DailyReports.aspx"><span>Daybook</span></a></li>
        <li><a href="DailyCashRpt.aspx"><span>Cash Balance Report</span></a></li>
        <li><a href="BankBalance.aspx"><span>Bank Balance Report</span></a></li>
        <li><a href="DailyReports.aspx"><span>Receipts& Disbursement</span></a></li>
        <li><a href="DailyReports.aspx"><span>General Ledger</span></a></li>
        <li><a href="TrialBal.aspx"><span>Trial Balance</span></a></li>

        </ul>
    </li>
    <li><div class="menuleft"></div><a href='#'><span>Others </span></a>
        <ul>
            <li><a href="TranCancelRpt.aspx"><span>Cancellation Report</span></a></li>
            <li><a href="TransactionLogReport.aspx"><span>User Log Report</span></a></li>
            <li><a href="TicketMaster_Report.aspx"><span>Ticket Register Report</span></a></li>
	    <li><a href="BulkUploadLoanAction.aspx"><span>Loan Action Bulkupload</span></a></li>
            <li><a href="Customermodified_rpt.aspx"><span>Customer Updation Log</span></a></li>
            
        </ul>
    </li>
  
    <asp:Panel ID="HoReports" runat="server">
      <li><div class="menuleft"></div><a href='#'><span>HO REPORTS</span></a>
        <ul>
            <li><a href="IncomeExpenseRpt.aspx"><span>MIS Reports</span></a>
            <ul>
            <li><a href="IntOutstandRpt.aspx"><span>Interest Outstand Report </span></a></li>
            <li><a href="LoanAnalysisRpt.aspx"><span>Loan Analysis Report </span></a></li>
            <li><a href="CanvassedBy_Rpt.aspx"><span>Canvasser Report </span></a></li>
            <li><a href="CompanyPosition.aspx"><span>Company Position </span></a></li>
             <li><a href="LoanLegalNotice.aspx"><span>Legal Notice</span></a></li>
            </ul>
            </li>            
            <li><a href="DailyBusinessRpt.aspx"><span>Daily Business Report </span></a></li>
            <li><a href="DepositReports.aspx"><span>Deposit Reports</span></a></li>
            <li><a href="IntRcvdPayableRpt.aspx"><span>Interest Report </span></a></li>
            <li><a href="OD_Int_Checking.aspx"><span>Busines Loan OD</span></a></li>
           
           <li><a href="DailyLoanReport.aspx"><span>Daily GL Report</span></a></li>
           <li><a href="BhartiAxa.aspx"><span>Bharti Axa-Insurance Report</span></a></li>
            
            
        </ul>
    </li>
    </asp:Panel>
  
</ul>
</div>
    </div>
    </asp:Panel>

</div>
</td><td width="85%" style="vertical-align:top;">
<div style="width:55%;float:left;">
  
    <%--<div style="width:312px; height:40px; float:left; border-top:1px solid #2063b7; border-right:1px solid #2063b7; border-left:1px solid #2063b7; border-radius:6px;"  >
        <div id="Heading" class="Mainheadshow" onclick="show('div_Heading');hide('div_Chart');setclass('Heading','Chart');">Main Heading</div>
        <div  id="Chart" class="Mainheadcss" onclick="show('div_Chart');hide('div_Heading');setclass('Chart','Heading');" style="margin-left:1px;">Bank Position</div>
    </div> --%> 
    
       
   <div id="div_Heading" style="width:100%;  float:left; display:block; border:1px solid #2063b7; margin-top:-2px; ">
         <div style="width:100%; float:left;">
            <div style="width:100%; height:70px; float:left; margin-top:20px;">
                <div class="subbtn" ><a href="CustomerRegister.aspx"><span class="subbtnarr"></span> Registration</a></div>
                 <div class="subbtn" ><a href="AccOpen.aspx" ><span class="subbtnarr"></span>A/C Open</a></div>
                 <div class="subbtn" ><a href="Transaction.aspx" ><span class="subbtnarr"></span>A/C Transaction</a></div>
                 
            </div>
             <div style="width:100%; height:70px; float:left;">
                <div class="subbtn" ><a href="ManagerPassing.aspx" ><span class="subbtnarr"></span>Authorisation</a></div>
                  <div class="subbtn" ><a href="TranCancellation.aspx" ><span class="subbtnarr"></span>Cancellation</a></div>
                 <div class="subbtn" ><a href="BondPrint.aspx" ><span class="subbtnarr"></span>Duplicate Printing</a></div>
                
             </div>
             <div style="width:100%; height:70px; float:left;">
                 <div class="subbtn" ><a href="ControlBalanceReport.aspx" ><span class="subbtnarr"></span>Control Balance</a></div>
                 <div class="subbtn" ><a href="DailyReports.aspx" ><span class="subbtnarr"></span>Daily Statements</a></div>
                 <div class="subbtn" ><a href="RptOutstanding.aspx" ><span class="subbtnarr"></span>Outstanding</a></div>
             </div>
             <div style="width:100%; height:70px; float:left;">
                  <div class="subbtn" ><a href="PersonalLedger.aspx" ><span class="subbtnarr"></span>Personal Ledger</a></div>
                 <div class="subbtn" ><a href="GoldRedemptionReport.aspx" ><span class="subbtnarr"></span>Gold Stock</a></div>
                 <%--<div class="subbtn" ><a href="LoanApproval.aspx" ><span class="subbtnarr"></span>Loan Sanction</a></div>--%>
             </div>
<%--              <div style="width:100%; height:70px; float:left;">
                  <div class="subbtn" ><a href="ChequeIssueMaster.aspx" ><span class="subbtnarr"></span>Cheque Issue</a></div>
                 <div class="subbtn" ><a href="ChequeReturn.aspx" ><span class="subbtnarr"></span>Cheque Return</a></div>
                 <div class="subbtn" ><a href="LoanApplication.aspx" ><span class="subbtnarr"></span>Loan Application</a></div>
             </div>--%>
<%--              <div style="width:100%; height:70px; float:left;">
                  <div class="subbtn" ><a href="LoanApplicationForm.aspx" ><span class="subbtnarr"></span>LA Form</a></div>
                 <div class="subbtn" ><a href="LoanPIForm.aspx" ><span class="subbtnarr"></span>Property Inspection</a></div>
                 <div class="subbtn" ><a href="LoanRIForm.aspx" ><span class="subbtnarr"></span>Record Inspection</a></div>
             </div>--%>
             <div style="width:100%; height:70px; float:left;">
                  
                 <div class="subbtn" ><a href="DayBeginEnd.aspx" ><span class="subbtnarr"></span>Day End</a></div>
                 
             </div>
             
             <div style="width:100%; float:left; ">           
                      <%--<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Width="100%" CssClass="grid-viewNew" >
                      <Columns>
                          <asp:BoundField DataField="PARTICULARS" HeaderText="TO DO LIST..."  ItemStyle-HorizontalAlign="Left"  HeaderStyle-HorizontalAlign="Left" />    
                      </Columns>
                  </asp:GridView>    --%>     
                  
                  <asp:GridView ID="DgvSummary" runat="server" CssClass="grid-viewCSS" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" >
                 <Columns>
                     <asp:BoundField  DataField="Particulars" HeaderText="Particulars" />
                     <asp:BoundField DataField="Value" HeaderText="Amount" />
                 </Columns>

             </asp:GridView>  
             </div>
         </div>
         <%--<div style="width:39%; float:left; border-left:2px solid #dfe0e4; padding-left:1%;" >
             <div style="width:100%; float:left;">
             <asp:GridView ID="DgvSummary" runat="server" CssClass="grid-viewCSS" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" >
                 <Columns>
                     <asp:BoundField  DataField="Particulars" HeaderText="Particulars" />
                     <asp:BoundField DataField="Value" HeaderText="Amount" />
                 </Columns>

             </asp:GridView>
                 </div>

              <div style="width:100%; float:left;">
                   <asp:GridView ID="DgvDeposit" runat="server" CssClass="grid-viewCSS" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" >
                 <Columns>
                     <asp:BoundField DataField="Cust_Name" HeaderText="Cust Name" />
                     <asp:BoundField DataField="Acc_No" HeaderText="A/C No." />
                     <asp:BoundField DataField="Dm_DepAmount" HeaderText="Amount" />
                     <asp:BoundField DataField="Cust_Mobile" HeaderText="Mobile No." />
                 </Columns>

             </asp:GridView>

              </div>
              <div style="width:100%; float:left;">
                 
                   <asp:GridView ID="DgvLoan" runat="server" CssClass="grid-viewCSS" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" >
                 <Columns>
                     <asp:BoundField  DataField="Cust_Name" HeaderText="Cust Name" />
                     <asp:BoundField DataField="Acc_No" HeaderText="A/C No." />
                     <asp:BoundField DataField="LoanAmt" HeaderText="Amount" />
                     <asp:BoundField DataField="Balance" HeaderText="Balance" />
                     <asp:BoundField DataField="Cust_Mobile" HeaderText="Mobile No." />
                 </Columns>

             </asp:GridView>

              </div>
             </div>--%>
     </div>
    </div>
 <div style="float:left; width:44%">
 
  <div id="DivChart" style="width:100%; float:left; border-left:2px solid #dfe0e4; padding-left:1%;" runat="server">
             <div  class="charthead" >Branch Position</div>

              <%--<div class="charttable">
             <div id ="DivChartHeading" class="chartsubhead" runat="server">Share Application Money</div>
                  <asp:GridView ID="Grid_Share" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Width="100%" CssClass="grid-viewCSS" >
                      <Columns>
                          <asp:BoundField DataField="PARTICULARS" HeaderText="PARTICULARS"   >
                          </asp:BoundField>
                          <asp:BoundField DataField="TOTALACCOUNT" HeaderText="TOTAL"  >

                          <ItemStyle HorizontalAlign="Right" />
                          </asp:BoundField>
                          <asp:BoundField DataField="SHAREELIGIBLE" HeaderText="ELIGIBLE SHARE"  >

                          <ItemStyle HorizontalAlign="Right" />
                          </asp:BoundField>
                      </Columns>
                  </asp:GridView>
            
              <div class="charttablehead" style="width:50%; height:130px;">
                  <asp:CHART id="ChartShare_Ac" runat="server" BackColor="WhiteSmoke" Height="127px" Width="190px" BorderlineDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2px" BorderColor="#1A3B69" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" RightToLeft="No" BackImageTransparentColor="Turquoise">

							<Legends>
                                <asp:Legend BackImageAlignment="Bottom" BackImageWrapMode="TileFlipY" LegendStyle="Row" Name="Legend2">
                                    <position height="18.5344849" width="81.6666641" x="1.47395706" y="70.4655151" />                                </asp:Legend>
                            </Legends>

							<series>
								<asp:Series Name="Default"  ChartType="Pie" BorderColor="180, 26, 59, 105" Color="220, 65, 140, 240" Legend="Legend2"  CustomProperties="PieDrawingStyle=SoftEdge" ToolTip="Value of X: #VALY Value of Y #VALY"></asp:Series>
							</series>
							<chartareas>
								<asp:ChartArea Name="ChartArea1" BorderColor="34, 34, 34, 34" BackSecondaryColor="Transparent"  BackColor="Transparent" ShadowColor="Transparent" BorderWidth="0" AlignmentOrientation="All">
									<position height="65" width="65" x="10.80710649" y="15.35987" />
                                    <innerplotposition height="89.5" width="54.7193565" x="8.64032" />
									<area3dstyle Rotation="0" Inclination="10" IsClustered="True" IsRightAngleAxes="False" LightStyle="Realistic" />
									<axisy LineColor="34, 34, 34, 34">
										
										<MajorGrid LineColor="34, 34, 34, 34" />
									</axisy>
									<axisx LineColor="34, 34, 34, 34">
										
										<MajorGrid LineColor="34, 34, 34, 34" />
									</axisx>
								</asp:ChartArea>
							</chartareas>
							<titles>
								<asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 10.25pt, style=Bold" ShadowOffset="3" Text="No of Accounts" Name="Title1" ForeColor="26, 59, 105">
                                    <Position Height="19.3940639" Width="84.234375" X="6.90625" Y="1.952381" />
                                </asp:Title>
							</titles>
						    <BorderSkin PageColor="Transparent" SkinStyle="Emboss" />
						</asp:CHART>
              </div>
             <div class="charttablehead" style="width:49%; height:130px; "> 

                <asp:CHART id="ChartShare_outSt" runat="server" BackColor="WhiteSmoke" Height="127px" Width="190px" BorderlineDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2px" BorderColor="#1A3B69" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" RightToLeft="No" BackImageTransparentColor="Turquoise">

							<Legends>
                                <asp:Legend BackImageAlignment="Bottom" BackImageWrapMode="TileFlipY" LegendStyle="Row" Name="Legend2">
                                    <position height="18.5344849" width="81.6666641" x="1.47395706" y="70.4655151" />
                                </asp:Legend>
                            </Legends>

							<series>
								<asp:Series Name="Default" ChartType="Pie" BorderColor="180, 26, 59, 105" Color="220, 65, 140, 240" Legend="Legend2" CustomProperties="PieDrawingStyle=SoftEdge"></asp:Series>
							</series>
							<chartareas>
								<asp:ChartArea Name="ChartArea1" BorderColor="34, 34, 34, 34" BackSecondaryColor="Transparent" BackColor="Transparent" ShadowColor="Transparent" BorderWidth="0" AlignmentOrientation="All">
									<position height="65" width="65" x="10.80710649" y="15.35987" />
                                    <innerplotposition height="89.5" width="54.7193565" x="8.64032" />
									<area3dstyle Rotation="0" Inclination="10" IsClustered="True" IsRightAngleAxes="False" LightStyle="Realistic" />
									<axisy LineColor="34, 34, 34, 34">
										
										<MajorGrid LineColor="34, 34, 34, 34" />
									</axisy>
									<axisx LineColor="34, 34, 34, 34">
										
										<MajorGrid LineColor="34, 34, 34, 34" />
									</axisx>
								</asp:ChartArea>
							</chartareas>
						    <Titles>
                               <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 10.25pt, style=Bold" ShadowOffset="3" Text="OutStanding" Name="Title1" ForeColor="26, 59, 105">
                                    <Position Height="19.3940639" Width="84.234375" X="6.90625" Y="1.952381" />
                                </asp:Title>
                            </Titles>
						    <BorderSkin PageColor="Transparent" SkinStyle="Emboss" />
						</asp:CHART>
             </div>
             </div>--%>
             <%--<div class="charttable">
                  <div class="chartsubhead">dEPOSIT dETAILS</div>
           
         <asp:GridView ID="Grid_Depost" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Width="100%" CssClass="grid-viewCSS" >
                      <Columns>
                          <asp:BoundField DataField="PARTICULARS" HeaderText="PARTICULARS" />
                          <asp:BoundField DataField="noofaccounts" HeaderText="NO OF ACCOUNTS" >
                          <ItemStyle HorizontalAlign="Right" />
                          </asp:BoundField>
                          <asp:BoundField DataField="outstanding" HeaderText="OUTSTANDING" >
                          <ItemStyle HorizontalAlign="Right" />
                          </asp:BoundField>
                      </Columns>
                  </asp:GridView>
              <div class="charttablehead" style="width:50%; height:130px;">
                   <asp:CHART id="ChartDeposit_Ac" runat="server" BackColor="WhiteSmoke" Height="127px" Width="190px" BorderlineDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2px" BorderColor="#1A3B69" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" RightToLeft="No" BackImageTransparentColor="Turquoise">

							<Legends>
                                <asp:Legend BackImageAlignment="Left" BackImageWrapMode="TileFlipY" LegendStyle="Column" Name="Legend2"  >
                                    <position height="90" width="30 "  X="65"  />
                                </asp:Legend>
                            </Legends>

							<series>
								<asp:Series Name="Default" ChartType="Pie" BorderColor="180, 26, 59, 105" Color="220, 65, 140, 240" Legend="Legend2" CustomProperties="PieDrawingStyle=SoftEdge" MarkerStyle="Circle"></asp:Series>
							</series>
							<chartareas>
								<asp:ChartArea Name="ChartArea1" BorderColor="34, 34, 34, 34" BackSecondaryColor="Transparent" BackColor="Transparent" ShadowColor="Transparent" BorderWidth="0" AlignmentOrientation="All">
									<position height="95" width="95" x="0.80710649" y="15.35987" />
                                    <innerplotposition height="89.5" width="54.7193565" x="8.64032" />
									<area3dstyle Rotation="0" Inclination="10" IsClustered="True" IsRightAngleAxes="False" LightStyle="Realistic" />
									<axisy LineColor="34, 34, 34, 34">
										
										<MajorGrid LineColor="34, 34, 34, 34" />
									</axisy>
									<axisx LineColor="34, 34, 34, 34">
										
										<MajorGrid LineColor="34, 34, 34, 34" />
									</axisx>
								</asp:ChartArea>
							</chartareas>
						    <Titles>
                                <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 10.25pt, style=Bold" ShadowOffset="3" Text="No of Accounts" Name="Title1" ForeColor="26, 59, 105">
                                    <Position Height="19.3940639" Width="84.234375" X="0" Y="1.952381" />
                                </asp:Title>
                            </Titles>
						    <BorderSkin PageColor="Transparent" SkinStyle="Emboss" />
						</asp:CHART>
              </div>
             <div class="charttablehead" style="width:49%; height:130px;">
                  <asp:CHART id="ChartDeposit_OutSt" runat="server" BackColor="WhiteSmoke" Height="127px" Width="190px" BorderlineDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2px" BorderColor="#1A3B69" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" RightToLeft="No" BackImageTransparentColor="Turquoise">

							<Legends>
                                <asp:Legend BackImageAlignment="Bottom" BackImageWrapMode="TileFlipY" LegendStyle="Column" Name="Legend2">
                                    <position height="90" width="30" x="65"  />
                                </asp:Legend>
                            </Legends>

							<series>
								<asp:Series Name="Default" ChartType="Pie" BorderColor="180, 26, 59, 105" Color="220, 65, 140, 240" Legend="Legend2" CustomProperties="PieDrawingStyle=SoftEdge"></asp:Series>
							</series>
							<chartareas>
								<asp:ChartArea Name="ChartArea1" BorderColor="34, 34, 34, 34" BackSecondaryColor="Transparent" BackColor="Transparent" ShadowColor="Transparent" BorderWidth="0" AlignmentOrientation="All">
									<position height="85" width="85" x="1" y="15" />
                                    <innerplotposition height="89.5" width="54.7193565" x="8.64032" />
									<area3dstyle Rotation="0" Inclination="10" IsClustered="True" IsRightAngleAxes="False" LightStyle="Realistic" />
									<axisy LineColor="34, 34, 34, 34">
										
										<MajorGrid LineColor="34, 34, 34, 34" />
									</axisy>
									<axisx LineColor="34, 34, 34, 34">
										
										<MajorGrid LineColor="34, 34, 34, 34" />
									</axisx>
								</asp:ChartArea>
							</chartareas>
						    <Titles>
                                <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 10.25pt, style=Bold" ShadowOffset="3" Text="OutStanding" Name="Title1" ForeColor="26, 59, 105">
                                    <Position Height="19.3940639" Width="84.234375" X="0" Y="1.952381" />
                                </asp:Title>
                            </Titles>
						    <BorderSkin PageColor="Transparent" SkinStyle="Emboss" />
						</asp:CHART>
             </div>
             </div>--%>

             <div class="charttable">
                  <div class="chartsubhead">lOAN dETAILS</div>
           
                 <asp:GridView ID="grid_loan" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Width="100%" CssClass="grid-viewCSS" >
                      <Columns>
                          <asp:BoundField DataField="PARTICULARS" HeaderText="PARTICULARS" />
                          <asp:BoundField DataField="NoofAccounts" HeaderText="NO OF ACCOUNTS" >
                          <ItemStyle HorizontalAlign="Right" />
                          </asp:BoundField>
                          <asp:BoundField DataField="outstanding" HeaderText="OUTSTANDING" >
                          <ItemStyle HorizontalAlign="Right" />
                          </asp:BoundField>
                      </Columns>
                  </asp:GridView>
                 <div class="charttablehead" style="width:49%; height:130px;">    
                  <asp:CHART id="ChartLoan_Ac" runat="server" BackColor="WhiteSmoke" Height="127px" Width="190px" BorderlineDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2px" BorderColor="#1A3B69" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" RightToLeft="No" BackImageTransparentColor="Turquoise">

							<%--<Legends>
                               <asp:Legend BackImageAlignment="Left" BackImageWrapMode="TileFlipY" LegendStyle="Column" Name="Legend2"  >
                                    <position height="90" width="30" X="70"   />
                                </asp:Legend>
                            </Legends>--%>

							<series>
								<asp:Series Name="Default" ChartType="Pie" BorderColor="180, 26, 59, 105" Color="220, 65, 140, 240" Legend="Legend2" CustomProperties="PieDrawingStyle=SoftEdge"></asp:Series>
							</series>
							<chartareas>
								<asp:ChartArea Name="ChartArea1" BorderColor="34, 34, 34, 34" BackSecondaryColor="Transparent" BackColor="Transparent" ShadowColor="Transparent" BorderWidth="0" AlignmentOrientation="All">
									<position height="85" width="85" x="1" y="15" />
                                    <innerplotposition height="89.5" width="54.7193565" x="8.64032" />
									<area3dstyle Rotation="0" Inclination="10" IsClustered="True" IsRightAngleAxes="False" LightStyle="Realistic" />
									<axisy LineColor="34, 34, 34, 34">
										
										<MajorGrid LineColor="34, 34, 34, 34" />
									</axisy>
									<axisx LineColor="34, 34, 34, 34">
										
										<MajorGrid LineColor="34, 34, 34, 34" />
									</axisx>
								</asp:ChartArea>
							</chartareas>
						    <Titles>
                                <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 10.25pt, style=Bold" ShadowOffset="3" Text="No of Accounts" Name="Title1" ForeColor="26, 59, 105">
                                    <Position Height="19.3940639" Width="84.234375" X="1" Y="1.952381" />
                                </asp:Title>
                            </Titles>
						    <BorderSkin PageColor="Transparent" SkinStyle="Emboss" />
						</asp:CHART>
              </div>
             <div class="charttablehead" style="width:49%; height:130px;">
                 <asp:CHART id="ChartLoan_OutSt" runat="server" BackColor="WhiteSmoke" Height="127px" Width="190px" BorderlineDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2px" BorderColor="#1A3B69" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" RightToLeft="No" BackImageTransparentColor="Turquoise">

							<%--<Legends>
                                <asp:Legend BackImageAlignment="Left" BackImageWrapMode="TileFlipY" LegendStyle="Column" Name="Legend2"   >
                                    <position height="90" width="30"  X="70"  />
                                </asp:Legend>
                            </Legends>--%>

							<series>
								<asp:Series Name="Default" ChartType="Pie" BorderColor="180, 26, 59, 105" Color="220, 65, 140, 240" Legend="Legend2" CustomProperties="PieDrawingStyle=SoftEdge"></asp:Series>
							</series>
							<chartareas>
								<asp:ChartArea Name="ChartArea1" BorderColor="34, 34, 34, 34" BackSecondaryColor="Transparent" BackColor="Transparent" ShadowColor="Transparent" BorderWidth="0" AlignmentOrientation="All">
									<position height="85" width="85" x="1" y="15" />
                                    <innerplotposition height="89.5" width="54.7193565" x="8.64032" />
									<area3dstyle Rotation="0" Inclination="10" IsClustered="True" IsRightAngleAxes="False" LightStyle="Realistic" />
									<axisy LineColor="34, 34, 34, 34">
										
										<MajorGrid LineColor="34, 34, 34, 34" />
									</axisy>
									<axisx LineColor="34, 34, 34, 34">
										
										<MajorGrid LineColor="34, 34, 34, 34" />
									</axisx>
								</asp:ChartArea>
							</chartareas>
						    <Titles>
                               <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 10.25pt, style=Bold" ShadowOffset="3" Text="OutStanding" Name="Title1" ForeColor="26, 59, 105">
                                    <Position Height="19.3940639" Width="84.234375" X="1" Y="1.952381" />
                                </asp:Title>
                            </Titles>
						    <BorderSkin PageColor="Transparent" SkinStyle="Emboss" />
						</asp:CHART>
             </div>
            
         </div>
           
 <div style="width:100%;float:left;">
 <div class="chartsubhead">lOAN Maturity</div>
  <asp:GridView ID="GridLoan" runat="server" CssClass="grid-viewCSS" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" >
                 <Columns>
                     <asp:BoundField  DataField="Cust_Name" HeaderText="Cust Name" />
                     <asp:BoundField DataField="Acc_No" HeaderText="A/C No." />
                     <asp:BoundField DataField="LoanAmt" HeaderText="Amount" />
                     <asp:BoundField DataField="Balance" HeaderText="Balance" />
                     <asp:BoundField DataField="Cust_Mobile" HeaderText="Mobile No." />
                 </Columns>

             </asp:GridView>
 </div>
 
  <div style="width:100%;float:left;">
 <div class="chartsubhead">Employee Reminder</div>
  <asp:GridView ID="Reminder" runat="server" CssClass="grid-viewCSS" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" >
                  <Columns>
                   <asp:BoundField  DataField="Reminder" HeaderText="Reminder" /> 

                 </Columns> 

             </asp:GridView>
 </div>
 
 
 </div>
</div>
</td></tr></table>
</div>
</asp:Content>
