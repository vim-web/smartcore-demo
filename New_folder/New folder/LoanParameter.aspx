<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="LoanParameter.aspx.vb" Inherits="SMARTCORE_UI.LoanParameter" 
    title="LoanParameter" %> <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
          var Ct10 = document.getElementById('<%=TxtMinLoanAmount.ClientID%>');
          if (Ct10.value == "") {
              alert('Enter Min Loan Amount..');
              Ctl0.focus();
              return false;
          }
          var Ct12 = document.getElementById('<%=TxtMaxLoanAmt.ClientID%>');
          if (Ct12.value == "") {
              alert('Enter Max Loan Amount..');
              Ctl2.focus();
              return false;
          }
          var Ct13 = document.getElementById('<%=TxtMinLoanPeriod.ClientID%>');
          if (Ct13.value == "") {
              alert('Enter Min Loan Period..');
              Ctl3.focus();
              return false;
          }
          var Ct14 = document.getElementById('<%=TxtMaxLoanPeriod.ClientID%>');
          if (Ct14.value == "") {
              alert('Enter Max Loan Period..');
              Ctl4.focus();
              return false;
          }
          
          var Ct6 = document.getElementById('<%=TxtMinAge.ClientID%>');
          if (Ct6.value == "") {
              alert('Enter Min Age..');
              Ct6.focus();
              return false;
          }
          
          var Ct5 = document.getElementById('<%=TxtMaxAge.ClientID%>');
          if (Ct5.value == "") {
              alert('Enter Max Age..');
              Ct5.focus();
              return false;
          }
          var Ct9 = document.getElementById('<%=TxtMinNetIncome.ClientID%>');
          if (Ct9.value == "") {
              alert('Enter Min Net Income..');
              Ct9.focus();
              return false;
          }


          var Ct4 = document.getElementById('<%=TxtMarginPer.ClientID%>');
          if (Ct4.value == "") {
              alert('Enter Margin Percentage..');
              Ct4.focus();
              return false;
          }


          var Ct7 = document.getElementById('<%=TxtSharePerc.ClientID%>');
          if (Ct7.value == "") {
              alert('Enter Share Percentage..');
              Ct7.focus();
              return false;
          }

          var Ct11 = document.getElementById('<%=TxtMinIntDays.ClientID%>');
          if (Ct11.value == "") {
              alert('Enter Min Int Days..');
              Ctl1.focus();
              return false;
          }

          var Ct8 = document.getElementById('<%=TxtMinNoOfShare.ClientID%>');
          if (Ct8.value == "") {
              alert('Enter Min no of Share..');
              Ct8.focus();
              return false;
          }
          
          var Ct3 = document.getElementById('<%=TxtDepLoanMaxElgibility.ClientID%>');
          if (Ct3.value == "") {
              alert('Enter Deposit Loan Maximum Eligibility..');
              Ct3.focus();
              return false;
          }
          
          var Ct2 = document.getElementById('<%=TxtDepLoanExtraIntRate.ClientID%>');
          if (Ct2.value == "") {
              alert('Enter DEposit Loan Extra Int Rate..');
              Ct2.focus();
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
<legend>Loan Parameter</legend>               
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
<div id="Div1"  style="overflow:scroll ;width:1200px" runat="server">
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
<td width="20%"><label>Date</label></td>
<td width="25%">    <asp:TextBox ID="TxtDate" runat="server" CssClass="twitterStyleTextbox" 
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
<td width="20%"><label>Minimum Loan Amount</label></td>
<td width="25%">    <asp:TextBox ID="TxtMinLoanAmount" runat="server" CssClass="twitterStyleTextbox" 
        onkeypress="return isFloatKey(event);" style="text-align:right" 
        Width="98%"></asp:TextBox>
    </td>
<td width="5%"></td>
<td width="20%"><label>Maximum Loan Amount</label></td>
<td width="25%">    <asp:TextBox ID="TxtMaxLoanAmt" runat="server" CssClass="twitterStyleTextbox" 
        onkeypress="return isFloatKey(event);" style="text-align:right" 
        Width="98%"></asp:TextBox>
    </td>
<td width="5%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="20%"><label>Minimum Loan Period</label></td>
<td width="25%">    <asp:TextBox ID="TxtMinLoanPeriod" runat="server" CssClass="twitterStyleTextbox" 
        onkeypress="return isFloatKey(event);"  
        Width="98%"></asp:TextBox>
    </td>
<td width="5%"></td>
<td width="20%"><label>Maximum Loan Period</label></td>
<td width="25%">    <asp:TextBox ID="TxtMaxLoanPeriod" runat="server" CssClass="twitterStyleTextbox" 
        onkeypress="return isFloatKey(event);" 
        Width="98%"></asp:TextBox>
    </td>
<td width="5%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="20%"><label>Minimum Age</label></td>
<td width="25%">    <asp:TextBox ID="TxtMinAge" runat="server" CssClass="twitterStyleTextbox" 
        onkeypress="return isFloatKey(event);" 
        Width="98%"></asp:TextBox>
    </td>
<td width="5%"></td>
<td width="20%"><label>Maximum Age</label></td>
<td width="25%">    <asp:TextBox ID="TxtMaxAge" runat="server" CssClass="twitterStyleTextbox" 
        onkeypress="return isFloatKey(event);" 
        Width="98%"></asp:TextBox>
    </td>
<td width="5%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="20%"><label>Minimum Net Income</label></td>
<td width="25%"> <asp:TextBox ID="TxtMinNetIncome" runat="server" CssClass="twitterStyleTextbox" 
        onkeypress="return isFloatKey(event);" style="text-align:right" 
        Width="98%"></asp:TextBox></td>
<td width="5%"></td>
<td width="20%"><label>Margin Percentage</label></td>
<td width="25%"> <asp:TextBox ID="TxtMarginPer" runat="server" CssClass="twitterStyleTextbox" 
        onkeypress="return isFloatKey(event);" style="text-align:right" 
        Width="98%"></asp:TextBox></td>
<td width="5%"></td>
</tr>

<tr>
<td width="5%"></td>
<td width="20%"><label> Morotorium Required</label></td>
<td width="25%">    <asp:DropDownList ID="CmbMortoriumRequired" runat="server" Width="100%">
    </asp:DropDownList>
    </td>
<td width="5%"></td>
<td width="20%"><label>Disbursal Type</label></td>
<td width="25%"><asp:DropDownList ID="CmbDispursalType" runat="server" Width="100%">
    </asp:DropDownList></td>
<td width="5%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="20%"><label>Share Required</label></td>
<td width="25%">    <asp:DropDownList ID="CmbShareRequired" runat="server" Width="100%">
    </asp:DropDownList>
    </td>
<td width="5%"></td>
<td width="20%"><label>Share Percentage</label></td>
<td width="25%"><asp:TextBox ID="TxtSharePerc" 
        runat="server" CssClass="twitterStyleTextbox" 
        onkeypress="return isFloatKey(event);" 
        Width="98%"></asp:TextBox></td>
<td width="5%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="20%"><label>Standing Instruction</label></td>
<td width="25%">    <asp:DropDownList ID="CmbStandingInstruction" runat="server" Width="100%">
    </asp:DropDownList>
    </td>
<td width="5%"></td>
<td width="20%"><label>Minimum Int Days</label></td>
<td width="25%"><asp:TextBox ID="TxtMinIntDays" 
        runat="server" CssClass="twitterStyleTextbox" 
        onkeypress="return isFloatKey(event);" 
        Width="98%"></asp:TextBox></td>
<td width="5%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="20%"><label>Minimum Number Of Share</label></td>
<td width="25%"><asp:TextBox ID="TxtMinNoOfShare" 
        runat="server" CssClass="twitterStyleTextbox" 
        onkeypress="return isFloatKey(event);" 
        Width="98%"></asp:TextBox></td>
<td width="5%"></td>
<td width="20%"><label>Interest Calculation</label></td>
<td width="25%">    <asp:DropDownList ID="CmbIntCal" runat="server" Width="100%">
    </asp:DropDownList>
    </td>
<td width="5%"></td>
</tr>

<tr>
<td width="5%"></td>
<td width="20%"><label>Deposit Loan Maximum Eligibility</label></td>
<td width="25%"><asp:TextBox ID="TxtDepLoanMaxElgibility" 
        runat="server" CssClass="twitterStyleTextbox" 
        onkeypress="return isFloatKey(event);" style="text-align:right" 
        Width="98%"></asp:TextBox></td>
<td width="5%"></td>
<td width="20%"><label>Deposit Loan Extra Int Rate</label></td>
<td width="25%"><asp:TextBox ID="TxtDepLoanExtraIntRate" 
        runat="server" CssClass="twitterStyleTextbox" 
        onkeypress="return isFloatKey(event);" style="text-align:right" 
        Width="98%"></asp:TextBox></td>
<td width="5%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="20%"><label>Agent Collection</label></td>
<td width="25%">    <asp:DropDownList ID="CmbAgentCollection" runat="server" Width="100%">
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
</asp:UpdatePanel></asp:Content>

