<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="HoConsolidationRpt.aspx.vb" Inherits="SMARTCORE_UI.HoConsolidationRpt" 
    title="HO Consolidation Rpt" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
  
        function onlyNumbers(event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alertify.error("Enter Only Numbers");
                return false;
            }
            else
                return true;
        }

        function CompareDates() {
            var str1 = document.getElementById('<%=Txt_FrDate.ClientID%>').value;

            var str2 = document.getElementById('<%=Txt_ToDate.ClientID%>').value;
            var dt1 = parseInt(str1.substring(0, 2), 10);
            var mon1 = parseInt(str1.substring(3, 5), 10);
            var yr1 = parseInt(str1.substring(6, 10), 10);
            var dt2 = parseInt(str2.substring(0, 2), 10);
            var mon2 = parseInt(str2.substring(3, 5), 10);
            var yr2 = parseInt(str2.substring(6, 10), 10);
            var date1 = new Date(yr1, mon1, dt1);
            var date2 = new Date(yr2, mon2, dt2);

            if (date2 < date1) {
                alertify.error("Please enter From:date less than or equal to To:date");
                document.getElementById('<%=Txt_FrDate.ClientID%>').value = '';
                document.getElementById('<%=Txt_FrDate.ClientID%>').focus();
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
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
 
     <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;margin-top:5%; font-family: Tahoma" >                
    
    <legend>Ho Consolidate Report</legend>  
        <table width="100%">
        <tr>
        <td width="5%"></td>
         <td width="40%"><label>Section</label></td>
         <td width="45%">      <asp:DropDownList ID="CmbSection" runat="server" Width="68%">
       </asp:DropDownList>
      </td>
         <td width="10%"></td>
        </tr>
             <tr>
        <td width="5%"></td>
         <td width="40%"><label>From</label></td>
         <td width="45%">
             <asp:TextBox ID="Txt_FrDate" runat="server" BackColor="White" 
                 CssClass="twitterStyleTextbox" OnChange="validatedate(this);CompareDates();" TabIndex="4" 
                 Width="97%"></asp:TextBox>
             <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                 TargetControlID="Txt_FrDate">
             </asp:CalendarExtender>
                 </td>
         <td width="10%"></td>
        </tr>
             <tr>
        <td width="5%"></td>
         <td width="40%"><label>To</label></td>
         <td width="45%">
             <asp:TextBox ID="Txt_ToDate" runat="server" BackColor="White" 
                 CssClass="twitterStyleTextbox" OnChange="validatedate(this);CompareDates();" TabIndex="4" 
                 Width="97%"></asp:TextBox>
             <asp:CalendarExtender ID="Txt_ToDate_CalendarExtender" runat="server" 
                 Format="dd/MM/yyyy" TargetControlID="Txt_ToDate">
             </asp:CalendarExtender>
                 </td>
         <td width="10%"></td>
        </tr>
             <tr>
        <td width="5%"></td>
         <td width="40%"><label></label></td>
         <td width="45%"></td>
         <td width="10%"></td>
        </tr>
            <tr>
                <td width="5%">
                    &nbsp;</td>
                <td colspan="2" style="width: 85%; " align="center" width="40%">
                    <asp:ImageButton ID="Cmd_Show" runat="server" Height="32px" 
                        ImageUrl="~/Images/Show.png" Width="96px" />
                    <asp:ImageButton ID="ImageButton2" runat="server" Height="32px" PostBackUrl="~/Main.aspx"
                        ImageUrl="~/images/Close.png" Width="96px" />
                </td>
                <td width="10%">
                    &nbsp;</td>
            </tr>
        </table>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>


</asp:Content>
