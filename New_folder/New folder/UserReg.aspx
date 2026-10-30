<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="UserReg.aspx.vb" Inherits="SMARTCORE_UI.UserReg" 
    title="USER EMPLOYEE REGISTRATION" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<script type='text/javascript'>

    function ValidateSave() {




        var Ctl = document.getElementById('<%=TxtUsrName.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('User Name Not Entered, Please Check');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtUsrName.ClientID%>');
        var Size = Ctl.value.length
        if (Size < 4 && Ctl.value.toUpperCase() != "SA") {
            alertify.error('User Name doest not meet minimum length(4), Please Check');
            Ctl.focus();
            return false;
        }      
        
        

        var Ctl = document.getElementById('<%=TxtPassword.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Password Not Entered, Please Check');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtPassword.ClientID%>');
        var Size = Ctl.value.length
        if (Size < 8) {
            alertify.error('Password doest not meet minimum length(8), Please Check');
            Ctl.focus();
            return false;
        }
        
        

        var Ctl = document.getElementById('<%=TxtPassword.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Confirmation Password Not Entered, Please Check');
            Ctl.focus();
            return false;
        }
        
        

        var Ct2 = document.getElementById('<%=TxtPassword.ClientID%>');
        var Ct3 = document.getElementById('<%=TxtConPassword.ClientID%>');

        if (Ct2.value != Ct3.value) {
            alertify.error('  Password Does not Match,  Please Check');
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
                     <asp:DropDownList ID="CmbEmployeeName" runat="server" Enabled="True" 
                         Width="100%" AutoPostBack="True"  >
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
                  CUSTOMER ID</label></td>
              <td>
                   <asp:TextBox ID="TxtCustId" runat="server" Width="100%" ></asp:TextBox>
              </td>
              <td>
                   </td>
          </tr>


          
               <tr>
              <td>
                   </td>
              <td>
                  <label>
                  CUSTOMER ADDRESS</label></td>
              <td>
                   <asp:TextBox ID="TxtCustAddr" runat="server" Width="100%" TextMode="MultiLine"></asp:TextBox>
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
                   </td>
              <td>
                  <label>
                  USER NAME</label></td>
              <td>
                   <asp:TextBox ID="TxtUsrName" runat="server" Width="100%"></asp:TextBox>
              </td>
              <td>
                   </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td>
                  <label>PASSWORD</label></td>
              <td>
                  <asp:TextBox ID="TxtPassword" runat="server" Width="100%" TextMode="Password"></asp:TextBox>
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
                  <asp:TextBox ID="TxtConPassword" runat="server" Width="100%" 
                      TextMode="Password"></asp:TextBox>
              </td>
              <td>
                   </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td>
                  <label>
                  HOME BRANCH</label></td>
              <td>
                  <asp:DropDownList ID="CmbBranch" runat="server" Width="100%" 
                      AutoPostBack="True">
                  </asp:DropDownList>
              </td>
              <td>
                   </td>
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
          <tr>
              <td>
                   </td>
              <td>
                  <label>
                  USER STATUS</label></td>
              <td>
                  <asp:DropDownList ID="CmbUserStatus" runat="server" Width="100%">
                      <asp:ListItem>ACTIVE</asp:ListItem>
                      <asp:ListItem>INACTIVE</asp:ListItem>
                  </asp:DropDownList>
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
                  <label>
                  <asp:HiddenField ID="HdnCustID" runat="server" />
                  </label>
              </td>
              <td>
                   </td>
          </tr>
             <tr>
                 <td>
                     &nbsp;</td>
                 <td align="center" colspan="2">
                     <table class="style1">
                         <tr>
                             <td align="left">
                                 <asp:CheckBox ID="ChkAll" Text="ALL" AutoPostBack="true" runat="server" />
                             </td>
                             <td>
                                 &nbsp;</td>
                         </tr>
                     </table>
                 </td>
                 <td>
                     &nbsp;</td>
             </tr>
             <tr>
                 <td>
                     &nbsp;</td>
                 <td align="center" colspan="2">
                 <div style="min-height:150px;max-height:200px;overflow:scroll;">
                      <asp:GridView ID="GvBranchDetails" runat="server" AutoGenerateColumns="False" 
                        CssClass="grid-viewNew" EnableModelValidation="True" ShowFooter="True" 
                        Width="100%">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="ChkSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Br_Code" HeaderText="Branch Code" />
                            <asp:BoundField DataField="Br_Name" HeaderText="Branch Name" />                            
                            <asp:BoundField DataField="Area_Name" HeaderText="Area" />
                            <asp:BoundField DataField="Region_Name" HeaderText="Region" />
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
                 <td align="center" colspan="2">
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
             </tr>
          <tr>
              <td>
                   </td>
              <td align="center" colspan="2">
                  <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" 
                      OnClientClick="return ValidateSave();" onclick="CmdSave_Click1" />
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
    
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
    
    </asp:Content>
