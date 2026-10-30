<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="ShareTran.aspx.vb" Inherits="SMARTCORE_UI.ShareTran" 
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

     <div class="gradientBoxesWithOuterShadows" style="width: 50%; margin-left:28%; margin-right:28%;">
<legend>SHARE INACTIVE LIST</legend>   
<table width="100%">
<tr>
<td width="2%"></td>
<td width="48%"><label>Branch</label></td>
<td width="48%">
    <asp:DropDownList ID="CmbBranch" runat="server" Width="80%">
    </asp:DropDownList>
    </td>
<td width="2%"></td>
</tr>
    <tr>
        <td width="2%">
            &nbsp;</td>
        <td width="48%">
            <label>Scheme</label></td>
        <td width="48%">
            <asp:DropDownList ID="CmbScheme" runat="server" Width="80%">
            </asp:DropDownList>
        </td>
        <td width="2%">
            &nbsp;</td>
    </tr>
    <tr>
        <td width="2%">
            &nbsp;</td>
        <td width="48%">
          <label>TranYear</label></td>
        <td width="48%">
            <asp:TextBox ID="Txt_TranYear" runat="server" Width="80%" MaxLength="3"></asp:TextBox>
        </td>
        <td width="2%">
            &nbsp;</td>
    </tr>
    <tr>
        <td width="2%">
            &nbsp;</td>
        <td width="48%"><label>
            AsonDate</label></td>
        <td width="48%">
            <asp:TextBox ID="Txtasondate" runat="server" OnChange="validatedate(this);" 
                Width="80%"></asp:TextBox>
            <asp:CalendarExtender ID="Txtasondate_CalendarExtender" runat="server" 
                Format="dd/MM/yyyy" TargetControlID="Txtasondate">
            </asp:CalendarExtender>
        </td>
        <td width="2%">
            &nbsp;</td>
    </tr>
<tr>
<td width="2%"></td>
<td width="48%" align="center" colspan="2" style="width: 96%">          
       <asp:ImageButton ID="CmdShow" runat="server" ImageUrl="~/Images/Show.png" />
       <asp:ImageButton ID="CmdClose" runat="server" 
           ImageUrl="~/Images/CloseBtn.png" />
		</td>
<td width="2%"></td>
</tr>
<tr>
<td width="2%"></td>
<td width="48%"><asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div ID="blur">
                    </div>
                    <div ID="progress">
                        <img alt="progress" src="Images/progress.gif" /> Please Wait...
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress></td>
<td width="48%">  </td>
<td width="2%"></td>
</tr>
</table>

                  
    </ContentTemplate>
  </asp:UpdatePanel> 

</asp:Content>
