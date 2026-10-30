<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="EmpLeavesettings.aspx.vb" Inherits="SMARTCORE_UI.EmpLeavesettings" 
    title="Untitled Page" %>
        <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> 
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
        </Scripts>
        </asp:ToolkitScriptManager>



<div class="gradientBoxesWithOuterShadows" style="width:60%; margin-left:20%; margin-right:30%;" >     
<legend>EmpLeaveSetting</legend>
    <table width="100%" >
    <tr>
    <td width="2%"></td>
    <td width="24%"></td>
     <td width="24%"></td>
      <td width="2%"></td>
      <td width="24%"></td>
     <td width="24%"></td>
      <td width="2%"></td>
    </tr>
     <tr>
    <td width="2%"></td>
    <td width="24%"><label>LeaveType</label></td>
     <td width="24%">
         <asp:DropDownList ID="CmbLeaveType" runat="server" Width="100%">
         </asp:DropDownList>
         </td>
      <td width="2%"></td>
      <td width="24%"><label>MaxLeavePerYear</label></td>
     <td width="24%">
         <asp:TextBox ID="TxtMaxLeave" runat="server" Width="90%"></asp:TextBox>
         </td>
      <td width="2%"></td>
    </tr>
     <tr>
    <td width="2%"></td>
    <td width="24%"></td>
     <td width="24%">
         <asp:CheckBox ID="ChkEncash" runat="server" Text="EnCashable" />
         </td>
      <td width="2%"></td>
      <td width="24%">
          <asp:CheckBox ID="Chk_CarryOverToNxtYr" runat="server" 
              Text="Carry Over To Next Yr" AutoPostBack="True" />
         </td>
     <td width="24%"></td>
      <td width="2%"></td>
    </tr>
     <tr>
    <td width="2%"></td>
    <td width="24%"><label>Max Carry Over To Nxt yr</label></td>
     <td width="24%">
         <asp:TextBox ID="TxtMaxCarry" runat="server"  Width="100%"></asp:TextBox>
         </td>
      <td width="2%"></td>
      <td width="24%"><label>PerLeaveCount</label></td>
     <td width="24%">
         <asp:TextBox ID="TxtLeaveCount" runat="server" Width="90%"></asp:TextBox>
         </td>
      <td width="2%"></td>
    </tr>
     <tr>
    <td width="2%"></td>
    <td width="24%"></td>
     <td width="24%"></td>
      <td width="2%"></td>
      <td width="24%"></td>
     <td width="24%"></td>
      <td width="2%"></td>
    </tr>
     <tr>
    <td width="2%"></td>
    <td width="24%"></td>
     <td colspan="3" style="width: 48%">
         <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
             meta:resourcekey="CmdSaveResource1" OnClientClick="return ValidateSave();" 
             TabIndex="35" />
         <asp:ImageButton ID="CmdClear0" runat="server" ImageUrl="~/Images/Clear.png" 
             meta:resourcekey="CmdSaveResource1" TabIndex="36" />
         <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/images/Close.png" 
             meta:resourcekey="CmdClearResource1" PostBackUrl="~/Main.aspx" TabIndex="37" />
         </td>
     <td width="24%">&nbsp;</td>
      <td width="2%"></td>
    </tr>
    </table>
    
    
    
    
                  
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
