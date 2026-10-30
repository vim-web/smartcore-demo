<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="AcquiantanceRpt.aspx.vb" Inherits="SMARTCORE_UI.AcquiantanceRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>



<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    

        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 44%; margin-left:28%; margin-right:28%;">
     <legend>Acquiantance Report </legend>
         <table width="100%">
             <tr>
                 <td width="2%">
                 </td>
                 <td width="48%">
                 <label>                     Effective Date</label>
                 </td>
                 <td width="48%">
                     <asp:TextBox ID="TxtEffDate" runat="server" Width="100%"></asp:TextBox>
                     <asp:CalendarExtender ID="txtrcptdate_CalendarExtender" runat="server" 
                         Format="dd/MM/yyyy" TargetControlID="TxtEffDate">
                     </asp:CalendarExtender>
                 </td>
                 <td width="2%">
                 </td>
             </tr>
             
             <%--       <tr>
              <td>
                   </td>
              <td>
                  <label>Reason</label></td>
              <td>
                  <asp:TextBox ID="TxtReason" runat="server" Width="100%"></asp:TextBox>
              </td>
              <td>
                   </td>
          </tr>--%>
             
             <tr>
                 <td width="2%">
                     </td>
                 <td width="48%">
                 <label>Branch</label>
                     </td>
                 <td width="48%">
                     <asp:DropDownList ID="CmbBranch" runat="server" 
                         meta:resourcekey="CmbOccupationResource1" TabIndex="12" Width="100%">
                     </asp:DropDownList>
                     </td>
                 <td width="2%">
                     </td>
             </tr>
             <tr>
                 <td width="2%">
                     </td>
                 <td width="48%"><label>Staff Type</label>
                     </td>
                 <td width="48%">
                     <asp:DropDownList ID="CmbStafftype" runat="server" 
                         meta:resourcekey="CmbStafftype" TabIndex="12" Width="100%">
                     </asp:DropDownList>
                     </td>
                 <td width="2%">
                     </td>
             </tr>
             
             <tr>
                 <td>
                 </td>
                 <td align="center" colspan="2">
                     <asp:CheckBox ID="ChkPost" runat="server" Text="Post to Accounts" />
                 </td>
                 <td>
                 </td>
             </tr>
             <tr>
                 <td>
                 </td>
                 <td align="center" colspan="2">
                     <asp:ImageButton ID="CmdShow" runat="server" Height="30px" 
                         ImageUrl="~/Images/Show.png" />
                     <asp:ImageButton ID="CmdPrint" runat="server" Height="30px" 
                         ImageUrl="~/Images/DUPVOUCHER.png" />
                     <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />
                 </td>
                 <td>
                 </td>
             </tr>
         </table>
     </div>
    </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    
    </asp:Content>

