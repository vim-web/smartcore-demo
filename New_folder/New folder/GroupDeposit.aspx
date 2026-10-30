<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="GroupDeposit.aspx.vb" Inherits="SMARTCORE_UI.GroupDeposit" 
    title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<div class="gradientBoxesWithOuterShadows" width: 100%; margin-left:25%; margin-right:25%; >                
<br />

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<table width="100%" border="0" style="font-family: Tahoma">
  <tr>
    <td width="2%"></td>
    <td width="20%"><label>Branch</label></td>
    <td width="27%">
            <asp:DropDownList ID="CmbBranch" runat="server" Width="100%">
            </asp:DropDownList>
        </td>
	<td width="2%"></td>
    <td width="20%"><label>Mode of Operation</label></td>
    <td width="27%">
            <asp:DropDownList ID="CmbOpMode" runat="server" Width="100%">
            </asp:DropDownList>
        </td>
    <td width="2%"></td>
  </tr>
  <tr>
    <td></td>
    <td><label>Scheme</label></td>
    <td>
            <asp:DropDownList ID="CmbScheme" runat="server" Width="100%">
            </asp:DropDownList>
        </td>
    <td></td>
    <td><label>No of Operators</label></td>
    <td>
            <asp:TextBox ID="TxtNoOfOperator" runat="server" Width = "100%"></asp:TextBox>
        </td>
    <td></td>
  </tr>
</table>
<table width="100%" border="0">
    <tr>
        <td width="2%"></td>
        <td width="96%">
        
            <asp:GridView ID="DgvCustomer" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="CustID" TabIndex="30" Width="100%">
                <Columns>
                    <asp:BoundField DataField="MemDtls" HeaderText="Member Details">
                        <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                            Width="15%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MemBrCode" HeaderText="Member BrCode" 
                        Visible="False" />
                    <asp:BoundField DataField="MemSchCode" HeaderText="MemSchCode" 
                        Visible="False" />
                    <asp:BoundField DataField="MemNo" HeaderText="Member No" Visible="False">
                        <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CustID" HeaderText="Customer ID">
                        <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="8%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CustName" DataFormatString="{0:F2}" 
                        HeaderText="Name">
                        <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                            Width="17%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Relation" HeaderText="Relation">
                        <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                            Width="20%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CustAdd" HeaderText="Address">
                        <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                            Width="30%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CustType" HeaderText="CustType" Visible="False" />
                    <asp:BoundField DataField="CustCatg" HeaderText="CustCatg" Visible="False" />
                    <asp:BoundField DataField="HouseNm" HeaderText="HouseNm" Visible="False" />
                    <asp:BoundField DataField="Place" HeaderText="Place" Visible="False" />
                    <asp:BoundField DataField="Dist" HeaderText="Dist" Visible="False" />
                    <asp:BoundField DataField="State" HeaderText="State" Visible="False" />
                    <asp:BoundField DataField="Country" HeaderText="Country" Visible="False" />
                    <asp:BoundField DataField="Pincode" HeaderText="PinCode" Visible="False" />
                    <asp:BoundField DataField="MblNo" HeaderText="MblNo" Visible="False" />
                    <asp:BoundField DataField="LandPh" HeaderText="LandPh" Visible="False" />
                    <asp:BoundField DataField="RelType" HeaderText="RelType" Visible="False" />
                    <asp:BoundField DataField="Dob" HeaderText="Dob">
                        <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                            Width="6%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Age" HeaderText="Age">
                        <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                            Width="4%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" />
                    </asp:BoundField>
                    <asp:CommandField DeleteText="&lt;img src='./images/del.jpg'&gt;" 
                        ShowDeleteButton="True">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:CommandField>
                </Columns>
                <AlternatingRowStyle BackColor="#F5F4FF" />
            </asp:GridView>
        
        </td>
        <td width="2%"></td>
    </tr>
</table>


<table width="100%" border="0">
  
	<tr>
		<td width = "2%"> </td>
		<td width = "15%"> </td>
		<td width = "15%"> </td>
		<td width = "2%"> </td>
		<td width = "16%"> </td>
		<td width = "16%"> </td>
		<td width = "2%"> </td>
		<td width = "15%"> </td>
		<td width = "15%"> </td>
		<td width = "2%"> </td>
	</tr>
	
    <tr>	  
    <td width="2%"></td>
    <td><label>Group Name</label></td>
    <td>
        <asp:DropDownList ID="CmbGroupName" runat="server" Width="100%">
        </asp:DropDownList>
    </td>
    
    <td></td>
    
    <td >
        <label>Opening Date</label>
    </td>
    <td >
        <asp:TextBox ID="TxtOpDate" runat="server" Width = "100%" TabIndex="1"></asp:TextBox>
    </td>
    <td ></td>
    <td >
        <label>Initial Deposit</label>
    </td>
    <td >
        <asp:TextBox ID="TxtInitialDep" runat="server" Width = "100%" 
            AutoPostBack="True" TabIndex="2"></asp:TextBox>
    </td>
    <td ></td>
  </tr>
  
  <tr>
    <td ></td>
    <td><label>Remarks</label></td>
    <td>
        <asp:TextBox ID="TxtRemarks" runat="server" Width = "100%" TabIndex="3"></asp:TextBox>
    </td>
    <td></td>
    
    <td><label>Agent</label></td>
    <td>
        <asp:DropDownList ID="CmbAgent" runat="server" Width="100%" TabIndex="4">
        </asp:DropDownList>
    </td>
    <td></td>
    <td><label>Cash / Transfer</label></td>
    <td>
            <asp:DropDownList ID="CmbCashTransfer" runat="server" Width="100%" 
                AutoPostBack="True" TabIndex="5">
            </asp:DropDownList>
                                </td>
    <td></td>

  </tr>
  <tr>
    <td></td>
    <td>
        <label>Standing Instruction</label>
    </td>
    <td>
        <asp:DropDownList ID="Ddl_SI" runat="server" Width="100%" AutoPostBack="True" 
            TabIndex="6">
            <asp:ListItem Value="N">No</asp:ListItem>
            <asp:ListItem Value="Y">Yes</asp:ListItem>                
        </asp:DropDownList>
    </td>
    <td></td>
    <td><label>Nominee</label></td>
    <td>
            <asp:DropDownList ID="CmbNomReq" runat="server" TabIndex="7" Width="100%" 
                AutoPostBack="True">
            </asp:DropDownList>
    </td>    
  </tr>
  
</table>

<div id ="NomDiv" runat="server" visible="false">
        <table width = "100%">
	        <tr>
		        <td width = "2%" > </td>
		        <td width = "15%"> <label>Nominee Name</label></td>
		        <td width = "49%"> <asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_NomName" 
                        runat="server"  Width="99%" TabIndex="8"></asp:TextBox></td>
		        <td width = "2%" > </td>
		        <td width = "15%"> <label>Relation</label></td>
		        <td width = "15%"> 
                    <asp:DropDownList ID="CmbNomRel" runat="server" Width="100%" TabIndex="9">
                    </asp:DropDownList>
                </td>
		        <td width = "2%" > </td>
	        </tr>	
            <tr>
                <td></td>
                <td><label>Nominee Address</label></td>
                <td><asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_NomAdd" runat="server"  
                        Width="99%" TabIndex="10"></asp:TextBox></td>
		        <td></td>
		        <td><label>Phone No</label></td>
		        <td><asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_NomPh" runat="server"  
                        Width="96%" TabIndex="11"></asp:TextBox></td>
                <td></td>
            </tr>
        </table>
</div>    

<div id="StandDiv" runat="server" visible="false" >
    <table width="100%">
        <tr>
            <td width = "2%"></td>
            <td width = "15%"><label>Standing Branch</label></td>
            <td width = "15%">
                <asp:DropDownList ID="CmbStandBr" runat="server" Width="100%" TabIndex="12">
                </asp:DropDownList>
            </td>
            <td width = "2%"></td>
            <td width = "16%"><label>Standing Scheme</label></td>
            <td width = "16%">
                <asp:DropDownList ID="CmbStandSch" runat="server" Width="100%" TabIndex="13">
                </asp:DropDownList>            
            </td>
            <td width = "2%"></td>            
            <td width = "15%"><label>Standing Account No</label></td>
            <td width = "15%">
                <asp:TextBox ID="TxtStandAcNo" runat="server" CssClass="twitterStyleTextbox" 
                    TabIndex="14" Width="96%"></asp:TextBox>
            </td>
            <td width = "2%"></td> 
        </tr>
    </table>
</div>   

<table width="100%">
    <tr>
        <td width="2%"></td>
            <td width="96%">
                <asp:GridView ID="DgvTransaction" runat="server" AutoGenerateColumns="False" 
                    Width="100%">
                    <Columns>
                        <asp:BoundField DataField="AccNo" HeaderText="AccNo" Visible="False">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="10%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AccHead" HeaderText="AccHead">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="30%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RctCash" HeaderText="Receipt Cash">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="15%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RctTransfer" HeaderText="Receipt Transfer">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="10%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PayCash" HeaderText="Payment Cash">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="10%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PayTransfer" HeaderText="Payment Transfer">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="10%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
            <td width="2%"></td>
        </tr>
            <tr>
                <td width="2%">
                    &nbsp;</td>
                <td width="96%">
                    <table border="0" width="100%">
                        <tr>
                            <td align="right" scope="col" width="40%">
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#0033CC" 
                                    Text="TOTAL"></asp:Label>
                            </td>
                            <td align="right" scope="col" width="20%">
                                <asp:Label ID="LblRctCashTotal" runat="server" Font-Bold="True" 
                                    ForeColor="#0033CC"></asp:Label>
                            </td>
                            <td align="right" scope="col" width="14%">
                                <asp:Label ID="LblRctTransferTotal" runat="server" Font-Bold="True" 
                                    ForeColor="#0033CC"></asp:Label>
                            </td>
                            <td align="right" scope="col" width="14%">
                                <asp:Label ID="LblPayCashTotal" runat="server" Font-Bold="True" 
                                    ForeColor="#0033CC"></asp:Label>
                            </td>
                            <td align="right" scope="col" width="12%">
                                <asp:Label ID="LblPayTransferTotal" runat="server" Font-Bold="True" 
                                    ForeColor="#0033CC"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="2%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="2%">
                    &nbsp;</td>
                <td width="96%" align="center">
                    <asp:Label ID="LblWords" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                    <td width="2%">
                        &nbsp;</td>
                </td>
            </tr>
</table>
	<br/>
	<table width="100%">
	<tr>
	<td ></td>
	<td  align="center">
            <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                meta:resourcekey="CmdSaveResource1" TabIndex="15"  />
            <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/images/Close.png" 
                meta:resourcekey="CmdClearResource1" Width="100px" />
    </td>

	</tr>
	<tr>
		<td colspan="2" >
        <asp:HiddenField ID="AgentCollection" runat="server" Value="" />
        <asp:HiddenField ID="HdnMemBrCode" runat="server" />
        <asp:HiddenField ID="HdnMemType" runat="server" />
        <asp:HiddenField ID="HdnMemNo" runat="server" />
    </td>
	</tr>
	</table>

</div>
</ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>
