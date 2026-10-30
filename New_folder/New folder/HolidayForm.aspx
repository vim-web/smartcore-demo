<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="HolidayForm.aspx.vb" Inherits="SMARTCORE_UI.HolidayForm" 
    title="Untitled Page" %>
     <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="Scripts/Common.js"></script> 
<style type="text/css">
   .txt_class{color:#7465C9; font-family:Arial; font-size:90%; font-weight:600;}
   
    </style>
    <script type="text/javascript" >
        function validation() {

            var date = document.getElementById('<%=txt_date.ClientID%>');
            var discription = document.getElementById('<%=txt_Description.ClientID%>');
            if (date.value == '') {
                alertify.error('Please Select Date');
                return false;
            }
            if (discription.value == '') {
                alertify.error('please select Discription');
            return false
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
<div class="gradientBoxesWithOuterShadows" style="width:1000px;margin:0 auto; margin-top:40px;  font-family: Tahoma" >    
<legend>Holiday Master</legend>
            
    <div style="width:100%;height:40px;  padding-top:20px; text-align:center; font-weight:bold; color:#7B67EB; " >
   

 </div>
 
 <div style="width:95%;  padding:20px;  ">
 <table width="100%">
 <tr>
 <td width="15%" ></td>
 <td width="15%" class="txt_class"  >Date</td>
 <td width="40%">
    <asp:CalendarExtender runat="server" ID="calanderid" TargetControlID="txt_date" Format="dd/MM/yyyy"  ></asp:CalendarExtender>
     <asp:TextBox ID="txt_Date" runat="server" Width="70%" ></asp:TextBox>
    
     </td>
 <td width="15%" ></td>
 
 </tr> 
  <tr>
 <td ></td>
 <td class="txt_class"  >Description</td>
 <td>
    
     <asp:TextBox ID="txt_Description" runat="server" Width="70%"  TextMode="MultiLine"></asp:TextBox>
    
     </td>
 <td ></td>
 
 </tr> 
  <tr>
 <td ></td>
 <td class="txt_class"  >&nbsp;</td>
 <td style="padding-top:20px;" >
    
     <asp:Button ID="but_save" runat="server" BorderStyle="None" Text="Save" Width="80" Height="30" CssClass="btn"  OnClientClick="return validation()" />
    
     <asp:Button ID="but_close" runat="server" BorderStyle="None" CssClass="btn"  style="margin-left:20px;"
         Height="30" Text="Close" Width="80" />
    
     </td>
 <td ></td>
 
 </tr> 
  <tr>
 <td ></td>
 <td class="txt_class"  ></td>
 <td>
    
     </td>
 <td ></td>
 
 </tr> 
 </table>
 </div>

   
       </div>
       
</div>

   </ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>
