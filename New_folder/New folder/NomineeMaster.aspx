<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NomineeMaster.aspx.vb" Inherits="SMARTCORE_UI.NomineeMaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"  EnablePageMethods="true">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
    <ContentTemplate>
    
    <table width="100%">
      <tr>
        <td width="2%">&nbsp;</td>
        <td width="22%">&nbsp;</td>
        <td width="25%">&nbsp;</td>
        <td width="2%">&nbsp;</td>
        <td width="22%">&nbsp;</td>
        <td width="25%">&nbsp;</td>
        <td width="2%">&nbsp;</td>
      </tr>
          <tr>
           
           <td></td>
                <td >
                    <asp:Label ID="LblBranch" runat="server" Text="Branch"  Width="100%" Font-Size="Medium" ForeColor="#6666FF"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList ID="DrpBranch" runat="server"   Width="100%">
                    </asp:DropDownList>
                </td>
                <td></td>
                <td >
                    <asp:Label ID="LblScheme" runat="server" Text="Scheme" Width="100%" Font-Size="Medium" ForeColor="#6666FF"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList ID="DrpScheme" runat="server" Width="100%" 
                        TabIndex="1">
                    </asp:DropDownList>
                </td>
                <td></td>
            </tr>
           
            <tr>
            <td></td>
                <td >
                    <asp:Label ID="LblAccountNo" runat="server" Text="Account No"  Width="100%" Font-Size="Medium" ForeColor="#6666FF"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="TxtAccountNo" runat="server"   Width="100%" 
                        BorderStyle="Solid" TabIndex="2"></asp:TextBox>
                </td>
                <td></td>
                <td  >
                    <asp:Label ID="LblRelation" runat="server" Text="Relation"  Width="100%" Font-Size="Medium" ForeColor="#6666FF"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList ID="Ddl_Relation" runat="server" Width="100%"
                        TabIndex="6">
                    </asp:DropDownList>
                </td>
                  <td></td>
            </tr>
            <tr>
            <td></td>
                <td >
                    <asp:Label ID="LblCustomerID" runat="server" Text="Nominee ID"  Width="100%" 
                        Font-Size="Medium" ForeColor="#6666FF"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="TxtNomineeID" runat="server"   Width="100%" 
                        BorderStyle="Solid" TabIndex="3"></asp:TextBox>
                </td>
                <td></td>
                <td  >
                    <asp:Label ID="LblPhone" runat="server" Text="Phone"  Width="100%" Font-Size="Medium" ForeColor="#6666FF"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="TxtPhone" runat="server"   Width="100%" BorderStyle="Solid" 
                        TabIndex="7"></asp:TextBox>
                </td>
                  <td></td>
            </tr>
            <tr>
                <td></td>
                <td >
                    <asp:Label ID="LblName" runat="server" Text="Name"  Width="100%" Font-Size="Medium" ForeColor="#6666FF"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="TxtName" runat="server"   Width="100%" BorderStyle="Solid" 
                        TabIndex="4"></asp:TextBox>
                </td>
                <td></td>
                <td  >
                    <asp:Label ID="LblOccupation" runat="server" Text="Occupation"  Width="100%" Font-Size="Medium" ForeColor="#6666FF"></asp:Label>
                </td>
                <td  >
                    <asp:DropDownList ID="Ddl_Occupation" runat="server"
                        Width="100%" Enabled="False" TabIndex="8">
                    </asp:DropDownList>
                </td>
                  <td></td>
            </tr>
            <tr>
            <td class="style4"></td>
                <td rowspan="2">
                    <asp:Label ID="LblAddress" runat="server" Text="Address"  Width="100%" Font-Size="Medium" ForeColor="#6666FF"></asp:Label>
                </td>
                <td  rowspan="2">
                    <asp:TextBox ID="TxtAddress" runat="server" Height="45px" TextMode="MultiLine" 
                        Width="100%" BorderStyle="Solid" TabIndex="5"></asp:TextBox>
                </td>
                <td class="style4"></td>
                <td class="style4"  >
                    <asp:Label ID="LblMinor" runat="server" Text="Minor"  Width="100%" Font-Size="Medium" ForeColor="#6666FF"></asp:Label>
                </td>
                <td class="style4" >
                    <asp:DropDownList ID="Ddl_Minor" runat="server"  Width="100%" 
                        TabIndex="9" AutoPostBack="True">
                        <asp:ListItem Value="N">NO</asp:ListItem>
                        <asp:ListItem Value="Y">YES</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style4"></td>
            </tr>
            <tr>
            <td></td><td></td>
                <td >
                    <asp:Label ID="LblDateOfBirth" runat="server" Text="Date Of Birth"  Width="100%" Font-Size="Medium" ForeColor="#6666FF"></asp:Label>
                </td>
                <td class="style3">
                    <asp:TextBox ID="TxtDateOfBirth" runat="server"   Width="100%" 
                        BorderStyle="Solid" TabIndex="12"></asp:TextBox>
            
                    
                </td>
                <td>  <asp:Image ID="Image1" runat="server" BorderStyle="Outset" Height="24px" 
                        ImageUrl="~/Images/Cal.jpg" meta:resourcekey="ImgDoaResource1" Width="27px" /> </td>
            </tr>
            <tr>
             <td></td>
                <td >
                     <asp:Label ID="LblGuardian" runat="server" Text="Guardian"  Width="100%" Font-Size="Medium" ForeColor="#6666FF"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="TxtGuardian" runat="server"   Width="100%" BorderStyle="Solid" 
                        TabIndex="10" Enabled="False">Nil</asp:TextBox>
                </td>
                <td></td>
                <td  >
                    <asp:Label ID="LblAge" runat="server" Text="Age"  Width="100%" Font-Size="Medium" ForeColor="#6666FF"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="TxtAge" runat="server"   Width="100%" BorderStyle="Solid" 
                        TabIndex="13"></asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
            <td></td>
                <td  >
                    <asp:Label ID="LblGaurdianAddress" runat="server" Text="Guardian Address"  Width="100%" Font-Size="Medium" ForeColor="#6666FF"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TxtGaurdianAddress" runat="server" Height="45px" 
                        TextMode="MultiLine"  Width="100%" BorderStyle="Solid" TabIndex="11" 
                        Enabled="False">Nil</asp:TextBox>
                </td>
                <td></td>
                <td  >
                    <asp:Label ID="LblRelation1" runat="server" Text="Relation"  Width="100%" Font-Size="Medium" ForeColor="#6666FF"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList ID="Ddl_GuardianRelation" runat="server" 
                        TabIndex="14" Width="100%" Enabled="False">
                    </asp:DropDownList>
                </td>
            </tr>
          <tr>
          <td></td>
              <tr>
                  <td>
                  </td>
                  <td>
                      &nbsp;</td>
                  <td>
                  </td>
                  <td>
                  </td>
                  <td class="style2"></td>
                 <td class="style3"> 
                 <asp:ImageButton ID="ImageButton1" runat="server" ImageAlign="Right" 
                     ImageUrl="~/images/Save.png" Width="89px" TabIndex="15" />
                 </td>
             <td> 
                 <asp:ImageButton ID="ImageButton2" runat="server" 
                     ImageUrl="~/images/Close.png" TabIndex="16" />
                 </td>
              </tr>
        </tr>
                
        </table>
        </ContentTemplate> 
                </asp:UpdatePanel> 

    </form>
</body>
</html>
