<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="ChittySuspense.aspx.vb" Inherits="SMARTCORE_UI.ChittySuspense" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
<script type='text/javascript' src="Scripts/Common.js"></script>

 <div class="gradientBoxesWithOuterShadows" style="width: 50%; margin-left:25%; margin-right:25%;margin-top:5%;">
<legend>GDS SUSPENSE TRANSFER</legend>               
<br />

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<table width="100%">
<tr>
<td width="2%"></td>
<td width="38%"><label>Branch</label></td>
<td width="58%">
    <asp:TextBox ID="TxtBranch" runat="server" AutoPostBack="True" Width="23%" CssClass="twitterStyleTextbox"></asp:TextBox>
    &nbsp;<asp:DropDownList ID="CmbBranch" runat="server" AutoPostBack="True" 
        Width="70%">
    </asp:DropDownList>
</td>
<td width="2%"></td>
</tr>
<tr>
<td width="2%"></td>
<td width="38%"><label>Scheme</label></td>
<td width="58%">
    <asp:TextBox ID="TxtScheme" runat="server" AutoPostBack="True" Width="23%" CssClass="twitterStyleTextbox"></asp:TextBox>
    &nbsp;<asp:DropDownList ID="CmbScheme" runat="server" AutoPostBack="True" 
        Width="70%">
    </asp:DropDownList>
</td>
<td width="2%"></td>
</tr>
    <tr>
        <td></td>
        <td><label>Chitty Group</label></td>
        <td>
            <asp:DropDownList ID="CmbGroup" runat="server" AutoPostBack="True" Width="100%">
            </asp:DropDownList>
        </td>
        <td></td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <label>Admission Fee</label></td>
        <td>
            <asp:TextBox ID="TxtAdmnFee" runat="server" Width="97%" CssClass="twitterStyleTextbox"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td align="center" colspan="2">
            <asp:ImageButton ID="CmdGenerate" runat="server" 
                ImageUrl="~/Images/ProcessBtn.png" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td colspan="2">
            <asp:GridView ID="GvAcc" runat="server" AutoGenerateColumns="False" 
                Width="100%" CssClass="grid-view">
                <Columns>
                    <asp:BoundField DataField="AccNo" HeaderText="Account No" />
                    <asp:BoundField DataField="MemNo" HeaderText="Member No" />
                    <asp:BoundField DataField="MemName" HeaderText="Name" />
                    <asp:BoundField DataField="Amt" HeaderText="Amount" />
                    <asp:BoundField DataField="AdmnFee" HeaderText="Admn Fee" />
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
            <asp:ImageButton ID="CmdTransfer" runat="server" ImageUrl="~/Images/Save.png" />
            &nbsp;<asp:ImageButton ID="CmdClose" runat="server" 
                ImageUrl="~/Images/Close.png" PostBackUrl="~/Main.aspx" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:HiddenField ID="HdnSuspAccid" runat="server" />
        </td>
        <td>
            <asp:HiddenField ID="HdnAdmnFee" runat="server" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
</table>
</div>
    </ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    
    </asp:Content>