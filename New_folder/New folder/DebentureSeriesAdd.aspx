<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="DebentureSeriesAdd.aspx.vb" Inherits="SMARTCORE_UI.DebentureSeriesAdd" %>
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
          var Ctl = document.getElementById('<%=TxtSeries.ClientID%>');
          if (Ctl.value == "") {
              alert('Enter Account  Code..');
              Ctl.focus();
              return false;
          }
          var Ct2 = document.getElementById('<%=TxtMaxno.ClientID%>');
          if (Ct2.value == "") {
              alert('Enter Account  Name..');
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
<div class="gradientBoxesWithOuterShadows" style="width: 50%; margin-left:25%; margin-right:25%;" > 
<legend>Series Add</legend>
<br />
    <table width="100%">
        <tr>
            <td width="5%">
            </td>
            <td width="30%">
                <label>
                Scheme</label></td>
            <td width="40%">
                <asp:DropDownList ID="CmbSch" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
            <td width="25%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="5%">
                &nbsp;</td>
            <td width="30%">
                <label>Series Code</label></td>
            <td width="40%">
                <asp:TextBox ID="TxtSeriesCode" Width="100%" runat="server" AutoPostBack="True"></asp:TextBox>
            </td>
            <td width="25%">
                 &nbsp; &nbsp;<asp:ImageButton ID="ImgSrch" runat="server" Height="20px" 
                    ImageUrl="~/images/Search.png" Width="23px" /></td>
        </tr>
        <tr>
            <td width="5%">
            </td>
            <td width="30%">
                <label>
               Series Name</label></td>
            <td width="40%">
                <asp:TextBox ID="TxtSeries" runat="server" AutoPostBack="True" 
                    CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
            </td>
            <td width="25%">    
                  
            </td>
        </tr>
        
        <tr>
            <td width="5%">
            </td>
            <td width="30%">
                <label>
                Effective Date</label></td>
            <td width="40%">
                <asp:TextBox ID="TxtEffdate" runat="server" OnChange="validatedate(this);" 
                    TabIndex="3" Width="100%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="TxtEffdate">
                </asp:CalendarExtender>
            </td>
            <td width="25%">
            </td>
        </tr>
        <tr>
            <td width="5%">
            </td>
            <td width="30%">
                <label>
                Max No</label></td>
            <td width="40%">
                <asp:TextBox ID="TxtMaxNo" runat="server" AutoPostBack="True" 
                    CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" 
                    Width="100%"></asp:TextBox>
            </td>
            <td width="25%">
            </td>
        </tr>
        <div ID="DivBranch" runat="server" visible="false">
        </div>
        <tr>
            <td width="5%">
            </td>
            <td width="30%">
            </td>
            <td width="40%">
            </td>
            <td width="25%">
            </td>
        </tr>
        <tr>
            <td width="5%">
            </td>
            <td align="center" colspan="2" style="width: 70%" width="30%">
                <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                    meta:resourcekey="CmdSaveResource1" OnClientClick="return ValidateSave();" 
                    TabIndex="56" />
                <asp:ImageButton ID="CmdClear0" runat="server" ImageUrl="~/Images/Clear.png" 
                    meta:resourcekey="CmdSaveResource1" TabIndex="36" />
                <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/images/Close.png" 
                    meta:resourcekey="CmdClearResource1" PostBackUrl="~/Main.aspx" 
                    style="margin-top: 0px" TabIndex="57" />
            </td>
            <td width="25%">
            </td>
        </tr>
        <tr>
            <td width="5%">
            </td>
            <td width="30%">
            </td>
            <td width="40%">
            </td>
            <td width="25%">
            </td>
        </tr>
    </table>
    </legend>               
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
