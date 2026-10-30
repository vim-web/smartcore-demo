<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="GsdStandingRpt.aspx.vb" Inherits="SMARTCORE_UI.GsdStandingRpt" 
    title="Untitled Page" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style= "margin-left:20%; margin-right:20%;margin-top:5%;" onkeypress="return WebForm_FireDefaultButton(event, '<%= BtnShow.ClientID %>')" >                
  <legend><b>Gsd/Ldd Standing Instruction Report</b> </legend>     
 <table width="100%">
 <tr >
 <td width="2%"></td>
  <td width="48%" colspan="2"></td>
   <td width="48%" colspan="2"></td>
 <td width="2%"></td>
 </tr>
  <tr >
 <td width="2%"></td>
  <td width="48%" colspan="2">
      <asp:Label ID="Lbl_Branch" runat="server" Font-Names="Tahoma" 
          Font-Size="Medium" ForeColor="#6666FF" Text="Branch"></asp:Label>
      </td>
   <td width="48%" colspan="2">
       <asp:DropDownList ID="Ddl_Branch" runat="server" Width="68%">
       </asp:DropDownList>
      </td>
 <td width="2%"></td>
 </tr>
  <tr >
 <td width="2%"></td>
  <td width="48%" style="width: 0%">
      <asp:CheckBox ID="ChkGsd" runat="server" Text="Gsd" />
      </td>
      <td style="width: 24%" width="48%">
          <asp:CheckBox ID="ChkLoan" runat="server" Text="Loan" />
      </td>
   <td width="48%" style="width: 0%">
       <asp:CheckBox ID="ChkFailure" runat="server" AutoPostBack="True" 
           Text="GsdFailed" />
      </td>
      <td style="width: 24%" width="48%">
          <asp:CheckBox ID="ChkFail" runat="server" AutoPostBack="True" 
              Text="LoanFailed" />
      </td>
 <td width="2%"></td>
 </tr>
  <tr >
 <td width="2%"></td>
  <td width="48%" colspan="2">
      <asp:Label ID="Lbl_FromDate" runat="server" Font-Names="Tahoma" 
          Font-Size="Medium" ForeColor="#6666FF" Text="From Date"></asp:Label>
      </td>
   <td width="48%" colspan="2">
       <asp:TextBox ID="Txt_FrDate" runat="server" BackColor="White"           
           OnChange="validatedate(this);" TabIndex="4" Width="68%"></asp:TextBox>
       <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
           TargetControlID="Txt_FrDate">
       </asp:CalendarExtender>
      </td>
 <td width="2%"></td>
 </tr>
  <tr >
 <td width="2%"></td>
  <td width="48%" colspan="2">
      <asp:Label ID="Lbl_ToDt" runat="server" Font-Names="Tahoma" Font-Size="Medium" 
          ForeColor="#6666FF" Text="To Date"></asp:Label>
      </td>
   <td width="48%" colspan="2">
       <asp:TextBox ID="Txt_ToDate" runat="server" BackColor="White" 
           
           OnChange="validatedate(this);" TabIndex="4" Width="68%"></asp:TextBox>
       <asp:CalendarExtender ID="Txt_ToDate_CalendarExtender" runat="server" 
           Format="dd/MM/yyyy" TargetControlID="Txt_ToDate">
       </asp:CalendarExtender>
      </td>
 <td width="2%"></td>
 </tr>
  <tr >
 <td width="2%"></td>
  <td width="48%" align="right" colspan="2">
      <asp:Button ID="BtnShow" runat="server" Text="Show" Width="50%" />
      </td>
   <td width="48%" colspan="2">
       <asp:Button ID="BtnClose" runat="server" PostBackUrl="~/Main.aspx" Text="Close" 
           Width="50%" />
      </td>
 <td width="2%"></td>
 </tr>
 </table>
 
 
 
 
<br />
<br />
</ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>
