<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="Budget.aspx.vb" Inherits="SMARTCORE_UI.Budget" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
 <script language="javascript" type="text/javascript">
     function onlyDotsAndNumbers(event) {
         var charCode = (event.which) ? event.which : event.keyCode
         if (charCode == 46) {
             return true;
         }
         if (charCode > 31 && (charCode < 48 || charCode > 57))
             return false;

         return true;
     }
</script>
     <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;margin-top:5%; font-family: Tahoma" >                
    
    <legend>BUDGET</legend>  
    <table width="100%">
    <tr>
    <td width="2%"></td>
    <td width="48%">
        <label>
        Branch</label></td>
    <td width="48%">
        <asp:DropDownList ID="CmbBranch" runat="server" Width="100%" 
            AutoPostBack="True">
        </asp:DropDownList>
        </td>
    <td width="2%"></td>
    </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%">
                <label>A/C Head</label></td>
            <td width="48%">
                <asp:DropDownList ID="CmbAcHead" runat="server" Width="100%" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%">
                <label>Financial Year</label></td>
            <td width="48%">
                <asp:DropDownList ID="CmbYear" runat="server" Width="100%" AutoPostBack="True">                 
                </asp:DropDownList>
            </td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%">
                <label>Budget Amount</label></td>
            <td width="48%">
                <asp:TextBox ID="TxtBudgAmt" runat="server" AutoPostBack="True" Enabled="False"></asp:TextBox>
            </td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td colspan="2" style="width: 96%" width="48%">
                <table width="100%">
                <tr>
                <td width="24%"><label>April</label></td>
                <td width="24%">
                    <asp:TextBox ID="TxtApr" onkeypress="return onlyDotsAndNumbers(event)" style="text-align:right"  runat="server" AutoPostBack="True" ></asp:TextBox>
                    </td>
                <td width="2%"></td>
                <td width="24%">
                    <label>
                    May</label></td>
                <td width="24%">
                    <asp:TextBox ID="TxtMay" runat="server" onkeypress="return onlyDotsAndNumbers(event)" style="text-align:right"  AutoPostBack="True"></asp:TextBox>
                    </td>
                </tr>
                    <tr>
                        <td width="24%">
                            <label>
                            June</label></td>
                        <td width="24%">
                            <asp:TextBox ID="TxtJune" runat="server" onkeypress="return onlyDotsAndNumbers(event)" style="text-align:right"  AutoPostBack="True"></asp:TextBox>
                        </td>
                        <td width="2%">
                            &nbsp;</td>
                        <td width="24%">
                            <label>
                            July</label></td>
                        <td width="24%">
                            <asp:TextBox ID="TxtJuly" runat="server" onkeypress="return onlyDotsAndNumbers(event)" style="text-align:right"  AutoPostBack="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="24%">
                            <label>
                            August</label></td>
                        <td width="24%">
                            <asp:TextBox ID="TxtAug" runat="server" onkeypress="return onlyDotsAndNumbers(event)" style="text-align:right"  AutoPostBack="True"></asp:TextBox>
                        </td>
                        <td width="2%">
                            &nbsp;</td>
                        <td width="24%">
                            <label>
                            September</label></td>
                        <td width="24%">
                            <asp:TextBox ID="TxtSep" runat="server" onkeypress="return onlyDotsAndNumbers(event)" style="text-align:right"  AutoPostBack="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="24%">
                            <label>
                            October</label></td>
                        <td width="24%">
                            <asp:TextBox ID="TxtOct" runat="server" onkeypress="return onlyDotsAndNumbers(event)" style="text-align:right"  AutoPostBack="True"></asp:TextBox>
                        </td>
                        <td width="2%">
                            &nbsp;</td>
                        <td width="24%">
                            <label>
                            November</label></td>
                        <td width="24%">
                            <asp:TextBox ID="TxtNov" runat="server" onkeypress="return onlyDotsAndNumbers(event)" style="text-align:right"  AutoPostBack="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="24%">
                            <label>
                            December</label></td>
                        <td width="24%">
                            <asp:TextBox ID="TxtDec" runat="server" onkeypress="return onlyDotsAndNumbers(event)" style="text-align:right"  AutoPostBack="True"></asp:TextBox>
                        </td>
                        <td width="2%">
                            &nbsp;</td>
                        <td width="24%">
                            <label>
                            January</label></td>
                        <td width="24%">
                            <asp:TextBox ID="TxtJan" runat="server" onkeypress="return onlyDotsAndNumbers(event)" style="text-align:right"  AutoPostBack="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="24%">
                            <label>
                            February</label></td>
                        <td width="24%">
                            <asp:TextBox ID="TxtFeb" runat="server" onkeypress="return onlyDotsAndNumbers(event)" style="text-align:right"  AutoPostBack="True"></asp:TextBox>
                        </td>
                        <td width="2%">
                            &nbsp;</td>
                        <td width="24%">
                            <label>
                            March</label></td>
                        <td width="24%">
                            <asp:TextBox ID="TxtMar" runat="server" onkeypress="return onlyDotsAndNumbers(event)" style="text-align:right"  AutoPostBack="True"></asp:TextBox>
                        </td>
                    </tr>
                </table></td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%" align="center" colspan="2" style="width: 50%">
                <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" />
                &nbsp;<asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />
            </td>
        </tr>
    </table>
    </div> 
</ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>
