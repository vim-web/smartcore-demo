<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="UserRights.aspx.vb" Inherits="SMARTCORE_UI.UserRights" 
    title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="Stylesheet" type="text/css" /> 
    <script type="text/javascript">
        function ValideSave()
        {
            var valid = document.getElementById('<%=TxtUser.ClientID%>');
            if (valid.value.length == 0)
            {
                alertify.error('Please Ender User ID');
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server" ID="ScriptManager"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpPanUser" runat="server">
       
        <ContentTemplate>
           
            <table style="width: 98%; height: 58px;">
                <tr>
                    <td width="40%">
                       </td>
                    <td width="30%" >
                        &nbsp;</td>
                    <td width="30%">
                       </td>
                </tr>
                  <tr>
                    <td >
                        <asp:Label ID="LblUser" runat="server" Text="User ID"  CssClass="lblCSS" style="margin-left:70%" ></asp:Label>
                    </td>
                    <td >
                        <asp:TextBox ID="TxtUser" runat="server" AutoPostBack="true" CssClass="twitterStyleTextbox" 
                            Height="16px" onkeypress="return onlyNumbers(event,'');" Width="80%" 
                            ></asp:TextBox>
                        <%--<asp:ImageButton ID="ImgButUser" runat="server" Height="17px" 
                            ImageUrl="~/Images/Srch.png"  meta:resourcekey="ImgButCustomerResource1"
                             OnClientClick="window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;" 
                            Width="22px" />
                           <asp:Button ID="ButShow" runat="server" CssClass="fb8" Height="16px" 
                            onclientclick="return ValidateShow();" Text="Show" />--%>
                    </td>
                    <td >
                     
                        <asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
                            ImageUrl="~/images/Search.png" Width="23px" />
                     
                    </td>
                </tr>
                 <tr>
                    <td>
                        <asp:Label ID="LblUserName" runat="server" Text="User Name" CssClass="lblCSS" style="margin-left:70%" ></asp:Label>
                     </td>
                    <td>
                        <asp:TextBox ID="TxtUserName" runat="server"  CssClass="twitterStyleTextbox" Enabled="False" Height="16px"   Width="80%"></asp:TextBox>
                     </td>
                    <td></td>
                </tr>
                  <tr>
                    <td>
                        <asp:Label ID="LblGroupId" runat="server" Text="Group ID"  CssClass="lblCSS" style="margin-left:70%" ></asp:Label>
                      </td>
                    <td> 
                        <asp:TextBox ID="TxtGroupId" runat="server"  CssClass="twitterStyleTextbox" Enabled="False" Height="16px" onkeypress="return onlyNumbers(event,'');"  Width="80%"></asp:TextBox>
                      </td>
                    <td></td>
                </tr>
                <tr>
                    <td >
                        <asp:Label ID="LblGrpName" runat="server" Text="Group Name"   CssClass="lblCSS" style="margin-left:70%" ></asp:Label>
                    </td>
                    <td >
                        <asp:TextBox ID="TxtGrpName" runat="server" AutoPostBack="false" CssClass="twitterStyleTextbox" 
                            Enabled="False" Height="16px" onkeypress="return onlyNumbers(event,'');" 
                           Width="80%"></asp:TextBox>
                       
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                  <tr>
                    <td >
                        <asp:Label ID="LblFormType" runat="server" Text="Form Type"   CssClass="lblCSS" style="margin-left:70%" ></asp:Label>
                    </td>
                    <td >
                     <asp:DropDownList ID="CmbFormType" runat="server" Width="70%" AutoPostBack="true">
                </asp:DropDownList>
                       
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>

                <tr>
                    <td valign="top" align="center" colspan="3">
                     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                          <div align="center" style="overflow: scroll; width: 100%; height: 334px;"> 
                      <asp:UpdateProgress ID="updProgress" runat="server" AssociatedUpdatePanelID="UpPanUser" DisplayAfter="1">
                <%-- <ProgressTemplate>
                   <div id="blur"></div>
                   <div id="progress">
                   <img alt="progress" src="../../Images/progress.gif" />Please wait...
                    </div>
                 </ProgressTemplate>--%>
          </asp:UpdateProgress>
                        <asp:GridView ID="GvUser" runat="server" AutoGenerateColumns="False" 
                            CssClass="grid-viewNew"  ShowFooter="True" 
                            Width="100%">
                            <RowStyle  />
                            <Columns>
                                <asp:BoundField DataField="Mnu_ID" HeaderText="Interface ID" />
                                <asp:TemplateField HeaderText="Menu">
                                    <ItemTemplate>
                                        <asp:Label ID="LblMenu" runat="server" Text='<% #Eval("Mnu_Caption") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:DropDownList ID="DropSearch" runat="server" AutoPostBack="true"  Visible="false"
                                               Width="100%" ></asp:DropDownList>
                                    </HeaderTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View ">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkView" runat="server" />
                                       
                                    </ItemTemplate>
                                    <ControlStyle Font-Size="Small" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Modify" Visible="false" >
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkAdd" runat="server" />
                                    </ItemTemplate>
                                    <ControlStyle Font-Size="Small" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View ">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtExistingView" runat="server" Width="100%" Height="100%" 
                                            Font-Size="Small"></asp:TextBox>
                                    </ItemTemplate>
                                    <ControlStyle Font-Size="Small" />
                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Modify"  Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtExistingAdd" runat="server" Width="100%" Height="100%" 
                                            Font-Size="Small"></asp:TextBox>
                                    </ItemTemplate>
                                    <ControlStyle Font-Size="Small" />
                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DrpViewRight" runat="server" Width="100%"  
                                            Font-Size="Small" AutoPostBack="false" onchange="Check_Click(this)">
                                            <asp:ListItem>Inherited</asp:ListItem>
                                            <asp:ListItem>Allow</asp:ListItem>
                                            <asp:ListItem>Deny</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Modify" Visible="false">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DrpAddRight" runat="server" Width="100%" 
                                            Font-Size="Small" AutoPostBack="false" onchange="Check_Click1(this)" >
                                            <asp:ListItem>Inherited</asp:ListItem>
                                            <asp:ListItem>Allow</asp:ListItem>
                                            <asp:ListItem>Deny</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                           
                        </asp:GridView>
                              </div>
                              </ContentTemplate>
                              </asp:UpdatePanel>
                       </td>
                </tr>
              
                <tr>
                    <td colspan="3">
                    <%--<div align="center" 
                        style="overflow: scroll; width: 100%; height: 334px; margin-right: 0px;"> 
                    </div>--%> 
                    </td>
                </tr>
                <tr>
                    <td >
                       </td>
                    <td >
                       <%-- <asp:Button ID="But_Save" runat="server" CssClass="btn" Height="30px"  BorderStyle="None"
                             Text="Save" Width="100px" 
                            onclientclick="return ValideSave();" />
                        <asp:Button ID="But_Reset" runat="server" CssClass="btn" Height="30px"  BorderStyle="None"
                            Text="Reset" Width="100px" />--%>
             <asp:Button ID="Bt_Save" runat="server"  CssClass="btn" Height="30px"  BorderStyle="None"  Text="Save" Width="100px" 
                            onclientclick="return ValideSave();" />
                             <asp:Button ID="Bt_Reset" runat="server"  CssClass="btn" Height="30px"  BorderStyle="None"  Text="Reset" Width="100px" 
                             />
                       </td>
                    <td align="right">
                     
                        
                    </td>
                </tr>
                <tr>
                    <td >
                       </td>
                    <td >
                       </td>
                    <td>
                       </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
