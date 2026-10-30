<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="LoanEMIRpt.aspx.vb" Inherits="SMARTCORE_UI.LoanEMIRpt" 
    title="Loan EMI Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

</script>

    <style type="text/css">





select 
{
	border: 1px solid #c4c4c4;
    font-size: 13px;
    padding: 4px 4px 4px 4px;
    border-radius: 4px;
    -moz-border-radius: 4px;
    -webkit-border-radius: 4px;
    box-shadow: 0px 0px 8px #d9d9d9;
    -moz-box-shadow: 0px 0px 8px #d9d9d9;
    -webkit-box-shadow: 0px 0px 8px #d9d9d9;
}
    
label {
	color: #03F;

}
    </style>
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
           <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                        <Scripts>
                            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
                        </Scripts>
        </asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 70%; margin-left:15%; margin-right:15%;">
        <legend>Loan EMI Report</legend>
        <table width = "100%">
    <tr>
        <td width = "10%"> </td>
        <td width = "40%">  
                <label> Branch </label>
        </td>
        <td width = "40%">  
                <asp:DropDownList ID="DdlBranch" runat="server" Width="98%">
                </asp:DropDownList>
        </td>
        <td width = "10%"> </td>
    </tr>

    <tr>
        <td> </td>
        <td>  
                <label> Scheme </label>
        </td>
        <td>  
                <asp:DropDownList ID="DdlScheme" runat="server" Width="98%">
                </asp:DropDownList>
        </td>
        <td> </td>
    </tr>
    <tr>
        <td> </td>
        <td> 
                      <label __designer:mapid="93">Account No</label></td>
        <td> 
                      <asp:TextBox ID="TxtAccNo" runat="server" Width="100%" AutoPostBack="True" 
                          TabIndex="3"></asp:TextBox> </td>
        <td> </td>
    </tr>
        <tr>
        <td> </td>
        <td> 
              <label __designer:mapid="7f">
              Customer Name</label></td>
        <td> 
              <asp:TextBox ID="TxtCustName" runat="server" Width="100%" TabIndex="500"></asp:TextBox>
            </td>
        <td> </td>
    </tr>
    
        <tr>
        <td> &nbsp;</td>
        <td valign="top">  
              <label __designer:mapid="7f">
              Address</label></td>
        <td> 
                      <label __designer:mapid="8d">
                      <asp:TextBox ID="TxtAddress" runat="server" Height="60px" 
                          TextMode="MultiLine" Width="100%" TabIndex="500"></asp:TextBox></label>
                  </td>
        <td> &nbsp;</td>
    </tr>
    
        <tr>
        <td> &nbsp;</td>
        <td>  
              <label __designer:mapid="7f">
              Ason Date</label></td>
        <td> 
                      <asp:TextBox ID="TxtDate" runat="server" Width="100%" OnChange="validatedate(this);"></asp:TextBox>
                      <asp:CalendarExtender ID="TxtDate_CalendarExtender" runat="server" 
                          Format="dd/MM/yyyy" TargetControlID="TxtDate">
                      </asp:CalendarExtender>
                  </td>
        <td> &nbsp;</td>
    </tr>
    
        <tr>
        <td> &nbsp;</td>
        <td colspan="2" align="center">  
                  <asp:ImageButton ID="CmdShow" runat="server" Height="25px" 
                      ImageUrl="~/Images/Show.png" />
                  <asp:ImageButton ID="CmdClose" runat="server" 
                      ImageUrl="~/Images/CloseBtn.png" />
              </td>
        <td> &nbsp;</td>
    </tr>
    
        <tr>
        <td> &nbsp;</td>
        <td>  
              &nbsp;</td>
        <td> 
                      <asp:HiddenField ID="HdnAccid" runat="server" />
            </td>
        <td> &nbsp;</td>
    </tr>
    
</table>
</div>

</asp:Content>
