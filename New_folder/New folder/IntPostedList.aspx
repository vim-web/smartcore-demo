<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="IntPostedList.aspx.vb" Inherits="SMARTCORE_UI.IntPostedList" 
    title="INTEREST POSTED LIST" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
    <script language="javascript" type="text/javascript">
     
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
      alert('Invalid date format! Please enter a valid date!');
     
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
      alert('Invalid date format! Please enter a valid date!');
     
  return false;  
  }  
  if ((lyear==true) && (dd>29))  
  {
      alert('Invalid date format! Please enter a valid date!');
     
  return false;  
  }   
  }   
  }   
  else  
  {   
      alert("Invalid date format! Please enter a valid date!");
     
  document.form1.text1.focus();  
  return false;  
  }  
  }  

</script>
        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
 <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%; font-family: Tahoma" >                

     <table width="100%">
        <tr>
            <td width="2%">
            </td>
            <td width="48%">
                <legend>INTEREST POSTED LIST</legend></td>
            <td width="48%"> 
                &nbsp;</td>
            <td width="2%">
            </td>
        </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td>
                 <label>
                 Branch</label></td>
             <td width="45%">
                 <asp:DropDownList ID="CmbBranch" runat="server" Width="100%">
                 </asp:DropDownList>
             </td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td>
                 <label>
                 Section</label></td>
             <td width="45%">
                 <asp:DropDownList ID="CmbSection" runat="server" AutoPostBack="True" 
                     Width="100%">
                     <asp:ListItem Value="0">SHARE</asp:ListItem>
                     <asp:ListItem Value="1">DEPOSIT</asp:ListItem>
                     <asp:ListItem Value="2">LOAN</asp:ListItem>
                     <asp:ListItem Value="3">SUSPENCE</asp:ListItem>
                 </asp:DropDownList>
             </td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td>
                 <label>
                 Scheme</label></td>
             <td width="45%">
                 <label>
                 <asp:DropDownList ID="CmbScheme" runat="server" Width="100%">
                 </asp:DropDownList>
                 </label>
             </td>
             <td width="2%">
                 `</td>
         </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td>
                 <label>
                 From Date</label></td>
             <td width="45%">
                 <asp:TextBox ID="TxtFromDate" runat="server" Width="100%" OnChange="validatedate(this);"></asp:TextBox>
                 <asp:CalendarExtender ID="TxtFromDate_CalendarExtender" runat="server"  Format="dd/MM/yyyy" 
                     TargetControlID="TxtFromDate">
                 </asp:CalendarExtender>
             </td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td>
                 <label>
                 To Date</label></td>
             <td width="45%">
                 <asp:TextBox ID="TxtTodate" runat="server" Width="100%" OnChange="validatedate(this);"></asp:TextBox>
                 <asp:CalendarExtender ID="TxtTodate_CalendarExtender" runat="server"    Format="dd/MM/yyyy" 
                     TargetControlID="TxtTodate">
                 </asp:CalendarExtender>
             </td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td width="48%">
                 &nbsp;</td>
             <td width="48%">
                 &nbsp;</td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td width="48%">
                 &nbsp;</td>
             <td width="48%">
                 <asp:ImageButton ID="CmdShow" runat="server" ImageUrl="~/Images/Show.png" />
                 <asp:ImageButton ID="CmdClose" runat="server" 
                     ImageUrl="~/Images/CloseBtn.png" />
             </td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td width="48%">
                 &nbsp;</td>
             <td width="48%">
                 &nbsp;</td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td align="center" colspan="2" style="width: 96%" width="48%">
                 &nbsp;</td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
         
           <tr>
            <td width="2%">
            </td>
            <td width="48%">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                    AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div ID="blur">
                        </div>
                        <div ID="progress">
                            <img alt="progress" src="Images/progress.gif" /> Please Wait...
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
              </td> 
            <td width="48%"> 
                &nbsp;</td>
            <td width="2%">
            </td>
        </tr>
        </table>
     </div>
    </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>


