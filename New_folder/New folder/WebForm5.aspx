<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm5.aspx.vb" Inherits="SMARTCORE_UI.WebForm5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>

<body>
<table width="100%"   border="0" >
                <tr>
                  <td  align="left" class="style1">
                        <label>Name</label>
                  </td>
                  <td>
                        <table width="100%">
                            <tr>
                                <td class="style2"> 
                                    <asp:TextBox ID="TxtName" runat="server" 
                                        meta:resourcekey="TxtLandPhoneResource1" Width="100%" ForeColor="Red" 
                                        TabIndex="6" Font-Bold="True"  CssClass="twitterStyleTextbox" 
                                        BackColor="#99CCFF" ReadOnly="True"></asp:TextBox> 
                                </td>
                                <td width = "2%"> </td>
                                <td width = "28%">
                                        &nbsp;</td>
                            </tr>
                        </table>              
                  </td>
                </tr>
                <tr>
                  <td align=left class="style1">
                  <label>Address</label></td>
                  <td width="79%" rowspan="2">
                      <asp:TextBox ID="TxtAddress" runat="server" Width="100%" Height="60Px"
                        meta:resourcekey="TxtLandPhoneResource1" TextMode="MultiLine" TabIndex="8"  
                          CssClass="twitterStyleTextbox" ReadOnly="True" BackColor="#A8D3FF"></asp:TextBox></td>
                  
                </tr>
                <tr>
                  <td class="style1">
                      <asp:Label ID="Lbl_Status" runat="server" Font-Bold="True" ForeColor="#FF3300" 
                          Text="Cheque A/c" Visible=false></asp:Label>
                    </td>
                </tr>
                <tr>
                
                  <td align=left class="style1">
                      <label>Mobile No</label></td>
                  <td>
                    <table width=100%>
                        <tr>
                            <td Width="40%">
                                <asp:TextBox ID="TxtMblNo" runat="server" Width="100%" 
                                    meta:resourcekey="TxtLandPhoneResource1"  CssClass="twitterStyleTextbox" 
                                      ReadOnly="True"></asp:TextBox></td>
                            </td>
                            <td Width="2%"></td>
                            <td Width="15%">
                                <label>MOP</label>
                            </td>
                            <td Width="48%">
                                <asp:DropDownList ID="CmbModeofOperation" runat="server" Width="100%"></asp:DropDownList>                     
                            </td>
                        </tr>
            </table>
</form>
</body>
</html>
