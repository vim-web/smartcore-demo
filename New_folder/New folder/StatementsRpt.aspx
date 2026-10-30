<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="StatementsRpt.aspx.vb" Inherits="SMARTCORE_UI.StatementsRpt"  MasterPageFile="~/SMARTCORE.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    t<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /><script language="javascript" type="text/javascript">

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
                        alert('Invalid date format! Please enter a valid date!');
                        inputText.focus();
                        return false;
                    }
                    if ((lyear == true) && (dd > 29)) {
                        alert('Invalid date format! Please enter a valid date!');
                        inputText.focus();
                        return false;
                    }
                }
            }
            else {
                alert("Invalid date format! Please enter a valid date!");
                inputText.focus();
                document.form1.text1.focus();
                return false;
            }
        }  

</script><div class="gradientBoxesWithOuterShadows" height: 640px;width: 100%; margin-left:25%; margin-right:25%;>    
    <table class="style1">
        <tr>
            <td width="10%"></td>
            <td width="15%"></td>
            <td width="25%"></td>
            <td width="15%"></td>
            <td width="15%"></td>
            <td width="20%"></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td>
                <asp:RadioButton ID="OptBranchWise" runat="server" Text="BranchWise"  
                    Width="98%" Checked="True" GroupName="Main"/>
            </td>
            <td></td>
            <td>
                <asp:RadioButton ID="OptConsolidation" runat="server" Text="Consolidation"  
                    Width="98%" GroupName="Main"/>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Branch"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DdlBranch" runat="server" Width="98%">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="From Date"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TxtFrDate" runat="server" Width="98%" OnChange="validatedate(this);"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TxtFrDate"> </asp:CalendarExtender>      
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Report"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DdlRpt" runat="server" Width="98%"></asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="To Date" Width="98%"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TxtToDate" runat="server" Width="98%" OnChange="validatedate(this);"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TxtToDate"> </asp:CalendarExtender>      
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td>
                <asp:Button ID="BtnProcess" runat="server" Text="Process" />
            </td>
            <td>
                <asp:Button ID="BtnShowRpt" runat="server" Text="Show Report" />
            </td>
            <td>
                <asp:Label ID="Lbl_Process" runat="server" Text="Status"></asp:Label>
            </td>
            <td></td>
        </tr>
    </table>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
            EnablePageMethods="true">
        </asp:ToolkitScriptManager>

        
        
        
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    <LINK REL=Stylesheet HREF= "CSS/MasterStyle.css" TYPE="text/css" MEDIA=screen>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
