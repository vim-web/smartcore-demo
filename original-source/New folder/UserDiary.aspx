<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="UserDiary.aspx.vb" Inherits="SMARTCORE_UI.UserDiary" 
    title="UserDiary" %>
      <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 

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

                        return false;
                    }
                    if ((lyear == true) && (dd > 29)) {
                        alertify.error('Invalid date format! Please enter a valid date!');

                        return false;
                    }
                }
            }
            else {
                alertify.error("Invalid date format! Please enter a valid date!");

                document.form1.text1.focus();
                return false;
            }
        }
        function ValidateSave() {




            var Ctl = document.getElementById('<%=TxtDate.ClientID%>');
            if (Ctl.value == "") {
                alertify.error('Enter Date...');
                Ctl.focus();
                return false;
            }
            var Ctl = document.getElementById('<%=TxtSubject.ClientID%>');
            if (Ctl.value == "") {
                alertify.error('Enter Subject...');
                Ctl.focus();
                return false;
            }
            var Ctl = document.getElementById('<%=TxtDescription.ClientID%>');
            if (Ctl.value == "") {
                alertify.error('Enter Description...');
                Ctl.focus();
                return false;
            }
        }
</script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
 
     <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;margin-top:5%;" >                
    
    <legend>User Diary</legend>  
    <table width="100%">
    <tr><td width="10%"></td>
    <td width="30%"><label>User Assigned</label></td>
    <td width="40%">
        <asp:DropDownList ID="CmbEmployeeName" runat="server" Enabled="True" 
            Width="100%">
        </asp:DropDownList>
        </td>
    <td width="10%"></td>
    </tr>
    <tr><td width="10%"></td>
    <td width="30%"><label>Date</label></td>
    <td width="40%">
        <asp:TextBox ID="TxtDate" runat="server" BackColor="White" 
            CssClass="twitterStyleTextbox" OnChange="validatedate(this);" TabIndex="4" 
            Width="97%"></asp:TextBox>
        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
            TargetControlID="TxtDate">
        </asp:CalendarExtender>
        </td>
    <td width="10%"></td>
    </tr>
    <tr><td width="10%"></td>
    <td width="30%"><label>Subject</label></td>
    <td width="40%">
        <asp:TextBox ID="TxtSubject" runat="server" Width="97%" CssClass="twitterStyleTextbox"></asp:TextBox>
        </td>
    <td width="10%"></td>
    </tr>
    <tr><td width="10%"></td>
    <td width="30%"><label>Description</label></td>
    <td width="40%">
        <asp:TextBox ID="TxtDescription" runat="server" Width="97%" CssClass="twitterStyleTextbox" TextMode="MultiLine"></asp:TextBox>
        </td>
    <td width="10%"></td>
    </tr>
    <tr><td width="10%"></td>
    <td width="30%"></td>
    <td width="40%"></td>
    <td width="10%"></td>
    </tr>
    <tr><td width="10%"></td>
    <td width="30%" align="center" colspan="2" style="width: 70%">
        <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" 
            onclick="CmdSave_Click1" OnClientClick="return ValidateSave();" />
        <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" />
        <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" PostBackUrl="~/Main.aspx"/>
        </td>
    <td width="10%"></td>
    </tr>
    <tr><td width="10%"></td>
    <td width="30%"></td>
    <td width="40%"></td>
    <td width="10%"></td>
    </tr>
    <tr><td width="10%"></td>
    <td width="30%"></td>
    <td width="40%"></td>
    <td width="10%"></td>
    </tr>
    </table>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    

</asp:Content>

