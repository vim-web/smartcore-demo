<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LoanIntSubventionRpt.aspx.vb" MasterPageFile="~/SMARTCORE.Master" Inherits="SMARTCORE_UI.LoanIntSubventionRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
 
     <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;margin-top:5%; font-family: Tahoma" >                
    
    <legend>LOAN INTEREST SUBVENTION</legend>  
    
    <table width="100%">
        <tr>
            <td width="2%"></td>
            <td width="24%"></td>
            <td width="22%"></td>
            <td width="2%"></td>
            <td width="24%"></td>
            <td width="24%"></td>
            <td width="2%"></td>
        </tr>
        <tr>
            <td width="2%"></td>
            <td width="24%"><label>Branch</label></td>
            <td width="24%"><asp:TextBox ID="TxtBranch" runat="server" Width="97%" AutoPostBack="True"></asp:TextBox></td>
            <td width="2%"></td>
            <td colspan="2">
              <asp:DropDownList ID="CmbBranch" runat="server" Width="100%" AutoPostBack="True" > </asp:DropDownList>
            </td>
            <td width="2%"></td>
        </tr>
        <tr>
            <td width="2%"></td>
            <td width="24%"><label>Scheme</label></td>
            <td width="24%"><asp:TextBox ID="TxtScheme" runat="server" Width="97%" AutoPostBack="True"></asp:TextBox></td>
            <td width="2%"></td>
            <td colspan="2">
              <asp:DropDownList ID="CmbScheme" runat="server" Width="100%" AutoPostBack="True" > </asp:DropDownList>
            </td>
            <td width="2%"></td>
        </tr>
        <tr>
            <td ></td>
            <td ><label>From Date</label></td>
            <td >
                <asp:TextBox ID="TxtFrDate" runat="server" Width="97%"></asp:TextBox>            
            </td>
            <td></td>
            <td><label>To Date</label></td>
            <td>
                <asp:TextBox ID="TxtToDate" runat="server" Width="97%"></asp:TextBox>            
            </td>
            <td></td>
        </tr>
        <tr>
            <td ></td>
            <td ><label>Percent 1</label></td>
            <td >
                <asp:TextBox ID="TxtPercent1" runat="server" Width="97%"></asp:TextBox>            
            </td>
            <td></td>
            <td><label>Percent 1</label></td>
            <td>
                <asp:TextBox ID="TxtPercent2" runat="server" Width="97%"></asp:TextBox>            
            </td>
            <td></td>
        </tr>
        <tr>
            <td width="2%"></td>
            <td width="24%"></td>
            <td width="22%"></td>
            <td width="2%"></td>
            <td width="24%"></td>
            <td width="24%">&nbsp;&nbsp;</td>
            <td width="2%"></td>
        </tr>
        <tr>
            <td width="2%"></td>
            <td colspan="5">
                <asp:CheckBox ID="ChkClosed" runat="server" Text="Claim For Closed Loans" 
                    BackColor="#99CCFF" ForeColor="White" /></td>
            <td width="2%"></td>
        </tr>
        <tr>
            <td ></td>
            <td ><label>Loan Date From</label></td>
            <td >
                <asp:TextBox ID="TxtLoanDtFr" runat="server" Width="97%"></asp:TextBox>            
            </td>
            <td></td>
            <td><label>Loan Date To</label></td>
            <td>
                <asp:TextBox ID="TxtLoanDtTo" runat="server" Width="97%"></asp:TextBox>            
            </td>
            <td></td>
        </tr>
        <tr>
            <td width="2%"></td>
            <td width="24%"></td>
            <td width="22%"></td>
            <td width="2%"></td>
            <td width="24%"></td>
            <td width="24%">&nbsp;&nbsp;</td>
            <td width="2%"></td>
        </tr>
        <tr>
            <td width="2%"></td>
            <td colspan="5">
                <asp:CheckBox ID="ChkLoanAmt" runat="server" Text="Loan Amount Between" 
                    BackColor="#99CCFF" /></td>
            <td width="2%"></td>
        </tr>
  
        <tr>
            <td ></td>
            <td ><label>Loan Amt From</label></td>
            <td >
                <asp:TextBox ID="TxtLnAmtFr" runat="server" Width="97%"></asp:TextBox>            
            </td>
            <td></td>
            <td><label>Loan Amt To</label></td>
            <td>
                <asp:TextBox ID="TxtLnAmtTo" runat="server" Width="97%"></asp:TextBox>            
            </td>
            <td></td>
        </tr>
        <tr>
            <td width="2%"></td>
            <td width="24%"></td>
            <td width="22%"></td>
            <td width="2%"></td>
            <td width="24%"></td>
            <td width="24%">&nbsp;&nbsp;</td>
            <td width="2%"></td>
        </tr>
        <tr>
            <td width="2%"></td>
            <td>
                <asp:CheckBox ID="ChkOutstnd" runat="server" Text="Outstanding As On " /></td>
            <td>
                <asp:TextBox ID="TxtOSDate" runat="server" WIDTH ="100%"></asp:TextBox>
            </td>
            <td></td>
            <td colspan="2">
                &nbsp;</td>
            <td width="2%"></td>
        </tr>
        <tr>
            <td width="2%"></td>
            <td width="24%"></td>
            <td width="22%"></td>
            <td width="2%"></td>
            <td width="24%"></td>
            <td width="24%"></td>
            <td width="2%"></td>
        </tr>
        <tr>
            <td width="2%"></td>
            <td width="24%"></td>
            <td width="22%"></td>
            <td width="2%"></td>
            <td width="24%"></td>
            <td width="24%"></td>
            <td width="2%"></td>
        </tr>
        <tr>
            <td width="2%"></td>
            <td colspan="5" align="center">
                <asp:ImageButton ID="CmdShow" runat="server" Height="32px" 
                              ImageUrl="~/Images/Show.png" Width="96px" />
                <asp:ImageButton ID="CmdClose" runat="server" Height="32px" 
                              ImageUrl="~/images/Close.png" Width="96px" 
                    PostBackUrl="~/Main.aspx" />
            </td>
            <td width="2%"></td>
        </tr>
    </table>
    
    </div>

     <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
         AssociatedUpdatePanelID="UpdatePanel1">
         <ProgressTemplate>
             <div ID="blur">
                 <asp:Panel ID="Panel1" runat="server">
                 </asp:Panel>
             </div>
             <div ID="progress">
                 <img alt="progress" src="Images/progress.gif" /> Please Wait...
             </div>
         </ProgressTemplate>
     </asp:UpdateProgress>
<br />
</div> 
</ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>
