<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DeactivateCustomer.aspx.vb" Inherits="SMARTCORE_UI.DeactivateCustomer" MasterPageFile="~/SMARTCORE.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    

<script type='text/javascript'>
    function ValidateSave() {
        var Ctl = document.getElementById('<%=HdnAlert.ClientID%>');      
        if (Ctl.value != "") {
            alert(Ctl.value);
            return false;
        }

        var Ctl = document.getElementById('<%=TxtDeactivateDate.ClientID%>');
        if (Ctl.value == "") {
            alert('Deactivate Date Not Entered, Please Check');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtDeactivatereason.ClientID%>');
        if (Ctl.value == "") {
            alert('Deactivate Reason Not Entered, Please Check');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtInformationDate.ClientID%>');
        if (Ctl.value == "") {
            alert('Information Date Not Entered, Please Check');
            Ctl.focus();
            return false;
        }
        
        return true;
    }

    function DeactiveType_Validate() {
        var Ctl = document.getElementById('<%=CmbDeactiveType.ClientID%>');
        var DeactiveType = Ctl.options[Ctl.selectedIndex].text.toUpperCase();
        var Ctl = document.getElementById('<%=TxtOther.ClientID%>');

        if (DeactiveType == "OTHERS") {
            Ctl.disabled = false;
        }
        else {
            Ctl.disabled = true;
        }
    }

    function Branch_Validate() {
        var CmbBr = document.getElementById('<%=CmbBranch.ClientID%>');
        document.getElementById('<%=TxtBranch.ClientID%>').value = CmbBr.options[CmbBr.selectedIndex].value;
    }

</script>
        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:25%; margin-right:25%;">
        <table style="width: 100%">
            <tr>
                <td width="2%">
                    &nbsp;</td>
                <td width="48%">
                    <label>Branch</label></td>
                <td width="48%">
                    <asp:TextBox ID="TxtBranch" runat="server" Width="25%" AutoPostBack="True"></asp:TextBox>
                    &nbsp;<asp:DropDownList ID="CmbBranch" runat="server" Width="70%">
                    </asp:DropDownList>
                </td>
                <td width="2%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="2%">
                </td>
                <td width="48%">
                    <label>
                    Customer ID</label></td>
                <td width="48%">
                    <asp:TextBox ID="TxtCid" runat="server" Width="100%" AutoPostBack="True"></asp:TextBox>
                </td>
                <td width="2%">
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <label>
                    Name</label>
                </td>
                <td>
                    <asp:TextBox ID="TxtName" runat="server" Width="100%"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <label>
                    Address</label>
                </td>
                <td>
                    <asp:TextBox ID="TxtAddress" runat="server" Width="100%" TextMode="MultiLine" Height="60Px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <label>
                    Deactive Type</label>
                </td>
                <td>
                    <asp:DropDownList ID="CmbDeactiveType" runat="server" Width="100%" >
                        <asp:ListItem>Decease Customer</asp:ListItem>
                        <asp:ListItem>Fraudulent Customer</asp:ListItem>
                        <asp:ListItem>Fraudulent Customer</asp:ListItem>
                        <asp:ListItem>Insolvent Customer</asp:ListItem>
                        <asp:ListItem>Others</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <label>
                    Deactive Other Reason</label>
                </td>
                <td>
                    <asp:TextBox ID="TxtOther" runat="server" Width="100%" AutoPostBack="True" Enabled=false></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <label>Deactivated Date</label></td>
                <td>
                    <asp:TextBox ID="TxtDeactivateDate" runat="server" Width="100%"></asp:TextBox>
                    <asp:CalendarExtender ID="TxtDeactivateDatey_CalendarExtender" runat="server" 
                        TargetControlID="TxtDeactivateDate" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    </td>
                <td class="style1">
                    <label>Deactivated Reason</label></td>
                <td class="style1">
                    <asp:TextBox ID="TxtDeactivatereason" runat="server" Width="100%"></asp:TextBox>
                </td>
                <td class="style1">
                    </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <label>Information Date</label></td>
                <td>
                    <asp:TextBox ID="TxtInformationDate" runat="server" Width="100%"></asp:TextBox>
                    <asp:CalendarExtender ID="TxtInformationDate_CalendarExtender" runat="server" 
                        TargetControlID="TxtInformationDate" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <label>Remarks/Source Of Information</label></td>
                <td>
                    <asp:TextBox ID="TxtRemarks" runat="server" Width="100%"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td align="center" colspan="2">
                    <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" OnClientClick="return ValidateSave();"/>
                    &nbsp;<asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" />
                    &nbsp;<asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />
                    <asp:HiddenField ID="HdnAlert" runat="server" Value="Please Enter Values" />
                </td>
                <td width="2%">
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    </legend>
   </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">

    <LINK REL=Stylesheet HREF= "CSS/MasterStyle.css" TYPE="text/css" MEDIA=screen>    
    <style type="text/css">
        .style1
        {
            height: 26px;
        }
    </style>
</asp:Content>
