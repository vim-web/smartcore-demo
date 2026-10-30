<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="CustVenuedetails.aspx.vb" Inherits="SMARTCORE_UI.CustVenuedetails" 
    title="Untitled Page" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
    <script language="javascript" type="text/javascript">
        function ValidateSave() {
            var Ct1 = document.getElementById('<%=Txtname.ClientID%>');
            var Ct2 = document.getElementById('<%=Txtaddress.ClientID%>');
            var Ct3 = document.getElementById('<%=Txtland.ClientID%>');
            var Ct4 = document.getElementById('<%=Txtph.ClientID%>');
            var Ct5 = document.getElementById('<%=Txtcontact.ClientID%>');
            var Ct6 = document.getElementById('<%=Txtmobile.ClientID%>');
            var re = /^[a-zA-Z"-"\$]{3,12}/;
            var va = /^[0-9]{10,12}/;
            if (Ct1.value == "") {
                alert('Enter the name..');
                Ct1.focus();
                return false;
            }
            else if (!re.test(Ct1.value)) {
                alert('Invalid name..');
                Ct1.focus();
                return false;
            }
            else if (Ct2.value == "") {
                alert('Enter the address..');
                Ct2.focus();
                return false;
            }
            else if (Ct3.value == "") {
                alert('Enter the landmark..');
                Ct3.focus();
                return false;
            }
            else if (Ct4.value == "") {
                alert('Enter the phone number..');
                Ct4.focus();
                return false;
            }
            else if (!va.test(Ct4.value)) {
                alert('Invalid phone number..');
                Ct4.focus();
                return false;
            }
            else if (Ct5.value == "") {
                alert('Enter the contact name..');
                Ct5.focus();
                return false;
            }
            else if (!re.test(Ct5.value)) {
                alert('Invalid contact name..');
                Ct5.focus();
                return false;
            }
            else if (Ct6.value == "") {
                alert('Enter the mobile number..');
                Ct6.focus();
                return false;
            }
            else if (!va.test(Ct6.value)) {
                alert('Invalid phone number..');
                Ct6.focus();
                return false;
            }

            else {
                alert('entered successful..');
            }
        }
      </script>
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        .fb8
{
    /*background: transparent url(../../images/buttonround.gif) no-repeat;*/
    font-family: Verdana, Arial, Helvetica, sans-serif;
    border-style: none;
    border-color: inherit;
    border-width: medium;
    outline: none;
    font-weight: bolder;
    font-size: 10px;
    background: blue;
    color: White;
    -webkit-border-radius: 3px;
    -moz-border-radius: 3px;
    border-radius: 3px;
    margin-right: 0px;
 
  background  : -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgb(181,181,181)), color-stop(100%,rgb(100,100,100))); /* Chrome,Safari4+ */
background : #CC6600;
}
        .fb8:hover
{
    border-style: solid;
    color: White;
    background: #FF9900;
    border: none;
    outline: none;
}
    </style>
 
   <script type="text/javascript" src="Scripts/jquery.minphoto.js"></script>
    <style type="text/css">

        .fb8
{
    /*background: transparent url(../../images/buttonround.gif) no-repeat;*/
    font-family: Verdana, Arial, Helvetica, sans-serif;
    border-style: none;
    border-color: inherit;
    border-width: medium;
    outline: none;
    font-weight: bolder;
    font-size: 10px;
    background: #CC6600;
    color: White;
    -webkit-border-radius: 3px;
    -moz-border-radius: 3px;
    border-radius: 3px;
    margin-right: 0px;
 
  background  : -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgb(181,181,181)), color-stop(100%,rgb(100,100,100))); /* Chrome,Safari4+ */
background : #CC6600;
}
        .style1
        {
            width: 41%;
        }
        .style2
        {
            width: 19%;
        }
        .style3
        {
            width: 19%;
            height: 28px;
        }
        .style4
        {
            width: 41%;
            height: 28px;
        }
        </style>

 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
&nbsp;<div style="width: 80%; margin-left:10%; margin-right:10%;margin-top:5%;" >                
    
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Venue Details<table width="100%">
    <tr>
    <td class="style2">
            &nbsp;</td>
        
    <td class="style2">
            <label __designer:mapid="2a">Venue ID</label></td>
        
    <td class="style1">
            <asp:TextBox ID="Txtid" runat="server" Width="240px" AutoPostBack="False" TabIndex="1"></asp:TextBox>
            <asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
                ImageUrl="~/images/Search.png"  Width="23px" />                
        </td>
    </tr>
    <tr>
    <td class="style2">
            &nbsp;</td>
        
    <td class="style2">
            <label __designer:mapid="2a">Venue Name</label></td>
        
    <td class="style1">
            <asp:TextBox ID="Txtname" runat="server" Width="240px" TabIndex="2"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td class="style2">
            &nbsp;</td>
        
    <td class="style2">
            <label __designer:mapid="2a">Venue Address</label></td>
        
    <td class="style1">
        
            <asp:TextBox ID="Txtaddress" runat="server" Height="80px" Width="240px" 
                TabIndex="3" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td class="style2">
            &nbsp;</td>
        
    <td class="style2">
            <label __designer:mapid="2a">Land Mark</label></td>
        
    <td class="style1">
        
            <asp:TextBox ID="Txtland" runat="server" Width="240px" TabIndex="4"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td class="style2">
            &nbsp;</td>
        
    <td class="style2">
            <label __designer:mapid="2a">Phone Number</label></td>
        
    <td class="style1">
        
            <asp:TextBox ID="Txtph" runat="server" Width="240px" TabIndex="5"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td class="style3">
            </td>
        
    <td class="style3">
            <label __designer:mapid="2a">Contact Person</label></td>
        
    <td class="style4">
            <asp:TextBox ID="Txtcontact" runat="server" Width="240px" TabIndex="6"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td class="style2">
            &nbsp;</td>
        
    <td class="style2">
            <label __designer:mapid="2a">Mobile Number</label></td>
        
    <td class="style1">
            <asp:TextBox ID="Txtmobile" runat="server" Width="240px" TabIndex="7"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td class="style2">

        &nbsp;</td>
        
    <td class="style2">

        &nbsp;</td>
        
    <td class="style1">
       </td>
    </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td class="style1">
    <asp:ImageButton ID="CmdSave0" runat="server" ImageUrl="~/images/save.png" 
        meta:resourcekey="CmdSaveResource1" OnClientClick="return ValidateSave();"
        TabIndex="8" />
            &nbsp;<asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/Clear.png"
                  TabIndex="9"/>
           &nbsp;<asp:ImageButton ID="CmdClose0" runat="server" ImageUrl="~/images/Close.png" 
        meta:resourcekey="CmdClearResource1" PostBackUrl="~/Main.aspx" 
        style="margin-top: 0px" TabIndex="10" />
                </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td class="style1">
                &nbsp;</td>
        </tr>
         </table>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    </div>
</asp:Content>

