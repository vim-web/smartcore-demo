<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LoanEMIChart.aspx.vb" Inherits="SMARTCORE_UI.LoanEMIChart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />      
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    
    
    <div class="gradientBoxesWithOuterShadows" style="width: 98%; margin-left:1%; margin-right:1%;margin-top:1%;">                      
    <br />    
        <table width ="100%">
            <tr>
            <td width = "2%"></td>
            <td width = "96%">
            <asp:GridView ID="DgvEMI" runat="server" AutoGenerateColumns="False" 
                TabIndex="32" Width="100%">
                <Columns>
                    <asp:BoundField DataField="Inst_No" HeaderText="Inst_No">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Inst_Date" HeaderText="Installment Date"  DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="15%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" ForeColor="#333399" />
                    </asp:BoundField>
 
                    <asp:BoundField DataField="Inst_Amount" HeaderText="Total Amount" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="15%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" ForeColor="#333399" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Inst_Princpal" HeaderText="Princpal" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="15%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" ForeColor="#333399" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Inst_Interest" HeaderText="Interest" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="15%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" ForeColor="#333399" />
                    </asp:BoundField>
                                        <asp:BoundField DataField="Prin_Balance" HeaderText="Princpal Balance" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="15%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" ForeColor="#333399" />
                    </asp:BoundField>
                </Columns>
                <AlternatingRowStyle BackColor="#F5F7FE" />
            </asp:GridView>
            </td>
            <td width = "2%"></td>
            </tr>
        </table>
            
        <br />
    </div>
    
            <asp:HiddenField ID="hdnAccid" runat="server" />
    </form>
</body>
</html>
