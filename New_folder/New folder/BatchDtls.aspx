<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BatchDtls.aspx.vb" Inherits="SMARTCORE_UI.BatchDtls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>BATCH DETAILS</title>
</head>
<body>
    <form id="form1" runat="server">
        m<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /><div class="gradientBoxesWithOuterShadows" style="width: 98%; margin-left:1%; margin-right:1%;margin-top:3%;margin-bottom:3%;height:94%;" >                    
    
        <asp:GridView ID="DgvBatch" runat="server" CssClass=grid-view 
            AutoGenerateColumns="False" Width="100%">
            <Columns>
                <asp:BoundField DataField="DrDtls" HeaderText="Debit Details">
                    <HeaderStyle Width="35%" />
                </asp:BoundField>
                <asp:BoundField DataField="DrAmt" DataFormatString="{0:F2}" 
                    HeaderText="Debit Amount">
                    <HeaderStyle Width="15%" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="CrDtls" HeaderText="Credit Details">
                    <HeaderStyle Width="35%" />
                </asp:BoundField>
                <asp:BoundField DataField="CrAmt" DataFormatString="{0:F2}" 
                    HeaderText="Credit Amount">
                    <HeaderStyle HorizontalAlign="Right" Width="15%" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>

            </Columns>
        </asp:GridView>
    
    </div>
    <div style="text-align:center">
    <asp:Label ID="LblDiffrence" runat="server" Width="100%" Font-Bold="True" 
        Font-Size="Larger" ForeColor="#FF3300"  ></asp:Label>
        </div>
    <asp:HiddenField ID="HdnBatchNo" runat="server" />
    </form>
</body>
</html>
