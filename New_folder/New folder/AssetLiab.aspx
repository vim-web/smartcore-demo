<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AssetLiab.aspx.vb" Inherits="SMARTCORE_UI.AssetLiab" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />      
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="gradientBoxesWithOuterShadows" style="width: 98%; margin-left:1%; margin-right:1%;margin-top:1%;">                      
            <legend>OWN LIABILITY</legend>    
            <br />
            <table width  = "100%">
            <tr>
            <td width = "2%"></td>
            <td width = "96%">
            <asp:GridView ID="DgvOwnLiab" runat="server" AutoGenerateColumns="False"  CssClass="grid-viewNew"
                TabIndex="32" Width="100%">
                <Columns>
                    <asp:BoundField DataField="Al_Type" HeaderText="Al_Type" Visible="False">
                       
                    </asp:BoundField>
                    <asp:BoundField DataField="DirIndir" HeaderText="DirIndir" Visible="False" >
                       
                    </asp:BoundField>
                    <asp:BoundField DataField="Br_Name" HeaderText="BRANCH">
                      
                    </asp:BoundField>
                    <asp:BoundField DataField="Acc_ID" HeaderText="Acc_ID" Visible="False">
                       
                    </asp:BoundField>
                    <asp:BoundField DataField="Acc_Dtls" HeaderText="ACCOUNT NO">
                        
                    </asp:BoundField>
                    <asp:BoundField DataField="Op_Date" HeaderText="OPEN DATE" DataFormatString="{0:dd/MM/yyyy}">
                        
                    </asp:BoundField>
                    <asp:BoundField DataField="Due_Date" HeaderText="DUE DATE" DataFormatString="{0:dd/MM/yyyy}">
                        
                    </asp:BoundField>
                    <asp:BoundField DataField="Bal_Amt" HeaderText="BALANCE">
                        
                       <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="OD_Amt" HeaderText="OVERDUE">
                        
                        <ItemStyle HorizontalAlign="Right" />

                    </asp:BoundField>
                    <asp:BoundField DataField="Int_Amt" HeaderText="INTEREST">
                       
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Section_Name" HeaderText="Section_Name" 
                        Visible="False" />
                    <asp:BoundField DataField="Module_Catg" HeaderText="Module_Catg" 
                        Visible="False" />
                </Columns>
                <AlternatingRowStyle BackColor="#F5F7FE" />
            </asp:GridView>
            </td>
            <td width = 2%"></td>
            </tr>
            <tr>
            <td width = 2%">&nbsp;</td>
            <td width = 96%">
                <table width="100%">
                <tr>
                    <td width = "70%"></td>
                    <td width = "10%" align="right" >
                        <asp:Label ID="LblOwnLiab" runat="server" ForeColor="#000066" Text="Label"></asp:Label>
                    </td>
                    <td width = "10%" align="right" >
                        <asp:Label ID="LblOwnOD" runat="server" ForeColor="#000066" Text="Label"></asp:Label>
                    </td>
                    <td width = "10%"></td>
                </tr>
            </table>
            </td>
            <td width = 2%">&nbsp;</td>
            </tr>
            </table>
        </div>
        <br />
        <div class="gradientBoxesWithOuterShadows" style="width: 98%; margin-left:1%; margin-right:1%;margin-top:1%;">                      
            <legend>SURETY LIABILITY</legend>    
            <br />
            <table width  = "100%">
            <tr>
            <td width = 2%"></td>
            <td width = 96%">            
            <asp:GridView ID="DgvSurLiab" runat="server" AutoGenerateColumns="False" 
                TabIndex="32" Width="100%">
                <Columns>
                    <asp:BoundField DataField="Al_Type" HeaderText="Al_Type" Visible="False">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DirIndir" HeaderText="DirIndir" Visible="False" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="30%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Br_Name" HeaderText="BRANCH">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="15%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" ForeColor="#333399" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Acc_ID" HeaderText="Acc_ID" Visible="False">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="35%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" HorizontalAlign="Right" ForeColor="#333399" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Acc_Dtls" HeaderText="ACCOUNT NO">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="35%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Op_Date" HeaderText="OPEN DATE" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Due_Date" HeaderText="DUE DATE" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Bal_Amt" HeaderText="BALANCE">
                         <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                       <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="OD_Amt" HeaderText="OVERDUE">
                         <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Right" />

                    </asp:BoundField>
                    <asp:BoundField DataField="Int_Amt" HeaderText="INTEREST">
                         <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Section_Name" HeaderText="Section_Name" 
                        Visible="False" />
                    <asp:BoundField DataField="Module_Catg" HeaderText="Module_Catg" 
                        Visible="False" />
                </Columns>
                <AlternatingRowStyle BackColor="#F5F7FE" />
            </asp:GridView>
            </td>
            <td width = 2%"></td>
            </tr>
            <tr>
            <td width = 2%">&nbsp;</td>
            <td width = 96%">            
            <table width="100%">
                <tr>
                    <td width = "70%"></td>
                    <td width = "10%" align="right" >
                        <asp:Label ID="LblSurLiab" runat="server" ForeColor="#000066" Text="Label"></asp:Label>
                    </td>
                    <td width = "10%" align="right" >
                        <asp:Label ID="LblSurOD" runat="server" ForeColor="#000066" Text="Label"></asp:Label>
                    </td>
                    <td width = "10%"></td>
                </tr>
            </table>
            </td>
            <td width = 2%">&nbsp;</td>
            </tr>
            </table>
        </div>
        <br />
        <div class="gradientBoxesWithOuterShadows" style="width: 98%; margin-left:1%; margin-right:1%;margin-top:1%;">                      
            <legend>ASSETS</legend>    
            <br />
            <table width  = "100%">
            <tr>
            <td width = "2%"></td>
            <td width = "96%">                        
            <asp:GridView ID="DgvAssets" runat="server" AutoGenerateColumns="False" 
                TabIndex="32" Width="100%">
                <Columns>
                    <asp:BoundField DataField="Al_Type" HeaderText="Al_Type" Visible="False">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DirIndir" HeaderText="DirIndir" Visible="False" >
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="30%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Br_Name" HeaderText="BRANCH">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="15%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" ForeColor="#333399" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Acc_ID" HeaderText="Acc_ID" Visible="False">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="35%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" HorizontalAlign="Right" ForeColor="#333399" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Acc_Dtls" HeaderText="ACCOUNT NO">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="65%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Op_Date" HeaderText="OPEN DATE" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Due_Date" HeaderText="DUE DATE" Visible="False" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Bal_Amt" HeaderText="BALANCE">
                         <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                       <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="OD_Amt" HeaderText="OVERDUE" Visible="False">
                         <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Right" />

                    </asp:BoundField>
                    <asp:BoundField DataField="Int_Amt" HeaderText="INTEREST" Visible="False">
                         <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Section_Name" HeaderText="Section_Name" 
                        Visible="False" />
                    <asp:BoundField DataField="Module_Catg" HeaderText="Module_Catg" 
                        Visible="False" />
                </Columns>
                <AlternatingRowStyle BackColor="#F5F7FE" />
            </asp:GridView>        
            </td>
            <td width = "2%"></td>
            </tr>
            <tr>
            <td width = "2%">&nbsp;</td>
            <td width = "96%">                        
                <table width="100%">
                <tr>
                    <td width = "90%"></td>
                    <td width = "10%" align="right" >
                        <asp:Label ID="LblAssBal" runat="server" ForeColor="#000066" Text="Label"></asp:Label>
                    </td>
                </tr>
                </table>
            </td>
            <td width = "2%">&nbsp;</td>
            </tr>
            </table>
            </br>
    </div>    
        <div class="gradientBoxesWithOuterShadows" style="width: 98%; margin-left:1%; margin-right:1%;margin-top:1%;">                      
            <legend>REBATE</legend>  
            <br />
            <table width  = "100%">
            <tr>
            <td width = "2%"></td>
            <td width = "96%">                        
            <asp:GridView ID="DgvRebate" runat="server" AutoGenerateColumns="False" 
                TabIndex="32" Width="100%">
                <Columns>
                    <asp:BoundField DataField="AccountNo" HeaderText="ACCOUNT NO">
                          <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="40%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RebateDate" HeaderText="REBATE DATE" >
                         <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="20%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RebateAmt" HeaderText="REBATE AMOUNT">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="50%" />
                    </asp:BoundField>
                </Columns>
                <AlternatingRowStyle BackColor="#F5F7FE" />
            </asp:GridView>        
            </td>
            <td width = "2%"></td>
            </tr>
            <tr>
            <td width = "2%">&nbsp;</td>
            <td width = "96%">                        
                <table width="100%">
                <tr>
                    <td width = "90%"></td>
                    <td width = "10%" align="right" >
                        &nbsp;</td>
                </tr>
                </table>
            </td>
            <td width = "2%">&nbsp;</td>
            </tr>
            </table>
            </br>
    </div>    
    <br />
    <asp:HiddenField ID="hdnAccid" runat="server" />
    <asp:HiddenField ID="hdnFilterOpt" runat="server" />
    </form>
</body>
</html>
