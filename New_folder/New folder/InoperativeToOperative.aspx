<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="InoperativeToOperative.aspx.vb" Inherits="SMARTCORE_UI.InoperativeToOperative" MasterPageFile="~/SMARTCORE.Master" %>

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
     <legend>Operative/Inoperative Status Marking</legend>
      <table width="100%">
          <tr>
              <td width="2%">
                   </td>
              <td width="48%">
                  <label>
                  Branch</label></td>
              <td width="48%">
                  <asp:TextBox ID="TxtBranch" runat="server" AutoPostBack="True" Width="23%"></asp:TextBox>
                   &nbsp;<asp:DropDownList ID="CmbBranch" runat="server" AutoPostBack="True" 
                      Width="68%">
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
                  <asp:TextBox ID="TxtScheme" runat="server" AutoPostBack="True" Width="23%"></asp:TextBox>
                   &nbsp;<asp:DropDownList ID="CmbScheme" runat="server" AutoPostBack="True" 
                      Width="68%">
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
                  <asp:TextBox ID="TxtShortCode" runat="server" AutoPostBack="True" Width="23%"></asp:TextBox>
                   &nbsp;<asp:TextBox ID="TxtAccNo" runat="server" AutoPostBack="True" Width="65%"></asp:TextBox></td>
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
                   &nbsp;</td>
              <td>
                  <label>
                  Status</label></td>
              <td>
                  <asp:DropDownList ID="CmbStatus" runat="server" Width="100%">
                      <asp:ListItem Value="Y">Operative</asp:ListItem>
                      <asp:ListItem Value="I">InOperative</asp:ListItem>
                  </asp:DropDownList>
              </td>
              <td>
                   &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
         <%-- <tr>
              <td>
              </td>
              <td>
                  <label>
                  Reason</label></td>
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
