<%@ Page EnableEventValidation ="true"  Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="LockerMaster.aspx.vb" Inherits="SMARTCORE_UI.LockerMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="head">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />      
    <script type='text/javascript' src="Scripts/Common.js"></script>  
    <script type='text/javascript' src="Scripts/jquery-1.8.3.js"></script>  
    <link href="CSS/ui-lightness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript' src="Scripts/jquery-ui-1.9.2.custom.min.js"></script>      
    
    <script type='text/javascript'>
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
<script type="text/javascript">
    function onlyAlphabets(event) {
        var Key = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
        if (Key == 13 || (Key >= 48 && Key <= 57)) return false;
        else return true;
    }
    </script>
        
</asp:Content>
<asp:Content ID="Content2" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    
     <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                <ContentTemplate>
     <div class="gradientBoxesWithOuterShadows" style="width: 98%; margin-left:1%; margin-right:1%;margin-top:1%;" >                          
                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                        <Scripts>
                            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
                        </Scripts>
                    </asp:ToolkitScriptManager>
        <legend>Locker Master</legend>                                                   
        <table width="100%">
            <tr>
                <td>
            </td>
            </tr>
            
        </table>
    
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
		<td width = "16%"> &nbsp;</td>
		<td width = "2%"> </td>
		<td width = "15%"> </td>
		<td width = "15%"> </td>
		<td width = "2%"> </td>
	</tr>
	
	<tr>
		<td width = "2%"> </td>
		<td width = "15%"> 
            <asp:Label ID="Lbl_LockerType" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Locker Type"></asp:Label>
        </td>
		<td width = "15%"> 
            <asp:DropDownList ID="Cmb_LockerType" runat="server" TabIndex="25" 
                Width="100%" AutoPostBack="True">
            </asp:DropDownList>
        </td>
		<td width = "2%"> </td>
		<td width = "16%"> 
            <asp:Label ID="Lbl_LockerNo" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Locker No"></asp:Label>
        </td>
		<td width = "16%"> 
            <asp:Panel ID="Panel_Lkr" runat="server">
                <asp:TextBox ID="Txt_Lkr" runat="server"></asp:TextBox>
            </asp:Panel>
            <asp:DropDownList ID="Cmb_LockerNo" runat="server" AutoPostBack="True" 
                CausesValidation="True" TabIndex="25" Width="100%">
            </asp:DropDownList>
        </td>
		<td width = "2%"> &nbsp;</td>
		<td width = "15%"> 
            <asp:Label ID="Lbl_KeyNo" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Key No"></asp:Label>
        </td>
		<td width = "15%"> 
            <asp:TextBox ID="Txt_KeyNo" runat="server" AutoPostBack="True" 
                CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" 
                style="text-align:right" Width="96%"></asp:TextBox>
        </td>
        
		<td width = "2%"> </td>
	</tr>
	
	<tr>
		<td width = "2%"> </td>
		<td width = "15%"> 
            <asp:Label ID="Lbl_RowNo" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Row No"></asp:Label>
        </td>
		<td width = "15%"> 
            <asp:TextBox ID="Txt_RowNo" runat="server" AutoPostBack="True" 
                CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" 
                style="text-align:right" Width="96%"></asp:TextBox>
        </td>
		<td width = "2%"> </td>
		<td width = "16%"> 
            <asp:Label ID="Lbl_ColumnNo" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Column No"></asp:Label>
        </td>
		<td width = "16%"> 
            <asp:TextBox ID="Txt_ColumnNo" runat="server" AutoPostBack="True" 
                CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" 
                style="text-align:right" Width="96%"></asp:TextBox>
        </td>
		<td width = "2%"> </td>
		<td width = "15%"> 
            <asp:Label ID="Lbl_RentAmount" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Rent Amount"></asp:Label>
        </td>
		<td width = "15%"> 
            <asp:TextBox ID="Txt_RentAmount" runat="server" AutoPostBack="True" 
                CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" 
                style="text-align:right" Width="96%"></asp:TextBox>
        </td>
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
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="Ce_OpeningDate" runat="server"  Width="96%" TabIndex="13" OnChange="validatedate(this);"></asp:TextBox>
        </td>
        <td width="2%">
            &nbsp;</td>
        <td width="16%">
            <asp:Label ID="Lbl_EffectiveDate" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Effective Date"></asp:Label>
        </td>
        <td width="16%">
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="Ce_EffectiveDt" runat="server" OnChange="validatedate(this);"  Width="96%"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                TargetControlID="Ce_EffectiveDt">
            </asp:CalendarExtender>
        </td>
        <td width="2%">
            &nbsp;</td>
        <td width="15%">
            <asp:Label ID="Lbl_DueDate" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Due Date"></asp:Label>
        </td>
        <td width="15%">
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="Ce_DueDate" runat="server" OnChange="validatedate(this);" Width="96%"></asp:TextBox>
        </td>
        <td width="2%">
            &nbsp;</td>
    </tr>
    <tr>
        <td width="2%">
            &nbsp;</td>
        <td width="15%">
            <asp:Label ID="Lbl_Period" runat="server" Font-Names="Tahoma" Font-Size="Medium" 
                ForeColor="#6666FF" Text="Period"></asp:Label>
        </td>
        <td width="15%">
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_Period" runat="server" AutoPostBack="True" 
                onkeypress="return isFloatKey(event);" style="text-align:right"  Width="96%"></asp:TextBox>
        </td>
        <td width="2%">
            &nbsp;</td>
        <td width="16%">
            <asp:Label ID="Lbl_RentPaidYears" runat="server" Font-Names="Tahoma" Font-Size="Medium" 
                ForeColor="#6666FF" Text="Rent Paid Years"></asp:Label>
        </td>
        <td width="16%">
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_RentPaidYears" runat="server" AutoPostBack="True" 
                onkeypress="return isFloatKey(event);" style="text-align:right"  Width="96%"></asp:TextBox>
        </td>
        <td width="2%">
            &nbsp;</td>
        <td width="15%">
            <asp:Label ID="Lbl_TotalRent" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Total Rent"></asp:Label>
        </td>
        <td width="15%">
            <asp:TextBox ID="Txt_TotalRent" runat="server" AutoPostBack="True" 
                CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" 
                style="text-align:right" Width="96%"></asp:TextBox>
        </td>
        <td width="2%">
            &nbsp;</td>
    </tr>
    <tr>
        <td width="2%">
            &nbsp;</td>
        <td width="15%">
            <asp:Label ID="Lbl_NoofNominees" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="No of Nominees"></asp:Label>
        </td>
        <td width="15%">
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_NoOfNomine" runat="server"  Width="96%" 
                AutoPostBack="True" onkeypress="return isFloatKey(event);" TabIndex="24"></asp:TextBox>
            <asp:DropDownList ID="CmbNomReq" runat="server" TabIndex="24" Width="100%" 
                AutoPostBack="True">
            </asp:DropDownList>                        
        </td>
        <td width="2%">
                            <asp:ImageButton ID="ImgAdd" runat="server" Height="24px" 
                    ImageUrl="~/Images/Plus.png" Width="23px" Visible="False" />
              </td>
        <td width="16%">
            <asp:Label ID="Lbl_StandingInst" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Standing Instruction"></asp:Label>
        </td>
        <td width="16%">
            <asp:DropDownList ID="Ddl_SI" runat="server" Width="100%" AutoPostBack="True">
                <asp:ListItem Value="N">No</asp:ListItem>
                <asp:ListItem Value="Y">Yes</asp:ListItem>                
            </asp:DropDownList>
        </td>
        <td width="2%">
            &nbsp;</td>
        <td width="15%">
            <asp:Label ID="Lbl_TockenNo" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Tocken No"></asp:Label>
        </td>
        <td width="15%">
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_TockenNo" runat="server"  Width="96%" TabIndex="26"></asp:TextBox>
        </td>
        <td width="2%">
            &nbsp;</td>
    </tr>
    <tr>
        <td width="2%">
            &nbsp;</td>
        <td width="15%">
            <asp:Label ID="Lbl_CanvassedBy" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Canvassed By"></asp:Label>
        </td>
        <td width="15%">
            <asp:DropDownList ID="CmbCanvassedBy" runat="server" TabIndex="25" Width="100%">
            </asp:DropDownList>
        </td>
        <td width="2%">
            &nbsp;</td>
        <td width="16%">
            <asp:Label ID="Lbl_CashTransfer" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Cash/Transfer"></asp:Label>
        </td>
        <td width="16%">
            <asp:DropDownList ID="CmbCashTransfer" runat="server" AutoPostBack="True" 
                Font-Names="Tahoma" Font-Size="Medium" ForeColor="#6666FF" TabIndex="29" 
                Width="100%">
            </asp:DropDownList>
        </td>
        <td width="2%">
            &nbsp;</td>
        <td width="15%"><label>Batch No</label>
            &nbsp;</td>
        <td width="15%">
            <asp:DropDownList ID="CmbBatchNo" runat="server" AutoPostBack="True" 
                TabIndex="1" Width="80%" Enabled="False">
            </asp:DropDownList>
            <asp:ImageButton ID="ImgBatch" runat="server"  
                ImageUrl="~/Images/Node.png" ImageAlign=Middle Enabled="False" 
                OnClientClick="return imgBatchDtls_onclick();"/>

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
            <asp:Label ID="Lbl_StandingInst0" runat="server" Font-Names="Tahoma" 
                Font-Size="Medium" ForeColor="#6666FF" Text="Security Details"></asp:Label>
        </td>
        <td width="16%">
            <asp:DropDownList ID="Ddl_SEC" runat="server" AutoPostBack="True" Width="100%">
                <asp:ListItem Value="N">No</asp:ListItem>
                <asp:ListItem Value="Y">Yes</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td width="2%">
            &nbsp;</td>
        <td width="15%">
        <asp:Label ID="LblCredit" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
            &nbsp;</td>
        <td width="15%">
            <asp:Label ID="LblDebit" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
            &nbsp;</td>
        <td width="2%">
            &nbsp;</td>
    </tr>
</table> 
<div id ="NomDiv" runat="server" visible="false">
        <table width = "100%">
	        <tr>
		        <td width = "2%" > </td>
		        <td width = "15%"> <label>Nominee Name</label></td>
		        <td width = "49%"> <asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_NomName" runat="server" onkeypress="return onlyAlphabets(event);"   Width="99%" TabIndex="30"></asp:TextBox></td>
		        <td width = "2%" > </td>
		        <td width = "15%"> <label>Relation</label></td>
		        <td width = "15%"> 
                    <asp:DropDownList ID="CmbNomRel" runat="server" Width="100%" TabIndex="31">
                    </asp:DropDownList>
                </td>
		        <td width = "2%" > </td>
	        </tr>	
            <tr>
                <td></td>
                <td><label>Nominee Address</label></td>
                <td><asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_NomAdd" runat="server"  Width="99%" TabIndex="32"></asp:TextBox></td>
		        <td></td>
		        <td><label>Phone No</label></td>
		        <td><asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_NomPh" runat="server"  Width="96%" TabIndex="33"></asp:TextBox></td>
                <td></td>
            </tr>
        </table>
</div>    
    <div id="Security" runat="server" visible="false" >
    <table width="100%">
        <tr>
            <td width = "2%"></td>
            <td width = "15%"><label> Branch</label></td>
            <td width = "15%">
                <asp:DropDownList ID="CmbSecBranch" runat="server" Width="100%" TabIndex="31">
                </asp:DropDownList>
            </td>
            <td width = "2%"></td>
            <td width = "16%"><label>&nbsp;Scheme</label></td>
            <td width = "16%">
                <asp:DropDownList ID="CmbSceScheme" runat="server" Width="100%" TabIndex="31">
                </asp:DropDownList>            
            </td>
            <td width = "2%"></td>            
            <td width = "15%"><label>Security Account No</label></td>
            <td width = "15%">
                <asp:TextBox ID="TxtSecAccNo" runat="server" CssClass="twitterStyleTextbox" 
                    TabIndex="33" Width="96%" AutoPostBack="True"></asp:TextBox>
            </td>
            <td width = "2%"></td> 
        </tr>
        <tr>
        <td>        </td>
        <td>        
            <label>
            Account Holder Name</label></td>
        <td>        
            <asp:TextBox ID="TxtAccHolName" runat="server" 
                onkeypress="return onlyAlphabets(event);"  AutoPostBack="True" 
                CssClass="twitterStyleTextbox" TabIndex="33" Width="96%"></asp:TextBox>
            </td>
        <td>        </td>
        <td>        
            <label>
            Account HolderAddress</label></td>
        <td colspan = "3">        
            <asp:TextBox ID="TxtAccHolAddress" runat="server" AutoPostBack="True" 
                CssClass="twitterStyleTextbox" TabIndex="33" Width="96%" 
                TextMode="MultiLine"></asp:TextBox>
            </td>

        <td>        &nbsp;</td>
                
        
        </tr>
    </table>
</div>
<div id="StandDiv" runat="server" visible="false" >
    <table width="100%">
        <tr>
            <td width = "2%"></td>
            <td width = "15%"><label>Standing Branch</label></td>
            <td width = "15%">
                <asp:DropDownList ID="CmbStandBr" runat="server" Width="100%" TabIndex="31">
                </asp:DropDownList>
            </td>
            <td width = "2%"></td>
            <td width = "16%"><label>Standing Scheme</label></td>
            <td width = "16%">
                <asp:DropDownList ID="CmbStandSch" runat="server" Width="100%" TabIndex="31">
                </asp:DropDownList>            
            </td>
            <td width = "2%"></td>            
            <td width = "15%"><label>Standing Account No</label></td>
            <td width = "15%">
                <asp:TextBox ID="TxtStandAcNo" runat="server" CssClass="twitterStyleTextbox" 
                    TabIndex="33" Width="96%" AutoPostBack="True"></asp:TextBox>
            </td>
            <td width = "2%"></td> 
        </tr>
        <tr>
        <td>        </td>
        <td>        
            <label>
            Account Holder Name</label></td>
        <td>        
            <asp:TextBox ID="TxtStandAcHoldenName" runat="server" onkeypress="return onlyAlphabets(event);"  AutoPostBack="True" 
                CssClass="twitterStyleTextbox" TabIndex="33" Width="96%"></asp:TextBox>
            </td>
        <td>        </td>
        <td>        
            <label>
            Account Holder Name</label></td>
        <td colspan = "3">        
            <asp:TextBox ID="TxtStandAddress" runat="server" AutoPostBack="True" 
                CssClass="twitterStyleTextbox" TabIndex="33" Width="96%"></asp:TextBox>
            </td>

        <td>        </td>
                
        
        </tr>
    </table>
</div>   

<div id="DivMultipleDep" runat="server" visible="false">
            <table width = "100%">
	        <tr>
		        <td width = "2%"> </td>
		        <td width = "15%"> <label>Month</label> </td>
		        <td width = "15%"> 
                    <asp:TextBox ID="Txt_Mth" runat="server" AutoPostBack="True" 
                        CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" 
                        style="text-align:right" Width="96%"></asp:TextBox>
                </td>
		        <td width = "2%"> </td>
		        <td width = "16%"> <label>Days</label> </td>
		        <td width = "16%"> 
                    <asp:TextBox ID="Txt_Dys" runat="server" AutoPostBack="True" 
                        CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" 
                        style="text-align:right" Width="96%"></asp:TextBox>
                </td>
		        <td width = "2%"> </td>
		        <td width = "15%"> <label>Due Date</label> </td>
		        <td width = "15%"> 
                    <asp:TextBox ID="Ce_DueDt" runat="server" OnChange="validatedate(this);" CssClass="twitterStyleTextbox" 
                        Width="96%"></asp:TextBox>
                </td>
		        <td width = "2%"> </td>
	        </tr>
	        <tr>
		        <td width = "2%"> </td>
		        <td width = "15%"> <label>Deposit Amt</label> </td>
		        <td width = "15%"> 
                    <asp:TextBox ID="Txt_DepAmt" runat="server" AutoPostBack="True" 
                        CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" 
                        style="text-align:right" Width="96%"></asp:TextBox>
                </td>
		        <td width = "2%"> </td>
		        <td width = "16%"> <label>Interest %</label> </td>
		        <td width = "16%"> 
                    <asp:TextBox ID="Txt_IntRate" runat="server" CssClass="twitterStyleTextbox" 
                        onkeypress="return isFloatKey(event);" style="text-align:right" 
                        Width="96%"></asp:TextBox>
                </td>
		        <td width = "2%"> </td>
		        <td width = "15%"> <label>Int Pay Mode</label> </td>
		        <td width = "15%"> 
		            <table width = "100%">
		                <tr>
		                    <td width = "74%">
                                <asp:DropDownList ID="CmbIntPayMode" runat="server" Width="100%">
                                </asp:DropDownList>
                            </td>
		                    <td width = "2%"></td>
		                    <td width = "24%">
                                <asp:ImageButton ID="CmdAdd" runat="server" Height="21px" 
                                    ImageUrl="~/Images/AddBtn.png" TabIndex="25" />
                            </td>
		                   
		                </tr>
		            </table>
                </td>
		        <td width = "2%"> </td>
	        </tr>
            </table>            
                <table border="0" width="100%">
                    <tr>
                        <td width="2%"></td>
                        <td width="96%">
                            <asp:GridView ID="DgvDepDtls" runat="server" AutoGenerateColumns="False" 
                                TabIndex="32" Width="100%">
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
                                        <HeaderStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="12%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Dep_NomAdd" HeaderText="Address" >
                                        <HeaderStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Small" ForeColor="White"  Width="23%" />
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
                                </Columns>
                            </asp:GridView>
                        <td width="2%"></td>
                    </tr>
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
	<table width="100%">
	    <tr>
	    <td width ="25%"></td>
	    <td width ="50%" align="center">
                <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                    meta:resourcekey="CmdSaveResource1" TabIndex="34" 
                    OnClientClick="return ValidateSave();"  />
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
            </td>
            <td>
                &nbsp;</td>
            <td>
                <asp:Label ID="Lbl_MaturityAmt" runat="server" Font-Names="Tahoma" 
                    Font-Size="Medium" ForeColor="#6666FF" Text="Maturity Amount" Visible="False"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Txt_MaturityAmt" runat="server" CssClass="twitterStyleTextbox" 
                    TabIndex="28" Visible="False" Width="96%"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="SecAccid" runat="server" />
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </div>
    </div>
                </ContentTemplate>
    </asp:UpdatePanel>
            </asp:Content>
            


