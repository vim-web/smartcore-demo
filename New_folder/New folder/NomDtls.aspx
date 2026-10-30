<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NomDtls.aspx.vb" Inherits="SMARTCORE_UI.NomDtls" %>

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
            <asp:GridView ID="DgvNominee" runat="server" AutoGenerateColumns="False" 
                TabIndex="32" Width="100%">
                <Columns>
                    <asp:BoundField DataField="Nm_Name" HeaderText="Name">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="30%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Nm_Relation" HeaderText="Relation" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="20%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" ForeColor="#333399" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Nm_Address" HeaderText="Address">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="50%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" ForeColor="#333399" />
                    </asp:BoundField>
                </Columns>
                <AlternatingRowStyle BackColor="#F5F7FE" />
            </asp:GridView>
    
    </div>
            <asp:HiddenField ID="hdnAccid" runat="server" />
    </form>
</body>
</html>
