<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="CustDetRpt.aspx.vb" Inherits="SMARTCORE_UI.CustDetRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
 
     <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;margin-top:5%; font-family: Tahoma" >                
    
    <legend>Customer Details Report</legend> 
    <table width="100%">
    <tr>
    <td width="2%">&nbsp;</td>
    <td width="48%"><label>Branch</label></td>
    <td width="48%" style="margin-left: 40px">
        <asp:DropDownList ID="DrpBranch" runat="server" Width="100%">
        </asp:DropDownList>
        </td>
    <td width="2%">
        &nbsp;</td>
    </tr>
        <tr>
            <td width="2%">
            </td>
            <td width="48%">
                <label>
                Customer Type</label></td>
            <td style="margin-left: 40px" width="48%">
                <asp:DropDownList ID="CmbCustType" runat="server" AutoPostBack="True" 
                    Enabled="False" Width="100%">
                </asp:DropDownList>
            </td>
            <td width="2%">
                <asp:CheckBox ID="ChkType" runat="server" AutoPostBack="True" />
            </td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%">
                <label>Category</label></td>
            <td width="48%">
                <asp:DropDownList ID="CmbCategory" runat="server" Width="100%" 
                    AutoPostBack="True" Enabled="False">
                </asp:DropDownList>
            </td>
            <td width="2%">
                <asp:CheckBox ID="ChkCategory" runat="server" AutoPostBack="True" />
            </td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%">
                <label>Gender</label></td>
            <td width="48%">
                <asp:DropDownList ID="CmbGender" runat="server" Width="100%" 
                    AutoPostBack="True" Enabled="False">
                </asp:DropDownList>
            </td>
            <td width="2%">
                <asp:CheckBox ID="ChkGender" runat="server" AutoPostBack="True" />
            </td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%">
                <label>Religion</label></td>
            <td width="48%">
                <asp:DropDownList ID="CmbReligion" runat="server" Width="100%" 
                    AutoPostBack="True" Enabled="False">
                </asp:DropDownList>
            </td>
            <td width="2%">
                <asp:CheckBox ID="ChkReligion" runat="server" AutoPostBack="True" />
            </td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%">
                <label>
                Caste</label></td>
            <td width="48%">
                <asp:DropDownList ID="CmbCaste" runat="server" Width="100%" AutoPostBack="True" 
                    Enabled="False">
                </asp:DropDownList>
            </td>
            <td width="2%">
                <asp:CheckBox ID="ChkCaste" runat="server" AutoPostBack="True" />
            </td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%">
                <label>
                Location</label></td>
            <td width="48%">
                <asp:DropDownList ID="CmbLocn" runat="server" Width="100%" AutoPostBack="True" 
                    Enabled="False">
                </asp:DropDownList>
            </td>
            <td width="2%">
                <asp:CheckBox ID="ChkLocn" runat="server" AutoPostBack="True" />
            </td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%">
                <label>
                Occupation</label></td>
            <td width="48%">
                <asp:DropDownList ID="CmbOccupn" runat="server" Width="100%" 
                    AutoPostBack="True" Enabled="False">
                </asp:DropDownList>
            </td>
            <td width="2%">
                <asp:CheckBox ID="ChkOccupn" runat="server" AutoPostBack="True" />
            </td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%">
                <label>
                Education</label></td>
            <td width="48%">
                <asp:DropDownList ID="CmbEducn" runat="server" Width="100%" AutoPostBack="True" 
                    Enabled="False">
                </asp:DropDownList>
            </td>
            <td width="2%">
                <asp:CheckBox ID="ChkEducn" runat="server" AutoPostBack="True" />
            </td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%">
                <label>
                Marital Status</label></td>
            <td width="48%">
                <asp:DropDownList ID="CmbMaritalStatus" runat="server" Width="100%" 
                    AutoPostBack="True" Enabled="False">
                </asp:DropDownList>
            </td>
            <td width="2%">
                <asp:CheckBox ID="ChkMarStatus" runat="server" AutoPostBack="True" />
            </td>
        </tr>
          <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%">
                <label>
                Panchayat</label></td>
            <td width="48%">
                <asp:TextBox ID="TxtPanchayt" runat="server" Width="100%" 
                     Enabled="False">
                </asp:TextBox>
            </td>
            <td width="2%">
                <asp:CheckBox ID="ChkPanchyt" runat="server" AutoPostBack="True" />
            </td>
        </tr>
         <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%">
                <label>
                Ward</label></td>
            <td width="48%">
                 <asp:TextBox ID="TxtCustWard" runat="server" Width="100%" 
                     Enabled="False">
                </asp:TextBox>
            </td>
            <td width="2%">
                <asp:CheckBox ID="Chkward" runat="server" AutoPostBack="True" />
            </td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td align="center" colspan="2" style="width: 96%" width="48%">
                <asp:ImageButton ID="cmdshow" runat="server" ImageUrl="~/Images/Show.png" />
                &nbsp;<asp:ImageButton ID="CmdClose" runat="server" 
                    ImageUrl="~/Images/CloseBtn.png" />
            </td>
            <td width="2%">
                &nbsp;</td>
        </tr>
    </table>

    </div> 
</ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>

