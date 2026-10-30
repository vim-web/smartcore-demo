<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Smartlogin.aspx.vb" Inherits="SMARTCORE_UI.LoginPage" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>LOGIN</title>
<link href="styles.css" rel="stylesheet" type="text/css" media="screen" />
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    

<style type="text/css">
.TextColor {
	color: #03F;
	font-weight: bold;
	font-size:small;
}
.TextColorNormal {
	color: #03F;
	font-weight:normal;
}
.TextColorRed{
	color:Red;
	font-weight: bold;
}

.ButtonStyle {
	color: #FFF;
	background-color: #039;
	text-align: center;
	display: inline;
        height: 26px;
    }
.CalendarCSS  
{  
    background-color:#3366CC;  
    color:Snow;  
}  

</style>
</head>


<body>
    <form id="form2" runat="server">
<!-- header begins -->
<div id="content" width = "100%">
	<br />
	<br />
	<div id="menu">
    </div>
    
<div id="header">
	<div id="logo">
		<h1><a href="#"></a></h1>
		<h2><a href="#" id="metamorph"></a></h2>
	</div>
</div>
<!-- header ends -->
<!-- content begins -->

<div id="main">
<div id="right">
    <h4></h4>
</div>
<div id="left">
	<h3></h3>
</div>
<div>
    <asp:ToolkitScriptManager ID="LoginToolkitScriptManager" runat="server">
    </asp:ToolkitScriptManager>    
    
    <table width="100%" border="0">
      <tr>
        <td width="58%"></td>
        <td width="2%">&nbsp;</td>
        <td width="15%"></td>
        <td width="25%">
        </td>
      </tr>

      <tr>
        <td ><label class ="TextColorRed">SAFE & SMARTBANK</label></td>
        <td >&nbsp;</td>
        <td ><label class ="TextColor">Branch</label>&nbsp;</td>
        <td >
            <asp:DropDownList ID="CmbBranch" runat="server" Width="100%">
            </asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td rowspan="4" align=justify><label class ="TextColorNormal">
            An ISO 9001-2008 Certified Company (going for ISO 27001 ,Information Security Management System) with More than 140+ Banking & Financial Clients and More than 400+ SW, 500 + HW & 400+NW Site Installation which follows ISO Standards and Systems for the implementation of our Solution with 70+ Committed professional with more than 15 years of Experience in the co-operative banking sector and with Technology Partners like IBM, CISCO, TYCO, Microsoft, APC, Emerson, Reliance , Wipro, Epson etc. 
            We have implemented End-to-end IT Solutions using cutting edge technologies to the delight of our Banking & Financial Clients.
        </label></td>
        <td>&nbsp;</td>
        <td><label class ="TextColor">Login ID</label>&nbsp;</td>
        <td>
            <asp:TextBox ID="TxtLogin" runat="server" Width = "95%" TabIndex="1"  CssClass="twitterStyleTextbox"></asp:TextBox>
        </td>
        
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td><label class ="TextColor">Password</label>&nbsp;</td>
        <td>
            <asp:TextBox ID="TxtPswd" runat="server" TextMode=Password Width = "95%" TabIndex="1"  CssClass="twitterStyleTextbox"></asp:TextBox>
                        </td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td><label class ="TextColor">Login Date</label></td>
        <td >
            <asp:TextBox ID="DtpLoginDate" runat="server"  Width = "95%" TabIndex="2"  CssClass="twitterStyleTextbox"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="DtpLoginDate" Format="dd/MM/yyyy" CssClass="CalendarCSS" >
            </asp:CalendarExtender>
        </td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>        
        <td width="7%" align=right>
        <asp:Button ID="CmbLogin" runat="server" Text="Login" class ="ButtonStyle"  
                Width = "60%" UseSubmitBehavior="False" TabIndex="4"/>
          &nbsp;</td>
        <td width="13%">
            <asp:Button ID="CmdClose" runat="server" Text="Close" class ="ButtonStyle"  
                Width = "30%" UseSubmitBehavior="False" TabIndex="4"/>
                        </td>
        <td>&nbsp;</td>
      </tr>
    </table>
</div>
        
<!--content ends -->
<!--footer begins -->
</div>
<div id="footer">
<p>Copyright &copy; 2012 by <a href="http://www.safenetin.net">Safe Software and Integrated Solutions Pvt Ltd</a></p> 
	<p>&nbsp;</p>
	</div>
</div>
<!-- footer ends-->
    </form>
</body>
</html>
