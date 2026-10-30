<%@ Page EnableEventValidation ="true"  Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="DepositMaster.aspx.vb" Inherits="SMARTCORE_UI.DepositMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="head">
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
                        alert('Invalid date format! Please enter a valid date!');

                        return false;
                    }
                }
                if (mm == 2) {
                    var lyear = false;
                    if ((!(yy % 4) && yy % 100) || !(yy % 400)) {
                        lyear = true;
                    }
                    if ((lyear == false) && (dd >= 29)) {
                        alert('Invalid date format! Please enter a valid date!');

                        return false;
                    }
                    if ((lyear == true) && (dd > 29)) {
                        alert('Invalid date format! Please enter a valid date!');

                        return false;
                    }
                }
            }
            else {
                alert("Invalid date format! Please enter a valid date!");

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
            width: 25%;
        }
        .style3
        {
            width: 25%;
            height: 21px;
        }
        .style4
        {
            height: 21px;
        }
    </style>
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




                     function Save() {

                         var r = confirm("Do You Want to Save ? ");
                         if (r == true) {
                             r = true;
                         }
                         else {
                             r = false;
                         }

                         return r;
                     }
     function ValidateDELETE() {
           
            var r = confirm("Do You Want to Delete ? ");
            if (r == true) {
                return true;
            }
            else {
                return false;
            }



            return true;
        }
        
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
        function textboxMultilineMaxNumber(txt, maxLen) {

            if (txt.value.length > maxLen - 1) {

                alertify.error("Maximum No Of Words Limit Reached");
            }
            else {
                return true;
            }
        }
</script>  
   
                 
                
<div  style="width: 98%; margin-left:1%; margin-right:1%;margin-top:1%;" >    
     
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
<table width="100%" style=" float:left;">
  <tr>
    <td width="2%"></td>
    <td width="20%"><label>Branch</label></td>
    <td width="27%" style="margin-left: 80px">
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
                                <asp:GridView ID="DgvCustomer" runat="server" AutoGenerateColumns="False" CssClass="grid-viewNew" 
                                    DataKeyNames="CustID" TabIndex="23" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="MemDtls" HeaderText="Member Details">
                                            
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MemBrCode" HeaderText="Member BrCode" 
                                            Visible="False" />
                                        <asp:BoundField DataField="MemSchCode" HeaderText="MemSchCode" 
                                            Visible="False" />
                                        <asp:BoundField DataField="MemNo" HeaderText="Member No" Visible="False">
                                           
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CustID" HeaderText="Customer ID">
                                           
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CustName" DataFormatString="{0:F2}" 
                                            HeaderText="Name">
                                            
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Relation" HeaderText="Relation">
                                            
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CustAdd" HeaderText="Address">
                                            
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
                                           
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Age" HeaderText="Age">
                                            
                                        </asp:BoundField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#F5F4FF" />
                                </asp:GridView>
                            </td>
                            <td width="2%">
                            </td>
                        </tr>
                    </table>
        
    
</asp:Panel>    
  <asp:Panel ID="PnlAuthorise3" runat="server" >
      
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
             </td>
        <td width="15%">
            <asp:Label ID="Lbl_OpeningDate" runat="server" CssClass="lblCSS"
                 Text="Opening Date"></asp:Label>
        </td>
        <td width="15%">
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="Ce_OpeningDate" runat="server"  Width="96%" TabIndex="13"></asp:TextBox>
        </td>
        <td width="2%">
             </td>
        <td width="16%">
            <asp:Label ID="Lbl_EffectiveDate" runat="server"  CssClass="lblCSS"
                 Text="Effective Date"></asp:Label>
        </td>
        <td width="16%">
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="Ce_EffectiveDt" runat="server"  Width="96%" TabIndex="14"  AutoPostBack="true"   ></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                TargetControlID="Ce_EffectiveDt">
            </asp:CalendarExtender>
        </td>
        <td width="2%">
             </td>
        <td width="15%">
            <asp:Label ID="Lbl_DueDate" runat="server"  
               CssClass="lblCSS" Text="Due Date"></asp:Label>
        </td>
        <td width="15%">
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="Ce_DueDate" runat="server"  Width="96%" TabIndex="15"></asp:TextBox>
        </td>
        <td width="2%">
             </td>
    </tr>

     <asp:Panel ID="PnlAuthorise4" runat="server" >
    <tr>
        <td width="2%">
             </td>
        <td width="15%">
            <asp:Label ID="Lbl_Month" runat="server" CssClass="lblCSS"
                 Text="Month"></asp:Label>
        </td>
        <td width="15%">
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_Month" runat="server" AutoPostBack="True" 
                onkeypress="return isFloatKey(event);" style="text-align:right"  
                Width="96%" MaxLength="3" TabIndex="16"></asp:TextBox>
        </td>
        <td width="2%">
             </td>
        <td width="16%">
            <asp:Label ID="Lbl_Days" runat="server"
                CssClass="lblCSS" Text="Days"></asp:Label>
        </td>
        <td width="16%">
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_Days" runat="server" AutoPostBack="True" 
                onkeypress="return isFloatKey(event);" style="text-align:right"  
                Width="96%" MaxLength="3" TabIndex="17"></asp:TextBox>
        </td>
        <td width="2%">
             </td>
        <td width="15%">
            <asp:Label ID="Lbl_IntPayMode" runat="server" CssClass="lblCSS" 
                 Text="Int Pay Mode"></asp:Label>
        </td>
        <td width="15%">
            <asp:DropDownList ID="CmbInterestPayMode" runat="server"
                Width="100%" TabIndex="18" AutoPostBack="true">
            </asp:DropDownList>
        </td>
        <td width="2%">
             </td>
    </tr>
    <tr>
        <td width="2%">
             </td>
        <td width="15%">
            <asp:Label ID="Lbl_InitialDeposit" runat="server" CssClass="lblCSS" 
                Text="Initial Deposit"></asp:Label>
        </td>
        <td width="15%">
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_InitialDeposit" runat="server" AutoPostBack="True" 
                onkeypress="return isFloatKey(event);"  style="text-align:right"  Width="96%" TabIndex="19"></asp:TextBox>
        </td>
        <td width="2%">
             </td>
        <td width="16%">
            <asp:Label ID="Lbl_InterestPer" runat="server" CssClass="lblCSS"
                 Text="Interest %"></asp:Label>
        </td>
        <td width="16%">
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_IntPercent" runat="server" AutoPostBack="true" 
                onkeypress="return isFloatKey(event);" style="text-align:right" TabIndex="20" Width="96%"></asp:TextBox>
        </td>
        <td width="2%">
             </td>
        <td width="15%">
            <asp:Label ID="Lbl_Agent" runat="server" CssClass="lblCSS" 
                Text="Agent"></asp:Label>
        </td>
        <td width="15%">
            <asp:DropDownList ID="CmbAgent" runat="server" TabIndex="23" Width="100%">
            </asp:DropDownList>
        </td>
        <td width="2%">
             </td>
    </tr>
</asp:Panel>

    <tr>
        <td width="2%">
             </td>
        <td width="15%">
            <asp:Label ID="Lbl_NoofNominees" runat="server" CssClass="lblCSS"
                 Text="No of Nominees"></asp:Label>
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
            <asp:Label ID="Lbl_StandingInst" runat="server" 
                CssClass="lblCSS" Text="Standing Instruction"></asp:Label>
        </td>
        <td width="16%">
            <asp:DropDownList ID="Ddl_SI" runat="server" Width="100%" AutoPostBack="True" TabIndex="25">
                <asp:ListItem Value="N">No</asp:ListItem>
                <asp:ListItem Value="Y">Yes</asp:ListItem>                
            </asp:DropDownList>
        </td>
        <td width="2%">
             </td>
        <td width="15%">
            <asp:Label ID="Lbl_MaturityAmt" runat="server" CssClass="lblCSS" 
                 Text="Maturity Amount"></asp:Label>        
        </td>
        <td width="15%">
            <asp:TextBox ID="Txt_MaturityAmt" runat="server" CssClass="twitterStyleTextbox"  style="text-align:right"
                    TabIndex="27" Width="96%"></asp:TextBox>
        </td>
        <td width="2%">
             </td>
    </tr>


    <tr>
        <td width="2%">
             </td>
        <td width="15%">
            <asp:Label ID="Lbl_CanvassedBy" runat="server" CssClass="lblCSS" 
                 Text="Canvassed By"></asp:Label>
        </td>
        <td width="15%">
            <asp:DropDownList ID="CmbCanvassedBy" runat="server" TabIndex="28" Width="100%">
            </asp:DropDownList>
        </td>
        <td width="2%">
             </td>
        <td width="16%">
            <asp:Label ID="Lbl_StandingInst0" runat="server"  CssClass="lblCSS"
                 Text="Split Amount"></asp:Label>
        </td>
        <td width="16%">
            <asp:DropDownList ID="Ddl_Split" runat="server" AutoPostBack="True" 
                TabIndex="28" Width="100%">
                <asp:ListItem Value="N">No</asp:ListItem>
                <asp:ListItem Value="Y">Yes</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td width="2%">
             </td>
        <td width="15%">
            <asp:Label ID="Lbl_CashTransfer" runat="server" CssClass="lblCSS"
                 Text="Cash/Transfer"></asp:Label>
        </td>
        <td width="15%">
            <asp:DropDownList ID="CmbCashTransfer" runat="server" AutoPostBack="True" 
                Font-Names="Tahoma" Font-Size="Medium" ForeColor="#6666FF"
                 Width="100%" TabIndex="29">
            </asp:DropDownList>
        </td>
        <td width="2%">
             </td>
    </tr>
    <tr>
        <td width="2%">
             </td>
        <td width="15%">
            <asp:Label ID="Lbl_Remarks" runat="server"  
               CssClass="lblCSS" Text="Remarks"></asp:Label>
        </td>
        <td width="15%">
            <asp:TextBox ID="Txt_Remarks" runat="server" AutoPostBack="True" onkeypress="return textboxMultilineMaxNumber(this,100);"
                CssClass="twitterStyleTextbox" TabIndex="32" Width="99%"></asp:TextBox>
        </td>
        <td width="2%">
             </td>
        <td width="16%"><label>Batch No</label>
             </td>
        <td width="16%">
            <asp:DropDownList ID="CmbBatchNo" runat="server" AutoPostBack="True" 
                TabIndex="1" Width="80%" Enabled="False">
            </asp:DropDownList>
            <asp:ImageButton ID="ImgBatch" runat="server"  
                ImageUrl="~/Images/Node.png" ImageAlign="Middle" Enabled="False" 
                OnClientClick="return imgBatchDtls_onclick();"/>
             </td>
        <td width="2%">
             </td>
        <td width="15%">
            <asp:Label ID="LblCredit" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
             </td>
        <td width="15%">
                <asp:Label ID="LblDebit" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
             </td>
        <td width="2%">
             </td>
    </tr>
    <tr>
        <td width="2%">
             </td>
        <td width="15%">
            <asp:Label ID="Label2" runat="server" CssClass="lblCSS" 
                 Text="Canvassed Branch"></asp:Label>
             </td>
        <td width="15%">
            <asp:DropDownList ID="DrpCanvassedBranch" runat="server" TabIndex="28" Width="100%">
            </asp:DropDownList>
             </td>
        <td width="2%">
             </td>
        <td width="16%">
             </td>
        <td width="16%">
             </td>
        <td width="2%">
             </td>
        <td width="15%">
             </td>
        <td width="15%">
            <label>Bank Details</label> <asp:CheckBox ID="CkBankDetails" runat="server" enabled="false" 
                AutoPostBack="True"/></td>
        <td width="2%">
             </td>
    </tr>
</table>

 </asp:Panel>  

 <asp:Panel ID="PnlAuthorise2" runat="server" Visible="false">      

<table id="dvDebenture" width = "100%">
   <tr>
        <td width="2%">
             </td>
        <td width="15%">
             <asp:Label ID="Lbl_Noofdeb" runat="server" CssClass="lblCSS" 
                 Text="No of Debenture"></asp:Label>
             </td>
        <td width="15%"><asp:TextBox  CssClass="twitterStyleTextbox" ID="txtNoDebenture" runat="server"  Width="96%" 
                AutoPostBack="True" onkeypress="return isFloatKey(event);" TabIndex="24"></asp:TextBox>
             </td>
        <td width="2%">
             </td>
        <td width="16%">
             <asp:Label ID="Lbl_distinctiveno" runat="server" CssClass="lblCSS" 
                 Text="Distictive No"></asp:Label>
             </td>
        <td width="16%"><asp:TextBox  CssClass="twitterStyleTextbox" ID="txtDistictiveNo" runat="server"  Width="96%" 
                AutoPostBack="True" onkeypress="return isFloatKey(event);" TabIndex="24"></asp:TextBox>
             </td>
        <td width="2%">
             </td>
        <td width="15%">
        <label>Deb Series</label>
            &nbsp;</td>
        <td width="15%">
             <asp:DropDownList ID="DrpDebSeries" runat="server" Width="98%">
             </asp:DropDownList>
             </td>
        <td width="2%">
             </td>
    </tr>
    <tr>
        <td width="2%">
             </td>
        <td width="15%">
             <asp:Label ID="LblRes" runat="server" CssClass="lblCSS" 
                 Text="Res.No"></asp:Label>
             </td>
        <td width="15%"><asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtResNo" runat="server"   Width="96%" 
                TabIndex="25"></asp:TextBox>
             </td>
        <td width="2%">
             </td>
        <td width="16%">
             <asp:Label ID="LblResDt" runat="server" CssClass="lblCSS" 
                 Text="Res.Date"></asp:Label>
             </td>
        <td width="16%">
            <asp:TextBox ID="TxtResDt" CssClass="twitterStyleTextbox" runat="server" OnChange="validatedate(this);" Width="96%"></asp:TextBox>
            <asp:CalendarExtender ID="TxtResDt_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                TargetControlID="TxtResDt">
            </asp:CalendarExtender>
             </td>
        <td width="2%">
             </td>
        <td width="15%">
             </td>
        <td width="15%">
             </td>
        <td width="2%">
             </td>
    </tr>
</table> 

</asp:Panel>
  
    
    <div id="DivBankDet"  runat="server" visible="false">
        <table class="style1" width="100%">
            <tr>
                <td class="style3">
                    </td>
                <td width="50%" class="style4">
                    </td>
                <td width="25%" class="style4">
                    </td>
            </tr>
            <tr>
                <td class="style2">
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
                              <asp:DropDownList ID="DdlTranType" runat="server" Width="100%">
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
                 <td class="style2">
                    &nbsp;</td>
                <td width="50%">
                    &nbsp;</td>
                <td width="25%">
                    &nbsp;</td>
            </tr>
        </table>
        </div>
  
<asp:Panel ID="PnlAuthorise1" runat="server">

<div id ="NomDiv" class="gradientBoxesWithOuterShadows" runat="server" visible="false" style=" width:97%; float:left; margin-left:2%;margin-right:2%;margin-bottom:2%;margin-top:2%;">
        <legend>NOMINEE DETAILS</legend>
        <table width = "100%" style="float:left;">
	        <tr>
		        <td width = "2%" > </td>
		        <td width = "15%"> <label>Nominee Name</label></td>
		        <td width = "49%"> <asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_NomName" runat="server"  Width="99%" TabIndex="30" onkeypress="return textboxMultilineMaxNumber(this,200);"></asp:TextBox></td>
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
                <td><asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_NomAdd" onkeypress="return textboxMultilineMaxNumber(this,200);" runat="server"  Width="99%" TabIndex="32"></asp:TextBox></td>
		        <td></td>
		        <td><label>Phone No</label></td>
		        <td><asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_NomPh" runat="server" onkeypress="return textboxMultilineMaxNumber(this,15);" Width="96%" TabIndex="33"></asp:TextBox></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
		        <td></td>
		        <td></td>
		        <td></td>
                <td></td>
            </tr>
        </table>
</div>    
<div id="StandDiv" class="gradientBoxesWithOuterShadows" runat="server" visible="false" style=" float:left; width:96%; margin-left:2%;margin-bottom:2%;margin-top:2%;">
    <legend>STANDING DETAILS</legend>
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
                <asp:TextBox ID="TxtStandAcNo" runat="server" CssClass="twitterStyleTextbox" onkeypress="return textboxMultilineMaxNumber(this,18);"
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
            <asp:TextBox ID="TxtStandAcHoldenName" runat="server" AutoPostBack="True" 
               onkeypress="return textboxMultilineMaxNumber(this,200);" CssClass="twitterStyleTextbox" TabIndex="33" Width="96%"></asp:TextBox>
            </td>
        <td>        </td>
        <td>        
            <label>
            Account Holder Address</label></td>
        <td colspan = "4">        
            <asp:TextBox ID="TxtStandAddress" runat="server" AutoPostBack="True" onkeypress="return textboxMultilineMaxNumber(this,200);"
                CssClass="twitterStyleTextbox" TabIndex="33" Width="98%"></asp:TextBox>
            </td>
      
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td colspan = "3"></td>
            <td></td>
        </tr>
    </table>
</div>   

<div id="DivMultipleDep" class="gradientBoxesWithOuterShadows" runat="server" visible="false" style=" float:left; width:96%; margin-left:2%;margin-top:2%;">
        <legend>SPLIT A/cs</legend>
            <table width = "100%"  style="float:left;">
                <tr>
		        <td width = "2%"> </td>
		        <td width = "15%"> <label>Split</label> </td>
		        <td width = "15%"> 
                    <asp:TextBox ID="txtNoofSplit" runat="server" 
                        CssClass="twitterStyleTextbox" onkeypress="return isNumberKey(event);"
                        style="text-align:right" Width="96%"></asp:TextBox>
                </td>
		        <td width = "2%"> </td>
		        <td width = "16%">  </td>
		        <td width = "16%"> 
                   
                </td>
		        <td width = "2%"> </td>
		        <td width = "15%">  </td>
		        <td width = "15%"> 
                   
                </td>
		        <td width = "2%"> </td>
	        </tr>
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
                    <asp:TextBox ID="Ce_DueDt" runat="server" CssClass="twitterStyleTextbox" 
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
                        Width="96%" Readonly="true" ></asp:TextBox>
                </td>
		        <td width = "2%"> </td>
		        <td width = "15%"> <label>Int Pay Mode</label> </td>
		        <td width = "15%"> 
		            <table width = "100%">
		                <tr>
		                    <td width = "74%">
                                <asp:DropDownList ID="CmbIntPayMode" runat="server" Width="100%" 
                                    >
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
                <table  width="100%">
                    <tr>
                        <td width="2%"></td>
                        <td width="96%">
                            <asp:GridView ID="DgvDepDtls" runat="server" AutoGenerateColumns="False"  CssClass="grid-viewNew"
                                TabIndex="32" Width="100%" DataKeyNames="SlNo">
                                <Columns>
                                    <asp:BoundField DataField="Dep_Mth" HeaderText="Month">
                                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Dep_Days" HeaderText="Days">
                                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Dep_DueDt" HeaderText="Due Date">
                                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Dep_Amt" HeaderText="Amount">
                                       
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Dep_IntRate" HeaderText="Int Rate">
                                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Dep_MatureAmt" HeaderText="Maturity Amt">
                                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Dep_IntPayMode" HeaderText="Int Pay Mode">
                                       
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Dep_NomNm" HeaderText="Nominee" >
                                         </asp:BoundField>
                                    <asp:BoundField DataField="Dep_NomRel" HeaderText="Relation" >
                                       </asp:BoundField>
                                    <asp:BoundField DataField="Dep_NomAdd" HeaderText="Address" >
                                         </asp:BoundField>
                                    <asp:BoundField DataField="Dep_Standing" HeaderText="Standing Instr" >
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
                            </td>
                        <td width="2%"></td>
                    </tr>
                    
                </table>                    
        </div>

        <table border="0" width="100%">
        <tr>
            <td width="2%">
            </td>
            <td width="96%">
                <asp:GridView ID="DgvTransaction" runat="server" AutoGenerateColumns="False"  CssClass="grid-viewNew"
                    TabIndex="32" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="AccNo" HeaderText="AccNo" Visible="False">
                            
                        </asp:BoundField>
                        <asp:BoundField DataField="AccHead" HeaderText="AccHead">
                           
                        </asp:BoundField>
                        <asp:BoundField DataField="RctCash" HeaderText="Receipt Cash">
                           
                        </asp:BoundField>                       
                           <asp:BoundField DataField="RctTransfer" HeaderText="Receipt Transfer">
                           
                        </asp:BoundField>
                        <asp:BoundField DataField="PayCash" HeaderText="Payment Cash">
                            
                        </asp:BoundField>
                        <asp:BoundField DataField="PayTransfer" HeaderText="Payment Transfer">
                           
                        </asp:BoundField>
                        <asp:BoundField DataField="Seq_No" HeaderText="Seq_No" Visible="False" />
                    </Columns>
                </asp:GridView>

            <td width="2%">
            </td>
        </tr>
            <tr>
                <td width="2%">
                     </td>
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
                     </td>
            </tr>
            <tr>
                <td width="2%">
                     </td>
                <td width="96%" align="center">
                    <asp:Label ID="LblWords" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                    <td width="2%">
                         </td>
                </td>
            </tr>
    </table>
    
    </asp:Panel>
    
	<table width="100%">
	    <tr>
	    <td width ="25%"></td>
	    <td width ="50%" align="center">
                <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                    meta:resourcekey="CmdSaveResource1" TabIndex="34" 
                    onclientclick="return Save(); ComboSelect();"  />
                <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/images/Close.png" 
                    meta:resourcekey="CmdClearResource1" TabIndex="35"   />
                    	<asp:ImageButton ID="cmdDelete" runat="server" ImageUrl="~/images/DELETE.png" Visibl="false"
                meta:resourcekey="CmdSaveResource1" TabIndex="29" OnClientClick="return ValidateDELETE();"  />
        </td>
	    <td width ="25%">  </td>
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
                 <asp:HiddenField ID="HdnTrAccId" runat="server" Value="" />
                 </td>
            <td>
                <asp:Label ID="Lbl_TockenNo" runat="server" Font-Names="Tahoma" 
                    Font-Size="Medium" ForeColor="#6666FF" Text="Tocken No" Visible="False"></asp:Label>
            </td>
            <td>
                <asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_TockenNo" runat="server"  Width="96%" TabIndex="26" Visible="false"></asp:TextBox>                 
            </td>
            <td>
                 </td>
            <td>
                 </td>
        </tr>
        <tr>
            <td>
                 <asp:HiddenField ID="SchType6" runat="server" />
                 </td>
            <td>
                 </td>
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