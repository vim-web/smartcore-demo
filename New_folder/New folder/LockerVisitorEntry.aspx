<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LockerVisitorEntry.aspx.vb" Inherits="SMARTCORE_UI.LockerVisitorEntry" MasterPageFile="~/SMARTCORE.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<%--<script type='text/javascript'>

    function ValidateSave() {

//        var Ctl = document.getElementById('<%=TxtShortCode.ClientID%>');
//        if (Ctl.value == "") {
//            alert('Locker Type Not Entered, Please Check');
//            Ctl.focus();
//            return false;
//        }


        var Ctl = document.getElementById('<%=TxtAccNo.ClientID%>');
        if (Ctl.value == "") {
            alert('Account Number Not Entered, Please Check');
            Ctl.focus();
            return false;
        }


        var Ctl = document.getElementById('<%=TxtCheckin.ClientID%>');
        if (Ctl.value == "") {
            alert('CheckIn Not Entered, Please Check');
            Ctl.focus();
            return false;
        }


        var Ctl = document.getElementById('<%=TxtCheckOut.ClientID%>');
        if (Ctl.value == "") {
            alert('CheckOut Not Entered, Please Check');
            Ctl.focus();
            return false;
        }
        
        

        var Ctl = document.getElementById('<%=TxtRemark.ClientID%>');
        if (Ctl.value == "") {
            alert('Remark Not Entered, Please Check');
            Ctl.focus();
            return false;
        }
        return true;
    }

</script>--%>
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    

        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        
     <div class="gradientBoxesWithOuterShadows" style="width: 50%; margin-left:25%; margin-right:25%;">
     <legend>LOCKER VISITOR ENTRY</legend>
    <table width="100%">
<tr>
<td width="2%"></td>
<td width="48%"><label>Branch</label></td>
<td width="48%">
    <table width="100%">
    <tr>
    <td width="30%">
    <asp:TextBox ID="TxtBranch" Width="95%" runat="server" AutoPostBack="True"></asp:TextBox>
    </td>
    <td width="70%">
    <asp:DropDownList ID="CmbBranch" Width="100%" runat="server" AutoPostBack="True">
    </asp:DropDownList>
    </td>
    </tr>
    </table>
    </td>
<td width="2%"></td>
</tr>
<tr>
<td></td>
<td><label>Scheme</label></td>
<td>
    <table width="100%">
    <tr>
    <td width="30%">
    <asp:TextBox ID="TxtScheme" Width="95%" runat="server" AutoPostBack="True"></asp:TextBox>
    </td>
    <td width="70%">
    <asp:DropDownList ID="CmbScheme" Width="100%" runat="server" AutoPostBack="True">
    </asp:DropDownList>
    </td>
    </tr>
    </table>
    </td>
<td></td>
</tr>

<%--<tr>
<td>&nbsp;</td>
<td>
    <label>
    Account Number</label></td>
<td>
<table width="100%">
<tr>
<td width="30%">
    <asp:TextBox ID="TxtSchCode" runat="server" AutoPostBack="True" Width="100%"></asp:TextBox>
</td>
<td width="70%">
<asp:TextBox ID="TxtAccNo" Width="100%" runat="server" AutoPostBack="True"></asp:TextBox>
</td>
</tr>
</table>   
    </td>
<td>&nbsp;</td>
</tr>
         <tr>
             <td>
                 &nbsp;</td>
             <td>
                 <label>
                 Name</label></td>
             <td>
                 <asp:TextBox ID="TxtAcHoldrName" runat="server" AutoPostBack="True" 
                    Width="100%"></asp:TextBox>
             </td>
             <td>
                 &nbsp;</td>
         </tr>
         <tr>
             <td>
                 &nbsp;</td>
             <td>
                 <label>
                 Address</label></td>
             <td>
                 <asp:TextBox ID="TxtAcHoldrAddr" runat="server" TextMode="MultiLine" 
                     Width="100%"></asp:TextBox>
             </td>
             <td>
                 &nbsp;</td>
         </tr>--%>


<tr>
<td>&nbsp;</td>
<td><label>Account No</label>&nbsp;</td>
<td>
   
    
        <asp:TextBox ID="TxtAccNo" runat="server" AutoPostBack="True" Width="100%"></asp:TextBox>
       </td>
<td>&nbsp;</td>
</tr>
<%--<tr>
<td>&nbsp;</td>
<td><label>Mode Of Operation</label>&nbsp;</td>
<td>
    <asp:DropDownList ID="CmbModOfOprn" runat="server" Width="100%">
        <asp:ListItem>Direct</asp:ListItem>
        <asp:ListItem>Indirect</asp:ListItem>
    </asp:DropDownList>
    </td>
<td>&nbsp;</td>
</tr>--%>
<%--<tr>
<td>&nbsp;</td>
<td><label>Rent Pending</label>&nbsp;</td>
<td>
    <asp:TextBox ID="TxtRentPending" runat="server" Width="100%"></asp:TextBox>
    </td>
<td>&nbsp;</td>
</tr>--%>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Visitor</label>&nbsp;&nbsp;</td>
            <td>
                <asp:CheckBoxList ID="ChkVisitor" runat="server" AutoPostBack="True" 
                    Width="100%">
                </asp:CheckBoxList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Address
                </label>
            </td>
            <td>
                <asp:TextBox ID="TxtAddress" runat="server" Enabled="False" 
                    TextMode="MultiLine" Width="100%"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
<tr>
<td>&nbsp;</td>
<td><label>Check In</label>&nbsp;</td>
<td>
    <asp:TextBox ID="TxtCheckIn" runat="server" Width="100%"></asp:TextBox>
    
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td><label>Check Out</label>&nbsp;</td>
<td>
    <asp:TextBox ID="TxtCheckOut" runat="server" Width="100%"></asp:TextBox>
    
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td><label>Remarks</label>&nbsp;</td>
<td>
    <asp:TextBox ID="TxtRemark" runat="server" Width="100%"></asp:TextBox>
    
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td>&nbsp;</td>
<td>
    &nbsp;</td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td align="center" colspan="2">
    <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" onclientclick="return ValidateSave();" />
    &nbsp;<asp:ImageButton ID="CmdClear" runat="server" 
        ImageUrl="~/Images/Clear.png" />
    &nbsp;<asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />
    </td>
<td>&nbsp;</td>
</tr>
        <tr>
            <td>
                &nbsp;</td>
            <td align="center" colspan="2">
                <asp:HiddenField ID="Cust_Id" runat="server" />
                <asp:HiddenField ID="AccId" runat="server" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
</table>
    </div>
    </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>

