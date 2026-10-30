<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="CustomerDiseaseRpt.aspx.vb" Inherits="SMARTCORE_UI.CustomerDiseaseRpt" 
    title="Untitled Page" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePageMethods="True" CombineScripts="True">
   
</asp:ToolkitScriptManager>

     <div class="gradientBoxesWithOuterShadows" style="width: 50%; margin-left:28%; margin-right:28%;">
<legend>DEATH MARKING</legend>   
<table width="100%">
<tr>
<td width="2%"></td>
<td width="48%"><label>Branch</label></td>
<td width="48%">
    <asp:DropDownList ID="ddlBranch" runat="server" Width="80%">
    </asp:DropDownList>
    </td>
<td width="2%"></td>
</tr>
<tr>
<td width="2%"></td>
<td width="48%"><label>Asondate</label></td>
<td width="48%">
    <asp:TextBox ID="Txtasondate" runat="server" OnChange="validatedate(this);" 
        Width="80%"></asp:TextBox>
    <asp:CalendarExtender ID="Txtasondate_CalendarExtender" runat="server" 
        Format="dd/MM/yyyy" TargetControlID="Txtasondate">
    </asp:CalendarExtender>
    </td>
<td width="2%"></td>
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
<td width="48%"></td>
<td width="48%">  </td>
<td width="2%"></td>
</tr>
</table>

                  
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
</asp:Content>
