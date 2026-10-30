<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="AgentCommission.aspx.vb" Inherits="SMARTCORE_UI.AgentCommission" title="Agent Commission" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
    <script type='text/javascript' src="Scripts/Common.js"></script>
    <style type="text/css">
    label {
	    color: #03F;
        font-size:20px;  
        font-weight:400;
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

<div class="gradientBoxesWithOuterShadows" style="height: 500px;width: 80%; margin-left:10%; margin-right:10%;">                
<legend> Agent Commission Posting</legend>
<table width ="100%">
<tr>
	<td width ="30%">	</td>
	<td width ="20%">	<label>Scheme</label></td>
	<td width ="20%">		
            <asp:DropDownList ID="CmbScheme" runat="server" TabIndex="23" Width="100%">
            </asp:DropDownList>
    </td>
	<td width ="30%">	</td>
</tr>
<tr>
    <td>	&nbsp;</td>
	<td><label>Agent</label></td>	
	<td>	
            <asp:DropDownList ID="CmbAgent" runat="server" TabIndex="23" Width="100%">
            </asp:DropDownList>
        </td>
	<td>	&nbsp;</td>
</tr>
<tr>
	<td>	&nbsp;</td>
	<td><label>From Date</label></td>
	<td>	
  <asp:TextBox ID="Txt_FromDate" runat="server" CssClass="twitterStyleTextbox"  Width="96%" TabIndex="3"></asp:TextBox>
   <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
              TargetControlID="Txt_FromDate">
          </asp:CalendarExtender>
                            </td>
	<td>	&nbsp;</td>
</tr>
<tr>
	<td>	&nbsp;</td>
	<td><label>To Date</label></td>
	<td>		
  <asp:TextBox ID="Txt_ToDate" runat="server" CssClass="twitterStyleTextbox"  Width="96%" TabIndex="3"></asp:TextBox>
   <asp:CalendarExtender ID="Txt_ToDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
              TargetControlID="Txt_ToDate">
          </asp:CalendarExtender>
                            </td>
	<td>	&nbsp;</td>
</tr>
<tr>
	<td>	&nbsp;</td>
	<td><label>Commission %</label></td>
	<td>	
            <asp:TextBox ID="Txt_CommissionPer" 
                runat="server" AutoPostBack="True" 
                onkeypress="return isFloatKey(event);" style="text-align:right"  
                 CssClass="twitterStyleTextbox"  Width="96%"></asp:TextBox>
        </td>
	<td>	&nbsp;</td>
</tr>
<tr>
	<td>	&nbsp;</td>
	<td colspan="2" align="center"> 
        <asp:Button ID="CmdShowDtls" runat="server" Text="Show Details" Width="80%" 
            BackColor="#00246F" BorderColor="#99CCFF" Font-Bold="True" Font-Italic="True" 
            ForeColor="White" />
                            </td>
	<td>	&nbsp;</td>
</tr>
<tr>
	<td>	&nbsp;</td>
	<td><label>Collection Amt</label></td>
	<td>	
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_Collection" 
                runat="server" AutoPostBack="True" 
                onkeypress="return isFloatKey(event);" style="text-align:right"  
                Width="96%"></asp:TextBox>
        </td>
	<td>	&nbsp;</td>
</tr>

<tr>
	<td>	&nbsp;</td>
	<td><label>Commission Amt</label></td>
	<td>	
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_CommissionAmt" 
                runat="server" AutoPostBack="True" 
                onkeypress="return isFloatKey(event);" style="text-align:right"  
                Width="96%"></asp:TextBox>
        </td>
	<td>	&nbsp;</td>
</tr>
</table>


<table width="100%" border="0">
    <tr>
        <td width="2%"></td>
        <td width="96%">
            <asp:GridView ID="DgvTransaction" runat="server" AutoGenerateColumns="False" 
                TabIndex="32" Width="100%">
                <Columns>
                    <asp:BoundField DataField="AccNo" HeaderText="AccNo" Visible="False">
                    </asp:BoundField>
                    <asp:BoundField DataField="AccCode" HeaderText="AccCode" Visible="False">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AccHead" HeaderText="Particulars">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="30%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" ForeColor="#333399" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RctCash" HeaderText="Receipt Cash" 
                        DataFormatString="{0:F2}">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="15%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" HorizontalAlign="Right" ForeColor="#333399" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RctTransfer" HeaderText="Receipt Transfer" 
                        DataFormatString="{0:F2}">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" HorizontalAlign="Right" ForeColor="#333399" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PayCash" HeaderText="Payment Cash" 
                        DataFormatString="{0:F2}">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" HorizontalAlign="Right" ForeColor="#333399" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PayTransfer" HeaderText="Payment Transfer" 
                        DataFormatString="{0:F2}">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" HorizontalAlign="Right" ForeColor="#333399" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Seq_No" HeaderText="Seq_No" Visible="False" />
                </Columns>
                <AlternatingRowStyle BackColor="#F5F7FE" />
            </asp:GridView>
        </td>
        <td width="2%"></td>
    </tr>
    <tr>
        <td width="2%">
            &nbsp;</td>
        <td width="96%">
                            <table width="100%" border="0">
                  <tr>
                  
                    <td width="40%" scope="col"  align="right">
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#0033CC" 
                            Text="TOTAL"></asp:Label>

                    </td>
                    <td width="20%" scope="col"  align="right">
                        <asp:Label ID="LblRctCashTotal" runat="server" Font-Bold="True" 
                            ForeColor="#0033CC"></asp:Label>
                      </td>
                    <td width="14%" scope="col"  align="right">
                        <asp:Label ID="LblRctTransferTotal" runat="server" Font-Bold="True" 
                            ForeColor="#0033CC"></asp:Label>
                      </td>
                    <td width="14%" scope="col"  align="right">
                        <asp:Label ID="LblPayCashTotal" runat="server" Font-Bold="True" 
                            ForeColor="#0033CC"></asp:Label>
                      </td>
                    <td width="12%" scope="col"  align="right">
                        <asp:Label ID="LblPayTransferTotal" runat="server" Font-Bold="True" 
                            ForeColor="#0033CC"></asp:Label>
                      </td>
                  </tr>
                </table>

            
            </td>
        <td width="2%">
            &nbsp;</td>
    </tr>    
</table>

    <table width="100%">
        <tr>
            <td width="30%">
            </td>
            <td width="40%">
                <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                    meta:resourcekey="CmdSaveResource1" TabIndex="21"  
                    OnClientClick="return ValidateSave();"/>
                <asp:ImageButton ID="CmdClear0" runat="server" ImageUrl="~/Images/Clear.png" 
                    meta:resourcekey="CmdSaveResource1" TabIndex="22" />
                <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/images/Close.png" 
                    meta:resourcekey="CmdClearResource1" TabIndex="23" />
            </td>
            <td width="30%">
                &nbsp;</td>
        </tr>
    </table>
</div>


</ContentTemplate>
</asp:UpdatePanel> 
    
</asp:Content>
