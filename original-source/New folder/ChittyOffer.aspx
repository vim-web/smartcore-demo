<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ChittyOffer.aspx.vb" Inherits="SMARTCORE_UI.Offer" MasterPageFile="~/SMARTCORE.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    

        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 44%; margin-left:10%; margin-right:10%;">
    <table align="center" style="width: 99%">
<tr>
<td width="2%" colspan="2" style="width: 50%"><legend>Offer</legend></td>
<td width="48%">
    &nbsp;</td>
<td width="2%">&nbsp;</td>
</tr>
        <tr>
            <td width="2%">
            </td>
            <td width="48%">
                <asp:Label ID="Label1" runat="server" Text="Offer No"></asp:Label>
            </td>
            <td width="48%">
                <asp:TextBox ID="TxtOfrNo" runat="server" Width="100%" AutoPostBack="True"></asp:TextBox>
            </td>
            <td width="2%">
            </td>
        </tr>
<tr>
<td>&nbsp;</td>
<td>
    <asp:Label ID="Label2" runat="server" Text="Offer Date"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="TxtOfrDate" Width="100%" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="TxtOfrDate_CalendarExtender" runat="server" 
        TargetControlID="TxtOfrDate"></asp:CalendarExtender>
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td>
    <asp:Label ID="Label3" runat="server" Text="Chitty Account No"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="TxtChittyAcNo" Width="100%" runat="server"></asp:TextBox>
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td></td>
<td>
    <asp:Label ID="Label4" runat="server" Text="Name"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="TxtName" Width="100%" runat="server"></asp:TextBox>
    </td>
<td></td>
</tr>
<tr>
<td>&nbsp;</td>
<td>
    <asp:Label ID="Label5" runat="server" Text="Address"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="TxtAddress" Width="100%" runat="server"></asp:TextBox>
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td>
    <asp:Label ID="Label6" runat="server" Text="Mode Of Receipt"></asp:Label>
    </td>
<td>
    <asp:DropDownList ID="CmbModeOfRecpt" runat="server" Width="100%">
        <asp:ListItem>Receipt</asp:ListItem>
        <asp:ListItem>Cash</asp:ListItem>
        <asp:ListItem>Cheque</asp:ListItem>
    </asp:DropDownList>
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td>
    <asp:Label ID="Label7" runat="server" Text="Details"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="TxtDetails" Width="100%" runat="server"></asp:TextBox>
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td align="center" colspan="2">
    <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" />
    &nbsp;<asp:ImageButton ID="CmdClear" runat="server" 
        ImageUrl="~/Images/Clear.png" />
    &nbsp;<asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />
    </td>
<td>&nbsp;</td>
</tr>
</table>
    </div>
     </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>

