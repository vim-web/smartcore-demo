<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LoanSecurityRpt.aspx.vb" MasterPageFile="~/SMARTCORE.Master" Inherits="SMARTCORE_UI.LoanSecurityRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<script type='text/javascript'>

    function ValidateSave() {


        return true;
    }

</script>
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

        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        
          <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:15%; ">
      <legend>LOAN SECURITY REPORT</legend>
         
         <table width="100%">
   
          <tr>
              <td width="10%" >
                   </td>
              <td width="30%"  >
                  <label>
                  BRANCH</label></td>
              <td width="50%" >
                   <asp:DropDownList ID="CmbBranch" runat="server" Width="100%" 
                       AutoPostBack="True">
                   </asp:DropDownList>
              </td>
              <td width="10%" >
                   </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td>
                  <label>SCHEME</label></td>
              <td>
                  <asp:DropDownList ID="CmbScheme" runat="server" Width="100%" AutoPostBack="True">
                  </asp:DropDownList>
              </td>
              <td>
                   </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td>
                  <label>
                  SECURITY</label></td>
              <td>
                  <asp:DropDownList ID="CmbSecurity" runat="server" Width="100%" AutoPostBack="true" Enabled  ="false" >
                  </asp:DropDownList>
              </td>
              <td>
                   <asp:CheckBox ID="ChkSecurity" runat="server" AutoPostBack="True" />
                   </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td>
                  <label>
                  ACCNO</label></td>
              <td>
                  <asp:TextBox ID="TxtAccNo" runat="server" AutoPostBack="True" Width="95%" ></asp:TextBox>
              </td>
              <td>
                   <asp:CheckBox ID="ChkAccNo" runat="server" AutoPostBack="True" />
                   </td>
          </tr>
             <tr>
                 <td>
                      </td>
                 <td>
                    <label>AS ON DATE</label> </td>
                 <td>
                     <asp:TextBox ID="TxtDate" runat="server" OnChange="validatedate(this);" Width="95%" ></asp:TextBox>
                     <asp:CalendarExtender ID="CalendarExtender2" runat="server" 
                Format="dd/MM/yyyy" TargetControlID="TxtDate" >
            </asp:CalendarExtender>
                 </td>
                 <td>
                      </td>
             </tr>
             <tr>
              <td>
                   </td>
              <td>
                  <label>
                  AREA TYPE</label></td>
              <td>
                  <asp:DropDownList ID="DdlAreaType" runat="server" Width="100%" Enabled ="false" >
                  </asp:DropDownList>
              </td>
              <td>
                  
                   <asp:CheckBox ID="ChkAreaType" runat="server" AutoPostBack="True" Text="Select Area Type" />
                  
                   </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td>
                  <label>
                  AREA FROM</label> </td>
              <td>
                  <asp:TextBox ID="TxtAreaFrom" runat="server" AutoPostBack="True" Width="49%"></asp:TextBox>
              </td>
           
          </tr>
              <tr>
              <td>
                   </td>
              <td><label>
                  AREA TO</label>
                  </td>
              <td>
                  <asp:TextBox ID="TxtAreaTo" runat="server" AutoPostBack="True" Width="49%"></asp:TextBox>
                  </td>
           
          </tr>
   <%--       <tr>
              <td>
                   </td>
              <td>
                  <label>Reason</label></td>
              <td>
                  <asp:TextBox ID="TxtReason" runat="server" Width="100%"></asp:TextBox>
              </td>
              <td>
                   </td>
          </tr>--%>
          <tr>
              <td>
                   </td>
              <td align="center" colspan="2">
                  <asp:ImageButton ID="CmdShow" runat="server" ImageUrl="~/Images/Show.png" 
                      OnClientClick="return ValidateSave();" />
                   <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" />
                   <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />
              </td>
              <td>
                   </td>
          </tr>
          <tr>
              <td>
                   </td>
              <td align="center" colspan="2">
                   </td>
              <td>
                   </td>
          </tr>
      </table>
     </div>
     
      </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    
    </asp:Content>
