<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="SuspenseMaster.aspx.vb" Inherits="SMARTCORE_UI.SuspenseMaster" 
    title="SUSPENSE"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />      
    <script type='text/javascript' src="Scripts/Common.js"></script>  
    <script type='text/javascript' src="Scripts/jquery-1.8.3.js"></script>  
    <link href="CSS/ui-lightness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript' src="Scripts/jquery-ui-1.9.2.custom.min.js"></script>      
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
                        inputText.focus();
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
                        inputText.focus();
                        return false;
                    }
                    if ((lyear == true) && (dd > 29)) {
                        alertify.error('Invalid date format! Please enter a valid date!');
                        inputText.focus();
                        return false;
                    }
                }
            }
            else {
                alertify.error("Invalid date format! Please enter a valid date!");
                inputText.focus();
                document.form1.text1.focus();
                return false;
            }
        }  

</script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 30px;
        }
        .style3
        {
            width: 30%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<script type = "text/javascript">
    function imgBatchDtls_onclick() {
        $('.gradientBoxesWithOuterShadows').append('<div id="resultdiv" title="Batch Details" visible="false" >');
        objDDl = document.getElementById('<%=CmbBatchNo.ClientID%>');
        var selval = objDDl.options[objDDl.selectedIndex].value;
        $("#resultdiv").load("BatchDtls.aspx?BatchNo=" + selval, {}, function(data) {
            $("#resultdiv").show();
        });

        $("#resultdiv").dialog({
            autoOpen: false,
            height: 600,
            width: 900,
            //beforeClose: function(event, ui) { $('#resultdiv').remove(); },
            close: function(event, ui) { $(this).dialog("destroy"); },
            modal: true
        });
        $('#resultdiv').dialog('open');

    }   
</script>

<div class="gradientBoxesWithOuterShadows"  width: 100%; margin-left:25%; margin-right:25%; >                
<legend>SUSPENSE ACCOUNT</legend>   
<br />
 <asp:Panel ID="PnlAuthorise" runat="server">
 
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
                DataKeyNames="CustID" TabIndex="23" Width="100%">
                <Columns>
                    <asp:BoundField DataField="MemDtls" HeaderText="Member Details">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
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
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CustID" HeaderText="Customer ID">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="8%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CustName" DataFormatString="{0:F2}" 
                        HeaderText="Name">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                            Width="17%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Relation" HeaderText="Relation">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                            Width="20%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CustAdd" HeaderText="Address">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
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
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                            Width="6%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Age" HeaderText="Age">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                            Width="4%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Small" />
                    </asp:BoundField>
                </Columns>
                <AlternatingRowStyle BackColor="#F5F4FF" />
            </asp:GridView>
        
        </td>
        <td width="2%"></td>
    </tr>
</table>
<br />

<table width="100%" border="0">
  <tr>
    <td width="2%">&nbsp;</td>
    <td width="17%"><label>Opening Date</label></td>
    <td width="30%">
            <asp:TextBox ID="TxtOpDate" runat="server" Width = "100%" OnChange="validatedate(this);"></asp:TextBox>
                                </td>
    <td width="2%">&nbsp;</td>
    <td width="17%"><label>Initial Amount</label></td>
    <td width="30%">
            <asp:TextBox ID="TxtInitialDep" runat="server" Width = "100%" 
                AutoPostBack="True" onkeypress="return isFloatKey(event);" style="text-align:right"></asp:TextBox>
        </td>
    <td width="2%">&nbsp;</td>
  </tr>
  <tr>
    <td class="style2"></td>
    <td class="style2"><label>Cash / Transfer</label></td>
    <td class="style2">
            <asp:DropDownList ID="CmbCashTransfer" runat="server" Width="100%" 
                AutoPostBack="True">
            </asp:DropDownList>
                                </td>
    <td class="style2"></td>
    <td class="style2"><label>Remarks</label></td>
    <td class="style2">
            <asp:TextBox ID="TxtRemarks" runat="server" Width = "100%"></asp:TextBox>
        </td>
    <td class="style2"></td>
  </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
           
            &nbsp;</td>
        <td>
            <asp:CheckBox ID="ChkBank" runat="server" Enabled="false"  Text="Bank Details" 
                AutoPostBack="True" />
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
  <tr>
    <td>&nbsp;</td>
    <td colspan="5">
        <asp:Panel ID="Panel2" runat="server" Visible="false">
            <table class="style1" width="100%">
                <tr width="25%">
                    <td colspan="2">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                </tr>
                <tr width="25%">
                    <td width="25%">
                        <label>
                        Period Month/Days</label></td>
                    <td width="25%">
                        <table class="style1" width="100%">
                            <tr>
                                <td width="45%">
                                    <asp:TextBox ID="TxtMonth" runat="server" CssClass="twitterStyleTextbox" 
                                        onkeypress="return isFloatKey(event);" style="text-align:Left" Width="96%" 
                                        AutoPostBack="True"></asp:TextBox>
                                </td>
                                <td width="10">
                                    &nbsp;</td>
                                <td width="45%">
                                    <asp:TextBox ID="TxtDays" runat="server" CssClass="twitterStyleTextbox" 
                                        onkeypress="return isFloatKey(event);" style="text-align:Left" Width="90%" 
                                        AutoPostBack="True"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="25%">
                        <label>
                        Int PayMode</label>&nbsp;&nbsp;</td>
                    <td width="25%">
                        <asp:DropDownList ID="DrpIntPayMode" runat="server" Width="98%" 
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr width="25%">
                    <td>
                        <label>
                        Interest Rate</label> &nbsp;</td>
                    <td width="25%">
                        <asp:TextBox ID="TxtIntRate" runat="server" CssClass="twitterStyleTextbox" 
                            onkeypress="return isFloatKey(event);" style="text-align:right" Width="96%"></asp:TextBox>
                    </td>
                    <td width="25%">
                        <label>
                        Canvased By</label> &nbsp;</td>
                    <td width="25%">
                        <asp:DropDownList ID="DrpCanvas" runat="server" Width="98%">
                        </asp:DropDownList>
                        &nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
        &nbsp;</td>
    <td>
        &nbsp;</td>
  </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <label>
            Member Required</label></td>
        <td>
            <table width="100%">
                <tr>
                    <td width="60%">
                        <asp:CheckBox ID="ChkMember" runat="server" />
                    </td>
                    <td width="40%">
                        <label>
                        Batch No</label></td>
                </tr>
            </table>
        </td>
        <td>
            &nbsp;</td>
        <td colspan="2">
            <table width="100%">
                <tr>
                    <td width="50%">
                        <asp:DropDownList ID="CmbBatchNo" runat="server" AutoPostBack="True" 
                            Enabled="False" TabIndex="1" Width="80%">
                        </asp:DropDownList>
                        <asp:ImageButton ID="ImgBatch" runat="server" Enabled="False" 
                            ImageAlign="Middle" ImageUrl="~/Images/Node.png" 
                            OnClientClick="return imgBatchDtls_onclick();" />
                    </td>
                    <td width="25">
                        <asp:Label ID="LblCredit" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                    </td>
                    <td width="25%">
                        <asp:Label ID="LblDebit" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
        <asp:Panel ID="Panel1" runat="server">
        </asp:Panel>
        <td>
            &nbsp;</td>
    </tr>
</table>

     <asp:Panel ID="PnlBank" runat="server" Visible="false">
         
         <table class="style1" width="100%">
             <tr>
                 <td width="25%">
                     &nbsp;</td>
                 <td width="50%">
                     &nbsp;</td>
                 <td width="25%">
                     &nbsp;</td>
             </tr>
             <tr>
                   <td width="25%">
                     &nbsp;</td>
                 <td width="50%">
                 <div id="DivChq" runat="server" class="gradientBoxesWithOuterShadows">
                 <legend>Bank Details</legend>
                  <table width="100%">
                    <tr>
                        <td width="2%">
                        </td>
                        <td width="48%">
                        </td>
                        <td width="48%">
                        </td>
                        <td width="2%">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <label>
                                Transfer Type</label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpTranferType" runat="server" Width="100%" 
                                AutoPostBack="True" AppendDataBoundItems="True">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                      <tr>
                          <td>
                              &nbsp;</td>
                          <td>
                              <label>Transaction</label></td>
                          <td>
                              <asp:DropDownList ID="DdlTranType" runat="server" Width="100%" >
                              </asp:DropDownList>
                          </td>
                          <td>
                              &nbsp;</td>
                      </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <label>
                                Bank</label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpBank" runat="server" Width="100%" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <label>
                                Bank Account No</label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpbankAccno" runat="server" Width="100%" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <label>
                                Narration</label>
                        </td>
                        <td>
                           
                            <asp:TextBox ID="TxtCheque" runat="server" Width="97%"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <label>
                                Cheque DATE</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtChequeDate" runat="server" Width="97%" OnChange="validatedate(this);"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtChequeDate">
                            </asp:CalendarExtender>
                        </td>
                        <td>
                        </td>
                    </tr>
                      <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                               <label>Transfer Amount</label> &nbsp;</td>
                            <td>
                                <asp:TextBox ID="TxtBankAmnt" runat="server" onkeypress="return isFloatKey(event);" Width="97%"></asp:TextBox></td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    <div runat="server" id="dvRTGS" style="display: none;">
                      
                        <tr>
                            <td>
                            </td>
                            <td>
                                <label>
                                    IFSC</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtIFSC" runat="server" Width="97%"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <label>
                                    Account No</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtaccno" runat="server" Width="97%"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </div>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <label>
                                Commission Amount</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCommission" runat="server" Width="97%" Text="0" ></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="center" colspan="2">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="center">
                       
                            <%--   <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" />
                   <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />--%>
                        </td>
                        <td>
                             <%--<asp:ImageButton ID="" runat="server" ImageUrl="~/Images/Sav11e.png" OnClientClick="return ValidateSave();"
                                OnClick="CmdSave_Click1" />--%>
                              <%--  <asp:Button ID="Button1" runat="server" Text="OK" OnClientClick="return ValidateSave();"
                                OnClick="CmdSave_Click1"  />--%>
                            &nbsp;</td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="center" colspan="2">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                </div>
                     &nbsp;</td>
                 <td width="25%">
                     &nbsp;</td>
             </tr>
             <tr>
                   <td width="25%">
                     &nbsp;</td>
                 <td width="50%">
                     &nbsp;</td>
                 <td width="25%">
                     &nbsp;</td>
             </tr>
         </table>
         
     </asp:Panel>

            <table border="0" width="100%">
                <tr>
                    <td width="2%">
                    </td>
                    <td width="96%">
                        <asp:GridView ID="DgvTransaction" runat="server" AutoGenerateColumns="False" 
                            TabIndex="32" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="AccNo" HeaderText="AccNo" Visible="False">
                                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                        BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="10%" />
                                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                        Font-Size="Small" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AccHead" HeaderText="AccHead">
                                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                        BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="30%" />
                                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                        Font-Size="Small" />
                                </asp:BoundField>
                                <asp:BoundField DataField="RctCash" HeaderText="Receipt Cash">
                                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                        BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="15%" />
                                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                        Font-Size="Small" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="RctTransfer" HeaderText="Receipt Transfer">
                                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                        BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="10%" />
                                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                        Font-Size="Small" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PayCash" HeaderText="Payment Cash">
                                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                        BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="10%" />
                                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                        Font-Size="Small" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PayTransfer" HeaderText="Payment Transfer">
                                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                        BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="10%" />
                                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                        Font-Size="Small" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Seq_No" HeaderText="Seq_No" Visible="False" />
                            </Columns>
                        </asp:GridView>
                        <td width="2%">
                        </td>
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
                    <td align="center" width="96%">
                        <asp:Label ID="LblWords" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                        <td width="2%">
                            &nbsp;</td>
                    </td>
                </tr>
    </table>

	<br/>
 </asp:Panel>
     
	<table width="100%">
	<tr>
	<td width  = "35%" ></td>
	<td  align="center" class="style3">
            <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                meta:resourcekey="CmdSaveResource1" Width="100px"  />
            <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/images/Close.png" 
                meta:resourcekey="CmdClearResource1" Width="100px" />
    </td>
	<td width = "35%">&nbsp;</td>
	</tr>
	</table>

</div>
</ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>
