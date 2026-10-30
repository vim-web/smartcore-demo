<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="ChittyNoticePrint.aspx.vb" Inherits="SMARTCORE_UI.ChittyNoticePrint" 
    title="Chitty Notice Print" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    

        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;">
          <legend>GDS Notice Print</legend>
              <table width="100%">
              <tr>
              <td width="2%"></td>
              <td width="48%"><label>Branch</label></td>
              <td width="48%">
                  <asp:DropDownList ID="CmbBranch" runat="server" Width="100%" 
                      AutoPostBack="True">
                  </asp:DropDownList>
                  </td>
              <td width="2%"></td>
              </tr>
                  <tr>
                      <td width="2%">
                           </td>
                      <td width="48%"><label>Scheme</label>
                           </td>
                      <td width="48%">
                          <asp:TextBox ID="TxtScheme" runat="server" Width="25%"></asp:TextBox>
                           <asp:DropDownList ID="CmbScheme" runat="server" Width="70%">
                          </asp:DropDownList>
                      </td>
                      <td width="2%">
                           </td>
                  </tr>
                  <tr>
                      <td width="2%">
                           </td>
                      <td width="48%">
                          <label>Chitty Group</label></td>
                      <td width="48%">
                          <asp:DropDownList ID="CmbChitty" runat="server" Width="100%">
                          </asp:DropDownList>
                      </td>
                      <td width="2%">
                           </td>
                  </tr>
                  <tr>
                      <td width="2%">
                           </td>
                      <td width="48%">
                           <label>
                           Notice Type</label></td>
                      <td width="48%">
                           <asp:DropDownList ID="CmbNoticeType" runat="server" AutoPostBack="True" 
                               Width="100%">
                           </asp:DropDownList>
                           </td>
                      <td width="2%">
                           </td>
                  </tr>
                  </tr>
                  </tr>
                  </tr>
                  <tr>
                      <td width="2%">
                           </td>
                      <td width="48%">
                          <label>
                          Notice Amount</label></td>
                      <td width="48%">
                          <asp:TextBox ID="TxtNoticeAmt" runat="server" Width="100%"></asp:TextBox>
                      </td>
                      <td width="2%">
                           </td>
                  </tr>
                  <tr>
                      <td width="2%">
                      </td>
                      <td width="48%">
                          <label>
                          Notice Sent To</label></td>
                      <td width="48%">
                          <asp:DropDownList ID="CmbNoticeSentTo" runat="server" AutoPostBack="True" 
                              Width="100%">
                              <asp:ListItem>Auctioned Members</asp:ListItem>
                              <asp:ListItem>Non Auctioned Members</asp:ListItem>
                          </asp:DropDownList>
                      </td>
                      <td width="2%">
                      </td>
                  </tr>
                  <tr>
                      <td width="2%">
                           </td>
                      <td width="48%">
                          <label>Select Over Due Numbers</label></td>
                      <td width="48%">
                      
                      <table width ="100%">
                      <tr>
                      <td width ="25%">
                          <label>
                          From
                          </label>
                      </td>
                      <td width ="25%">
                          <asp:TextBox ID="TxtOdNoFrom" runat="server" Width="95%"></asp:TextBox>
                      </td>
                      <td width ="25%">
                          <label>
                          T0</label></td>
                      <td width ="25%">
                          <asp:TextBox ID="TxtOdNoTo" runat="server" Width="100%"></asp:TextBox>
                      </td>
                      </tr>
                      
                      </table>
                      
                      
                      
                          </td>
                      <td width="2%">
                           </td>
                  </tr>
                  <tr>
                      <td width="2%">
                          &nbsp;</td>
                      <td width="48%">
                          <label>
                          As On Date</label></td>
                      <td width="48%">
                          <asp:TextBox ID="TxtFrDate" runat="server" Width="100%"></asp:TextBox>
                          <asp:CalendarExtender ID="TxtFrDate_CalendarExtender" runat="server" 
                              Format="dd/MM/yyyy" TargetControlID="TxtFrDate">
                          </asp:CalendarExtender>
                      </td>
                      <td width="2%">
                          &nbsp;</td>
                  </tr>
                  <tr>
                      <td width="2%">
                           </td>
                      <td width="48%">
                           <applet archive="dist/jzebra.jar" code="jzebra.PrintApplet.class" height="25" 
                               name="jZebra" width="25">
                               <param name="printer" value="zebra" />
                               <!-- <param name="sleep" value="200"> -->
                           </applet></td>
                      <td width="48%">
                           <asp:ImageButton ID="CmdProcess" runat="server" 
                               ImageUrl="~/Images/ProcessBtn.png" />
                           </td>
                      <td width="2%">
                           </td>
                  </tr>
                  <tr>
                      <td width="2%">
                           </td>
                      <td width="48%" colspan="2" style="width: 96%">
                          <asp:GridView ID="DgvNoticeNew" runat="server" AutoGenerateColumns="False" 
                              CellPadding="2" DataKeyNames="Acc_No" ForeColor="#333333">
                              <Columns>
                                  <asp:TemplateField HeaderText="CheckAll">
                                      <HeaderTemplate>
                                          <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="true" 
                                              OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                      </HeaderTemplate>
                                      <ItemTemplate>
                                          <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" 
                                              OnCheckedChanged="chkSelect_CheckedChanged" />
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:BoundField DataField="Acc_No" HeaderText="Account No" 
                                      SortExpression="Acc_No" Visible="false" />
                                  <asp:TemplateField HeaderText="Acc No" SortExpression="Outstanding">
                                      <ItemTemplate>
                                          <asp:TextBox ID="Txtacc_No" runat="server" BorderStyle="none" ForeColor="Blue" 
                                              ReadOnly="true" Text='<%# Bind("Acc_No") %>'>
                        </asp:TextBox>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                      <ItemTemplate>
                                          <asp:TextBox ID="TxtCust_Name" runat="server" BorderStyle="none" 
                                              BorderWidth="0px" ForeColor="Blue" ReadOnly="true" 
                                              Text='<%# Bind("cust_Name") %>'>
                        </asp:TextBox>
                                      </ItemTemplate>
                                  </asp:TemplateField>
<%--                                  <asp:TemplateField HeaderText="Balance" SortExpression="Outstanding">
                                      <ItemTemplate>
                                          <asp:TextBox ID="TxtOut" runat="server" BorderStyle="none" ForeColor="Blue" 
                                              ReadOnly="true" Text='<%# Bind("Outstanding") %>'>
                        </asp:TextBox>
                                      </ItemTemplate>
                                  </asp:TemplateField>--%>
                                  <asp:TemplateField HeaderText="Paid Installment" SortExpression="paidinst">
                                      <ItemTemplate>
                                          <asp:TextBox ID="TxtOdAmt" runat="server" BorderStyle="none" ForeColor="Blue" 
                                              ReadOnly="true" Text='<%# Bind("paidinst") %>'>
                        </asp:TextBox>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Pending Installment" SortExpression="ODno">
                                      <ItemTemplate>
                                          <asp:TextBox ID="TxtInt" runat="server" BorderStyle="none" ForeColor="Blue" 
                                              ReadOnly="true" Text='<%# Bind("ODno") %>'>
                        </asp:TextBox>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                              </Columns>
                          </asp:GridView>
                      </td>
                      <td width="2%">
                           </td>
                  </tr>
                  <tr>
                      <td width="2%">
                           </td>
                      <td colspan="2" style="width: 96%" width="48%">
                          <div ID="DivPrint" runat="server">
                              <table width="100%">
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
                                              style="font-family: 'Courier New', Courier, monospace; font-size: medium;background-color:#0066FF;color:White; height: 26px; font-weight: bold;" 
                                              Text="Generate Print" Width="50%" />
                                          <input ID="Button1" onclick="return Button1_onclick();" 
                                              style="font-family: 'Courier New', Courier, monospace; font-size: medium;background-color:#0066FF;color:White; height: 26px; font-weight: bold;" 
                                              type="button" value="Print" /><asp:Button ID="CmdClose0" runat="server" 
                                              BackColor="#0066FF" Font-Bold="True" Font-Names="Courier New" ForeColor="White" 
                                              Height="26px" Text="Close" Width="74px" />
                                      </td>
                                  </tr>
                                  <tr>
                                      <td class="style1" width="33%">
                                          <asp:HiddenField ID="HdnNoticeHead" runat="server" />
                                          <asp:HiddenField ID="HdnText" runat="server" />
                                      </td>
                                      <td class="style1" width="33%">
                                          <asp:HiddenField ID="HdnMiscHead" runat="server" />
                                      </td>
                                      <td class="style1" width="34%">
                                      </td>
                                  </tr>
                                  <tr>
                                      <td width="33%">
                                           </td>
                                      <td width="33%">
                                           </td>
                                      <td width="34%">
                                           </td>
                                  </tr>
                              </table>
                          </div>
                      </td>
                      <td width="2%">
                           </td>
                  </tr>
                  <tr>
                      <td width="2%">
                            </td>
                      <td align="center" colspan="2" style="width: 96%" width="48%">
                           </td>
                      <td width="2%">
                            </td>
                  </tr>
                  <tr>
                      <td width="2%">
                      </td>
                      <td align="center" colspan="2" style="width: 96%" width="48%">
                          <asp:ImageButton ID="CmdShow" runat="server" ImageUrl="~/Images/Show.png" />
                          <asp:ImageButton ID="CmdClose" runat="server" 
                              ImageUrl="~/Images/CloseBtn.png" />
                      </td>
                      <td width="2%">
                      </td>
                  </tr>
              </table>
          </legend>
              </div>
    </tr>
   </ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>
