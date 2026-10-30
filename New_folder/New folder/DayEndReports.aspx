<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DayEndReports.aspx.vb" Inherits="SMARTCORE_UI.DayEndReports"  MasterPageFile="~/SMARTCORE.Master"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    <table width="100%">
        <tr>
            <td width="10%"></td>
            <td width="10%">&nbsp;</td>
            <td width="30%">&nbsp;</td>
            <td width="30%"></td>
            <td width="10%"></td>
            <td width="10%"></td>
        </tr>
        <tr>
            <td ></td>
            <td >
                <asp:Label ID="LblBranch" runat="server" Text="Branch"></asp:Label>
            </td>
            <td >
                <asp:DropDownList ID="DdlBranch" runat="server" AutoPostBack="True" Width="100%">
                </asp:DropDownList>
            </td>
            <td ></td>
            <td ></td>
            <td ></td>
        </tr>
        <tr>
            <td ></td>
            <td >
                <asp:Label ID="LblBranch0" runat="server" Text="Report Type"></asp:Label>
            </td>
            <td> 
                <asp:RadioButton ID="Opt_GLOS_Diff_Detail" runat="server" 
                    Text="GL and Outstanding Difference - Detail" Checked="True" 
                    GroupName="Main" />
            </td>
            <td >
                <asp:Label ID="LblProgress" runat="server" BorderStyle="None" Text="Label"></asp:Label>
            </td>
            <td ></td>
            <td ></td>
        </tr>
        <tr>
            <td ></td>
            <td ></td>
            <td >
                <asp:RadioButton ID="Opt_GLOS_Diff_WithNPA" runat="server" 
                    Text="GL and Outstanding Difference With NPA" GroupName="Main" />
            </td>
            <td ></td>
            <td ></td>
        </tr>
        <tr>
            <td ></td>
            <td ></td>
            <td > 
                <asp:RadioButton ID="Opt_GLOS_Diff" runat="server" 
                    Text="GL and Outstanding Difference" GroupName="Main" />
            </td>
            <td ></td>
            <td ></td>
        </tr>
        <tr>
            <td ></td>
            <td ></td>
            <td > 
                <asp:RadioButton ID="Opt_RandD_Tran_Diff" runat="server" 
                    Text="R&D and Transaction Difference" GroupName="Main" />
            </td>
            <td ></td>
            <td ></td>
        </tr>
        <tr>
            <td ></td>
            <td ></td>
            <td > 
                <asp:RadioButton ID="Opt_Daybook_Diff0" runat="server" 
                    Text="Daybook Difference"  GroupName="Main"/>
            </td>
            <td ></td>
            <td ></td>
        </tr>
        <tr>
            <td ></td>
            <td >
                &nbsp;</td>
            <td >
                &nbsp;</td>
            <td ></td>
            <td ></td>
            <td ></td>
        </tr>

    </table>
    <table width="100%">
        <tr>
            <td width="10%"></td>
            <td width="10%">
                <asp:Label ID="LblBranch1" runat="server" Text="Date"></asp:Label>
            </td>
            <td width="10%" style="width: 20%">
                <asp:TextBox ID="TxtAsonDate" runat="server" Width="95%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TxtAsonDate"> </asp:CalendarExtender>      
            </td>
            <td width="10%">
                <asp:Button ID="BtnProcess" runat="server" Text="Process" Width="95%" />
            </td>
            <td width="10%">
                <asp:Button ID="BtnShow" runat="server" Text="Show" Width="95%" />
            </td>
            <td width="10%"></td>
            <td width="10%"></td>
            <td width="10%"></td>
            <td width="10%"></td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
            EnablePageMethods="true">
        </asp:ToolkitScriptManager>

        
        
        
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>