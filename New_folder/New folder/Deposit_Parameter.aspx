<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="Deposit_Parameter.aspx.vb" Inherits="SMARTCORE_UI.Deposit_Parameter" 
    title="Deposit Parameter" %>
     <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
<script type='text/javascript' src="Scripts/Common.js"></script>
  
  
  <script type="text/javascript">

      function validatedate(inputText) {
          var dateformat = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
          // Match the date format through regular expression  
          if (inputText.value.match(dateformat)) {
              //  document.form1.text1.focus();  
              //Test which seperator is used '/' or '-'  
              var opera1 = inputText.value.split('/');
              var opera2 = inputText.value.split('-');
              lopera1 = opera1.length;
              lopera2 = opera2.length;
              // Extract the string into month, date and year  
              if (lopera1 > 1) {
                  var pdate = inputText.value.split('/');
              }
              else if (lopera2 > 1) {
                  var pdate = inputText.value.split('-');
              }
              var dd = parseInt(pdate[0]);
              var mm = parseInt(pdate[1]);
              var yy = parseInt(pdate[2]);
              // Create list of days of a month [assume there is no leap year by default]  
              var ListofDays = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
              if (mm == 1 || mm > 2) {
                  if (dd > ListofDays[mm - 1]) {
                      alert('Invalid date format! Please enter a valid date!');

                      return false;
                  }
              }
              if (mm == 2) {
                  var lyear = false;
                  if ((!(yy % 4) && yy % 100) || !(yy % 400)) {
                      lyear = true;
                  }
                  if ((lyear == false) && (dd >= 29)) {
                      alert('Invalid date format! Please enter a valid date!');

                      return false;
                  }
                  if ((lyear == true) && (dd > 29)) {
                      alert('Invalid date format! Please enter a valid date!');

                      return false;
                  }
              }
          }
          else {
              alert("Invalid date format! Please enter a valid date!");

              document.form1.text1.focus();
              return false;
          }
      }
      
      function ValidateSave() {
          var Ctl = document.getElementById('<%=TxtDate.ClientID%>');
          if (Ctl.value == "") {
              alert('Enter Effective Date..');
              Ctl.focus();
              return false;
          }
          var Ct2 = document.getElementById('<%=TxtMinOpeningAmt.ClientID%>');
          if (Ct2.value == "") {
              alert('Enter Min Opening Amount..');
              Ct2.focus();
              return false;
          }
          var Ct3 = document.getElementById('<%=TxtMaxOpeningAmt.ClientID%>');
          if (Ct3.value == "") {
              alert('Enter Max Opening Amount..');
              Ct3.focus();
              return false;
          }
          var Ct4 = document.getElementById('<%=TxtMinTransactionAmt.ClientID%>');
          if (Ct4.value == "") {
              alert('Enter Min Transaction Amount..');
              Ct4.focus();
              return false;
          }
          var Ct5 = document.getElementById('<%=TxtMaxTransactionAmt.ClientID%>');
          if (Ct5.value == "") {
              alert('Enter Max Transaction Amount..');
              Ct5.focus();
              return false;
          } 
          
          }
          
 </script>
 
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True">
    <Scripts>
            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
    </Scripts>
</asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 90%; margin-left:5%; margin-right:5%;" > 
<legend>Deposit Parameter</legend>               
<br />
<table width="100%">
<tr>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="20%">
    <label>
    Scheme</label></td>
<td width="25%">
    <asp:DropDownList ID="CmbSchemeFrom" runat="server" AutoPostBack="True" 
        Width="100%">
    </asp:DropDownList>
    </td>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
</tr>
<tr>
<td width="5%"></td>
<td colspan="5" >
<div  style="overflow:scroll ;width:1200px" runat="server">
    <asp:GridView ID="DgvParam" runat="server" AutoGenerateColumns="True" 
        TabIndex="999" Width="100%">
        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
            BorderWidth="1px" Font-Size="Medium" ForeColor="White" />
        <AlternatingRowStyle BackColor="#F5F7FE" />
    </asp:GridView>
    </div>
    </td>
<td width="5%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="20%">
    <label>
    Date</label></td>
<td width="25%">
    <asp:TextBox ID="TxtDate" runat="server" CssClass="twitterStyleTextbox" 
        OnChange="validatedate(this);" Width="98%"></asp:TextBox>
    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
        TargetControlID="TxtDate">
    </asp:CalendarExtender>
    </td>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="20%"><label>Min Opening Amt</label></td>
<td width="25%">
    <asp:TextBox ID="TxtMinOpeningAmt" runat="server" CssClass="twitterStyleTextbox" 
        onkeypress="return isFloatKey(event);" style="text-align:right" 
        Width="98%"></asp:TextBox>
    </td>
<td width="5%"></td>
<td width="20%"><label>Max Opening Amt</label></td>
<td width="25%">
    <asp:TextBox ID="TxtMaxOpeningAmt" runat="server" CssClass="twitterStyleTextbox" 
        onkeypress="return isFloatKey(event);" style="text-align:right" 
        Width="98%"></asp:TextBox>
    </td>
<td width="5%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="20%"><label>Min Transaction Amt</label></td>
<td width="25%">
    <asp:TextBox ID="TxtMinTransactionAmt" runat="server" CssClass="twitterStyleTextbox" 
        onkeypress="return isFloatKey(event);" style="text-align:right" 
        Width="98%"></asp:TextBox>
    </td>
<td width="5%"></td>
<td width="20%"><label>Max Transaction Amt</label></td>
<td width="25%">
    <asp:TextBox ID="TxtMaxTransactionAmt" runat="server" CssClass="twitterStyleTextbox" 
        onkeypress="return isFloatKey(event);" style="text-align:right" 
        Width="98%"></asp:TextBox>
    </td>
<td width="5%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
</tr>
<div id="DivSBCA" runat="server" >
<tr>
<td width="5%"></td>
<td width="20%"><label>Min Balance(Cheque Issued)</label></td>
<td width="25%">
    <asp:TextBox ID="TxtMinBalanceChqIss" runat="server" 
        CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" 
        style="text-align:right" Width="98%"></asp:TextBox>
    </td>
<td width="5%"></td>
<td width="20%"><label>Min Balance(Non Cheque Issued)</td>
<td width="25%">
    <asp:TextBox ID="TxtTxtMinBalanceNonChqIss" runat="server" 
        CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" 
        style="text-align:right" Width="98%"></asp:TextBox>
    </td>
<td width="5%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="20%"><label>Int Posting Frequency</label></td>
<td width="25%">
    <asp:DropDownList ID="CmbIntPostingFrequency" runat="server" 
        Width="100%">
    </asp:DropDownList>
    </td>
<td width="5%"></td>
<td width="20%"><label>Cheque Book Allowed</label></td>
<td width="25%"><asp:DropDownList ID="CmbChequeBookAllowed" runat="server" 
        Width="100%">
    </asp:DropDownList></td>
<td width="5%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="20%"><label>Cheque Book Group</label></td>
<td width="25%"><asp:DropDownList ID="CmbChequeBookGroup" runat="server" 
        Width="100%"></asp:DropDownList></td>
<td width="5%"></td>
<td width="20%"><label>Free Cheque Leaf</label></td>
<td width="25%">
    <asp:TextBox ID="TxtFreeChequeLeaf" runat="server" 
        CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" 
         Width="98%"></asp:TextBox>
    </td>
<td width="5%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="20%"><label>In Operative Period</label></td>
<td width="25%">
    <asp:TextBox ID="TxtInOperativePeriod" runat="server" 
        CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" 
        Width="98%"></asp:TextBox>
    </td>
<td width="5%"></td>
<td width="20%"><label>Dormant Period</label></td>
<td width="25%">
    <asp:TextBox ID="TxtDormantPeriod" runat="server" 
        CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" 
        Width="98%"></asp:TextBox>
    </td>
<td width="5%"></td>
</tr>
<tr>
<td width="5%" ></td>
<td width="20%" ><label>TOD Allowed</label></td>
<td width="25%" >
    <asp:DropDownList ID="CmbTODAllowed" runat="server" Width="100%">
    </asp:DropDownList>
    </td>
<td width="5%" ></td>
<td width="20%" ></td>
<td width="25%" ></td>
<td width="5%" ></td>
</tr>
</div>
<tr>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
</tr>
<div id="DivFDRDDD" runat="server">
<tr>
<td width="5%"></td>
<td width="20%"><label>Pre Closure Allowed</label></td>
<td width="25%">
    <asp:DropDownList ID="CmbPreClosureAllowed" runat="server" Width="100%">
    </asp:DropDownList>
    </td>
<td width="5%"></td>
<td width="20%"><label>Renewal Allowed</label></td>
<td width="25%">
    <asp:DropDownList ID="CmbRenewalAllowed" runat="server" Width="100%">
    </asp:DropDownList>
    </td>
<td width="5%"></td>

</tr>
<tr>
<td width="5%"></td>
<td width="20%"><label>Auto Renewal</label></td>
<td width="25%">
    <asp:DropDownList ID="CmbAutoRenewal" runat="server" Width="100%">
    </asp:DropDownList>
    </td>
<td width="5%"></td>
<td width="20%"><label>Renewal with Renewal No</label></td>
<td width="25%">
    <asp:DropDownList ID="CmbRenewalwithRenewalNo" runat="server" Width="100%">
    </asp:DropDownList>
    </td>
<td width="5%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="20%"><label>Loan Allowed</label></td>
<td width="25%">
    <asp:DropDownList ID="CmbLoanAllowed" runat="server" Width="100%">
    </asp:DropDownList>
    </td>
<td width="5%"></td>
<td width="20%"><label>Holiday Interest Applicable</label></td>
<td width="25%">
    <asp:DropDownList ID="CmbHolidayInterestApplicable" runat="server" Width="100%">
    </asp:DropDownList>
    </td>
<td width="5%"></td>
</tr>
</div>
<tr>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
</tr>
<div id="DivRD" runat="server" >
<tr>
<td width="5%"></td>
<td width="20%"><label>Installment Multiples</label></td>
<td width="25%">
    <asp:TextBox ID="TxtInstallmentMultiples" runat="server" 
        CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" 
        Width="98%"></asp:TextBox>
    </td>
<td width="5%"></td>
<td width="20%"><label>Penalty For Pending Installment</label></td>
<td width="25%">
    <asp:DropDownList ID="CmbPenaltyForPendingIns" runat="server" 
        Width="100%">
    </asp:DropDownList>
    </td>
<td width="5%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="20%"><label>Penalty Grade Period</label></td>
<td width="25%">
    <asp:TextBox ID="TxtPenaltyGradePeriod" runat="server" 
        CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" 
        Width="98%"></asp:TextBox>
    </td>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
</tr>
</div>
<tr>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
</tr>
<div id="DivFDRD" runat="server">
<tr>
<td width="5%"></td>
<td width="20%"><label>Standing Instruction</label></td>
<td width="25%">
    <asp:DropDownList ID="CmbStandingInstruction" runat="server" Width="100%">
    </asp:DropDownList>
    </td>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
</tr>
</div>
<tr>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
</tr>
<div id="DivFD" runat="server">
<tr>
<td width="5%"></td>
<td width="20%"><label>External Lein</label></td>
<td width="25%">
    <asp:DropDownList ID="CmbExternalLein" runat="server" Width="100%">
    </asp:DropDownList>
    </td>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
</tr>
</div>
<tr>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
</tr>
<div id="DivDD" runat="server">
<tr>
<td width="5%"></td>
<td width="20%"><label>Agent Collection</label></td>
<td width="25%">
    <asp:DropDownList ID="CmbAgentCollection" runat="server" Width="100%">
    </asp:DropDownList>
    </td>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
</tr>
</div>
<tr>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="20%"></td>
<td colspan="3" style="width: 45%" align="center"><asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
        meta:resourcekey="CmdSaveResource1" OnClientClick="return ValidateSave();" 
        TabIndex="56" />
                        <asp:ImageButton ID="CmdClear0" runat="server" ImageUrl="~/Images/Clear.png" 
                    meta:resourcekey="CmdSaveResource1" TabIndex="36" />
                
    <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/images/Close.png" 
        meta:resourcekey="CmdClearResource1" PostBackUrl="~/Main.aspx" 
        style="margin-top: 0px" TabIndex="57" /></td>
<td width="25%"></td>
<td width="5%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
</tr>
</table>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

