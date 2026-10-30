<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="OrnamentImg.aspx.vb" Inherits="SMARTCORE_UI.OrnamentImg" %>

<!DOCTYPE htmlPUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />      

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div  class="gradientBoxesWithOuterShadows" style="width: 98%; margin-left:1%; margin-right:1%;margin-top:1%;">
    <table >
        <tr>
            <td>
                <label >Weight</label>
            </td>
            <td>
                <label >Ornament</label>
            </td>
        </tr>
        <tr>
            
            <td>
<asp:Image ID="ImgPhoto" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                            Height="150px" ImageUrl="~/Images/no_photo.jpg" TabIndex="56" Width="130px" />
                  </td>

                  <td>
<asp:Image ID="ImgPhoto2" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                            Height="150px" ImageUrl="~/Images/no_photo.jpg" TabIndex="56" Width="130px" />
                  </td>
        </tr>
    </table>
    </div>
        
            <asp:HiddenField ID="hdnAccid" runat="server" />
    </form>
</body>
</html>
