<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="InvestmenrIntRpt.aspx.vb" Inherits="SMARTCORE_UI.InvestmenrIntRpt" 
    title="Untitled Page" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script type="text/javascript">

    function validate() {
        var strdt = document.getElementById('<%=TxtEffDate.ClientID%>').value;
        if (strdt = "") {
            alertify.error('Please Enter AsOnDate');
            return false;
        }
        
    }

        function CompareDates() {
            var str1 = document.getElementById('<%=TxtEffDate.ClientID%>').value;

            var str2 = document.getElementById('<%=HdnDate.ClientID%>').value;
            var dt1 = parseInt(str1.substring(0, 2), 10);
            var mon1 = parseInt(str1.substring(3, 5), 10);
            var yr1 = parseInt(str1.substring(6, 10), 10);
            var dt2 = parseInt(str2.substring(0, 2), 10);
            var mon2 = parseInt(str2.substring(3, 5), 10);
            var yr2 = parseInt(str2.substring(6, 10), 10);
            var date1 = new Date(yr1, mon1, dt1);
            var date2 = new Date(yr2, mon2, dt2);

            if (date2 < date1) {
                alertify.error("Please enter Date less than Tran date");
                document.getElementById('<%=TxtEffDate.ClientID%>').value = '';
                document.getElementById('<%=TxtEffDate.ClientID%>').focus();
                return false;
            }
        }

       function validatedate(inputText)  
  {  
  var dateformat = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;  
  // Match the date format through regular expression  
  if(inputText.value.match(dateformat))  
  {  
//  document.form1.text1.focus();  
  //Test which seperator is used '/' or '-'  
  var opera1 = inputText.value.split('/');  
  var opera2 = inputText.value.split('-');  
  lopera1 = opera1.length;  
  lopera2 = opera2.length;  
  // Extract the string into month, date and year  
  if (lopera1>1)  
  {  
  var pdate = inputText.value.split('/');  
  }  
  else if (lopera2>1)  
  {  
  var pdate = inputText.value.split('-');  
  }  
  var dd = parseInt(pdate[0]);  
  var mm  = parseInt(pdate[1]);  
  var yy = parseInt(pdate[2]);  
  // Create list of days of a month [assume there is no leap year by default]  
  var ListofDays = [31,28,31,30,31,30,31,31,30,31,30,31];  
  if (mm==1 || mm>2)  
  {  
  if (dd>ListofDays[mm-1])  
  {
      alertify.error('Invalid date format! Please enter a valid date!');
      
  return false;  
  }  
  }  
  if (mm==2)  
  {  
  var lyear = false;  
  if ( (!(yy % 4) && yy % 100) || !(yy % 400))   
  {  
  lyear = true;  
  }  
  if ((lyear==false) && (dd>=29))  
  {
      alertify.error('Invalid date format! Please enter a valid date!');
       
  return false;  
  }  
  if ((lyear==true) && (dd>29))  
  {
      alertify.error('Invalid date format! Please enter a valid date!');
     
  return false;  
  }   
  }   
  }   
  else  
  {
      alertify.error("Invalid date format! Please enter a valid date!");
      
  document.form1.text1.focus();  
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
     <legend>Investment Interest Report </legend>
         <table width="100%">
             <tr>
                 <td width="2%">
                 </td>
                 <td width="48%">
                 &nbsp;</td>
                 <td width="48%">
                     &nbsp;</td>
                 <td width="2%">
                 </td>
             </tr>
             
             <tr>
                 <td width="2%">
                     </td>
                 <td width="48%">
                 &nbsp;<label>As On Date</label>
                     </td>
                 <td width="48%">
                     <asp:TextBox ID="TxtEffDate" runat="server" Width="100%" OnChange="validatedate(this);CompareDates();"></asp:TextBox>
                     <asp:CalendarExtender ID="txtrcptdate_CalendarExtender" runat="server" 
                         Format="dd/MM/yyyy" TargetControlID="TxtEffDate">
                     </asp:CalendarExtender>
                     </td>
                 <td width="2%">
                     </td>
             </tr>
             <tr>
                 <td width="2%">
                     </td>
                 <td width="48%">&nbsp;</td>
                 <td width="48%">
                     &nbsp;</td>
                 <td width="2%">
                     </td>
             </tr>
             
             <tr>
                 <td>
                 </td>
                 <td align="center" colspan="2">
                     <asp:HiddenField ID="HdnDate" runat="server" />
                 </td>
                 <td>
                 </td>
             </tr>
             <tr>
                 <td>
                 </td>
                 <td align="center" colspan="2">
                     <asp:ImageButton ID="CmdShow" runat="server" Height="30px" OnClientClick="return validate();"
                         ImageUrl="~/Images/Show.png" />
                     <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />
                 </td>
                 <td>
                 </td>
             </tr>
         </table>
     </div>
    </ContentTemplate>
</asp:UpdatePanel> 


</asp:Content>
