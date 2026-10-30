<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="AccountRevoke.aspx.vb" Inherits="SMARTCORE_UI.AccountRevoke" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
<script type='text/javascript' src="Scripts/Common.js"></script> 
<script type='text/javascript' src="Scripts/jquery-1.8.3.js"></script>  

<script type='text/javascript'>

    function ValidateSave() {
        var Ctl = document.getElementById('<%=HdnAlert.ClientID%>');
        if (Ctl.value != "") {
            alert(Ctl.value);
            return false;
        }

        var Ctl = document.getElementById('<%=CmbFreezType.ClientID%>');
        var FreezType = Ctl.options[Ctl.selectedIndex].text.toUpperCase();

        if (FreezType == 'ACCOUNT') {
            var Ctl = document.getElementById('<%=TxtShortCode.ClientID%>');
            if (Ctl.value == "") {
                alertify.error('Account No Not Entered, Please Check');
                Ctl.focus();
                return false;
            }
        }
        else {
            var Ctl = document.getElementById('<%=TxtCID.ClientID%>');
            if (parseFloat(Ctl.value) == 0 || Ctl.value == "") {
                alertify.error('Customer ID Not Entered, Please Check');
                Ctl.focus();
                return false;
            }
        }

        var Ctl = document.getElementById('<%=TxtRevokeReason.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Revoke Reason Not Entered, Please Check');
            Ctl.focus();
            return false;
        }

        return true;
    }

</script>
        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 44%; margin-left:28%; margin-right:28%;">
     <legend>Account Revoke</legend>
     <table width="100%">
      <tr>
      <td></td>
      <td width="48%">
      <label>Freeze Type</label>
      </td>
      <td width="48%">
          <asp:DropDownList ID="CmbFreezType" runat="server" Width="100%" 
              AutoPostBack="True">
              <asp:ListItem>Customer</asp:ListItem>
              <asp:ListItem>Account</asp:ListItem>
          </asp:DropDownList>
          </td>
      <td width="2%"></td>
      </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td colspan="2" style="width: 96%" width="48%">
              <div id= "account"  runat="server">
              <table width="100%">
              <tr>
              <td width="48%">
                  <label>
                  Branch</label></td>
              <td width="48%">
                  <asp:TextBox ID="TxtBranch" runat="server" Width="25%" AutoPostBack="True"></asp:TextBox>
                  &nbsp;<asp:DropDownList ID="CmbBranch" runat="server" Width="70%" 
                      AutoPostBack="True">
                  </asp:DropDownList>
                  </td>
              </tr>
                  <tr>
                      <td width="48%">
                          <label>
                          Scheme</label></td>
                      <td width="48%">
                          <asp:TextBox ID="TxtScheme" runat="server" Width="25%" AutoPostBack="True"></asp:TextBox>
                          &nbsp;<asp:DropDownList ID="CmbScheme" runat="server" Width="70%" 
                              AutoPostBack="True">
                          </asp:DropDownList>
                      </td>
                  </tr>
                  <tr>
                      <td width="48%">
                          <label>
                          Account No</label>&nbsp;&nbsp;</td>
                      <td width="48%">
                          <asp:TextBox ID="TxtShortCode" runat="server" Width="25%" AutoPostBack="True"></asp:TextBox>
                          &nbsp;<asp:TextBox ID="TxtAccNo" runat="server" Width="67%" AutoPostBack="True"></asp:TextBox></td>
                  </tr>
              </table>
              </div>
              <td width="2%">
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  <label>Customer Id</label></td>
              <td>
                  <asp:TextBox ID="TxtCID" runat="server" Width="100%" AutoPostBack="True"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  <label>Name</label></td>
              <td>
                  <asp:TextBox ID="TxtName" runat="server" Width="100%"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  <label>Address</label></td>
              <td>
                  <asp:TextBox ID="TxtAddress" runat="server" Width="100%" Height="58px" 
                      TextMode="MultiLine"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td colspan="2">
              <div>
                  <asp:GridView ID="DgvAcFreezing" runat="server" Width="100%"  CssClass="grid-view"
                      AutoGenerateColumns="False" PageSize="2">
                      <Columns>
                          <asp:BoundField DataField="Br_Name" HeaderText="Branch" >
                          </asp:BoundField>
                          <asp:BoundField DataField="Sch_Name" HeaderText="Scheme" >
                          </asp:BoundField>
                          <asp:BoundField DataField="AccountNo" HeaderText="Account No" >
                          </asp:BoundField>
                          <asp:BoundField DataField="Balance" HeaderText="Balance" >
                              <ItemStyle HorizontalAlign="Right" />
                          </asp:BoundField>
                          <asp:BoundField DataField="Acc_ID" HeaderText="Acc_ID" Visible="False" />
                      </Columns>
                  </asp:GridView>
                  </div>
                  </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  <label>Transaction Type</label></td>
              <td>
                  <asp:DropDownList ID="CmbTransactionType" runat="server" Width="100%">
                      <asp:ListItem>BOTH</asp:ListItem>
                      <asp:ListItem>DR</asp:ListItem>
                      <asp:ListItem>CR</asp:ListItem>
                  </asp:DropDownList>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  <label>Amount</label></td>
              <td>
                  <asp:TextBox ID="TxtAmount" runat="server" Width="100%"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  <label>Intimation date</label></td>
              <td>
                  <asp:TextBox ID="TxtIntimationDate" runat="server" Width="100%"></asp:TextBox>
                  <asp:CalendarExtender ID="TxtIntimationDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                      TargetControlID="TxtIntimationDate">
                  </asp:CalendarExtender>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  <label>Expiry Date</label></td>
              <td>
                  <asp:TextBox ID="TxtExpiryDate" runat="server" Width="100%"></asp:TextBox>
                  <asp:CalendarExtender ID="TxtExpiryDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                      TargetControlID="TxtExpiryDate">
                  </asp:CalendarExtender>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  <label>Reason</label></td>
              <td>
                  <asp:TextBox ID="TxtReason" runat="server" Width="100%"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
                   <tr>
              <td>
                  &nbsp;</td>
              <td>
                  <label>Revoke Reason</label></td>
              <td>
                  <asp:TextBox ID="TxtRevokeReason" runat="server" Width="100%"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td align="center" colspan="2">
                  <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png"  OnClientClick="return ValidateSave();"/>
                  &nbsp;<asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" />
                  &nbsp;<asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td align="center" colspan="2">
              <asp:HiddenField ID="AccId" runat="server" />
                  <asp:HiddenField ID="HdnAlert" runat="server" Value="Please Enter Values" />
              </td>
              <td>
                  &nbsp;</td>
          </tr>
      </table>
     </div>
    </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    
    </asp:Content>
