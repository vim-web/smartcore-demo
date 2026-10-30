<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="EmpLeave.aspx.vb" Inherits="SMARTCORE_UI.EmpLeave" 
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


<div class="gradientBoxesWithOuterShadows" style="width:60%; margin-left:20%; margin-right:20%;" >                
    <legend>Leave Entry</legend> 
    <table width="100%">
    <tr>
    <td width="2%"></td>
    <td width="24%"><label>EmpId</label></td>
    <td width="23%" style="width: 0%">
        <asp:TextBox ID="TxtEmp_Id" runat="server" Width="100%"></asp:TextBox>
        </td>
        <td style="width: 5%" width="4%">
            <asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
                ImageUrl="~/images/Search.png" Width="23px" />
        </td>
    <td width="20%"><label>Empname</label></td>
    <td width="24%">
        <asp:TextBox ID="TxtEmp_Name" runat="server" Height="25px" Width="100%"></asp:TextBox>
        </td>
    <td width="2%"></td>
     </tr>
     <tr>
    <td width="2%"></td>
    <td width="24%"><label>LeaveType</label></td>
    <td width="24%" colspan="2">
        <asp:DropDownList ID="Cmb_LeaveType" runat="server" Width="100%">
        </asp:DropDownList>
         </td>
    <td width="24%"><label>Description</label></td>
    <td width="24%">
        <asp:TextBox ID="Txt_Description" runat="server" Width="100%" 
            TextMode="MultiLine"></asp:TextBox>
         </td>
    <td width="2%"></td>
     </tr>
     <tr>
    <td width="2%"></td>
    <td width="24%"><label>FromDate</label></td>
    <td width="24%" colspan="2">
        <asp:TextBox ID="TxtFromDate" runat="server" OnChange="validatedate(this);" 
            Width="90%"></asp:TextBox>
        <asp:CalendarExtender ID="TxtFromDate_CalendarExtender" runat="server" 
            Format="dd/MM/yyyy" TargetControlID="TxtFromDate">
        </asp:CalendarExtender>
         </td>
    <td width="24%"><label>ToDate</label></td>
    <td width="24%">
        <asp:TextBox ID="TxtToDate" runat="server" OnChange="validatedate(this);" 
            Width="100%"></asp:TextBox>
        <asp:CalendarExtender ID="TxtToDate_CalendarExtender" runat="server" 
            Format="dd/MM/yyyy" TargetControlID="TxtToDate">
        </asp:CalendarExtender>
         </td>
    <td width="2%"></td>
     </tr>
     <tr>
    <td width="2%"></td>
    <td width="24%"></td>
    <td width="24%" colspan="2" align="right">
        <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
            meta:resourcekey="CmdSaveResource1" TabIndex="56" />
         </td>
    <td width="24%">
        <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/images/Close.png" 
            meta:resourcekey="CmdClearResource1" TabIndex="57" />
         </td>
    <td width="24%"></td>
    <td width="2%"></td>
     </tr>
     <tr>
    <td width="2%"></td>
    <td width="24%"></td>
    <td width="24%" colspan="2"></td>
    <td width="24%"></td>
    <td width="24%"></td>
    <td width="2%"></td>
     </tr>
     
    </table>
    
    
                  
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
