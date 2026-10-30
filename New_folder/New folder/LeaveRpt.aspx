<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="LeaveRpt.aspx.vb" Inherits="SMARTCORE_UI.LeaveRpt" 
    title="Untitled Page" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
    <style type="text/css">
        .style1
        {
            height: 23px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
 
     <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;margin-top:5%; font-family: Tahoma" >                
    
    <legend>LeaveBalance</legend>  
    <table width="100%">
        <tr>
            <td width="2%">
            </td>
            <td width="48%">
                <label>
                Type</label></td>
            <td width="48%">
                <asp:DropDownList ID="CmbType" runat="server" Width="80%" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td width="2%">
            </td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%">
                 <label>
                As On Date</label></td>
            <td width="48%">
                <asp:TextBox ID="TxtAsonDate" runat="server" OnChange="validatedate(this);" 
                    Width="80%"></asp:TextBox>
                <asp:CalendarExtender ID="TxtAsonDate_CalendarExtender" runat="server" 
                    Format="dd/MM/yyyy" TargetControlID="TxtAsonDate">
                </asp:CalendarExtender>
            </td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%">
                <label>
                <asp:Label ID="LblEmp" runat="server" Text="EmpName"></asp:Label>
                </label>
            </td>
            <td width="48%">
                <asp:DropDownList ID="CmbEmpName" runat="server" Width="80%">
                </asp:DropDownList>
            </td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%">
                <label><asp:Label ID="LblLeaveType" runat="server" Text="LeaveType"></asp:Label></label>
            </td>
            <td width="48%">
                <asp:DropDownList ID="CmbLeavetype" runat="server" Width="80%">
                </asp:DropDownList>
            </td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%" class="style1">
                </td>
            <td width="48%" class="style1">
                </td>
            <td width="48%" class="style1">
            </td>
            <td width="2%" class="style1">
                </td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%" align="center" colspan="2" style="width: 96%">
                <asp:ImageButton ID="CmdShow" runat="server" ImageUrl="~/Images/Show.png" />
                &nbsp;<asp:ImageButton ID="CmdClose" runat="server" 
                    ImageUrl="~/Images/CloseBtn.png" />
            </td>
            <td width="2%">
                &nbsp;</td>
        </tr>
    </table>
    </div> 
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
