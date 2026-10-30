<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="TicketMaster.aspx.vb" Inherits="SMARTCORE_UI.TicketMaster" 
    title="Ticket Master" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
        

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
     <script type='text/javascript' src="Scripts/Common.js"></script>  
    <style type="text/css">
        .style1
        {
            height: 30px;
        }
        .style2
        {
            height: 28px;
        }
        .style3
        {
            width: 100%;
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
    <div class="gradientBoxesWithOuterShadows" style="width: 80%; margin-left:10%; margin-right:10%;margin-top:5%; font-family: Tahoma" >                
<legend>TICKET MASTER</legend>  
<br />
<table width = "100%">
<tr>
	<td width = "2%" class="style2"></td>
	<td width = "23%" class="style2">
                                                    <label>
                                                    Customer ID</label></td>
	<td width = "24%" class="style2">
                                                                <asp:TextBox ID="TxtCustID" runat="server"
                                                                    CssClass="twitterStyleTextbox" TabIndex="1" Width="79%"  Text="NEW"
                                                                    AutoPostBack="True" ></asp:TextBox>
                                                                <asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
                                                                    ImageUrl="~/images/Search.png" Width="23px" />
                                                            </td>
	<td width = "2%" class="style2"></td>
	<td width = "23%" class="style2">
                                                    <label>
                                                    Travel agency</label></td>
	<td width = "24%" class="style2">
                                                      
                                    <asp:DropDownList ID="CmbTravlAgncy" runat="server"  Width="100%">
                                    </asp:DropDownList>
    </td>
	<td width = "2%" class="style2"></td>
</Tr>
<Tr>
	<td width = "2%" class="style1"></td>
	<td width = "23%" class="style1">
                                                    <label>
                                                    Customer Name</label></td>
	<td width = "24%" class="style1">
                                                                <asp:TextBox ID="TxtName" runat="server"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="2" Width="99%"></asp:TextBox>
                                                            </td>
	<td width = "2%" class="style1"></td>
	<td width = "23%" class="style1">
                                                    <label>
                                                    From</label></td>
	<td width = "24%" class="style1">
                   <asp:TextBox ID="txtFrom" runat="server"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="13" Width="99%"></asp:TextBox>
        </td>
	<td width = "2%" class="style1"></td>
</Tr>
<%--<Tr>
	<td width = "2%"></td>
	<td width = "23%">
                                                    <label>
                                                    Relative Name</label></td>
	<td width = "24%">
                                                                <asp:TextBox ID="TxtRelName" runat="server"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="6" Width="99%"></asp:TextBox>
                                                            </td>
	<td width = "2%"></td>
	<td width = "23%">
                                                    <label>
                                                    Branch</label></td>
	<td width = "24%">
            <asp:DropDownList ID="CmbBranch" runat="server" Width="100%" 
                meta:resourcekey="CmbOccupationResource1" TabIndex="12">
            </asp:DropDownList>
        </td>
	<td width = "2%"></td>
</Tr>--%>
<Tr>
	<td width = "2%"></td>
	<td width = "23%">
                                                    <label>
                                                    Customer Address</label></td>
	<td width = "24%">
                                                                <asp:TextBox ID="TxtAdd1" runat="server"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="3" Width="99%"></asp:TextBox>
                                                            </td>
	<td width = "2%"></td>
	<td width = "23%">
        <label>
                                                    To</label></td>
	<td width = "24%">
           <asp:TextBox ID="txtTo" runat="server"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="14" Width="99%"></asp:TextBox>
                                                            </td>
	<td width = "2%"></td>
</Tr>
<Tr>
	<td width = "2%"></td>
	<td width = "23%"></td>
	<td width = "24%">
                                                                <asp:TextBox ID="TxtAdd2" runat="server"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="6" Width="99%"></asp:TextBox>
                                                            </td>
	<td width = "2%"></td>
	<td width = "23%">
        <label>
                                                    Departure Date</label></td>
	<td width = "24%">
                                                                <asp:TextBox ID="txtDepDate" runat="server"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="15" Width="99%"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender3" runat="server" 
            Format="dd/MM/yyyy" TargetControlID="txtDepDate">
        </asp:CalendarExtender>
                                                            </td>
	<td width = "2%"></td>
</Tr>
<Tr>
	<td width = "2%"></td>
	<td width = "23%">
                                                    </td>
	<td width = "24%">
                                                                <asp:TextBox ID="TxtAdd3" runat="server"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="6" Width="99%"></asp:TextBox>
                                                            </td>
	<td width = "2%"></td>
	<td width = "23%">
                                                    <label>
                                                   Return Date</label></td>
	<td width = "24%">
                                                  <asp:TextBox ID="txtRetDate" runat="server"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="16" Width="99%"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
            Format="dd/MM/yyyy" TargetControlID="txtRetDate">
        </asp:CalendarExtender>
                                                            </td>
	<td width = "2%"></td>
</Tr>
<Tr>
	<td width = "2%"></td>
	<td width = "23%">
                                                    <label>
                                                    Phone No</label></td>
	<td width = "24%">
                                                                <asp:TextBox ID="TxtPhoneNo" runat="server"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="6" Width="99%" MaxLength="12" onkeypress="return isFloatKey(event);"></asp:TextBox>
                                                            </td>
	<td width = "2%"></td>
	<td width = "23%">
                                                    <label>
                                                    No of Passengers
                                                    </label>
                                                    
    </td>
	<td width = "24%">
                                                                <asp:TextBox ID="txtNoofPassanger" runat="server"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="17" Width="99%"  onkeypress="return isFloatKey(event);" ></asp:TextBox>
                                                 
                                                            </td>
	<td width = "2%">
                                                                &nbsp;</td>
</Tr>
<Tr>
	<td width = "2%"></td>
	<td width = "23%">
                                                    <label>
                                                    Passport No</label></td>
	<td width = "24%">
                                                                <asp:TextBox ID="TxtPassportNo" runat="server"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="7" Width="99%" MaxLength="12" ></asp:TextBox>
    </td>
	<td width = "2%"></td>
	<td width = "23%" colspan="2" style="width: 47%"><label>Adults(12+yrs) </label>
        <asp:TextBox ID="txtAdult" runat="server" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);"
            TabIndex="18" Width="7%" Text="0"></asp:TextBox>
            <label>Childrens(2-12yrs) </label>
        <asp:TextBox ID="txtchilfrens" runat="server" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);"
            TabIndex="19" Width="7%" Text="0"></asp:TextBox>
            <label>Infants(Under 2yrs) </label>
        <asp:TextBox ID="txtInfants" runat="server" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);"
            TabIndex="20" Width="7%" Text="0" ></asp:TextBox>
            
    </td>
	<td width = "2%"></td>
</Tr>


<%--<Tr>
	<td width = "2%"></td>
	<td width = "23%">
        <label>
                                                    Date of Birth &amp; Age</label></td>
	<td width = "24%">
        <asp:TextBox ID="TxtDob" runat="server" onblur="CalculateAge(this)"
        CssClass="twitterStyleTextbox" TabIndex="6" Width="55%"></asp:TextBox>
        <asp:CalendarExtender ID="CalendarExtender2" runat="server" 
            Format="dd/MM/yyyy" TargetControlID="TxtDob">
        </asp:CalendarExtender>

        <asp:TextBox ID="TxtAge" runat="server"
        CssClass="twitterStyleTextbox" TabIndex="6" Width="22%"></asp:TextBox>                                                           </td>
	<td width = "2%"></td>
	<td width = "23%">
                                                    <label>
                                                    Status</label></td>
	<td width = "24%">
            <asp:DropDownList ID="CmbStatus" runat="server" Width="100%" 
                meta:resourcekey="CmbOccupationResource1" TabIndex="12">
            </asp:DropDownList>
        </td>
	<td width = "2%"></td>
</Tr>--%>
<Tr>
	<td width = "2%"></td>
	<td width = "23%"><label> Travel Type</label>
        </td>
	<td width = "24%">  
        <asp:DropDownList ID="CmbTravelType" runat="server" Width="100%" 
                meta:resourcekey="CmbTravelBy" TabIndex="8" Visible ="True" >
                              
            </asp:DropDownList> </td>
	<td width = "2%"></td>
	<td width = "23%"><label> Class of Travel</label></td>
	<td width = "24%">
        <asp:DropDownList ID="cmbClassoftravel" runat="server" Width="100%" 
                meta:resourcekey="cmbClassoftravel" TabIndex="21" Visible ="True" >
                
            </asp:DropDownList></td>
	<td width = "2%"></td>
</Tr>

<Tr>
	<td width = "2%"></td>
	<td width = "23%">
                                                    <label>
                                                   Name of the Airline</label></td>
	<td width = "24%">
                                                                <asp:TextBox ID="txtNameofAirline" runat="server"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="9" Width="99%" MaxLength="12" AutoPostBack="True" ></asp:TextBox>
    </td>
	<td width = "2%"></td>
	<td width = "23%"><label>Per Ticket </label>
    </td>
	<td width = "24%">
                                                                <asp:TextBox ID="txtPerticket" runat="server" onkeypress="return isFloatKey(event);"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="22" Width="99%"></asp:TextBox>
                                                            </td>
	<td width = "2%"></td>
</Tr>
<Tr>
	<td width = "2%"></td>
	<td width = "23%"><label> Gender</label>
        </td>
	<td width = "24%">  
        <asp:DropDownList ID="cmbGender" runat="server" Width="100%" 
                meta:resourcekey="CmbStafftype" TabIndex="10" Visible ="True" >
                              
            </asp:DropDownList> </td>
	<td width = "2%"></td>
	<td width = "23%"><label>Tax (%)</label></td>
	<td width = "24%">   
        <asp:TextBox ID="txtTax" runat="server" onkeypress="return isFloatKey(event);"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="23" Width="99%"></asp:TextBox></td>
	<td width = "2%"></td>
</Tr>
<Tr>
	<td width = "2%"></td>
	<td width = "23%"><label>Type of Travel</label>
        </td>
	<td width = "24%">  
        <asp:DropDownList ID="cmbTravelsection" runat="server" Width="100%" AutoPostBack="true"
                meta:resourcekey="CmbStafftype" TabIndex="11" Visible ="True" >
                    
            </asp:DropDownList> </td>
	<td width = "2%"></td>
	<td width = "23%"><label>Total Amount</label></td>
	<td width = "24%">   
        <asp:TextBox ID="txtTotAmt" runat="server" onkeypress="return isFloatKey(event);"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="24" Width="99%" 
            AutoPostBack="True"></asp:TextBox></td>
	<td width = "2%"></td>
</Tr>
<Tr>
	<td width = "2%"></td>
	<td width="23%">
       <Label>Tran Type</Label>
                </td>
	<td width = "24%">  <asp:DropDownList ID="DdlTranType" runat="server" Width="100%" 
            AutoPostBack="True">
        </asp:DropDownList></td>
    <td width="2%">
        </td>
    <td width="23%"><label> Commission Amount</label>
        </td>
    <td width="24%">   
        <asp:TextBox ID="txtCommAmt" runat="server" onkeypress="return isFloatKey(event);"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="25" Width="99%"></asp:TextBox>
        </td>
    <td width="2%">
        </td>
</tr>
<Tr>
	<td width = "2%">&nbsp;</td>
	<td width="23%"><label>Remarks</label>
                  &nbsp;</td>
	<td colspan="4">
        <asp:TextBox ID="TxtRemarks" runat="server" Width="98%"></asp:TextBox>
    </td>
    <td width="2%">
        &nbsp;</td>
</tr> 
    <tr>
        <td width="2%">
            &nbsp;</td>
        <td colspan="5">
            <table class="style3">
                <tr>
                    <td>
                    <div id="DivTranGrid" runat="server">
                <table width="100%" border="0">
                    <tr>
                        <td ></td>
                        <td >
                            <asp:GridView ID="DgvTransaction" runat="server" AutoGenerateColumns="False"  CssClass="grid-viewNew"
                                TabIndex="999" Width="100%" >
                                <Columns>
                                    <asp:BoundField DataField="AccNo" HeaderText="AccNo" Visible="False">
                                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AccCode" HeaderText="AccCode" Visible="False" />
                                    <asp:BoundField DataField="AccHead" HeaderText="Particulars">
                                       
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RctCash" HeaderText="Receipt Cash" 
                                        DataFormatString="{0:F2}">
                                       
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RctTransfer" HeaderText="Receipt Transfer" 
                                        DataFormatString="{0:F2}">
                                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PayCash" HeaderText="Payment Cash" 
                                        DataFormatString="{0:F2}">
                                       
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PayTransfer" HeaderText="Payment Transfer" 
                                        DataFormatString="{0:F2}">
                                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Seq_No" HeaderText="Seq_No" Visible="False" />
                                </Columns>
                                <AlternatingRowStyle BackColor="#F5F7FE" />
                            </asp:GridView>
                        </td>
                        <td ></td>
                    </tr>
                    <tr>
                        <td width="2%"></td>
                        <td width="96%">
                        <table width="100%" border="0">
                            <tr>                  
                                <td width="20%" scope="col"  align="right">
                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#0033CC" 
                                        Text="TOTAL" Visible="False"></asp:Label>
                                </td>
                                <td width="20%" scope="col"  align="right">
                                    <asp:Label ID="LblRctCashTotal" runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>
                                </td>
                                <td width="20%" scope="col"  align="right">
                                    <asp:Label ID="LblRctTransferTotal" runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>
                                </td>
                                <td width="20%" scope="col"  align="right">
                                    <asp:Label ID="LblPayCashTotal" runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>
                                </td>
                                <td width="20%" scope="col"  align="right">
                                    <asp:Label ID="LblPayTransferTotal" runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        </td>
                        <td width="2%"></td>
                    </tr>
                </table>
                </div>
                        &nbsp;</td>
                </tr>
            </table>
        </td>
        <td width="2%">
            &nbsp;</td>
    </tr>
<Tr>
	<td width = "2%"></td>
	<td width="23%">
                    <asp:HiddenField ID="HdnCustid" runat="server" />
                </td>
	<td width = "24%"></td>
	<td width = "2%"></td>
	<td width="23%">
                </td>
	<td width = "24%"></td>
    <td width="2%">
        </td>
</tr> 
<Tr>
	<td width="2%">
    </td>
    <td align="center" colspan="5">
        <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
            meta:resourcekey="CmdSaveResource1" TabIndex="56" />
        <asp:ImageButton ID="butClear" runat="server" ImageUrl="~/Images/Clear.png" />
        <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/images/Close.png" 
            meta:resourcekey="CmdClearResource1" TabIndex="57" />
    </td>
    <td width="2%">
    </td>
</tr>
</table>
</div>
        </ContentTemplate>
        </asp:UpdatePanel> 

</asp:Content>
