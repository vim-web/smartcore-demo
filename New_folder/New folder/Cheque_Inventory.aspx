<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="Cheque_Inventory.aspx.vb" Inherits="SMARTCORE_UI.Cheque_Inventory" 
    title="Untitled Page" %>
    
     <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
 <script type='text/javascript' src="Scripts/Common.js"></script>  
    <script type='text/javascript' src="Scripts/jquery-1.8.3.js"></script>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePageMethods="True" CombineScripts="True">
   
</asp:ToolkitScriptManager>

<div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;margin-top:5%">                
    <legend>CHEQUE INVENTORY</legend> 
<table width="100%">
<tr>
<td width="2%">&nbsp;</td>
<td width ="23%">&nbsp;</td>
<td width ="23%">&nbsp;</td>
<td width="4%">&nbsp;</td>
<td width ="23%">&nbsp;</td>
<td width ="23%">                 
    &nbsp;</td>

<td width="2%">&nbsp;</td>

</tr>
    <tr>
        <td width="2%">
        </td>
        <td width="23%">
            <label>
            Branch</label></td>
        <td width="23%">
            <asp:DropDownList ID="CmbBranch" runat="server" Width="100%">
            </asp:DropDownList>
        </td>
        <td width="4%">
        </td>
        <td width="23%">
            <label>
            Register Date</label></td>
        <td width="23%">
            <asp:TextBox ID="txt_date" runat="server" AutoPostBack="True" Width="100%"></asp:TextBox>
            <asp:CalendarExtender ID="txt_date_CalendarExtender" runat="server" 
                Format="dd/MM/yyyy" TargetControlID="txt_date">
            </asp:CalendarExtender>
        </td>
        <td width="2%">
        </td>
    </tr>
<tr>
<td width="2%">&nbsp;</td>
<td width ="23%"><label>Group Name</label></td>
<td width ="23%"><asp:DropDownList ID="CmbGrpName" runat="server" Width="100%" 
        AutoPostBack="True">
            </asp:DropDownList></td>
<td width="4%">&nbsp;</td>
<td width ="23%"><label>Book Type</label></td>
<td width ="23%">
<asp:DropDownList ID="CmbBookType" runat="server" Width="100%" AutoPostBack="True">
            </asp:DropDownList></td>
<td width="2%">&nbsp;</td>

</tr>
<tr>
<td width="2%">&nbsp;</td>
<td width ="23%"><label>No Of Books</label></td>
<td width ="23%">
    <asp:TextBox ID="txt_noofbooks" runat="server" AutoPostBack="True" Width="100%"   onkeypress="return isFloatKey(event);" ></asp:TextBox></td>
<td width="4%">&nbsp;</td>
<td width ="23%">&nbsp;</td>
<td width ="23%">&nbsp;</td>
<td width="2%">&nbsp;</td>

</tr>
    <tr>
        <td width="2%">
            &nbsp;</td>
        <td width="23%"><label>
           Starting Cheque No</label></td>
        <td width="23%">
             <asp:TextBox ID="txt_startcheq" runat="server" AutoPostBack="True" Width="100%"   onkeypress="return isFloatKey(event);" ></asp:TextBox></td>
        <td width="4%">
            &nbsp;</td>
        <td width="23%"><label>
            Ending Cheque No</label></td>
        <td width="23%">
             <asp:TextBox ID="txt_endcheq" runat="server" Enabled="False"  Width="100%"></asp:TextBox></td>
        <td width="2%">
            &nbsp;</td>
    </tr>
    <tr>
        <td width="2%">
            &nbsp;</td>
        <td width="23%">
            &nbsp;</td>
        <td width="23%">
            &nbsp;</td>
        <td width="4%">
            &nbsp;</td>
        <td width="23%">
            &nbsp;</td>
        <td width="23%">
            <asp:ImageButton ID="CmdAdd" runat="server" Height="21px" 
                ImageUrl="~/Images/AddBtn.png" TabIndex="25" />
        </td>
        <td width="2%">
            &nbsp;</td>
    </tr>
    <tr>
        <td width="2%">
            &nbsp;</td>
        <td colspan="5" align="center">
            <asp:GridView ID="GvRpt" runat="server" CssClass="grid-view" Width="90%">
                     <AlternatingRowStyle BackColor="#FBFBFB" />
                 </asp:GridView>
        </td>
        <td width="2%">
            &nbsp;</td>
    </tr>
    <tr>
        <td width="2%">
            &nbsp;</td>
        <td width="23%">
            &nbsp;</td>
        <td width="23%">
            &nbsp;</td>
        <td width="4%">
            &nbsp;</td>
        <td width="23%">
            &nbsp;</td>
        <td width="23%">
            &nbsp;</td>
        <td width="2%">
            &nbsp;</td>
    </tr>
    <tr>
        <td width="2%">
            &nbsp;</td>
        <td width="23%">
            &nbsp;</td>
        <td colspan="3" style="width: 46%" align="center">
            <asp:ImageButton ID="btn_save" runat="server" ImageUrl="~/Images/Save.png" 
                OnClientClick="return ValidateSave();" />
            <asp:ImageButton ID="btn_clear" runat="server" ImageUrl="~/Images/Clear.png" />
            <asp:ImageButton ID="btn_close" runat="server" ImageUrl="~/Images/Close.png" />
        </td>
        <td width="23%">
            &nbsp;</td>
        <td width="2%">
            &nbsp;</td>
    </tr>
    <tr>
        <td width="2%">
            &nbsp;</td>
        <td width="23%">
            &nbsp;</td>
        <td width="23%">
            &nbsp;</td>
        <td width="4%">
            &nbsp;</td>
        <td width="23%">
            &nbsp;</td>
        <td width="23%">
            &nbsp;</td>
        <td width="2%">
            &nbsp;</td>
    </tr>
</table>
</div>
 </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
