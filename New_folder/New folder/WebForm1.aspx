<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm1.aspx.vb" Inherits="SMARTCORE_UI.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="css/MenuCss.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <ul id="cm-nav">
   
		   <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
   
		<li style="width:80px;"><a id="UM1" href="javascript:void(0)" runat="server" class="arrow">MASTER</a>
		<ul class="arrow-pad">
	
				<li><a href="AccountMaster.aspx" runat="server" visible="True" ID="a1">Customer</a></li>		
		<li><a href="AccountMaster.aspx" runat="server" visible="True" ID="accMast">Account Opening</a></li>				
		<li><a href="GroupMaster.aspx" runat="server" visible="True" ID="grpMast">Bills</a></li>
		<li><a href="ProfitCenter.aspx" runat="server" visible="True" ID="prftCnter">Demand Draft</a></li>
		<li><a href="BranchMaster.aspx"  runat="server" visible="True" ID="BrnchMast">Cheque Issue</a></li>		
		<li><a href="javascript:void(0)" runat="server" visible="True" ID="Sett">Instrument Marking</a></li>		
		</ul>
        </li>
		
		<li style="width:120px;"><a id="UM2" href="javascript:void(0)" runat="server" class="arrow">TRANSACTIONS</a>
		<ul>
		<li><a href="javascript:void(0)" runat="server" visible="True" ID="recVou">Account Transactions</a></li>
		<li><a href="javascript:void(0)" runat="server" visible="True" ID="jouVou">HO Transaction</a></li>
		<li><a href="javascript:void(0)" runat="server" visible="True" ID="openBalEntry">General Voucher</a></li>		
		<li><a href="javascript:void(0)" runat="server" visible="True" ID="chqClr">Transaction Cancellation</a></li>		
		</ul>
		</li>
		
		
		<li style="width:100px;"><a id="UM3" href="javascript:void(0)" runat="server" class="arrow">AUTHORISATION</a>
		<ul  class="arrow-pad">
		<li><a href="AssetMaster.aspx" runat="server" visible="True" ID="AssMas" >Manager Passing</a></li>
		<li><a href="ExecutiveMaster.aspx" runat="server" visible="True" ID="FeildExe">Cashier Passing</a></li>
		</ul>
		</li>
		
		<li style="width:100px;"><a id="UM4" href="javascript:void(0);" runat="server" class="arrow">CASH</a>

		<ul>
		<li><a href="UserMenu.aspx" runat="server" visible="True" ID="useMen">Cash Receipt</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="secrtn">Coin War</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="sezDel">Cashier Settings</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="Pledg">Cash Allotment</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A56">Cashier Acceptance</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A57">Cashier Returns</a></li>		
		</ul>
		</li>
		
		
		
		<li style="width:125px;"><a id="UM5" href="javascript:void(0)" runat="server" class="arrow">DAILY ACTIVITIES</a>
		<ul  class="arrow-pad">
		<li><a href="AssetMaster.aspx" runat="server" visible="True" ID="A58" >Attendence Register</a></li>
		<li><a href="AssetMaster.aspx" runat="server" visible="True" ID="A59" >Day Opening</a></li>
		<li><a href="ExecutiveMaster.aspx" runat="server" visible="True" ID="A60">Day Closing</a></li>
		</ul>
		</li>
		
		<td></td>
		
		<li style="width:155px; text-align: center;"><a id="UM6" href="javascript:void(0);" runat="server" class="arrow">BATCH PROCESSING</a>
		<ul>
		 <li><a href='CustEnq.aspx?Mode=0' runat="server" visible="True" ID="enqry"><span>Deposit Standing Instruction</span></a></li>
         <li><a href='CustEnq.aspx?Mode=1' runat="server" visible="True" ID="enqAll"><span>Interest Application</span></a></li>
         <li><a href='CustEnq.aspx?Mode=2' runat="server" visible="True" ID="FTEnt"><span>SB Interest Application</span></a></li>
         <li><a href='CustEnq.aspx?Mode=3' runat="server" visible="True" ID="FTApp"><span>Loan OverDue and Notice Printing</span></a></li>
	         <li><a href='CustEnq.aspx?Mode=3' runat="server" visible="True" ID="A61"><span>Share Suspense Transfer</span></a></li>
		</ul>
		</li>
		
		
		
		<li style="width:100px;"><a id="UM8" href="javascript:void(0);" runat="server" class="arrow">UTILITIES</a>
		<ul>
		 <li><a href='CustEnq.aspx?Mode=0' runat="server" visible="True" ID="A63"><span>Pickup Master</span></a></li>
         <li><a href='CustEnq.aspx?Mode=1' runat="server" visible="True" ID="A64"><span>Print Voucher</span></a></li>
         <li><a href='CustEnq.aspx?Mode=2' runat="server" visible="True" ID="A65"><span>Location Maintenance</span></a></li>
         <li><a href='CustEnq.aspx?Mode=3' runat="server" visible="True" ID="A66"><span>Postoffice Master</span></a></li>
	       <li><a href='CustEnq.aspx?Mode=3' runat="server" visible="True" ID="A67"><span>Customer Search</span></a></li>
	      <li><a href='CustEnq.aspx?Mode=3' runat="server" visible="True" ID="A62"><span>Image Scanner</span></a></li>
	     <li><a href='CustEnq.aspx?Mode=3' runat="server" visible="True" ID="A68"><span>Mis Rpt Settings</span></a></li>
	     <li><a href='CustEnq.aspx?Mode=3' runat="server" visible="True" ID="A69"><span>Change Password</span></a></li>
	     <li><a href='CustEnq.aspx?Mode=3' runat="server" visible="True" ID="A70"><span>Change Nominee</span></a></li>
	     <li><a href='CustEnq.aspx?Mode=3' runat="server" visible="True" ID="A71"><span>Cheque Finder</span></a></li>
	     <li><a href='CustEnq.aspx?Mode=3' runat="server" visible="True" ID="A72"><span>Bank Master</span></a></li>
	     <li><a href='CustEnq.aspx?Mode=3' runat="server" visible="True" ID="A73"><span>Transaction Query</span></a></li>
	     <li><a href='CustEnq.aspx?Mode=3' runat="server" visible="True" ID="A74"><span>Transaction Log</span></a></li>
	     <li><a href='CustEnq.aspx?Mode=3' runat="server" visible="True" ID="A75"><span>Account Freezing</span></a></li>
	     <li><a href='CustEnq.aspx?Mode=3' runat="server" visible="True" ID="A76"><span>Commission Master</span></a></li>
	     <li><a href='CustEnq.aspx?Mode=3' runat="server" visible="True" ID="A77"><span>Stop Interest Calculation</span></a></li>
	     <li><a href='CustEnq.aspx?Mode=3' runat="server" visible="True" ID="A78"><span>Token Master</span></a></li>
	     <li><a href='CustEnq.aspx?Mode=3' runat="server" visible="True" ID="A79"><span>Interest Slab Calculater</span></a></li>
	     <li><a href='CustEnq.aspx?Mode=3' runat="server" visible="True" ID="A80"><span>Transfer Batch</span></a></li>
	     <li><a href='CustEnq.aspx?Mode=3' runat="server" visible="True" ID="A81"><span></span>BackUp</a></li>
	     <li><a href='CustEnq.aspx?Mode=3' runat="server" visible="True" ID="A82"><span>Mortgage Details</span></a></li>
	     <li><a href='CustEnq.aspx?Mode=3' runat="server" visible="True" ID="A83"><span>GetCustomerId</span></a></li>
	     <li><a href='CustEnq.aspx?Mode=3' runat="server" visible="True" ID="A84"><span>Customer Liability Entry Details</span></a></li>
	     <li><a href='CustEnq.aspx?Mode=3' runat="server" visible="True" ID="A85"><span>Loan Interest UptoDate</span></a></li>
	     <li><a href='CustEnq.aspx?Mode=3' runat="server" visible="True" ID="A86"><span>Reminder</span></a></li>
		</ul>
		</li>
				
		
		
		<li style="width:100px;"><a id="UM7" href="javascript:void(0)" runat="server" class="arrow">REPORTS</a>
	    <ul>
	    <li><a class="arrow" href="javascript:void(0);" runat="server" visible="True" ID="AssMasLis">Customer </a>
		<ul>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A16">Customer Reports</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="a17">Asset/Liability</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A18">Category Wise</a></li>
    	<li><a href="javascript:void(0);" runat="server" visible="True" ID="A19">Minor/Major Customer</a></li>		
		</ul>
	    <li><a class="arrow" href="javascript:void(0);" runat="server" visible="True" ID="a7">Daily Reports </a>
			<ul>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A2">Subsidiary</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="a3">IB Subsidiary</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A4">Cash Chitta</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A5">Transfer List</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A6">Cash Scroll</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A8">Manager Scroll</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A9">Day Book</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A10">Liquidity</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A11">R & D</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A12">Trail Balance</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A13">UserWise TranList</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A14">General Ledger</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A15">Day Summary</a></li>
		</ul>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="det">Day End Report</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="Remlat">Outstanding</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="NotPri">Schedule</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="DemSta">Accurde Interest Rate</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="DemCol">Average Interest Rate</a></li>
		<li><a class="arrow" href="javascript:void(0);" runat="server" visible="True" ID="InsExp">Clearing</a>
		<ul>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A20">Register</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="OveSta">Data Check</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="AgeOut">Clearing Advice</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="DatOut">Return Memo</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="FolUp">Bills Payable</a></li>
		</ul>
		<li><a class="arrow" href="javascript:void(0);" runat="server" visible="True" ID="ParLed">HO Reports</a>
		<ul>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="StoOnHirSta">HO Daybook</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="IntAccBut">HO Advice Report</a></li>
		</ul>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="DifSta">Inter Branch Collection Register</a></li>
		<li><a class="arrow" href="javascript:void(0);" runat="server" visible="True" ID="CheToBeCol">Reconcillation Report</a>
		<ul>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="CheRetLis">Inter Branch Report</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="HPSta">Inter Bank Report</a></li>
		</ul>
		<li><a class="arrow" href="javascript:void(0);" runat="server" visible="True" ID="FCSta">Bank Account</a>
		<ul>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A21">Demand Draft Issue</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A22">Transaction List</a></li>
		</ul>
		<li><a class="arrow" href="javascript:void(0);" runat="server" visible="True" ID="FCRecRecSta">Miscellaneous Report</a>
		<ul>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="ChaRec">Customer Report</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="FCBreMon">Data Entry Report</a></li>
		</ul>
		<li><a class="arrow" href="javascript:void(0);" runat="server" visible="True" ID="SecAgeSto">Share</a>
		<ul>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A23">Personal Ledger</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A24">Opening/Closing</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A25">Active/Inactive List</a></li>
		<li><a class="arrow" href="javascript:void(0);" runat="server" visible="True" ID="A26">Suspense Posting Details </a>
		</ul>
		<li><a class="arrow" href="javascript:void(0);" runat="server" visible="True" ID="PosDatCheLis">Loan</a>
		<ul>
		
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A27">Personal Ledger</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A28">Opening/Closing Register</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A29">Gold Custody</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A30">Gold A/C Wise Ornament List</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A31">DCB</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A32">NPA Report</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A33">NPA Movement Register</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A34">Issue Recovery Statement</a></li>	
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="ParTriBal">Loan List</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="OutSta">Security Details</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="SecWisOut">Loan With No Receipts </a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="SecOut">Loan OverDue List</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="PleOut">Loan Enhancement Report</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="PenIntSta">Interest Subvention</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="PenIntRec">AmountWise Details</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="SerTaxSta">Loanable Fund</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="OutAna">Priority List</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="Chasta">Action Taken Details</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="LivCas">Minority Report</a></li>
		</ul>
		</li>
		<li><a class="arrow" href="javascript:void(0);" runat="server" visible="True" ID="accHedLis">Deposit</a>
		<ul>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="dayBoo">Personal Ledger</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="Ledgr">Opening/Closure Report</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="tribal">Standing Instruction</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="proLosAcc">DueDate Dairy</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="balShe">Matured List</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="schdl">Agent Collection List</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="accRec">Minor Turning Major Deposits </a><li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="outsatandng">Minor/Senior Citizens</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="ageAna">AmountWise Transaction Details</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="ovrsta">Interest Ratewise Deposit</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="unaRec">Matured List</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A35">Cheque issue Details</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A36">DD/PO Stop Payment</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="a37">Undesirable Accounts</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="a38">Defaulters List</a></li>
        <li><a href="javascript:void(0);" runat="server" visible="True" ID="a39">Transaction Scrollt</a></li>
        <li><a href="javascript:void(0);" runat="server" visible="True" ID="a40">Member/NonMember List</a></li>
        <li><a href="javascript:void(0);" runat="server" visible="True" ID="a41">Inop.List</a></li>
        <li><a href="javascript:void(0);" runat="server" visible="True" ID="a42">External Liened/Revoked List</a></li>
		</ul>
		</li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="casBoo">Statements</a></li>
		<li><a class="arrow" href="javascript:void(0);" runat="server" visible="True" ID="banBoo">Suspense</a>
		<ul>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="a43">Pesonal Ledger</a></li>
		</ul>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="accExt">Investment Ledger</a>
		<ul>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A44">Personal Ledger</a></li>
		</ul>
		<li><a class="arrow" href="javascript:void(0);" runat="server" visible="True" id="traLis">Locker</a>
		<ul>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="recVou1">Personal Ledger</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="PayVou1"> Rent Collection/Visit Register</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="jouVou1">Opening/Closure Register</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="salVou">DueDate Dairy</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="salRet">Locker Status Details</a></li>
		</ul>
		</li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="accPay">Transaction List</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="banRec">Bills</a></li>
		<li><a class="arrow" href="javascript:void(0);" runat="server" visible="True" ID="intCal">Account Reports</a>
		<ul>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="recsta">Account Heads</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="cloSta">P & L/Balance Sheet</a></li>
		</ul>
		<li><a class="arrow" href="javascript:void(0);" runat="server" visible="True" ID="A45">Cashier Module</a>
		<ul>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A46">Coin War Register</a></li>
		</ul>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A47">Agent Commission Posting</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A48">Modification Details</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A49">Changed Nominee Details</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A50">Cancelled Entry List</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A51">Interest Posted Details</a></li>
		<li><a class="arrow" href="javascript:void(0);" runat="server" visible="True" ID="A52">Govt Security</a>
		<ul>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A53">Ledger</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A54">DueDate Dairy</a></li>
		<li><a href="javascript:void(0);" runat="server" visible="True" ID="A55">SLR/Non SLR Report</a></li>
		</ul>
		</li>
		
	
		
		<li style="width:100px;"><a  href="javascript:void(0);" class="arrow">WINDOW</a>
		<ul style="width:100px;">
		<li><asp:LinkButton ID="lnkNotepad" runat="server">Notepad</asp:LinkButton></li>
		<li><asp:LinkButton ID="lnkCalculator" runat="server">Calculator</asp:LinkButton></li>
		<li><a href="javascript:void(0);">Calendar</a></li>		
		<li><a href="Login.aspx">Logout</a></li>	
		</ul>
		</li>
	</ul>

    </div>

    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Button ID="Button1" runat="server" ForeColor="#D9EFFF" Text="Button" />
    </p>

    </form>
</body>
</html>
