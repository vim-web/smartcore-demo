<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="Sch_branch_settings.aspx.vb" Inherits="SMARTCORE_UI.Sch_branch_settings" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
   <script language="javascript" type="text/javascript">
  

  </script>

 
  
 
    
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePageMethods="True" CombineScripts="True">
   
</asp:ToolkitScriptManager>


<div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:10%; margin-right:10%; font-family: Tahoma" >   
    <legend>Scheme Branch Settings</legend>   
              <table  class="login-container">
                          <tr>
                            <td width="10%"></td>
                                 <td width="15%">&nbsp;</td>
                                 <td width="25%"></td>
                              <td width="15%"></td>
                                 <td width="25%"></td>
                            <td width="10%"></td>
                           </tr>
                             <%--<tr>
                            <td width="10%">&nbsp;</td>
                                 <td width="15%"><label>Section</label></td>
                                 <td width="25%">
                                     <asp:DropDownList ID="DrpSection" Width="100%" runat="server" 
                                         AutoPostBack="True">
                                     </asp:DropDownList>
                                 </td>
                                 <td width="15%">&nbsp;</td>
                                 <td width="25%">&nbsp;</td>
                            <td width="10%">&nbsp;</td>
                            </tr>--%>
                            <tr>
                                <td width="10%">
                                </td>
                                <td width="15%">
                                    <label>
                                    Scheme</label></td>
                                <td width="25%">
                                    <asp:DropDownList ID="DrpScheme" runat="server" AutoPostBack="True" 
                                        CssClass="ComboBoxDropDown" Height="100%" TabIndex="1" Width="100%">
                                    </asp:DropDownList>
                                </td>
                                <td width="15%">
                                </td>
                                <td width="25%">
                                </td>
                                <td width="10%">
                                </td>
                          </tr>
                            <tr>
                           <td width="10%"></td>
                                 <td width="15%">&nbsp;</td>
                                 <td width="25%"></td>
                                 <td width="15%"></td>
                                <td width="25%"></td>
                            <td width="10%"></td>
                            </tr>
                             <tr>
                       <%--     <td width="14%"></td>--%>
                                 <td colspan="6">
                                     <div style="width:94%; height:400px;overflow:scroll;" align="center">
                                     <asp:GridView ID="GVBranch" runat="server" CssClass="grid-viewNew"   PageSize="3" ShowFooter="True" Width="100%" AutoGenerateColumns="false" >
                                      
                                         <Columns>
                                              <asp:TemplateField >
                                                <HeaderTemplate>
                                                <asp:CheckBox ID="chkHeader"   runat="server"  AutoPostBack="True" Text="Check All" 
                                                        oncheckedchanged="chkHeader_CheckedChanged"  />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                <asp:CheckBox ID="chkChild"  runat="server" 
                                                       />
                                                </ItemTemplate>
                                    <ControlStyle Font-Size="Small" />
                            </asp:TemplateField>
                                              <asp:BoundField DataField="Br_Code" HeaderText="Branch Code" />
                                              <asp:BoundField DataField="Br_Name" HeaderText="Branch Name"  />
                                                  
                                         </Columns>
                                        
                                         
                                      
                                     </asp:GridView>
                                         </div>
                                 </td>
                            <td width="10%"></td>
                            </tr>
                             <tr>
                            <td width="10%" class="auto-style3"></td>
                                 <td width="15%" class="auto-style3"></td>
                                 <td width="25%" align="center" class="auto-style4" colspan="2">
                                      <asp:ImageButton ID="btn_save" runat="server" ImageUrl="~/Images/Save.png"/>
            <asp:ImageButton ID="btn_clear" runat="server" ImageUrl="~/Images/Clear.png" />
                                 </td>
                                 <td width="25%" class="auto-style3"></td>
                            <td width="10%" class="auto-style3"></td>
                            </tr>
                           
                            </table>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>