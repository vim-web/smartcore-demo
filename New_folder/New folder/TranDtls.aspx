<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TranDtls.aspx.vb" Inherits="SMARTCORE_UI.TranDtls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>TRANSACTION DETAILS</title>
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
</head>
<body>
    <form id="form1" runat="server">
    <div class="gradientBoxesWithOuterShadows" style="width: 98%; margin-left:1%; margin-right:1%;margin-top:3%;margin-bottom:3%;height:94%;" >                    
    <table width="100%">
<tr>
    <td width="2%"></td>
    <td width="24%">
    <label>Branch</label></td>
    <td width="24%">
        <asp:TextBox ID="TxtBranch" Width="100%" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
    </td>
    <td width="2%"></td>
    <td width="23%">
    <label>Scheme</label></td>
    <td width="23%">
        <asp:TextBox ID="TxtScheme" Width="100%" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
    </td>
    <td width="23%">&nbsp;</td>
</tr>
<tr>
<td></td>
<td>
<label>Account No</label></td>
<td>
    <asp:TextBox ID="TxtAccNo" Width="100%" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
</td>
<td></td>
<td>
<label>Customer ID</label></td>
<td>
    <asp:TextBox ID="TxtCid" Width="100%" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td>
    <label>Opening Date</label></td>
<td>
    <asp:TextBox ID="TxtOpenDate" Width="100%" runat="server" 
        CssClass="twitterStyleTextbox"></asp:TextBox>
    </td>
<td>&nbsp;</td>
<td>
<label>Name</label></td>
<td>
    <asp:TextBox ID="TxtName" Width="100%" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td>
    <label>Period</label></td>
<td>
    <asp:TextBox ID="TxtPeriod" Width="100%" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
    </td>
<td>&nbsp;</td>
<td rowspan="2">
<label>Address</label></td>
<td rowspan="2">
    <asp:TextBox ID="TxtAddress" Width="100%" runat="server" TextMode="MultiLine" CssClass="twitterStyleTextbox"></asp:TextBox>
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td>
    <label>Due Date</label></td>
<td>
    <asp:TextBox ID="TxtDueDate" Width="100%" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
    </td>&nbsp;</td>
<td>
    &nbsp;</td>
</tr>
<tr>
<td>&nbsp;</td>
<td>
    <label>Interest Rate</label></td>
<td>
    <asp:TextBox ID="TxtInterestRate" Width="45%" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
    <asp:TextBox ID="TxtPenalInterest" Width="45%" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
    </td>
<td>&nbsp;</td>
<td><label> Balance</label></td>
<td>
    <asp:TextBox ID="TxtBalance" Width="100%" onkeypress="return validate(event)" 
        runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
                    </td>
<td>&nbsp;</td>
</tr>
</table>
<table width="100%">
<tr>
<td width ="2%">&nbsp;</td>
<td width ="24%">
    <label> Installment Amount</label></td>
<td width ="24%">
    <asp:TextBox ID="TxtInstlmentAmt" Width="100%" 
        onkeypress="return validate(event)" runat="server" CssClass="twitterStyleTextbox"></asp:TextBox>
    </td>
<td width ="2%">&nbsp;</td>
<td width ="23%">
<label>Overdue</label></td>
<td width ="23%">
    <asp:TextBox ID="TxtOverdue" Width="100%" runat="server" 
        CssClass="twitterStyleTextbox"></asp:TextBox>
    </td>
<td width ="2%">&nbsp;</td>
</tr>
<tr>
<td width ="2%">&nbsp;</td>
<td width ="24%">
    <label>Interest Days</label>&nbsp;</td>
<td width ="24%">
    <asp:TextBox ID="TxtDays" runat="server" Width="45%"></asp:TextBox>
                    </td>
<td width ="2%">&nbsp;</td>
<td width ="23%">
    <LABEL>Susupense Balance</LABEL>&nbsp;</td>
<td width ="23%">
    <asp:TextBox ID="Txt_SusBal" runat="server" Width="100%"></asp:TextBox>&nbsp;</td>
<td width ="2%">&nbsp;</td>
</tr>
<tr>
<td width ="2%">&nbsp;</td>
<td width ="24%">
    <label>Canvased By</label>&nbsp;</td>
<td width ="24%">
    <asp:TextBox ID="Txt_Canvased" runat="server" Width="100%"></asp:TextBox>&nbsp;</td>
<td width ="2%">&nbsp;</td>
<td width ="23%">
    <label>Pending InstNo</label>&nbsp;</td>
<td width ="23%">
    <asp:TextBox ID="TxtPendInst" runat="server" Width="100%"></asp:TextBox>&nbsp;</td>
<td width ="2%">&nbsp;</td>
</tr>
</table>
<table width="100%">
    <tr>
    <td width ="2%">&nbsp;</td>
    <td width ="24%">
        &nbsp;</td>
    <td  width ="72%">
        &nbsp;</td>
    <td width ="2%">&nbsp;</td>
    </tr>
    <tr>
    <td width ="2%">&nbsp;</td>
    <td width ="24%">
    <label>Other Details</label></td>
    <td  width ="72%">
        <asp:TextBox ID="TxtDetails" Width="100%" runat="server" 
            CssClass="twitterStyleTextbox"></asp:TextBox>
    </td>
    <td width ="2%">&nbsp;</td>
    </tr>
</table>


<div id = "DivAcTran" runat ="server" visible="true">
    <table width ="100%">
<tr>
<td width ="2%">&nbsp;</td>
<td width ="96%">
    <asp:GridView ID="DgvTransaction" Width="100%" runat="server" 
        AutoGenerateColumns="False" PageSize="5">
        <Columns>
            <asp:BoundField DataField="TRAN_ID" HeaderText="TRAN_ID" Visible="False" >
                <HeaderStyle Font-Size="Small" />
            </asp:BoundField>
            <asp:BoundField DataField="TR_DATE" HeaderText="DATE" 
                DataFormatString="{0:dd/MM/yyyy}">
                <HeaderStyle Width="9%" Font-Size="Small" />
            </asp:BoundField>
            <asp:BoundField DataField="CREDIT_AMT" HeaderText="CREDIT">
                <HeaderStyle Width="9%" Font-Size="Small"/>
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="DEBIT_AMT" HeaderText="DEBIT">
                <HeaderStyle Width="9%" Font-Size="Small"/>
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="INTEREST_CR" HeaderText="INT CR">
                <HeaderStyle Width="9%" Font-Size="Small"/>
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="INTEREST_DR" HeaderText="INT DR">
                <HeaderStyle Width="9%" Font-Size="Small"/>
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="CHRG_DR" HeaderText="CHARGES DR">
                <HeaderStyle Width="9%" Font-Size="Small"/>
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="CHRG_CR" HeaderText="CHARGES CR">
                <HeaderStyle Width="9%" Font-Size="Small"/>
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="Total_AMT" HeaderText="TOTAL">
                <HeaderStyle Width="9%" Font-Size="Small"/>
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="BAL_AMT" HeaderText="BALANCE">
                <HeaderStyle Width="9%" Font-Size="Small"/>
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="REMARKS" HeaderText="REMARKS">
                <HeaderStyle Width="28%" Font-Size="Small"/>
            </asp:BoundField>
        </Columns>
        <HeaderStyle BackColor="#666699" ForeColor="White" />
                 <AlternatingRowStyle BackColor="#D2D2FF" />
        
    </asp:GridView>
    </td>
<td width ="2%"></td>
</tr>
<tr>
<td width ="2%">&nbsp;</td>
<td width ="96%">
    <table width = "100%">
	<tr>
		<td width = "9%"></td>
		<td width = "9%" align="right">
                        <asp:Label ID="LblCrTotal" runat="server" ForeColor="#000066" Text="Label"></asp:Label>
                    </td>
		<td width = "9%" align="right">
                        <asp:Label ID="LblDrTotal" runat="server" ForeColor="#000066" 
                Text="Label"></asp:Label>
                    </td>
		<td width = "9%" align="right">
                        <asp:Label ID="LblIntCr" runat="server" ForeColor="#000066" 
                Text="Label"></asp:Label>
                    </td>
		<td width = "9%" align="right">
                        <asp:Label ID="LblIntDr" runat="server" ForeColor="#000066" 
                Text="Label"></asp:Label>
                    </td>
		<td width = "9%" align="right">
                        <asp:Label ID="LblChrDr" runat="server" ForeColor="#000066" 
                Text="Label"></asp:Label>
                    </td>
		<td width = "9%" align="right">
                        <asp:Label ID="LblChrCr" runat="server" ForeColor="#000066" 
                Text="Label"></asp:Label>
                    </td>
		<td width = "9%"></td>
		<td width = "28%"></td>
	</table>
</td>
<td width ="2%">&nbsp;</td>
</tr>
</table>
</div>


<div id = "DivChitty" runat ="server" visible="true">
    <table width ="100%">
<tr>
<td width ="2%">&nbsp;</td>
<td width ="96%">
    <asp:GridView ID="DgvGrpDep" Width="100%" runat="server" 
        AutoGenerateColumns="False" PageSize="5">
        <Columns>
            <asp:BoundField DataField="TRAN_ID" HeaderText="TRAN_ID" Visible="False" >
                <HeaderStyle Font-Size="Small" />
            </asp:BoundField>
            <asp:BoundField DataField="TR_DATE" HeaderText="DATE" 
                DataFormatString="{0:dd/MM/yy}">
                <HeaderStyle Width="8%" Font-Size="Small" />
            </asp:BoundField>
            <asp:BoundField DataField="INST_NO" HeaderText="INST NO">
                <HeaderStyle Width="4%" Font-Size="Small"/>
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="AUC_DATE" HeaderText="AUCDATE" 
                DataFormatString="{0:dd/MM/yy}">
                <HeaderStyle Width="8%" Font-Size="Small" />
            </asp:BoundField>
            <asp:BoundField DataField="CREDIT_AMT" HeaderText="CREDIT">
                <HeaderStyle Width="8%" Font-Size="Small"/>
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="DEBIT_AMT" HeaderText="DEBIT">
                <HeaderStyle Width="8%" Font-Size="Small"/>
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="DISC" HeaderText="DISC">
                <HeaderStyle Width="8%" Font-Size="Small"/>
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="INTAMT" HeaderText="INTAMT">
                <HeaderStyle Width="8%" Font-Size="Small"/>
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="FORFEIT" HeaderText="FORFEIT">
                <HeaderStyle Width="8%" Font-Size="Small"/>
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="BANKCHRG" HeaderText="BANKCHRG">
                <HeaderStyle Width="8%" Font-Size="Small"/>
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="CHRG_DR" HeaderText="CHARG DR">
                <HeaderStyle Width="8%" Font-Size="Small"/>
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="CHRG_CR" HeaderText="CHARG CR">
                <HeaderStyle Width="8%" Font-Size="Small"/>
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="BAL_AMT" HeaderText="BALANCE">
                <HeaderStyle Width="8%" Font-Size="Small"/>
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="REMARKS" HeaderText="REMARKS">
                <HeaderStyle Width="8%" Font-Size="Small"/>
            </asp:BoundField>
        </Columns>
        <HeaderStyle BackColor="#666699" ForeColor="White" />
                 <AlternatingRowStyle BackColor="#D2D2FF" />
        
    </asp:GridView>
    </td>
<td width ="2%"></td>
</tr>
<tr>
<td width ="2%">&nbsp;</td>
<td width ="96%">
    <table width = "100%">
	<tr>
		<td width = "8%"></td>
		<td width = "4%" align="right">&nbsp;</td>
		<td width = "8%" align="right">&nbsp;</td>
		<td width = "8%" align="right">
           <asp:Label ID="LblChittyCr" runat="server" ForeColor="#000066"></asp:Label>
        </td>
		<td width = "8%" align="right">
           <asp:Label ID="LblChittyDr" runat="server" ForeColor="#000066"></asp:Label>
        </td>
		<td width = "8%" align="right">
           <asp:Label ID="LblChittyDisc" runat="server" ForeColor="#000066"></asp:Label>
        </td>
		<td width = "8%" align="right">
           <asp:Label ID="LblChittyInt" runat="server" ForeColor="#000066"></asp:Label>
        </td>
		<td width = "8%" align="right">
           <asp:Label ID="LblChittyFor" runat="server" ForeColor="#000066"></asp:Label>
        </td>
		<td width = "8%" align="right"><asp:Label ID="LblChittyBnkChrg" runat="server" 
                ForeColor="#000066"></asp:Label></td>
		<td width = "8%" align="right"><asp:Label ID="LblChittyChrCr" runat="server" 
                ForeColor="#000066"></asp:Label></td>
		<td width = "8%" align="right"><asp:Label ID="LblChittyChrDr" runat="server" 
                ForeColor="#000066"></asp:Label></td>
		<td width = "8%" align="right">&nbsp;</td>
		<td width = "8%" align="right">&nbsp;</td>
	</tr>
	</table>
</td>
<td width ="2%">&nbsp;</td>
</tr>
</table>
</div>


            <asp:HiddenField ID="hdnAccid" runat="server" />
            <asp:HiddenField ID="hdnSection" runat="server" />
      <br />
    </div>
    </form>
</body>
</html>
