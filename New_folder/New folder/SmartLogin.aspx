<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SmartLogin.aspx.vb" Inherits="SMARTCORE_UI.SmartLogin" Title="Login" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ OutputCache Duration=5 VaryByParam="None" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>SAFE & SMARTBANK Login</title>
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
<style type="text/css">
#Footer
{
    position: absolute;
    bottom: 0px;
    height: 40px;
    background-color: #F3F3F3;
    vertical-align:middle;
}

.watermarked {   
   color:Silver;   
    border: 1px solid #c4c4c4;
    width: 100%;
    height: 18px;
    font-size: 13px;
    padding: 4px 4px 4px 4px;
    border-radius: 4px;
    -moz-border-radius: 4px;
    -webkit-border-radius: 4px;
    box-shadow: 0px 0px 8px #d9d9d9;
    -moz-box-shadow: 0px 0px 8px #d9d9d9;
    -webkit-box-shadow: 0px 0px 8px #d9d9d9;  
}
.buttonStyle
{
    background-color:#8492F2; background-image: none; border-style:solid; border-width:1; border-color: #CEFBEC;padding: 3px 3px 3px 3px;border-top-left-radius: 5px 5px;border-top-right-radius: 5px 5px;border-bottom-left-radius: 5px 5px;border-bottom-right-radius: 5px 5px;    
}

select 
{
	border: 1px solid #c4c4c4;
    height: 28px;
    font-size: 13px;
    padding: 4px 4px 4px 4px;
    border-radius: 4px;
    -moz-border-radius: 4px;
    -webkit-border-radius: 4px;
    box-shadow: 0px 0px 8px #d9d9d9;
    -moz-box-shadow: 0px 0px 8px #d9d9d9;
    -webkit-box-shadow: 0px 0px 8px #d9d9d9;
}


{
    margin: 0px;
    padding: 0px;
}
label{color:Blue}

    </style>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="CmbLogin">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

    <div>
<table width ="100%"  >
<tr >
    <td width ="35%" height ="1px">   </td>
    <td width ="30%">   </td>
    <td width ="35%">   </td>
</tr>
<tr >
    <td colspan="3" height="125" valign="middle" align="right">   
        <img  src="Images/Logo.jpg" />
    </td>
</tr>

<tr>
    <td colspan="3">
        <img  width ="100%" src="Images/LoginImg1.jpg" 
            style="border-left: thick none #6699FF; border-right: thick none #C0C0C0; border-top: thick solid #DFDFDF; border-bottom: thick solid #9999FF; border-width: 25px thick 15px thick; height: 160px; border-color: #F3F3F3 #C0C0C0 #CECEFF #6699FF;" />
    </td>
</tr>
<tr>
    <td height="180">   
        <asp:ToolkitScriptManager ID="LoginToolkitScriptManager" runat="server">
        </asp:ToolkitScriptManager>       
    </td>
    <td valign="middle">   
    <br />
    <table width="100%">
        <tr>
            <td width = "50%"><label >Branch</label>&nbsp;</td>
            <td width = "45%">
                <asp:TextBox ID="TxtBrCode" runat="server" CssClass="twitterStyleTextbox" 
                    Width="95%" TabIndex="1" AutoPostBack="True" ></asp:TextBox>
                <asp:TextBoxWatermarkExtender ID="TxtBrCode_TextBoxWatermarkExtender" 
                    runat="server" TargetControlID="TxtBrCode" WatermarkCssClass="watermarked" 
                    WatermarkText="Enter Branch Code" />
            </td>  
            <td width="5%"><asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
                    ImageUrl="~/images/Search.png" Width="23px" /></td>          
        </tr>
        <tr>
            <td><label >Login Name</label>&nbsp;</td>
            <td>
            <asp:TextBox ID="TxtLogin" runat="server" Width = "95%" TabIndex="2"  
                    CssClass="twitterStyleTextbox"></asp:TextBox>
            <asp:TextBoxWatermarkExtender ID="TBWE1" runat="server" TargetControlID="TxtLogin" WatermarkText="Enter Login Name" WatermarkCssClass="watermarked" />
            </td>            
        </tr>
        <tr>
            <td><label >Password</label>&nbsp;</td>
            <td>
            <asp:TextBox ID="TxtPswd" runat="server" TextMode="Password" Width = "95%"  
                    TabIndex="3"  CssClass="twitterStyleTextbox" Height="23" ></asp:TextBox>
            <asp:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="TxtPswd" WatermarkText="Enter Password" WatermarkCssClass="watermarked" />
            </td>            
        </tr>
        <tr>
            <td><label >Login Date</label></td>
            <td>
            <asp:TextBox ID="DtpLoginDate" runat="server"  Width = "95%" TabIndex="4"  
                    CssClass="twitterStyleTextbox"></asp:TextBox>
            </td>            
        </tr>
        <tr>
         <td><label >Branch Name</label></td>
            <td>
             
                <asp:TextBox ID="TxtBrName" runat="server" CssClass="twitterStyleTextbox" 
                    TabIndex="5" Width="95%"></asp:TextBox>
             
            <td>
                &nbsp;</td>
        </tr>
        <tr >
            <td height ="1px" colspan="2">   
                <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                <br /></td>
        </tr>

        <tr>
            <td align="right">
                <asp:Button ID="CmbLogin" runat="server" CssClass="buttonStyle" Enabled="False" 
                    Font-Names="Arial Rounded MT Bold" ForeColor="White" TabIndex="6" Text="Login" 
                    UseSubmitBehavior="False" Width="60%" />
            </td>
            <td align="left">
                <asp:Button ID="CmdClose" runat="server" CssClass="buttonStyle" 
                    Font-Names="Arial Rounded MT Bold" ForeColor="White" TabIndex="7" Text="Close" 
                    UseSubmitBehavior="False" Width="60%" />
            </td>
        </tr>
    </table>
    </td>
    <td>   </td>
</tr>
    <tr>
        <td colspan="3" align="center">
            <br />
            <br />
            <br />
            <br />       
            </td>   
    </tr>
</table>
    </div>
</ContentTemplate>
</asp:UpdatePanel> 

    </form>
<div id="Footer" 
        style="width:100%;text-align:center;vertical-align:middle; font-family: 'Arial Rounded MT Bold'; font-size: small; color: #91AFC8; text-decoration: blink; font-weight: lighter; text-transform: none;"> <font style="color: #B1AFE0"> <br />Copyright &copy; 2012 by Safe Software and Integrated Solutions Pvt Ltd</font> </div>

<p>
    </p>

</body>
</html>
