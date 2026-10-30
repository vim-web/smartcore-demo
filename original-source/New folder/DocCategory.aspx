<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="DocCategory.aspx.vb" Inherits="SMARTCORE_UI.DocCategory" 
title="DOCUMENT CATEGORY" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
    function btnAccept_onclick() {        
        var name;            
        name = document.getElementById('TxtID').value;
        PageMethods.SetName(name, onSuccessMethod,onFailMethod);
        // Call Server side method SetName() by passing this parameter 'name'
    </script>

    <style type="text/css">
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
        .style1
        {
            height: 25px;
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
    <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:ScriptManager ID="ScriptManager1" runat="server"  EnablePageMethods="true">
                </asp:ScriptManager>

    <br/>
    <br/>
    <table width="100%" border="1">
      <tr>
        <td class="TitleBar" WIDTH = "100%">&nbsp;&nbsp;DOCUMENT CATEGORY</td>
      </tr>
    </table>
          
    <table width="100%" border="0">
      <tr>
        <td width="17%"><label>Category ID</label></td>
        <td width="1%"></td>
        <td width="80%">
        <asp:TextBox ID="TxtID" type="text" size="40" maxlength="10"  runat="server" 
                Width="416px" AutoPostBack="True" ></asp:TextBox>       
            <asp:ImageButton ID="ImgSrch" runat="server" Height="20px" Width="24px" 
                ImageUrl="~/images/Search.png" />
            <asp:ImageButton ID="Show" runat="server" Height="20px" Width="24px" 
                ImageUrl="~/images/show.png" />
            
        </td>
        
        <td width="1%"></td>
        <td width="1%"></td>
      </tr>
      
      <tr>
        <td width="17%"><label class="">Category Name</label></td>
        <td width="1%"></td>
        <td width="80%">
        <asp:TextBox ID="TxtName" type="text" size="100" maxlength="100"  runat="server" 
                Width="555px"></asp:TextBox>      
        </td>
        
        <td width="1%"></td>
        <td width="1%"></td>
      </tr>
      <tr>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
      </tr>
      <tr height = "25px">
        <td class="style1"></td>
	    <td class="style1"></td>
        <td class="style1">
            <asp:Button ID="CmdSave" runat="server" CssClass ="ButtonStyle" Text="Save" Width="58px" 
                UseSubmitBehavior="False" Height="20px"/>
            <asp:Button ID="CmdClose" runat="server" CssClass="ButtonStyle" Text="Cancel" Width="63px" 
                UseSubmitBehavior="False" Height="20px"/>
          </td>
        <td class="style1"></td>
        <td class="style1"></td>
      </tr>
      <tr>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
      </tr>
    </table>
    <table width="100%" border="0">
<tr>
<td width = "100%" valign=middle>
  <asp:Panel ID="PnlMsg" runat="server" Visible="False">
      <asp:Image ID="Image1" runat="server" ImageUrl="~/images/errmsg.jpg" />
      &nbsp;&nbsp;<asp:Label ID="LblMsg" runat="server" Font-Bold="True" 
          Font-Names="Bookman Old Style" Font-Size="Medium" ForeColor="Red" ></asp:Label></asp:Panel>
</td></tr>
</table>

            </ContentTemplate>
        </asp:UpdatePanel> 
</div>
</asp:Content>
