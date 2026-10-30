<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ChittyAuction.aspx.vb" Inherits="SMARTCORE_UI.ChittyAuction" MasterPageFile="~/SMARTCORE.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
<br />
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 70%; margin-left:15%; margin-right:15%;">
<legend>Chitty Auction</legend>
<table width="100%">
<tr>
    <td width="2%"></td>
    <td width="48%"><label>Scheme</label></td>
    <td width="48%">
        <asp:DropDownList ID="CmbScheme" runat="server" Width="100%" 
            AutoPostBack="True">
        </asp:DropDownList>
    </td>
    <td width="2%"></td>
</tr>
<tr>
    <td width="2%"></td>
    <td width="48%"><label>Chitty Group</label></td>
    <td width="48%">
        <asp:DropDownList ID="CmbChittyGrp" runat="server" Width="100%" 
            AutoPostBack="True">
        </asp:DropDownList>
    </td>
    <td width="2%"></td>
</tr>
<tr>
    <td></td>
    <td><label>Auction No</label></td>
    <td>
        <asp:TextBox ID="TxtAuctionNo" runat="server" Width="100%"  CssClass="twitterStyleTextbox"></asp:TextBox>
    </td>
    <td></td>
</tr>
<tr>
    <td>&nbsp;</td>
    <td><label>Auction Date</label>&nbsp;</td>
    <td>
        <asp:TextBox ID="TxtAuctionDate" runat="server" Width="100%"  CssClass="twitterStyleTextbox"></asp:TextBox>
        <asp:CalendarExtender ID="TxtAuctionDate_CalendarExtender" runat="server" TargetControlID="TxtAuctionDate"  Format="dd/MM/yyyy" >
        </asp:CalendarExtender>
    </td>
    <td>&nbsp;</td>
</tr>
<tr>
    <td>&nbsp;</td>
    <td><label>Account No</label></td>
    <td>
        <asp:DropDownList ID="CmbAccNo" runat="server" Width="100%" AutoPostBack="True">
        </asp:DropDownList>
    </td>
    <td>&nbsp;</td>
</tr>
<tr>
    <td>&nbsp;</td>
    <td><label>Name</label></td>
    <td>
        <asp:TextBox ID="TxtName" runat="server" Width="100%" CssClass="twitterStyleTextbox"></asp:TextBox>
    </td>
    <td>&nbsp;</td>
</tr>
<tr>
    <td>&nbsp;</td>
    <td><label>Auction Amount</label></td>
    <td><asp:TextBox ID="TxtAuctnAmt" runat="server" Width="100%"  
            onkeypress="return isFloatKey(event);" style="text-align:right"  
            CssClass="twitterStyleTextbox" AutoPostBack="True"></asp:TextBox></td>
    <td>&nbsp;</td>
</tr>
<tr>
    <td>&nbsp;</td>
    <td><label>Bank Charge</label></td>
    <td><asp:TextBox ID="TxtBankChrg" runat="server" style="text-align:right" Width="100%" CssClass="twitterStyleTextbox"></asp:TextBox></td>
    <td>&nbsp;</td>
</tr>
<tr>
    <td>&nbsp;</td>
    <td><label>Discount Amount</label></td>
    <td><asp:TextBox ID="TxtDiscntAmt" runat="server"  style="text-align:right" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox></td>
    <td>&nbsp;</td>
</tr>
<tr>
    <td></td>
    <td><label>Balance Amount</label></td>
    <td><asp:TextBox ID="TxtBalAmt" runat="server" style="text-align:right"  CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox></td>
    <td></td>
</tr>

<tr>
    <td colspan ="4">
        <asp:GridView ID="DgvTransaction" runat="server" AutoGenerateColumns="False" 
            TabIndex="32" Width="100%">
            <Columns>
                <asp:BoundField DataField="AccNo" HeaderText="AccNo" Visible="False">
                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Medium" />
                </asp:BoundField>
                <asp:BoundField DataField="AccCode" HeaderText="AccCode" Visible="False" />
                <asp:BoundField DataField="AccHead" HeaderText="Particulars">
                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Names="Times New Roman" Font-Size="Medium" 
                        ForeColor="White" HorizontalAlign="Left" Width="30%" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Medium" ForeColor="#333399" />
                </asp:BoundField>
                <asp:BoundField DataField="RctCash" DataFormatString="{0:F2}" 
                    HeaderText="Receipt Cash">
                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="15%" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Medium" ForeColor="#333399" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="RctTransfer" DataFormatString="{0:F2}" 
                    HeaderText="Receipt Transfer">
                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Medium" ForeColor="#333399" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="PayCash" DataFormatString="{0:F2}" 
                    HeaderText="Payment Cash">
                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Medium" ForeColor="#333399" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="PayTransfer" DataFormatString="{0:F2}" 
                    HeaderText="Payment Transfer">
                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Medium" ForeColor="#333399" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Seq_No" HeaderText="Seq_No" Visible="False" />
            </Columns>
            <AlternatingRowStyle BackColor="#F5F7FE" />
        </asp:GridView>
    </td>
</tr>

<tr>
    <td>
        <asp:HiddenField ID="HdnForeman" runat="server" />
    </td>
    <td align="center" colspan="2">
        <asp:HiddenField ID="HdnSala" runat="server" />
        <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" />
        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Clear.png" />
        <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" 
            PostBackUrl="~/Main.aspx" />
    </td>
    <td>
        <asp:HiddenField ID="HdnChrg" runat="server" />
    </td>
</tr>
</table>
    
</div>
</ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>
