<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LoanTranDtls.aspx.vb" Inherits="SMARTCORE_UI.LoanTranDtls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />      

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
            <%--<asp:ScriptManager ID="ScriptManager1" runat="server"  EnablePageMethods="true">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
    <ContentTemplate>--%>
    <div class="gradientBoxesWithOuterShadows" style="width: 98%; margin-left:1%; margin-right:1%;margin-top:1%;">                      
    <br />    
    <table width = "100%">
    <tr>
    <td width = "2%"></td>
    <td width = "96%">
            <asp:GridView ID="DgvLoanTranDtls" runat="server" AutoGenerateColumns="False" 
                TabIndex="32" Width="100%">
                <Columns>
                    <asp:BoundField DataField="TranType" HeaderText="TranType" Visible="False">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AccHead" HeaderText="PARTICULARS" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="65%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TranAmt" HeaderText="AMOUNT">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="35%" 
                            Font-Names="Times New Roman" HorizontalAlign="Right" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" ForeColor="#333399" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SeqNo" HeaderText="SeqNo" Visible="False">
                    </asp:BoundField>
                </Columns>
                <AlternatingRowStyle BackColor="#F5F7FE" />
            </asp:GridView>
            <table width = "100%">
                <tr>
                    <td width = "65%" align="right">  <label> TOTAL</label></td>
                    <td width = "35%" align="right">
                        <asp:Label ID="LblTotal" runat="server" Font-Bold="True" Font-Size="Large" 
                            ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table> 
            <br />   
    </td>    
    <td width = "2%"></td>
    </tr>  
    <tr>
    <td width = "2%">&nbsp;</td>    
    <td width = "96%">
        <table width="100%">
            <tr>
                <td width = "49%" align="right">  
                    
                    <label> DUE CLEARANCE</label></td>
                <td width = "2%">   </td>
                <td width = "49%" align="left">  
                       <asp:Label ID="LblDueClear" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Red"></asp:Label>
                    
                </td>
            </tr>
        </table>    
    </td>    
    <td width = "2%">&nbsp;</td>
    </tr>   
    <tr>
    <td width = "2%">&nbsp;</td>    
    <td width = "96%">
        <table width="100%">
            <tr>
                <td width = "49%" align="right">  
                    
                    <label> RECEIPT AMOUNT</label></td>
                <td width = "2%">   </td>
                <td width = "49%" align="left">  
                    
                    <asp:TextBox ID="TxtTotal" runat="server" Width="60%" 
                        onkeypress="return isFloatKey(event);" CssClass="twitterStyleTextbox" 
                        style="text-align:right;color:Red;"></asp:TextBox>   
                    
                </td>
            </tr>
        </table>    
    </td>    
    <td width = "2%">&nbsp;</td>
    </tr>    
    <tr>
    <td width = "2%">&nbsp;</td>    
    <td width = "96%" align="center">
        <asp:ImageButton ID="ImgClose" runat="server" 
            ImageUrl="~/Images/CloseBtn.png" Visible="False" />
    </td>    
    <td width = "2%">&nbsp;</td>
    </tr>    
    </table>
        <asp:HiddenField ID="HdnTotal" runat="server" />
    <br />
    </div>    
    <%--</ContentTemplate>
    </asp:UpdatePanel> --%>
    </form>
</body>
</html>
