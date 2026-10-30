<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="LoginUserStatus.aspx.vb" Inherits="SMARTCORE_UI.LoginUserStatus" 
    title="Untitled Page" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePageMethods="True" CombineScripts="True">
   
</asp:ToolkitScriptManager>


<div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;" >                
    <legend>UserLogin</legend> 
    <table width="100%">
        <tr> <td width="50%">&nbsp;</td>
            <td width="50%">
                &nbsp;</td>

        </tr>
        <tr>
            <td width="50%"  colspan="2" style="width: 100%">
                <asp:GridView ID="Dgvlogin" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" DataKeyNames="Emp_ID,User_Name" PageSize="5" TabIndex="8" 
                    Width="100%">
                    <RowStyle Height="20px" />
                    <Columns>
                        <asp:CommandField SelectText="Logout" ShowSelectButton="True">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" Width="5%" />
                        </asp:CommandField>
                        <%-- <asp:CheckBoxField DataField="Revoked">
                        <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" Width="2%" />
                        </asp:CheckBoxField>  --%>
                        <asp:BoundField DataField="Emp_ID" HeaderText="Id" Visible="False">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" ForeColor="White" Width="42%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="User_Name" HeaderText="UserName">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" ForeColor="White" Width="42%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Emp_Name" HeaderText="EmpName">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" ForeColor="White" Width="42%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" />
                        </asp:BoundField>
                    </Columns>
                    <AlternatingRowStyle BackColor="#F5F4FF" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td width="50%" align="center" colspan="2" style="width: 100%">
                
                <asp:ImageButton ID="CmdClose" runat="server" 
                    ImageUrl="~/Images/CloseBtn.png" />
                
            </td>
        </tr>
    </table>
    
    
                  
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
