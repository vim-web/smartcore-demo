<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="LoanSanctionRpt.aspx.vb" Inherits="SMARTCORE_UI.LoanSanctionRpt" 
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


<div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%; margin-top:10%" >                
    <legend>Loan Sanction Procedure</legend> 
    <table width="100%">
        <tr>
            <td width="50%"><label>Branch</label></td>
        
            <td width="50%">
                <asp:DropDownList ID="CmbBranch" runat="server" Width="50%">
                </asp:DropDownList>
            </td>
                

        </tr>
        <tr> <td width="50%"><label>Report Options</label></td>
            <td width="50%">
                <asp:DropDownList ID="CmbRptOpt" runat="server" Width="50%">
                </asp:DropDownList>
            </td>

        </tr>
        <tr>
            <td width="50%">
                <label>
                As on Date</label></td>
            <td width="50%">
                <asp:TextBox ID="TxtToDate" runat="server" OnChange="validatedate(this);" 
                    Width="50%"></asp:TextBox>
                <asp:CalendarExtender ID="TxtToDate_CalendarExtender" runat="server" 
                    Format="dd/MM/yyyy" TargetControlID="TxtToDate">
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td width="50%" ></td>
            <td width="50%" ></td>
        </tr>
        <tr>
            <td width="50%" align="right">
                <asp:ImageButton ID="CmdShow" runat="server" ImageUrl="~/Images/Show.png" style="margin-bottom: 0px" />
            </td>
            <td width="50%">
                <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/CloseBtn.png" />
            </td>
        </tr>
    </table>
    
    
                  
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
