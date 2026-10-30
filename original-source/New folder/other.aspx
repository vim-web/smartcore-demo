<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="other.aspx.vb" Inherits="SMARTCORE_UI.other" 
    title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True">
    <Scripts>
            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
    </Scripts>
</asp:ToolkitScriptManager>
    
                
                  
                  
         <div class="gradientBoxesWithOuterShadows" style="width: 100%; ">
                <div id="DivPageName" runat="server">
                    <table class="style1">
                        <tr>
                            <td><legend>Other Transaction</legend>
                                &nbsp;</td>
                        </tr>
                    </table>
                
                </div>  

                    <div id="DivtranTable" runat="server">
                    <table width="78%" border="0" align="center">
                    <tr>
                    <td width="2%"></td>
                    <td width="11%"><label>Company</label></td>
                    <td width="13%">
                        <asp:DropDownList ID="CmbCompany" runat="server" Width="100%" 
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td width="2%"></td>
                    <td width="11%"><label>Service Type</label> </td>
                    <td width="13%"> 
                        <asp:DropDownList ID="CmbService" runat="server" Width="100%">
                        </asp:DropDownList>
                    </td>
                    <td width="2%"></td>
                    <td width="11%"></td>
                    <td width="13%"></td>
                    </tr>
                    </table>
                    <table width="78%" border="0" align="center">
                    <tr>
                    <td width="2%"></td>
                    <td width="11%"><label>Tranaction Type</label></td>
                    <td width="13%">
                        <asp:DropDownList ID="CmbTranTp" runat="server" Width="100%">                        
                        </asp:DropDownList>
                    </td>
                    <td width="2%"></td>
                    <td width="11%"><label>Cash</label></td>
                    <td width="13%">
                        <asp:TextBox ID="TxtCash" runat="server" Width="100%" DataFormatString="{0:F2}"></asp:TextBox></td>
                    <td width="2%"></td>
                    <td width="11%"><label>Transfer</label></td>
                    <td width="13%">
                        <asp:TextBox ID="TxtTrans" runat="server" Width="100%" DataFormatString="{0:F2}"></asp:TextBox></td>
   
                    </tr>
                    
                        <tr>
                            <td width="2%">
                                &nbsp;</td>
                            <td width="11%"><label>Remarks</label>
                                &nbsp;</td>
                            <td colspan="7">
                                <asp:TextBox ID="TxtNarration" runat="server" Width="98%"></asp:TextBox>
                                &nbsp;</td>
                                <td width="5%">
                                    <asp:Button ID="Button1" runat="server" ForeColor="#6699FF" Text="ADD" />
                            </td>
                        </tr>
                    
                    </table>
                    </div>
                    
                                
                <div id="DivTranGrid" runat="server">
                <table width="100%" border="0">
                    <tr>
                        <td ></td>
                        <td >
                            <asp:GridView ID="DgvTransaction" runat="server" AutoGenerateColumns="False"  CssClass="grid-viewNew"
                                TabIndex="999" Width="100%" >
                                <Columns>
                                    
                                        
                                  
                                    
                                    <asp:BoundField DataField="Particular" HeaderText="Particulars">
                                       
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Rct_Cash" HeaderText="Receipt Cash" 
                                        DataFormatString="{0:F2}">
                                       
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Rct_Tran" HeaderText="Receipt Transfer" 
                                        DataFormatString="{0:F2}">
                                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Pay_Cash" HeaderText="Payment Cash" 
                                        DataFormatString="{0:F2}">
                                       
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Pay_Tran" HeaderText="Payment Transfer" 
                                        DataFormatString="{0:F2}">                                        
                                    </asp:BoundField>
                                    
                                </Columns>
                                <AlternatingRowStyle BackColor="#F5F7FE" />
                            </asp:GridView>
                        </td>
                        <td ></td>
                    </tr>
                    <tr>
                        <td width="2%"></td>
                        <td width="96%">
                 
                        </td>
                        <td width="2%"></td>
                    </tr>
                </table>
                </div>
                

   






 <br />
 </div>
</ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>
