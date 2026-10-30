<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="SkipInterest.aspx.vb" Inherits="SMARTCORE_UI.SkipInterest" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<script type='text/javascript'>

    function ValidateSave() {



        var Ctl = document.getElementById('<%=TxtAccNo.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Account Number Not Entered, Please Check');
            Ctl.focus();
            return false;
        }


        var Ctl = document.getElementById('<%=TxtRcptno.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Receipt Number Not Entered, Please Check');
            Ctl.focus();
            return false;
        }


        var Ctl = document.getElementById('<%=TxtRcptDate.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Date Not Entered, Please Check');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtReason.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Reason Not Entered, Please Check');
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
     <div class="gradientBoxesWithOuterShadows" style="width: 100%; margin-left:0%; margin-right:0%;">
     <div class="gradientBoxesWithOuterShadows" style="width: 50%; margin-left:30%; margin-right:10%;">
     <table style="width: 100%">
<tr>
<td width="2%" colspan="2" style="width: 50%"><legend>Skip interest</legend></td>
<td width="48%">
    &nbsp;</td>
<td width="2%">&nbsp;</td>
</tr>
         <tr>
             <td width="2%">
             </td>
             <td width="48%">
                 <asp:Label ID="Label1" runat="server" Text="Branch"></asp:Label>
             </td>
             <td width="48%">
                 <table width="100%">
                     <tr>
                         <td width="30%">
                             <asp:TextBox ID="TxtBranch" runat="server" Width="95%" AutoPostBack="True"></asp:TextBox>
                         </td>
                         <td width="70%">
                             <asp:DropDownList ID="CmbBranch" runat="server" Width="100%" 
                                 AutoPostBack="True">
                             </asp:DropDownList>
                         </td>
                     </tr>
                 </table>
             </td>
             <td width="2%">
             </td>
         </tr>
<tr>
<td>&nbsp;</td>
<td>
    <asp:Label ID="Label3" runat="server" Text="Scheme"></asp:Label>
    </td>
<td>
<table width="100%">
<tr>
<td width="30%">
<asp:TextBox ID="TxtScheme" runat="server" Width="95%" AutoPostBack="True" 
        style="height: 22px"></asp:TextBox>
</td>
<td width="70%">
<asp:DropDownList ID="CmbScheme" runat="server" Width="100%" AutoPostBack="True">
    </asp:DropDownList>
</td>
</tr>
</table>    
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td>
    <asp:Label ID="Label5" runat="server" Text="Account No"></asp:Label>
    </td>
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
                 <asp:Label ID="Label10" runat="server" Text="Name"></asp:Label>
             </td>
             <td>
                 <asp:TextBox ID="TxtAcHoldrName" runat="server" AutoPostBack="True" 
                     BorderStyle="None" Width="100%"></asp:TextBox>
             </td>
             <td>
                 &nbsp;</td>
         </tr>
         <tr>
             <td>
                 &nbsp;</td>
             <td>
                 Address</td>
             <td>
                 <asp:TextBox ID="TxtAcHoldrAddr" runat="server" TextMode="MultiLine" 
                     Width="100%"></asp:TextBox>
             </td>
             <td>
                 &nbsp;</td>
         </tr>
<tr>
<td>&nbsp;</td>
<td>
    <asp:Label ID="Label6" runat="server" Text="Mode Of Receipt"></asp:Label>
    </td>
<td>
    <asp:DropDownList ID="CmbModOfRecpt" runat="server" Width="100%">
        <asp:ListItem>Direct</asp:ListItem>
        <asp:ListItem>Letter</asp:ListItem>
        <asp:ListItem>Mail</asp:ListItem>
    </asp:DropDownList>
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td>
    <asp:Label ID="Label7" runat="server" Text="Receipt No"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="TxtRcptno" Width="100%" runat="server"></asp:TextBox>
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td>
    <asp:Label ID="Label8" runat="server" Text="Receipt Date"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="TxtRcptDate" Width="100%" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="txtrcptdate_CalendarExtender" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtrcptdate">
    </asp:CalendarExtender>
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td>
    <asp:Label ID="Label9" runat="server" Text="Reason"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="TxtReason" Width="100%" runat="server"></asp:TextBox>
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td align="center" colspan="2">
    <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" 
        onclientclick="return ValidateSave();" />
    &nbsp;<asp:ImageButton ID="CmdClear" runat="server" 
        ImageUrl="~/Images/Clear.png" />
    &nbsp;<asp:ImageButton ID="CmdClose" runat="server" 
        ImageUrl="~/Images/Close.png" />
    </td>
<td>&nbsp;</td>
</tr>
</table>
 </div>
    </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>

