<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ExternalLien.aspx.vb" Inherits="SMARTCORE_UI.ExternalLien" MasterPageFile="~/SMARTCORE.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
        </asp:ToolkitScriptManager>
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
<script type='text/javascript' src="Scripts/Common.js"></script> 
<script type='text/javascript'>

    function ValidateSave() {
        var Ctl = document.getElementById('<%=HdnAlert.ClientID%>');
        if (Ctl.value != "") {
            alert(Ctl.value);
            return false;
        }

        var Ctl = document.getElementById('<%=TxtShortCode.ClientID%>');
        if (parseFloat(Ctl.value) == 0 || Ctl.value == "") {
            alert('Account No Not Entered, Please Check');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtLienWith.ClientID%>');
        if (Ctl.value == "") {
            alert('Lien With Not Entered, Please Check');
            Ctl.focus();
            return false;
        }
        return true;
    }

</script>


        <br />
        
    <div class="gradientBoxesWithOuterShadows" style="width: 45%; margin-left:10%; margin-right:10%;">
    <table style="width: 100%">
       <tr>
           <td colspan="4" style="width: 4%">
               <legend>External Lien</legend></td>
       </tr>
        </td>
        </tr>
        <tr>
            <td width="2%">
            </td>
            <td>
                <label>
                Branch</label>
            </td>
            <td width="48%">
                <table width="100%">
                    <tr>
                        <td width="30%">
                            <asp:TextBox ID="TxtBranch" runat="server" AutoPostBack="True" Width="98%"></asp:TextBox>
                        </td>
                        <td width="70%">
                            <asp:DropDownList ID="CmbBranch" runat="server" AutoPostBack="True" Width="100%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>                       
            </td>
            <td width="2%">
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Scheme</label>
            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td width="30%">
                            <asp:TextBox ID="TxtScheme" runat="server" AutoPostBack="True" Width="98%"></asp:TextBox>
                        </td>
                        <td width="70%">
                            <asp:DropDownList ID="CmbScheme" runat="server" AutoPostBack="True" 
                                Width="100%">
                            </asp:DropDownList>
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
                <label>
                Account No</label>
            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td width="30%">
                            <asp:TextBox ID="TxtShortCode" runat="server" AutoPostBack="True" Width="98%"></asp:TextBox>
                        </td>
                        <td width="70%">
                            <asp:TextBox ID="TxtAccNo" runat="server" AutoPostBack="True" Width="100%"></asp:TextBox>
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
                <label>
                Name</label></td>
            <td>
                <asp:TextBox ID="TxtName" runat="server" Width="100%"></asp:TextBox>
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
                <asp:TextBox ID="TxtAddress" runat="server" Height="72px" TextMode="MultiLine" 
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
                Mobile No</label></td>
            <td>
                <asp:TextBox ID="TxtMobNo" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Balance</label></td>
            <td>
                <label>
                <asp:TextBox ID="TxtBalance" runat="server" onkeypress="return isNumberKey(event);" 
                     Width="100%"></asp:TextBox>
                </label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Lien With</label>
            </td>
            <td>
                <asp:TextBox ID="TxtLienWith" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Remarks</label>
            </td>
            <td>
                <asp:TextBox ID="TxtRemark" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:HiddenField ID="AccID" runat="server" />
                <asp:HiddenField ID="HdnAlert" runat="server" />
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td align="center" colspan="2">
                <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" OnClientClick="return ValidateSave();"/>
                &nbsp;<asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" />
                &nbsp;<asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        </caption>
        </legend>
</table>
    </div>
     </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>
