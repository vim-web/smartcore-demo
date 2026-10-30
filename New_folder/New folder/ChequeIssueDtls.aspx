<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ChequeIssueDtls.aspx.vb" Inherits="SMARTCORE_UI.ChequeIssueDtls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />      
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="gradientBoxesWithOuterShadows" style="width: 98%; margin-left:1%; margin-right:1%;margin-top:1%;">                      
            <legend>TOTAL CHEQUE STATUS</legend>    
            <br />
            <table width  = "100%">
            <tr>
            <td width = 2%"></td>
            <td width = 96%">
            <asp:GridView ID="DgvIssueDtls0" runat="server" AutoGenerateColumns="False" CssClass="grid-view"
                TabIndex="32" Width="100%">
                <Columns>
                    <asp:BoundField DataField="Issue_Date" HeaderText="Issue Date" 
                        DataFormatString="{0:dd/MM/yyyy}" >
                        <HeaderStyle Width="40%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NoofLeafs" HeaderText="No of Leafs" >
                        <HeaderStyle Width="20%" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FromNo" HeaderText="From Number" >
                        <HeaderStyle Width="20%" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ToNo" HeaderText="To Number" >
                        <HeaderStyle Width="20%" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>                
            </asp:GridView>
            <asp:GridView ID="DgvStatusDtls" runat="server" AutoGenerateColumns="False" CssClass="grid-view"
                TabIndex="32" Width="100%">
                <Columns>
                    <asp:BoundField DataField="No_of_ChequeIssued" HeaderText="Issued">
                        <HeaderStyle Width="20%" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="No_of_ChequePresented" HeaderText="Presented" >
                        <HeaderStyle Width="20%" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="No_of_StopPayment" HeaderText="Stop Payment" >
                        <HeaderStyle Width="20%" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="No_Of_ChequeReturn" HeaderText="Cheque Return" >
                        <HeaderStyle Width="20%" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="No_of_ChequeInHand" HeaderText="Cheque In Hand" >
                        <HeaderStyle Width="20%" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
                
            </asp:GridView>
            </td>
            <td width = 2%"></td>
            </tr>
            </table>
        </div>
        <br />
        <div class="gradientBoxesWithOuterShadows" style="width: 98%; margin-left:1%; margin-right:1%;margin-top:1%;">                      
            <legend>CHEQUE ISSUE DETAILS</legend>    
            <br />
            <table width  = "100%">
            <tr>
            <td width = 2%"></td>
            <td width = 96%">            
            <asp:GridView ID="DgvIssueDtls" runat="server" AutoGenerateColumns="False" CssClass="grid-view"
                TabIndex="32" Width="100%">
                <Columns>
                    <asp:BoundField DataField="Issue_Date" HeaderText="Issue Date" 
                        DataFormatString="{0:dd/MM/yyyy}" >
                        <HeaderStyle Width="40%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NoofLeafs" HeaderText="No of Leafs" >
                        <HeaderStyle Width="20%" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FromNo" HeaderText="From Number" >
                        <HeaderStyle Width="20%" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ToNo" HeaderText="To Number" >
                        <HeaderStyle Width="20%" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>                
            </asp:GridView>
            </td>
            <td width = 2%"></td>
            </tr>
            </table>
        </div>
        <br />
        <div class="gradientBoxesWithOuterShadows" style="width: 98%; margin-left:1%; margin-right:1%;margin-top:1%;">                      
            <legend>CHEQUE PRESENTED DETAILS</legend>    
            <br />
            <table width  = "100%">
            <tr>
            <td width = 2%"></td>
            <td width = 96%">                        
            <asp:GridView ID="DgvTranDtls" runat="server" AutoGenerateColumns="False" CssClass="grid-view"
                TabIndex="32" Width="100%">
                <Columns>
                    <asp:BoundField DataField="Tr_Date" HeaderText="Transaction Date" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle Width="17%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Cheque_Date" HeaderText="Cheque Date" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle Width="17%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Cheque_No" HeaderText="Cheque Number" >
                        <HeaderStyle Width="16%" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Ch_Amount" HeaderText="Cheque Amount" >
                        <HeaderStyle Width="20%" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Particulars" HeaderText="Particulars" >
                        <HeaderStyle Width="30%" />
                    </asp:BoundField>
                </Columns>                
            </asp:GridView>      
            </td>
            <td width = 2%"></td>
            </tr>
            <tr>
            <td width = 2%">&nbsp;</td>
            <td width = 96%">                        
                <table width="100%">
                <tr>
                    <td width = "90%"></td>
                    <td width = "10%" align="right" >
                        &nbsp;</td>
                </tr>
                </table>
            </td>
            <td width = 2%">&nbsp;</td>
            </tr>
            </table>
            </br>
    </div>    
    <br />
    <asp:HiddenField ID="hdnAccid" runat="server" />
    </form>
</body>
</html>
