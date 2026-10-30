<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="AssetDepreciation_rpt.aspx.vb" Inherits="SMARTCORE_UI.AssetDepreciation_rpt" 
    title="Asset Depreciation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
     <script type='text/javascript' src="Scripts/Common.js"></script> 
   </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  
        EnablePageMethods="True" CombineScripts="True"> 
       
        </asp:ToolkitScriptManager>
    <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;margin-top:5%; " >                
        <legend>DEPRECIATION REPORT</legend> <br />

<table width="100%">
<tr>
<td width="2%"></td>
<td width="48%"></td>
<td width="48%"></td>
<td width="2%"></td>
</tr>
<tr>
<td width="2%"></td>
<td width="48%">
   <label>From Date</label>
    </td>
<td width="48%">
    <asp:TextBox ID="Txt_DateFrom" runat="server" BackColor="White" 
        CssClass="twitterStyleTextbox"  TabIndex="1" 
        Width="97%"></asp:TextBox>
    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
        TargetControlID="Txt_DateFrom">
    </asp:CalendarExtender>
    </td>
<td width="2%"></td>
</tr>
<tr>
<td width="2%"></td>
<td width="48%"></td>
<td width="48%"></td>
<td width="2%"></td>
</tr>
<tr>
<td width="2%"></td>
<td width="48%" colspan="2" style="width: 96%" align="center">
  <asp:ImageButton ID="CmdShow" runat="server" Height="32px" 
        ImageUrl="~/Images/Show.png" Width="96px" />
    <asp:ImageButton ID="CmdClose" runat="server" Height="32px" 
        ImageUrl="~/Images/CloseBtn.png" Width="96px" /></td>
<td width="2%">
  
    </td>
</tr>
<tr>
<td width="2%"></td>
<td width="48%"></td>
<td width="48%"></td>
<td width="2%"></td>
</tr>


</table>

</div>
</ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
