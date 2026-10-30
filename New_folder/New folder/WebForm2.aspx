<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="WebForm2.aspx.vb" Inherits="SMARTCORE_UI.WebForm2" 
    title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script language="javascript" type="text/javascript">        
    function Writetofile() {
        var fso = new ActiveXObject("Scripting.FileSystemObject");
        varFileObject = fso.OpenTextFile("E:\\Sachin.txt", 2, true,0);
        varFileObject.write("File handling in Javascript");
        varFileObject.close();
    }
    function Button1_onclick() {
        var fso = new ActiveXObject("Scripting.FileSystemObject");
        var s = fso.CreateTextFile("lpt1", true);
        s.WriteLine("helloworld");
        s.Close();
    }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <table width = "100%">
	<tr>
		<td width = "2%" > </td>
		<td width = "15%"> 
            <asp:Label ID="Label1" runat="server" Text="Label" BackColor="#3333CC"></asp:Label>
        </td>
		<td width = "49%"> 
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </td>
			<td width = "2%" > </td>
		<td width = "15%"> </td>
		<td width = "15%"> </td>
		<td width = "2%" > </td>
	</tr>	
	<tr>
		<td width = "2%" > &nbsp;</td>
		<td width = "15%"> &nbsp;</td>
		<td width = "49%"> <asp:Button id="cmdSumit" runat="server" OnClientClick="Writetofile()"/>
		</td>
			<td width = "2%" > &nbsp;</td>
		<td width = "15%"> 
            <input id="Button1" type="button" value="button" onclick="return Button1_onclick()" /></td>
		<td width = "15%"> &nbsp;</td>
		<td width = "2%" > &nbsp;</td>
	</tr>	
	<tr>
		<td width = "2%" > &nbsp;</td>
		<td width = "15%"> &nbsp;</td>
		<td width = "49%"> &nbsp;</td>
			<td width = "2%" > &nbsp;</td>
		<td width = "15%"> &nbsp;</td>
		<td width = "15%"> &nbsp;</td>
		<td width = "2%" > &nbsp;</td>
	</tr>	
</table>

</asp:Content>
