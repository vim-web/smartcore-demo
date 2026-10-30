<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="Loan_appDetail.aspx.vb" Inherits="SMARTCORE_UI.Loan_appDetail" 
    title="Untitled Page" %>
   
        <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

    <style type="text/css">
        .style1
        {
            height: 36px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%;  float:left; " >                  
    <legend>Loan Application Report</legend>
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
              <td>
                  <label>
                  Branch</label></td>
              <td>
                  <asp:DropDownList ID="Ddl_Branch" runat="server" AutoPostBack="true" 
                      TabIndex="1" Width="100%">
                  </asp:DropDownList>
              </td>
              <td>
                  &nbsp;</td>
              <td>&nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
     
          <tr>
              <td class="style1">
                  </td>
              <td class="style1">
                  <label>
                  Approved Status</label></td>
              <td class="style1">
                  <asp:DropDownList ID="Drp_Approved" runat="server" AutoPostBack="true" 
                      TabIndex="1" Width="100%">
                      <asp:ListItem>APPROVED</asp:ListItem>
                      <asp:ListItem>REJECTED</asp:ListItem>
                  </asp:DropDownList>
              </td>
              <td class="style1">
                  </td>
              <td class="style1"></td>
              <td class="style1">
                  </td>
              <td class="style1">
                  </td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  <label>
                  From Date</label></td>
              <td>
                  <asp:TextBox ID="Txt_Fromdate" runat="server" OnChange="validatedate(this);" 
                      TabIndex="4" Width="98%"></asp:TextBox>
                  <asp:CalendarExtender ID="Txt_Fromdate_CalendarExtender" runat="server" 
                      Format="dd/MM/yyyy" TargetControlID="Txt_Fromdate">
                  </asp:CalendarExtender>
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
                  <label>
                  To Date</label></td>
              <td>
                  <asp:TextBox ID="Txt_Todate" runat="server" OnChange="validatedate(this);" 
                      TabIndex="4" Width="98%"></asp:TextBox>
                  <asp:CalendarExtender ID="Txt_Todate_CalendarExtender" runat="server" 
                      Format="dd/MM/yyyy" TargetControlID="Txt_Todate">
                  </asp:CalendarExtender>
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
                  &nbsp;</td>
              <td align="center" colspan="3">
                  <asp:ImageButton ID="CmdShow" runat="server" Height="25px" 
                      ImageUrl="~/Images/Show.png" />
                  &nbsp;
                  <asp:ImageButton ID="CmdClose" runat="server" 
                      ImageUrl="~/Images/CloseBtn.png" />
<%--                      <asp:ImageButton ID="CmdClose" runat="server" 
                      ImageUrl="~/Images/CloseBtn.png" />--%>
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
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
     
          <tr>
              <td colspan="7">
                  <%--<asp:GridView ID="Grid_custsub" runat="server" CssClass="grid-viewNew"  Width="100%">
                      <Columns>
                          <asp:BoundField headertext="Customer id" DataField="cust_id"/>
                          <asp:BoundField headertext="Customer Name" DataField="cust_name"/>
                          <asp:BoundField headertext="Date of submission" DataField="date_of_sub"/>
                      </Columns>
                  </asp:GridView>--%>
                  <asp:GridView ID="Grid_LoanApp" runat="server" CssClass="grid-viewNew"  Width="100%"
                      AutoGenerateColumns="False"   >
                      <Columns>
                          <asp:BoundField HeaderText="Account No" DataField="LM_AccNo"  />
                          <asp:BoundField HeaderText="Customer Name" DataField="Cust_Name" />
                          <asp:BoundField HeaderText="Loan Date" DataField="Lm_LoanDate"  DataFormatString="{0:MM/dd/yyyy}"/>
                         
                          <asp:ButtonField CommandName="Select" Text="View Details" />
                         
                      </Columns>
                  </asp:GridView>
              </td>
          </tr>
     
     </table> 
 
   </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>  