<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GoldItems.aspx.vb" Inherits="SMARTCORE_UI.GoldItems" %>

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
        <table width ="100%">
            <tr>
            <td width = "2%"></td>
            <td width = "96%">
            <asp:GridView ID="DgvJointAc" runat="server" AutoGenerateColumns="False" 
                TabIndex="32" Width="100%">
                <Columns>
                    <asp:BoundField DataField="Orn_Remarks" HeaderText="Ornament">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="70%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Orn_Count" HeaderText="Count" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="30%" 
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
