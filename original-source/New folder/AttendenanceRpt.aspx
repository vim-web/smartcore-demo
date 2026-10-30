<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="AttendenanceRpt.aspx.vb" Inherits="SMARTCORE_UI.AttendenanceRpt" 
    title="Untitled Page" %>
     <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePageMethods="True" CombineScripts="True">
   
</asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;" >                
    <legend>Attendenance Register</legend> 
    <table width="100%">
        <tr>
            <td width="50%" ><label>Branch</label></td>
        
            <td width="50%" >
                <asp:DropDownList ID="CmbBranch" runat="server" Width="50%">
                </asp:DropDownList>
            </td>
                

        </tr>
        <tr>
            <td  width="50%">
                <label>
                From Date</label></td>
            <td  width="50%">
                <asp:TextBox ID="TxtFromDate" runat="server" OnChange="validatedate(this);" Width="50%"></asp:TextBox>
                <asp:CalendarExtender ID="TxtFromDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="TxtFromDate">
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr> <td width="50%"><label>To Date</label></td>
            <td width="50%">
               <asp:TextBox ID="TxtToDate" runat="server" OnChange="validatedate(this);" Width="50%"></asp:TextBox>
                <asp:CalendarExtender ID="TxtToDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="TxtToDate">
                </asp:CalendarExtender>
                 </td>

        </tr>
        <tr>
            <td width="50%" ><label>LeaveType</label></td>
            <td width="50%" >
                <asp:DropDownList ID="CmbLeaveType" runat="server" Width="50%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="50%">
                &nbsp;</td>
            <td width="50%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="50%" align="right">
                <asp:ImageButton ID="CmdShow" runat="server" ImageUrl="~/Images/Show.png" />
            </td>
            <td width="50%">
                <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/CloseBtn.png" />
            </td>
        </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    
                  
    
</asp:Content>
