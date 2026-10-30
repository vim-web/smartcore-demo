<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="BranchDetailsUpdation.aspx.vb" Inherits="SMARTCORE_UI.BranchDetailsUpdation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
 <script language="javascript" type="text/javascript">
     function ValidateSave() {
        
         var Ctl = document.getElementById('<%=txt_Brname.ClientID%>');
         if (Ctl.value == "") {
             alert('Please enter Branch Name');
             Ctl.focus();
             return false;
         }
         var Ctl = document.getElementById('<%=txt_Address.ClientID%>');
         if (Ctl.value == "") {
             alert('Please enter Branch Address');
             Ctl.focus();
             return false;
         }
         
         
        
        
         
     }
     
        </script>
        <script language="javascript" type="text/javascript">
            function onlyNumbers(event) {
                var charCode = (event.which) ? event.which : event.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;

                return true;
            }
    </script>
     <script language="javascript" type="text/javascript">
         function checkEmail() {
             alert("1");
             var email = document.getElementById('<%=txt_EmailID.ClientID%>');
             var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
             if (!filter.test(email.value)) {
                 email.value = '';
                 alertify.error('Please provide a valid email address');
                 return false;
             }
             else
                 return true;
         }
</script>
<script language="javascript" type="text/javascript">

    function validateEmail(field) {
        var regex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,5}$/;
        return (regex.test(field)) ? true : false;
    }
    function validateMultipleEmails(emailcntl, seperator) {
        var value = emailcntl.value;
        if (value != '') {
            var result = value.split(seperator);
            for (var i = 0; i < result.length; i++) {
                if (result[i] != '') {
                    if (!validateEmail(result[i])) {
                        emailcntl.focus();
                        alert('Please provide a valid email address');
                        return false;
                    }
                }
            }
        }
        return true;
    }
   
        
    </script>
    <style type="text/css">
        .style3
        {
            width: 10px;
        }
        .style4
        {
            width: 26px;
        }
        .style9
        {
            width: 117px;
        }
        .style10
        {
            width: 217px;
        }
        .style12
        {
            width: 233px;
        }
        .style13
        {
            width: 164px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%;  float:left; " >                          
    <legend>BRANCH CREATION</legend>
    <table style="width:100%; height: 291px; margin-top: 0px;">
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style13">
                &nbsp;</td>
            <td class="style12">
                &nbsp;</td>
            <td class="style4">
                &nbsp;</td>
            <td class="style9">
                &nbsp;</td>
            <td class="style10">
                &nbsp;</td>
            <td width="10px">
            </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style13">
                &nbsp;</td>
            <td class="style12">
                &nbsp;</td>
            <td class="style4">
                &nbsp;</td>
            <td class="style9">
                &nbsp;</td>
            <td class="style10">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style13">
                &nbsp;</td>
            <td class="style12">
                &nbsp;</td>
            <td class="style4">
                &nbsp;</td>
            <td class="style9">
             <%-- <label>District Code</label></td>--%>
            <td class="style10">
                <%--<asp:TextBox ID="txt_DistCode" runat="server" CssClass="twitterStyleTextbox" AutoPostBack="true"
                    Width="100%"></asp:TextBox>--%>
            </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style13">
                <label>
                Branch Code</label></td>
            <td class="style12">
                <asp:TextBox ID="txt_BrCode" runat="server" CssClass="twitterStyleTextbox" AutoPostBack ="True"
                    Width="100%"></asp:TextBox>
          
            </td>
            <td class="style4">
                <asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
                    ImageUrl="~/images/Search.png" Width="23px" />
            </td>
            <td class="style9">
                <label>
                Branch Name</label></td>
            <td class="style10">
                <asp:TextBox ID="txt_Brname" runat="server" CssClass="twitterStyleTextbox" 
                    Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style13">
                <label>
                Branch Address</label></td>
            <td class="style12">
                <asp:TextBox ID="txt_Address" runat="server" CssClass="twitterStyleTextbox" 
                    Width="100%" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td class="style4">
                &nbsp;</td>
            <td class="style9">
                
                &nbsp;</td>
            <td class="style10">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style3" >
            &nbsp;</td>
            <td class="style13">
                <label>
                Phone 1</label>
            </td>
            <td class="style12">
                <asp:TextBox ID="txt_phone1" runat="server" CssClass="twitterStyleTextbox" MaxLength="12" onkeypress="return onlyNumbers(event)"
                    Width="100%"></asp:TextBox>
            </td>
            <td class="style4">
                &nbsp;</td>
            <td class="style9"><label>Phone 2</label>
            </td>
            <td class="style10">
                <asp:TextBox ID="txt_phone2" runat="server" CssClass="twitterStyleTextbox"  MaxLength="12" onkeypress="return onlyNumbers(event)"
                    Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style13">
                <label>
                E-MailId</label></td>
            <td class="style12">
                <asp:TextBox ID="txt_EmailID" runat="server" CssClass="twitterStyleTextbox" OnChange="return validateMultipleEmails(this,',')"
                    Width="100%"></asp:TextBox>
                     

            </td>
            <td class="style4">
                &nbsp;</td>
            <td class="style9">
                &nbsp;</td>
            <td class="style10">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style3" >&nbsp;</td>
            <td class="style13">
                &nbsp;</td>
            <td class="style12">
                &nbsp;</td>
            <td class="style4">
                &nbsp;</td>
            <td class="style9" >&nbsp;</td>
            <td class="style10">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style13">
                &nbsp;</td>
            <td class="style12">
                &nbsp;</td>
            <td class="style4">
                &nbsp;</td>
            <td class="style9">
                &nbsp;</td>
            <td class="style10">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style13">
                &nbsp;</td>
            <td colspan="4">
                <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" meta:resourcekey="CmdSaveResource1" OnClientClick="return ValidateSave()"/>
                <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" meta:resourcekey="CmdSaveResource1" />
                <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" meta:resourcekey="CmdSaveResource1" />
            </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style13">
                &nbsp;</td>
            <td class="style12">
                &nbsp;</td>
            <td class="style4">
                &nbsp;</td>
            <td class="style9">
                &nbsp;</td>
            <td class="style10">
                &nbsp;</td>
        </tr>
    </table>
    </div> 
    </ContentTemplate>
     
</asp:UpdatePanel> 


</asp:Content>
