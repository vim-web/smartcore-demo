<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CustImage.aspx.vb" Inherits="SMARTCORE_UI.CustImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />      
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script language="javascript" type="text/javascript" >
        $().ready(function () {
            $('#imgCust').mouseover(function () { $(this).width(400); $(this).height(375) });
            $('#imgCust').mouseleave(function () { $(this).width(250); $(this).height(240) });
        })
    </script>
</head>



<body>
    <form id="form1" runat="server">
    <div class="gradientBoxesWithOuterShadows" style="width: 98%; margin-left:1%; margin-right:1%;margin-top:1%;">                      
     <asp:GridView ID="DgvImage" runat="server" AutoGenerateColumns="False" 
         BackColor="White" BorderColor="Tan" BorderWidth="1px" 
         CellPadding="2" ForeColor="Black" GridLines="None" Height="322px" 
         Width="100%" runat="server">
         <Columns>
             <asp:BoundField DataField="Cust_Id" HeaderText="Customer Id" />
             <asp:BoundField DataField="Img_Name" HeaderText="ImageType" />
             <asp:BoundField DataField="Img_ID" HeaderText="Img_ID" Visible="False" />
             <asp:TemplateField HeaderText="Image">
                 <ItemTemplate>
                     <img id="imgCust" height="240" 
                         src='ShowImage.ashx?Cust_Id=<%# Eval("Cust_Id").ToString() %>&amp;Img_Id=<%# Eval("Img_Id").ToString() %>' 
                         width="250" onmouseover="XandY()" />
                 </ItemTemplate>
             </asp:TemplateField>
         </Columns>
         <FooterStyle BackColor="Tan" />
         <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
             HorizontalAlign="Center" />
         <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
         <HeaderStyle BackColor="#3333CC" Font-Bold="True" ForeColor="GhostWhite" />
         <AlternatingRowStyle BackColor="#C2C8FC" />
     </asp:GridView>
 
 
    <br />    
        <br />
    </div>
    
            <asp:HiddenField ID="hdnAccid" runat="server" />
    </form>
</body>
</html>
