<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ExternalLienRevoke.aspx.vb" Inherits="SMARTCORE_UI.ExternalLienRevoke" MasterPageFile="~/SMARTCORE.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    

        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;">
    <table width= "100%">
        <tr>
            <td colspan="2" style="width: 42%" width="2%">
                <legend>External Lien Revoke</legend></td>
        </tr>
        </td>
        <tr>
            <td width="2%">
            </td>
            <td width="40%">
                <label>
                Branch</label>
            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td width="30%">
                            <asp:TextBox ID="TxtBranch" runat="server" Width="95%" AutoPostBack="True"></asp:TextBox>
                        </td>
                        <td width="70%">
                            <asp:DropDownList ID="CmbBranch" runat="server" AutoPostBack="True" 
                                Width="100%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="2%">
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Scheme</label>
            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td width="30%">
                            <asp:TextBox ID="TxtScheme" runat="server" AutoPostBack="True" Width="95%"></asp:TextBox>
                        </td>
                        <td width="70%">
                            <asp:DropDownList ID="CmbScheme" runat="server" AutoPostBack="True" 
                                Width="100%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Account No</label>
            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td width="30%">
                            <asp:TextBox ID="TxtShortCode" runat="server" AutoPostBack="True" Width="95%"></asp:TextBox>
                        </td>
                        <td width="70%">
                            <asp:TextBox ID="TxtAccNo" runat="server" AutoPostBack="True" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="2">
                &nbsp;<asp:GridView ID="DgvLienRevoke" runat="server" 
                    AutoGenerateColumns="False" Width="100%" CssClass="grid-view">
                    <Columns>
                        <asp:BoundField DataField="Tr_Date" HeaderText="Lien Date" />
                        <asp:BoundField DataField="LienWith" HeaderText="Lien With" />
                        <asp:BoundField DataField="LienRemarks" HeaderText="Lien Remarks" />
                        <asp:ButtonField Text="Revoke" />
                    </Columns>
                </asp:GridView>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td align="center" colspan="2">
            <asp:HiddenField ID="HdnAccID" runat="server" />
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
</table>
    
    </div>
   </ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>

