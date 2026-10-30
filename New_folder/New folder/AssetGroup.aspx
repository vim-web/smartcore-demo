<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="AssetGroup.aspx.vb" Inherits="SMARTCORE_UI.WebForm8" 
    title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
     <script type='text/javascript' src="Scripts/Common.js"></script> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  
        EnablePageMethods="True" CombineScripts="True"> 
       
        </asp:ToolkitScriptManager>
    <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;margin-top:5%; " >                
        <legend>ASSET GROUP</legend> <br />
<table width="100%">
<tr>
<td width="2%"></td>
<td width="48%"><label>Group Code</label></td>
<td width="45%">
    <asp:TextBox ID="TxtGrpCode" runat="server" Width="100%" onkeypress="return isFloatKey(event);" 
        CssClass="twitterStyleTextbox" AutoPostBack="True" ></asp:TextBox>
    </td>
<td width="5%">
    <asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
        ImageUrl="~/images/Search.png" TabIndex="999" Width="23px" />
    </td>
</tr>
<tr>
<td width="2%"></td>
<td width="48%"><label>Group Name</label></td>
<td width="48%">
    <asp:TextBox ID="TxtGrpName" runat="server" Width="100%" 
        CssClass="twitterStyleTextbox" TabIndex="1"></asp:TextBox>
    </td>
<td width="2%"></td>
</tr>
<tr>
<td width="2%"></td>
<td width="48%"><label>Short Name</label></td>
<td width="48%">
    <asp:TextBox ID="TxtGrpShorName" runat="server" Width="100%" 
        CssClass="twitterStyleTextbox" TabIndex="2"></asp:TextBox>
    </td>
<td width="2%"></td>
</tr>
<tr>
<td width="2%"></td>
<td width="48%"><label>Depreciation %</label></td>
<td width="48%">
    <asp:TextBox ID="TxtGrpDepPerc" runat="server" Width="100%" 
        CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" 
        MaxLength="2" TabIndex="3" ></asp:TextBox>
    </td>
<td width="2%"></td>
</tr>
<tr>
<td width="2%"></td>
<td width="48%" colspan="2" style="width: 96%" align="center">
    <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
        meta:resourcekey="CmdSaveResource1" OnClientClick="return ValidateSave();" 
        TabIndex="4" style="height: 33px" />
    <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" 
        meta:resourcekey="CmdSaveResource1" TabIndex="5" />
    <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/images/Close.png" 
        meta:resourcekey="CmdClearResource1"  TabIndex="6" 
        style="height: 33px; width: 92px;" />
                         
</td>
<td width="2%"></td>
</tr></table>
</div>
</ContentTemplate>
</asp:UpdatePanel>

</asp:Content>

