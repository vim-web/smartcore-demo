<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="DailyReports.aspx.vb" Inherits="SMARTCORE_UI.DailyReports" 
    title="Daily Reports" %>
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

</script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
 
     <div class="gradientBoxesWithOuterShadows" onkeypress="return WebForm_FireDefaultButton(event, '<%= ImageButton1.ClientID %>')"  style="width: 60%; margin-left:20%; float :left; margin-top:5%;"  >                
    
    <legend>Daily Reports</legend>  
    
     <table width="100%" style="float:left;">
          <tr>
        <td width="1%">&nbsp;</td>
        <td width="23%" rowspan="10">
            <asp:Panel ID="Panel2" runat="server" >
                <asp:RadioButton ID="Opt_CashScroll" runat="server" AutoPostBack="True" 
                    Checked="True" CssClass="lblCSS"
                    GroupName="RptType" Text="Cash Scroll" />
                <br />
                <asp:RadioButton ID="Opt_TransferList" runat="server" AutoPostBack="True" 
                  CssClass="lblCSS"
                    GroupName="RptType" Text="Transfer List" />
                <br />
                <asp:RadioButton ID="Opt_DayBook" runat="server" AutoPostBack="True"  CssClass="lblCSS"
                   
                    GroupName="RptType" Text="Day Book" />
                <br />
                <asp:RadioButton ID="Opt_RctPay" runat="server" AutoPostBack="True" 
                   CssClass="lblCSS"
                    GroupName="RptType" Text="Receipts &amp; Disbursement" />
                <br />
                <asp:RadioButton ID="Opt_GL" runat="server" AutoPostBack="True" 
                   CssClass="lblCSS"
                    GroupName="RptType" Text="General Ledger" />
                <br />
                <asp:RadioButton ID="Opt_IB" runat="server" AutoPostBack="True"  CssClass="lblCSS"
                   
                    GroupName="RptType" Text="IB Transaction" /> 
                     <br />   
                    
<asp:RadioButton ID="Opt_IBVOU" runat="server" AutoPostBack="True"  CssClass="lblCSS"
                    
                    GroupName="RptType" Text="IB Voucher" />  
                          <br/>
                
                <asp:RadioButton ID="Opt_DayEnd" runat="server" AutoPostBack="True"  CssClass="lblCSS"
                   
                    GroupName="RptType" Text="DayEnd" />              
                <asp:RadioButtonList ID="RdoGlType" runat="server" ForeColor="White" 
                    Visible="False" width="100%">
                    <asp:ListItem>GLType1</asp:ListItem>
                    <asp:ListItem>GLType2</asp:ListItem>
                    <asp:ListItem>GLType3</asp:ListItem>
                </asp:RadioButtonList>
            </asp:Panel>
              </td>
        <td width="2%">&nbsp;</td>
        <td width="10%">&nbsp;</td>
        <td width="2%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
        <td width="12%">&nbsp;</td>
        <td width="2%">&nbsp;</td>
      </tr>
      
      <tr>
      <td></td>
      <td></td>
      <td></td>
      <td></td>
      <td>
          <asp:Panel ID="TransferPnl" runat="server">
              <asp:RadioButton ID="OptOwnBranch" runat="server" Checked="True"  CssClass="lblCSS"
                  GroupName="Transfer" Text="Own Branch Entries" Visible="False" />
              <br />
              <asp:RadioButton ID="OptAllTrans" runat="server" GroupName="Transfer"  CssClass="lblCSS"
                  Text="All Transfer Entries" Visible="False" />
          </asp:Panel>
          </td>
      <td>&nbsp;</td>
      </tr>
          <tr>
      <td></td>
      <td></td>
      <td>
          <asp:Label ID="Label2" runat="server" CssClass="lblCSS"
               Text="Region"></asp:Label>
          </td>
      <td></td>
      <td>
          <asp:DropDownList ID="DrpRegn" runat="server"  Width="100%" AutoPostBack="true">
          </asp:DropDownList>
          </td>
      <td>
          &nbsp;</td>
      </tr>
          <tr>
      <td></td>
      <td></td>
      <td>
          <asp:Label ID="Label1" runat="server" CssClass="lblCSS"
               Text="Area"></asp:Label>
          </td>
      <td></td>
      <td>
          <asp:DropDownList ID="DrpArea" runat="server"  Width="100%" AutoPostBack="true">
          </asp:DropDownList>
          </td>
      <td>
          &nbsp;</td>
      </tr>
      <tr>
      <td></td>
      <td></td>
      <td>
          <asp:Label ID="Lbl_Branch" runat="server" CssClass="lblCSS"
               Text="Branch"></asp:Label>
          </td>
      <td></td>
      <td>
          <asp:DropDownList ID="Ddl_Branch" runat="server"  Width="100%">
          </asp:DropDownList>
          </td>
      <td>
          <asp:CheckBox ID="Chk_BalHeads" runat="server" Text="Balancing Heads" />
          </td>
      </tr>
      
      <tr>
      <td></td>
      <td></td>
      <td>
          <asp:Label ID="Lbl_Date" runat="server"  CssClass="lblCSS"
               Text="Date"></asp:Label>
          </td>
      <td></td>
      <td>
          <asp:TextBox ID="Txt_DateFrom" runat="server" BackColor="White" OnChange="validatedate(this);"
              CssClass="twitterStyleTextbox" TabIndex="4" Width="97%"></asp:TextBox>
          <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
              TargetControlID="Txt_DateFrom">
          </asp:CalendarExtender>
          </td>
      <td>
          <asp:CheckBox ID="Chk_BrWise" runat="server" Text="Branch Wise" />
          </td>
      </tr>
      
      <tr>
      <td></td>
      <td></td>
      <td>
          <asp:Label ID="Lbl_DateTo" runat="server" CssClass="lblCSS"
              Text="Date" Visible="True"></asp:Label>
          </td>
      <td></td>
      <td>
          <asp:TextBox ID="Txt_DateTo" runat="server" BackColor="White" OnChange="validatedate(this);"
              CssClass="twitterStyleTextbox" TabIndex="4" Width="97%" Visible="True"></asp:TextBox>
          <asp:CalendarExtender ID="Txt_DateTo_CalendarExtender" runat="server" 
              Format="dd/MM/yyyy" TargetControlID="Txt_DateTo">
          </asp:CalendarExtender>
          </td>
      <td>
          <asp:CheckBox ID="Chk_RecPay" Text ="R&P Only"  runat="server" />
          </td>
      </tr>
      
      <tr>
      <td>&nbsp;</td>
      <td>&nbsp;</td>
      <td>
          <asp:Label ID="Lbl_AccHead" runat="server"  
             CssClass="lblCSS" Text="Account Head" Visible ="false" ></asp:Label>
          </td>
      <td>&nbsp;</td>
      <td>
          <asp:DropDownList ID="CmbGL" runat="server" Width="100%" Visible="False">
          </asp:DropDownList>
          </td>
      <td>
          <asp:CheckBox ID="Chk_Rec" runat="server" Text="Receipt" />
          </td>
      </tr>
       
      
          <tr>
              <td>
              </td>
              <td>
              </td>
              <td>
                  <asp:Label ID="Lbl_IBTran" runat="server" CssClass="lblCSS"
                      Text="Transaction"></asp:Label>
              </td>
              <td>
              </td>
              <td>
                  <asp:DropDownList ID="CmbIB" runat="server" Width="100%">
                  </asp:DropDownList>
              </td>
              <td>
                  <asp:CheckBox ID="Chk_Pay" runat="server" Text="Payment" />
              </td>
          </tr>
       
      
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:Label ID="Lbl_Date0" runat="server"  CssClass="lblCSS"
                      Text="Output To"></asp:Label>
              </td>
              <td>
                  &nbsp;</td>
              <td>
                  <asp:DropDownList ID="CmbOutput" runat="server" Width="100%">
                  </asp:DropDownList>
              </td>
              <td>
                  <asp:CheckBox ID="Chk_Grp" runat="server" Text="Group" />
              </td>
          </tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
                  <td>
                  &nbsp;</td>
              <td>
                 <asp:CheckBox ID="Chck_Excel" runat="server" Text="Excel" Visible="false" />
              </td>
             
          </tr>
      
     </table>
     
     <table width="100%">
     
         <tr>
             <td width="40%">
                     <asp:Panel ID="PnlPage" runat="server" Visible="false">
                            <asp:Label ID="Lbl_AccHead0" runat="server" CssClass="lblCSS"
                                  Text="Start Page" Visible="False"></asp:Label>
                            <asp:TextBox ID="Txt_DateFrom0" runat="server" BackColor="White" 
                                CssClass="twitterStyleTextbox" TabIndex="4" Width="20%"></asp:TextBox>
                        </asp:Panel>
                 &nbsp;</td>
            <td width="40%">
                      <asp:ImageButton ID="ImageButton1" runat="server" Height="32px" 
                          ImageUrl="~/Images/Show.png" Width="96px" />
                      <asp:ImageButton ID="ImageButton2" runat="server" Height="32px" 
                          ImageUrl="~/images/Close.png" Width="96px" />
             </td>
               
                    <td width="20%">
                 
                     
                
                 
                     
             </td>
           
         </tr>
     
    
  
     </table>
 
 
 
 
<br />
     <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
         AssociatedUpdatePanelID="UpdatePanel1">
         <ProgressTemplate>
             <div ID="blur">
                 <asp:Panel ID="Panel1" runat="server">
                     <asp:Panel ID="Panel3" runat="server">
                         <asp:Panel ID="Panel4" runat="server">
                         </asp:Panel>
                     </asp:Panel>
                 </asp:Panel>
             </div>
             <div ID="progress">
                 <img alt="progress" src="Images/progress.gif" /> Please Wait...
             </div>
         </ProgressTemplate>
     </asp:UpdateProgress>
<br />
</div> 
</ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>
