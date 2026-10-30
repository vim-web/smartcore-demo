<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="ChangePwd.aspx.vb" Inherits="SMARTCORE_UI.ChangePwd" 
    title=" CHANGE USER PASSWORD" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<script type='text/javascript'>

    function ValidateSave() {

             var Ctl = document.getElementById('<%=TxtCurrPwd.ClientID%>');
             if (Ctl.value == "") {
                 alertify.error('Current Password Not Entered, Please Check');
                 Ctl.focus();
                 return false;
             }


             var Ctl = document.getElementById('<%=TxtPassword.ClientID%>');
             var Ct2 = document.getElementById('<%=TxtUsrName.ClientID%>');
             var Size = Ctl.value.length
             
             if (Size < 8 && Ct2.value.toUpperCase() != "SA") {
                 alertify.error('Password doest not meet minimum length(8), Please Check');
                 Ctl.focus();
                 return false;
             }
             
             

             var Ct2 = document.getElementById('<%=TxtPassword.ClientID%>');
             var Ct3 = document.getElementById('<%=TxtConPassword.ClientID%>');

             if (Ct2.value != Ct3.value) {
                 alertify.error('  Password Does not Match,  Please Check');
                 Ct3.focus();
                 return false;
               }

        return true;
    }

</script>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    

        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 44%; margin-left:28%; margin-right:28%;">
      <legend>    CHANGE&nbsp;&nbsp; PASSWORD  </legend>
         
         <table width="100%">
          <tr>
              <td width="2%">
                    </td>
              <td width="48%">
                   </td>
              <td width="48%">
                   </td>
              <td width="2%">
                    </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td>
                  <label>
                  USER NAME</label></td>
              <td>
                   <asp:TextBox ID="TxtUsrName" runat="server" Width="100%" ReadOnly="True"></asp:TextBox>
              </td>
              <td>
                   </td>
          </tr>
          <tr>
              <td>
                   &nbsp;</td>
              <td>
                  <label>
                  ENTER CURRENT PASSWORD</label></td>
              <td>
                  <asp:TextBox ID="TxtCurrPwd" runat="server" Width="100%" TextMode="Password"></asp:TextBox>
              </td>
              <td>
                   &nbsp;</td>
          </tr>
             <tr>
                 <td>
                 </td>
                 <td>
                     <label>
                     NEW PASSWORD</label></td>
                 <td>
                     <asp:TextBox ID="TxtPassword" runat="server" TabIndex="2" Width="100%" 
                         TextMode="Password"></asp:TextBox>
                 </td>
                 <td>
                 </td>
             </tr>
          <tr>
              <td>
                   </td>
              <td>
                  <label>CONFIRM PASSWORD</label></td>
              <td>
                  <asp:TextBox ID="TxtConPassword" runat="server" Width="100%" TabIndex="3" 
                      TextMode="Password"></asp:TextBox>
              </td>
              <td>
                   </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
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
              <td>
                   </td>
              <td align="center" colspan="2">
                  &nbsp;</td>
              <td>
                   </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td align="center" colspan="2">
                  <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" 
                      OnClientClick="return ValidateSave();" TabIndex="4" />
                   <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" 
                      TabIndex="10" />
                   <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" 
                      TabIndex="5" />
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
