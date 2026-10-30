<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="StandingCancellation.aspx.vb" Inherits="SMARTCORE_UI.StandingCancellation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
<script type='text/javascript' src="Scripts/Common.js"></script>
<%--<script type='text/javascript'>

    function ValidateSave() {

        var Ctl = document.getElementById('<%=TxtAccNoFrom.ClientID%>');
        if (Ctl.value == "") {
            alert('New Account No Not Entered, Please Check');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtAccNoTo.ClientID%>');
        if (Ctl.value == "") {
            alert('AccNo Not Entered, Please Check');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtExecutionDay.ClientID%>');
        if (Ctl.value == "") {
            alert('Execution Day Not Entered, Please Check');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtAmount.ClientID%>');
        if (Ctl.value == "") {
            alert('Amount Not Entered, Please Check');
            Ctl.focus();
            return false;
        }

        return true;
    }
        </script>--%>
<div class="gradientBoxesWithOuterShadows" width: 100%; margin-left:25%; margin-right:25%; > 
<legend>Standing Cancellation</legend>               
<br />

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<table width="100%">
<tr>
<td width="4%" class="style3"></td>
<td width="10%" class="style3"><label>Standing Id</label></td>
<td width="15%" class="style3">
    <asp:TextBox ID="TxtStandingId" runat="server" Width="70%" AutoPostBack="True"></asp:TextBox>
    &nbsp;<asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
        ImageUrl="~/images/Search.png" Width="23px" />
    </td>
    <td width="2%" class="style3">
        </td>
<td width="10%" class="style3">&nbsp;</td>
    <td width="15%" class="style3">
        &nbsp;</td>
    <td width="4%" class="style3">
        </td>
</tr>
<tr>
<td class="style2"></td>
<td class="style2">
    <asp:Label ID="Label1" runat="server" BackColor="Black" ForeColor="White" 
        Text="DEBIT    DETAILS"></asp:Label>
    </td>
<td class="style2"></td>
    <td class="style2">
        </td>
<td class="style2">
    &nbsp;</td>
    <td class="style2">
        </td>
    <td class="style2">
        </td>
</tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <label>Branch</label></td>
        <td>
            <asp:DropDownList ID="CmbBranchFrom" runat="server" AutoPostBack="True" 
                Width="100%" Enabled="False">
            </asp:DropDownList>
        </td>
        <td>
           </td>
        <td>
            <label>
            Name</label></td>
        <td>
         <table width="100%">
        <tr>
        <td width="70%">
         <asp:TextBox ID="TxtFromName" runat="server" Width="95%" Enabled="False"></asp:TextBox>
        </td>
        <td width="30%">
         <asp:TextBox ID="TxtCustId" runat="server" Width="100%" Enabled="False"></asp:TextBox>
        </td>
        </tr>
        </table>    
        </td>
        <td>
            </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <label>Scheme</label></td>
        <td>
        <table width="100%">
        <tr>
        <td width="30%">
        <asp:TextBox ID="TxtSchCodeFrom" width="95%" runat="server" AutoPostBack="True"></asp:TextBox>
        </td>
        <td width="70%">
         <asp:DropDownList ID="CmbSchemeFrom" runat="server" Width="100%" 
                AutoPostBack="True" Enabled="False">
            </asp:DropDownList>
            </td>
        </tr>
        </table>      
        </td>
        <td>
            &nbsp;</td>
        <td rowspan="2">
            <label>
            Address</label></td>
        <td rowspan="2">
            <asp:TextBox ID="TxtFromAddress" runat="server" Height="100%" TextMode="MultiLine" 
                Width="100%"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <label>Account No</label></td>
        <td>
        <table width="100%">
        <tr>
        <td width="30%">
         <asp:TextBox ID="TxtShortCode" runat="server" Width="95%" AutoPostBack="True" 
                Enabled="False"></asp:TextBox>
        </td>
        <td width="70%">
        <asp:TextBox ID="TxtAccNoFrom" runat="server" Width="100%" 
                AutoPostBack="True" Enabled="False"></asp:TextBox>
        </td>
        </tr>
        </table>
           
            
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            </td>
        <td>
            <asp:Label ID="Label2" runat="server" BackColor="Black" ForeColor="White" 
                Text="CREDIT DETAILS"></asp:Label>
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td >
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <label>Branch</label></td>
        <td>
        <asp:DropDownList ID="CmbBranchTo" width="100%" runat="server" Enabled="False">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
        <td>
            <label>
            Name</label></td>
        <td>
        <table width="100%">
        <tr>
        <td width="70%">
        <asp:TextBox ID="TxtToName" runat="server" Width="95%" Enabled="False"></asp:TextBox>
        </td>
        <td width="30%">
         <asp:TextBox ID="TxtToCustId" runat="server" Width="100%" Enabled="False"></asp:TextBox>
        </td>
        </tr>
        </table>    
            
            
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            </td>
        <td>
            <label>Scheme</label></td>
        <td>
        <table width="100%">
        <tr>
        <td width="30%">
        <asp:TextBox ID="TxtSchCodeTo" width="95%" runat="server" AutoPostBack="True" 
                style="height: 22px" Enabled="False"></asp:TextBox>
        </td>
        <td width="70%">
        <asp:DropDownList ID="CmbSchemeTo" width="100%" runat="server" AutoPostBack="True" 
                Enabled="False">
            </asp:DropDownList>
            </td>
        </tr>
        </table>        
            </td>
        <td>
            &nbsp;</td>
        <td rowspan="2">
            <label>
            Address</label></td>
        <td rowspan="2">
            <asp:TextBox ID="TxtToAddress" runat="server" Height="100%" TextMode="MultiLine" 
                Width="100%" Enabled="False"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            </td>
        <td>
            <label>Account No</label></td>
        <td>
        <table width="100%">
        <tr>
        <td width="30%">
         <asp:TextBox ID="TxtShortCode2" runat="server" Width="95%" AutoPostBack="True" 
                Enabled="False"></asp:TextBox>
        </td>
        <td width="70%">
        <asp:TextBox ID="TxtAccNoTo" runat="server" Width="100%" 
                AutoPostBack="True" Enabled="False"></asp:TextBox>
        </td>
        </tr>
        </table>
        </td>
        <td>
            </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <label>Status</label></td>
        <td>
            <asp:DropDownList ID="CmbStatus" runat="server" Width="100%">
                <asp:ListItem>Active / Current Posting</asp:ListItem>
                <asp:ListItem>Disable / Inactive</asp:ListItem>
                <asp:ListItem>Next Posting</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp; 
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td colspan="5">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:HiddenField ID="AccIDFrom" runat="server" /></td>
        <td>
            <asp:HiddenField ID="AccIdTo" runat="server" /></td>
        <td>
            <asp:HiddenField ID="DrAmount" runat="server" /></td>
        <td>
            <asp:HiddenField ID="CrAmount" runat="server" />
            </td>
        <td>
            <asp:HiddenField ID="FreqType" runat="server" />
           </td>
        <td>
            </td>
    </tr>
    <tr>
        <td>
            </td>
        <td align="center" colspan="5">
            <asp:ImageButton ID="CmdSave" runat="server" OnClientClick="return ValidateSave();" ImageUrl="~/Images/Save.png" />
            &nbsp;<asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" />
            &nbsp;<asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />
            </td>
    </tr>
</table>
    </div>
    </ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <LINK REL=Stylesheet HREF= "CSS/MasterStyle.css" TYPE="text/css" MEDIA=screen>
    <style type="text/css">
        .style2
        {
            height: 42px;
        }
        .style3
        {
            height: 44px;
        }
        </style>
</asp:Content>
