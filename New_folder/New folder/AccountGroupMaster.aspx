<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="AccountGroupMaster.aspx.vb" Inherits="SMARTCORE_UI.AccountGroupMaster" 
    title="Account Group Master" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
<script type='text/javascript' src="Scripts/Common.js"></script>
 <script type="text/javascript">
     function ValidateSave() {
         var Ctl = document.getElementById('<%=TxtAccGroupCode.ClientID%>');
         if (Ctl.value == "") {
             alert('Enter Account Group Code..');
             Ctl.focus();
             return false;
         }
         var Ct2 = document.getElementById('<%=TxtAccGrpName.ClientID%>');
         if (Ct2.value == "") {
             alert('Enter Account Group Name..');
             Ct2.focus();
             return false;
         }
         var Ct3 = document.getElementById('<%=TxtSeqNo.ClientID%>');
         if (Ct3.value == "") {
             alert('Enter Sequence No..');
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
<legend>Account Group Master</legend>               
<br />
<table width="100%">
<tr>
<td width="5%"></td>
<td width="30%"></td>
<td width="40%"></td>
<td width="25%"></td>
</tr>

<tr>
<td width="5%"></td>
<td width="30%"><label>Group Code</label></td>
<td width="40%">
    <asp:TextBox ID="TxtAccGroupCode" runat="server" AutoPostBack="True" 
        CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" 
        Width="100%"></asp:TextBox>
    </td>
<td width="25%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="30%"><label>Group Name</label></td>
<td width="40%">
    <asp:TextBox ID="TxtAccGrpName" runat="server" AutoPostBack="True" 
        CssClass="twitterStyleTextbox" 
        Width="100%"></asp:TextBox>
    </td>
<td width="25%"></td>
</tr>
<tr>
<td width="5%"></td>
<td width="30%"><label>Sequence No</label></td>
<td width="40%">
    <asp:TextBox ID="TxtSeqNo" runat="server" AutoPostBack="True" 
        CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" 
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
<td width="30%"></td>
<td width="40%"></td>
<td width="25%"></td>
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
<td width="25%"></td>
</tr>
</table>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>


