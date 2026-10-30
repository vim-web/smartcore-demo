<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="UserEmployee.aspx.vb" Inherits="SMARTCORE_UI.UserEmployee" 
    title="USER EMPLOYEE REGISTRATION" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<script type='text/javascript'>

    function ValidateSave() {

     
        



        var Ctl = document.getElementById('<%=TxtEmpID.ClientID%>');
        if (Ctl.value == "") {
            alert(' Employee ID Not Set, Please Check');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtCustId.ClientID%>');
        if (Ctl.value == "") {
            alert(' Customer Details ID Not Set, Please Check');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtDOJ.ClientID%>');
        if (Ctl.value == "") {
            alert(' Date Not Set, Please Check');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtDOR.ClientID%>');
        if (Ctl.value == "") {
            alert(' Date Not Set, Please Check');
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
      <legend>    USER EMPLOYEE REGISTRATION  </legend>
         
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
                 <td width="2%">
                 </td>
                 <td width="48%">
                     <label>
                     EMPLOYEE ID</label></td>
                 <td width="48%">
                     <asp:TextBox ID="TxtEmpID" runat="server" AutoPostBack="True" Width="100%"></asp:TextBox>
                 </td>
                 <td width="2%">
                 </td>
             </tr>
          <tr>
              <td>
                   </td>
              <td>
                  <label>
                  DESIGNATION</label></td>
              <td>
                  <asp:DropDownList ID="CmbDesignation" runat="server" Enabled="True" 
                      Width="100%">
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
                     EMPLOYEE TYPE</label></td>
                 <td>
                     <asp:DropDownList ID="CmbEmpType" runat="server" Width="100%">
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
                  CUSTOMER ID</label></td>
              <td>
                   <asp:TextBox ID="TxtCustId" runat="server" AutoPostBack="True" Width="80%"></asp:TextBox>
                   <asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
                       ImageUrl="~/images/Search.png" Width="23px" />
              </td>
              <td>
                   </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td>
                  <label>NAME</label></td>
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
                  <label>ADDRESS</label></td>
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
                  <label>DATE OF JOINING</label></td>
              <td>
                  <asp:TextBox ID="TxtDOJ" runat="server" Width="100%"></asp:TextBox>
                  <asp:CalendarExtender ID="txtrcptdate_CalendarExtender" runat="server" 
                      Format="dd/MM/yyyy" TargetControlID="TxtDOJ">
                  </asp:CalendarExtender>
              </td>
              <td>
                   </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td>
                  <label>
                  DATE OF RETIREMENT</label></td>
              <td>
                  <asp:TextBox ID="TxtDOR" runat="server" Width="100%"></asp:TextBox>
                  <asp:CalendarExtender ID="TxtDOR_CalendarExtender" runat="server" 
                      Format="dd/MM/yyyy" TargetControlID="TxtDOR">
                  </asp:CalendarExtender>
              </td>
              <td>
                   </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td>
                   </td>
              <td>
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
