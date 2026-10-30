<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="AgentAssignment.aspx.vb" Inherits="SMARTCORE_UI.AgentAssignment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
<script type='text/javascript' src="Scripts/Common.js"></script> 
<script type='text/javascript' src="Scripts/jquery-1.8.3.js"></script>  

<%--<script type='text/javascript'>
    //    $(function() {
    //    var dropDownList1 = $('select[id$=' + '<%=CmbScheme.ClientID%>' + ']');
    //        dropDownList1.removeAttr('onchange');
    //        dropDownList1.change(function(e) {
    //            if (this.value != 0) {
    //                setTimeout('__doPostBack(\'CmbScheme\',\'\')', 0);
    //                alert('hai josu');
    //            }
    //        });
    //    });

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
                alert('Account No Not Entered, Please Check');
                Ctl.focus();
                return false;
            }
        }
        else {
            var Ctl = document.getElementById('<%=TxtCID.ClientID%>');
            if (parseFloat(Ctl.value) == 0 || Ctl.value == "") {
                alert('Customer ID Not Entered, Please Check');
                Ctl.focus();
                return false;
            }
        }

        var Ctl = document.getElementById('<%=TxtIntimationDate.ClientID%>');
        if (Ctl.value == "") {
            alert('Intimation Date Not Entered, Please Check');
            Ctl.focus();
            return false;
        }
        var Tr = document.getElementById('<%=HdnTrDate.ClientID%>');

        if (new Date(Ctl.value) > new Date(Tr.value)) {
            alert('Intimation Date Should be Less Than or Equal To Transaction Date');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtReason.ClientID%>');
        if (Ctl.value == "") {
            alert('Reason Not Entered, Please Check');
            Ctl.focus();
            return false;
        }

        return true;
    }

</script>--%>

        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;">
          
          <table width="100%">
          <tr>
          <td width="2%"></td>
          <td width="48%"><label>Br Code</label></td>
          <td width="48%">
              <asp:DropDownList ID="CmbBrCode" runat="server" Width="100%">
              </asp:DropDownList>
              </td>
          <td width="2%"></td>
          </tr>
          <tr>
          <td width="2%"></td>
          <td width="48%"><label>Scheme</label></td>
          <td width="48%">
              <asp:TextBox ID="TxtScheme" runat="server" AutoPostBack="True" Width="23%"></asp:TextBox>
              &nbsp;<asp:DropDownList ID="CmbScheme" runat="server" Width="70%" 
                  AutoPostBack="True">
              </asp:DropDownList>
              </td>
          <td width="2%"></td>
          </tr>
          <tr>
          <td width="2%"></td>
          <td width="48%"><label>Acc No</label></td>
          <td width="48%">
              <asp:TextBox ID="TxtShortCode" runat="server" Width="20%" AutoPostBack="True"></asp:TextBox>
              &nbsp;<asp:TextBox ID="TxtAccNo" runat="server" Width="64%" AutoPostBack="True"></asp:TextBox>
              &nbsp;<asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
                  ImageUrl="~/images/Search.png" Width="23px" />
              </td>
          <td width="2%"></td>
          </tr>
          <tr>
          <td width="2%"></td>
          <td width="48%"><label>Name</label></td>
          <td width="48%">
              <asp:TextBox ID="TxtName" runat="server" AutoPostBack="True"></asp:TextBox>
              </td>
          <td width="2%"></td>
          </tr>
          <tr>
          <td width="2%"></td>
          <td width="48%"><label>Address</label></td>
          <td width="48%">
              <asp:TextBox ID="TxtAddress" runat="server" AutoPostBack="True" 
                  TextMode="MultiLine" Width="100%"></asp:TextBox>
              </td>
          <td width="2%"></td>
          </tr>
          <tr>
          <td width="2%"></td>
          <td width="48%"><label>Agent</label></td>
          <td width="48%">
              <asp:DropDownList ID="CmbAgent" runat="server" Width="100%">
              </asp:DropDownList>
              </td>
          <td width="2%"></td>
          </tr>
              <tr>
                  <td width="2%">
                      &nbsp;</td>
                  <td align="center" colspan="2" style="width: 96%" width="48%">
                      <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                          meta:resourcekey="CmdSaveResource1" TabIndex="56" />
                      &nbsp;<asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" />
                      &nbsp;<asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />
                  </td>
                  <td width="2%">
                      &nbsp;</td>
              </tr>
              <tr>
                  <td width="2%">
                      &nbsp;</td>
                  <td align="center" colspan="2" style="width: 96%" width="48%">
                  <asp:HiddenField ID="HdnAccId" runat="server" />
                      &nbsp;</td>
                  <td width="2%">
                      &nbsp;</td>
              </tr>
          </table>

          </div>
    </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    
    </asp:Content>