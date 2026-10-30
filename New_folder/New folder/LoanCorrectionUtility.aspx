<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="LoanCorrectionUtility.aspx.vb" Inherits="SMARTCORE_UI.LoanCorrectionUtility" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
<script type='text/javascript' src="Scripts/Common.js"></script> 
<script type='text/javascript' src="Scripts/jquery-1.8.3.js"></script> 

<script type='text/javascript'>

    function ValidateSave() {

        var Ctl = document.getElementById('<%=TxtAccNo.ClientID%>');
        if (Ctl.value == "") {
            alert('ACC NO Not Entered, Please Check');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtWardCode.ClientID%>');
        if (Ctl.value == "") {
            alert('Ward Code Not Entered, Please Check');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtGrahanNo.ClientID%>');
        if (Ctl.value == "") {
            alert('Grahan No Not Entered, Please Check');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtReslNo.ClientID%>');
        if (Ctl.value == "") {
            alert('Resl No Not Entered, Please Check');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtExtend.ClientID%>');
        if (Ctl.value == "") {
            alert('Extend Not Entered, Please Check');
            Ctl.focus();
            return false;
        }
         var Ctl = document.getElementById('<%=TxtResurveyNo.ClientID%>');
        if (Ctl.value == "") {
            alert('ResurveyNo Not Entered, Please Check');
            Ctl.focus();
            return false;
        }
         var Ctl = document.getElementById('<%=TxtSurveyNo.ClientID%>');
        if (Ctl.value == "") {
            alert('SurveyNo Not Entered, Please Check');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtReslDate.ClientID%>');
        if (Ctl.value == "") {
            alert('Resl Date Not Entered, Please Check');
            Ctl.focus();
            return false;
        }
        return true;
    }
        </script>

        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        
     <div class="gradientBoxesWithOuterShadows" style="width: 50%; margin-left:25%; margin-right:25%;">
     <legend>LOAN CORRECTION UTILITY</legend>
     <table width="100%">
     <tr>
     <td width="2%"></td>
     <td width="24%"><label>Branch</label></td>
     <td width="24%">
         <asp:DropDownList ID="CmbBranch" runat="server" Width="100%">
         </asp:DropDownList>
         </td>
     <td width="2%"></td>
     <td width="24%">
         <label>
         Scheme</label></td>
     <td width="24%">
         <asp:DropDownList ID="CmbScheme" runat="server" Width="100%">
         </asp:DropDownList>
         </td>
     <td width="2%"></td>
     </tr>

     <tr>
     <td width="2%"></td>
     <td width="24%"><label>Acc No</label></td>
     <td width="24%">
         <asp:TextBox ID="TxtAccNo" runat="server" AutoPostBack="True"></asp:TextBox>
         </td>
     <td width="2%"></td>
     <td width="24%"><label>Purpose</label></td>
     <td width="24%">
         <asp:DropDownList ID="CmbPurpose" runat="server" AutoPostBack="True" 
             Width="100%">
         </asp:DropDownList>
         </td>
     <td width="2%"></td>
     </tr>

     <tr>
     <td width="2%">&nbsp;</td>
     <td width="24%"><label>Name</label></td>
     <td width="24%">
         <asp:TextBox ID="TxtName" runat="server" AutoPostBack="True"></asp:TextBox>
         </td>
     <td width="2%">&nbsp;</td>
     <td width="24%"><label>AccDetails</label></td>
     <td width="24%">
         <asp:TextBox ID="TxtAccDetails" runat="server" AutoPostBack="True" 
             TextMode="MultiLine"></asp:TextBox>
         </td>
     <td width="2%">&nbsp;</td>
     </tr>

         <tr>
             <td width="2%">
             </td>
             <td width="24%">
                 <label>
                 Sub Purpose</label></td>
             <td width="24%">
                 <asp:DropDownList ID="CmbSubpurpose" runat="server" AutoPostBack="True" 
                     Width="100%">
                 </asp:DropDownList>
             </td>
             <td width="2%">
             </td>
             <td width="24%">
                 <label>
                 Ward Code</label></td>
             <td width="24%">
                 <asp:TextBox ID="TxtWardCode" runat="server"></asp:TextBox>
             </td>
             <td width="2%">
             </td>
         </tr>

     <tr>
     <td width="2%"></td>
     <td width="24%"><label>Religion</label></td>
     <td width="24%">
         <asp:DropDownList ID="CmbReligion" runat="server" Width="100%" 
             AutoPostBack="True">
         </asp:DropDownList>
         </td>
     <td width="2%"></td>
     <td width="24%"><label>Caste</label></td>
     <td width="24%">
         <asp:DropDownList ID="CmbCaste" runat="server" Width="100%" AutoPostBack="True">
         </asp:DropDownList>
         </td>
     <td width="2%"></td>
     </tr>

     <tr>
     <td width="2%"></td>
     <td width="24%"><label>Gender</label></td>
     <td width="24%">
         <asp:DropDownList ID="CmbGender" runat="server" Width="100%">
         </asp:DropDownList>
         </td>
     <td width="2%"></td>
     <td width="24%"><label>Grahan No</label></td>
     <td width="24%">
         <asp:TextBox ID="TxtGrahanNo" runat="server"></asp:TextBox>
         </td>
     <td width="2%"></td>
     </tr>

     <tr>
     <td width="2%"></td>
     <td width="24%"><label>Reg Office</label></td>
     <td width="24%">
         <asp:DropDownList ID="CmbRegoff" runat="server" AutoPostBack="True" 
             Width="100%">
             <asp:ListItem Value="0">KAKKODI</asp:ListItem>
             <asp:ListItem Value="1">CHELANNUR</asp:ListItem>
             <asp:ListItem Value="2">WESTHILL</asp:ListItem>
         </asp:DropDownList>
         </td>
     <td width="2%"></td>
     <td width="24%"><label>Resl No</label></td>
     <td width="24%">
         <asp:TextBox ID="TxtReslNo" runat="server"></asp:TextBox>
         </td>
     <td width="2%"></td>
     </tr>

     <tr>
     <td width="2%"></td>
     <td width="24%"><label>Resl Date</label></td>
     <td width="24%">
         <asp:TextBox ID="TxtReslDate" runat="server"></asp:TextBox>
         <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                        TargetControlID="TxtReslDate">
                    </asp:CalendarExtender>
         </td>
     <td width="2%"></td>
     <td width="24%"><label>Recommended Directors</label></td>
     <td width="24%">
         <asp:DropDownList ID="CmbDirectors" runat="server" Width="100%">
         </asp:DropDownList>
         </td>
     <td width="2%"></td>
     </tr>

         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td width="24%">
                <label> Extend</label></td>
             <td width="24%">
                 <asp:TextBox ID="TxtExtend" runat="server"></asp:TextBox>
               
             </td>
             <td width="2%">
                 &nbsp;</td>
             <td width="24%">
                 <label>SurveyNo</label></td>
             <td width="24%">
                
                 <asp:TextBox ID="TxtSurveyNo" runat="server"></asp:TextBox>
                
             </td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td width="24%">
                 <label>DocumentNo</label></td>
             <td width="24%">
                 <asp:TextBox ID="TxtDocNo" runat="server"></asp:TextBox>
               
             </td>
             <td width="2%">
                 &nbsp;</td>
             <td width="24%">
                 <label>ResurveyNo</label></td>
             <td width="24%">
                 <asp:TextBox ID="TxtResurveyNo" runat="server"></asp:TextBox>
               
             </td>
             <td width="2%">
                 &nbsp;</td>
         </tr>

     <tr>
     <td width="2%"></td>
     <td align="center" colspan="5">
         <asp:ImageButton ID="CmdSave" runat="server" OnClientClick="return ValidateSave();" ImageUrl="~/Images/Save.png" />
         &nbsp;<asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" />
         &nbsp;<asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />
         </td>
     <td width="2%"></td>
     </tr>

     <tr>
     <td width="2%"></td>
     <td width="24%">
     <asp:HiddenField ID="HdnAccId" runat="server" />
     </td>
     <td width="24%"></td>
     <td width="2%"></td>
     <td width="24%"></td>
     <td width="24%"></td>
     <td width="2%"></td>
     </tr>
     </table>

     </div>
    </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>

