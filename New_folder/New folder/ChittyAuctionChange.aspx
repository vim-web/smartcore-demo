<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="ChittyAuctionChange.aspx.vb" Inherits="SMARTCORE_UI.ChittyAuctionChange" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 

    <script type='text/javascript'>
        //    $(function() {
        //    var dropDownList1 = $('select[id$=' + '<%=CmbScheme.ClientID%>' + ']');
        //        dropDownList1.removeAttr('onchange');
        //        dropDownList1.change(function(e) {
        //            if (this.value != 0) {
        //                setTimeout('__doPostBack(\'CmbScheme\',\'\')', 0);
        //                alert('hai josu');
        //            }
        //        });
        //    });

        function ValidateSave() {
            var Ctl = document.getElementById('<%=TxtNewAccNo.ClientID%>');

            if (Ctl.value != "") {
                alertify.error("New Account No not entered");
                return false;
            }

            var Ctl = document.getElementById('<%=TxtAccNo.ClientID%>');

            if (Ctl.value != "") {
                alertify.error(" Account No not entered");
                return false;
            }

            return true;
        }
        

</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
 
     <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;margin-top:5%; font-family: Tahoma" >                
    
    <legend>Chitty Auction Change</legend>  
    <table width="100%">
    <tr>
<td width="2%">&nbsp;</td>
<td width="48%"><label>Branch</label></td>
<td width="48%">
    <asp:DropDownList ID="CmbBranch" runat="server" Width="100%" 
        AutoPostBack="True">
    </asp:DropDownList>
    </td>
<td width="2%">&nbsp;</td>
</tr>
    <tr>
        <td width="2%">
            &nbsp;</td>
        <td width="48%">
            <label>Scheme</label></td>
        <td width="48%">
            <asp:DropDownList ID="CmbScheme" runat="server" Width="100%" 
                AutoPostBack="True">
            </asp:DropDownList>
        </td>
        <td width="2%">
            &nbsp;</td>
    </tr>
<tr>
<td width="2%"></td>
<td width="48%"><label>Chitty Group</label></td>
<td width="48%">
    <asp:DropDownList ID="CmbChityGroup" runat="server" Width="100%" 
        AutoPostBack="True">
    </asp:DropDownList>
    </td>
<td width="2%"></td>
</tr>
<tr>
<td></td>
<td><label>Auction No</label></td>
<td>
    <asp:DropDownList ID="CmbAuctionNo" runat="server" AutoPostBack="True" 
        Width="100%">
    </asp:DropDownList>
    </td>
<td></td>
</tr>
<tr>
<td>&nbsp;</td>
<td><label>Auction Amount</label>&nbsp;</td>
<td>
    <asp:TextBox ID="TxtAuctnAmt" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td><label>Bank Charge</label>&nbsp;</td>
<td>
    <asp:TextBox ID="TxtBankCharge" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td><label>Discount Amount</label>&nbsp;</td>
<td>
    <asp:TextBox ID="TxtDiscntAmt" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td>&nbsp;</td>
<td>&nbsp;</td>
<td>&nbsp;</td>
</tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Account No</label>&nbsp;</td>
            <td>
                <asp:TextBox ID="TxtAccNo" runat="server" AutoPostBack="True"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
<tr>
<td>&nbsp;</td>
<td><label>New Account No</label>&nbsp;</td>
<td>
    <asp:TextBox ID="TxtNewAccNo" runat="server" Width="100%" AutoPostBack="True"></asp:TextBox>
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td><label>Name</label>&nbsp;</td>
<td>
    <asp:TextBox ID="TxtName" runat="server" ReadOnly="true" Width="100%" 
        AutoPostBack="True"></asp:TextBox>
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td><label>Address</label>&nbsp;</td>
<td>
    <asp:TextBox ID="TxtAddress" runat="server" ReadOnly="true" Width="100%" 
        TextMode="MultiLine" AutoPostBack="True"></asp:TextBox>
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td align="center" colspan="2">
    <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" />
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
                <asp:HiddenField ID="HdnAccId" runat="server" />
                <asp:HiddenField ID="HdnNewAccId" runat="server" />
                </td>
            <td>
                &nbsp;</td>
        </tr>
</table>

    </div> 
</ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>

