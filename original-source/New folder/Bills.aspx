<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Bills.aspx.vb" Inherits="SMARTCORE_UI.Bills" MasterPageFile="~/SMARTCORE.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <script type='text/javascript' src="Scripts/Common.js"></script>  
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<script type='text/javascript'>


    function ValidateSave() {


        var Ctl = document.getElementById('<%=TxtInstDate.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('InstDate  Not Selected');
            Ctl.focus();
            return false;
        }
        
        var Ctl = document.getElementById('<%=TxtInstDate.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('InstDate  Not Selected');
            Ctl.focus();
            return false;
        }


        var Ctl = document.getElementById('<%=TxtBillAmt.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Amount  Not Selected');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtDate.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Date  Not Selected');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtInstNo.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('InstNo  Not Selected');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtAmount.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Amount  Not Selected');
            Ctl.focus();
            return false;
        }



        var r = confirm("Do You Want to Save ? ");
        if (r == true) {
            return true;
        }
        else {
            return false;
        }  
        
        return true;
    }

</script>

    <script language="javascript" type="text/javascript">

        function validatedate(inputText) {
            var dateformat = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
            // Match the date format through regular expression  
            if (inputText.value.match(dateformat)) {
                //  document.form1.text1.focus();  
                //Test which seperator is used '/' or '-'  
                var opera1 = inputText.value.split('/');
                var opera2 = inputText.value.split('-');
                lopera1 = opera1.length;
                lopera2 = opera2.length;
                // Extract the string into month, date and year  
                if (lopera1 > 1) {
                    var pdate = inputText.value.split('/');
                }
                else if (lopera2 > 1) {
                    var pdate = inputText.value.split('-');
                }
                var dd = parseInt(pdate[0]);
                var mm = parseInt(pdate[1]);
                var yy = parseInt(pdate[2]);
                // Create list of days of a month [assume there is no leap year by default]  
                var ListofDays = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
                if (mm == 1 || mm > 2) {
                    if (dd > ListofDays[mm - 1]) {
                        alertify.error('Invalid date format! Please enter a valid date!');

                        return false;
                    }
                }
                if (mm == 2) {
                    var lyear = false;
                    if ((!(yy % 4) && yy % 100) || !(yy % 400)) {
                        lyear = true;
                    }
                    if ((lyear == false) && (dd >= 29)) {
                        alertify.error('Invalid date format! Please enter a valid date!');

                        return false;
                    }
                    if ((lyear == true) && (dd > 29)) {
                        alertify.error('Invalid date format! Please enter a valid date!');

                        return false;
                    }
                }
            }
            else {
                alertify.error("Invalid date format! Please enter a valid date!");

                document.form1.text1.focus();
                return false;
            }
        }  

</script>
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
<div class="gradientBoxesWithOuterShadows" style="width: 90%; margin-left:5%; margin-right:5%;" > 
               
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
    </Scripts>
   
</asp:ToolkitScriptManager>
     <legend>  BILLS  </legend>
     
<table width = "100%">
	<tr>
		<td width = "25%"></td>
		<td width = "50%" align="center">
            <asp:RadioButtonList ID="RdoBillType" runat="server" 
                RepeatDirection="Horizontal" Width="100%">
                <asp:ListItem Selected="True" Value="O">Outward Bills</asp:ListItem>
                <asp:ListItem Value="I">Inward Bills</asp:ListItem>
            </asp:RadioButtonList>
        </td>
		<td width = "25%"></td>
	</table>

<table width = "100%">
	<tr>
		<td width = "2%"></td>
		<td width = "15%">
            <label>
            Bill Type</label></td>
		<td width = "15%">
            <asp:DropDownList ID="cmbBillType" runat="server" AutoPostBack="True" 
                Width="100%" TabIndex="1">
            </asp:DropDownList>
        </td>
		<td width = "2%"></td>
		<td width = "16%">
            <label>
            Date</label></td>
		<td width = "16%">
            <asp:TextBox ID="TxtDate" runat="server"  Width="97%" OnChange="validatedate(this);"
                CssClass="twitterStyleTextbox" TabIndex="2"></asp:TextBox>
        </td>
		<td width = "2%"></td>
		<td width = "15%">
            <label>
            Bill Amt</label></td>
		<td width = "15%">
            <asp:TextBox ID="TxtBillAmt" runat="server" AutoPostBack="True" Width="97%" 
                CssClass="twitterStyleTextbox" TabIndex="3" onkeypress="return isFloatKey(event);"></asp:TextBox>
        </td>
		<td width = "2%"></td>
	    <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="15%">
                <label>
                Inst Type</label></td>
            <td width="15%">
                <asp:DropDownList ID="CmbInstType" runat="server" Width="100%" TabIndex="4">
                </asp:DropDownList>
            </td>
            <td width="2%">
                &nbsp;</td>
            <td width="16%">
                <label>
                Inst No</label></td>
            <td width="16%">
                <asp:TextBox ID="TxtInstNo" runat="server"  Width="97%" 
                    CssClass="twitterStyleTextbox" TabIndex="5" onkeypress="return isFloatKey(event);"></asp:TextBox>
            </td>
            <td width="2%">
                &nbsp;</td>
            <td width="15%">
                <label>
                Inst Date</label></td>
            <td width="15%">
                <asp:TextBox ID="TxtInstDate" runat="server"  Width="97%" OnChange="validatedate(this);"
                    CssClass="twitterStyleTextbox" TabIndex="6"></asp:TextBox>
                <asp:CalendarExtender ID="TxtInstDate_CalendarExtender" runat="server" 
                    Format="dd/MM/yyyy" TargetControlID="TxtInstDate">
                </asp:CalendarExtender>
            </td>
            <td width="2%">
                &nbsp;</td>
            <tr>
                <td width="2%">
                    &nbsp;</td>
                <td width="15%">
                    <label>
                    Bank Bc</label></td>
                <td width="15%">
                    <asp:TextBox ID="TxtBankBc" runat="server"  Width="97%" 
                        CssClass="twitterStyleTextbox" TabIndex="7"></asp:TextBox>
                </td>
                <td width="2%">
                    &nbsp;</td>
                <td width="16%">
                    <label>
                    Bank Bc Date</label></td>
                <td width="16%">
                    <asp:TextBox ID="TxtBankBcDate" runat="server"  Width="97%" 
                        CssClass="twitterStyleTextbox" TabIndex="8"></asp:TextBox>
                    <asp:CalendarExtender ID="TxtBankBcDate_CalendarExtender" runat="server" 
                        Format="dd/MM/yyyy" TargetControlID="TxtBankBcDate">
                    </asp:CalendarExtender>
                </td>
                <td width="2%">
                    &nbsp;</td>
                <td width="15%">
                    &nbsp;</td>
                <td width="15%">
                    &nbsp;</td>
                <td width="2%">
                    &nbsp;</td>
            </tr>
        </tr>
	</table>
<div id ="BankDtailsDiv" runat="server" visible="false">
    <table style="width: 100%">
	    <tr>
		    <td width = "2%"></td>
		    <td width = "15%">
                <label>
                Bank Code</label></td>
		    <td width = "15%">
                <asp:TextBox ID="TxtBankCode" runat="server" Width="97%" 
                    CssClass="twitterStyleTextbox" TabIndex="9"></asp:TextBox>
            </td>
		    <td width = "2%"></td>
		    <td width = "16%">
                <label>
                Bank Name</label></td>
		    <td width = "16%">
                <asp:DropDownList ID="CmbBankName" runat="server" Width="100%" TabIndex="10">
                </asp:DropDownList>
            </td>
		    <td width = "2%"></td>
		    <td width = "15%">
                <label>
                Bank Branch</label> </td>
		    <td width = "15%">
                <asp:DropDownList ID="CmbBankBranch" runat="server" Width="100%" TabIndex="11">
                </asp:DropDownList>
            </td>
		    <td width = "2%"></td>
	   </table>
</div>

<table width = "100%">
	<tr>
		<td width = "2%"></td>
		<td width = "15%">
            <label>
            Payee Bank</label> </td>
		<td width = "15%">
            <asp:TextBox ID="TxtPayeeBank" runat="server"  Width="97%" 
                CssClass="twitterStyleTextbox" TabIndex="12"></asp:TextBox>
            <asp:autocompleteextender ID="AutoCompleteExtender1" runat="server" 
                TargetControlID="TxtPayeeBank" MinimumPrefixLength="1" CompletionInterval="10" 
                EnableCaching="true" CompletionSetCount="6"
                UseContextKey="True" ServiceMethod="GetCompletionList_PayeeBank" 
                CompletionListCssClass="wordWheel listMain .box" 
                CompletionListItemCssClass="wordWheel itemsMain" 
                CompletionListHighlightedItemCssClass="wordWheel itemsSelected">
            </asp:autocompleteextender>

        </td>
		<td width = "2%"></td>
		<td width = "16%">
            <label>
            Payee Branch</label></td>
		<td width = "16%">
            <asp:TextBox ID="TxtPayeeBranch" runat="server"  Width="97%" 
                CssClass="twitterStyleTextbox" TabIndex="13"></asp:TextBox>
            <asp:autocompleteextender ID="AutoCompleteExtender2" runat="server" 
                TargetControlID="TxtPayeeBranch" MinimumPrefixLength="1" CompletionInterval="10" 
                EnableCaching="true" CompletionSetCount="6"
                UseContextKey="True" ServiceMethod="GetCompletionList_PayeeBranch" 
                CompletionListCssClass="wordWheel listMain .box" 
                CompletionListItemCssClass="wordWheel itemsMain" 
                CompletionListHighlightedItemCssClass="wordWheel itemsSelected">
            </asp:autocompleteextender>
        </td>
		<td width = "2%"></td>
		<td width = "15%">

		
           <%-- <table width="100%">
                <tr>
                    <td width="30%">--%>
                        <label>
                        Collection Bank</label>
                    </td>

                
                    <td width="15%">
                        <asp:DropDownList ID="CmbCollnBank" runat="server" Width="100%" TabIndex="14">
                        </asp:DropDownList>
                <%--    </td>
                </tr>
            </table>--%>

		
 </td>

		<td width = "2%"></td>
	    <tr>
            <td width="2%">&nbsp;</td>
            <td width="15%">
                <label>
                Drawer</label>&nbsp;</td>
            <td width="15%">
                <asp:TextBox ID="TxtDrawer" runat="server" Width="97%" 
                    CssClass="twitterStyleTextbox" TabIndex="15"></asp:TextBox>
            </td>
            <td width="2%">
                &nbsp;</td>
            <td width="16%">
                <label>
                Bearer</label>&nbsp;</td>
            <td width="16%">
                <asp:TextBox ID="TxtBearer" runat="server"  Width="97%" 
                    CssClass="twitterStyleTextbox" TabIndex="16"></asp:TextBox>
            </td>
            <td width="2%">
                &nbsp;</td>
            <td width="15%">
                <label>
                Remarks</label></td>
            <td width="15%">
                <asp:TextBox ID="TxtRemark" runat="server"  Width="97%" 
                    CssClass="twitterStyleTextbox" TabIndex="17"></asp:TextBox>
            </td>
            <td width="2%">&nbsp;</td>
        </tr>
</table>
    <table width = "100%">
	    <tr>
		    <td width = "2%"></td>
		    <td width = "48%" valign="top">
                <table width = "100%">
	                <tr>
		                <td width = "33%">
                            <label>
                            Branch</label></td>
		                <td width = "10%">
                            <asp:TextBox ID="TxtBranch" runat="server" AutoPostBack="True" Width="96%" 
                                CssClass="twitterStyleTextbox" TabIndex="17" ></asp:TextBox>
                        </td>
		                <td width = "47%">
                            <asp:DropDownList ID="CmbBranch" runat="server" AutoPostBack="True" 
                                Width="100%" TabIndex="18">
                            </asp:DropDownList>
                        </td>
		                <td width = "5%"></td>
		                <td width = "5%"></td>
	                    <tr>
                            <td width="20%">
                                <label>
                                Scheme</label></td>
                            <td width="10%">
                                <asp:TextBox ID="TxtScheme" runat="server" AutoPostBack="True" Width="96%" 
                                    CssClass="twitterStyleTextbox" TabIndex="19" ></asp:TextBox>
                            </td>
                            <td width="60%">
                                <asp:DropDownList ID="CmbScheme" runat="server" AutoPostBack="True" 
                                    Width="100%" TabIndex="20">
                                </asp:DropDownList>
                            </td>
                            <td width="5%">
                                &nbsp;</td>
                            <td width="5%">
                                &nbsp;</td>
                            <tr>
                                <td width="20%">
                                    <label>
                                    Account No</label></td>
                                <td width="10%">
                                    <asp:TextBox ID="TxtShortCode" runat="server" AutoPostBack="True" 
                                        CssClass="twitterStyleTextbox" TabIndex="21" Width="96%" onkeypress="return isFloatKey(event);"></asp:TextBox>
                                </td>
                                <td width="60%">
                                    <asp:TextBox ID="TxtAccNo" runat="server" AutoPostBack="True" 
                                        CssClass="twitterStyleTextbox" TabIndex="22" Width="99%" onkeypress="return isFloatKey(event);"></asp:TextBox>
                                </td>
                                <td width="5%">
                                    <asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
                                        ImageUrl="~/images/Search.png" Width="23px" TabIndex="500" />
                                </td>
                                <td width="5%">
                                    &nbsp;</td>
                            </tr>
                        </tr>
                </table>
		    </td>
		    <td valign="top" width="48%">
                <table width="100%">
                    <tr>
                        <td width="33%">
                            <label>
                            Account Details</label></td>
                        <td width="10%">
                            &nbsp;</td>
                        <td width="47%">
                            <asp:TextBox ID="TxtAccDetls" runat="server" Rows="3" TextMode="MultiLine"  Height="25"
                                Width="100%" TabIndex="500"></asp:TextBox>
                        </td>
                        <td width="5%">
                        </td>
                        <td width="5%">
                        </td>
                        <tr>
                            <td width="20%">
                                <label>
                                Amount</label></td>
                            <td width="10%">
                                &nbsp;</td>
                            <td width="60%">
                                <asp:TextBox ID="TxtAmount" runat="server" CssClass="twitterStyleTextbox" 
                                    Width="97%" TabIndex="23" onkeypress="return isFloatKey(event);"></asp:TextBox>
                            </td>
                            <td width="5%">
                                &nbsp;</td>
                            <td width="5%">
                                &nbsp;</td>
                            <tr>
                                <td width="20%">
                                    <label>
                                    Remarks</label></td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="60%">
                                    <asp:TextBox ID="TxtRemarks" runat="server" CssClass="twitterStyleTextbox" 
                                        Width="97%" TabIndex="24"></asp:TextBox>
                                </td>
                                <td width="5%">
                                    <asp:Button ID="BtnAdd" runat="server" Text="+" TabIndex="25" />
                                </td>
                                <td width="5%">
                                    &nbsp;</td>
                            </tr>
                        </tr>
                    </tr>
                </table>
            </td>
            <td width="2%">
            </td>
	    </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td colspan="2" style="width: 96%" valign="top" width="48%">
                <asp:GridView ID="DgvBill" runat="server" AutoGenerateColumns="False" 
                    CssClass="grid-viewNew" Width="100%" TabIndex="500">
                    <Columns>
                        <asp:BoundField DataField="Bill_ID" HeaderText="Bill_ID" Visible="False" />
                        <asp:BoundField DataField="TrACC_ID" HeaderText="TrACC_ID" Visible="False" />
                        <asp:BoundField DataField="Name" HeaderText="Name" Visible="True" />
                        <asp:BoundField DataField="Tran_Type" HeaderText="Tran_Type" Visible="False" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" />
                        <asp:BoundField DataField="Narration" HeaderText="Narration" />
                        <%--  <asp:BoundField DataField="Sl_No" HeaderText="Sl_No" Visible="False" />--%>
                    </Columns>
                </asp:GridView>
            </td>
            <td width="2%">
                &nbsp;</td>
        </tr>
    </table>
    
    <table width = "100%">
	<tr>
		<td width = "25%"></td>
		<td width = "50%" align="center">
            <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" 
                OnClientClick="return ValidateSave();" TabIndex="26"  />
            <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" 
                TabIndex="27" />
            <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" 
                TabIndex="28" />
        </td>
		<td width = "25%">
            <asp:HiddenField ID="HdnCustName" runat="server" Visible="False" />
        </td>
	</table>
       

   </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>
