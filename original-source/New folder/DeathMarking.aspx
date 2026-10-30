<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="DeathMarking.aspx.vb" Inherits="SMARTCORE_UI.DeathMarking" 
    title="Untitled Page" %>
     <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />   
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    

<script type='text/javascript'>
    function ValidateSave() {

        var Ctl = document.getElementById('<%=TxtDeathDate.ClientID%>');
        if (Ctl.value == "") {
            alert('Death Date Not Entered, Please Check');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=Txt_Intimationdate.ClientID%>');
        if (Ctl.value == "") {
            alert('IntimationDate Date Not Entered, Please Check');
            Ctl.focus();
            return false;
        }
        
        return true;
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
    <asp:DropDownList ID="Cmbbranch" runat="server" Width="80%">
    </asp:DropDownList>
    </td>
<td width="2%"></td>
</tr>
<tr>
<td width="2%"></td>
<td width="48%"><label>CustId</label></td>
<td width="48%">
    <asp:TextBox ID="Txt_custid" runat="server" Width="80%" AutoPostBack="True"></asp:TextBox>
    <asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
        ImageUrl="~/images/Search.png" Width="23px" />
    </td>
<td width="2%"></td>
</tr>
<tr>
<td width="2%"></td>
<td width="48%"><label>CustName</label></td>
<td width="48%">
    <asp:TextBox ID="Txt_Name" runat="server" Width="80%"></asp:TextBox>
    </td>
<td width="2%"></td>
</tr>
<tr>
<td width="2%"></td>
<td width="48%"><label>Address</label></td>
<td width="48%">
    <asp:TextBox ID="Txt_Address" runat="server" Width="80%" TextMode="MultiLine"></asp:TextBox>
    </td>
<td width="2%"></td>
</tr>
<tr>
<td width="2%"></td>
<td width="48%"><label>DeathDate</label></td>
<td width="48%">
    </asp:TextBox>
                <asp:TextBox ID="TxtDeathDate" runat="server" OnChange="validatedate(this);" 
                    Width="80%"></asp:TextBox>
                <asp:CalendarExtender ID="TxtDeathDate_CalendarExtender" 
        runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="TxtDeathDate">
                </asp:CalendarExtender>
    </td>
<td width="2%"></td>
</tr>
<tr>
<td width="2%"></td>
<td width="48%"><label>IntimationDate</label></td>
<td width="48%">
                <asp:TextBox ID="Txt_Intimationdate" runat="server" OnChange="validatedate(this);" 
                    Width="80%"></asp:TextBox>
                <asp:CalendarExtender ID="Txt_Intimationdate_CalendarExtender" 
        runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="Txt_Intimationdate">
                </asp:CalendarExtender>
    </td>
<td width="2%"></td>
</tr>
<tr>
<td width="2%"></td>
<td width="48%"><label>Description</label></td>
<td width="48%">
    <asp:TextBox ID="Txt_Description" runat="server" Width="80%" 
        TextMode="MultiLine"></asp:TextBox>
    </td>
<td width="2%"></td>
</tr>
<tr>
<td width="2%"></td>
<td width="48%"></td>
<td width="48%">  </td>
<td width="2%"></td>
</tr>
<tr>
<td width="2%"></td>
<td width="48%" align="center" colspan="2" style="width: 96%">          
       <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" OnClientClick="return ValidateSave();"
                    meta:resourcekey="CmdSaveResource1" TabIndex="56" />
         <asp:ImageButton ID="CmdClear0" runat="server" ImageUrl="~/Images/Clear.png" 
             meta:resourcekey="CmdSaveResource1" TabIndex="36" />
		<asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/images/Close.png" 
                    meta:resourcekey="CmdClearResource1" PostBackUrl="~/Main.aspx" TabIndex="57" />
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
