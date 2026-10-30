<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="Loan_DueDate_Diary.aspx.vb" Inherits="SMARTCORE_UI.Loan_DueDate_Diary" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
 
     <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;margin-top:5%; font-family: Tahoma" >                
    
    <legend>Loan Due Date Diary</legend> 
    
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
            <td width="48%"><label>From Date</label>
                &nbsp;</td>
            <td width="48%">
                <asp:TextBox ID="TxtFrDate" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="TxtFrDate_CalendarExtender" runat="server" format="dd/MM/yyyy"
                    TargetControlID="TxtFrDate">
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
                To Date</label></td>
            <td width="48%">
                <asp:TextBox ID="TxtToDate" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="TxtToDate_CalendarExtender" runat="server" 
                    format="dd/MM/yyyy" TargetControlID="TxtToDate">
                </asp:CalendarExtender>
            </td>
            <td width="2%">
                &nbsp;</td>
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

