<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="AccountDocument.aspx.vb" Inherits="SMARTCORE_UI.AccountDocument" 
    title="Untitled Page" %>
      <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="Scripts/Common.js"></script> 
<style type="text/css">
   .txt_class{color:#7465C9; font-family:Arial; font-size:90%; font-weight:600;}
   
    </style>
    <script type="text/javascript">
        function validation() {
           
            var DocDate = document.getElementById('<%=txt_DocDate.ClientID %>');
            var DocDetails = document.getElementById('<%=txt_DocDetails.ClientID %>');
            if (DocDate.value == '')
            { alertify.error('Please Specify Document Date'); DocDate.focus(); return false; }
            if (DocDetails.value == '')
            { alertify.error('Please Specify Document Details'); DocDetails.focus(); return false; }
            return true;
        }

        function ValidateSave() {
            var AccNo = document.getElementById('<%=txt_AccNo.ClientID %>');
            var status = document.getElementById('<%=hid_status.ClientID %>');
            
            if (AccNo.value == '')
            { alertify.error('Please Specify Account No'); AccNo.focus(); return false; }
            if (status.value == "NEW" || status.value=="NEWOLD")
            { alertify.error('Please Add Details'); return false; }
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
<legend> Account Documents</legend>              
    <div style="width:100%;height:40px; padding-top:20px; text-align:center; font-weight:bold; color:#7B67EB; " >
        
 </div>
 
 <div style="width:95%;  padding:20px;  "><input type="hidden" id="hid_AccID" runat="server" value="" />
 <table width="100%">
 <tr>
 <td width="15%" class="txt_class" >Branch</td>
 <td width="30%"   >
     <asp:DropDownList ID="ddl_branch" runat="server" Width="100%" >
     </asp:DropDownList>
     </td>
 <td width="10%">
     </td>
 <td width="15%" class="txt_class">
     Neme</td>
 <td width="30%" class="txt_class" >
     <asp:TextBox ID="txt_name" runat="server" Width="100%" ></asp:TextBox>
     </td>
 
 </tr> 

     <tr>
         <td  class="txt_class">
             Scheme</td>
         <td class="txt_class" >
             <asp:DropDownList ID="ddl_Scheme" runat="server" Width="100%" >
             </asp:DropDownList>
         </td>
         <td >
             &nbsp;</td>
         <td class="txt_class" >
             Address</td>
         <td >
             <asp:TextBox ID="txt_address" runat="server"  Width="100%" 
                 TextMode="MultiLine" ></asp:TextBox>
         </td>
     </tr>
  <tr>
         <td class="txt_class" >
             Acc No.</td>
         <td class="txt_class" >
         
             <asp:TextBox ID="txt_AccNo" runat="server" Width="100%" AutoPostBack="True" ></asp:TextBox>
         </td>
         <td >
             &nbsp;</td>
         <td >
             &nbsp;</td>
         <td >
             &nbsp;</td>
     </tr>
       <tr>
         <td >
             &nbsp;</td>
         <td class="txt_class" >
             &nbsp;</td>
         <td >
             &nbsp;</td>
         <td >
             &nbsp;</td>
         <td >
             &nbsp;</td>
     </tr>
       <tr>
         <td class="txt_class" >
             &nbsp;</td>
         <td  >
             &nbsp;</td>
         <td >
             &nbsp;</td>
         <td class="txt_class" >
             &nbsp;</td>
         <td >
             &nbsp;</td>
     </tr>
     <tr>
         <td class="txt_class" >
             Date</td>
         <td class="txt_class">
         <asp:CalendarExtender ID="calanderID" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_DocDate"></asp:CalendarExtender>
             <asp:TextBox ID="txt_DocDate" runat="server" Width="100%"></asp:TextBox>
         </td>
         <td>
             &nbsp;</td>
         <td class="txt_class" >
             Document Type</td>
         <td>
             <asp:DropDownList ID="ddl_DocType" runat="server" Width="100%">
             </asp:DropDownList>
         </td>
     </tr>
     <tr>
         <td class="txt_class" >
             Details</td>
         <td class="txt_class">
             <asp:TextBox ID="txt_DocDetails" runat="server" Width="100%"></asp:TextBox>
         </td>
         <td>
             &nbsp;</td>
         <td>
             <asp:Button ID="but_add" runat="server" BorderStyle="None" CssClass="btn" 
                 Height="30" OnClientClick="return validation()" Text="Add" Width="70" />
         </td>
         <td>
             &nbsp;</td>
     </tr>
     <tr>
         <td>
             &nbsp;</td>
         <td class="txt_class">
             &nbsp;</td>
         <td>
             &nbsp;</td>
         <td>
             &nbsp;</td>
         <td>
             &nbsp;</td>
     </tr>
     <tr>
         <td>
             &nbsp;</td>
         <td class="txt_class">
             &nbsp;</td>
         <td>
             &nbsp;</td>
         <td>
             &nbsp;</td>
         <td>
             &nbsp;</td>
     </tr>
     <tr>
         <td colspan="5"><input type="hidden" runat="server" value="NEW" id="hid_status" />
             <asp:GridView ID="Grid_DcumentDetails" runat="server" CssClass="grid-view"  Width="100%"
                 AutoGenerateColumns="False" >
                 <Columns>
                  <asp:BoundField DataField="Doc_TypeID" HeaderText="Document Type"  />
                     <asp:BoundField DataField="Tr_Date" HeaderText="Date"  />
                     <asp:BoundField DataField="Doc_Dtls" HeaderText="Details" />
                     <asp:BoundField DataField="Doc_Status" HeaderText="Status" />
                     <asp:BoundField DataField="Doc_RtnDate" HeaderText="Return Date" />
                     <asp:BoundField DataField="Doc_RtnDtls" HeaderText="Return Details" />
                     <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/del.jpg" 
                         ShowDeleteButton="True" />
                 </Columns>
             </asp:GridView>
         </td>
     </tr>
     <tr>
         <td>
             &nbsp;</td>
         <td class="txt_class">
             &nbsp;</td>
         <td>
         <asp:Button ID="but_save" runat="server" BorderStyle="None" Text="Save" Width="80" Height="30" CssClass="btn"  OnClientClick="return ValidateSave()" />
           </td>
         <td>
         
    
     <asp:Button ID="but_close" runat="server" BorderStyle="None" CssClass="btn"  style="margin-left:20px;"
         Height="30" Text="Close" Width="80" />
             &nbsp;</td>
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
