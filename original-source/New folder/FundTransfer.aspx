<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="FundTransfer.aspx.vb" Inherits="SMARTCORE_UI.FundTransfer" 
    title="Fund Transfer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
    <link href="CSS/ui-lightness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript' src="Scripts/Common.js"></script>
    <script src="Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script type='text/javascript' src="Scripts/jquery-ui-1.9.2.custom.min.js"></script>      
    <script type="text/javascript">
       
      
        function transactionpopup(url,Title)
        {
            
            $('#divEffect').append('<div id="resultdiv" title="'+Title+'" visible="true" >');
          //  alert('hi1');
            $("#resultdiv").load(url+"?AccID=" + document.getElementById('<%=AccID.ClientID%>').value, {}, function (data) {
               $("#resultdiv").show();
           });

           $("#resultdiv").dialog({
               autoOpen: false,
               height: 600,
               width: 900,
               //beforeClose: function(event, ui) { $('#resultdiv').remove(); },
               close: function (event, ui) { $(this).dialog("destroy"); },
               modal: true
           });
           $('#resultdiv').dialog('open');
        }
        function imgChequeStatus_onclick() {
            $('#divEffect').append('<div id="resultdiv" title="Cheque Status" visible="false" >');
            $("#resultdiv").load("ChequeIssueDtls.aspx?AccID=" + document.getElementById('<%=AccID.ClientID%>').value, {}, function(data) {
                $("#resultdiv").show();
            });

            $("#resultdiv").dialog({
                autoOpen: false,
                height: 600,
                width: 1200,
                //beforeClose: function(event, ui) { $('#resultdiv').remove(); },
                close: function(event, ui) { $(this).dialog("destroy"); },
                modal: true
            });
            $('#resultdiv').dialog('open');
        }


        function imgSecurityDetails_onclick() {
            $('#divEffect').append('<div id="resultdiv" title="Security details" visible="false" >');
            $("#resultdiv").load("ChittySuertySecurity.aspx?AccID=" + document.getElementById('<%=AccID.ClientID%>').value, {}, function(data) {
                $("#resultdiv").show();
            });

            $("#resultdiv").dialog({
                autoOpen: false,
                height: 600,
                width: 1200,
                //beforeClose: function(event, ui) { $('#resultdiv').remove(); },
                close: function(event, ui) { $(this).dialog("destroy"); },
                modal: true
            });
            $('#resultdiv').dialog('open');
        }
       
         function imgTranList_onclick() {
             alert('hi');
             $('#divEffect').append('<div id="resultdiv" title="Transaction" visible="false" >');
             $("#resultdiv").load("TranDtls.aspx?AccID="+document.getElementById('<%=AccID.ClientID%>').value, {}, function(data) {
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
         function imgTranNominee_onclick() {
             $('#divEffect').append('<div id="resultdiv" title="Nominee" visible="false" >');
             $("#resultdiv").load("NomDtls.aspx?AccID="+document.getElementById('<%=AccID.ClientID%>').value, {}, function(data) {
                 $("#resultdiv").show();
             });
             $("#resultdiv").dialog({
                 autoOpen: false,
                 height: 300,
                 width: 800,
                 //beforeClose: function(event, ui) { $('#resultdiv').remove(); },
                 close: function(event, ui) { $(this).dialog("destroy"); },
                 modal: true
             });
             $('#resultdiv').dialog('open');

         }
         function imgTranJoint_onclick() {
             $('#divEffect').append('<div id="resultdiv" title="Joint Account" visible="false" >');
             $("#resultdiv").load("JointAcc.aspx?AccID="+document.getElementById('<%=AccID.ClientID%>').value, {}, function(data) {
                 $("#resultdiv").show();
             });
             $("#resultdiv").dialog({
                 autoOpen: false,
                 height: 300,
                 width: 1000,
                 //beforeClose: function(event, ui) { $('#resultdiv').remove(); },
                 close: function(event, ui) { $(this).dialog("destroy"); },
                 modal: true
             });
             $('#resultdiv').dialog('open');
         }
         
         function imgTranImage_onclick() {
             $('#divEffect').append('<div id="resultdiv" title="Customer Image" visible="false" >');
             $("#resultdiv").load("CustImage.aspx?CustID=" + document.getElementById('<%=HdnCustId.ClientID%>').value, {}, function(data) {
                 $("#resultdiv").show();
             });
             $("#resultdiv").dialog({
                 autoOpen: false,
                 height: 600,
                 width: 750,
                 //beforeClose: function(event, ui) { $('#resultdiv').remove(); },
                 close: function(event, ui) { $(this).dialog("destroy"); },
                 modal: true
             });
             $('#resultdiv').dialog('open');
         }
         function Ornaments_onclick() {

             $('#divEffect').append('<div id="resultdiv" title="ORNAMENTS PLEDGED" visible="false" >');
             $("#resultdiv").load("GoldItems.aspx?AccID=" + document.getElementById('<%=AccID.ClientID%>').value, {}, function(data) {
                 $("#resultdiv").show();
             });
             $("#resultdiv").dialog({
                 autoOpen: false,
                 height: 400,
                 width: 1000,
                 //beforeClose: function(event, ui) { $('#resultdiv').remove(); },
                 close: function(event, ui) { $(this).dialog("destroy"); },
                 modal: true
             });
             $('#resultdiv').dialog('open');
         }

         function ActionTaken_onclick() {

             $('#divEffect').append('<div id="resultdiv" title="ACTION TAKEN" visible="false" >');
             $("#resultdiv").load("LoanAction.aspx?AccID=" + document.getElementById('<%=AccID.ClientID%>').value, {}, function(data) {
                 $("#resultdiv").show();
             });
             $("#resultdiv").dialog({
                 autoOpen: false,
                 height: 400,
                 width: 1000,
                 //beforeClose: function(event, ui) { $('#resultdiv').remove(); },
                 close: function(event, ui) { $(this).dialog("destroy"); },
                 modal: true
             });
             $('#resultdiv').dialog('open');
         }
         function imgLiab_onclick() {

             $('#divEffect').append('<div id="resultdiv" title="Customer Asset Liablities" visible="false" >');
             $("#resultdiv").load("AssetLiab.aspx?CustID=" + document.getElementById('<%=HdnCustId.ClientID%>').value, {}, function(data) {
                 $("#resultdiv").show();
             });
             $("#resultdiv").dialog({
                 autoOpen: false,
                 height: 600,
                 width: 1200,
                 //beforeClose: function(event, ui) { $('#resultdiv').remove(); },
                 close: function(event, ui) { $(this).dialog("destroy"); },
                 modal: true
             });
             $('#resultdiv').dialog('open');
         }

         function imgTranDtls_onclick() {
             var Section = document.getElementById('<%=SchSection.ClientID%>').value;

             if (Section == "LOAN") {
                 $('#divEffect').append('<div id="resultdiv" title="Details" visible="false" >');
                 $("#resultdiv").load("LoanTranDtls.aspx?AccID=" + document.getElementById('<%=AccID.ClientID%>').value, {}, function(data) {
                     $("#resultdiv").show();
                 });
                 $("#resultdiv").dialog({
                     autoOpen: false,
                     height: 500,
                     width: 450,
                     //beforeClose: function(event, ui) { $('#resultdiv').remove(); },
                     modal: true,
                     close: function(event, ui) { $(this).dialog("destroy"); }
                 });
                 $('#resultdiv').dialog('open');

             }
             if (Section == "GROUP DEPOSIT") {
                 $('#divEffect').append('<div id="resultdiv" title="Details" visible="false" >');
                 $("#resultdiv").load("ChiityDtls.aspx?AccID=" + document.getElementById('<%=AccID.ClientID%>').value, {}, function(data) {
                     $("#resultdiv").show();
                 });
                 $("#resultdiv").dialog({
                     autoOpen: false,
                     height: 500,
                     width: 850,
                     //beforeClose: function(event, ui) { $('#resultdiv').remove(); },
                     close: function(event, ui) { $(this).dialog("destroy"); },
                     modal: true
                 });
                 $('#resultdiv').dialog('open');

             }
         }

         function divclose_LoanTranDtls(tot) {
             $("#resultdiv").html('');
             $("#resultdiv").dialog("destroy").remove();
             return true
         }

         function imgBatchDtls_onclick() {
             $('#divEffect').append('<div id="resultdiv" title="Batch Details" visible="false" >');
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
    
    <style type="text/css">
        .listMain
{
    background-image:url('images/wordWheelBack.gif');
    background-repeat:repeat-x;
    background-color:#FFFFFF;
    z-index:1000;
    width:795px !important;
    max-height:310px;
    list-style:none;
    overflow-y:scroll;
    scrollbar-arrow-color:#B89020;
    scrollbar-base-color:#8E6E1C;
    scrollbar-face-color:#B6C5D4;
    scrollbar-3dlight-color:#8E6E1C;
    scrollbar-highlight-color:#EED47D;
    scrollbar-shadow-color:#959595;
    scrollbar-darkshadow-color:#00337E;
    margin-left:0px;
    border-bottom:1px solid #B5C6D4;
    border-left:1px solid #B5C6D4;
    margin-top:0px;
    font-family : Courier New ;
    font-size:small;
    text-align : left;   
}
.wordWheel .itemsMain
{
    background:none;
    border-collapse:collapse;
    color:#00337E;
    white-space:nowrap;
    font-family : Courier New ;
    font-size:small;
    text-align : left;
}
.wordWheel .itemsSelected
{
    background-color:#3366CC;  
    color:Snow;  
    font-family : Courier New ;
    font-size:small;
    text-align : left;
}
.TitleBar {
    background-color: #33F;
    border-top-color: #39F;
    border-right-color: #39F;
    border-bottom-color: #39F;
    border-left-color: #39F;
    padding: 2px;
    font-size: 12px;
    font-weight: bold;
    color: #FFF;
    text-decoration: blink;
}
.floatingDiv
{
  position:absolute;         
  top: 630px;
  box-shadow: 10px 10px 10px #888888;
  color: #42a2d7;
  background-color:#FFFFFF;
  border:1px solid #000000; 
  border-radius:3px; 
  width:450px; 
  height:auto; 
  display:none;
}        
        #progress
        {
            width: 135px;
        }
        
    </style>
    <script type="text/javascript">

/**--------------------------
//* Validate Date Field script- By JavaScriptKit.com
//* For this script and 100s more, visit http://www.javascriptkit.com
//* This notice must stay intact for usage
---------------------------**/

function checkForDate(strPassed) {
   var newDate = new Date();
   newDate.setTime(Date.parse(strPassed));
   if (isNaN(parseFloat(newDate.getFullYear()))) {
        //alert("t");
      return (true);
   }
   return (true);
}



function ValidateSave() {

    var Ctl = document.getElementById('<%=HdnAlert.ClientID%>');
    if (Ctl.value != "") {
        alert(Ctl.value);
        return false;
    }

    objDDl = document.getElementById('<%=CmbTransType.ClientID%>');
    SchAccH = objDDl.options[objDDl.selectedIndex].text;

    if (SchAccH == "Account No") {
        objDDl = document.getElementById('<%=CmbSchTrans.ClientID%>');
        var TrMode = objDDl.options[objDDl.selectedIndex].text;
    }

    if (TrMode != "Renewal") {
        // Blocking Save Incase of Amount Not Entered
        var RctCash = document.getElementById('<%=LblRctCashTotal.ClientID%>').innerHTML;
        var RctTrans = document.getElementById('<%=LblRctTransferTotal.ClientID%>').innerHTML;
        var PayCash = document.getElementById('<%=LblPayCashTotal.ClientID%>').innerHTML;
        var PayTrans = document.getElementById('<%=LblPayTransferTotal.ClientID%>').innerHTML;
    }
    // Not Numeric
     var schcode=document.getElementById('<%=TxtSchCode.ClientID%>');

    if (schcode.value != 215) {
        if (parseFloat(RctCash) + parseFloat(RctTrans) + parseFloat(PayCash) + parseFloat(PayTrans) == 0) {
            alertify.error('Transaction Amount Not Entered, Please Check');
            return false;
        }
    }

    // Blocking Transaction if Authorisation Pending For LOANs / Closure / Renewal
    var Section = document.getElementById('<%=SchSection.ClientID%>').value;
    var TrDate = document.getElementById('<%=HdnTrDate.ClientID%>').value;

    if (SchAccH == "Account No") {
        if (TrMode != "Renewal") {
            var InstrDate = document.getElementById('<%=TxtInstrDate.ClientID%>').value;
        }
        var PendingCr = document.getElementById('<%=lblPendingCr.ClientID%>').value;
        var PendingDr = document.getElementById('<%=lblPendingDr.ClientID%>').value;        

        // Not Numeric
        if (Section == 'LOAN') {
            if (parseFloat(PendingCr) + parseFloat(PendingDr) > 0) {
                alertify.error('Previous Entry Pending For Passing, Please Check');
                return false;
            }
        }

        //    SplitDt = InstrDate.split("/");
        //    Frdt = new Date(SplitDt[2], SplitDt[1] - 1, SplitDt[0]);
        //    
        //    SplitDt = TrDate.split("/");
        //    Todt = new Date(SplitDt[2], SplitDt[1] - 1, SplitDt[0]);

        //    var out = DateDiff.inMonths(Frdt, Todt);
        //    alert(out);

        if (TrMode == "Renewal") {
            var RenewDueDate = document.getElementById('<%=TxtRenewDueDate.ClientID%>').value;

            if (RenewDueDate == "") {
                alertify.error("Invlaid Renewal Details, Please Check");
                return false;
            }
             var MONTH = document.getElementById('<%=TxtRenewMonths.ClientID%>').value;
            var DAYS = document.getElementById('<%=TxtRenewDays.ClientID%>').value;
            if(MONTH=='' & DAYS=='')
            {
                alertify.error('Month is not Entered')
                 return false;
            }
            SplitDt = TrDate.split("/");
            Frdt = new Date(SplitDt[2], SplitDt[1] - 1, SplitDt[0]);

            SplitDt = RenewDueDate.split("/");
            Todt = new Date(SplitDt[2], SplitDt[1] - 1, SplitDt[0]);

            if (Frdt > Todt) {
                alertify.error("Renewal Duedate Should be Greater than Transaction Date");
                return false;
            }
        }
    }
                         var r = confirm("Do You Want to Save ? ");
                         if (r == true) {
                             return true ;
                         }
                         else {
                             return false  ;
                         }
    return true;
}

</script>
    <script language="javascript" type="text/javascript">        
function fSplitValues(source, eventArgs) {
var sStr;
var extender = source.get_id();         //id of the source extender            
    if (document.getElementById("ctl00_ContentPlaceHolder1_TxtAccCode").value != "") {          
        sStr = document.getElementById("ctl00_ContentPlaceHolder1_TxtAccCode").value;
        sStr = sStr.replace(/  /g,"")
        sStr = sStr.replace("Code:","")
        sStr = sStr.replace(".","")
        if (sStr.indexOf("|") != -1) {                
            sStr = sStr.split("|");
            document.getElementById("ctl00_ContentPlaceHolder1_TxtAccCode").value = sStr[1];
            document.getElementById("ctl00_ContentPlaceHolder1_TxtAccName").value = sStr[0].replace(".","");                        
            document.getElementById("ctl00_ContentPlaceHolder1_TxtTransAmt").focus();
        }
    }
}

</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True">
    <Scripts>
            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
    </Scripts>
</asp:ToolkitScriptManager>
    
 <div id="divEffect">
    
<div  onkeypress="return WebForm_FireDefaultButton(event, '<%= CmdSave.ClientID %>')" style="width: 98%; margin-left:1%; margin-right:1%;margin-top:1%;">                


                <asp:Panel ID="PnlAuthorise" runat="server">
                <div  style="margin-top:10px" >
                    <br />
                    <table width="100%" border="0">
                  <tr>
                    <td width="2%">&nbsp;</td>
                    <td width="10%">
                        <asp:Label ID="LblCredit" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                      </td>
                    <td width="12%"><label>Transaction Type</label></td>
                    <td width="12%">
                    <asp:DropDownList ID="CmbTransType" runat="server" Width="100%" AutoPostBack="True">
                    </asp:DropDownList>
                    </td>    
                    <td width="13%">
                        <asp:Label ID="LblCashTransfer" Text="Cash / Transfer" runat="server" ForeColor="#008080" font-family= "Cambria" font-size="90%"></asp:Label>                        
                    </td>
                    <td width="12%">
                        <asp:DropDownList ID="CmbCashTransfer" runat="server" AutoPostBack="True" 
                            TabIndex="1" Width="100%">
                        </asp:DropDownList>
                      </td>
                    <td width="2%"></td>
                    <td width="10%"><label>Batch No</label></td>
                    <td width="13%"  >
                        <asp:DropDownList ID="CmbBatchNo" runat="server" Width="80%" Enabled="False" 
                            AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:ImageButton ID="ImgBatch" runat="server"  
                            ImageUrl="~/Images/Node.png" ImageAlign=Middle Enabled="False" 
                            OnClientClick="return imgBatchDtls_onclick();"/>
                      </td>
                    <td width="2%"></td>
                    <td width="10%">
                        <asp:Label ID="LblDebit" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                      </td>
                    <td width="2%">&nbsp;</td>
                  </tr>
                </table>
                
                </div>
                </asp:Panel>
                <div  runat="server" id="AccNoDiv" style="margin-top:1%;">                  
                  <asp:Panel ID="PnlAuthorise1" runat="server">
                  <table width ="100%">   
                    <tr>
                      <td width = "2%"></td>
                      <td width = "96%">
                        <%--#99CCFF--%>
                           <div style="background-color: #75BAFF;box-shadow:0px 2px 16px rgba(23, 69, 88, .5);"> 
                           <table width = "100%">
	                        <tr>
		                        <td width = "2%"></td>
		                        <td width = "15%"><label style="color:White">Name</label></td>
		                        <td width = "2%"><label style="color:White">:</label></td>
		                        <td width = "30%">
                                    <asp:Label ID="lblName" runat="server" CssClass="lblCSS" ForeColor="White"></asp:Label>
                                    &nbsp;<label style="color:White">-</label>&nbsp;<asp:Label ID="lblCustID" runat="server"  CssClass="lblCSS"
                                        ForeColor="White"></asp:Label></td>
		                        <td width = "2%"></td>
		                        <td width = "15%"><label style="color:White">Current Balance</label></td>
		                        <td width = "2%"><label style="color:White">:</label></td>			
		                        <td width = "30%">
                                    <asp:Label ID="lblCurBal" runat="server" CssClass="lblCSS" ForeColor="White"></asp:Label>
                                </td>
		                        <td width = "2%"></td>			
	                        </tr>
                               <tr>
                                   <td></td>
                                   <td><label style="color:White">Member No</label></td>
                                   <td><label style="color:White">:</label></td>
                                   <td>
                                       <asp:Label ID="lblMemNo" runat="server" CssClass="lblCSS" ForeColor="White"></asp:Label>
                                   </td>
                                   <td></td>
                                   <td><label style="color:White">Shadow Balance</label></td>
                                   <td><label style="color:White">:</label></td>
                                   <td>
                                       <asp:Label ID="lblShadowBal" runat="server" CssClass="lblCSS" ForeColor="White"></asp:Label>
                                   </td>
                                   <td></td>
                               </tr>
                               <tr>
                                   <td></td>
                                   <td>
                                       <label style="color:White">
                                       Address</label></td>
                                   <td><label style="color:White">:</label></td>
                                   <td rowspan="2" valign="top">
                                       <asp:Label ID="lblAddress" runat="server" CssClass="lblCSS" ForeColor="White"></asp:Label>
                                   </td>
                                   <td></td>
                                   <td><label style="color:White">Pending Cr</label></td>
                                   <td><label style="color:White">:</label></td>
                                   <td>
                                       <asp:Label ID="lblPendingCr" CssClass="lblCSS" runat="server" ForeColor="White"></asp:Label>
                                   </td>
                                   <td></td>
                               </tr>
                               <tr>
                                   <td></td>
                                   <td>
                                       <asp:Label ID="Lbl_Status" CssClass="lblCSS" runat="server" Font-Bold="True" ForeColor="#FF3300" 
                                           Text="Cheque A/c" Visible="false"></asp:Label>
                                   </td>
                                   <td><label style="color:White">:</label></td>
                                   <td></td>
                                   <td><label style="color:White">Pending Dr</label></td>
                                   <td><label style="color:White">:</label></td>
                                   <td>
                                       <asp:Label ID="lblPendingDr" CssClass="lblCSS" runat="server" ForeColor="White"></asp:Label>
                                   </td>
                                   <td></td>
                               </tr>
                               <tr>
                                   <td></td>
                                   <td>
                                       <label style="color:White">
                                       Mobile No</label></td>
                                   <td>
                                       <label style="color:White">
                                       :</label></td>
                                   <td>
                                       <asp:Label ID="lblMobileNo" CssClass="lblCSS" runat="server" ForeColor="White"></asp:Label>
                                   </td>
                                   <td></td>                                   
                                   <td><label style="color:White">Mode of Operation</label></td>
                                   <td><label style="color:White">:</label></td>
                                  <td>
                                      <asp:Label ID="lblModeOp" CssClass="lblCSS" runat="server" ForeColor="White"></asp:Label>
                                   </td>
                                   <td></td>
                               </tr>                                                                            
                            </table>
                           </div>
                        </td>
                        <td width = "2%"></td>
                  </tr>
                  </table>
                  </asp:Panel>
                  <table width ="100%">   
                      <tr>
                          <td width="2%">
                              &nbsp;</td>
                          <td width="96%" align="center">
                              <asp:ImageButton ID="ImgImage" runat="server" ImageUrl="~/Images/TrImg1.png" 
                                  OnClientClick="return imgTranImage_onclick();" TabIndex="999" />
                              <asp:ImageButton ID="ImgJointAc" runat="server" ImageUrl="~/Images/TrImg2.png" 
                                  OnClientClick="return transactionpopup('JointAcc.aspx','Joint Details');" TabIndex="999" />
                              <asp:ImageButton ID="ImgNominee" runat="server" Enabled="False" 
                                  ImageUrl="~/Images/TrImg3.png" OnClientClick="return transactionpopup('NomDtls.aspx');" 
                                  TabIndex="999" />
                              <asp:ImageButton ID="ImgPassbook" runat="server" ImageUrl="~/Images/TrImg4.png" 
                                  OnClientClick="return transactionpopup('TranDtls.aspx','Passbook');" TabIndex="999" />
                              <asp:ImageButton ID="ImgLiab" runat="server" ImageUrl="~/Images/TrImg9.png" 
                                  OnClientClick="return transactionpopup('AssetLiab.aspx','Asset & Liability');" TabIndex="999" />
                              <asp:ImageButton ID="ImgDetails" runat="server" ImageUrl="~/Images/TrImg5.png" 
                                  OnClientClick="return imgTranDtls_onclick();" TabIndex="999" />
                              <asp:ImageButton ID="ImgAction" runat="server" Enabled="False" 
                                  ImageUrl="~/Images/TrImg6.png" OnClientClick="return transactionpopup('LoanAction.aspx','Loan Action Details');" 
                                  TabIndex="999" />
                              <asp:ImageButton ID="ImgOrnaments" runat="server" Enabled="False" 
                                  ImageUrl="~/Images/TrImg7.png" OnClientClick="return transactionpopup('GoldItems.aspx','Ornaments Details');" 
                                  TabIndex="999" />
                                  
                              <asp:ImageButton ID="ImageButton2" runat="server" Enabled="True" 
                                  ImageUrl="~/Images/TrImgEMI.png" OnClientClick="return transactionpopup('LoanEMIChart.aspx','EMI CHART');" 
                                  TabIndex="999" />
                                  
                              <asp:ImageButton ID="ImgCheque" runat="server" Enabled="true" 
                                  ImageUrl="~/Images/TrImg8.png" 
                                 OnClientClick="return transactionpopup('ChequeIssueDtls.aspx','Cheque Details');" TabIndex="999" />
                              <asp:ImageButton ID="ImageButton1" runat="server" Enabled="true" 
                                  ImageUrl="~/Images/details.png" 
                                  OnClientClick="return imgBatchDtls_onclick();" TabIndex="999" />
                                  
                          </td>
                          <td width="2%">
                              &nbsp;</td>
                      </tr>
                      </table>
                  
                  <asp:Panel ID="PnlAuthorise2" runat="server"> 
                  <table width ="100%">   
                      <tr>
                          <td width="2%">
                              &nbsp;</td>
                          <td align="center" width="96%">
                                <table width = "100%">
	                            <tr>
		                            <td width = "15%" align="left" rowspan="3" style="width: 49%" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td width="24%">
                                                    <label>
                                                    Branch</label></td>
                                                <td width="76%">
                                                    <asp:DropDownList ID="CmbBranch" runat="server" AutoPostBack="True" 
                                                        TabIndex="2" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="24%">
                                                    <label>
                                                    Section</label></td>
                                                <td width="76%">
                                                     
                                                                <asp:DropDownList ID="CmbSection" runat="server" AutoPostBack="True" 
                                                                    TabIndex="4" Width="100%">
                                                                </asp:DropDownList>
                                                            </td>
                                                        
                                            </tr>
                                            <tr>
                                                <td width="30%">
                                                    <label>
                                                    Scheme</label></td>
                                                <td width="70%">
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="20%">
                                                                <asp:TextBox ID="TxtSchCode" runat="server" AutoPostBack="True" 
                                                                    CssClass="twitterStyleTextbox" TabIndex="3" Width="100%"></asp:TextBox>
                                                            </td>
                                                            <td width="2%">
                                                            </td>
                                                            <td width="78%">
                                                                <asp:DropDownList ID="CmbScheme" runat="server" AutoPostBack="True" 
                                                                    TabIndex="4" Width="100%">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            <tr>
                                                <td width="30%">
                                                    <label>
                                                    Account No</label></td>
                                                <td width="70%">
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="20%">
                                                                <asp:TextBox ID="TxtShortCode" runat="server" AutoPostBack="True" 
                                                                    CssClass="twitterStyleTextbox" TabIndex="5" Width="100%"></asp:TextBox>
                                                            </td>
                                                            <td width="2%">
                                                            </td>
                                                            <td width="67%">
                                                                <asp:TextBox ID="TxtAccNo" runat="server" AutoPostBack="True" 
                                                                    CssClass="twitterStyleTextbox" TabIndex="6" Width="99%"></asp:TextBox>
                                                            </td>
                                                            <td width="2%">
                                                            </td>
                                                            <td align="right" width="8%">
                                                                <asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
                                                                    ImageUrl="~/images/Search.png" TabIndex="999" Width="23px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            </table>
                                    </td>
		                            <td width = "2%"></td>
		                            <td width = "15%" align="left" rowspan="3" style="width: 49%" valign="top">
		                                <table width = "100%">
	                                        <tr>
		                                        <td width = "30%">
                                                    <label>
                                                    Transaction</label></td>
		                                        <td width = "70%">
                                                    <asp:DropDownList ID="CmbSchTrans" runat="server" AutoPostBack="True" 
                                                        TabIndex="7" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
	                                        </tr>
                                            <tr>
                                                <td colspan="2" style="width: 100%" width="30%">
                                                        <div ID="DivInstl" runat="server" visible="false">
                                                        <table border="0" width="100%">
                                                            <tr>
                                                                <td width="30%">
                                                                    <label>
                                                                    No of Installment</label>
                                                                </td>
                                                                <td width="70%">
                                                                    <asp:TextBox ID="TxtInstNo" runat="server" AutoPostBack="true" 
                                                                        CssClass="twitterStyleTextbox" TabIndex="8" Width="97%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        </div>                                        

                                                        <div ID="DivShare" runat="server" visible="false">
                                                            <table border="0" width="100%">
                                                                <tr>
                                                                    <td width="30%">
                                                                        <label>
                                                                        No of Share</label>
                                                                    </td>
                                                                    <td width="70%">
                                                                        <asp:TextBox ID="TxtShareNo" runat="server" AutoPostBack="True" 
                                                                            CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" 
                                                                            style="text-align:right" TabIndex="9" Width="98%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                               </table>
                                                               </div>
                                                        <div ID="DivResl" runat="server" visible="false">                                                                   
                                                               <table width = "100%">
                                                                <tr>
                                                                    <td width="30%">
                                                                        <label>
                                                                        Resolution
                                                                        </label>
                                                                    </td>
                                                                    <td width="70%">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td width="50%">
                                                                                    <asp:TextBox ID="TxtShReslNo" runat="server" CssClass="twitterStyleTextbox" 
                                                                                        TabIndex="10" Width="98%"></asp:TextBox>
                                                                                </td>
                                                                                <td width="50%">
                                                                                    <asp:TextBox ID="TxtShReslDt" runat="server" CssClass="twitterStyleTextbox" 
                                                                                        TabIndex="11" Width="98%"></asp:TextBox>
                                                                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" 
                                                                                        TargetControlID="TxtShReslDt">
                                                                                    </asp:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div ID="DivSuspense" runat="server" visible="True">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td width="30%">
                                                                        <label>
                                                                        Member No
                                                                        </label>
                                                                    </td>
                                                                    <td width="21%">
                                                                        <asp:DropDownList ID="CmbMemBranch" runat="server" TabIndex="12" Width="100%">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td width="21%">
                                                                        <asp:DropDownList ID="CmbMemType" runat="server" AutoPostBack="True" 
                                                                            TabIndex="13" Width="100%">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td width="28%">
                                                                        <asp:TextBox ID="TxtMemberNo" runat="server" AutoPostBack="True" 
                                                                            CssClass="twitterStyleTextbox" TabIndex="14" Width="96%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="30%">
                                                                        &nbsp;</td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="TxtMemDtls" runat="server" CssClass="twitterStyleTextbox" 
                                                                            ReadOnly="true" TabIndex="999" Width="98%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div ID="DivCRDR" runat="server">
                                                            <table border="0" width="100%">
                                                                <tr>
                                                                    <td width="30%">
                                                                        <label>
                                                                        Transaction Amt</label>
                                                                    </td>
                                                                    <td width="70%">
                                                                        <asp:TextBox ID="TxtTransAmt" runat="server" AutoPostBack="True" 
                                                                            CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" 
                                                                            style="text-align:right" TabIndex="15" Width="98%" MaxLength="15"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div ID="DivInstr" runat="server">
                                                            <table border="0" width="100%">
                                                                <tr>
                                                                    <td width="30%">
                                                                        <label>
                                                                        Instrument
                                                                        </label>
                                                                    </td>
                                                                    <td width="70%">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td width="33%">
                                                                                    <asp:DropDownList ID="CmbInstType" runat="server" AutoPostBack="True" 
                                                                                        TabIndex="16" Width="100%">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td width="34%">
                                                                                    <asp:TextBox ID="TxtInstrNo" runat="server" AutoPostBack="True" 
                                                                                        CssClass="twitterStyleTextbox" TabIndex="17" Width="98%"></asp:TextBox>
                                                                                </td>
                                                                                <td width="33%">
                                                                                    <asp:TextBox ID="TxtInstrDate" runat="server" AutoPostBack="True" 
                                                                                        CssClass="twitterStyleTextbox" TabIndex="18" Width="98%"></asp:TextBox>
                                                                                    <asp:CalendarExtender ID="TxtInstrDate_CalendarExtender" runat="server" 
                                                                                        Format="dd/MM/yyyy" TargetControlID="TxtInstrDate">
                                                                                    </asp:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div id ="DivInvest" runat="server" visible="false">
                                                            <table border="0" width="100%">
                                                            <tr>
                                                                <td width= "30%">
                                                                    <label> Interest Amt</label>                                                                    
                                                                </td>
                                                                <td width= "70%">
                                                                    <asp:TextBox ID="TxtInvestIntAmt" runat="server" AutoPostBack="True" 
                                                                            CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" 
                                                                            style="text-align:right" TabIndex="15" Width="98%" MaxLength="15">
                                                                    </asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            </table>
                                                        </div>
                                                        <div id ="DivRebate" runat="server" visible="false">
                                                            <table border="0" width="100%">
                                                            <tr>
                                                                <td width= "30%">
                                                                    <label> Interest Rebate</label>                                                                    
                                                                </td>
                                                                <td width= "70%">
                                                                    <asp:TextBox ID="TxtRebate" runat="server" AutoPostBack="True" 
                                                                            CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" 
                                                                            style="text-align:right" TabIndex="15" Width="98%" MaxLength="15">
                                                                    </asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            </table>
                                                        </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
	                            </tr>
                                    <tr>
                                        <td width="2%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td width="2%">
                                            &nbsp;</td>
                                    </tr>
                                </table>          
                          </td>
                          <td width="2%">
                              &nbsp;</td>
                      </tr>                      
                  </table>    
                  </asp:Panel>
                  
                </div>
                <asp:Panel ID="PnlAuthorise3" runat="server"> 
                <div id="DivFDRENEW" visible="false" runat="server" >     
                <legend>RENEW DETAILS</legend>   
<table width ="100%">
	<tr>
		<td width = "2%"></td>
		<td width = "15%"> <label>
            Renew Type</label>
            &nbsp;</td>
		<td width = "8%">
             <asp:DropDownList ID="CmbRenewType" runat="server" AutoPostBack="True" 
                TabIndex="21" Width="100%">
            </asp:DropDownList>
        </td>
		<td width = "2%"></td>
		<td width = "8%">
            <label>
            Principle Amt</label>&nbsp;&nbsp;</td>
		<td width = "8%">
            <asp:TextBox ID="TxtRenewPrnAmt" runat="server" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" style="text-align:right" TabIndex="19" Width="95%"></asp:TextBox>
        </td>
		<td width = "2%"></td>
		<td width = "10%">
           &nbsp;<label>Int Amt</label>&nbsp;</td>
		<td width = "11%">
          
            <asp:TextBox ID="TxtRenewIntAmt" runat="server" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);"
                style="text-align:right" TabIndex="20" Width="98%"></asp:TextBox>
        </td>
		<td width = "3%"></td>
		<td width = "11%">
           &nbsp;<label>Adnl Amt</label></td>
		<td width = "11%">
           
            <asp:TextBox ID="TxtRenewAddiAmt" runat="server" AutoPostBack="True" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" style="text-align:right" TabIndex="27" Width="98%"></asp:TextBox>
        </td>
		<td width = "2%"></td>
	    <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="11%"> <label>
            Effect Date</label>
                &nbsp;</td>
            <td width="11%"> <asp:TextBox ID="TxtRenewEffDate" runat="server" CssClass="twitterStyleTextbox" 
                onblur="return checkForDate(this.TxtRenewEffDate)" TabIndex="22" Width="95%"></asp:TextBox>
                &nbsp;</td>
            <td width="3%">
                &nbsp;</td>
            <td width="11%">
                <label>
                Months</label>&nbsp;&nbsp;</td>
            <td width="11%">
                <asp:TextBox ID="TxtRenewMonths" runat="server" AutoPostBack="True" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" style="text-align:center" TabIndex="24" Width="95%"></asp:TextBox>
            </td>
            <td width="2%">
                &nbsp;</td>
            <td width="11%">
                <label>
                Days</label>&nbsp;&nbsp;</td>
            <td width="11%">
                <asp:TextBox ID="TxtRenewDays" runat="server" AutoPostBack="True" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" style="text-align:center" TabIndex="25" Width="98%"></asp:TextBox>
            </td>
            <td width="3%">
                &nbsp;</td>
            <td width="11%">
                <label>
                Due Date</label></td>
            <td width="11%">
                <asp:TextBox ID="TxtRenewDueDate" runat="server" CssClass="twitterStyleTextbox" onblur="return checkForDate(this.TxtRenewDueDate)" TabIndex="26" Width="95%"></asp:TextBox>
            </td>
            <td width="2%">
                &nbsp;</td>
            <tr>
                <td width="2%">
                    &nbsp;</td>
                <td width="11%">
                    <label>
                    Privilage</label></td>
                <td width="11%">
                    <asp:DropDownList ID="CmbRenewPrivilage" runat="server" AutoPostBack="True" Width="100%">
                    </asp:DropDownList>
                </td>
                <td width="3%">
                    &nbsp;</td>
                <td width="11%">
                    <label>
                    Int Rate</label></td>
                <td width="11%">
                    <asp:TextBox ID="TxtRenewIntRate" runat="server" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" style="text-align:right" TabIndex="23" Width="98%"></asp:TextBox>
                </td>
                <td width="2%">
                    &nbsp;</td>
                <td width="11%"> 
                    <label>
                    Int Pay Mode</label></td>
                <td width="11%">
                    <asp:DropDownList ID="CmbRenewIntPayMode" runat="server" Width="100%">
                    </asp:DropDownList>
                </td>
                <td width="3%">
                    &nbsp;</td>
                <td width="11%">
                    <label>
                    ST Inst</label>&nbsp;</td>
                <td width="11%">
                    <asp:DropDownList ID="Ddl_SI" runat="server" AutoPostBack="True" TabIndex="29" Width="100%">
                        <asp:ListItem Value="N">No</asp:ListItem>
                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="2%">
                    &nbsp;</td>
            </tr>
            
           <tr>
                <td width="2%"></td>
                <td width="11%">
                    <label>
                    Renew Amt</label></td>
                <td width="11%">
                    <asp:TextBox ID="TxtRenewAmt" runat="server" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" style="text-align:right" TabIndex="28" Width="100%"></asp:TextBox>
                </td>
                <td width="2%"></td>
                <td width="11%">
                    <label>
                    Nominee</label></td>
                <td width="11%">
                    <asp:DropDownList ID="CmbNomReq" runat="server" AutoPostBack="True" TabIndex="30" Width="100%">
                    </asp:DropDownList>
                </td>
            <tr>
                <td colspan="13">
                    <div id ="NomDiv" class="gradientBoxesWithOuterShadows" runat="server" visible="false" style="margin-left:2%;margin-right:2%;margin-bottom:2%;margin-top:2%;">
                        <legend>NOMINEE DETAILS</legend>
                        <table width = "100%">
                            <tr>
                                <td width = "2%" > </td>
                                <td width = "15%"> <label>Nominee Name</label></td>
                                <td width = "49%"> <asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_NomName" 
                                        runat="server"  Width="99%" TabIndex="31" MaxLength="200"></asp:TextBox></td>
                                <td width = "2%" > </td>
                                <td width = "15%"> <label>Relation</label></td>
                                <td width = "15%"> 
                                    <asp:DropDownList ID="CmbNomRel" runat="server" Width="100%" TabIndex="32">
                                    </asp:DropDownList>
                                </td>
                                <td width = "2%" > </td>
                            </tr>	
                            <tr>
                                <td></td>
                                <td><label>Address</label></td>
                                <td><asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_NomAdd" runat="server"  
                                        Width="99%" TabIndex="33"></asp:TextBox></td>
                                <td></td>
                                <td><label>Phone No</label></td>
                                <td><asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_NomPh" runat="server"  
                                        Width="96%" TabIndex="34"></asp:TextBox></td>
                                <td></td>
                            </tr>
                        <%--    <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>--%>
                        </table>
                    </div>
                </td>
            </tr>
            
            <tr>
                <td colspan="13">
                    <div id="StandDiv" class="gradientBoxesWithOuterShadows" runat="server" visible="false" style="margin-left:2%;margin-right:2%;margin-bottom:2%;margin-top:2%;">
                        <legend>STANDING DETAILS</legend>
                        <table width="100%">
                            <tr>
                                <td width = "2%"></td>
                                <td width = "15%"><label>Standing Branch</label></td>
                                <td width = "15%">
                                    <asp:DropDownList ID="CmbStandBr" runat="server" Width="100%" TabIndex="35">
                                    </asp:DropDownList>
                                </td>
                                <td width = "2%"></td>
                                <td width = "16%"><label>Standing Scheme</label></td>
                                <td width = "16%">
                                    <asp:DropDownList ID="CmbStandSch" runat="server" Width="100%" TabIndex="36">
                                    </asp:DropDownList>            
                                </td>
                                <td width = "2%"></td>            
                                <td width = "15%"><label>Standing Account No</label></td>
                                <td width = "15%">
                                    <asp:TextBox ID="TxtStandAcNo" runat="server" CssClass="twitterStyleTextbox" 
                                        TabIndex="37" Width="96%" AutoPostBack="True"></asp:TextBox>
                                </td>
                                <td width = "2%"></td> 
                            </tr>
                            <tr>
                            <td>        </td>
                            <td>        
                                <label>
                                Name</label></td>
                            <td>        
                                <asp:TextBox ID="TxtStandAcHoldenName" runat="server" AutoPostBack="True" 
                                    CssClass="twitterStyleTextbox" TabIndex="38" Width="96%"></asp:TextBox>
                                </td>
                            <td>            
                                </td>
                            <td>        
                                <label>
                                Address</label></td>
                            <td colspan = "4">        
                                <asp:TextBox ID="TxtStandAddress" runat="server" AutoPostBack="True" 
                                    CssClass="twitterStyleTextbox" TabIndex="39" Width="98%"></asp:TextBox>
                                </td>
                            
                            
                            </tr>
                <%--            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td colspan = "3"></td>
                                <td></td>
                            </tr>--%>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
            <td colspan="13">
        
            </td>
            </tr>
            
            <%--<tr>
                <td width="2%">
                    </td>
                <td width="11%">
                <td width="11%">
                </td>
                <td width="3%"></td>
                <td width="11%">
                <td width="11%">
                </td>
                <td width="2%">
                    &nbsp;</td>
                <td width="11%">
                    &nbsp;</td>
                <td width="11%">
                    &nbsp;</td>
                <td width="3%">
                    &nbsp;</td>
                <td width="11%">
                    &nbsp;</td>
                <td width="11%">
                    &nbsp;</td>
                <td width="2%">
                    &nbsp;</td>
            </tr>--%>
        </tr>
</table>
  
 
                  <table width="100%"   border="0" >
                    <tr>
                      <td width="20%" height="100%">&nbsp;</td>
                      <td width="60%" valign="top">
                                
                                &nbsp;</td>
                      <td width="20%">&nbsp;</td>
                    </tr>
                    </table>     
                    </div>     
                <div runat="server" id="AcHeadDiv" visible="false"   style="margin-top:1%;">                
                  
                  
                  
                  <table width="100%"   border="0" >
                    <tr>
                      <td width="2%">&nbsp;</td>
                      <td width="15%"><label>&nbsp;Transaction</label></td>
                      <td width="7%">
                          <asp:DropDownList ID="CmbAcheadTran" runat="server" Width="100%" 
                            AutoPostBack="True" TabIndex="29">
                        </asp:DropDownList>
                        </td>
                      <td width="1%">&nbsp;</td>
                      <td width="7%"><label>Account</label></td>      
                      <td width="7%" style="margin-left: 40px">
                          <asp:TextBox ID="TxtAccCode" runat="server" Width="95%" TabIndex="30"  
                              CssClass="twitterStyleTextbox" AutoPostBack="True"></asp:TextBox>
                          <asp:autocompleteextender ID="AutoCompleteExtender1" runat="server" 
                                TargetControlID="TxtAccCode" MinimumPrefixLength="1" CompletionInterval="10" 
                                EnableCaching="true" CompletionSetCount="6"
                                UseContextKey="True" ServiceMethod="GetCompletionList" 
                                OnClientItemSelected="fSplitValues" 
                                CompletionListCssClass="wordWheel listMain .box" 
                                CompletionListItemCssClass="wordWheel itemsMain" 
                                CompletionListHighlightedItemCssClass="wordWheel itemsSelected">
                            </asp:autocompleteextender>
                      </td>      
                      <td width="20%">
                          <asp:TextBox ID="TxtAccName" runat="server" Width="100%" TabIndex="999"  
                              CssClass="twitterStyleTextbox"></asp:TextBox>
                      </td>
                      <td width="1%">&nbsp;</td>
                      <td width="7%"><label>Cash</label></td>                      
                      <td width="10%">
                          <asp:TextBox ID="TxtAcHeadTransAmt" runat="server" Width="95%" 
                              TabIndex="31" onkeypress="return isFloatKey(event);"  
                              CssClass="twitterStyleTextbox" AutoPostBack="True"></asp:TextBox></td>      
                      <td width="1%">   
                      <td width="7%"><label>Transfer</label></td>                      
                      <td width="10%">
                          <asp:TextBox ID="TxtTransfer" runat="server" Width="95%" 
                              TabIndex="31" onkeypress="return isFloatKey(event);"  
                              CssClass="twitterStyleTextbox" AutoPostBack="True"></asp:TextBox></td>      
                      <td width="6%">   
                      
                          <asp:Button ID="CmdOrnAdd" runat="server"  UseSubmitBehavior="False" 
                                    Width="27" TabIndex="32" BackColor="#9999FF" CssClass="Add_btn" />
                            </td>
                     
                    </tr>
                </table>
                
                </div>  
                <table width="100%"   border="0" >
                  <tr>
                    <td >&nbsp;</td>
                    <td ><label>&nbsp;Remarks </label></td>
                    <td colspan = "5">
                            &nbsp;&nbsp;<asp:TextBox ID="TxtRemarks" runat="server" Width="98%" 
                            meta:resourcekey="TxtLandPhoneResource1" TabIndex="33"  
                                CssClass="twitterStyleTextbox"></asp:TextBox></td>
                    <td>&nbsp;</td>
                  </tr>
                  <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td colspan = "5">
                            &nbsp;&nbsp;<asp:TextBox ID="TxtRemarks1" runat="server" Width="98%" 
                            meta:resourcekey="TxtLandPhoneResource1" TabIndex="34"  
                                CssClass="twitterStyleTextbox"></asp:TextBox></td>
                    <td>&nbsp;</td>
                  </tr>
                  <tr>
                    <td>&nbsp;</td>
                    <td width ="25%">&nbsp;</td>
                      <td width="25%" style="width: 0%"><label>From Branch</label>&nbsp;</td>
                      <td style="width: 12%" width="25%">
                          <asp:DropDownList ID="DrpFrmBr" runat="server" Width="100%">
                          </asp:DropDownList>
                      </td>
                      <td width="25%" style="width: 0%"><label>To Branch</label>&nbsp;</td>
                      <td style="width: 12%" width="25%">
                          <asp:DropDownList ID="DrpToBr" runat="server" Width="100%">
                          </asp:DropDownList>
                      </td>
                      <td width="25%"><label width="35%">CostCenter</label><asp:DropDownList 
                              ID="DrpCostCentr" runat="server" Width="35%">
                          </asp:DropDownList>
                          &nbsp;</td>
                    <td>&nbsp;</td>
                  </tr>
                    <tr>
                        <td width="2%">
                            </td>
                        <td width="14%">
                            <label>
                            &nbsp;Tocken No</label></td>
                        <td width="10%" colspan="2">
                            &nbsp;&nbsp;<asp:TextBox ID="TxtTockenNo" runat="server" CssClass="twitterStyleTextbox" 
                                meta:resourcekey="TxtLandPhoneResource1" TabIndex="999" Width="50%"></asp:TextBox>
                        </td>
                        <td width="2%" colspan="2">
                            </td>
                        <td width="70%">
                            <asp:Label ID="LblWords" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                        </td>
                        <td width="2%">
                            </td>
                    </tr>
                </table>
                <table width="100%"   border="0" >
                <tr align="right"><td  width="100%"><table width="100%"><tr><td width ="97%" align="right">
                    <asp:CheckBox ID="ChkQuickTransfer" runat="server" AutoPostBack="true" 
                        Text="Allow Quick Transfer" Visible="False"/></td><td width="3%"></td></tr></table></td></tr>
                    <tr>
                    <td>
            <div ID="TrfrDiv" runat="server"  
                    style="margin-left:2%;margin-right:2%;margin-bottom:2%;margin-top:2%;" visible="false">
                <legend> Quick Transfer</legend> 
                    <table width="100%">
                        <tr>
                            
                                <td width="2%">
                                </td>
                                <td width="15%">
                                    <label>
                                    Transfer Branch</label></td>
                                <td width="15%">
                                    <asp:DropDownList ID="CmbTrfrBr" runat="server" TabIndex="31" Width="100%" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td width="2%">
                                </td>
                                <td width="16%">
                                    <label>
                                    Transfer Scheme</label></td>
                                <td width="16%">
                                    <asp:DropDownList ID="CmbTrfrSch" runat="server" TabIndex="31" Width="100%" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td width="2%">
                                </td>
                                <td width="15%">
                                    <label>
                                    Transfer Account No</label></td>
                                <td width="15%">
                                    <asp:TextBox ID="TxtTrfrAcNo" runat="server" AutoPostBack="True" 
                                        CssClass="twitterStyleTextbox" 
                                        onkeypress="return textboxMultilineMaxNumber(this,18);" TabIndex="33" 
                                        Width="96%"></asp:TextBox>
                                </td>
                                <td width="2%">

                                                                                                    <asp:ImageButton ID="ImgTrAccNo" runat="server" Height="24px" 
                                                                    ImageUrl="~/images/Search.png" TabIndex="999" Width="23px" />
                                                            </td>
                           
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <label>
                                 Name </label></td>
                            <td>
                                <asp:TextBox ID="TxtTrfrAcHoldenName" runat="server" AutoPostBack="True" 
                                    CssClass="twitterStyleTextbox" 
                                    onkeypress="return textboxMultilineMaxNumber(this,200);" TabIndex="33" 
                                    Width="96%"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <label>
                                
                               Address</label></td>
                            <td colspan="4">
                                <asp:TextBox ID="TxtTrfrAddress" runat="server" AutoPostBack="True" 
                                    CssClass="twitterStyleTextbox" 
                                    onkeypress="return textboxMultilineMaxNumber(this,200);" TabIndex="33" 
                                    Width="98%"></asp:TextBox>
                            </td>
                            <td width="2%">
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <label>
                                Balance</label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtTrfrBalAmt" runat="server" CssClass="twitterStyleTextbox" 
                                    onkeypress="return textboxMultilineMaxNumber(this,18);" TabIndex="33" 
                                    Width="96%"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td><label>Narration</label>
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="TxtTrfrRemarks" runat="server" AutoPostBack="True" 
                                    CssClass="twitterStyleTextbox" 
                                   TabIndex="33" 
                                    Width="98%"></asp:TextBox>
                            </td>
                            <td width="2%">
                            </td>
                        </tr>

                    </table>
                </div>            
                    </td>
                    </tr></table>
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
                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#0033CC" Text="TOTAL"></asp:Label>
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
                </asp:Panel>
    <table width="100%">
        <tr>
            <td width="30%">
            </td>
            <td  width="40%">
                <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                    meta:resourcekey="CmdSaveResource1" TabIndex="35"  
                    OnClientClick="return ValidateSave();"/>
                <asp:ImageButton ID="CmdClear0" runat="server" ImageUrl="~/Images/Clear.png" 
                    meta:resourcekey="CmdSaveResource1" TabIndex="36" />
                <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/images/Close.png" 
                    meta:resourcekey="CmdClearResource1" TabIndex="37" 
                    PostBackUrl="~/Main.aspx" />
                <asp:ImageButton ID="CmdBatch" runat="server" Enabled="False" 
                    ImageUrl="~/Images/batch.png" meta:resourcekey="CmdSaveResource1" 
                    OnClientClick="return ValidateSave();" TabIndex="35" />
            </td>
            <td width="30%">
                <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                    AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div ID="blur">
                        </div>
                        <div ID="progress">
                            <img alt="progress" src="Images/progress.gif" /> Please Wait...
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>--%>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="SchSection" runat="server" />
    <asp:HiddenField ID="SchAchead" runat="server" />
    <asp:HiddenField ID="SchModuleCatg" runat="server" />
    <asp:HiddenField ID="AccID" runat="server" />
    <asp:HiddenField ID="HdnAlert" runat="server" Value="" />
    <asp:HiddenField ID="HdnTrDate" runat="server" Value="" />
    <asp:HiddenField ID="HdnChqValidityPeriod" runat="server" Value="" />
    <asp:HiddenField ID="HdnChqValidityPeriodType" runat="server" Value="M" />
    <asp:HiddenField ID="HdnRenewDueDt" runat="server"  />
    <asp:HiddenField ID="HdnReferAccID" runat="server" Value="0"  />
    <asp:HiddenField ID="HdnCustId" runat="server" Value="0"  />
                <asp:HiddenField ID="HdnTrfrAccID" runat="server" />
</div>

<div id = "LoanDtlsDiv" class="floatingDiv" runat="server" style="left: 57%;top : 52%;width:30% ">
    <asp:HiddenField ID="SchModuleCatg0" runat="server" />

    <table width="100%" border="0">
      <tr>
        <td>&nbsp;</td>
        <td><label>Total Amount</label></td>
        <td><asp:Label ID="LblLoanTotal" runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label></td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td><label>Overdue Amount</label></td>
        <td><asp:Label ID="LblLoanOD" runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label></td>
        <td>&nbsp;</td>
      </tr>
    </table>
</div>

<div id = "FDDtlsDiv"  class="floatingDiv" runat="server" style="left: 57%;top : 52%;width:30% ">
    <table width="100%" border="0">
      <tr>
        <td>&nbsp;</td>
        <td><label>Interest Payable</label></td>
        <td><asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label></td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td><label>Interest Paid</label></td>
        <td><asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label></td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td><label>Balance Interest</label></td>
        <td><asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label></td>
        <td>&nbsp;</td>
      </tr>            
    </table>
</div>

 <br />
 </div>
</ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>

