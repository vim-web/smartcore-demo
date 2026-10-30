<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="Sangam_Detls.aspx.vb" Inherits="SMARTCORE_UI.Sangam_Detls" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
<script type='text/javascript' src="Scripts/Common.js"></script> 
<script type='text/javascript' src="Scripts/jquery-1.8.3.js"></script>      
<script type='text/javascript'>

    function ValidateSave() {

        var Ctl = document.getElementById('<%=TxtName.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Name Not Entered, Please Check');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtAddress1.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Address Not Entered, Please Check');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtAddress2.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Address Not Entered, Please Check');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtAddress3.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Address Not Entered, Please Check');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtDate.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Date Not Entered, Please Check');
            Ctl.focus();
            return false;
        }
        return true;
        }
        </script>

        <script language="javascript" type="text/javascript">

            function validatedate(inputText) {
                var dateformat = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
                
                
                // Match the date format through regular expression  
                if (inputText.value.match(dateformat)) {
                    //  document.form1.text1.focus();  
                    //Test which seperator is used '/' or '-'  
                    var opera1 = inputText.value.split('/');
                    var opera2 = inputText.value.split('-');
                    lopera1 = opera1.length;
                    lopera2 = opera2.length;
                    // Extract the string into month, date and year  
                    if (lopera1 > 1) {
                        var pdate = inputText.value.split('/');
                    }
                    else if (lopera2 > 1) {
                        var pdate = inputText.value.split('-');
                    }
                    var dd = parseInt(pdate[0]);
                    var mm = parseInt(pdate[1]);
                    var yy = parseInt(pdate[2]);
                    // Create list of days of a month [assume there is no leap year by default]  
                    var ListofDays = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
                    if (mm == 1 || mm > 2) {
                        if (dd > ListofDays[mm - 1]) {
                            alertify.error('Invalid date format! Please enter a valid date!');
                            inputText.value = "";
                            return false;
                        }
                    }
                    if (mm == 2) {
                        var lyear = false;
                        if ((!(yy % 4) && yy % 100) || !(yy % 400)) {
                            lyear = true;
                        }
                        if ((lyear == false) && (dd >= 29)) {
                            alertify.error('Invalid date format! Please enter a valid date!');
                            inputText.value = "";
                            return false;
                        }
                        if ((lyear == true) && (dd > 29)) {
                            alertify.error('Invalid date format! Please enter a valid date!');
                            inputText.value = "";
                            return false;
                        }
                        
                    }
                }
                
                else {
                    alertify.error("Invalid date format! Please enter a valid date!");
                    inputText.value = "";
                    document.form1.text1.focus();
                    return false;
                }
            }  

</script>

<br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 44%; margin-left:28%; margin-right:28%;">
     <legend>SANGAM DETAILS</legend>
     <table width="100%">
     <tr>
     <td width="2%"></td>
     <td width="48%"><label>Id</label></td>
     <td width="48%">
         <asp:TextBox ID="TxtId" runat="server" AutoPostBack="True" Width="80%"></asp:TextBox>
         &nbsp;<asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
             ImageUrl="~/images/Search.png" Width="23px" />
         </td>
     <td width="2%"></td>
     </tr>
     <tr>
     <td width="2%"></td>
     <td width="48%"><label>Name</label></td>
     <td width="48%">
         <asp:TextBox ID="TxtName" runat="server"></asp:TextBox>
         </td>
     <td width="2%"></td>
     </tr>
     <tr>
     <td width="2%"></td>
     <td width="48%"><label>Address</label></td>
     <td width="48%">
         <asp:TextBox ID="TxtAddress1" runat="server"></asp:TextBox>
         </td>
     <td width="2%"></td>
     </tr>
     <tr>
     <td width="2%"></td>
     <td width="48%"></td>
     <td width="48%">
         <asp:TextBox ID="TxtAddress2" runat="server"></asp:TextBox>
         </td>
     <td width="2%"></td>
     </tr>
     <tr>
     <td width="2%"></td>
     <td width="48%"></td>
     <td width="48%">
         <asp:TextBox ID="TxtAddress3" runat="server"></asp:TextBox>
         </td>
     <td width="2%"></td>
     </tr>
     <tr>
     <td width="2%"></td>
     <td width="48%"><label>Formation Date</label></td>
     <td width="48%">
         <asp:TextBox ID="TxtDate" runat="server" OnChange="validatedate(this);"></asp:TextBox>
         <asp:CalendarExtender ID="TxtDate_CalendarExtender" runat="server" format="dd/MM/yyyy"
             TargetControlID="TxtDate">
         </asp:CalendarExtender>
         </td>
     <td width="2%"></td>
     </tr>
     <tr>
     <td width="2%"></td>
     <td width="48%" align="center" colspan="2" style="width: 96%">
         <asp:ImageButton ID="CmdSave" runat="server" OnClientClick="return ValidateSave();" ImageUrl="~/Images/Save.png" />
         &nbsp;<asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" />
         &nbsp;<asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />
         </td>
     <td width="2%"></td>
     </tr>
     </table>
      </div>
    </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    
    </asp:Content>
