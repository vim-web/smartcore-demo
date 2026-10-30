<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="DirPaySettings.aspx.vb" Inherits="SMARTCORE_UI.DirPaySettings" 
    title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
     <script type='text/javascript' src="Scripts/Common.js"></script>  
    <style type="text/css">
        .style1
        {
            width: 152px;
        }
        .style2
        {
            width: 3%;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  
        EnablePageMethods="True" CombineScripts="True"> 
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
        </Scripts>
        </asp:ToolkitScriptManager>
    <div class="gradientBoxesWithOuterShadows" style="width: 80%; margin-left:10%; margin-right:10%;margin-top:5%; " >                
        <legend>DIRECTOR FEE PAYMENT</legend> <br />
    <table width="100%">
        <tr>
        <td width="2%"></td>
        <td width="48%"></td>
        <td width="48%"></td>
        <td width="2%"></td>
            </tr>
            <tr>
                <td width="2%" ></td>
                <td width="48%"><label>Director</label></td>
                <td width="48%" >
                    <asp:DropDownList ID="CmbDirector" runat="server" TabIndex="9" Width="100%">
                    </asp:DropDownList>
                </td>
                <td width="2%" ></td>
            </tr>
             <tr>
                <td width="2%"></td>
                <td width="48%"><label>
                    Cash/Transfer</label></td>
                <td width="48%">
                    <asp:DropDownList ID="CmbCashTransfer" runat="server" AutoPostBack="True" TabIndex="29" Width="100%">
                    </asp:DropDownList>
                 </td>
                <td width="2%"></td>
            </tr>
            <tr>
                <td width="2%" </td>
                <td width="48%" </td>
                    <label>
                    Transaction Amt</label><td width="48%" </td>
                        <asp:TextBox ID="TxtAcHeadTransAmt" runat="server" AutoPostBack="True" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" TabIndex="31" Width="100%"></asp:TextBox>
                <td width="2%" </td>
            </tr>
          <tr>
                <td width="2%" </td>
                <td width="48%" </td>
                <td width="48%" </td align="right">
                    <asp:ImageButton ID="CmdAdd" runat="server" Height="21px" ImageUrl="~/Images/AddBtn.png" TabIndex="25" />
                <td width="2%" </td>
            </tr>
          <tr>
                <td width="2%" </td >
                <td width="48%" </td >
                <td width="48%" </td >
                <td width="2%" </td >
            </tr>
          <tr>
                <td width="2%" </td>
                <td width="48%" </td colspan="2" style="width: 96%">
                    <asp:GridView ID="DgvTransaction" runat="server" AutoGenerateColumns="False" TabIndex="999" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="AccNo" HeaderText="AccNo" Visible="False">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AccCode" HeaderText="AccCode" Visible="False" />
                            <asp:BoundField DataField="AccHead" HeaderText="Particulars">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Names="Times New Roman" Font-Size="Medium" ForeColor="White" HorizontalAlign="Left" Width="30%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="#333399" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RctCash" DataFormatString="{0:F2}" HeaderText="Receipt Cash">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="#333399" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RctTransfer" DataFormatString="{0:F2}" HeaderText="Receipt Transfer">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="#333399" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PayCash" DataFormatString="{0:F2}" HeaderText="Payment Cash">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="#333399" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PayTransfer" DataFormatString="{0:F2}" HeaderText="Payment Transfer">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="#333399" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Seq_No" HeaderText="Seq_No" Visible="False" />
                            <asp:CommandField DeleteText="&lt;img src='./images/del.jpg'&gt;" 
                    ShowDeleteButton="True" DeleteImageUrl="~/Images/DeleteN.png" 
                         EditImageUrl="~/Images/DeleteN.png" 
                         NewImageUrl="~/Images/DeleteN.png" SelectImageUrl="~/Images/DeleteN.png">
                    <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="5%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="#333399"  />
                </asp:CommandField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#F5F7FE" />
                    </asp:GridView>
                <td width="2%" </td>
                </tr>
          <tr>
                <td width="2%" </td>
                <td width="48%" </td colspan="2" style="width: 96%">
                    <table border="0" width="100%">
                        <tr>
                            <td align="right" scope="col" width="40%">
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#0033CC" Text="TOTAL"></asp:Label>
                            </td>
                            <td align="right" scope="col" width="20%">
                                <asp:Label ID="LblRctCashTotal" runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>
                            </td>
                            <td align="right" scope="col" width="14%">
                                <asp:Label ID="LblRctTransferTotal" runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>
                            </td>
                            <td align="right" scope="col" width="14%">
                                <asp:Label ID="LblPayCashTotal" runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>
                            </td>
                            <td align="right" scope="col" width="12%">
                                <asp:Label ID="LblPayTransferTotal" runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>
                            </td>
                        </tr>
                    </table>
                <td width="2%" </td>
                </tr>
           <tr>
                <td width="2%" </td>
                <td width="48%" </td colspan="2" style="width: 96%">
                    <table width="100%">
                        <tr>
                            <td width="30%"></td>
                            <td width="40%">
                                <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" meta:resourcekey="CmdSaveResource1" OnClientClick="return ValidateSave();" TabIndex="35" />
                                <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" meta:resourcekey="CmdSaveResource1" TabIndex="36" />
                                <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/images/Close.png" meta:resourcekey="CmdClearResource1"  TabIndex="37" style="height: 33px" />
                            </td>
                            <td width="30%"></td>
                        </tr>
                    </table>
                <td width="2%" </td>
                </tr>
         <tr>
                <td width="2%" </td>
                <td width="48%" </td colspan="2" style="width: 96%">
                <td width="2%" </td>
                </tr>
       
          <tr>
                <td width="2%" </td>
                <td width="48%" </td colspan="2" style="width: 96%">
                <td width="2%" </td>
                </tr>
       

    </table>
    
</div>
</ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>
