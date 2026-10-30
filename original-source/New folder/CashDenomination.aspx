<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="CashDenomination.aspx.vb" Inherits="SMARTCORE_UI.CashDenomination" %>    
     <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
       <script type="text/javascript" src="Scripts/jquery_json.js"></script>
     <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
    <link href="CSS/ui-lightness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript' src="Scripts/Common.js"></script>
    <script src="Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script type='text/javascript' src="Scripts/jquery-ui-1.9.2.custom.min.js"></script>   
    
      <script type="text/javascript">

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

    <style type="text/css">
        .style1
        {
            width: 32%;
        }
        .auto-style1 {
            width: 30%;
        }
        .auto-style2 {
            width: 2%;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%;  float:left; " >                  
    <legend><span style="font-size:10.0pt;font-family:&quot;Arial&quot;,sans-serif">CASH DENOMINATION</span></legend>
     <table width="100%">
          <tr>
          <td width="2%"></td>
        <td class="style1">&nbsp;</td>
        <td width="30%">&nbsp;</td>
        <td width="2%">&nbsp;</td>
        <td width="15%" colspan="2">&nbsp;</td>
        <td class="auto-style1">&nbsp;</td>
        <td class="auto-style2">&nbsp;</td>
      </tr>     
     
          <tr>
              <td width="2%">
                  &nbsp;</td>
              <td class="style1">
                  <label>
                  Branch</label></td>
              <td colspan="5">
                  <asp:DropDownList ID="DrpBranch0" width="59%" runat="server">
                  </asp:DropDownList>
              </td>
              <td class="auto-style2">
                  &nbsp;</td>
          </tr>
     
          <tr>
              <td width="2%">&nbsp;</td>
              <td class="style1">
                  <label>
                  Date</label></td>
              <td colspan="5">
                  <asp:TextBox ID="TxtFromDate" runat="server" OnChange="validatedate(this);" TabIndex="3" Width="59%"></asp:TextBox>
                  <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TxtFromDate">
                  </asp:CalendarExtender>
              </td>
              <td class="auto-style2">&nbsp;</td>
          </tr>
     
          <tr>
              <td>
                  &nbsp;</td>
              <td class="style1">
                  <label>
                  RS.2000</label></td>
              <td>
                  <asp:TextBox ID="Txt2000" runat="server" AutoPostBack="True" onkeypress="return isFloatKey(event);" Width="45%"></asp:TextBox>
              </td>
              <td colspan="2">
                  <asp:TextBox ID="Txtsum2000" enabled="false" runat="server"></asp:TextBox>
              </td>
              <td colspan="2">
                  &nbsp;</td>
              <td class="auto-style2">
                  &nbsp;</td>
          </tr>
          <tr>
              <td>&nbsp;</td>
              <td class="style1">
                  <label>
                  RS.1000</label></td>
              <td>
                  <asp:TextBox ID="Txt1000" runat="server" enabled="false" AutoPostBack="True" onkeypress="return isFloatKey(event);" Width="45%" TabIndex="1"></asp:TextBox>
              </td>
              <td colspan="2">
                  <asp:TextBox ID="Txtsum1000" enabled="false" runat="server"></asp:TextBox>
              </td>
              <td colspan="2">&nbsp;</td>
              <td class="auto-style2">&nbsp;</td>
          </tr>
          <tr>
              <td>&nbsp;</td>
              <td class="style1">
                  <label>
                  RS.500</label></td>
              <td>
                  <asp:TextBox ID="Txt500" runat="server"  AutoPostBack="True" onkeypress="return isFloatKey(event);" Width="45%" Columns="2"></asp:TextBox>
              </td>
              <td colspan="2">
                  <asp:TextBox ID="Txtsum500" enabled="false" runat="server"></asp:TextBox>
              </td>
              <td colspan="2">&nbsp;</td>
              <td class="auto-style2">&nbsp;</td>
          </tr>
          <tr>
              <td>&nbsp;</td>
              <td class="style1">
                  <label>
                  RS.200</label></td>
              <td>
                  <asp:TextBox ID="Txt200" runat="server" AutoPostBack="True" onkeypress="return isFloatKey(event);" Width="45%" TabIndex="3"></asp:TextBox>
              </td>
              <td colspan="2">
                  <asp:TextBox ID="Txtsum200" enabled="false" runat="server"></asp:TextBox>
              </td>
              <td colspan="2">&nbsp;</td>
              <td class="auto-style2">&nbsp;</td>
          </tr>
          <tr>
              <td>&nbsp;</td>
              <td class="style1">
                  <label>
                  RS.100</label></td>
              <td>
                  <asp:TextBox ID="Txt100" runat="server" AutoPostBack="True" onkeypress="return isFloatKey(event);" Width="45%" TabIndex="4"></asp:TextBox>
              </td>
              <td colspan="2">
                  <asp:TextBox ID="Txtsum100" enabled="false" runat="server"></asp:TextBox>
              </td>
              <td colspan="2">&nbsp;</td>
              <td class="auto-style2">&nbsp;</td>
          </tr>
          <tr>
              <td>&nbsp;</td>
              <td class="style1">
                  <label>
                  RS.50</label></td>
              <td>
                  <asp:TextBox ID="Txt50" runat="server" AutoPostBack="True" onkeypress="return isFloatKey(event);" Width="45%" TabIndex="5"></asp:TextBox>
              </td>
              <td colspan="2">
                  <asp:TextBox ID="Txtsum50" enabled="false" runat="server"></asp:TextBox>
              </td>
              <td colspan="2">&nbsp;</td>
              <td class="auto-style2">&nbsp;</td>
          </tr>
          <tr>
              <td>&nbsp;</td>
              <td class="style1">
                  <label>
                  RS.20</label></td>
              <td>
                  <asp:TextBox ID="Txt20" runat="server" AutoPostBack="True" onkeypress="return isFloatKey(event);" Width="45%" TabIndex="6"></asp:TextBox>
              </td>
              <td colspan="2">
                  <asp:TextBox ID="Txtsum20" enabled="false" runat="server"></asp:TextBox>
              </td>
              <td colspan="2">&nbsp;</td>
              <td class="auto-style2">&nbsp;</td>
          </tr>
          <tr>
              <td>&nbsp;</td>
              <td class="style1">
                  <label>
                  RS.10</label></td>
              <td>
                  <asp:TextBox ID="Txt10" runat="server" AutoPostBack="True" onkeypress="return isFloatKey(event);" Width="45%" TabIndex="7"></asp:TextBox>
              </td>
              <td colspan="2">
                  <asp:TextBox ID="Txtsum10" enabled="false" runat="server"></asp:TextBox>
              </td>
              <td colspan="2">&nbsp;</td>
              <td class="auto-style2">&nbsp;</td>
          </tr>
          <tr>
              <td>&nbsp;</td>
              <td class="style1">
                  <label>
                  RS.5</label></td>
              <td>
                  <asp:TextBox ID="Txt5" runat="server" AutoPostBack="True" onkeypress="return isFloatKey(event);" Width="45%" TabIndex="7"></asp:TextBox>
              </td>
              <td colspan="2">
                  <asp:TextBox ID="Txtsum5" enabled="false" runat="server"></asp:TextBox>
              </td>
              <td colspan="2">&nbsp;</td>
              <td class="auto-style2">&nbsp;</td>
          </tr>
          <tr>
              <td>&nbsp;</td>
              <td class="style1">
                  <label>
                  Coin 5</label></td>
              <td>
                  <asp:TextBox ID="Txtcoin5" runat="server" AutoPostBack="True" onkeypress="return isFloatKey(event);" Width="45%" TabIndex="8"></asp:TextBox>
              </td>
              <td colspan="2">
                  <asp:TextBox ID="Txtsumcoin5" enabled="false" runat="server"></asp:TextBox>
              </td>
              <td colspan="2">&nbsp;</td>
              <td class="auto-style2">&nbsp;</td>
          </tr>
          <tr>
              <td>&nbsp;</td>
              <td class="style1">
                  <label>
                  Coin 2</label></td>
              <td>
                  <asp:TextBox ID="Txtcoin2" runat="server" AutoPostBack="True" onkeypress="return isFloatKey(event);" Width="45%" TabIndex="9"></asp:TextBox>
              </td>
              <td colspan="2">
                  <asp:TextBox ID="Txtsumcoin2" enabled="false" runat="server"></asp:TextBox>
              </td>
              <td colspan="2">&nbsp;</td>
              <td class="auto-style2">&nbsp;</td>
          </tr>
          <tr>
              <td>&nbsp;</td>
              <td class="style1">
                  <label>
                  Coin 1</label></td>
              <td>
                  <asp:TextBox ID="Txtcoin1" runat="server" AutoPostBack="True" onkeypress="return isFloatKey(event);" Width="45%" TabIndex="10"></asp:TextBox>
              </td>
              <td colspan="2">
                  <asp:TextBox ID="Txtsumcoin1" enabled="false" runat="server"></asp:TextBox>
              </td>
              <td colspan="2">&nbsp;</td>
              <td class="auto-style2">&nbsp;</td>
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
              <td>&nbsp;</td>
              <td class="style1">
                  <label>
                  Others</label></td>
              <td>
                  <asp:TextBox ID="Txtothers" runat="server" Enabled="false" AutoPostBack="True" onkeypress="return isFloatKey(event);" TabIndex="10" Width="45%"></asp:TextBox>
              </td>
              <td colspan="2">
                  <asp:TextBox ID="Txtother" runat="server" AutoPostBack="True" onkeypress="return isFloatKey(event);"></asp:TextBox>
              </td>
              <td colspan="2">&nbsp;</td>
              <td class="auto-style2">&nbsp;</td>
          </tr>
          <tr>
              <td></td>
              <td class="style1">
                  <label>
                  Closing Balance</label></td>
              <td>
                  <asp:TextBox ID="Txtcsbal" runat="server" AutoPostBack="True" Enabled="false" onkeypress="return isFloatKey(event);" TabIndex="10" Width="45%"></asp:TextBox>
                  &nbsp;
                  <label>
                  &nbsp;Total Sum</label></td>
              <td colspan="2">
                  <asp:TextBox ID="TxtSum" runat="server" AutoPostBack="True" enabled="false" onkeypress="return isFloatKey(event);" Width="100%"></asp:TextBox>
              </td>
              <td colspan="2">&nbsp;</td>
              <td class="auto-style2">&nbsp;&nbsp;</td>
          </tr>
          <tr>
              <td>&nbsp;</td>
              <td class="style1">
                  &nbsp;</td>
              <td colspan="3">
                  &nbsp;</td>
              <td colspan="2">
                  &nbsp;</td>
              <td class="auto-style2">&nbsp;</td>
          </tr>
          <tr>
              <td>&nbsp;</td>
              <td class="style1">&nbsp;</td>
              <td>&nbsp;</td>
              <td colspan="4">&nbsp;</td>
              <td class="auto-style2">&nbsp;</td>
              <td>
                  &nbsp;</td>
              <td class="style1">
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td colspan="2">
                  &nbsp;</td>
              <td class="auto-style1">&nbsp;</td>
              <td class="auto-style2">&nbsp;</td>
          </tr>
     
          <tr>
              <td>
                  &nbsp;</td>
              <td class="style1">
                  &nbsp;</td>
              <td align="center" colspan="4">
              
                  <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" meta:resourcekey="CmdSaveResource1" TabIndex="56" />
                  &nbsp;
                  <asp:ImageButton ID="CmdShow" runat="server" ImageUrl="~/Images/Clear.png" TabIndex="57" />
                  &nbsp; <%--                      <asp:ImageButton ID="CmdClose" runat="server" 
                      ImageUrl="~/Images/CloseBtn.png" />--%>
                  <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/images/Close.png" meta:resourcekey="CmdClearResource1" TabIndex="37" />
                  </td>
              <td class="auto-style1">
                  &nbsp;</td>
              <td class="auto-style2">
                  &nbsp;</td>
              <td>&nbsp;</td>
              <td class="style1">&nbsp;</td>
              <td>
                  <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                      <ProgressTemplate>
                          <div id="blur">
                          </div>
                          <div id="progress">
                              <img alt="progress" visible="false" src="Images/progress.gif" />
                              Please Wait...
                          </div>
                      </ProgressTemplate>
                  </asp:UpdateProgress>
              </td>
              <td>&nbsp;</td>
              <td colspan="2">&nbsp;</td>
              <td class="auto-style1">
                  &nbsp;</td>
              <td class="auto-style2">
                  &nbsp;</td>
          </tr>
     
          <tr>
              <td colspan="8">
                  <%--<asp:GridView ID="Grid_custsub" runat="server" CssClass="grid-viewNew"  Width="100%">
                      <Columns>
                          <asp:BoundField headertext="Customer id" DataField="cust_id"/>
                          <asp:BoundField headertext="Customer Name" DataField="cust_name"/>
                          <asp:BoundField headertext="Date of submission" DataField="date_of_sub"/>
                      </Columns>
                  </asp:GridView>--%>
                  <asp:GridView ID="Grid_D" runat="server" CssClass="grid-viewNew" visible="false" Width="100%"
                      AutoGenerateColumns="False"   >
                      <Columns>
                        <%--  <asp:BoundField HeaderText="Customer id" DataField="cust_id"  />
                          <asp:BoundField HeaderText="Customer Name" DataField="cust_name" />
                          <asp:BoundField HeaderText="Bank Name & Branch" DataField="BnkNmeBrh" />
                          <asp:BoundField HeaderText="Bank Acc No" DataField="BnkAccNo" />
                          <asp:BoundField HeaderText="IFSC Code" DataField="IfscCode" />--%>
                          <%--<asp:BoundField HeaderText="Date of submission" DataField="date_of_sub"  DataFormatString="{0:MM/dd/yyyy}"/>--%>
                         
                      </Columns>
                  </asp:GridView>
                  </td>
          </tr>
     
     </table> 
 
   </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>  

