<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CustImg.aspx.vb" Inherits="SMARTCORE_UI.CustImg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Customer Image</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

 <div>
 <table width="100%">
 <tr>
 <td>
 
     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
         BackColor="White" BorderColor="Tan" BorderWidth="1px" 
         CellPadding="2" ForeColor="Black" GridLines="None" Height="322px" Width="100%">
         <Columns>
             <asp:BoundField DataField="Cust_Id" HeaderText="Customer Id" />
             <asp:BoundField DataField="Img_Name" HeaderText="ImageType" />
             <asp:BoundField DataField="Img_ID" HeaderText="Img_ID" Visible="False" />
             <asp:TemplateField HeaderText="Image">
                 <ItemTemplate>
                     <img height="100" 
                         src='ShowImage.ashx?Cust_Id=<%# Eval("Cust_Id").ToString() %>&amp;Img_Id=<%# Eval("Img_Id").ToString() %>' 
                         width="150" />
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
 
 
 </td>
 
</table>
 
 </div> 
    
   
            </ContentTemplate>
        </asp:UpdatePanel> 




    </form>
</body>
</html>
