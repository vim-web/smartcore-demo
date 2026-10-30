<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="DepBalStatement.aspx.vb" Inherits="SMARTCORE_UI.DepBalStatement" 
    title="Account Balance Statement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">





select 
{
	border: 1px solid #c4c4c4;
    font-size: 13px;
    padding: 4px 4px 4px 4px;
    border-radius: 4px;
    -moz-border-radius: 4px;
    -webkit-border-radius: 4px;
    box-shadow: 0px 0px 8px #d9d9d9;
    -moz-box-shadow: 0px 0px 8px #d9d9d9;
    -webkit-box-shadow: 0px 0px 8px #d9d9d9;
}
    
label {
	color: #03F;

}
    </style>
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
           <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                        <Scripts>
                            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
                        </Scripts>
        </asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 70%; margin-left:15%; margin-right:15%;">
        &nbsp;Account Balance Statement</legend>
        <table width = "100%">
    <tr>
        <td width = "10%"> </td>
        <td width = "40%">  
                <label> Branch </label>
        </td>
        <td width = "40%">  
                <asp:DropDownList ID="DdlBranch" runat="server" Width="98%">
                </asp:DropDownList>
        </td>
        <td width = "10%"> </td>
    </tr>

    <tr>
        <td> </td>
        <td>  
                <label> Scheme </label>
        </td>
        <td>  
                <asp:DropDownList ID="DdlScheme" runat="server" Width="98%">
                </asp:DropDownList>
        </td>
        <td> </td>
    </tr>
    <tr>
        <td> </td>
        <td> 
                      <label __designer:mapid="93">Account No</label></td>
        <td> 
                      <asp:TextBox ID="TxtAccNo" runat="server" Width="100%" AutoPostBack="True" 
                          TabIndex="3"></asp:TextBox> </td>
        <td> </td>
    </tr>
        <tr>
        <td> </td>
        <td> 
              <label __designer:mapid="7f">
              Customer Name</label></td>
        <td> 
              <asp:TextBox ID="TxtCustName" runat="server" Width="100%" TabIndex="500"></asp:TextBox>
            </td>
        <td> </td>
    </tr>
    
        <tr>
        <td> &nbsp;</td>
        <td valign="top">  
              <label __designer:mapid="7f">
              Address</label></td>
        <td> 
                      <label __designer:mapid="8d">
                      <asp:TextBox ID="TxtAddress" runat="server" Height="60px" 
                          TextMode="MultiLine" Width="100%" TabIndex="500"></asp:TextBox></label>
                  </td>
        <td> &nbsp;</td>
    </tr>
    
        <tr>
        <td> &nbsp;</td>
        <td>  
              <label __designer:mapid="7f">
              Ason Date</label></td>
        <td> 
                      <asp:TextBox ID="TxtDate" runat="server" Width="100%"></asp:TextBox>
                      <asp:CalendarExtender ID="TxtDate_CalendarExtender" runat="server" 
                          Format="dd/MM/yyyy" TargetControlID="TxtDate">
                      </asp:CalendarExtender>
                  </td>
        <td> &nbsp;</td>
    </tr>
    
        <tr>
        <td> &nbsp;</td>
        <td colspan="2" align="center">  
                  <asp:ImageButton ID="CmdShow" runat="server" Height="25px" 
                      ImageUrl="~/Images/Show.png" />
                  <asp:ImageButton ID="CmdClose" runat="server" 
                      ImageUrl="~/Images/CloseBtn.png" />
              </td>
        <td> &nbsp;</td>
    </tr>
    
        <tr>
        <td> &nbsp;</td>
        <td>  
              &nbsp;</td>
        <td> 
                      <asp:HiddenField ID="HdnAccid" runat="server" />
            </td>
        <td> &nbsp;</td>
    </tr>
    
</table>
</div>

</asp:Content>