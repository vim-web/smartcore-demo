<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="DepositCanvased.aspx.vb" Inherits="SMARTCORE_UI.DepositCanvased" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
    <link href="CSS/ui-lightness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript' src="Scripts/Common.js"></script>
    <script type='text/javascript'>
        function ValidateSave() {

            var Ctl = document.getElementById('<%=Txt_Cust_Id.ClientID%>');
            if (Ctl.value == "") {
                alertify.error('Please Select a CustomerID');
                Ctl.focus();
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="Updatepanel1" runat="server">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePageMethods="True" CombineScripts="True">
    <Scripts>
            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
    </Scripts>
</asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 90%; margin-left:5%; margin-right:5%; margin-top:2%; font-family: Tahoma">
<legend>Deposit Canvas</legend>
<table>
<tr>
<td width="50px"></td>
<td width="50px"></td>
<td></td>
<td></td>
<td width="50px"></td>
<td width="50px"></td>
<td></td>
</tr>
<tr>
<td></td>
<td></td>
<td>
    <asp:Label ID="Lbl_Br_Code" runat="server" CssClass="lblCSS" Text="Branch Code"></asp:Label>
    </td>
<td></td>
<td>
    <asp:DropDownList ID="Drp_Branch" runat="server" Height="28px" Width="100%" 
        AutoPostBack="True" Enabled="False">
    </asp:DropDownList>
    </td>
<td></td>
<td></td>
</tr>
<tr>
<td></td>
<td></td>
<td>
    <asp:Label ID="Lbl_Customer_Id" runat="server" CssClass="lblCSS" 
        Text="Customer ID"></asp:Label>
    </td>
<td></td>
<td class="twitterStyleTextbox">
    <asp:TextBox ID="Txt_Cust_Id" runat="server" CssClass="twitterStyleTextbox" 
        Height="20px" Width="250px" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
    </td>
<td>
    <asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
        ImageUrl="~/images/Search.png" Width="26px" />
    </td>
<td></td>
</tr>
<tr>
<td></td>
<td></td>
<td>
    <asp:Label ID="Lbl_Customer_Name" runat="server" CssClass="lblCSS" 
        Text="Customer Name"></asp:Label>
    </td>
<td></td>
<td>
    <asp:TextBox ID="Txt_Cust_Name" runat="server" Height="20px" Width="250px" 
        CssClass="twitterStyleTextbox" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
    </td>
<td></td>
<td></td>
</tr>
<tr>
<td></td>
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
<td colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
        meta:resourcekey="CmdSaveResource1" OnClientClick="return ValidateSave();" 
        TabIndex="35" />
    <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" 
        meta:resourcekey="CmdSaveResource1" TabIndex="36" />
    <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/images/Close.png" 
        meta:resourcekey="CmdClearResource1" TabIndex="37" />
    </td>
<td></td>
<td></td>
</tr>
</table>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
