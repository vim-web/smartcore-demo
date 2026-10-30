<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="PrintingNotice.aspx.vb" Inherits="SMARTCORE_UI.PrintingNotice" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type='text/javascript' src="Scripts/Common.js"></script> 
<script language="javascript" type="text/javascript">
 
function CheckBoxListSelect(cbControl, state)
{   
       var chkBoxList = document.getElementById(cbControl);
        var chkBoxCount= chkBoxList.getElementsByTagName("input");
        for(var i=0;i<chkBoxCount.length;i++)
        {
            chkBoxCount[i].checked = state;
        }
       
        return false; 
}
 
</script>
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
                        inputText.focus();
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
                        inputText.focus();
                        return false;
                    }
                    if ((lyear == true) && (dd > 29)) {
                        alert('Invalid date format! Please enter a valid date!');
                        inputText.focus();
                        return false;
                    }
                }
            }
            else {
                alert("Invalid date format! Please enter a valid date!");
                inputText.focus();
                document.form1.text1.focus();
                return false;
            }
        }  

</script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    

        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;">
          <legend>Notice Printing</legend>
          <table width="100%">
          <tr>
          <td width="2%"></td>
           <td width="13%"></td>
           <td width="13%"></td>
           <td width="13%"></td>
           <td width="13%"></td>
           <td width="2%"></td>
          </tr>
              <tr>
                  <td>&nbsp;</td>
                  <td>
                      <label>
                      Branch</label></td>
                  <td>
                      <asp:TextBox ID="TxtBranch" runat="server" Width="96%"></asp:TextBox>
                      </td>
                  <td colspan="2">
                      <asp:DropDownList ID="CmbBranch" runat="server" Width="100%" 
                          AutoPostBack="True">
                      </asp:DropDownList>
                      </td>
                  <td>
                      </td>
              </tr>
              <tr>
                  <td>
                      &nbsp;</td>
                  <td>
                      <label>
                      As On Date</label></td>
                  <td colspan="3">
                      <asp:TextBox ID="TxtAsOnDate" runat="server" Width="100%" OnChange="validatedate(this);"></asp:TextBox>
                    <asp:CalendarExtender ID="TxtIntimationDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                      TargetControlID="TxtAsOnDate">
                    </asp:CalendarExtender>

                  </td>
                  <td>
                      &nbsp;</td>
              </tr>
              <tr>
                  <td>
                      </td>
                  <td>
                      &nbsp;<label>Loan Type</label></td>
                  <td>
                      <asp:DropDownList ID="CmbLoanTyype" runat="server" Width="100%" 
                          AutoPostBack="True">
                      </asp:DropDownList>
                  </td>
                  <td>
                      <label>
                      &nbsp; Notice Type</label></td>
                  <td align="center">
                      <asp:DropDownList ID="CmbNoticeType" runat="server" Width="100%" 
                          AutoPostBack="True">
                      </asp:DropDownList>
                  </td>
                  <td>
                  </td>
              </tr>
              <tr>
                  <td>
                      &nbsp;</td>
                  <td>
                      <label>Notice Amount</label></td>
                  <td>
                      <asp:TextBox ID="TxtNoticeAmt" runat="server" Width="100%"></asp:TextBox>
                  </td>
                  <td>
                      <label>&nbsp; Ref No </label></td>
                  <td>
                      <asp:TextBox ID="TxtRefNo" runat="server" Width="100%"></asp:TextBox>
                  </td>
                  <td>
                      &nbsp;</td>
              </tr>
              <tr>
                  <td>
                      &nbsp;</td>
                  <td>
                      <label>Auction Date</label></td>
                  <td>
                      <asp:TextBox ID="TxtAuctionDate" runat="server" Width="100%" OnChange="validatedate(this);"></asp:TextBox>
                      <asp:CalendarExtender ID="TxtAuctionDate_CalendarExtender" runat="server" 
                          TargetControlID="TxtAuctionDate">
                      </asp:CalendarExtender>
                  </td>
                  <td>
                     <label>&nbsp; Time</label></td>
                  <td>
                      <asp:TextBox ID="TxtTime" runat="server" Width="100%"></asp:TextBox>
                      <asp:CalendarExtender ID="TxtTime_CalendarExtender" runat="server" 
                          TargetControlID="TxtTime">
                      </asp:CalendarExtender>
                  </td>
                  <td>
                      &nbsp;</td>
              </tr>
              <tr>
                  <td>
                      </td>
                  <td>
                      <label>Misc Ammount</label></td>
                  <td>
                      <asp:TextBox ID="TxtMiscAmt" runat="server" Width="100%" onkeypress="return isFloatKey(event);"></asp:TextBox>
                  </td>
                  <td>
                      </td>
                  <td>
                      </td>
                  <td>
                      </td>
              </tr>
              <tr>
                  <td>
                  </td>
                  <td colspan="2">
                      <asp:RadioButton ID="RadOutstanding" runat="server" Text="Outstanding" 
                          GroupName="LIstType" />
                  </td>
                  <td colspan="2">
                      <asp:RadioButton ID="OptOverDue" runat="server" Text="Overdue" Checked="True" 
                          GroupName="LIstType" />
                  </td>
                  <td>
                      &nbsp;</td>
              </tr>
              <tr>
                  <td>
                      &nbsp;</td>
                  <td colspan="4">
                      <div class="gradientBoxesWithOuterShadowsLite">
                      <table width="100%">
                      <tr>
                      <td width="2%">
                          <asp:CheckBox ID="CheckBox1" runat="server" Text="Between OD Date" />
                          </td>
                      <td width="23%"></td>
                      <td width="23%"></td>
                      <td width="2%"></td>
                      <td width="23%"></td>
                      </tr>
                          <tr>
                              <td width="25%">
                                  <label>From</label></td>
                              <td width="25%">
                                  <asp:TextBox ID="TextBox1" runat="server" Width="100%"></asp:TextBox>
                              </td>
                              <td width="25%">
                                  <label>
                                  &nbsp;To</label></td>
                              <td width="25%">
                                  <asp:TextBox ID="TxtOdDateTo0" runat="server" Width="100%"></asp:TextBox>
                                  <asp:CalendarExtender ID="TxtOdDateTo0_CalendarExtender" runat="server" 
                                      TargetControlID="TxtOdDateTo0">
                                  </asp:CalendarExtender>
                              </td>
                              <td>
                              </td>
                          </tr>
                          <tr>
                              <td width="25%">
                                  <asp:CheckBox ID="ChkLoanNo0" runat="server" Text="Between Loan No" />
                              </td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td>
                              </td>
                          </tr>
                          <tr>
                              <td width="25%">
                                  <label>
                                  From</label></td>
                              <td width="25%">
                                  <asp:TextBox ID="TxtLoanNoFrom0" runat="server" Width="100%"></asp:TextBox>
                                  <asp:CalendarExtender ID="TxtLoanNoFrom0_CalendarExtender" runat="server" 
                                      TargetControlID="TxtLoanNoFrom0">
                                  </asp:CalendarExtender>
                              </td>
                              <td width="25%">
                                  <label>
                                  &nbsp;To</label></td>
                              <td width="25%">
                                  <asp:TextBox ID="TxtLoanNoTo0" runat="server" Width="100%"></asp:TextBox>
                                  <asp:CalendarExtender ID="TxtLoanNoTo0_CalendarExtender" runat="server" 
                                      TargetControlID="TxtLoanNoTo0">
                                  </asp:CalendarExtender>
                              </td>
                              <td>
                              </td>
                          </tr>
                          <tr>
                              <td width="25%">
                                  <asp:CheckBox ID="ChkOdNo0" runat="server" Text="Between OD No" />
                              </td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td>
                              </td>
                          </tr>
                          <tr>
                              <td width="25%">
                                  <label>
                                  From</label></td>
                              <td width="25%">
                                  <asp:TextBox ID="TxtOdNoFrom" runat="server" Width="100%"></asp:TextBox>
                                  <asp:CalendarExtender ID="TxtOdNoFrom_CalendarExtender" runat="server" 
                                      TargetControlID="TxtOdNoFrom">
                                  </asp:CalendarExtender>
                              </td>
                              <td width="25%">
                                  <label>
                                  &nbsp;To</label></td>
                              <td width="25%">
                                  <asp:TextBox ID="TxtOdNoTo" runat="server" Width="100%"></asp:TextBox>
                                  <asp:CalendarExtender ID="TxtOdNoTo_CalendarExtender" runat="server" 
                                      TargetControlID="TxtOdNoTo">
                                  </asp:CalendarExtender>
                              </td>
                              <td>
                              </td>
                          </tr>
                      </table>
                      </div>
                      
                      
                      </td>
                  <td>
                      &nbsp;</td>
              </tr>
              <tr>
              
                  <td>
                      &nbsp;</td>
                  <td>
                      <asp:RadioButton ID="OptLoanee" runat="server" GroupName="Main" Text="Loanee" 
                          Checked="True" />
                  </td>
                  <td>
                      <asp:RadioButton ID="OptSurties" runat="server" GroupName="Main" 
                          Text="Sureties" />
                  </td>
                  <td>
                      <asp:RadioButton ID="OptBoth" runat="server" GroupName="Main" Text="Both" />
                  </td>
                  <td>
                  </td>
                  <td>
                  </td>
              </tr>
              <tr>
                  <td>
                      &nbsp;</td>
                  <td>
                      <label>
                      Print Type</label></td>
                  <td colspan="3">
                      <asp:DropDownList ID="CmbPrintType" runat="server" Width="100%">
                      </asp:DropDownList>
                  </td>
                  <td>
                      &nbsp;</td>
              </tr>
              <tr>
                  <td>
                      &nbsp;</td>
                  <td colspan="2">
                      <asp:CheckBox ID="ChkActionType" runat="server" Text="Action Type Wise" 
                          AutoPostBack="True" />
                  </td>
                  <td colspan="2">
                      <asp:CheckBox ID="ChkPrintPreview" runat="server" Text="Show Print Preview" />
                  </td>
                  <td>
                      &nbsp;</td>
              </tr>
              <tr>
                  <td>
                      &nbsp;</td>
                  <td colspan="2">
                       <div id="DivActExcept" class="gradientBoxesWithOuterShadows" runat="server" visible="false">
                  <table width="100%">
                      <tr>
                          <td width="100%">
                              <a id="A3" href="#" onclick="javascript: CheckBoxListSelect ('<%= Lst_ActExcept.ClientID %>',true)">Select All</a>
                              &nbsp;
                              
                              <label>Except Action</label>
                              <asp:CheckBoxList ID="Lst_ActExcept" runat="server">
                              </asp:CheckBoxList>
                          </td>
                          
                      </tr>
                  </table>
                  </div></td>
                  <td colspan="2">
                      <div id="DivActInclude" class="gradientBoxesWithOuterShadows" runat="server" visible="false">
                  <table width="100%">
                      <tr>
                          <td width="100%">
                              <a id="A1" href="#" onclick="javascript: CheckBoxListSelect ('<%= Lst_ActInclude.ClientID %>',true)">Select All</a>
                              &nbsp;<label>Incude Action</label>
                               <asp:CheckBoxList ID="Lst_ActInclude" runat="server">
                              </asp:CheckBoxList>
                          </td>
                          
                      </tr>
                  </table>
                  </div>
                  </td>
                  <td>
                      &nbsp;</td>
              </tr>
              <tr>
                  <td>
                      &nbsp;</td>
                  <td colspan="4" align="center">
                      <asp:ImageButton ID="CmdProcess" runat="server" 
                          ImageUrl="~/Images/ProcessBtn.png" />
                      <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />
                  </td>
                  <td>
                      &nbsp;</td>
              </tr>
              <tr>
                  <td>
                      &nbsp;</td>
                  <td colspan="4" align="center">
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
                  <td>
                      &nbsp;</td>
              </tr>
              
              <tr>
                  <td>
                      &nbsp;</td>
                  <td colspan="4" align="center">

                      <asp:GridView ID="DgvNotice" runat="server" Width="100%" CssClass="grid-view" 
                          AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False">
                          <Columns>
                              <asp:BoundField DataField="Acc_No" HeaderText="Account No">
                                  <HeaderStyle Width="15%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="Cust_Name" HeaderText="Name">
                                  <HeaderStyle Width="30%" />
                                  <ItemStyle HorizontalAlign="Left" />
                              </asp:BoundField>
                              <asp:BoundField DataField="Outstanding" HeaderText="Balance">
                                  <HeaderStyle Width="15%" />
                                  <ItemStyle HorizontalAlign="Right" />
                              </asp:BoundField>
                              <asp:BoundField DataField="OD_Amt" HeaderText="OD Amount">
                                  <HeaderStyle Width="15%" />
                                  <ItemStyle HorizontalAlign="Right" />
                              </asp:BoundField>
                              <asp:BoundField DataField="Accrude_IntAmt" HeaderText="Interest">
                                  <HeaderStyle Width="15%" />
                                  <ItemStyle HorizontalAlign="Right" />
                              </asp:BoundField>
                          </Columns>
                      </asp:GridView>

                  </td>
                  <td>
                      &nbsp;</td>
              </tr>

              
              <tr>
                  <td>
                      &nbsp;</td>
                  <td>
                      <asp:HiddenField ID="HdnNoticeHead" runat="server" />
                  </td>
                  <td>
                      <asp:HiddenField ID="HdnMiscHead" runat="server" />
                  </td>
                  <td>
                      <label>
                      &nbsp; </label></td>
                  <td>
                      &nbsp;</td>
                  <td>
                      &nbsp;</td>
              </tr>
          </table>



          </div>
   </ContentTemplate>
</asp:UpdatePanel> 


</asp:Content>
