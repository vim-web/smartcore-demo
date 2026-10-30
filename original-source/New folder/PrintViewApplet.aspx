<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="PrintViewApplet.aspx.vb" Inherits="SMARTCORE_UI.PrintViewApplet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Print Preview</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 30px;
        }
    </style>
    <script src="Scripts/Print.js" type="text/javascript"></script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table class="style1">
            <tr>
                <td Width="5%">
                    &nbsp;</td>
                <td Width="30%" colspan="2">
                    &nbsp;</td>
                <td colspan="2">
                    &nbsp;</td>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td Width="5%">
                    &nbsp;</td>
                <td Width="30%" colspan="2">
                    &nbsp;</td>
                <td colspan="2">
                    &nbsp;</td>
                <td colspan="2">
                    <asp:Label ID="LblDate" runat="server" Text="[LblDate]" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td Width="5%">
                    &nbsp;</td>
                <td Width="30%" colspan="2" style="text-align: right">
                    <asp:Label ID="LblSelPrinter" runat="server" Align="right" 
                        style="text-align: right">Select The Printer</asp:Label>
                </td>
                <td colspan="2">
                    
                    
                    <table width ="100%">
                    <tr>
                    <td width="60%"><asp:DropDownList ID="DrpPrinters" runat="server" Width ="100%"> </asp:DropDownList></td>
                    <td width="20%">
                    <asp:Button ID="ButPrint" runat="server" Text="Print" Align="right"  
                        OnClientClick="return print();" />
                        </td>
                    <td width="20%">
                    <asp:Button ID="ButClose" runat="server" Text="Close" 
                        onclientclick="window.close();" />
                        </td>
                    </tr>
                    </table>
                    
                </td>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td Width="5%">
                    &nbsp;</td>
                <td Width="30%" colspan="2">
                    &nbsp;</td>
                <td colspan="2">
                    &nbsp;</td>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td align=center colspan="4">
                    <asp:Label ID="LblHeader" runat="server" ></asp:Label>
                </td>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td >
                    </td>
                <td align=center colspan="4" >
                    <asp:Label ID="LblAdd" runat="server"></asp:Label>
                </td>
                <td colspan="2" >
                    <asp:Label ID="LblVrNo" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td align=center colspan="4">
                    <asp:Label ID="LblVrType" runat="server"></asp:Label>
                </td>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td  colspan="4" rowspan="2" align="justify">
                    <asp:Label ID="LblVrHeaderDet" runat="server"></asp:Label>
                </td>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td colspan="4">
                    <asp:GridView ID="GVRecDetails" runat="server" Width="100%">
                    </asp:GridView>
                </td>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td colspan="2">
                    <asp:Label ID="LblWords" runat="server"></asp:Label>
                </td>
                <td colspan="2">
                    &nbsp;</td>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td colspan="2">
                    <asp:Label ID="LblUser" runat="server"></asp:Label>
                </td>
                <td colspan="2">
                    &nbsp;</td>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td align=right>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td align=right>
                    <asp:Label ID="LblCopies" runat="server" Align="right"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DrpCopies" runat="server">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    
    </div>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <applet archive="dist/jzebra.jar" code="jzebra.PrintApplet.class" height="100" 
           name="jZebra" width="100">
           <param name="printer" value="zebra" />
<!-- <param name="sleep" value="200"> -->
       </applet>
    </form>
</body>
</html>
