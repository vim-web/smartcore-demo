<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="BudgetRpt.aspx.vb" Inherits="SMARTCORE_UI.BudgetRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 



    <style type="text/css">
        .style1
        {
            width: 38%;
        }
        .style2
        {
            width: 2%;
        }
    </style>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;margin-top:5%; font-family: Tahoma" >                
      <table width="100%">
      <tr>
      <td class="style2"></td>
      <td width="10%"></td>
      <td class="style1"><label>Branch</label>&nbsp;</td>
      <td width="38%">
          <asp:DropDownList ID="Ddl_Branch" runat="server" Width="98%">
          </asp:DropDownList>
          </td>
          <td width="10%"</td>
      <td width="2%"></td>
      </tr>
          <tr>
              <td class="style2">
                  &nbsp;</td>
                  <td width="10%"></td>
              <td class="style1">
                  <label>
                  Financial Year</label></td>
              <td width="38%">
                    <asp:DropDownList ID="CmbYear" runat="server" Width="100%">

                  </asp:DropDownList>
                  <td width="10%"></td>
              </td>
              <td width="2%">
                  &nbsp;</td>
          </tr>
          <tr>
              <td class="style2">
                  &nbsp;</td>
              <td align="right" colspan="2">
                  <asp:ImageButton ID="CmbShow" runat="server" ImageUrl="~/Images/Show.png" />
                  &nbsp;</td>
              <td width="2%">
                  <asp:ImageButton ID="CmbClose" runat="server" 
                      ImageUrl="~/Images/CloseBtn.png" />
              </td>
          </tr>
      </table>

       </div> 
</ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>
