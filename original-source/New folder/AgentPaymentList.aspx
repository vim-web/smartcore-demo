<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="AgentPaymentList.aspx.vb" Inherits="SMARTCORE_UI.AgentPaymentList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    

        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;">
          <legend>AGENT WISE PAYMENT</legend>
              <table width="100%">
              <tr>
              <td width="2%"></td>
              <td width="48%"><label>Branch</label></td>
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
                      <td width="48%"><label>Scheme</label>
                          &nbsp;</td>
                      <td width="48%">
                          <asp:TextBox ID="TxtScheme" runat="server" Width="25%" AutoPostBack="True"></asp:TextBox>
                          &nbsp;<asp:DropDownList ID="CmbScheme" runat="server" Width="70%" 
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
                          <label>Agent</label></td>
                      <td width="48%">
                          <asp:DropDownList ID="CmbAgent" runat="server" Width="100%">
                          </asp:DropDownList>
                      </td>
                      <td width="2%">
                          &nbsp;</td>
                  </tr>
                  <tr>
                      <td width="2%">
                          &nbsp;</td>
                      <td width="48%">
                          <label>From Date</label></td>
                      <td width="48%">
                          <asp:TextBox ID="TxtFrDate" runat="server" Width="100%"></asp:TextBox>
                          <asp:CalendarExtender ID="TxtFrDate_CalendarExtender" runat="server" 
                              TargetControlID="TxtFrDate" Format="dd/MM/yyyy" >
                          </asp:CalendarExtender>
                      </td>
                      <td width="2%">
                          &nbsp;</td>
                  </tr>
                  <tr>
                      <td width="2%">
                          &nbsp;</td>
                      <td width="48%">
                          <label>To Date</label></td>
                      <td width="48%">
                          <asp:TextBox ID="TxtToDate" runat="server" Width="100%"></asp:TextBox>
                          <asp:CalendarExtender ID="TxtToDate_CalendarExtender" runat="server" 
                              TargetControlID="TxtToDate" Format="dd/MM/yyyy" >
                          </asp:CalendarExtender>
                      </td>
                      <td width="2%">
                          &nbsp;</td>
                  </tr>
                  <tr>
                      <td width="2%">
                          &nbsp;</td>
                      <td align="center" colspan="2" style="width: 96%" width="48%">
                          <asp:ImageButton ID="CmdShow" runat="server" ImageUrl="~/Images/Show.png" />
                          &nbsp;<asp:ImageButton ID="CmdClose" runat="server" 
                              ImageUrl="~/Images/CloseBtn.png" />
                      </td>
                      <td width="2%">
                          &nbsp;</td>
                  </tr>
              </table>
          </legend>
              </div>
    </tr>
   </ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>
