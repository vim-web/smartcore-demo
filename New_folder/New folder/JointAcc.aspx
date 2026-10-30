<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="JointAcc.aspx.vb" Inherits="SMARTCORE_UI.JointAcc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />      
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    
    
    <div class="gradientBoxesWithOuterShadows" style="width: 98%; margin-left:1%; margin-right:1%;margin-top:1%;">                      
    <br />    
            <asp:GridView ID="DgvJointAc" runat="server" AutoGenerateColumns="False" 
                TabIndex="32" Width="100%">
                <Columns>
                <asp:BoundField DataField="OprType" HeaderText="Operator Type">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Cust_Id" HeaderText="Customer ID">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Cust_Name" HeaderText="Name" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="25%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Cust_Address" HeaderText="Address">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="45%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" ForeColor="#333399" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Cust_Mobile" HeaderText="Mobile No">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" HorizontalAlign="Right" ForeColor="#333399" />
                    </asp:BoundField>
                </Columns>
                <AlternatingRowStyle BackColor="#F5F7FE" />
            </asp:GridView>
        <br />
    </div>
    
            <asp:HiddenField ID="hdnAccid" runat="server" />
    </form>
</body>
</html>
