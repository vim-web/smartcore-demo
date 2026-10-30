<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="BankAccountMaster.aspx.vb" Inherits="SMARTCORE_UI.BankAccountMaster" 
    title="Bank Account Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="head">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />      
    <script type='text/javascript' src="Scripts/Common.js"></script>  
    <script type='text/javascript' src="Scripts/jquery-1.8.3.js"></script>  

</asp:Content>
<asp:Content ID="Content2" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">    
     <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                <ContentTemplate>
                
                 <script type = "text/javascript">

                     // To Bind the function to the textbox for moving the cursor from one textbox to another
                     $(document).ready(function() {

                         var elem = document.getElementById('<%=DivMain.ClientID%>').getElementsByTagName("*");

                         for (var i = 0; i < elem.length; i++) {
                             elem[i].disabled = true;
                         }

                     });



                 </script>
                 
                 
   
                 
       
     <div class="gradientBoxesWithOuterShadows" style="width: 98%; margin-left:1%; margin-right:1%;margin-top:1%;" >  
     
   <asp:Panel ID="PnlAuthorise" runat="server">                          
                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                        <Scripts>
                            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
                        </Scripts>
                        
                        
                        
                        
                    </asp:ToolkitScriptManager>
        <legend>ACCOUNT OPEN</legend>       
        
        
        
                                                    
        <table width="100%">
            <tr>
                <td>
                    </td>
            </tr>
            
        </table>
 <div id="DivMain" runat ="server" > 
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
            <asp:DropDownList ID="CmbOpMode" runat="server" Width="100%" TabIndex="1">
            </asp:DropDownList>
        </td>
    <td width="2%"></td>
  </tr>
  <tr>
    <td></td>
    <td><label>Scheme</label></td>
    <td>
            <asp:DropDownList ID="CmbScheme" runat="server" Width="100%" 
                AutoPostBack="True" TabIndex="4">
            </asp:DropDownList>
        </td>
    <td></td>
    <td><label>No of Operators</label></td>
    <td>
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtNoOfOperator" runat="server"  Width="96%"></asp:TextBox>
        </td>
    <td></td>
  </tr>
</table>
</div>   
          <tr>
          <td>
          
          </td>
          </tr>
          
                    <table border="0" width="100%">
                        <tr>
                            <td width="2%">
                            </td>
                            <td width="96%">
                                <asp:GridView ID="DgvCustomer" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="CustID" TabIndex="23" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="MemDtls" HeaderText="Member Details">
                                            <HeaderStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" 
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
                                            <HeaderStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" 
                                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                                Font-Size="Small" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CustID" HeaderText="Customer ID">
                                            <HeaderStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" 
                                                BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="8%" />
                                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                                Font-Size="Small" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CustName" DataFormatString="{0:F2}" 
                                            HeaderText="Name">
                                            <HeaderStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" 
                                                BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                                                Width="17%" />
                                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                                Font-Size="Small" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Relation" HeaderText="Relation">
                                            <HeaderStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" 
                                                BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                                                Width="20%" />
                                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                                Font-Size="Small" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CustAdd" HeaderText="Address">
                                            <HeaderStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" 
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
                                            <HeaderStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" 
                                                BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                                                Width="6%" />
                                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                                Font-Size="Small" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Age" HeaderText="Age">
                                            <HeaderStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" 
                                                BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                                                Width="4%" />
                                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                                Font-Size="Small" />
                                        </asp:BoundField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#F5F4FF" />
                                </asp:GridView>
                            </td>
                            <td width="2%">
                            </td>
                        </tr>
                    </table>
        
        
        
<table width = "100%">
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
        <td width="2%">
            &nbsp;</td>
        <td width="15%">
            <asp:Label ID="Lbl_OpeningDate" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Opening Date"></asp:Label>
        </td>
        <td width="15%">
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="Ce_OpeningDate" runat="server"  Width="96%" TabIndex="13"></asp:TextBox>
        </td>
        <td width="2%">
            &nbsp;</td>
        <td width="16%">
            <asp:Label ID="Lbl_EffectiveDate" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Effective Date"></asp:Label>
        </td>
        <td width="16%">
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="Ce_EffectiveDt" runat="server"  Width="96%"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                TargetControlID="Ce_EffectiveDt">
            </asp:CalendarExtender>
        </td>
        <td width="2%">
            &nbsp;</td>
        <td width="15%">
            <asp:Label ID="Lbl_DueDate" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Interest %"></asp:Label>
        </td>
        <td width="15%">
            <asp:TextBox ID="Txt_IntPercent" runat="server" CssClass="twitterStyleTextbox" 
                onkeypress="return isFloatKey(event);" style="text-align: right" Width="96%"></asp:TextBox>
        </td>
        <td width="2%">
            &nbsp;</td>
    </tr>
<%--    <tr>
        <td width="2%">
            &nbsp;</td>
        <td width="15%">
            <asp:Label ID="Lbl_Month" runat="server" Font-Names="Tahoma" Font-Size="Medium" 
                ForeColor="#6666FF" Text="Month"></asp:Label>
        </td>
        <td width="15%">
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_Month" runat="server" AutoPostBack="True" 
                onkeypress="return isFloatKey(event);" style="text-align:right"  
                Width="96%" MaxLength="3"></asp:TextBox>
        </td>
        <td width="2%">
            &nbsp;</td>
        <td width="16%">
            <asp:Label ID="Lbl_Days" runat="server" Font-Names="Tahoma" Font-Size="Medium" 
                ForeColor="#6666FF" Text="Days"></asp:Label>
        </td>
        <td width="16%">
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_Days" runat="server" AutoPostBack="True" 
                onkeypress="return isFloatKey(event);" style="text-align:right"  
                Width="96%" MaxLength="3"></asp:TextBox>
        </td>
        <td width="2%">
            &nbsp;</td>
        <td width="15%">
            <asp:Label ID="Lbl_IntPayMode" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Int Pay Mode"></asp:Label>
        </td>
        <td width="15%">
            <asp:DropDownList ID="CmbInterestPayMode" runat="server"
                Width="100%">
            </asp:DropDownList>
        </td>
        <td width="2%">
            &nbsp;</td>
    </tr>--%>
    <tr>
        <td width="2%">
            &nbsp;</td>
        <td width="15%">
            <asp:Label ID="Lbl_InitialDeposit" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Initial Deposit"></asp:Label>
        </td>
        <td width="15%">
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_InitialDeposit" runat="server" AutoPostBack="True" 
                onkeypress="return isFloatKey(event);"  style="text-align:right"  Width="96%"></asp:TextBox>
        </td>
        <td width="2%">
            &nbsp;</td>
        <td width="16%">
            <asp:Label ID="Lbl_Agent" runat="server" Font-Names="Tahoma" Font-Size="Medium" 
                ForeColor="#6666FF" Text="Bank A/c No"></asp:Label>
        </td>
        <td width="16%">
            <asp:TextBox ID="Txt_BankAcNo" runat="server" CssClass="twitterStyleTextbox" 
                TabIndex="28" Width="96%"></asp:TextBox>
        </td>
        <td width="2%">
            &nbsp;</td>
        <td width="15%">
            <asp:Label ID="Lbl_Agent0" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Bank Name"></asp:Label>
        </td>
        <td width="15%">
            <asp:TextBox ID="Txt_BankName" runat="server" CssClass="twitterStyleTextbox" 
                TabIndex="28" Width="96%"></asp:TextBox>
        </td>
        <td width="2%">
            &nbsp;</td>
    </tr>
    <tr>
        <td width="2%">
            &nbsp;</td>
        <td width="15%">
            <asp:Label ID="Lbl_Agent1" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Bank Branch Name"></asp:Label>
        </td>
        <td width="15%">
            <asp:TextBox ID="Txt_BankBrName" runat="server" CssClass="twitterStyleTextbox" 
                TabIndex="28" Width="96%"></asp:TextBox>
        </td>
        <td width="2%">
                            &nbsp;</td>
        <td width="16%">
            &nbsp;</td>
        <td width="16%">
            &nbsp;</td>
        <td width="2%">
            &nbsp;</td>
        <td width="15%">
            <asp:Label ID="Lbl_CashTransfer" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Cash/Transfer"></asp:Label>
        </td>
        <td width="15%">
            <asp:DropDownList ID="CmbCashTransfer" runat="server" AutoPostBack="True" 
                Font-Names="Tahoma" Font-Size="Medium" ForeColor="#6666FF" TabIndex="29" 
                Width="100%">
            </asp:DropDownList>
        </td>
        <td width="2%">
            &nbsp;</td>
    </tr>
    
    <tr>
        <td width="2%">
            &nbsp;</td>
        <td width="15%">
            &nbsp;</td>
        <td width="15%">
            &nbsp;</td>
        <td width="2%">
            &nbsp;</td>
        <td width="16%">
            &nbsp;</td>
        <td width="16%">
            &nbsp;</td>
        <td width="2%">
            &nbsp;</td>
        <td width="15%">
            &nbsp;</td>
        <td width="15%">
            &nbsp;</td>
        <td width="2%">
            &nbsp;</td>
    </tr>
</table> 

<div id="DivMultipleDep" class="gradientBoxesWithOuterShadows" runat="server" visible="false" style="margin-left:2%;margin-right:2%;margin-bottom:2%;margin-top:2%;">
        <legend>SPLIT A/cs</legend>
            <table width = "100%">
	        
            </table>            
                <table border="0" width="100%">
                    <tr>
                        <td width="2%"></td>
                        <td width="96%">
                            <asp:GridView ID="DgvDepDtls" runat="server" AutoGenerateColumns="False" 
                                TabIndex="32" Width="100%" DataKeyNames="SlNo">
                                <Columns>
                                    <asp:BoundField DataField="Dep_Mth" HeaderText="Month">
                                        <HeaderStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="5%" />
                                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                            Font-Size="Small" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Dep_Days" HeaderText="Days">
                                        <HeaderStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="5%" />
                                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                            Font-Size="Small" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Dep_DueDt" HeaderText="Due Date">
                                        <HeaderStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="5%" />
                                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                            Font-Size="Small" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Dep_Amt" HeaderText="Amount">
                                        <HeaderStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="5%" />
                                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                            Font-Size="Small" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Dep_IntRate" HeaderText="Int Rate">
                                        <HeaderStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="5%" />
                                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                            Font-Size="Small" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Dep_MatureAmt" HeaderText="Maturity Amt">
                                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="8%" />
                                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                            Font-Size="Small" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Dep_IntPayMode" HeaderText="Int Pay Mode">
                                        <HeaderStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="10%" />
                                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                            Font-Size="Small" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Dep_NomNm" HeaderText="Nominee" >
                                        <HeaderStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Dep_NomRel" HeaderText="Relation" >
                                        <HeaderStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="9%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Dep_NomAdd" HeaderText="Address" >
                                        <HeaderStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Size="Small" ForeColor="White"  Width="18%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Dep_Standing" HeaderText="Standing Instr" >
                                        <HeaderStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Dep_IntPayModeCode" HeaderText="IntPayModeCode" 
                                        Visible="False" />
                                    <asp:BoundField DataField="Dep_StndBr" HeaderText="StndBr" Visible="False" />
                                    <asp:BoundField DataField="Dep_StndSch" HeaderText="StndSch" Visible="False" />
                                    <asp:BoundField DataField="Dep_StndAccNo" HeaderText="StndAccNo" 
                                        Visible="False" />
                                    <asp:BoundField DataField="Dep_NomRelCode" HeaderText="NomRelCode" 
                                        Visible="False" />
                                    <asp:CommandField DeleteText="&lt;img src='./images/del.jpg'&gt;" 
                                        ShowDeleteButton="True">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:CommandField>
                                    <asp:BoundField DataField="SlNo" Visible="False" />
                                </Columns>
                            </asp:GridView>
                        <td width="2%"></td>
                    </tr>
                    <tr><td></td></tr>
                </table>                    
        </div>

        <table border="0" width="100%">
        <tr>
            <td width="2%">
            </td>
            <td width="96%">
                <asp:GridView ID="DgvTransaction" runat="server" AutoGenerateColumns="False" 
                    TabIndex="32" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="AccNo" HeaderText="AccNo" Visible="False">
                            <HeaderStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="10%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AccHead" HeaderText="AccHead">
                            <HeaderStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="30%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RctCash" HeaderText="Receipt Cash">
                            <HeaderStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="15%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RctTransfer" HeaderText="Receipt Transfer">
                            <HeaderStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="10%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PayCash" HeaderText="Payment Cash">
                            <HeaderStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="10%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PayTransfer" HeaderText="Payment Transfer">
                            <HeaderStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="10%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Seq_No" HeaderText="Seq_No" Visible="False" />
                    </Columns>
                </asp:GridView>

            <td width="2%">
            </td>
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
   </asp:Panel>
	<table width="100%">
	    <tr>
	    <td width ="25%"></td>
	    <td width ="50%" align="center">
                <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                    meta:resourcekey="CmdSaveResource1" TabIndex="34"  />
                <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/images/Close.png" 
                    meta:resourcekey="CmdClearResource1" TabIndex="35" />
        </td>
	    <td width ="25%"> &nbsp;</td>
	    </tr>
	</table>

    
    
    
    <div id ="OthDtls" runat="server" visible="false">
    <table width="100%">
    <tr>
        <td width ="25%"> 
            <asp:Label ID="Lbl_AutoRenewal" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Auto Renewal"></asp:Label>
        </td>
        <td width ="25%"> 
            <asp:DropDownList ID="Ddl_AutoRenew" runat="server" 
                Width="123px">
                <asp:ListItem Value="Y">Yes</asp:ListItem>
                <asp:ListItem Value="N">No</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td width ="25%"> 
            <asp:Label ID="Lbl_MatAccNo" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Mat Acc No"></asp:Label>
        </td>
        <td width ="25%"> 
            <asp:TextBox ID="Txt_MatAcNo" runat="server" CssClass="twitterStyleTextbox" 
                TabIndex="27" Width="96%"></asp:TextBox>
        </td>
        <td width ="25%"> </td>
        <td width ="25%"> </td>
    </tr>
    <tr>
        <td> 
            <asp:Label ID="Lbl_BondNo" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Bond No"></asp:Label>
        </td>
        <td> 
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_BondNo" runat="server"></asp:TextBox>
        </td>
        <td> 
            <asp:Label ID="Lbl_ChequeIss" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Cheque Issue"></asp:Label>
        </td>
        <td> 
            <asp:DropDownList ID="Ddl_ChqIssue" runat="server"  Width="123px">
                <asp:ListItem Value="Y">Yes</asp:ListItem>
                <asp:ListItem Value="N">No</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td> </td>
        <td> </td>
    </tr>
    <tr>
        <td> 
            <asp:Label ID="Lbl_Introduceby" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Introduced By"></asp:Label>
        </td>
        <td> 
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_IntroBy" runat="server"></asp:TextBox>
        </td>
        <td> 
            <asp:Label ID="Lbl_Interestupto" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Interest Upto"></asp:Label>
        </td>
        <td> 
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="Ce_IntUpto" runat="server"></asp:TextBox>
        </td>
        <td> </td>
        <td> </td>
    </tr>
        <tr>
            <td>
                <asp:HiddenField ID="SchModuleCatg" runat="server" />
                <asp:HiddenField ID="SchSection" runat="server" />
                <asp:HiddenField ID="AgentCollection" runat="server" Value="" />
                <asp:HiddenField ID="HdnDoublingPeriod" runat="server" Value="" />
            </td>
            <td>
                &nbsp;</td>
            <td>
                <asp:Label ID="Lbl_TockenNo" runat="server" Font-Names="Tahoma" 
                    Font-Size="Medium" ForeColor="#6666FF" Text="Tocken No" Visible="False"></asp:Label>
            </td>
            <td>
                <asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_TockenNo" runat="server"  Width="96%" TabIndex="26" Visible="false"></asp:TextBox>                 
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                 </td>
            <td>
                 </td>
            <td>
                 </td>
            <td>
                 </td>
        </tr>
    </table>
    </div>
    </div>
                </ContentTemplate>
    </asp:UpdatePanel>
            </asp:Content>
            


