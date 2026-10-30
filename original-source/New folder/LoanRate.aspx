<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="LoanRate.aspx.vb" Inherits="SMARTCORE_UI.LoanRate" 
    title="Loan Percentage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
   
       <script type='text/javascript' src="Scripts/Common.js"></script>
<%--   <script type='text/javascript'>
       function ValidateSave() {
           var Ctl = document.getElementById('<%=txt_rate.ClientID%>');
       var Ct2 = document.getElementById('<%=txtrate.ClientID%>');
       var Ct3 = document.getElementById('<%=txtweight_rate.ClientID%>');
       var Ct4 = document.getElementById('<%=txt_weight.ClientID%>');
            
            if ( Ctl.value == "") {
                alert('Effective Gram Rate Not Entered');
                Ctl.focus();
                return false;
            }
          

       
       else if (Ct2.value == "") {
            alert('Bank Gram Rate Not Entered, Please Check');
            Ctl.focus();
            return false;
       }
       else if (Ct3.value == "") {
           alert('Minimum Gram Rate Not Entered, Please Check');
           Ctl.focus();
           return false;
       }
       else if (Ct4.value == "") {
           alert('Minimum Gram Weight Not Entered, Please Check');
           Ctl.focus();
           return false;
       }
        
            return true;
        
    }

</script>--%>
 
    
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePageMethods="True" CombineScripts="True">
   
</asp:ToolkitScriptManager>


<div class="gradientBoxesWithOuterShadows" style="width: 80%; margin-left:15%; margin-right:15%; " >   
    <legend>LOAN RATE</legend>   
    <table width="80%" >
         <tr>
             <td width="3%">&nbsp;</td>
<td colspan="3" >
    &nbsp;</td>
        <td width="15%" >&nbsp;</td>
        <td width="30%" >&nbsp;</td>
              <td width="3%">&nbsp;</td>

    </tr>
         <tr>
             <td width="3%">
             </td>
             <td width="15%">
                 <label>Branch</label>
             <td width="5%" style="width: 35%">
                 
                 <asp:DropDownList ID="CmbBranch" runat="server" Visible="True" Width="100%" 
                     AutoPostBack="True">
                 </asp:DropDownList>
                 
             </td>
             <td width="4%">
             </td>
             <td width="15%">
             </td>
             <td width="30%">
                 &nbsp;</td>
             <td width="3%">
             </td>
         </tr>
         <tr>
              <td width="3%"></td>
             <td width="15%">
                 <label>
                 Scheme</label></td>
             <td width="30%">
                 <asp:DropDownList ID="Cmbscheme" runat="server" Width="100%" AutoPostBack="True">
                 </asp:DropDownList>
             </td>
              <td width="4%"></td>
             <td width="15%"></td>
             <td width="30%"></td>
              <td width="3%"></td>
         </tr>
         <tr>
              <td width="3%"></td>
             <td width="15%">
                 <label>
                 Effective Date</label></td>
             <td width="30%">
                 <asp:TextBox ID="txt_date" runat="server" Width="100%" AutoPostBack="True"  CssClass="twitterStyleTextbox"  ></asp:TextBox>
                 <asp:CalendarExtender ID="txt_date_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                     TargetControlID="txt_date">
                </asp:CalendarExtender>
             </td>
              <td width="4%"></td>
             <td width="15%">
                 <label>
                Maximum Rate(per)</label></td>
             <td width="30%">
                 <asp:TextBox ID="txt_rate" runat="server" AutoPostBack="True" 
                     CssClass="twitterStyleTextbox" MaxLength="5" 
                     onkeypress="return isFloatKey(event);" Width="100%"></asp:TextBox>
             </td>
              <td width="3%"></td>
         </tr>
         <tr>
              <td width="3%"></td>
             <td width="15%">
                 <label>
                 Bank Rate Type</label></td>
             <td width="30%">
                 <asp:DropDownList ID="cmbrate" runat="server" Width="100%" AutoPostBack="True">
                       <asp:ListItem>PERCENTAGE</asp:ListItem>
                 </asp:DropDownList>
             </td>
              <td width="4%"></td>
             <td width="15%">
               <label>Loan Rate(per)</label></td>
             <td width="30%">
                 <asp:TextBox ID="Txt_BankRate" runat="server" onkeypress="return isFloatKey(event);" Width="100%"></asp:TextBox>
             </td>
              <td width="3%"></td>
         </tr>
         <tr>
              <td width="3%"></td>
             <td width="15%">
                </td>
             <td width="30%">
                 &nbsp;</td>
              <td width="4%"></td>
             <td width="15%">
                 </td>
             <td width="30%">
                 &nbsp;</td>
              <td width="3%"></td>
         </tr>
         <tr>
              <td width="3%"></td>
             <td width="15%"></td>
             <td width="30%"></td>
              <td width="4%"></td>
             <td width="15%"></td>
             <td width="30%"></td>
          <td width="3%"></td>
         </tr>
         <tr>
             <td width="3%">&nbsp;</td>
             <td width="15%">&nbsp;</td>
             <td width="30%">&nbsp;</td>
             <td width="4%">&nbsp;</td>
             <td width="15%">&nbsp;</td>
             <td width="30%">&nbsp;</td>
             <td width="3%">&nbsp;</td>
         </tr>
         <tr>
              <td width="3%"></td>
             <td width="15%" ><legend>Locate Details</legend></td>
             <td width="30%" ></td>
              <td width="4%"></td>
             <td width="15%" ></td>
             <td width="30%" ></td>
              <td width="3%"></td>
         </tr>
         <tr>

              <td width="3%"></td>
             <td  colspan="5" align="center">
                 <asp:GridView ID="GvRpt" runat="server" CssClass="grid-view" Width="100%">
                     <AlternatingRowStyle BackColor="#FBFBFB" />
                 </asp:GridView>
             </td>  
            <td width="3%"></td>
         </tr>       
         
         
         <tr>
               <td width="3%">&nbsp;</td>
             <td width="15%">&nbsp;</td>
            
             <td width="30%" align="right">
                 
                 &nbsp;</td>
              <td width="4%">&nbsp;</td>
             <td width="15%" align="left">
                 
                 &nbsp;</td>
              <td width="30%">
             
                  &nbsp;</td>
                 <td width="3%">&nbsp;</td>
                 
         </tr>
         <tr>
             <td width="3%"></td>
             <td width="15%"></td>
             <td align="right" width="30%"></td>
             <td width="4%"></td>
             <td align="left" width="15%"></td>
             <td width="30%"></td>
             <td width="3%"></td>
         </tr>
         <tr>
             <td width="3%">&nbsp;</td>
             <td width="15%">&nbsp;</td>
             <td align="right" colspan="3" style="width: 45%">
                 <asp:ImageButton ID="btn_save" runat="server" ImageUrl="~/Images/Save.png"/>
            <asp:ImageButton ID="btn_clear" runat="server" ImageUrl="~/Images/Clear.png" />
                 <asp:ImageButton ID="btn_close" runat="server" ImageUrl="~/Images/Close.png" />
                   </td>
             <td width="30%">&nbsp;</td>
             <td width="3%">&nbsp;</td>
         </tr>
    </table>         
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>