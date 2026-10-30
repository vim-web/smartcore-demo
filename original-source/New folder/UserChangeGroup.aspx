<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="UserChangeGroup.aspx.vb" Inherits="SMARTCORE_UI.UserChangeGroup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>



<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    

        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 44%; margin-left:28%; margin-right:28%;">
      <legend>    USER&nbsp; REGISTRATION  </legend>
         
         <table width="100%">
          <tr>
              <td width="2%">
                   &nbsp;</td>
              <td width="58%">
                  &nbsp;</td>
              <td width="38%">
                  &nbsp;</td>
              <td width="2%">
                   &nbsp;</td>
          </tr>
             <tr>
                 <td width="2%">
                 </td>
                 <td width="48%">
                     <label>
                     EMPLOYEE&nbsp; NAME</label></td>
                 <td width="48%">
                     <asp:DropDownList ID="CmbEmployeeName" runat="server" Enabled="True"  AutoPostBack="true"
                         Width="100%">
                     </asp:DropDownList>
                 </td>
                 <td width="2%">
                 </td>
             </tr>
               <tr>
              <td>
                   </td>
              <td>
                  <label>CUST ID</label></td>
              <td>
                  <asp:TextBox ID="TxtCustId" runat="server" Width="100%" 
                      ></asp:TextBox>
              </td>
              <td>
                   </td>
          </tr>
             <tr>
                 <td>
                     &nbsp;</td>
                 <td>
                     <label>
                     USER TYPE</label></td>
                 <td>
                     <asp:DropDownList ID="CmbUserType" runat="server" Width="100%">
                     </asp:DropDownList>
                 </td>
                 <td>
                     &nbsp;</td>
             </tr>
        
        
        
         
             <tr>
                 <td>
                     &nbsp;</td>
                 <td>
                     <label>
                     MAXIMUM TRANSACTION AMOUNT</label></td>
                 <td>
                     <asp:TextBox ID="TxtMaxAmt" runat="server" Width="100%">0.0</asp:TextBox>
                 </td>
                 <td>
                     &nbsp;</td>
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
              <td>
                   </td>
              <td align="center" colspan="2">
                  <asp:HiddenField ID="HdnCustID" runat="server" />
              </td>
              <td>
                   </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td align="center" colspan="2">
                  <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" 
                      onclick="CmdSave_Click1" />
                   <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" />
                   <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />
              </td>
              <td>
                   </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td align="center" colspan="2">
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
