<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="ProcessingFeeWaiver.aspx.vb" Inherits="SMARTCORE_UI.ProcessingFeeWaiver" 
    title="Waiver" %>
    
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
    <legend>Processing Fee Waiver</legend>
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
              <td width="2%">
                  &nbsp;</td>
              <td width="18%"><label>Branch</label>
                  &nbsp;</td>
              <td width="30%">
                  <asp:DropDownList ID="Ddl_Branch" runat="server" Width="98%" 
                      AutoPostBack="True">
                  </asp:DropDownList>
              </td>
              <td width="2%">
                  &nbsp;</td>
              <td width="15%">
                  &nbsp;</td>
              <td width="30%">
                  &nbsp;</td>
              <td width="2%">
                  &nbsp;</td>
          </tr>
          <tr>
              <td width="2%">
                  &nbsp;</td>
              <td width="18%"><label>Scheme</label>
                  &nbsp;</td>
              <td width="30%">
                  <asp:DropDownList ID="Ddl_Scheme" runat="server" Width="98%">
                  </asp:DropDownList>
              </td>
              <td width="2%">
                  &nbsp;</td>
              <td width="15%">
                  &nbsp;</td>
              <td width="30%">
                  &nbsp;</td>
              <td width="2%">
                  &nbsp;</td>
          </tr>
     
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  <label>
                  Acc_No</label></td>
              <td>
                  <asp:TextBox ID="Txt_AccNo" runat="server" Width="98%" AutoPostBack="True"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td><asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
                    ImageUrl="~/images/Search.png" Width="23px" />
                  &nbsp;</td>
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
                  Customer Name</label></td>
              <td colspan="4" class="style1">
                  <asp:TextBox ID="Txt_CustName" runat="server" TabIndex="7" Width="75%"></asp:TextBox>
              </td>
              <td class="style1">
                  </td>
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
                  <label>Date</label></td>
              <td>
                  <asp:TextBox ID="Txt_subdate" runat="server" OnChange="validatedate(this);" 
                      TabIndex="4" Width="98%"></asp:TextBox>
                  <asp:CalendarExtender ID="Txt_ToDate0_CalendarExtender" runat="server" 
                      Format="dd/MM/yyyy" TargetControlID="Txt_subdate">
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
              <td>
                  &nbsp;</td>
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
              <asp:GridView ID="Grd_processfee" runat="server" CssClass="grid-viewNew"  Width="100%"
                      AutoGenerateColumns="False"   >
                      <Columns>
                          <asp:BoundField HeaderText="Acc_No" DataField="Acc_No"  />
                          <asp:BoundField HeaderText="Customer Name" DataField="Cust_Name" />
                          <asp:BoundField HeaderText="Particulars" DataField="Display" />
                          <asp:BoundField HeaderText="Amount" DataField="Amount" />
                          <asp:BoundField HeaderText="Tranaction Date" DataField="Tr_Date"  DataFormatString="{0:dd/MM/yyyy}"/>
                         
                      </Columns>
                  </asp:GridView>
                  &nbsp;</td>
          </tr>
     
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td align="center" colspan="3">
                  <asp:ImageButton ID="BtnPro" runat="server" 
                      ImageUrl="~/Images/ProcessBtn.png" />
                  &nbsp;
                  <asp:ImageButton ID="Btn_Close" runat="server" ImageUrl="~/Images/Close.png" />
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
                  
              </td>
          </tr>
     
     </table> 
 
   </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>  
