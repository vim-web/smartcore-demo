<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="Gramrate.aspx.vb" Inherits="SMARTCORE_UI.Gramrate" %>

 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
   
       <script type='text/javascript' src="Scripts/Common.js"></script>
   <script type='text/javascript'>
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

</script>
 
    
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePageMethods="True" CombineScripts="True">
   
</asp:ToolkitScriptManager>


<div class="gradientBoxesWithOuterShadows" style="width: 100%; " >   
    <legend>GRAM RATE</legend>   
    <table width="80%" align="center" >
         <tr>
             <td width="3%">&nbsp;</td>
<td colspan="4" >
    <%--<asp:RadioButtonList ID="RadioButtonList1" runat="server" Width="98%"
     RepeatDirection="Horizontal" AutoPostBack="true">
        <asp:ListItem>Gram Rate</asp:ListItem>
        <asp:ListItem>Percentage</asp:ListItem>
    </asp:RadioButtonList>--%>
             </td>
        <td width="15%" >&nbsp;</td>
        <td width="30%" >&nbsp;</td>
              <td width="3%">&nbsp;</td>

    </tr>
         <tr>
             <td width="3%">
             </td>
             <td width="15%">
                 <asp:Label ID="LblBr" runat="server" Text="Branch"></asp:Label>&nbsp;</td>
             <td width="5%">
                 <asp:TextBox ID="Txtbr_code" runat="server" AutoPostBack="True" Visible="True" 
                     Width="100%"></asp:TextBox>
             </td>
             <td style="width: 25%" width="30%">
                 <asp:DropDownList ID="CmbBranch" runat="server" Visible="True" Width="100%">
                 </asp:DropDownList>
             </td>
             <td width="4%">
             </td>
             <td width="15%">
             </td>
             <td width="30%">
             </td>
             <td width="3%">
             </td>
         </tr>
         <tr>
              <td width="3%"></td>
             <td width="15%">
                 <label>
                 Scheme</label></td>
             <td width="30%" colspan="2">
                 <asp:DropDownList ID="Cmbscheme" runat="server" Width="100%" AutoPostBack="True">
                 </asp:DropDownList>
             </td>
              <td width="4%"></td>
             <td width="15%"> <label>
                Market Gram Rate</label></td>
             <td width="30%">
                 <asp:TextBox ID="txt_rate" runat="server" CssClass="twitterStyleTextbox" 
                     MaxLength="5" onkeypress="return isFloatKey(event);" Width="100%"></asp:TextBox>
              </td>
              <td width="3%"></td>
         </tr>
         <tr>
              <td width="3%"></td>
             <td width="15%">
                 <label>
                 Effective Date</label></td>
             <td width="30%" colspan="2">
                 <asp:TextBox ID="txt_date" runat="server" Width="100%" AutoPostBack="True"  CssClass="twitterStyleTextbox"  ></asp:TextBox>
                 <asp:CalendarExtender ID="txt_date_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                     TargetControlID="txt_date">
                </asp:CalendarExtender>
             </td>
              <td width="4%"></td>
             <td width="15%"><label>Maximum Lending Rate</label>
                </td>
             <td width="30%">
                 <asp:TextBox ID="Txt_LendingRate" runat="server" Width="100%" CssClass="twitterStyleTextbox" 
                     MaxLength="5" onkeypress="return isFloatKey(event);"></asp:TextBox>
              </td>
              <td width="3%"></td>
         </tr>
         <tr>
              <td width="3%"></td>
             <td width="15%">
                 <label>
                 Bank Rate Type</label></td>
             <td width="30%" colspan="2">
                 <asp:DropDownList ID="cmbrate" runat="server" Width="100%" AutoPostBack="True">
                     <asp:ListItem>RATE</asp:ListItem>
                     <asp:ListItem>PERCENTAGE</asp:ListItem>
                 </asp:DropDownList>
             </td>
              <td width="4%"></td>
             <td width="15%">
               <label>  <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></label></td>
             <td width="30%">
                 <asp:TextBox ID="txtrate" runat="server" Width="100%" MaxLength="5" AutoPostBack="True"  CssClass="twitterStyleTextbox"  onkeypress="return isFloatKey(event);" ></asp:TextBox>
             </td>
              <td width="3%"></td>
         </tr>
         <tr>
              <td width="3%"></td>
             <td width="15%">
                 <label>
                 Minimum Weight(grms)</label></td>
             <td width="30%" colspan="2">
                 <asp:TextBox ID="txt_weight" runat="server" Width="100%" MaxLength="5"  CssClass="twitterStyleTextbox"  onkeypress="return isFloatKey(event);"></asp:TextBox>
             </td>
              <td width="4%"></td>
             <td width="15%">
                 <label>
                 Minimum Weight Rate</label></td>
             <td width="30%">
                 <asp:TextBox ID="txtweight_rate" runat="server" Width="100%" MaxLength="5"  CssClass="twitterStyleTextbox"  onkeypress="return isFloatKey(event);" ></asp:TextBox>
             </td>
              <td width="3%"></td>
         </tr>
         <tr>
              <td width="3%"></td>
             <td width="15%"></td>
             <td width="30%" colspan="2"></td>
              <td width="4%"></td>
             <td width="15%"></td>
             <td width="30%"></td>
          <td width="3%"></td>
         </tr>
         <tr>
             <td width="3%">&nbsp;</td>
             <td width="15%">&nbsp;</td>
             <td colspan="2" width="30%">&nbsp;</td>
             <td width="4%">&nbsp;</td>
             <td width="15%">&nbsp;</td>
             <td width="30%">&nbsp;</td>
             <td width="3%">&nbsp;</td>
         </tr>
         <tr>
              <td width="3%"></td>
             <td width="15%" ><legend>Locate Details</legend></td>
             <td width="30%" colspan="2" ></td>
              <td width="4%"></td>
             <td width="15%" ></td>
             <td width="30%" ></td>
              <td width="3%"></td>
         </tr>
         <tr>

              <td width="3%"></td>
             <td  colspan="6" align="center">
                 <asp:GridView ID="GvRpt" runat="server" CssClass="grid-view" Width="100%">
                     <AlternatingRowStyle BackColor="#FBFBFB" />
                 </asp:GridView>
             </td>  
            <td width="3%"></td>
         </tr>       
         
         
         <tr>
               <td width="3%">&nbsp;</td>
             <td width="15%">&nbsp;</td>
            
             <td width="30%" align="right" colspan="2">
                 
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
             <td align="right" colspan="2" width="30%"></td>
             <td width="4%"></td>
             <td align="left" width="15%"></td>
             <td width="30%"></td>
             <td width="3%"></td>
         </tr>
         <tr>
             <td width="3%">&nbsp;</td>
             <td width="15%">&nbsp;</td>
             <td align="right" colspan="4" style="width: 45%">
                 <asp:ImageButton ID="btn_save" runat="server" ImageUrl="~/Images/Save.png" OnClientClick="return ValidateSave();"/>
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
