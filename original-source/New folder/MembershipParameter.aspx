<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="MembershipParameter.aspx.vb" Inherits="SMARTCORE_UI.MembershipParameter" 
    title="Membership Parameter" %>
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
          var Ct2 = document.getElementById('<%=TxtSharevalue.ClientID%>');
          if (Ct2.value == "") {
              alert('Enter Share Value..');
              Ct2.focus();
              return false;
          }
          var Ct3 = document.getElementById('<%=TxtMinshare.ClientID%>');
          if (Ct3.value == "") {
              alert('Enter Minimum share value..');
              Ct3.focus();
              return false;
          }
          var Ct4 = document.getElementById('<%=TxtMaxshare.ClientID%>');
          if (Ct4.value == "") {
              alert('Enter Maximum share value..');
              Ct4.focus();
              return false;
          }
          var Ct5 = document.getElementById('<%=TxtSharePeriod.ClientID%>');
          if (Ct5.value == "") {
              alert('Enter Share Period..');
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
<legend>Memebership Parameter</legend>               
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
<td width="20%"><label>Scheme</label></td>
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
<td colspan="5">
    <asp:GridView ID="DgvParam" runat="server" AutoGenerateColumns="True" 
        TabIndex="999" Width="100%">
  
               <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                    BorderWidth="1px" Font-Size="Medium" ForeColor="White"  />
              
                
     
        
                
                                  
                                
       
        <AlternatingRowStyle BackColor="#F5F7FE" />
    </asp:GridView>
    </td>
<td width="5%"></td>
</tr>
<tr>
<td width="5%">&nbsp;</td>
<td width="20%">&nbsp;</td>
<td width="25%">
    
        &nbsp;</td>
<td width="5%">&nbsp;</td>
<td width="20%">&nbsp;</td>
<td width="25%">&nbsp;</td>
<td width="5%">&nbsp;</td>
</tr>
    <tr>
        <td width="5%">
        </td>
        <td width="20%">
            <label>
            Date</label></td>
        <td width="25%">
            <asp:TextBox ID="TxtDate" runat="server" Width="100%"  CssClass="twitterStyleTextbox" OnChange="validatedate(this);"  ></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                TargetControlID="TxtDate">
            </asp:CalendarExtender>
        </td>
        <td width="5%">
        </td>
        <td width="20%">
            <label>
            Distinctive No Required</label></td>
        <td width="25%">
            <asp:DropDownList ID="CmbDistinctive" runat="server" Width="100%">
            </asp:DropDownList>
        </td>
        <td width="5%">
        </td>
    </tr>
<tr>
<td width="5%"></td>
<td width="20%"><label>Share Value</label></td>
<td width="25%">
    <asp:TextBox ID="TxtSharevalue" runat="server" Width="100%"  CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);"  style="text-align:right" ></asp:TextBox>
    </td>
<td width="5%"></td>
<td width="20%">
    <label>
    Divident Payment</label></td>
<td width="25%">
    <asp:DropDownList ID="CmbDivPayment" runat="server" Width="100%">
    </asp:DropDownList>
    </td>
<td width="5%"></td>
</tr>

<tr>
<td width="5%"></td>
<td width="20%"><label>Min No Of Share</label></td>
<td width="25%">
    <asp:TextBox ID="TxtMinshare" runat="server" Width="100%"  CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" ></asp:TextBox>
    </td>
<td width="5%"></td>
<td width="20%">
    <label>
    Voting Rights</label></td>
<td width="25%">
    <asp:DropDownList ID="CmbVotingRights" runat="server" Width="100%">
    </asp:DropDownList>
    </td>
<td width="5%"></td>
</tr>

<tr>
<td width="5%"></td>
<td width="20%"><label>Max No Of Share</label></td>
<td width="25%">
    <asp:TextBox ID="TxtMaxshare" runat="server" Width="100%"  CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" ></asp:TextBox>
    </td>
<td width="5%"></td>
<td width="20%">
    <label>
    Loan Eligibility</label></td>
<td width="25%">
    <asp:DropDownList ID="CmbLoanEligibility" runat="server" Width="100%">
    </asp:DropDownList>
    </td>
<td width="5%"></td>
</tr>

<tr>
<td width="5%"></td>
<td width="20%">
    <label>
    MinShare Period</label></td>
<td width="25%">
    <asp:TextBox ID="TxtSharePeriod" runat="server" Width="100%"  CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" ></asp:TextBox>
    </td>
<td width="5%"></td>
<td width="20%">
    <label>
    Share Suspence Required</label> </td>
<td width="25%">
    <asp:DropDownList ID="CmbShareSus" runat="server" Width="100%">
    </asp:DropDownList>
    </td>
<td width="5%"></td>
</tr>

<tr>
<td width="5%"></td>
<td width="20%">&nbsp;</td>
<td width="25%">
    &nbsp;</td>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
</tr>

<tr>
<td width="5%"></td>
<td width="20%">&nbsp;</td>
<td width="25%">
    &nbsp;</td>
<td width="5%"></td>
<td width="20%"></td>
<td width="25%"></td>
<td width="5%"></td>
</tr>

<tr>
<td width="5%"></td>
<td width="20%">&nbsp;</td>
<td colspan="3" style="width: 45%" align="center">
    <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
        meta:resourcekey="CmdSaveResource1" OnClientClick="return ValidateSave();" 
        TabIndex="56" />
                        <asp:ImageButton ID="CmdClear0" runat="server" ImageUrl="~/Images/Clear.png" 
                    meta:resourcekey="CmdSaveResource1" TabIndex="36" />
                
    <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/images/Close.png" 
        meta:resourcekey="CmdClearResource1" PostBackUrl="~/Main.aspx" 
        style="margin-top: 0px" TabIndex="57" />
    </td>
<td width="25%"></td>
<td width="5%"></td>
</tr>

<tr>
<td width="5%"></td>
<td width="20%">&nbsp;</td>
<td width="25%">
    &nbsp;</td>
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

