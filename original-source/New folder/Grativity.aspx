<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="Grativity.aspx.vb" Inherits="SMARTCORE_UI.Grativity" 
    title="Salary Report" %>

  <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />
   


    <style type="text/css">
        .style1
        {
            height: 18px;
        }
        .style2
        {
            height: 18px;
        }
        </style>
   


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
 
     <div class="gradientBoxesWithOuterShadows" 
        style="width: 69%; margin-left:20%; margin-right:20%;margin-top:5%; font-family: Tahoma" >                
    
    <legend>GRATUVITY </legend>  
        <table style="height: 317px; width: 99%">
            <tr>
                <td width="2%">
                </td>
                <td width="48%">
                    &nbsp;</td>
                <td width="48%">
                    &nbsp;</td>
                <td width="2%">
                </td>
            </tr>
            <tr>
                <td width="2%">
                </td>
                <td width="48%">
                    <label>
                    Employee Name</label>
                </td>
                <td width="48%">
                    <asp:DropDownList ID="CmbEmployeeName" runat="server" Width="100%" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td width="2%">
                </td>
            </tr>
            <tr>
                <td width="2%">
                </td>
                <td width="48%">
                    <label>
                    Basic Salary</label></td>
                <td width="48%">
                    <asp:TextBox ID="TxtBasic" runat="server" Width="100%" ReadOnly="True"></asp:TextBox>
                </td>
                <td width="2%">
                </td>
            </tr>
            <tr>
                <td width="2%">
                </td>
                <td width="48%">
                    <label>
                    Da</label>&nbsp;</label></td>
                <td width="48%">
                    <asp:TextBox ID="TxtDA" runat="server" Width="100%" ReadOnly="True"></asp:TextBox>
                </td>
                <td width="2%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="2%" class="style1">
                    </td>
                <td width="48%" class="style1">
                    <label>
                    Year in Service&nbsp;&nbsp;&nbsp; </label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                <td class="style2">
                <table width = "100%">
                <tr>
                    <td width = "25%">
                        <label>
                        <asp:TextBox ID="TxtYrs" runat="server" Height="25px" ReadOnly="True" 
                            Width="100%"></asp:TextBox>
                        </label>
                    </td>
                    <td width = "25%">
                        <label>
                        &nbsp; Years</label></td>
                    <td width = "25%">
                        <asp:TextBox ID="TxtMths" runat="server" Height="25px" ReadOnly="True" 
                            Width="100%"></asp:TextBox>
                    </td>
                    <td width = "25%">
                        <label>
                        &nbsp; Month</label></td>
                </tr>
                </table>
                 </td>
                <td width="2%" class="style1">
                    </td>
            </tr>
            <tr>
                <td width="2%">
                    &nbsp;</td>
                <td width="48%">
                    <label>
                    Gratuvity Entitlement</label>&nbsp;</td>
                <td width="48%">
                    <asp:TextBox ID="TxtGratuvity" runat="server" ReadOnly="True" Width="51%"></asp:TextBox>
                </td>
                <td width="2%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="2%">
                </td>
                <td align="center" colspan="2" style="width: 96%" width="48%">
                    &nbsp;</td>
                <td width="2%">
                </td>
            </tr>
            <tr>
                <td width="2%">
                    &nbsp;</td>
                <td align="center" colspan="2" style="width: 96%" width="48%">
                    &nbsp;</td>
                <td width="2%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="2%">
                    &nbsp;</td>
                <td width="48%">
                    &nbsp;</td>
                <td width="2%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="2%">
                    &nbsp;</td>
                <td align="center" colspan="2" style="width: 96%" width="48%">
                    &nbsp;</td>
                <td width="2%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="2%">
                    &nbsp;</td>
                <td align="center" colspan="2" style="width: 96%" width="48%">
                    &nbsp;</td>
                <td width="2%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="2%">
                    &nbsp;</td>
                <td align="center" colspan="2" style="width: 96%" width="48%">
                    <asp:ImageButton ID="CmdClose" runat="server" 
                        ImageUrl="~/Images/CloseBtn.png" />
                </td>
                <td width="2%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="2%">
                    &nbsp;</td>
                <td align="center" colspan="2" style="width: 96%" width="48%">
                    &nbsp;</td>
                <td width="2%">
                    &nbsp;</td>
            </tr>
        </table>
   
        
    </div> 
</ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>

