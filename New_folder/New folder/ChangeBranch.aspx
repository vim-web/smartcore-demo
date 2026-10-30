
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="ChangeBranch.aspx.vb" Inherits="SMARTCORE_UI.ChangeBranch" 
    title="CHANGE USER BRANCH" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<script type='text/javascript'>

    function ValidateSave() {



   
        
        




        return true;
    }

</script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
      .hidden
     {
         display:none;
     }
    </style>
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    

        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 44%; margin-left:28%; margin-right:28%;">
      <legend>  CHANGE USER BRANCH / DEACTIVE  </legend>
         
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
                      </td>
                 <td width="48%">
                      <asp:RadioButtonList ID="RdoOption" runat="server" AutoPostBack="True">
                          <asp:ListItem Value="C" Selected="True">Change Branch</asp:ListItem>
                          <asp:ListItem Value="D">Active / DeActive</asp:ListItem>
                      </asp:RadioButtonList>
                      </td>
                 <td width="2%">
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
                <tr>
              <td>
                   </td>
              <td>
                  <label>EMP.ID</label></td>
              <td>
                  <asp:TextBox ID="TxtUserEmpId" runat="server" Width="98%" AutoPostBack="True"></asp:TextBox>
              </td>
              <td>
                   <asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
                            ImageUrl="~/images/Search.png" Width="23px" /></td>
          </tr>
          <tr>
              <td>
                   </td>
              <td>
                  <label>
                  USER NAME</label></td>
              <td>
                   <asp:TextBox ID="TxtUserName" Width="98%" runat="server"></asp:TextBox>
              </td>
              <td>
                   </td>
          </tr>
       
             <tr>
                 <td>
                     &nbsp;</td>
                 <td>
                     <label>EMPLOYEE NAME</label></td>
                 <td>
                     <asp:TextBox ID="TxtEmpName" runat="server" Width="98%"></asp:TextBox></td>
                 <td>
                     &nbsp;</td>
             </tr>
       
          <tr>
              <td>
                   </td>
              <td>
                  <asp:Label ID="LblReason" runat="server" Text="REASON FOR BRANCH CHANGE" 
                     CssClass="lblCSS"></asp:Label>
                  </td>
              <td>
                  <asp:TextBox ID="TxtRemarks" runat="server" Width="100%" TextMode="MultiLine"></asp:TextBox>
              </td>
              <td>
                   </td>
          </tr>
          <div id="DivAssign" runat="server" visible="false">
         
             <tr>
                 <td>
                      </td>
                 <td>
                     <label>
                     AUTHORISED IN</label></td>
                 <td>
                     <asp:DropDownList ID="CmbAutBranch" runat="server" Width="100%">
                                              <asp:ListItem>ASSIGNED BRANCH</asp:ListItem>
                         <asp:ListItem>ALL BRANCH</asp:ListItem>
                     </asp:DropDownList>
                 </td>
                 <td>
                      </td>
             </tr>
              </div>
                   <tr>
              <td>
                   </td>
              <td>
                  <asp:Label ID="LblAssignBr" runat="server"  Text="ORIGINAL BRANCH" 
                     CssClass="lblCSS"></asp:Label>
                  </td>
              <td>
                  <asp:DropDownList ID="DrpOrgBranch" Width="100%" runat="server">
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
                  <asp:HiddenField ID="HdnCustID" runat="server" />
              </td>
              <td>
                   </td>
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
                     &nbsp;</td>
                 <td align="center" colspan="2">
                     <table class="style1">
                         <tr>
                             <td align="left">
                                 <asp:CheckBox ID="ChkAll" Text="ALL" runat="server" AutoPostBack="True" />
                                 <asp:CheckBox ID="ChkExisting" runat="server" Text="Existing Branch" 
                                     AutoPostBack="True" />
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
                            <asp:BoundField DataField="Region_Name" HeaderText="Region" >
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                            </asp:BoundField>
                              <asp:BoundField DataField="Is_Active" HeaderText="Is_Active" >
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                            </asp:BoundField>
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
    
    </asp:Content>
