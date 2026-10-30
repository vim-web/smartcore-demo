<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LockerStock.aspx.vb" Inherits="SMARTCORE_UI.LockerStock" MasterPageFile="~/SMARTCORE.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />   
<script type='text/javascript' src="Scripts/Common.js"></script> 
<script type='text/javascript'>

    function ValidateSave() {
        try {
            var Clt = document.getElementById('<%=TxtLockerType.ClientID%>');
            if (Clt.value == "") {
                alertify.error('Locker Type Not Entered, Please Check');
                Clt.focus();
                return false;
            }

            var Clt = document.getElementById('<%=TxtManufacturer.ClientID%>');
            if (Clt.value == "") {
                alertify.error('Manufacturer Not Entered, Please Check');
                Clt.focus();
                return false;
            }

            var Clt = document.getElementById('<%=TxtInstalatnDate.ClientID%>');
            if (Clt.value == "") {
                alertify.error('Instalation Date Not Entered, Please Check');
                Clt.focus();
                return false;
            }

            var Clt = document.getElementById('<%=TxtNoOfLockr.ClientID%>');
            if (parseFloat(Clt.value) == 0 || Clt.value == "") {
                alertify.error('No Of Locker Not Entered, Please Check');
                Clt.focus();
                return false;
            }

            var Clt = document.getElementById('<%=TxtWidth.ClientID%>');
            if (parseFloat(Clt.value) == 0 || Clt.value == "") {
                alertify.error('Width Not Entered, Please Check');
                Clt.focus();
                return false;
            }

            var Clt = document.getElementById('<%=TxtHeight.ClientID%>');
            if (parseFloat(Clt.value) == 0 || Clt.value == "") {
                alertify.error('Height Not Entered, Please Check');
                Clt.focus();
                return false;
            }

            var Clt = document.getElementById('<%=TxtDepth.ClientID%>');
            if (parseFloat(Clt.value) == 0 || Clt.value == "") {
                alertify.error('Depth Not Entered, Please Check');
                Clt.focus();
                return false;
            }

            var Clt = document.getElementById('<%=TxtFrom.ClientID%>');
            if (parseFloat(Clt.value) == 0 || Clt.value == "") {
                alertify.error('Locker No From Not Entered, Please Check');
                Clt.focus();
                return false;
            }

            var Clt = document.getElementById('<%=TxtTo.ClientID%>');
            if (parseFloat(Clt.value) == 0 || Clt.value == "") {
                alertify.error('Locker No To Not Entered, Please Check');
                Clt.focus();
                return false;
            }

            var ToNo = document.getElementById('<%=TxtTo.ClientID%>');
            var FrNo = document.getElementById('<%=TxtFrom.ClientID%>');
            if (parseFloat(FrNo.value) > parseFloat(ToNo.value)) {
                alertify.error('Locker To Number Must be Greater Than or Equal To From Number, Please Check');
                FrNo.focus();
                return false;
            }

            return true;

        }
        catch (e) {
            alertify.error('An error has occurred: ' + e.message);
            return false;
        }
    }

</script>

        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:10%; margin-right:10%;">
     <table width="100%">
<tr>
<td width="2%" colspan="2" style="width: 32%"><legend>Locker Stock</legend></td>
<td width="48%">
    &nbsp;</td>
<td width="2%">&nbsp;</td>
</tr>
         <tr>
             <td width="2%">
             </td>
             <td width="30%">
                 <label>
                 Branch</label></td>
             <td width="48%">
                 <table width="100%">
                     <tr>
                         <td width="30%">
                             <asp:TextBox ID="TxtBranch" runat="server" AutoPostBack="True" Width="95%"></asp:TextBox>
                         </td>
                         <td width="100%">
                             <asp:DropDownList ID="CmbBranch" runat="server" AutoPostBack="True" 
                                 Width="100%">
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
<td><label>Locker Code</label></td>
<td>
    <asp:TextBox ID="TxtLockerCode" runat="server" Width="100%" AutoPostBack="True" onkeypress="return isNumberKey(event);"></asp:TextBox>
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td></td>
<td><label>Locker Type</label></td>
<td>
    <asp:TextBox ID="TxtLockerType" runat="server" Width="100%"></asp:TextBox>
    </td>
<td></td>
</tr>
<tr>
<td>&nbsp;</td>
<td><label>Manufacturer</label>&nbsp;</td>
<td>
    <asp:TextBox ID="TxtManufacturer" runat="server" Width="100%"></asp:TextBox>
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td><label>Instalation Date</label>&nbsp;</td>
<td>
    <asp:TextBox ID="TxtInstalatnDate" runat="server" Width="100%"></asp:TextBox>
    <asp:CalendarExtender ID="TxtInstalatnDate_CalendarExtender" runat="server" 
        TargetControlID="TxtInstalatnDate" Format="dd/MM/yyyy">
    </asp:CalendarExtender>
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td><label>No of Locker</label></td>
<td>
    <asp:TextBox ID="TxtNoOfLockr" runat="server" onkeypress="return isNumberKey(event);" Width="100%" ></asp:TextBox>
    </td>
<td>&nbsp;</td>
</tr>
         <tr>
             <td>
                 &nbsp;</td>
             <td>
                 <label>Locker Size</label></td>
             <td>
                 <table width="100%">
<tr>
<td width="30%">
    <label>
    Width</label></td>
<td width="30%">
    <label>
    Height</label></td>
<td width="30%">
    <label>
    Depth</label></td>
</tr>
    <tr>
        <td width="20%">
            <asp:TextBox ID="TxtWidth" runat="server" Width="95%" onkeypress="return isFloatKey(event);"></asp:TextBox>
        </td>
        <td width="20%">
            <asp:TextBox ID="TxtHeight" runat="server" Width="95%" onkeypress="return isFloatKey(event);"></asp:TextBox>
        </td>
        <td width="20%">
            <asp:TextBox ID="TxtDepth" runat="server" Width="95%" onkeypress="return isFloatKey(event);"></asp:TextBox>
        </td>
    </tr>
</table>
                 </td>
             <td>
                 &nbsp;</td>
         </tr>
         <tr>
             <td>
                 &nbsp;</td>
             <td>
                 <label>Locker No</label></td>
             <td>
                 <table width="100%">
    <tr>
        
        <td width="30%">
            <label>
            From</label></td>
        <td width="30%">
            <label>
            To</label></td>
    </tr>
    <tr>
        <td width="30%">
            <asp:TextBox ID="TxtFrom" runat="server" onkeypress="return isNumberKey(event);" Width="95%"></asp:TextBox>
        </td>
        <td width="30%">
            <asp:TextBox ID="TxtTo" runat="server" onkeypress="return isNumberKey(event);" Width="95%"></asp:TextBox>
        </td>
    </tr>
    </table>
                 </td>
             <td>
                 &nbsp;</td>
         </tr>
<tr>
<td>&nbsp;</td>
<td colspan="2">&nbsp;</td>
<td></td>
</tr>
<tr>
<td>&nbsp;</td>
<td align="center" colspan="2">
    <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" OnClientClick="return ValidateSave();"/>
    &nbsp;<asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" />
    &nbsp;<asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />
    </td>
<td>&nbsp;</td>
</tr>
</table>
    </div>
    </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>
