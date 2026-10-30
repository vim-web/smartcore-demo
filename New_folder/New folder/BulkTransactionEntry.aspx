<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="BulkTransactionEntry.aspx.vb" Inherits="SMARTCORE_UI.BulkTransactionEntry" 
    title="Untitled Page" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> 
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
        </Scripts>
        </asp:ToolkitScriptManager>


<div class="gradientBoxesWithOuterShadows" style="width:80%; margin-left:10%; margin-right:20%;" >                
    <legend>BulkTransactionEntry</legend> 
    <table width="100%">
    <tr>
    <td width="2%"></td>
    <td width="23%" colspan="2"></td>
     <td width="23%"></td>
     <td width="2%"></td>
     <td width="23%" colspan="2"></td>
     <td width="23%"></td>
     <td width="2%"></td>
    </tr>
     <tr>
    <td width="2%"></td>
    <td width="23%" colspan="2"><label>BRANCH</label></td>
     <td width="23%">
         <asp:DropDownList ID="CmbBranch" runat="server" Width="100%">
         </asp:DropDownList>
         </td>
     <td width="2%"></td>
     <td width="23%" style="width: 0%"><label>SCHEME</label></td>
         <td style="width: 11%" width="23%">
             &nbsp;</td>
     <td width="23%">
         <asp:DropDownList ID="Cmbscheme" runat="server" Width="100%" 
             AutoPostBack="True">
         </asp:DropDownList>
         </td>
     <td width="2%"></td>
    </tr>
     <tr>
    <td width="2%"></td>
    <td width="23%" style="width: 0%"><label>ACCNO</label></td>
         <td style="width: 11%" width="23%">
             <asp:TextBox ID="Txtshortcode" runat="server" Width="100%" AutoPostBack="True" 
                 CssClass="twitterStyleTextbox"></asp:TextBox>
         </td>
     <td width="23%">
         <asp:TextBox ID="TxtAccno" runat="server" Width="100%" AutoPostBack="True"></asp:TextBox>
         </td>
     <td width="2%"></td>
     <td width="23%" colspan="2"><label>NAME</label></td>
     <td width="23%">
         <asp:TextBox ID="TxtName" runat="server" Width="100%"></asp:TextBox>
         </td>
     <td width="2%"></td>
    </tr>
     <tr>
    <td width="2%"></td>
    <td width="23%" colspan="2"><label>CASH/TRANSFER</label></td>
     <td width="23%">
         <asp:DropDownList ID="CmbCashTransfer" runat="server" Width="100%" 
             AutoPostBack="True">
         </asp:DropDownList>
         </td>
     <td width="2%"></td>
     <td width="23%" colspan="2"><label>TRANTYPE</label></td>
     <td width="23%">
         <asp:DropDownList ID="CmbTranType" runat="server" Width="100%" 
             AutoPostBack="True">
         </asp:DropDownList>
         </td>
     <td width="2%">
         <asp:Button ID="CmdOrnAdd" runat="server" BackColor="#9999FF" 
             CssClass="Add_btn" TabIndex="32" UseSubmitBehavior="False" Width="100%" />
         </td>
    </tr>
    <tr>
    <td width="2%"></td>
    <td width="23%" colspan="2"></td>
     <td width="23%"></td>
     <td width="2%"></td>
     <td width="23%" colspan="2">
         <label>
         TRANSACTION AMT</label></td>
     <td width="23%">
         <asp:TextBox ID="TxtAmt" runat="server" CssClass="twitterStyleTextbox" 
             Width="100%"></asp:TextBox>
        </td>
     <td width="2%"></td>
    </tr>
    <tr>
    <td width="2%"></td>
    <td width="23%" colspan="2"><label>REMARKS</label></td>
     <td colspan="4" style="width: 46%">
         <asp:TextBox ID="TxtRemarks" runat="server" CssClass="twitterStyleTextbox" 
             Width="150%"></asp:TextBox>
        </td>
     <td width="23%">
         &nbsp;</td>
     <td width="2%">
         &nbsp;</td>
    </tr>
    <tr>
    <td width="2%"></td>
    <td width="23%" colspan="2"></td>
     <td width="23%"></td>
     <td width="2%"></td>
     <td width="23%" colspan="2">
         &nbsp;</td>
     <td width="23%">
         &nbsp;</td>
     <td width="2%">
         &nbsp;</td>
    </tr>
    <tr>
    <td width="2%"></td>
    <td width="23%" colspan="2"></td>
     <td width="23%"></td>
     <td width="2%"></td>
     <td width="23%" colspan="2"></td>
     <td width="23%"></td>
     <td width="2%"></td>
    </tr>
    <tr>
    <td width="2%"></td>
    <td width="23%" colspan="2"></td>
     <td width="23%"></td>
     <td width="2%"></td>
     <td width="23%" colspan="2"></td>
     <td width="23%"></td>
     <td width="2%"></td>
    </tr>
     <tr>
    <td width="2%"></td>
    <td colspan="7">
        <asp:GridView ID="DgvTransaction" runat="server" AutoGenerateColumns="False" 
            TabIndex="999" Width="100%">
            <Columns> 
                
                <asp:BoundField DataField="AccNo" HeaderText="AccNo" Visible="False">
                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Medium" />
                </asp:BoundField>   
                        
                 <asp:BoundField DataField="AccId" HeaderText="AccId" Visible="False">
                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Medium" />
                </asp:BoundField>   
                
                 <asp:BoundField DataField="AccCode" HeaderText="Scheme" Visible="False">
                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Medium" />
                </asp:BoundField>
                    
                
                <asp:BoundField DataField="RctCash" DataFormatString="{0:F2}" 
                    HeaderText="Receipt Cash">
                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Medium" ForeColor="#333399" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="RctTransfer" DataFormatString="{0:F2}" 
                    HeaderText="Receipt Transfer">
                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Medium" ForeColor="#333399" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="PayCash" DataFormatString="{0:F2}" 
                    HeaderText="Payment Cash">
                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Medium" ForeColor="#333399" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="PayTransfer" DataFormatString="{0:F2}" 
                    HeaderText="Payment Transfer">
                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Medium" ForeColor="#333399" HorizontalAlign="Right" />
                </asp:BoundField>
            
                <asp:BoundField DataField="Narration" Visible="False" 
                    HeaderText="Narration">
                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Medium" ForeColor="#333399" HorizontalAlign="Right" />
                </asp:BoundField>
                
                <asp:BoundField DataField="Seq_No" HeaderText="Seq_No" Visible="False" />
                <asp:CommandField DeleteImageUrl="~/Images/DeleteN.png" 
                    DeleteText="&lt;img src='./images/del.jpg'&gt;" 
                    EditImageUrl="~/Images/DeleteN.png" NewImageUrl="~/Images/DeleteN.png" 
                    SelectImageUrl="~/Images/DeleteN.png" ShowDeleteButton="True">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="5%" />
                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Size="Medium" ForeColor="#333399" />
                </asp:CommandField>
            </Columns>
            <AlternatingRowStyle BackColor="#F5F7FE" />
        </asp:GridView>
         </td>
     <td width="2%"></td>
    </tr>
     <tr>
    <td width="2%"></td>
    <td width="23%" colspan="2" align="center">
        <asp:Label ID="LblRctCashTotal" runat="server" Font-Bold="True" 
            ForeColor="Red"></asp:Label>
         </td>
     <td width="23%" align="center">
         <asp:Label ID="LblRctTransferTotal" runat="server" Font-Bold="True" 
             ForeColor="Red"></asp:Label>
         </td>
     <td width="2%"></td>
     <td width="23%" colspan="2" align="center">
         <asp:Label ID="LblPayCashTotal" runat="server" Font-Bold="True" 
             ForeColor="Red"></asp:Label>
         </td>
     <td width="23%" align="center">
         <asp:Label ID="LblPayTransferTotal" runat="server" Font-Bold="True" 
             ForeColor="Red"></asp:Label>
         </td>
     <td width="2%"></td>
    </tr>
    <tr>
    <td width="2%"></td>
    <td width="23%" colspan="2"></td>
     <td colspan="4" style="width: 46%">
         <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
             meta:resourcekey="CmdSaveResource1" OnClientClick="return ValidateSave();" 
             TabIndex="35" />
         <asp:ImageButton ID="CmdClear0" runat="server" ImageUrl="~/Images/Clear.png" 
             meta:resourcekey="CmdSaveResource1" TabIndex="36" />
         <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/images/Close.png" 
             meta:resourcekey="CmdClearResource1" PostBackUrl="~/Main.aspx" TabIndex="37" />
        </td>
     <td width="23%"></td>
     <td width="2%"></td>
    </tr>
    <tr>
    <td width="2%"></td>
    <td width="23%" colspan="2">
        <asp:HiddenField ID="HidAccId" runat="server" />
        </td>
     <td width="23%">
         <asp:HiddenField ID="HidTrdate" runat="server" />
        </td>
     <td width="2%"></td>
     <td width="23%" colspan="2"></td>
     <td width="23%"></td>
     <td width="2%"></td>
    </tr>
    <tr>
    <td width="2%"></td>
    <td width="23%" colspan="2"></td>
     <td width="23%"></td>
     <td width="2%"></td>
     <td width="23%" colspan="2"></td>
     <td width="23%"></td>
     <td width="2%"></td>
    </tr>
    </table>
    
    
    
                  
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
