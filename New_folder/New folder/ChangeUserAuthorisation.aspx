<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="ChangeUserAuthorisation.aspx.vb" Inherits="SMARTCORE_UI.ChangeUserAuthorisation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<script type='text/javascript'>

    function ValidateSave() {
        var Ctl = document.getElementById('<%=TxtReason.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Reason Not Entered, Please Check');
            Ctl.focus();
            return false;
        }

        return true;
    }

</script>
<script language="javascript" type="text/javascript">

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
                    alertify.error('Invalid date format! Please enter a valid date!');
                    inputText.value = "";
                    return false;
                }
            }
            if (mm == 2) {
                var lyear = false;
                if ((!(yy % 4) && yy % 100) || !(yy % 400)) {
                    lyear = true;
                }
                if ((lyear == false) && (dd >= 29)) {
                    alertify.error('Invalid date format! Please enter a valid date!');
                    inputText.value = "";
                    return false;
                }
                if ((lyear == true) && (dd > 29)) {
                    alertify.error('Invalid date format! Please enter a valid date!');
                    inputText.value = "";
                    return false;
                }
            }
        }
        else {
            alertify.error("Invalid date format! Please enter a valid date!");
            inputText.value = "";
            document.form1.text1.focus();
            return false;
        }
    }  

</script>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    

        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 44%; margin-left:28%; margin-right:28%;">
      <legend>  CHANGE  USER  AUTHORISATION  </legend>
      <table width="100%">
          <tr>
              <td width="2%">
                    </td>
              <td width="48%">
                   </td>
              <td width="48%">
                   </td>
              <td width="2%">
                    </td>
          </tr>
             <tr>
                 <td>
                      </td>
                 <td>
                      </td>
                 <td>
                      </td>
                 <td>
                      </td>
             </tr>
          <tr>
              <td>
                   </td>
              <td>
                  <label>
                  USER NAME</label></td>
              <td>
                   <asp:TextBox ID="TxtUserName" runat="server" AutoPostBack="True" 
                       Enabled="False" Width="97%"></asp:TextBox>
              </td>
              <td>
                   </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td>
                  <label>TRANSFER TO USER NAME</label></td>
              <td>
                  <asp:DropDownList ID="CmbNewUserName" runat="server" Width="100%" 
                      AutoPostBack="True" >
                  </asp:DropDownList>
              </td>
              <td>
                   </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td>
                  <label>
                  FROM DATE</label></td>
              <td>
                  <asp:TextBox ID="TxtFromDate" runat="server" Width="97%" 
                      OnChange="validatedate(this);"></asp:TextBox>
                  <asp:CalendarExtender ID="TxtFromDate_CalendarExtender" runat="server" format="dd/MM/yyyy"
                      TargetControlID="TxtFromDate">
                  </asp:CalendarExtender>
              </td>
              <td>
                   </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td>
                  <label>
                  TO DATE</label></td>
              <td>
                  <asp:TextBox ID="TxtToDate" runat="server" OnChange="validatedate(this);" 
                      Width="97%"></asp:TextBox>
                  <asp:CalendarExtender ID="TxtToDate_CalendarExtender" runat="server" format="dd/MM/yyyy" 
                      TargetControlID="TxtToDate">
                  </asp:CalendarExtender>
              </td>
              <td>
                   </td>
          </tr>
             <tr>
                 <td>
                      </td>
                 <td>
                     <label>
                     REASON</label></td>
                 <td>
                     <asp:TextBox ID="TxtReason" runat="server" TextMode="MultiLine" Width="97%"></asp:TextBox>
                 </td>
                 <td>
                      </td>
             </tr>
   <%--       <tr>
              <td>
                   </td>
              <td>
                  <label>Reason</label></td>
              <td>
                  <asp:TextBox ID="TxtReason" runat="server" Width="100%"></asp:TextBox>
              </td>
              <td>
                   </td>
          </tr>--%>
          <tr>
              <td>
                   </td>
              <td align="center" colspan="2">
                  <asp:HiddenField ID="HdnUserId" runat="server" />
                  <asp:HiddenField ID="HdnNewUserId" runat="server" />
              </td>
              <td>
                   </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td align="center" colspan="2">
                  <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" 
                      OnClientClick="return ValidateSave();" onclick="CmdSave_Click1" />
                   <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" />
                   <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />
              </td>
              <td>
                   </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td align="center" colspan="2">
                   </td>
              <td>
                   </td>
          </tr>
      </table>

      </div>
    </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    
    </asp:Content>
         