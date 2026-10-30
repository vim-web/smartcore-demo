<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="RegionMaster.aspx.vb" Inherits="SMARTCORE_UI.RegionMaster" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
<script type='text/javascript' src="Scripts/Common.js"></script>
 <script type="text/javascript">
     function ValidateSave() {
         var Ctl = document.getElementById('<%=TxtRegionCode.ClientID%>');
         if (Ctl.value == "") {
             alert('Enter Region  Code..');
             Ctl.focus();
             return false;
         }
         var Ct2 = document.getElementById('<%=TxtRegionName.ClientID%>');
         if (Ct2.value == "") {
             alert('Enter Region Name..');
             Ct2.focus();
             return false;
         }
         var Ct3 = document.getElementById('<%=TxtRemarks.ClientID%>');
         if (Ct3.value == "") {
             alert('Enter Remarks..');
             Ct3.focus();
             return false;
         }
         var Ct4 = document.getElementById('<%=TxtRemarks.ClientID%>');
         if (Ct4.value == "") {
             alert('Enter Remarks..');
             Ct4.focus();
             return false;
         }
     }
 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True">
    <Scripts>
            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
    </Scripts>
</asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 50%; margin-left:25%; margin-right:25%;" > 
<legend>Region Master</legend>               
<br />
<table width="100%">
<tr>
<td width="5%"></td>
<td width="30%"></td>
<td width="40%"></td>
<td width="25%">&nbsp;</td>
</tr>

<tr>
<td width="5%"></td>
<td width="30%"><label>Region Code</label></td>
<td width="40%">
    <asp:TextBox ID="TxtRegionCode" runat="server" AutoPostBack="True" 
        CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" 
        Width="100%"></asp:TextBox>
    </td>
<td width="25%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="30%"><label>Region Name</label></td>
<td width="40%">
    <asp:TextBox ID="TxtRegionName" runat="server" AutoPostBack="True" 
        CssClass="twitterStyleTextbox" 
        Width="100%"></asp:TextBox>
    </td>
<td width="25%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="30%"><label>Remarks</label></td>
<td width="40%">
    <asp:TextBox ID="TxtRemarks" runat="server" AutoPostBack="True" 
        CssClass="twitterStyleTextbox"
        Width="100%"></asp:TextBox>
    </td>
<td width="25%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="30%"><label>Emp.ID</label></td>
<td width="40%">
    <asp:TextBox ID="TxtRegionManager" runat="server" Width="100%" 
        AutoPostBack="True"></asp:TextBox></td>
<td width="25%">
    <asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
        ImageUrl="~/images/Search.png" Width="23px" />
    </td>
</tr>
    <tr>
        <td width="5%">
            &nbsp;</td>
        <td width="30%">
            <label>Region Manager</label></td>
        <td width="40%">
            <asp:TextBox ID="TxtName" width="100%" runat="server"></asp:TextBox></td>
        <td width="25%">
            &nbsp;</td>
    </tr>
<tr>
<td width="5%"></td>
<td width="30%"></td>
<td width="40%"></td>
<td width="25%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="30%" colspan="2" style="width: 70%" align="center">
    <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
        meta:resourcekey="CmdSaveResource1" OnClientClick="return ValidateSave();" 
        TabIndex="56" />
    <asp:ImageButton ID="CmdClear0" runat="server" ImageUrl="~/Images/Clear.png" 
        meta:resourcekey="CmdSaveResource1" TabIndex="36" />
    <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/images/Close.png" 
        meta:resourcekey="CmdClearResource1" PostBackUrl="~/Main.aspx" 
        style="margin-top: 0px" TabIndex="57" />
    </td>
<td width="25%">
    <asp:HiddenField ID="hid_RegCode" runat="server" />
    </td>
</tr>
</table>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

