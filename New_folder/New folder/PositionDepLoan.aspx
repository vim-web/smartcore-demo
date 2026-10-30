<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="PositionDepLoan.aspx.vb" Inherits="SMARTCORE_UI.ReportsMonthly" 
    title="Untitled Page" %>
   <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="CSS/PageStyle.css" type="text/css" rel="Stylesheet" />
<script type="text/javascript" src="Scripts/Common.js" ></script>
<script type="text/javascript" >
    function validate() {
        var valid = document.getElementById('<%=txt_date.ClientID %>');
        if (valid.value == '') {
            alertify.error('Please Select Transaction Date');
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
<div style="width:100%; margin:0 auto;">
<div style="width:1000px;  margin:0 auto; margin-top:40px;" class="gradientBoxesWithOuterShadows">
<legend> Position Deposit Loan</legend>
<div style="width:100%; height:40px; padding:20px; text-align:center; font-size:24px;font-weight:bold; color:#7B67EB; " >
   </div>
<table width="100%" style="padding:30px;" >
<tr>
<td width="20%" ></td>
<td width="20%" >&nbsp;</td>
<td width="40%"></td>
<td width="20%">&nbsp;</td>
</tr>
<tr>
<td ></td>
<td>Date</td>
<td>
<asp:CalendarExtender runat="server" ID="Calandar_date" TargetControlID="txt_date"  Format="dd/MM/yyyy" ></asp:CalendarExtender>
<asp:TextBox runat="server"  ID="txt_date" onchange="validatedate(this);" Width="100%" ></asp:TextBox>  </td>
<td></td>
</tr>
<tr>
<td ></td>
<td></td>
<td></td>
<td></td>
</tr>
<tr>
<td ></td>
<td> 
        
         <asp:ImageButton ID="But_Process" runat="server"  style="margin-left:60px;" OnClientClick="return validate()"
        ImageUrl="~/Images/Process.png" Height="28px" />
    </td>
<td>
    <asp:ImageButton ID="but_Show" runat="server" ImageUrl="~/Images/Show.png"  OnClientClick="return validate()"
        Height="30px" />
    <asp:ImageButton ID="ImageButton3" runat="server" Height="32px"  style="margin-left:30px;"
        ImageUrl="~/Images/Close.png" />
    </td>
<td></td>
</tr>
</table>
</div>
</div>

   </ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>
