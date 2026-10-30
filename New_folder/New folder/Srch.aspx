<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Srch.aspx.vb" Inherits="SMARTCORE_UI.Srch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SMART SEARCH</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"  EnablePageMethods="true">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
    <ContentTemplate>
    
    <table width ="100%">
        <tr>
            <td width = "15%">
                <label> Search with </label>
            </td>
            <td width = "15%">
                <asp:DropDownList ID="CmbFilter" runat="server" Width="100%" 
                    AutoPostBack="True">                                
                </asp:DropDownList>
            </td>
            <td width = "70%">
                <asp:TextBox ID = "TxtSearch" runat="server" Width = "100%" AutoPostBack="True"></asp:TextBox>
            </td>
        </tr>
                <tr>
            <td>
                <label>  </label>
            </td>
            <td>
                <asp:DropDownList ID="CmbFilter1" runat="server" Width="100%" 
                    AutoPostBack="True">                                
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID = "TxtSearch1" runat="server" Width = "100%" AutoPostBack="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <label>  </label>
            </td>
            <td>
                <asp:DropDownList ID="CmbFilter2" runat="server" Width="100%" 
                    AutoPostBack="True">                                
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID = "TxtSearch2" runat="server" Width = "100%" AutoPostBack="True"></asp:TextBox>
            </td>
        </tr>
    <tr>
        <td colspan = 3>
                <asp:GridView ID="DgvSearch" runat="server" BackColor="#D7FFEF" 
        BorderColor="#9999FF" BorderStyle="Groove" Width="100%" AllowPaging="True" 
                    PageSize="20">
                    <RowStyle BackColor="White" BorderColor="#0066FF" BorderStyle="Solid" 
            BorderWidth="1px" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                    <SelectedRowStyle BackColor="#FF6699" />
                    <HeaderStyle BackColor="#6666FF" ForeColor="White" />
                    <AlternatingRowStyle BackColor="#CCCCFF" />
                </asp:GridView>
        </td>    
    </tr>
    </table>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="TxtSearch" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="CmbFilter" 
                    EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel> 
        
    </div>
    </form>
</body>
</html>
