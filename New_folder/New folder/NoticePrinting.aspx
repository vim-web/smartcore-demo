<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NoticePrinting.aspx.vb" Inherits="SMARTCORE_UI.NoticePrinting" enableEventValidation="false"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />  
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
        <script src="Scripts/Print.js" type="text/javascript"></script> 

    <script language="javascript" type="text/javascript">
// <!CDATA[

        function print() {
            alert('hai');
        }

        function Button1_onclick() {
            var DrpPrn = document.getElementById('<%=DrpPrinters.ClientID%>');
            var Prn = document.getElementById('<%=HdnText.ClientID%>');
            var ClPrinter1 = DrpPrn.options[DrpPrn.selectedIndex].text;
            printNew(Prn.value, ClPrinter1);
        }
        function exportToExcel() {
            var oExcel = new ActiveXObject("Excel.Application");
            var oBook = oExcel.Workbooks.Add;
            var oSheet = oBook.Worksheets(1);
            for (var y = 0; y < GridView1.rows.length; y++)
            // GridView3 is the table where the content to be exported is
            {
                for (var x = 0; x < GridView1.rows(y).cells.length; x++) {
                    oSheet.Cells(y + 1, x + 1) =
    GridView1.rows(y).cells(x).innerText;
                }
            }
            oExcel.Visible = true;
            oExcel.UserControl = true;
        }

// ]]>
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>


        

     <div class="gradientBoxesWithOuterShadows" style="width: 70%; margin-left:15%; margin-right:15%; margin-top:10%">
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
                      <asp:TextBox ID="TxtAsOnDate" runat="server" Width="99%"></asp:TextBox>


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
                      <asp:TextBox ID="TxtAuctionDate" runat="server" Width="100%"></asp:TextBox>

                  </td>
                  <td>
                     <label>&nbsp; Time</label></td>
                  <td>
                      <asp:TextBox ID="TxtTime" runat="server" Width="100%"></asp:TextBox>

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
                      <asp:TextBox ID="TxtMiscAmt" runat="server" Width="100%"></asp:TextBox>
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
                          GroupName="LIstType" Visible="False" />
                  </td>
                  <td colspan="2">
                      <asp:RadioButton ID="OptOverDue" runat="server" Text="Overdue" Checked="True" 
                          GroupName="LIstType" Visible="False" />
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
                          <asp:CheckBox ID="Chk_BetOD_Date" runat="server" Text="Between OD Date" 
                              AutoPostBack="True" />
                          </td>
                      <td width="23%"></td>
                      <td width="23%"></td>
                      <td width="2%"></td>
                      <td width="23%"></td>
                      </tr>
                      <tr>
                      <td colspan="4">
                            <div id ="DivOdDate" runat ="server" >
                            <table width ="100%">
                            <tr>
                            <td width ="25%"> <label>From Date</label> </td>
                            <td width ="25%">  
                                                              <asp:TextBox ID="TxtOdDateFrom" runat="server" Width="100%"></asp:TextBox>

                                                                                    </td>
                            <td width ="25%">  
                                <label>
                                &nbsp; To Date</label></td>
                            <td width ="25%">
                                                              <asp:TextBox ID="TxtOdDateTo" runat="server" Width="100%"></asp:TextBox>

                                                                                    </td>
                            </tr>

                            </table>
                            </div>
                          
                          
                          
                          
                          </td>
                      <td width="23%">&nbsp;</td>
                      </tr>
                          <tr>
                              <td width="25%">
                                  <asp:CheckBox ID="ChkLoanNo" runat="server" Text="Between Loan No" 
                                      AutoPostBack="True" />
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
                              <td colspan="4">
                                  <div id="DivLoanNo" runat="server">
                                      <table width="100%">
                                          <tr>
                                              <td width="25%">
                                                  <label>
                                                  From No</label>
                                                        </td>
                                                        <td width="25%">
                                  <asp:TextBox ID="TxtLoanNoFrom" runat="server" Width="100%"></asp:TextBox>

                                                        </td>
                                                        <td width="25%">
                                                            <label>
                                                            &nbsp; To No</label></td>
                                                        <td width="25%">
                                  <asp:TextBox ID="TxtLoanNoTo" runat="server" Width="100%"></asp:TextBox>

                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                              </td>
                              <td>
                                  &nbsp;</td>
                          </tr>
                          <tr>
                              <td width="25%">
                                  <asp:CheckBox ID="ChkOdNo" runat="server" Text="Between OD No" 
                                      AutoPostBack="True" />
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
                              <td colspan="4">
                                  <div id="DivODNo" runat="server">
                                      <table width="100%">
                                          <tr>
                                              <td width="25%">
                                                  <label>
                                                  From OD No</label>
                                                        </td>
                                                        <td width="25%">
                                  <asp:TextBox ID="TxtOdNoFrom" runat="server" Width="100%"></asp:TextBox>

                                                        </td>
                                                        <td width="25%">
                                                            <label>
                                                            &nbsp; To OD No</label></td>
                                                        <td width="25%">
                                  <asp:TextBox ID="TxtOdNoTo" runat="server" Width="100%"></asp:TextBox>

                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                              </td>
                              <td>
                                  &nbsp;</td>
                          </tr>
                          <tr>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
                              <td width="25%">
                                  &nbsp;</td>
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
                      <label>
                      Sent Type</label></td>
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
                              <a id="A1" href="#" onclick="javascript: CheckBoxListSelect ('<%= Lst_ActInclude.ClientID %>',true)">
                              Select All</a>
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
                      &nbsp;</td>
                  <td>
                      &nbsp;</td>
              </tr>
              <tr>
                  <td>
                      &nbsp;</td>
                  <td align="center" colspan="4">
                      <asp:ImageButton ID="CmdProcess" runat="server" 
                          ImageUrl="~/Images/ProcessBtn.png" />
                                        <asp:ImageButton ID="CmdClose" runat="server" height="30px"
                         ImageUrl="~/Images/CloseBtn.png" Width="80px" />
                  </td>
                  <td>
                      &nbsp;</td>
              </tr>
              <tr>
                  <td>
                      &nbsp;</td>
                  <td colspan="4" align="center">
      <%--          <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                    AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div ID="blur">
                        </div>
                        <div ID="progress">
                            <img alt="progress" src="Images/progress.gif" /> Please Wait...
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>--%>

                  </td>
                  <td>
                      &nbsp;</td>
              </tr>
              
              <tr>
                  <td>
                      &nbsp;</td>
                  <td colspan="4" align="center">

                      <asp:GridView ID="DgvNotice" runat="server" Width="100%" CssClass="grid-view" 
                          AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Acc_No" 
                 onrowcommand="DgvNotice_RowCommand" Visible ="false" >
                          <Columns>

                                          
                          
                     <asp:CommandField SelectText="Print" ShowSelectButton="True">
                         <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" Width="5%" />
                     </asp:CommandField>
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
                            
                              <asp:CheckBoxField />
                            
                          </Columns>
                      </asp:GridView>
                      
                            <asp:GridView ID="DgvNoticeNew" runat="server" 
                                      AutoGenerateColumns="False" 
                                      CellPadding="2" ForeColor="#333333" 
                                      DataKeyNames="Acc_No">
                        <Columns>
                        
                        
                        <asp:TemplateField HeaderText="CheckAll">
                        <HeaderTemplate>
                        <asp:CheckBox ID="chkSelectAll" runat="server" 
                                      AutoPostBack="true" 
                                      OnCheckedChanged="chkSelectAll_CheckedChanged"/>
                        </HeaderTemplate>
                        <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" 
                                      AutoPostBack="true" 
                                      OnCheckedChanged="chkSelect_CheckedChanged"/>
                        </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Acc_No" HeaderText="Account No" 
                                        SortExpression="Acc_No" Visible ="false" />
                                        
                        <asp:TemplateField HeaderText="Acc No" SortExpression="Outstanding">
                        <ItemTemplate>
                        <asp:TextBox ID="Txtacc_No" runat="server" 
                                     Text='<%# Bind("Acc_No") %>' 
                                     ForeColor="Blue" BorderStyle="none" 
                                     ReadOnly="true">
                        </asp:TextBox>
                        </ItemTemplate>
                        </asp:TemplateField>

                                        
                                        
                        <asp:TemplateField HeaderText="Name" SortExpression="Name">
                        <ItemTemplate>
                        <asp:TextBox ID="TxtCust_Name" runat="server" 
                                     Text='<%# Bind("cust_Name") %>' ForeColor="Blue" 
                                     BorderStyle="none" BorderWidth="0px" 
                                     ReadOnly="true" >
                        </asp:TextBox>
                        </ItemTemplate>
                        </asp:TemplateField>
                        
                        

                        <asp:TemplateField HeaderText="Balance" SortExpression="Outstanding">
                        <ItemTemplate>
                        <asp:TextBox ID="TxtOut" runat="server" 
                                     Text='<%# Bind("Outstanding") %>' 
                                     ForeColor="Blue" BorderStyle="none" 
                                     ReadOnly="true">
                        </asp:TextBox>
                        </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="OD Amount" SortExpression="od_amt">
                        <ItemTemplate>
                        <asp:TextBox ID="TxtOdAmt" runat="server" 
                                     Text='<%# Bind("od_amt") %>' 
                                     ForeColor="Blue" BorderStyle="none" 
                                     ReadOnly="true">
                        </asp:TextBox>
                        </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="Interest" SortExpression="Accrude_IntAmt">
                        <ItemTemplate>
                        <asp:TextBox ID="TxtInt" runat="server" 
                                     Text='<%# Bind("Accrude_IntAmt") %>' 
                                     ForeColor="Blue" BorderStyle="none" 
                                     ReadOnly="true">
                        </asp:TextBox>
                        </ItemTemplate>
                        </asp:TemplateField>


                        </Columns>
                        </asp:GridView>

                  </td>
                  <td>
                      &nbsp;</td>
              </tr>

              
              <tr>
                  <td>
                      &nbsp;</td>
                  <td colspan="4" align="center">

                      <asp:HiddenField ID="HdnMiscHead" runat="server" />

                  </td>
                  <td>
                      &nbsp;</td>
              </tr>

              
              <tr>
                  <td colspan="6">
<div id="DivPrint"  runat ="server"> 
<table width ="100%">
<tr>
<td width="33%">

                      <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Larger" 
                          ForeColor="#3333CC" Text="Select  Printer " Width="100%"></asp:Label>
                  </td>
<td width="33%">

                      <asp:DropDownList ID="DrpPrinters" runat="server" Width="96%">
                      </asp:DropDownList>
                                        </td>
<td width="34%">

                      <asp:Button ID="BtnGeneratePrint" runat="server" 
        Text="Generate Print"   style="font-family: 'Courier New', Courier, monospace; font-size: medium;background-color:#0066FF;color:White; height: 26px; font-weight: bold;" 
                          Width="50%" />

                        <input id="Button1" onclick="return Button1_onclick();" 
                          style="font-family: 'Courier New', Courier, monospace; font-size: medium;background-color:#0066FF;color:White; height: 26px; font-weight: bold;" 
                          type="button" value="Print" /></td>
</tr>
<tr>
<td width="33%"></td>
<td width="33%"></td>
<td width="34%">&nbsp;</td>
</tr>

<tr>
<td width="33%">
                      &nbsp;</td>
<td width="33%">
                      &nbsp;</td>
<td width="34%">&nbsp;</td>
</tr>

</table>

</div>                  
                  
                   </td>
              </tr>

              
              <tr>
                  <td>
                      &nbsp;</td>
                  <td>
                      <asp:HiddenField ID="HdnNoticeHead" runat="server" />
                  </td>
                  <td>

                      &nbsp;</td>
                  <td>
                      <label>
                      &nbsp; </label>&nbsp;
                        </td>
                  <td>
    <applet archive="dist/jzebra.jar" code="jzebra.PrintApplet.class" height="25" 
           name="jZebra" width="25">
           <param name="printer" value="zebra" />
<!-- <param name="sleep" value="200"> -->
       </applet>
                      
                      <asp:HiddenField ID="HdnText" runat="server" />
                      
                      </td>
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
             </td>
                  <td>
                      &nbsp;</td>
                  <td>
                      &nbsp;</td>
              </tr>
          </table>



          </div>

    </form>
        <p>
            &nbsp;</p>
</body>
</html>
