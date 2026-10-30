<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="LoanLegalNotice.aspx.vb" Inherits="SMARTCORE_UI.LoanLegalNotice" %>
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

</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%;  float:left; " >                  
    <legend>Legal Notice</legend>
     <table width="100%">
          <tr>
          <td width="2%"></td>
        <td width="18%">&nbsp;</td>
        <td width="30%">&nbsp;</td>
        <td width="2%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
        <td width="30%">&nbsp;</td>
        <td width="2%">&nbsp;</td>
      </tr>     
      <tr>
      <td>
          &nbsp;</td>
      <td><label>Branch</label></td>
      <td>
          <asp:DropDownList ID="Ddl_Branch" runat="server" Width="100%"  AutoPostBack="true"
              TabIndex="1" >
          </asp:DropDownList>
          </td>
      <td></td>
      <td>
          <label>Section</label></td>
      <td>
          <asp:DropDownList ID="CmbSection" runat="server" Width="100%" 
              AutoPostBack="True">
             
          </asp:DropDownList>
          </td>
      <td>
          &nbsp;</td>
      </tr>
      
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  <label>Scheme</label></td>
              <td>
                  <asp:DropDownList ID="Ddl_Scheme" runat="server" TabIndex="2" Width="100%" 
                      AutoPostBack="True">
                  </asp:DropDownList>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
      
         <tr>
 <td>
     &nbsp;</td>
 <td><label>As on Date</label></td>
 <td>
  <asp:TextBox ID="Txt_FromDate" runat="server" Width="75%" TabIndex="3" OnChange="validatedate(this);"></asp:TextBox>
   <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
              TargetControlID="Txt_FromDate">
          </asp:CalendarExtender>
          <asp:Image ID="ImgDoa" runat="server" BorderStyle="Outset" Height="24px" 
              ImageUrl="~/Images/Cal.jpg" meta:resourcekey="ImgDoaResource1" Width="15%" />
 </td>
 
 
 <td>
     &nbsp;</td>
 </tr>
      
      
       <tr>
      <td >
          &nbsp;</td>
      <td ><label>Account No</label></td>
      <td>
          <asp:TextBox ID="Txt_AccNo" runat="server" Width="80%" AutoPostBack="True" 
              TabIndex="5"></asp:TextBox>
              <asp:ImageButton ID="ImgSrch" runat="server" Height="20px" ImageUrl="~/images/Search.png" Width="23px"
          />
           </td>
      <td ></td>
     <%-- <td >
          <asp:CheckBox ID="ChkLoanARC" runat="server" Text="ARC Statement" Visible="false" /></td>--%>
      <td ></td>
      <td >
          &nbsp;</td>
      </tr>
     
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  <label>
                  Customer Id</label></td>
              <td>
                  <asp:TextBox ID="Txt_CustId" runat="server" Width="100%"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  <label>
                  Customer Name</label></td>
              <td colspan="4">
                  <asp:TextBox ID="Txt_CustName" runat="server" TabIndex="7" Width="100%"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  <label>
                  Customer Address</label></td>
              <td colspan="4">
                  <asp:TextBox ID="Txt_CustAddress" runat="server" TabIndex="8" Width="100%" 
                      TextMode="MultiLine"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          <%--<tr>
              <td>
                  &nbsp;</td>
              <td>
                  <label>Post Office</label></td>
              <td colspan="4">
                  <asp:TextBox ID="TxtpostOfc" runat="server" Width="100%"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
          </tr>--%>
        

          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:CheckBox ID="ChkExcl" Text="Excel" runat="server" />
              </td>
              <td align="center" colspan="3">
                  <asp:ImageButton ID="CmdShow" runat="server" Height="25px" 
                      ImageUrl="~/Images/Show.png" />
                  &nbsp;<asp:ImageButton ID="CmdClose" runat="server" 
                      ImageUrl="~/Images/CloseBtn.png" />
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
              
              <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div ID="blur"> </div>
                    <div ID="progress">
                        <img alt="progress" src="Images/progress.gif" /> Please Wait...
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
                  </td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:HiddenField ID="HdnAccId" runat="server" />
              </td>
              <td>
                  &nbsp;</td>
          </tr>
     
     </table> 
 
   </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>  
      


