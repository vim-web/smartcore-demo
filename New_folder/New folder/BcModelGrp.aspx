<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="BcModelGrp.aspx.vb" Inherits="SMARTCORE_UI.BcModelGrp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>

    <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;margin-top:5%; font-family: Tahoma" >                    
    <legend>BC Model Group</legend>           
    <table width="100%">
    <tr>
    <td width="2%"></td>
    <td width="48%"><label>Branch</label></td>
    <td width="48%">
        <asp:DropDownList ID="CmbBranch" runat="server" Width="100%">
        </asp:DropDownList>
        </td>
    <td width="2%"></td>
    </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%">
                <label>
                Group Id</label></td>
            <td width="48%">
                <asp:TextBox ID="TxtGrpId" runat="server" AutoPostBack="True"></asp:TextBox>
            </td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%">
                <label>
                Group Name</label></td>
            <td width="48%"> 
                <asp:TextBox ID="TxtGrpName" runat="server"  Width="100%"></asp:TextBox>
            </td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%">
                <label>
                Date</label></td>
            <td width="48%">
                <asp:TextBox ID="TxtDate" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="TxtDate_CalendarExtender" runat="server"  Format="dd/MM/yyyy" 
                    TargetControlID="TxtDate">
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
                Loan Scheme</label></td>
            <td width="48%">
                <asp:DropDownList ID="CmbLoanScheme" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%">
                <label>
                Loan No</label></td>
            <td width="48%">
                <asp:TextBox ID="TxtLoanNo" runat="server" AutoPostBack="True"></asp:TextBox>
            </td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%">
                <label>
                Name</label></td>
            <td width="48%">
                <asp:TextBox ID="TxtName" runat="server" AutoPostBack="True" Width="100%"></asp:TextBox>
            </td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%">
                <label>
                Address</label></td>
            <td width="48%">
                <asp:TextBox ID="TxtAddress" runat="server" TextMode="MultiLine" Width="100%" 
                    AutoPostBack="True"></asp:TextBox>
            </td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td align="center" colspan="2" style="width: 96%" width="48%">
                <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" />
                &nbsp;<asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />
                <asp:HiddenField ID="HdnAccid" runat="server" />
            </td>
            <td width="2%">
                &nbsp;</td>
        </tr>
    </table>

     </div> 
</ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>
