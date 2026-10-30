<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DayEndRep.aspx.vb" Inherits="SMARTCORE_UI.DayEndRep" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Day End Report</title>
        <script language="javascript" type="text/javascript">        
      function CallPrint(strid)
      {
          var prtContent = document.getElementById(strid);
          var WinPrint = window.open('','','letf=0,top=0,width=400,height=400,toolbar=0,scrollbars=0,status=0');
          WinPrint.document.write(prtContent.innerHTML);
          WinPrint.document.close();
          WinPrint.focus();
          WinPrint.print();
          WinPrint.close();
       }
function btnClose_onclick() {

}

        </script>
        <style type="text/css">
        label {
	        color:  #03F;
	        font-weight: bold;
        }
        .ButtonStyle {
	        color: #FFF;
	        background-color: #039;
	        text-align: center;
	        display: inline;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divPrint" runat="server">
    <table width = "100%">
        <tr>
            <td width = "100%" align ="center">
            
                <asp:Label ID="LblBankName" runat="server" Font-Bold="True" Font-Size="Larger" 
                    ForeColor="#660066"></asp:Label>
            
            </td>
        </tr>
        <tr>
            <td width = "100%" align ="center">
            
                <asp:Label ID="LblHeading" runat="server" Font-Bold="True" Font-Size="Large" 
                    ForeColor="#CC0099"></asp:Label>
            
            </td>
        </tr>
        </table>
    <table width = "100%">
        <tr>
            <td width = "50%" valign ="top">
            <table width = "100%">
            <tr>
                <td width = "100%"   >
                    <Legend> Day End Summary</Legend>
                    <asp:GridView ID="DgvSummary" runat="server" AutoGenerateColumns="false" 
                    TabIndex="32" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="Particulars" HeaderText="Particulars">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="70%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Value" HeaderText="Value">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="30%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                    </Columns>
                    <AlternatingRowStyle BackColor="#F5F7FE" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td width = "100%">
                    
                    </td>
            </tr>            
            </table>
            </td>
            <td width = "50%" valign ="top">
                <table  width = "100%">
                    <tr>
                        <td  width = "100%">
                        <Legend> Performance Analysis</Legend>
                    <asp:GridView ID="DgvPerformance" runat="server" AutoGenerateColumns="false" 
                    TabIndex="32" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="Particulars" HeaderText="Particulars">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="70%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Value" HeaderText="Value">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="30%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                    </Columns>
                    <AlternatingRowStyle BackColor="#F5F7FE" />
                    </asp:GridView>                

                        </td>
                    </tr>

                    <tr>
                        <td  width = "100%">
                            &nbsp;</td>
                    </tr>

                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                    
                    </td>
        </tr>

        <tr>
            <td colspan="2">
                    <Legend> Share Details  ...... </Legend>
                    <asp:GridView ID="DgvShare" runat="server" AutoGenerateColumns="False" 
                    TabIndex="32" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="SchemeName" HeaderText="Scheme">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="20%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OpBalanceNo" HeaderText="Op Bal No">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="OpBalanceAmt" HeaderText="Op Bal Amt" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="ReceiptNo" HeaderText="Rct No" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="ReceiptAmt" HeaderText="Rct Amt" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="PayNo" HeaderText="Pay No" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="PayAmt" HeaderText="Pay Amt" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="BalanceNo" HeaderText="Balance No" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="BalanceAmt" HeaderText="Balance Amt" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="GLBalance" HeaderText="GL Bal" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="DifAmt" HeaderText="Difference" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                    </Columns>
                    <AlternatingRowStyle BackColor="#F5F7FE" />
                    </asp:GridView>                
            </td>
        </tr>
        <tr>
            <td colspan="2"></td>
        </tr>
        <tr>
            <td colspan="2"></td>
        </tr>
        <tr>
            <td width = "50%" colspan="2" style="width: 100%">
                    <Legend> Deposit Details  ...... </Legend>
                    <asp:GridView ID="DgvDeposit" runat="server" AutoGenerateColumns="False" 
                    TabIndex="32" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="SchemeName" HeaderText="Scheme">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="20%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OpBalanceNo" HeaderText="Op Bal No">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="OpBalanceAmt" HeaderText="Op Bal Amt" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="ReceiptNo" HeaderText="Rct No" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="ReceiptAmt" HeaderText="Rct Amt" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="PayNo" HeaderText="Pay No" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="PayAmt" HeaderText="Pay Amt" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="BalanceNo" HeaderText="Balance No" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="BalanceAmt" HeaderText="Balance Amt" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="MaturedAmt" HeaderText="Matured" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="IntAmt" HeaderText="Interest" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="GLBalance" HeaderText="GL Bal" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="DifAmt" HeaderText="Difference" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                    </Columns>
                    <AlternatingRowStyle BackColor="#F5F7FE" />
                    </asp:GridView>                
            </td>
        </tr>
        <tr>
            <td colspan="2"></td>
        </tr>
        <tr>
            <td colspan="2"></td>
        </tr>
        <tr>
            <td width = "50%" colspan="2" style="width: 100%; margin-left: 120px;">
                    <Legend> Loan Details  ...... </Legend>
                    <asp:GridView ID="DgvLoan" runat="server" AutoGenerateColumns="False" 
                    TabIndex="32" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="SchemeName" HeaderText="Scheme">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="20%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OpBalanceNo" HeaderText="Op Bal No">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="OpBalanceAmt" HeaderText="Op Bal Amt" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="ReceiptNo" HeaderText="Rct No" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="ReceiptAmt" HeaderText="Rct Amt" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="PayNo" HeaderText="Pay No" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="PayAmt" HeaderText="Pay Amt" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="BalanceNo" HeaderText="Balance No" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="BalanceAmt" HeaderText="Balance Amt" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="OverdueAmt" HeaderText="Overdue" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="OverduePer" HeaderText="Overdue %" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="IntAmt" HeaderText="Interest" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="ODIntAmt" HeaderText="OD Interest" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="GLBalance" HeaderText="GL Bal" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="DifAmt" HeaderText="Difference" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="7%" 
                                Font-Names="Times New Roman" HorizontalAlign="Left" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" ForeColor="#333399"  HorizontalAlign="Right"/>
                        </asp:BoundField>
                    </Columns>
                    <AlternatingRowStyle BackColor="#F5F7FE" />
                    </asp:GridView>                
            </td>
        </tr>
        <tr>
            <td width = "50%" colspan="2" style="width: 100%; margin-left: 120px;">
                    &nbsp;</td>
        </tr>
        <tr>
            <td width = "50%" colspan="2" style="width: 100%; margin-left: 120px;">
                    &nbsp;</td>
        </tr>
    </table>
    </div>
    <table width = "100%">
    <tr>
        <td width = "25%">
        </td>
        <td width = "50%" align ="center">            
            <input type="button" value="PRINT" id="btnPrint" Class ="ButtonStyle"  runat="Server" onclick="javascript:CallPrint('divPrint')" />
            <asp:Button ID="CmdClose" runat="server" Text="CLOSE" 
                CssClass ="ButtonStyle" />        
            </td>
        <td width = "25%">
        </td>
    </tr>
    </table>
    </form>
</body>
</html>
