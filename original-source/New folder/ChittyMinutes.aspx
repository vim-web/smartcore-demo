<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ChittyMinutes.aspx.vb" Inherits="SMARTCORE_UI.Minutes" MasterPageFile="~/SMARTCORE.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    

        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 44%; margin-left:10%; margin-right:10%;">
    <table width="100%">
<tr>
<td width= "2%" colspan="2" style="width: 50%"><legend>Minutes</legend></td>
<td width= "48%">
    &nbsp;</td>
<td width= "2%">&nbsp;</td>
</tr>
        <tr>
            <td width="2%">
            </td>
            <td width="48%">
                <asp:Label ID="Label1" runat="server" Text="Chitty Group"></asp:Label>
            </td>
            <td width="48%">
                <asp:DropDownList ID="CmbChittyGrp" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
            <td width="2%">
            </td>
        </tr>
<tr>
<td>&nbsp;</td>
<td>
    <asp:Label ID="Label2" runat="server" Text="Auction No"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="TxtAucnNo" runat="server" Width="100%"></asp:TextBox>
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td>
    <asp:Label ID="Label5" runat="server" Text="Auction Date"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="TxtAucndate" runat="server" Width="100%"></asp:TextBox>
    <asp:CalendarExtender ID="TxtAucndate_CalendarExtender" runat="server" 
        TargetControlID="TxtAucndate">
    </asp:CalendarExtender>
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td>
    <asp:Label ID="Label6" runat="server" Text="Chitty Account No"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="TxtChittyAcNo" runat="server" Width="100%"></asp:TextBox>
    </td>
<td>&nbsp;</td>
</tr>
        <tr>
<td>&nbsp;</td>
<td>
    <asp:Label ID="Label3" runat="server" Text="Name"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="TxtName" runat="server" Width="100%"></asp:TextBox>
    </td>
<td>&nbsp;</td>
        </tr>
        <tr>
<td>&nbsp;</td>
<td>
    <asp:Label ID="Label4" runat="server" Text="Address"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="TxtAddress" runat="server" Width="100%"></asp:TextBox>
    </td>
<td>&nbsp;</td>
        </tr>
<tr>
<td>&nbsp;</td>
<td>
    <asp:Label ID="Label7" runat="server" Text="Amount"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="TxtAmount" runat="server" onkeypress="return validate(event)" Width="100%"></asp:TextBox>
    </td>
<td>
    <asp:ImageButton ID="CmdAdd" runat="server" Height="16px" />
    </td>
</tr>
<tr>
<td>&nbsp;</td>
<td colspan="2">
    <asp:GridView ID="Dgv_Chitty" Width="100%" runat="server" 
        AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField HeaderText="Chitty account no" DataField="Chtty_AccNo" />
            <asp:BoundField HeaderText="Name" DataField="Chtty_Name" />
            <asp:BoundField HeaderText="Address" DataField="Chtty_Address" />
            <asp:BoundField HeaderText="Amount" DataField="Chtty_Amt" />
            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
    </td>

<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td align="center" colspan="2">
    <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" />
    &nbsp;<asp:ImageButton ID="CmdClear" runat="server" 
        ImageUrl="~/Images/Clear.png" />
    &nbsp;<asp:ImageButton ID="CmdClose" runat="server" 
        ImageUrl="~/Images/Close.png" PostBackUrl="~/Main.aspx" />
    </td>
<td>&nbsp;</td>
</tr>
</table>
    </div>
    </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>

