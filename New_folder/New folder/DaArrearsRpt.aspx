<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="DaArrearsRpt.aspx.vb" Inherits="SMARTCORE_UI.DaArrearsRpt" 
    title="DA Arrears" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                        alert('Invalid date format! Please enter a valid date!');

                        return false;
                    }
                }
                if (mm == 2) {
                    var lyear = false;
                    if ((!(yy % 4) && yy % 100) || !(yy % 400)) {
                        lyear = true;
                    }
                    if ((lyear == false) && (dd >= 29)) {
                        alert('Invalid date format! Please enter a valid date!');

                        return false;
                    }
                    if ((lyear == true) && (dd > 29)) {
                        alert('Invalid date format! Please enter a valid date!');

                        return false;
                    }
                }
            }
            else {
                alert("Invalid date format! Please enter a valid date!");

                document.form1.text1.focus();
                return false;
            }
        }
        function validateshow() {
            var a = document.getElementById('<%=TxtID.ClientID %>');
            if (a.value =="") {
                alert('Please Enter Employee ID');
                return false;
            }
            var b = document.getElementById('<%=TxtRate.ClientID %>');
            if (b.value == "") {
                alert('Please Enter DA Rate');
                return false;
            }
            var C = document.getElementById('<%=Txt_DateFrom.ClientID %>');
            if (C.value == "") {
                alert('Please Enter From Date');
                return false;
            }
            var d = document.getElementById('<%=Txt_DateTo.ClientID %>');
            if (d.value == "") {
                alert('Please Enter From Date');
                return false;
            }
        }
      
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>



<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    

        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 44%; margin-left:28%; margin-right:28%;">
     <legend>&nbsp;DA Arrears Report </legend>
         <table Width="75%">
             <tr>
                 <td width="2%">
                 </td>
                 <td width="48%">
                 <label>                     Employee ID</label>
                 </td>
                 <td width="48%">
                     <asp:TextBox ID="TxtID" runat="server" AutoPostBack="True" 
                         CssClass="twitterStyleTextbox" Width="75%"></asp:TextBox>
                     <asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
                         ImageUrl="~/images/Search.png" Width="23px" />
                 </td>
                 <td width="2%">
                     &nbsp;</td>
             </tr>
             
          
             
             <tr>
                 <td width="2%">
                     </td>
                 <td width="48%">
                 <label>Employee Name</label>
                     </td>
                 <td width="48%">
                     <asp:TextBox ID="TxtName" runat="server" CssClass="twitterStyleTextbox" 
                         TabIndex="1" Width="75%"></asp:TextBox>
                 </td>
                 <td width="2%">
                     </td>
             </tr>
             <tr>
                 <td width="2%">
                     </td>
                 <td width="48%"><label>From Date</label>
                     </td>
                 <td width="48%">
                     <asp:TextBox ID="Txt_DateFrom" runat="server" BackColor="White" 
                         CssClass="twitterStyleTextbox" OnChange="validatedate(this);" TabIndex="4" 
                         Width="75%"></asp:TextBox>
                     <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                         TargetControlID="Txt_DateFrom">
                     </asp:CalendarExtender>
                 </td>
                 <td width="2%">
                     </td>
             </tr>
                    <tr>
                 <td width="2%">
                     </td>
                 <td width="48%"><label>To Date</label>
                     </td>
                 <td width="48%">
                     <asp:TextBox ID="Txt_DateTo" runat="server" BackColor="White" 
                         CssClass="twitterStyleTextbox" OnChange="validatedate(this);" TabIndex="4" 
                          Width="75%"></asp:TextBox>
                     <asp:CalendarExtender ID="Txt_DateTo_CalendarExtender" runat="server" 
                         Format="dd/MM/yyyy" TargetControlID="Txt_DateTo">
                     </asp:CalendarExtender>
                        </td>
                 <td width="2%">
                     </td>
             </tr>
                 <tr>
                 <td width="2%">
                     </td>
                 <td width="48%"><label> DA Rate</label>
                     </td>
                 <td width="48%">
                     <asp:TextBox ID="TxtRate" runat="server" 
                         CssClass="twitterStyleTextbox" Width="75%"></asp:TextBox>
                     </td>
                 <td width="2%">
                     </td>
             </tr>
             <tr>
                 <td>
                 </td>
                 <td align="center" colspan="2">
                     &nbsp;</td>
                 <td>
                 </td>
             </tr>
             <tr>
                 <td>
                 </td>
                 <td align="center" colspan="2">
                     <asp:ImageButton ID="CmdShow" runat="server" Height="30px" OnClientClick ="return validateshow();"
                         ImageUrl="~/Images/Show.png" />
                     <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" PostBackUrl="~/Main.aspx"/>
                 </td>
                 <td>
                 </td>
             </tr>
         </table>
     </div>
    </ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>
