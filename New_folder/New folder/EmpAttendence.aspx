<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="EmpAttendence.aspx.vb" Inherits="SMARTCORE_UI.EmpAttendence" 
    title=" EMPLOYEE ATTENDENCE MARKING" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<script type='text/javascript'>

    function ValidateSave() {







        return true;
    }

</script>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    

        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 44%; margin-left:28%; margin-right:28%; margin-top:10%">
      <legend>   EMPLOYEE ATTENDENCE MARKING  </legend>
         
         <table width="100%">
          <tr>
              <td width="2%">
                   &nbsp;</td>
              <td width="48%">
                  &nbsp;</td>
              <td width="48%">
                  &nbsp;</td>
              <td width="2%">
                   &nbsp;</td>
          </tr>
             <tr>
                 <td width="2%">
                 </td>
                 <td width="48%">
                     <label>BRANCH</label></td>
                 <td width="48%">
                     <asp:DropDownList ID="CmbBranch" runat="server" Width="100%" 
                         AutoPostBack="True">
                     </asp:DropDownList>
                 </td>
                 <td width="2%">
                 </td>
             </tr>
             <tr>
                 <td width="2%">
                 </td>
                 <td width="48%">
                     <label>
                     EMPLOYEE NAME</label></td>
                 <td width="48%">
                     <asp:DropDownList ID="CmbEmployeeName" runat="server" Enabled="True" 
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
                     <label>
                     EMPLOYEE STATUS</label></td>
                 <td>
                     <asp:DropDownList ID="CmbEmpStatus" runat="server" Width="100%" 
                         AutoPostBack="True">
                         <asp:ListItem>PRESENT</asp:ListItem>
                         <asp:ListItem>ABSENT</asp:ListItem>
                     </asp:DropDownList>
                 </td>
                 <td>
                     </td>
             </tr>
          <tr>
              <td>
                   </td>
              <td colspan="2">
              <div id="DivAbsent" runat ="server" visible="false"   >
              
              <table width ="100%">
              <tr>
              <td width ="50%">
              
              </td>
              <td width ="50%">
              
              
              </td>
              
              </tr>
              <tr>
              <td>
              
                  <label>
                  LEAVE TYPE</label></td>
              <td>
              
              
                  <asp:DropDownList ID="CmbLeaveType" runat="server" Width="100%">
                  </asp:DropDownList>
              
              
              </td>
              
              </tr>
              <tr>
              <td>
              
                  <label>
                  REASON FOR LEAVE</label></td>
              <td>
              
              
                  <asp:TextBox ID="TxtReason" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
              
              
              </td>
              
              </tr>
              
              </table>
              
              </div>
              
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
                     &nbsp;</td>
                 <td colspan="2">

                 
                 
                 
                     <asp:GridView ID="Dgvemp" runat="server" AllowPaging="True" 
                         AutoGenerateColumns="False" DataKeyNames="Emp_Id,Emp_Name" PageSize="50" TabIndex="8" 
                         Width="100%">
                         <RowStyle Height="20px" />
                         <Columns>
                          
                             <asp:BoundField DataField="Emp_Id" HeaderText="EmpId">
                                 <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                     BorderWidth="1px" ForeColor="White" Width="42%" />
                                 <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                     Font-Size="Small" />
                             </asp:BoundField>
                             <asp:BoundField DataField="Emp_Name" HeaderText="Name">
                                 <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                     BorderWidth="1px" ForeColor="White" Width="42%" />
                                 <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                     Font-Size="Small" />
                             </asp:BoundField>
                             <asp:BoundField DataField="Status" HeaderText="Status">
                                 <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                     BorderWidth="1px" ForeColor="White" Width="42%" />
                                 <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                     Font-Size="Small" />
                             </asp:BoundField>
                         </Columns>
                         <AlternatingRowStyle BackColor="#F5F4FF" />
                     </asp:GridView>
                 
                 
                 
                  </td>
                 <td>
                     &nbsp;</td>
             </tr>
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
    
    </asp:Content>
