<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="Changenominee_rpt.aspx.vb" Inherits="SMARTCORE_UI.Changenominee_rpt" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
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
                        inputText.focus();
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
                        inputText.focus();
                        return false;
                    }
                    if ((lyear == true) && (dd > 29)) {
                        alertify.error('Invalid date format! Please enter a valid date!');
                        inputText.focus();
                        return false;
                    }
                }
            }
            else {
                alertify.error("Invalid date format! Please enter a valid date!");
                inputText.focus();
                document.form1.text1.focus();
                return false;
            }
        }

</script>

    <style type="text/css">
        .auto-style1
        {
            height: 23px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePageMethods="True" CombineScripts="True">
   
</asp:ToolkitScriptManager>


<div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%; " >                
    <legend>Change Nominee</legend> 
    <table width="100%">
        <tr>
            <td width="50%"><label>Branch</label></td>
        
            <td width="50%">
                <asp:DropDownList ID="CmbBranch" runat="server" Width="50%">
                </asp:DropDownList>
            </td>
                

        </tr>
        <tr>
            <td width="50%">
                <label>
                From Date</label></td>
            <td width="50%">
                <asp:TextBox ID="TxtFromDate" runat="server" OnChange="validatedate(this);" Width="50%"></asp:TextBox>
                <asp:CalendarExtender ID="TxtFromDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="TxtFromDate">
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr> <td width="50%"><label>To Date</label></td>
            <td width="50%">
                <asp:TextBox ID="TxtToDate" runat="server" Width="50%" OnChange="validatedate(this);"></asp:TextBox>
           
                  <asp:CalendarExtender ID="TxtToDate_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                    TargetControlID="TxtToDate">
                </asp:CalendarExtender>
           
                 </td>

        </tr>
        <tr>
            <td width="50%" class="auto-style1"></td>
            <td width="50%" class="auto-style1"></td>
        </tr>
        <tr>
            <td width="50%" align="right">
                <asp:ImageButton ID="CmdShow" runat="server" ImageUrl="~/Images/Show.png" style="margin-bottom: 0px" />
            </td>
            <td width="50%">
                <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/CloseBtn.png" />
            </td>
        </tr>
    </table>
    
    
                  
    </ContentTemplate>
    </asp:UpdatePanel>
   
</asp:Content>
