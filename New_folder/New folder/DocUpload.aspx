<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="DocUpload.aspx.vb" Inherits="SMARTCORE_UI.DocUpload" 
    title="Untitled Page" %>
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
    <script language="javascript" type="text/javascript">        

        function isNumberKey(evt)
        {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if(charCode <96 && charCode >105)
            return true;
            if (charCode > 31 && (charCode < 48 || charCode > 57) )
            return false;
            return true;
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br/>
<br/>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  EnablePageMethods="true">
                </asp:ToolkitScriptManager>

<table width="100%" border="1">
  <tr>
    <td class="TitleBar" WIDTH = "100%">&nbsp;&nbsp;DOCUMENT UPLOAD</td>
  </tr>
</table>


<table width="100%" border="0">
  <tr>
  
    <td width = "0%"></td>
    <td width = "20%"><label>Document ID</label></td>
    <td width = "0%"></td>
    <td width = "20%">
        <asp:TextBox ID="TxtDocNo" runat="server" Width = "100%" AutoPostBack="True" onkeypress="return isNumberKey(event);"></asp:TextBox>
    </td>
    <td width = "0%"></td>
    <td width = "20%" align="right"><label>Upload Date</label>&nbsp;&nbsp;</td>
    <td width = "20%"><asp:TextBox ID="TxtDate" runat="server" Width="100%" ></asp:TextBox>
        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TxtDate" Format="dd/MM/yyyy">
        </asp:CalendarExtender>
    </td>
    <td width = "20%"></td>
    
  </tr>
  <tr>
    <td></td>
    <td><label>Document Category</label></td>
    <td></td>
    <td colspan="4">
        <asp:DropDownList ID="CmbCategory" runat="server" Width ="100%">
        </asp:DropDownList>
      </td>
    <td></td>
  </tr>
  <tr>
    <td></td>
    <td><label>Title</label></td>
    <td></td>
    <td colspan="4">
        <asp:TextBox ID="TxtTitle" runat="server" Width="100%"></asp:TextBox>
      </td>
    <td></td>
  </tr>

  <tr>
    <td></td>
    <td></td>
    <td></td>
    <td colspan="4"></td>
    <td></td>
  </tr>
  <tr>
    <td></td>
    <td></td>
    <td></td>
    <td colspan="4"></td>
    <td></td>
  </tr>
  <tr>
    <td></td>
    <td></td>
    <td></td>
    <td colspan="4"></td>
    <td></td>
  </tr>

  <tr>
    <td></td>
    <td><label>Description</label></td>
    <td></td>
    <td rowspan="5" colspan="4">
        <asp:TextBox ID="TxtDescription" runat="server"  Width="100%" Height = "100%" 
            TextMode="MultiLine"></asp:TextBox>
      </td>
    <td></td>
  </tr>
  <tr>
    <td></td>
    <td></td>    
    <td></td>
    <td></td>
  </tr>
  <tr>
    <td></td>
    <td></td>
    <td></td>
    <td></td>
  </tr>
  <tr>
    <td></td>
    <td></td>
    <td></td>
    <td></td>
  </tr>
  <tr>
    <td></td>
    <td></td>
    <td></td>
    <td></td>
  </tr>
  <tr>
    <td></td>
    <td><label>File to Upload</label></td>
    <td></td>
    <td colspan="4"><asp:FileUpload ID="fileUpEx" runat="server" accept="doc/*" /><asp:Label ID="lblStatus" runat="server"></asp:Label></td>
    <td></td>
  </tr>
  <tr>
    <td></td>
    <td><label>Document Access</label></td>    
    <td></td>
    <td colspan="4">
        <asp:DropDownList ID="CmbAccess" runat="server">
        </asp:DropDownList>
      </td>
    <td></td>
  </tr>
  
  <tr>
    <td></td>
    <td></td>
    <td></td>
    <td colspan="4">
        
      </td>
    <td></td>
  </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:HiddenField ID="HdnFileNm" runat="server" />
        </td>
        <td>
            &nbsp;</td>
        <td colspan="4" align=center>
                <asp:Button ID="CmdSave" runat="server" CssClass="ButtonStyle" Text="Save" 
            Width="64px" Height="23px" UseSubmitBehavior="False"/>
        <asp:Button ID="CmdClose" runat="server" CssClass="ButtonStyle" Text="Cancel" Height="23px" 
            Width="64px" UseSubmitBehavior="False" />
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td colspan="4">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td colspan="6">
            <asp:Panel ID="PnlMsg" runat="server" Visible="False">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/errmsg.jpg" />
                &nbsp;&nbsp;<asp:Label ID="LblMsg" runat="server" Font-Bold="True" 
                    Font-Names="Bookman Old Style" Font-Size="Medium" ForeColor="Red"></asp:Label>
            </asp:Panel>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td colspan="4">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
          </ContentTemplate>
               <Triggers>
                <asp:PostBackTrigger ControlID="CmdSave" />
            </Triggers>
        </asp:UpdatePanel> 

</asp:Content>
