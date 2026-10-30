<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DeactivateRevoke.aspx.vb" Inherits="SMARTCORE_UI.DeactivateRevoke" MasterPageFile="~/SMARTCORE.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
    <br />
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="gradientBoxesWithOuterShadows" 
        style="width: 49%; margin-left: 10%; margin-right: 10%;">
    <table style="width: 100%">
            <tr>
                <td width="2%">
                    &nbsp;</td>
                <td width="48%">
                    <label>Branch</label></td>
                <td width="48%">
                    <asp:TextBox ID="TxtBranch" runat="server" Width="30%"></asp:TextBox>
                    &nbsp;<asp:DropDownList ID="CmbBranch" runat="server" Width="65%">
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
                    Customer No</label></td>
                <td width="48%">
                    <asp:TextBox ID="TxtCid" runat="server" Width="100%"></asp:TextBox>
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
                    <asp:TextBox ID="TxtAddress" runat="server" Width="100%" TextMode="MultiLine"></asp:TextBox>
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
                    <asp:DropDownList ID="CmbDeactiveType" runat="server" Width="100%">
                        <asp:ListItem>Decease Customer</asp:ListItem>
                        <asp:ListItem>Fraudulent Customer</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <label>Revoke Date</label></td>
                <td>
                    <asp:TextBox ID="TxtRevokeDate" runat="server" Width="100%"></asp:TextBox>
                    <asp:CalendarExtender ID="TxtRevokeDate_CalendarExtender" runat="server" 
                        TargetControlID="TxtRevokeDate">
                    </asp:CalendarExtender>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <label>Revoke Reason</label></td>
                <td>
                    <asp:TextBox ID="TxtRevokeReason" runat="server" Width="100%"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td align="center" class="style5" colspan="2">
                    <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" />
                    &nbsp;<asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" />
                    &nbsp;<asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />
                </td>
                <td width="2%">
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>
