<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="AccDocumentRpt.aspx.vb" Inherits="SMARTCORE_UI.AccDocumentRpt" 
    title="Untitled Page" %>
     <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" >
        function Validate() {
            var StrDate = document.getElementById('<%=txt_DocDate.ClientID %>');
            if (StrDate.value == '') {
                alertify.error("Please Select Transaction Date");
                StrDate.focus();
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
<div style="width:1000px; margin:0 auto; padding:20px;">
<div class="gradientBoxesWithOuterShadows" style="width: 100%; margin-top:40px;  font-family: Tahoma" >                
    <legend>Account Documents</legend>
    <div style="width:100%;height:40px; padding-top:20px; text-align:center; font-weight:bold; color:#7B67EB; " >
      
 </div>
 
 <div style="width:95%;  padding:20px;  "><input type="hidden" id="hid_AccID" runat="server" value="" />
 <table width="100%">
 <tr>
 <td width="15%" class="txt_class" >&nbsp;</td>
     <td class="txt_class" width="15%">
         Branch</td>
 <td width="30%"   >
     <asp:DropDownList ID="ddl_branch" runat="server" Width="100%" >
     </asp:DropDownList>
     </td>
 <td width="15%" class="txt_class">
     &nbsp;</td>
 
 </tr> 

     <tr>
         <td  class="txt_class">
             &nbsp;</td>
         <td class="txt_class">
             Scheme</td>
         <td class="txt_class" >
             <asp:DropDownList ID="ddl_Scheme" runat="server" Width="100%" >
             </asp:DropDownList>
         </td>
         <td class="txt_class" >
             &nbsp;</td>
     </tr>
     <tr>
         <td class="txt_class" >
             &nbsp;</td>
         <td class="txt_class">
             Return Date</td>
         <td class="txt_class">
         <asp:CalendarExtender ID="calanderID" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_DocDate"></asp:CalendarExtender>
             <asp:TextBox ID="txt_DocDate" runat="server" Width="100%"></asp:TextBox>
         </td>
         <td class="txt_class" >
             &nbsp;</td>
     </tr>
     <tr>
         <td>
             &nbsp;</td>
         <td class="txt_class" >
             Report Type</td>
         <td class="txt_class">
             <asp:RadioButtonList ID="Rdo_RptType" runat="server" 
                 RepeatDirection="Horizontal">
                 <asp:ListItem Value="C">Closed Loan</asp:ListItem>
                 <asp:ListItem Value="A" Selected>All Loan</asp:ListItem>
             </asp:RadioButtonList>
         </td>
         <td>
             &nbsp;</td>
     </tr>
     <tr>
         <td>
             &nbsp;</td>
         <td>
             &nbsp;</td>
         <td class="txt_class">
             &nbsp;</td>
         <td>
             &nbsp;</td>
     </tr>
     <tr>
         <td>&nbsp;</td>
         <td>
             &nbsp;</td>
         <td class="txt_class">
             <asp:Button ID="but_save" runat="server" BorderStyle="None" CssClass="btn" 
                 Height="30" OnClientClick="return Validate()" Text="Show" Width="80" />
             <asp:Button ID="but_close" runat="server" BorderStyle="None" CssClass="btn" 
                 Height="30" style="margin-left:20px;" Text="Close" Width="80" />
         </td>
         <td>
             &nbsp;</td>
     </tr>
 </table>
 </div>

   
       </div>
       
       </div>
</div>

   </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
