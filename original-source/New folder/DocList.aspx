<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="DocList.aspx.vb" Inherits="SMARTCORE_UI.DocList" 
    title="Document List" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css" >
        label {
	        color:  #03F;
	        font-weight: bold;
        }
        .ButtonStyle {
	        color: #FFF;
	        background-color: #039;
	        text-align: center;
	        display: inline;	        
        }
        .TitleBar {
	        background-color: #33F;
	        border-top-color: #39F;
	        border-right-color: #39F;
	        border-bottom-color: #39F;
	        border-left-color: #39F;
	        padding: 2px;
	        font-size: 12px;
	        font-weight: bold;
	        color: #FFF;
	        text-decoration: blink;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
<br />
<br />
<table width="100%" border="1">
  <tr>
    <td class="TitleBar" WIDTH = "100%">&nbsp;&nbsp;DOCUMENT LIST</td>
  </tr>
</table>

    <br />
    <table width="100%">
    <tr>
    <td width="30%" ></td>
    <td width="40%" align="center">
    <asp:Image ID="Image1" runat="server" ImageAlign=Middle
        ImageUrl="~/images/DocImg.jpg" Width="180px" />    
    </td>
    <td width="30%"></td>
    </tr>
    </table>
    
    <br />
    
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  EnablePageMethods="true">
                </asp:ToolkitScriptManager>
    
    
    <table width="100%">
    <tr>
    <td width="30%" >
            <table width="100%">
                <tr>
                    <td width="35%">
                            <asp:CheckBox ID="ChkFilterCategory" runat="server" AutoPostBack="true" 
                                Text="Filter Category" />
                                <br />
                            <asp:DropDownList ID="CmbCategory" runat="server" Width="100%" 
                                AutoPostBack="true" 
                                OnSelectedIndexChanged ="CmbCategory_SelectedIndexChanged">
                            </asp:DropDownList>
                    </td>

                    <td width="35%">
                            <asp:CheckBox ID="ChkFilterSection" runat="server" AutoPostBack="true" 
                                Text="Filter Section" />
                                <br />
                            <asp:DropDownList ID="CmbSection" runat="server" Width="100%" 
                                AutoPostBack="true" 
                                OnSelectedIndexChanged ="CmbCategory_SelectedIndexChanged">
                            </asp:DropDownList>                    
                    </td>
                    <td width="30%">
                        <asp:CheckBox ID="ChkFilterDate" runat="server" AutoPostBack="true" 
                            Text="Filter Date" />
                            <br />
                        <asp:TextBox ID="TxtDate" runat="server" Width="100%" ></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TxtDate" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GridView1" runat="server" Width="100%">
            </asp:GridView>
        <br />
    </td>
    </tr>
    </table>
        
    </table>
    
             </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="CmbCategory" 
                            EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="CmbSection" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
        </asp:UpdatePanel> 
    <br />
    <br />
    
</asp:Content>
