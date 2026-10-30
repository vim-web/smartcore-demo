<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="LoanStanding.aspx.vb" Inherits="SMARTCORE_UI.LoanStanding" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<script type='text/javascript'>

    function ValidateSave() {



        var Ctl = document.getElementById('<%=TxtLoanAccNo.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Loan Account Number Not Entered, Please Check');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtFrmAccNo.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Deposit Account Number Not Entered, Please Check');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtAmount.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Amount Not Entered, Please Check');
            Ctl.focus();
            return false;
        }




        return true;
    }

</script>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    

        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 44%; margin-left:28%; margin-right:28%;">
     <legend>Loan Standing</legend>
     <table width="100%">
     <tr>
     <td width="2%"></td>
     <td width="48%"><legend>Standing To</legend></td>
     <td width="48%"></td>
     <td width="48%"></td>
     </tr>
     <tr>
     <td width="2%"></td>
     <td width="48%"><label>Branch</label></td>
     <td width="48%">
         <asp:DropDownList ID="CmbToBranch" runat="server" Width="100%" 
             AutoPostBack="True">
         </asp:DropDownList>
         </td>
     <td width="48%"></td>
     </tr>
     <tr>
     <td width="2%"></td>
     <td width="48%"><label>Scheme</label></td>
     <td width="48%">
         <asp:TextBox ID="TxtLoanScheme" runat="server" Width="23%" AutoPostBack="True"></asp:TextBox>
         &nbsp;
         <asp:DropDownList ID="CmbLoanScheme" runat="server" Width="68%" 
             AutoPostBack="True">
         </asp:DropDownList>
         </td>
     <td width="48%"></td>
     </tr>
     <tr>
     <td width="2%"></td>
     <td width="48%"><label>Account No</label></td>
     <td width="48%">
         <asp:TextBox ID="TxtLoanShCode" runat="server" Width="23%" AutoPostBack="True"></asp:TextBox>
         &nbsp;<asp:TextBox ID="TxtLoanAccNo" runat="server" Width="65%" 
             AutoPostBack="True"></asp:TextBox>
         </td>
     <td width="48%"></td>
     </tr>
     <tr>
     <td width="2%"></td>
     <td width="48%"><label>Cust No</label></td>
     <td width="48%">
         <asp:TextBox ID="TxtToCustNo" runat="server"></asp:TextBox>
         </td>
     <td width="48%"></td>
     <tr>
     <td width="2%"></td>
     <td width="48%"><label>Name</label></td>
     <td width="48%">
         <asp:TextBox ID="TxtToName" runat="server"></asp:TextBox>
         </td>
     <td width="48%"></td>
     </tr>
     <tr>
     <td width="2%"></td>
     <td width="48%"><label>Address</label></td>
     <td width="48%">
         <asp:TextBox ID="TxtToAddress" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
         </td>
     <td width="48%"></td>
     </tr>
     <tr>
     <td width="2%"></td>
     <td width="48%"><legend>Standing From</legend></td>
     <td width="48%"></td>
     <td width="48%"></td>
     </tr>
     <tr>
     <td width="2%"></td>
     <td width="48%"><label>Branch</label></td>
     <td width="48%">
         <asp:DropDownList ID="CmbFrmBranch" runat="server" Width="100%" 
             AutoPostBack="True">
         </asp:DropDownList>
         </td>
     <td width="48%"></td>
     </tr>
     <tr>
     <td width="2%"></td>
     <td width="48%"><label>Scheme</label></td>
     <td width="48%">
         &nbsp;<asp:TextBox ID="TxtDepScheme" runat="server" AutoPostBack="True" Width="23%"></asp:TextBox>
         &nbsp;<asp:DropDownList ID="CmbDepScheme" runat="server" Width="68%" 
             AutoPostBack="True">
         </asp:DropDownList>
         </td>
     <td width="48%"></td>
     </tr>
     <tr>
     <td width="2%"></td>
     <td width="48%"><label>Account No</label></td>
     <td width="48%">
         <asp:TextBox ID="TxtFrmShCode" runat="server" AutoPostBack="True" Width="23%"></asp:TextBox>
         &nbsp;<asp:TextBox ID="TxtFrmAccNo" runat="server" AutoPostBack="True" 
             Width="65%"></asp:TextBox>
         </td>
     <td width="48%"></td>
     </tr>
     <tr>
     <td width="2%"></td>
     <td width="48%"><label>Cust No</label></td>
     <td width="48%">
         <asp:TextBox ID="TxtFrmCustNo" runat="server"></asp:TextBox>
         </td>
     <td width="48%"></td>
     </tr>
     <tr>
     <td width="2%"></td>
     <td width="48%"><label>Name</label></td>
     <td width="48%">
         <asp:TextBox ID="TxtFrmName" runat="server"></asp:TextBox>
         </td>
     <td width="48%"></td>
     </tr>
     <tr>
     <td width="2%"></td>
     <td width="48%"><label>Address</label></td>
     <td width="48%">
         <asp:TextBox ID="TxtFrmAddress" runat="server"></asp:TextBox>
         </td>
     <td width="48%"></td>
     </tr>
     <tr>
     <td width="2%"></td>
     <td width="48%"><label>Amount</label></td>
     <td width="48%">
         <asp:TextBox ID="TxtAmount" runat="server" Width="45%"></asp:TextBox>
         &nbsp;<asp:DropDownList ID="CmbType" runat="server" Width="47%">
         </asp:DropDownList>
         </td>
     <td width="48%"></td>
     </tr>
     <tr>
     <td width="2%"></td>
     <td width="48%"><label>Status and Execution Day</label></td>
     <td width="48%">
         <asp:DropDownList ID="CmbStatus" runat="server" Width="45%">
             <asp:ListItem>Active / Current Posting</asp:ListItem>
             <asp:ListItem>Disable / Inactive</asp:ListItem>
             <asp:ListItem>Next Posting</asp:ListItem>
         </asp:DropDownList>
         &nbsp;<asp:TextBox ID="TxtExeDay" runat="server" Width="45%"></asp:TextBox>
         </td>
     <td width="48%"></td>
     </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td width="48%">
                 <label>
                 Amount Type</label></td>
             <td width="48%">
                 <asp:DropDownList ID="CmbAmountType" runat="server" Width="100%">
                 </asp:DropDownList>
             </td>
             <td width="48%">
                 &nbsp;</td>
         </tr>
     <tr>
     <td width="2%"></td>
     <td width="48%" align="center" colspan="2" style="width: 96%">
         <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" OnClientClick="return ValidateSave();"
             meta:resourcekey="CmdSaveResource1" TabIndex="56" />
         <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" />
         <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />
         </td>
     <td width="48%"></td>
     </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td align="center" colspan="2" style="width: 96%" width="48%">
                 <asp:HiddenField ID="AccIDFrom" runat="server" />
                 <asp:HiddenField ID="AccIdTo" runat="server" />
             </td>
             <td width="48%">
                 &nbsp;</td>
         </tr>
     <tr>
     <td width="2%"></td>
     <td width="48%"></td>
     <td width="48%"></td>
     <td width="48%"></td>
     </tr>
     </tr>
     </table>
     </div>
    </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    
    </asp:Content>
