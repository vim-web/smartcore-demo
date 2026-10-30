<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="AccountPrevilage.aspx.vb" Inherits="SMARTCORE_UI.AccountPrevilage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<script type='text/javascript'>

    function ValidateSave() {



        var Ctl = document.getElementById('<%=TxtAccNo.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Account Number Not Entered, Please Check');
            Ctl.focus();
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
     <legend>Account Status</legend>
      <table width="100%">
          <tr>
              <td width="2%">
                   </td>
              <td width="48%">
                  <label>
                  Branch</label></td>
              <td width="48%">
                  <asp:TextBox ID="TxtBranch" runat="server" AutoPostBack="True" Width="25%"></asp:TextBox>
                   <asp:DropDownList ID="CmbBranch" runat="server" AutoPostBack="True" 
                      Width="70%">
                  </asp:DropDownList>
              </td>
              <td width="2%">
                   </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td>
                  <label>
                  Scheme</label></td>
              <td>
                  <asp:TextBox ID="TxtScheme" runat="server" AutoPostBack="True" Width="25%"></asp:TextBox>
                   <asp:DropDownList ID="CmbScheme" runat="server" AutoPostBack="True" 
                      Width="70%">
                  </asp:DropDownList>
              </td>
              <td>
                   </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td>
                  <label>
                  Account No</label></td>
              <td>
                  <asp:TextBox ID="TxtShortCode" runat="server" AutoPostBack="True" Width="25%"></asp:TextBox>
                   <asp:TextBox ID="TxtAccNo" runat="server" AutoPostBack="True" Width="67%"></asp:TextBox></td>
              <td>
                   </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td>
                  <label>Name</label></td>
              <td>
                  <asp:TextBox ID="TxtName" runat="server" Width="100%"></asp:TextBox>
              </td>
              <td>
                   </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td>
                  <label>Address</label></td>
              <td>
                  <asp:TextBox ID="TxtAddress" runat="server" Width="100%" Height="58px" 
                      TextMode="MultiLine"></asp:TextBox>
              </td>
              <td>
                   </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td>
                  <label>Present Previlage</label></td>
              <td>
                  <asp:DropDownList ID="CmbOldPrevilage" runat="server" Width="100%" 
                      Enabled="False">
                  </asp:DropDownList>
              </td>
              <td>
                   </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td>
                  <label>New Previlage</label></td>
              <td>
                  <asp:DropDownList ID="CmbNewPrevilage" runat="server" Width="100%">
                  </asp:DropDownList>
              </td>
              <td>
                   </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td>
                  <label>InterestRate</label></td>
              <td>
    <asp:TextBox ID="Txt_IntRate" Width="100%" runat="server" MaxLength="2"></asp:TextBox>
  <%--  <asp:CalendarExtender ID="txtrcptdate_CalendarExtender" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="TxtEffDate">
   </asp:CalendarExtender>--%>
              </td>
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
                  <asp:HiddenField ID="HdnAccID" runat="server" />
              </td>
              <td>
                   </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td align="center" colspan="2">
                  <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" OnClientClick="return ValidateSave();" />
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
