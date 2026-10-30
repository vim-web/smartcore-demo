<%@ Page Language="vb" MasterPageFile="~/SMARTCORE.Master"  AutoEventWireup="false" CodeBehind="LoanMaster.aspx.vb" Inherits="SMARTCORE_UI.LoanMaster" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />      
    <script type='text/javascript' src="Scripts/Common.js"></script>  
    <script type="text/javascript" src="Scripts/jquery_json.js"></script>
    <script type='text/javascript' src="Scripts/jquery-1.8.3.js"></script>  
    <link href="CSS/ui-lightness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript' src="Scripts/jquery-ui-1.9.2.custom.min.js"></script>  
 
    
    
    <script src="Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
        <script type="text/javascript">
    
        function transactionpopup(url,Title)
        {
         
            
            $('#divEffect').append('<div id="resultdiv" title="'+Title+'" visible="true" >');
           // alert(url);
            $("#resultdiv").load("ChequeIssueDtls.aspx?AccID=97" , {}, function (data) {
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
   
           function imgChequeStatus_onclick() {
           alert('ggg');
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
</script>  
   
    <script type="text/javascript">
    
       
 
    
        var safe = function (id)
        { return document.getElementById(id); }
        function show(id) {
            safe(id).style.display = "block";
        }

        function hide(id) {
            safe(id).style.display = "none";
        }

        //function Validatesave() {
        //    var Ctl = document.getElementById('ctl00_ContentPlaceHolder1_Hid_ODPStatus');
        //    if (Ctl.value == "N") {
        //        alertify.error('Please Verify Mobile No.');

        //        return false;
        //    }
        //    return true;
        //}

        function onlyNumbers(event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alertify.error("Enter Only Numbers");
                return false;
            }
            else
                return true;
        }

        function CompareDates() {
            var str1 = document.getElementById('<%=Txt_ReslDt.ClientID%>').value;

            var str2 = document.getElementById('<%=HdnDate.ClientID%>').value;
            var dt1 = parseInt(str1.substring(0, 2), 10);
            var mon1 = parseInt(str1.substring(3, 5), 10);
            var yr1 = parseInt(str1.substring(6, 10), 10);
            var dt2 = parseInt(str2.substring(0, 2), 10);
            var mon2 = parseInt(str2.substring(3, 5), 10);
            var yr2 = parseInt(str2.substring(6, 10), 10);
            var date1 = new Date(yr1, mon1, dt1);
            var date2 = new Date(yr2, mon2, dt2);

            if (date2 < date1) {
                alertify.error("Please enter ResDate less than or equal to Transaction date");
                document.getElementById('<%=Txt_ReslDt.ClientID%>').value = '';
                document.getElementById('<%=Txt_ReslDt.ClientID%>').focus();
                return false;
            }
        }

       function validatedate(inputText)  
  {  
  var dateformat = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;  
  // Match the date format through regular expression  
  if(inputText.value.match(dateformat))  
  {  
//  document.form1.text1.focus();  
  //Test which seperator is used '/' or '-'  
  var opera1 = inputText.value.split('/');  
  var opera2 = inputText.value.split('-');  
  lopera1 = opera1.length;  
  lopera2 = opera2.length;  
  // Extract the string into month, date and year  
  if (lopera1>1)  
  {  
  var pdate = inputText.value.split('/');  
  }  
  else if (lopera2>1)  
  {  
  var pdate = inputText.value.split('-');  
  }  
  var dd = parseInt(pdate[0]);  
  var mm  = parseInt(pdate[1]);  
  var yy = parseInt(pdate[2]);  
  // Create list of days of a month [assume there is no leap year by default]  
  var ListofDays = [31,28,31,30,31,30,31,31,30,31,30,31];  
  if (mm==1 || mm>2)  
  {  
  if (dd>ListofDays[mm-1])  
  {
      alertify.error('Invalid date format! Please enter a valid date!');
      inputText.value = "";
      
  return false;  
  }  
  }  
  if (mm==2)  
  {  
  var lyear = false;  
  if ( (!(yy % 4) && yy % 100) || !(yy % 400))   
  {  
  lyear = true;  
  }  
  if ((lyear==false) && (dd>=29))  
  {
      alertify.error('Invalid date format! Please enter a valid date!');
      inputText.value = "";
  return false;  
  }  
  if ((lyear==true) && (dd>29))  
  {
      alertify.error('Invalid date format! Please enter a valid date!');
      inputText.value = "";
  return false;  
  }   
  }   
  }   
  else  
  {
      alertify.error("Invalid date format! Please enter a valid date!");
      inputText.value = "";
  //document.form1.text1.focus();  
  return false;  
  }  
  }  

</script>
    <script type="text/javascript">



        var safe = function (id)
        { return document.getElementById(id); }
        function show(id)
        { safe(id).style.display = 'block'; }
        function hide(id)
        { safe(id).style.display = 'none'; }

        $(function () {
            var Ctl = document.getElementById('<%=Txt_Month.ClientID%>');
            Ctl.focus();
        });


        function imgLiab_onclick() {

            $('.gradientBoxesWithOuterShadows1').append('<div id="resultdiv" title="Customer Asset Liablities" visible="false" >');
            $("#resultdiv").load("AssetLiab.aspx?CustID=" + document.getElementById('<%=HdnSurCustID.ClientID%>').value, {}, function (data) {
                $("#resultdiv").show();
            });
            $("#resultdiv").dialog({
                autoOpen: false,


                height: 600,
                width: 1200,
                //beforeClose: function(event, ui) { $('#resultdiv').remove(); },
                close: function (event, ui) { $(this).dialog("destroy"); },
                modal: true
            });
            $('#resultdiv').dialog('open');
            //    open: function(event, ui) {$(this).dialog('open'); }
        }


        function ValidateSaveBond() {
            var Ctl = document.getElementById('<%=TxtIssuedPostOffice.ClientID%>');
            if (Ctl.value == "") {
                alertify.error('Please Enter Post Office');
                Ctl.focus();
                return false;
            }
            var Ctl = document.getElementById('<%=TxtSerialNo.ClientID%>');
              if (Ctl.value == "") {
                  alertify.error('Please Enter SerialNo');
                  Ctl.focus();
                  return false;
              }
              var Ctl = document.getElementById('<%=TxtPhaseValue.ClientID%>');
     if (Ctl.value == "") {
         alertify.error('Please Enter PhaseValue');
         Ctl.focus();
         return false;
     }
     var Ctl = document.getElementById('<%=TxtDueDate0.ClientID%>');
             if (Ctl.value == "") {
                 alertify.error('Please Enter DueDate');
                 Ctl.focus();
                 return false;
             }
             var Ctl = document.getElementById('<%=TxtTotalPhaseValue.ClientID%>');
             if (Ctl.value == "") {
                 alertify.error('Please Enter TotalPhaseValue');
                 Ctl.focus();
                 return false;
             }
         }

         function ValidateaddVehicle() {

             var Ctl = document.getElementById('<%=TxtType.ClientID%>');
             if (Ctl.value == "") {
                 alertify.error('Please Enter Vehicle Type');
                 Ctl.focus();
                 return false;
             }
             var Ctl = document.getElementById('<%=TxtRegionNo.ClientID%>');
             if (Ctl.value == "") {
                 alertify.error('Please Enter RegionNo');
                 Ctl.focus();
                 return false;
             }
             var Ctl = document.getElementById('<%=TxtChassisNo.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter vehicle ChassisNo');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtEngineNo.ClientID%>');
             if (Ctl.value == "") {
                 alertify.error('Please Enter vehicle EngineNo');
                 Ctl.focus();
                 return false;
             }
             var Ctl = document.getElementById('<%=TxtBrand.ClientID%>');
             if (Ctl.value == "") {
                 alertify.error('Please Enter vehicle Brand');
                 Ctl.focus();
                 return false;
             }
             var Ctl = document.getElementById('<%=TxtModel.ClientID%>');
             if (Ctl.value == "") {
                 alertify.error('Please Enter vehicle Model');
                 Ctl.focus();
                 return false;
             }

             var Ctl = document.getElementById('<%=TxtRTOffice.ClientID%>');
             if (Ctl.value == "") {
                 alertify.error('Please Enter RTO Office');
                 Ctl.focus();
                 return false;
             }



             var Ctl = document.getElementById('<%=TxtInsuranceDetails.ClientID%>');
             if (Ctl.value == "") {
                 alertify.error('Please Enter vehicle InsuranceDetails');
                 Ctl.focus();
                 return false;
             }
             var Ctl = document.getElementById('<%=TxtInsuranceValue.ClientID%>');
             if (Ctl.value == "") {
                 alertify.error('Please Enter vehicle InsuranceValue');
                 Ctl.focus();
                 return false;
             }
             var Ctl = document.getElementById('<%=TxtOtherDetails0.ClientID%>');
             if (Ctl.value == "") {
                 alertify.error('Please Enter OtherDetails');
                 Ctl.focus();
                 return false;
             }
         }


         function ValidatetermDeposit() {
             var Ctl = document.getElementById('<%=TxtDepositeAmt.ClientID%>');
             if (Ctl.value == "") {
                 alertify.error('Please Enter DepositeAmt');
                 Ctl.focus();
                 return false;
             }
             var Ctl = document.getElementById('<%=TxtAmtOutstanding.ClientID%>');
             if (Ctl.value == "") {
                 alertify.error('Please Enter AmtOutstanding');
                 Ctl.focus();
                 return false;
             }
             var Ctl = document.getElementById('<%=TxtSecTDBrName.ClientID%>');
             if (Ctl.value == "") {
                 alertify.error('Please Enter Section Branch Name');
                 Ctl.focus();
                 return false;
             }

             var Ctl = document.getElementById('<%=TxtCustomerNo.ClientID%>');
             if (Ctl.value == "") {
                 alertify.error('Please Enter Customer No');
                 Ctl.focus();
                 return false;
             }
             var Ctl = document.getElementById('<%=TxtSecTD_CustName.ClientID%>');
             if (Ctl.value == "") {
                 alertify.error('Please Enter Name');
                 Ctl.focus();
                 return false;
             }
             var Ctl = document.getElementById('<%=TxtTDAddress.ClientID%>');
             if (Ctl.value == "") {
                 alertify.error('Please Enter Address');
                 Ctl.focus();
                 return false;
             }
         }

         function ValidateLand() {

             var Ctl = document.getElementById('<%=TxtDeedNo.ClientID%>');
             if (Ctl.value == "") {
                 alertify.error('Please Enter Deed No');
                 Ctl.focus();
                 return false;
             }
             var Ctl = document.getElementById('<%=TxtDeedDate.ClientID%>');
             if (Ctl.value == "") {
                 alertify.error('Please Enter Deed Date');
                 Ctl.focus();
                 return false;
             }

             var Ctl = document.getElementById('<%=TxtExtend.ClientID%>');
          if (Ctl.value == "") {
              alertify.error('Please Enter Due Extend');
              Ctl.focus();
              return false;
          }
          var Ctl = document.getElementById('<%=TxtDistrict.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter District');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtTehsil.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter Tehsil');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtPanchMunci.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter Panchayath');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtVillage.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter Village');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtSupRegOff.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter SubRegOffice');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtMarketValue.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter MarketValue');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtSurveyNo.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter SurveyNo No');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtWardNo.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter Ward No.');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtRecepitNo.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter Recepit No.');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtPossessionCrtNo.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter Possession Crt No.');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=txtPossessionCrtDate.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter Possession Crt Date.');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=txtEasyNo.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter Easy No.');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtEasyDate.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter Easy Date.');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtRecepitDate.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter Recepit Date.');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtGahanNo.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter Gahan No.');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtGahanDate.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter Gahan Date.');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtLoacationMapNo.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter Loacation Map No.');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=txtLoacationMapDate.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter Loacation Map Date.');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtPofAtNo.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter PofAt No.');
            Ctl.focus();
            return false;
        }

        var Ctl = document.getElementById('<%=TxtPofatNoDate.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter PofAt Date.');
            Ctl.focus();
            return false;
        }

    }

    function ValidateSaveinsurance() {

        var Ctl = document.getElementById('<%=TxtInsuranceCompany.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter InsuranceCompany');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtPolicyName.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter Policy Name');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtPolicyNo.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter Policy No');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtSumAssured.ClientID%>');
             if (Ctl.value == "") {
                 alertify.error('Please Enter SumAssured');
                 Ctl.focus();
                 return false;
             }
             var Ctl = document.getElementById('<%=TxtSurrenderValue.ClientID%>');
             if (Ctl.value == "") {
                 alertify.error('Please Enter Surrender Value');
                 Ctl.focus();
                 return false;
             }
         }

         function validatecrop() {
             var Ctl = document.getElementById('<%=TxtCropName.ClientID%>');
             if (Ctl.value == "") {
                 alertify.error('Please Enter Crop Name');
                 Ctl.focus();
                 return false;
             }
             var Ctl = document.getElementById('<%=TxtCropExtend.ClientID%>');
             if (Ctl.value == "") {
                 alertify.error('Please Enter Extend');
                 Ctl.focus();
                 return false;
             }
             var Ctl = document.getElementById('<%=txtCropSurveyNo.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter Survey No');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtCropTaxNo.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter Tax No');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=txtCropTaxDate.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter Tax Date ');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=txtCropVillage.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter Village No');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtCropPcNo.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter Pc No.');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtCropPcDate.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Please Enter Pc Date');
            Ctl.focus();
            return false;
        }

    }

    function imgBatchDtls_onclick() {
        $('.gradientBoxesWithOuterShadows').append('<div id="resultdiv" title="Batch Details" visible="false" >');
        objDDl = document.getElementById('<%=CmbBatchNo.ClientID%>');
            var selval = objDDl.options[objDDl.selectedIndex].value;
            $("#resultdiv").load("BatchDtls.aspx?BatchNo=" + selval, {}, function (data) {
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
    </script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 21px;
        }
        .hiddencol
          {
            display: none;
          }
    </style>
</asp:Content>


<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                <Scripts>
            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
        </Scripts>
    </asp:ToolkitScriptManager>
       <script type="text/javascript">
                           Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
                           function BeginRequestHandler(sender, args) {

                               args.get_postBackElement().disabled = true;
                           }
</script>
 
        <asp:HiddenField ID="HdnDate" runat="server" />
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <%--<div id=""></div> <input type="hidden" id="hid_CustID" runat="server" />--%>
            <div id="cover"></div> 
    <div class="popup" id="VerifyMob" style="width:200px;height:100px; margin-left:60%; margin-top:250px; ">
          <div class="popupclose" onclick="hide('VerifyMob');hide('cover');"></div>
        <div style="float:left; width:100%; margin-top:5px;">
            <asp:TextBox runat="server" ID="txt_verifycode" Width="70%" style="margin-left:20px;" ></asp:TextBox>
            <input type="hidden" value="N" runat="server" id="Hid_ODPStatus" />
        </div>
          <div style="float:left; width:100%; margin-top:5px; ">
              <asp:Button ID="But_verify" runat="server" Text="Verify" style="margin-left:100px;" />
          </div>
         <div style="float:left; width:100%; margin-top:5px; ">
          <label id="lbl_sessionexp" style="font-size:11px; color:red; display:none;">Session Expired</label>
             <label id="lbl_errMsg" style="font-size:11px; color:red; display:none;">Enter Correct OTP Code</label> 
          </div>
    </div>  

<div   style="width: 100%; margin-top:1%;">                
  <asp:Panel ID="PnlAuthorise" runat="server">
    <table width="100%" >
      <tr>
        <td width="2%"></td>
        <td width="20%"><label>Branch</label></td>
        <td width="27%">
                <asp:DropDownList ID="CmbBranch" runat="server" Width="100%" TabIndex="999">
                </asp:DropDownList>
            </td>
	    <td width="2%"></td>
        <td width="20%"><label>Mode of Operation</label></td>
        <td width="27%">
                <asp:DropDownList ID="CmbOpMode" runat="server" Width="100%" TabIndex="999">
                </asp:DropDownList>
            </td>
        <td width="2%"></td>
      </tr>
      <tr>
        <td></td>
        <td><label>Scheme</label></td>
        <td>
                <asp:DropDownList ID="CmbScheme" runat="server" Width="100%" Enabled="False">
                </asp:DropDownList>
            </td>
        <td></td>
        <td><label>No of Operators</label></td>
        <td>
                <asp:TextBox ID="TxtNoOfOperator" runat="server" Width = "98%" onkeypress="return onlyNumbers(event)" TabIndex="999"></asp:TextBox>
            </td>
        <td></td>
      </tr>
    </table>
    <table width="100%" border="0">
        <tr>
            <td width="2%"></td>
            <td width="96%" style="padding-top:30px; padding-bottom:30px;">
            
                <asp:GridView ID="DgvCustomer" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="CustID" TabIndex="23" Width="100%" CssClass="grid-viewNew" >
                    <Columns>
                        <asp:BoundField DataField="MemDtls" HeaderText="Member Details">
                            
                        </asp:BoundField>
                        <asp:BoundField DataField="MemBrCode" HeaderText="Member BrCode" 
                           ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                        <asp:BoundField DataField="MemSchCode" HeaderText="MemSchCode" 
                            ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                        <asp:BoundField DataField="MemNo" HeaderText="Member No" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                           
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
            <td width="2%"></td>
        </tr>
        <tr>
            <td width="2%">&nbsp;</td>
            <td width="96%">
                <table class="style1">
                    <tr>
                        <td width="15%"> <asp:Label ID="LblPl" runat="server" Text="Eligible Loan Amount" Font-Bold="True" ForeColor="Red"></asp:Label></td>
                        <td width="25%"> <asp:Label ID="LblPLEligibleAmnt" runat="server" Text="0.00" Font-Bold="True" ForeColor="Red"></asp:Label></td>
                        <td width="30%">
                            <asp:HiddenField ID="HdnPLEligible" runat="server" />
                        </td>
                        <td width="30%">
                            <asp:HiddenField ID="HdnGLSecScheme" Value="N" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="2%">&nbsp;</td>
        </tr>
    </table>
    <br />

    <table width = "100%">
	<tr>
		<td width="2%"> </td>
		<td width="10%"> 
                            <asp:Label ID="Lbl_LoanDate" runat="server" Text="Loan Date"  CssClass="lblCSS"   ></asp:Label>
                      
        </td>
		<td width="20%"> 
                            <asp:TextBox ID="Txt_LoanDate" runat="server" BackColor="White" Width="97%"></asp:TextBox>
        </td>
		<td width="2%"> </td>
		<td width="10%"> 
                            <asp:Label ID="Lbl_Month" runat="server" CssClass="lblCSS"  
                                 Text="Month"></asp:Label>
                           
        </td>
		<td width="20%"> 
            <table width="100%"><tr>
                <td width="30%">
                     <asp:TextBox ID="Txt_Month" runat="server" AutoPostBack="True" onkeypress="return onlyNumbers(event)" style="text-align:right"  Width="100%" TabIndex="1"></asp:TextBox>
                </td>
                <td width="30%">
                    <asp:Label ID="Lbl_Days" runat="server" CssClass="lblCSS"  Text="Days" style="margin-left:19%" ></asp:Label>
                </td>
                <td width="30%">
                     <asp:TextBox ID="Txt_Days" runat="server" AutoPostBack="True" onkeypress="return onlyNumbers(event)" style="text-align:right"  Width="95%" TabIndex="2" ></asp:TextBox>
                </td>
                                </tr></table>
                            
                           
        </td>
		<td width="2%"> </td>
		<td width="10%"> 
                                <asp:Label ID="Lbl_DueDate0" runat="server" CssClass="lblCSS"  Text="Due Date"></asp:Label>
        </td>
		<td width="20%"> 
                                    <asp:TextBox ID="Txt_DueDate" runat="server"  Width="97%"></asp:TextBox>
        </td>
		<td width="2%"> </td>
	</tr>
	<tr>
		<td > &nbsp;</td>
		<td > 
                            <asp:Label ID="Lbl_Interest" runat="server" CssClass="lblCSS"  Text="Interest %"></asp:Label>
                            
        </td>
		<td > 
                                <table class="style1" width="100%">
                                    <tr>
                                        <td width="35%">
                                            <asp:TextBox ID="Txt_Interest" runat="server" 
                                onkeypress="return isFloatKey(event);" style="text-align:right" Tabindex="4" 
                                Width="97%"></asp:TextBox></td>
                                        <td width="30%" align="right">
                                            <label>IRR %</label></td>
                                        <td width="35%">
                                            <asp:TextBox ID="TxtIRR" runat="server" 
                                onkeypress="return isFloatKey(event);" style="text-align:right" Tabindex="4" 
                                Width="97%"></asp:TextBox></td>
                                    </tr>
                                </table>
        </td>
		<td > &nbsp;</td>
		<td> 
                                    <asp:Label ID="Lbl_PenalInterest" runat="server" CssClass="lblCSS"  Text="Penal %"></asp:Label>
        </td>
		<td> 
                                    <asp:TextBox ID="Txt_PenalInterest" runat="server" onkeypress="return isFloatKey(event);" style="text-align:right" Width="96%" TabIndex="6"></asp:TextBox>
        </td>
		<td > &nbsp;</td>
		<td > 
                                        <asp:Label ID="Lbl_InterestUpto" runat="server" CssClass="lblCSS"  Text="Interest Upto"></asp:Label>
        </td>
		<td > 
                                            <asp:TextBox ID="Txt_InterestUpto" runat="server"  Width="97%"></asp:TextBox>
        </td>
		<td > &nbsp;</td>
	</tr>
	<tr>
		<td > &nbsp;</td>
		<td > 
                            <asp:Label ID="Lbl_LoanAmount" runat="server" 
                Text="Loan Amount" CssClass="lblCSS" 
                        ></asp:Label>
        </td>
		<td > 
                                <asp:TextBox ID="Txt_LoanAmount" runat="server" 
                        AutoPostBack="True" 
                TabIndex="7" Width="97%"   onkeypress="return isFloatKey(event);" style="text-align:right"  ></asp:TextBox>
        </td>
		<td > &nbsp;</td>
		<td> 
                                    <asp:Label ID="Lbl_IssuedAmt" runat="server" CssClass="lblCSS"  Text="Issued Amt"></asp:Label>
        </td>
		<td > 
                                        <asp:TextBox ID="Txt_IssuedAmt" runat="server" AutoPostBack="True" onkeypress="return isFloatKey(event);" style="text-align:right"  Width="97%" TabIndex="8"></asp:TextBox>
        </td>
		<td > &nbsp;</td>
		<td > 
                                            <asp:Label ID="Lbl_Balance" runat="server" 
                Text="Balance Amount" CssClass="lblCSS" ></asp:Label>
        </td>
		<td > 
                                                <asp:TextBox ID="Txt_BalanceAmount" 
                runat="server" 
                           Width="97%"   onkeypress="return isFloatKey(event);" style="text-align:right" ></asp:TextBox>
        </td>
		<td > &nbsp;</td>
	</tr>
	<tr>
		<td > &nbsp;</td>
		<td > 
                            <asp:Label ID="Lbl_InstNo" runat="server" Text="Installment No" CssClass="lblCSS" 
                        ></asp:Label>
                            
        </td>
		<td > 
                                <table class="style1" width="100%">
                                    <tr>
                                        <td width="35%">
                                            <asp:TextBox ID="Txt_InstNo" runat="server" AutoPostBack="True" 
                                onkeypress="return isFloatKey(event);"  style="text-align:right" tabindex="10" 
                                Width="97%"></asp:TextBox></td>
                                        <td width="30%">
                                            <label>AdvInst</label></td>
                                        <td width="35%">
                                            <asp:TextBox ID="TxtAdvInst" Width="97%" runat="server"></asp:TextBox></td>
                                    </tr>
                                </table>
        </td>
		<td > &nbsp;</td>
		<td> 
                                    <asp:Label ID="Lbl_InstAmt" runat="server" 
                Text="Installment Amt" CssClass="lblCSS" ></asp:Label>
        </td>
		<td > 
                                        <asp:TextBox ID="Txt_InstAmt" runat="server" 
                         Width="97%"   onkeypress="return isFloatKey(event);" style="text-align:right" ></asp:TextBox>
        </td>
		<td > &nbsp;</td>
		<td > 
                                            <asp:Label ID="Lbl_Priority" runat="server" CssClass="lblCSS"  Text="Priority"></asp:Label>
        </td>
		<td > 
                                                <asp:DropDownList ID="Ddl_Priority" runat="server" Font-Names="Tahoma" Font-Size="Medium" ForeColor="#6666FF" TabIndex="16" Width="100%">
                                                </asp:DropDownList>
        </td>
		<td > &nbsp;</td>
	</tr>
	<tr>
		<td > &nbsp;</td>
		<td > 
                            <asp:Label ID="Lbl_Purpose" runat="server" Text="Purpose" 
                       CssClass="lblCSS" ></asp:Label>
        </td>
		<td > 
                                <asp:DropDownList ID="Ddl_Purpose" runat="server" Font-Names="Tahoma" 
                                    Font-Size="Medium" ForeColor="#6666FF" 
                Width="100%" AutoPostBack="True">
                                </asp:DropDownList>
        </td>
		<td > &nbsp;</td>
		<td> 
                                    <asp:Label ID="Lbl_SubPurpose" runat="server" Text="Sub Purpose" CssClass="lblCSS"  ></asp:Label>
        </td>
		<td > 
                                        <asp:DropDownList ID="Ddl_SubPurpose" 
                runat="server" Font-Names="Tahoma" 
                                            Font-Size="Medium" ForeColor="#6666FF" 
                 Width="100%" 
                                            TabIndex="13">
                                        </asp:DropDownList>
        </td>
		<td> &nbsp;</td>
		<td > 
                                            <asp:Label ID="Lbl_SanctionedBy" runat="server" CssClass="lblCSS" Text="Sanctioned By"></asp:Label>
        </td>
		<td > 
                                                <asp:DropDownList ID="Ddl_SanctionedBy" runat="server" Font-Names="Tahoma" Font-Size="Medium" ForeColor="#6666FF" TabIndex="15" Width="100%">
                                                </asp:DropDownList>
        </td>
		<td > &nbsp;</td>
	</tr>
	<tr>
		<td > &nbsp;</td>
		<td > 
                            <asp:Label ID="LblWrdNo" runat="server" CssClass="lblCSS"  Text="Ward No"></asp:Label>
        </td>
		<td > 
                                <asp:TextBox ID="Txt_wrdNo" runat="server" AutoPostBack="True" CssClass="twitterStyleTextbox" TabIndex="2" Width="97%"></asp:TextBox>
        </td>
		<td> &nbsp;</td>
		<td> 
                                    <asp:Label ID="Lbl_GrahanNo" runat="server" CssClass="lblCSS"  Text="Grahan No"></asp:Label>
        </td>
		<td > 
                                        <asp:TextBox ID="Txt_GrahanNo" runat="server" AutoPostBack="True" CssClass="twitterStyleTextbox" TabIndex="3" Width="97%"></asp:TextBox>
        </td>
		<td > &nbsp;</td>
		<td > 
                                            <asp:Label ID="Lbl_TockenNo" runat="server" CssClass="lblCSS"  Text="Sro No"></asp:Label>
        </td>
		<td > 
                                                <asp:TextBox ID="Txt_SroNo" runat="server" TabIndex="4" Width="97%"></asp:TextBox>
        </td>
		<td  align="center"> 
            &nbsp;</td>
	</tr>
        <tr>
            <td >
                &nbsp;</td>
            <td >
                <asp:Label ID="Lbl_Priority0" runat="server" CssClass="lblCSS"  Text="Resl No"></asp:Label>
            </td>
            <td >
                <asp:TextBox ID="Txt_ReslNo" runat="server" AutoPostBack="True" CssClass="twitterStyleTextbox" TabIndex="5" Width="97%"></asp:TextBox>
            </td>
            <td >
                &nbsp;</td>
            <td>
                <asp:Label ID="LblReslDt" runat="server" CssClass="lblCSS"  Text="Resl Date"></asp:Label>
            </td>
            <td >
                <asp:TextBox ID="Txt_ReslDt" runat="server" OnChange="validatedate(this);CompareDates();" TabIndex="6" Width="97%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="Txt_ReslDt">
                </asp:CalendarExtender>
            </td>
            <td align="center">
                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>
            </td>
            <td >
                <asp:Label ID="Lbl_Director" runat="server" CssClass="lblCSS"  Text="Director" 
                    Visible="False"></asp:Label>
            </td>
            <td >
                <asp:DropDownList ID="CmbDirector" runat="server" AutoPostBack="True" 
                    Font-Names="Tahoma" TabIndex="7" Font-Size="Medium" ForeColor="#6666FF" 
                    Width="100%" Visible="False">
                </asp:DropDownList>
            </td>
            <td >
                &nbsp;</td>
        </tr>
        <tr>
            <td >
                &nbsp;</td>
            <td  >
                <asp:Label ID="Lbl_CashTransfer" runat="server" CssClass="lblCSS"  Text="Cash/Transfer"></asp:Label>
         </td>
            <td >
            
                  
                  <table class="style1" width="100%">
                      <tr>
                          <td width="75%">
                            <asp:DropDownList ID="Ddl_CashTransfer" runat="server" AutoPostBack="True" Font-Names="Tahoma" Font-Size="Medium" ForeColor="#6666FF" TabIndex="8" Width="100%">
                    </asp:DropDownList>
                              &nbsp;</td>
                          <td width="25%">
                              <asp:CheckBox ID="ChkBank" runat="server" text="Bank Details" Enabled="false" 
                                  AutoPostBack="True"/>&nbsp;</td>
                      </tr>
                  </table>
            </td>
            <td >
              
                &nbsp;</td>
            <td>
                <asp:Label ID="lbl_BondNo" runat="server" CssClass="lblCSS"  Text="Bond No"></asp:Label>
            </td>
            <td >
                <asp:TextBox ID="Txt_BondNo" runat="server" AutoPostBack="True" CssClass="twitterStyleTextbox"  Width="97%"></asp:TextBox>
            </td>
            <td >
                &nbsp;</td>
            <td >
                <asp:Label ID="lbl_BondDate" runat="server" CssClass="lblCSS"  Text="Bond Date"></asp:Label>
            </td>
            <td >
                <asp:TextBox ID="Txt_BondDate" runat="server" AutoPostBack="True" CssClass="twitterStyleTextbox"  Width="97%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="Txt_BondDate">
                </asp:CalendarExtender>
            </td>
            <td >
                &nbsp;</td>
        </tr>
        <tr>
            <td ></td>
            <td >
                <asp:Label ID="Lbl_Remarks" runat="server" CssClass="lblCSS"  Text="Remarks"></asp:Label>
            </td>
            <td >
                <asp:TextBox ID="Txt_Remarks" runat="server" AutoPostBack="True" CssClass="twitterStyleTextbox" TabIndex="11" Width="99%"></asp:TextBox>
            </td>
            <td ></td>
             <td ><label>
                Mobile No</label></td>
             <td >
                 <asp:TextBox ID="Txt_MobileNo" runat="server" AutoPostBack="True" CssClass="twitterStyleTextbox" Width="77%" style="float:left;" Enabled="False"></asp:TextBox>
                 <div style="float:left;" id="div_Verify">
                     <asp:Button ID="Btnverfiy" runat="server" Text="Verify"   style="width:50px; height:20px;" /></div>        
            <img src="Images/verify.jpg" style=" float:left; display:none;" id="imgVerify" />
           <img src="Images/Verifyerror.jpg" style=" float:left;display:none;" id="imgverifyError" />
            </td>
            <td >
               </td>
            <td>
                <label>
                Nominee</label>
            </td>
            <td > <asp:DropDownList ID="CmbNomReq" runat="server" AutoPostBack="True" TabIndex="24" Width="100%">
                </asp:DropDownList></td>
            <td></td>
        </tr>
        <tr>
            <td >
                &nbsp;</td>
            <td >
                <label>
                Batch No</label></td>
            <td>
                <asp:DropDownList ID="CmbBatchNo" runat="server" AutoPostBack="True" 
                    Enabled="False" TabIndex="1" Width="80%">
                </asp:DropDownList>
                <asp:ImageButton ID="ImgBatch" runat="server" Enabled="False" 
                    ImageAlign="Middle" ImageUrl="~/Images/Node.png" 
                    OnClientClick="return imgBatchDtls_onclick();" />
            </td>
            <td >
            </td>
            <td>
                <asp:Label ID="LblCredit" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
            </td>
            <td >
                <asp:Label ID="LblDebit" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
            </td>
            <td >
                &nbsp;</td>
            <td >
                <asp:Label ID="Lbl_Cent" runat="server" CssClass="lblCSS" 
                     Text="Cent"></asp:Label>
            </td>
            <td >
                <asp:TextBox ID="Txt_Cent" runat="server" onkeypress="return isFloatKey(event);" AutoPostBack="True" 
                    style="text-align:right" TabIndex="12" 
                    Width="97%"></asp:TextBox>
            </td>
            <td >
                &nbsp;</td>
        </tr>
        <tr>
            <td >
                &nbsp;</td>
            <td ><label>Canvassed By</label></td>
            <td  colspan="4" >
                
              <%--  <input type="radio" id="che_surety" name="Security" onclick="show('DivSur'); hide('AddMem'); hide('DivOtherSuerties');"  /> <label for="che_surety">Surety</label> 
                 <input type="radio" id="Che_Security" name="Security" onclick="show('DivOtherSuerties'); hide('AddMem'); hide('DivSur');"  /> <label for="Che_Security">Security</label>
                 <input type="radio" id="Che_ADL_Member" name="Security" onclick="show('AddMem'); hide('DivOtherSuerties'); hide('DivSur');"  /> <label for="Che_ADL_Member">ADL Member</label>--%>
                <asp:DropDownList ID="CmbCanvassedBy" runat="server" TabIndex="28" Width="35%">
                </asp:DropDownList>
                </td>
            <td >
                &nbsp;</td>
            <td >
                <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                    Text="Processing Fee Change To Cash" />
            </td>
            <td >
                <asp:ImageButton ID="btnEMI" runat="server" Enabled="True" 
                    ImageUrl="~/Images/TrImgEMI.png" TabIndex="999" Visible="true" />
            </td>
            <td >
                &nbsp;</td>
        </tr>

        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td colspan="4">
             <div id="DivBankDetails"  runat="server" visible="false">
                                    
                                        <table class="style1" width="100%">
                                            <tr>
                                                <td width="25%" class="style2">
                                                    </td>
                                                <td width="50%" class="style2">
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
                </div></td>
                                                <td width="25%" class="style2">
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td width="25%">
                                                    &nbsp;</td>
                                                <td width="50%">
                                                <%--<div id="DivChq" class="gradientBoxesWithOuterShadows" >--%>
                                             
                                                    &nbsp;</td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    
                                    </div></td>
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
            <td>
                &nbsp;</td>
            <td>
                <label>
                Security</label>
            </td>
            <td colspan="4">
                <%--  <input type="radio" id="che_surety" name="Security" onclick="show('DivSur'); hide('AddMem'); hide('DivOtherSuerties');"  /> <label for="che_surety">Surety</label> 
                 <input type="radio" id="Che_Security" name="Security" onclick="show('DivOtherSuerties'); hide('AddMem'); hide('DivSur');"  /> <label for="Che_Security">Security</label>
                 <input type="radio" id="Che_ADL_Member" name="Security" onclick="show('AddMem'); hide('DivOtherSuerties'); hide('DivSur');"  /> <label for="Che_ADL_Member">ADL Member</label>--%>
                <asp:RadioButtonList ID="RadioSecurity" runat="server" AutoPostBack="True" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem Value="Surety">Surety</asp:ListItem>
                    <asp:ListItem Value="Security">Security</asp:ListItem>
                    <asp:ListItem Value="ADL Member">ADL Member</asp:ListItem>
                </asp:RadioButtonList>
                <asp:HiddenField ID="AccID" runat="server" Value="97" />
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
            <td >&nbsp;</td>
            <td colspan="8">
                <div id ="DivSur"  runat="server" visible="false"   class="gradientBoxesWithOuterShadows" style="width: 100%;  " >
<table width ="100%">
     <tr>
         <td width ="40%">
             <asp:HiddenField ID="HdnSurCustID" runat="server" />
              <asp:HiddenField ID="SurMemAccid" runat="server" />
        </td>
         <td width ="56%">
             <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtSurAccID" runat="server" AutoPostBack="True" TabIndex="13" 
                 Visible="false" Width="95%"></asp:TextBox>
         </td>
         <td width ="2%">
            </td>
     </tr>
<tr>
<td align="left">
     <legend>SURETY DETAILS</legend> 
    &nbsp;</td>
<td>
   
    &nbsp;</td>
<td>
    &nbsp;</td>

</tr>
     <tr>
         <td align="left">
             
             <label>
             Member Branch &amp; Scheme &amp; Member No</label></td>
         <td>
             <table width="100%">
                 <tr>
                     <td width="30%">
                         <asp:DropDownList ID="CmbSurBranch" runat="server" TabIndex="11" Width="100%">
                         </asp:DropDownList>
                     </td>
                     <td width="70%">
                         <table width="100%">
                             <tr>
                                 <td>
                                     <asp:DropDownList ID="CmbSurMemType" runat="server" AutoPostBack="True" TabIndex="12" Width="100%">
                                     </asp:DropDownList>
                                 </td>
                                 <td>
                                     <table width="100%">
                                         <tr>
                                             <td width="85%" class="auto-style1">
                                                 <asp:TextBox ID="TxtSurMemNo" runat="server" AutoPostBack="True" CssClass="twitterStyleTextbox" TabIndex="13" Width="95%"></asp:TextBox>
                                                 </td>
                                             <td class="auto-style1">
                                                 <asp:ImageButton ID="ImgSrch" runat="server" Height="24px" ImageUrl="~/images/Search.png" TabIndex="999" Width="23px" />
                                             </td>
                                             <td width="15%" class="auto-style1">
                                                 <asp:ImageButton ID="ImgBtnSureity" runat="server" ImageUrl="~/Images/AddBtn.png" onclientclick="return imgLiab_onclick();" TabIndex="32" Visible="false" />
                                             </td>
                                         </tr>
                                     </table>
                                 </td>
                             </tr>
                         </table>
                     </td>
                 </tr>
             </table>
         </td>
         <td></td>
     </tr>
     <tr>
         <td colspan="3">
             <label>
             Surety Details</label></td>
     </tr>
     <tr>
         <td colspan="3">
            <asp:GridView ID="DgvCustSur" runat="server" AutoGenerateColumns="False"  CssClass="grid-viewNew"
                 DataKeyNames="CustID" TabIndex="17" Width="100%">
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
                      <asp:BoundField DataField="Dob" HeaderText="Dob" 
                DataFormatString="{0:dd/MM/yyyy}">
                     
                     
 
                     </asp:BoundField>
                     <asp:BoundField DataField="Age" HeaderText="Age">
 
                     </asp:BoundField>
                <asp:CommandField DeleteText="&lt;img src='./images/del.jpg'&gt;" 
                    ShowDeleteButton="True" DeleteImageUrl="~/Images/DeleteN.png" 
                         EditImageUrl="~/Images/DeleteN.png" 
                         NewImageUrl="~/Images/DeleteN.png" SelectImageUrl="~/Images/DeleteN.png">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
                     
                 </Columns>
                 <AlternatingRowStyle BackColor="#F5F4FF" />
             </asp:GridView>
         </td>
     </tr>
     <tr>
         <td>
              <asp:HiddenField ID="HdnSurMemNo" runat="server" Visible="False" />
              </td>
         <td>
             </td>
         <td>
             </td>
     </tr>
    
 </table>    
     </div>
     
            <%--------------------------------------------------------------    --%>
  <div id ="DivOtherSuerties"  runat="server" class="gradientBoxesWithOuterShadows" style="width: 100%; "  visible="false" >
            <table width ="100%">             
                <tr>
                    <td  style="padding:20px 0px 20px 0px">
                        <asp:RadioButtonList ID="RadiOtherSuerties" runat="server" RepeatDirection="Horizontal" AutoPostBack="True">
                            <asp:ListItem Selected Value="Land And Building">Land And Building</asp:ListItem>
                            <asp:ListItem Value="Crops">Crops</asp:ListItem>
                             <asp:ListItem Value="Gold Ornaments">Gold Ornaments</asp:ListItem>
                             <asp:ListItem Value="Insurance">Insurance</asp:ListItem>
                             <asp:ListItem Value="Other Bonds">Other Bonds</asp:ListItem>
                             <asp:ListItem Value="Vehicle">Vehicle</asp:ListItem>
                            <asp:ListItem Value="Term Deposit">Term Deposit</asp:ListItem>
                            <asp:ListItem Value="Salary Slip" >Salary Slip</asp:ListItem>
                        </asp:RadioButtonList>
                    
                    </td>
                    
                </tr>
                                <tr>
                  <td style="padding-right:2%;" >
                        
                                    <div id="DivLandBuilding"  runat="server" >
                                        <table width="100%">
                                            <tr>
                                                <td width="13%">
                                                    <label>
                                                    Deed Details</label></td>
                                                <td width="13%"></td>
                                                <td width="2%"></td>
                                                <td width="13%">
                                                    <asp:CheckBox ID="ChkTax" runat="server" AutoPostBack="True" Text="Tax Details" />
                                                </td>
                                                <td width="13%"></td>
                                                <td width="2%"></td>
                                                <td width="13%">
                                                    <asp:CheckBox ID="ChkBuilding" runat="server" AutoPostBack="True" Text="Building Details" />
                                                </td>
                                                <td width="13%"></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                    No. :</label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtDeedNo" runat="server" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" Width="100%"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td>
                                                    <label>
                                                    No. :</label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtTaxNo" runat="server" CssClass="twitterStyleTextbox" Width="100%" Enabled="False"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td>
                                                    <label>
                                                   Building No. :</label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtBuildingNo" runat="server" CssClass="twitterStyleTextbox"  Width="100%" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                    Date. :</label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtDeedDate" runat="server" CssClass="twitterStyleTextbox" Width="100%" onchange="return validatedate(this);"   ></asp:TextBox>
                                                    <asp:CalendarExtender ID="TxtDeedDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="TxtDeedDate">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td></td>
                                                <td>
                                                    <label>
                                                    Date. :</label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtTaxDate" runat="server" CssClass="twitterStyleTextbox" Width="100%" Enabled="False" onchange="return validatedate(this);"  ></asp:TextBox>
                                                    <asp:CalendarExtender ID="TxtTaxDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="TxtTaxDate">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td></td>
                                                <td>
                                                    <label>
                                                    Ward No.. :</label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtWardNo" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                    
                                                </td>
                                            </tr>
                                            <tr> 
                                                <td>
                                                    <label>
                                                    Sub Reg Office. :</label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtSupRegOff" runat="server" CssClass="twitterStyleTextbox"  Width="100%"></asp:TextBox>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <label>
                                                    Possession CrtNo. :</label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtPossessionCrtNo" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <label>
                                                    Recepit No. :</label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtRecepitNo" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                    Village. :</label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtVillage" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                                <td>&nbsp;</td>
                                               <td>
                                                    <label>
                                                    Possession CrtDate:</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtPossessionCrtDate" runat="server" CssClass="twitterStyleTextbox" Width="100%" onchange="return validatedate(this);"  ></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtPossessionCrtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtPossessionCrtDate">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td><label>Recepit Date</label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtRecepitDate" runat="server" CssClass="twitterStyleTextbox" Width="100%" onchange="return validatedate(this);" ></asp:TextBox>
                                                    <asp:CalendarExtender ID="TxtRecepitDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="TxtRecepitDate">
                                                    </asp:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                    Panchayath/Muncipality. :</label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtPanchMunci" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                                <td>&nbsp;</td>
                                               <td>
                                                    <label>
                                                    Easy No :</label>
                                    </td>
                                               
                                                    <td>
                                                        <asp:TextBox ID="txtEasyNo" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <label>
                                                        GahanNo</label></td>
                                                    <td>
                                                        <asp:TextBox ID="TxtGahanNo" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                       
                                                    </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                            Tehsil. :</label></td>
                                        <td>
                                            <asp:TextBox ID="TxtTehsil" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <label>
                                            Easy Date</label></td>
                                        <td>
                                            <asp:TextBox ID="TxtEasyDate" runat="server" CssClass="twitterStyleTextbox" Width="100%" onchange="return validatedate(this);" ></asp:TextBox>
                                            <asp:CalendarExtender ID="EasyDateCalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="TxtEasyDate">
                                            </asp:CalendarExtender>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <label>
                                            Gahan Date</label></td>
                                        <td>
                                            <asp:TextBox ID="TxtGahanDate" runat="server" CssClass="twitterStyleTextbox" Width="100%" onchange="return validatedate(this);"  ></asp:TextBox>
                                            <asp:CalendarExtender ID="TxtGahanDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="TxtGahanDate">
                                            </asp:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                            District
                                            </label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtDistrict" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <label>
                                            Loacation Map No</label></td>
                                        <td>
                                            <asp:TextBox ID="TxtLoacationMapNo" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                            
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <label>
                                            PofAtNo</label></td>
                                        <td>
                                            <asp:TextBox ID="TxtPofAtNo" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                            Survey No. :</label></td>
                                        <td>
                                            <asp:TextBox ID="TxtSurveyNo" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <label>
                                            Loacation Map Date :</label></td>
                                        <td>
                                            <asp:TextBox ID="txtLoacationMapDate" runat="server" CssClass="twitterStyleTextbox" onchange="return validatedate(this);"  Width="100%"></asp:TextBox>
                                            <asp:CalendarExtender ID="txtLoacationMapDateCalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtLoacationMapDate">
                                            </asp:CalendarExtender>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <label>
                                            PofAtNoDate</label></td>
                                        <td>
                                            <asp:TextBox ID="TxtPofatNoDate" runat="server" CssClass="twitterStyleTextbox" Width="100%" onchange="return validatedate(this);"  ></asp:TextBox>
                                            <asp:CalendarExtender ID="TxtPofatNoDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="TxtPofatNoDate">
                                            </asp:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                            Extend. :</label></td>
                                        <td>
                                            <asp:TextBox ID="TxtExtend" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>
                                            </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                            Market Value. :</label></td>
                                        <td>
                                            <asp:TextBox ID="TxtMarketValue" runat="server" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);"  Width="100%"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:ImageButton ID="ImgBtnAddLand" runat="server" ImageUrl="~/Images/AddBtn.png" OnClientClick="return ValidateLand() " />
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td  colspan="8">
                                            <asp:GridView ID="DgvLand" runat="server" AutoGenerateColumns="False" CssClass="grid-viewNew" Width="100%">
                                                <Columns>
                                                    <asp:BoundField DataField="Tax" HeaderText="Tax" Visible="False" />
                                                    <asp:BoundField DataField="BuildingNo" HeaderText="Building No" Visible="False" />
                                                    <asp:BoundField DataField="DeedNo" HeaderText="DeedNo" />
                                                    <asp:BoundField DataField="DeedDate" HeaderText="Deed Date" Visible="true" />
                                                    <asp:BoundField DataField="WardNo" HeaderText="Ward No" Visible="true" />
                                                    <asp:BoundField DataField="Extend" HeaderText="Extend" Visible="true" />
                                                    <asp:BoundField DataField="District" HeaderText="District" Visible="true" />
                                                    <asp:BoundField DataField="Tehsil" HeaderText="Tehsil" Visible="true" />
                                                    <asp:BoundField DataField="Panchayath" HeaderText="Panchayath" Visible="true" />
                                                    <asp:BoundField DataField="Village" HeaderText="Village" Visible="true" />
                                                    <asp:BoundField DataField="SubRegOffice" HeaderText="Sub Reg Office" Visible="true" />
                                                    <asp:BoundField DataField="MarketValue" HeaderText="Market Value" Visible="true" />
                                                    <asp:BoundField DataField="TaxNo" HeaderText="Tax No" Visible="False" />
                                                    <asp:BoundField DataField="TaxDate" HeaderText="Tax Date" Visible="False" />
                                                    <asp:BoundField DataField="SurveyNo" HeaderText="Tax Details" Visible="False" />
                                                    <asp:BoundField DataField="PossessionCrtNo" HeaderText="Possession Crt No" Visible="False" />
                                                    <asp:BoundField DataField="PossessionCrtDate" HeaderText="PossessionCrtDate " Visible="False" />
                                                    <asp:BoundField DataField="EasyNo" HeaderText="EasyNo" Visible="False" />
                                                    <asp:BoundField DataField="EasyDate" HeaderText="EasyDate" Visible="False" />
                                                    <asp:BoundField DataField="LoactionMapNo" HeaderText="LoactionMapNo" Visible="False" />
                                                    <asp:BoundField DataField="LocationMapdate" HeaderText="LocationMapdate" Visible="False" />
                                                    <asp:BoundField DataField="RecepitNo" HeaderText="RecepitNo" Visible="False" />
                                                    <asp:BoundField DataField="RecepitDate" HeaderText="RecepitDate" Visible="False" />
                                                    <asp:CommandField ShowDeleteButton="True" />
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                            
                                        </table>
                                    </div>
                                    <div id="DivCrops"  runat="server" visible="false">
                                        
                                        <table width="100%">
                                            <tr>
                                                <td width="15%"></td>
                                                <td width="15%"></td>
                                                <td width="2%"></td>
                                                <td width="15%">&nbsp;</td>
                                                <td width="15%">&nbsp;</td>
                                                <td width="2%">&nbsp;</td>
                                                <td width="15%"></td>
                                                <td width="15%"></td>
                                               
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                    Crop Name :</label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtCropName" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <label>
                                                    Extend :</label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtCropExtend" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td><label>SurveyNo</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtCropSurveyNo" runat="server" Width="100%" onkeypress="return isFloatKey(event);" ></asp:TextBox>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                    TaxNo :</label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtCropTaxNo" runat="server" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);"  Width="100%"></asp:TextBox>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <label>
                                                    Tax Date</label>&nbsp;</td>
                                                <td>
                                                    <asp:TextBox ID="txtCropTaxDate" runat="server" CssClass="twitterStyleTextbox" Width="100%" onchange="return validatedate(this);" ></asp:TextBox>
                                                <asp:CalendarExtender ID="txtCropTaxDateCalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtCropTaxDate">
                                            </asp:CalendarExtender>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td><label>Village</label></td>
                                                <td> <asp:TextBox ID="txtCropVillage" runat="server"   Width="100%"></asp:TextBox>
                                               </td>
                                                
                                            </tr>
                                            
                                            <tr>
                                                <td>
                                                    <label>Pc No</label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtCropPcNo" runat="server" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);"  Width="100%"></asp:TextBox>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <label>
                                                    Pc Date</label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtCropPcDate" runat="server" CssClass="twitterStyleTextbox" Width="100%" onchange="return validatedate(this);" ></asp:TextBox>
                                                     <asp:CalendarExtender ID="TxtCropPcDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="TxtCropPcDate">
                                            </asp:CalendarExtender>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <asp:ImageButton ID="ImgBtnCropAdd" runat="server" ImageUrl="~/Images/AddBtn.png" OnClientClick="return validatecrop()" />
                                                </td>
                                                <td>
                                                
                                                </td>
                                            </tr>
                                           
                                            <tr>
                                                <td colspan="8">
                                                    <asp:GridView ID="DgvCrop" runat="server" AutoGenerateColumns="False" CssClass="grid-viewNew" Width="100%">
                                                        <Columns>
                                                            <asp:BoundField DataField="CropName" HeaderText="Crop Name" />
                                                            <asp:BoundField DataField="SurveyNo" HeaderText="Survey No." />
                                                            <asp:BoundField DataField="Extend" HeaderText="Extend" />
                                                             <asp:BoundField DataField="TaxNo" HeaderText="Tax No" />
                                                            <asp:BoundField DataField="TaxDate" HeaderText="Tax Date" />
                                                            <asp:BoundField DataField="Village" HeaderText="Village" />
                                                             <asp:BoundField DataField="PcNo" HeaderText="Pc No." />
                                                            <asp:BoundField DataField="PcDate" HeaderText="Pc Date" />
                                                            <asp:CommandField ShowDeleteButton="True" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                   

                         <div id="DivSalary"  runat="server" visible="false">
                                        
                                        <table width="100%">
                                            <tr>
                                                <td width="15%"></td>
                                                <td width="15%"></td>
                                                <td width="2%">&nbsp;</td>
                                                <td width="15%">&nbsp;</td>
                                                <td width="15%">&nbsp;</td>
                                                <td width="2%"></td>
                                                <td width="15%"></td>
                                                <td width="15%"></td>
                                               
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                    Certi. No :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtCertiNo" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <label>
                                                    FirmName :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtFirmName" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <label>
                                                    Branch</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtBranch" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                    Employee Name :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtEmployeeName" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <label>
                                                    Designation :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtDesignation" runat="server" CssClass="twitterStyleTextbox"  Width="100%"></asp:TextBox>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <label>
                                                    Join Date :</label>&nbsp;</td>
                                                <td> 
                                                    <asp:TextBox ID="txtJoinDate" runat="server" CssClass="twitterStyleTextbox" onchange="return validatedate(this);"  Width="100%"></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtJoinDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtJoinDate">
                                                    </asp:CalendarExtender>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                    Basic Salary :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txt_basicSalary" runat="server" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);"  Width="100%"></asp:TextBox>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <label>
                                                    Net Pay :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtNetPay" runat="server" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" Width="100%"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td>
                                                    <label>
                                                    Gross Pay :</label></td>
                                                <td>
                                                     <asp:TextBox ID="txtGrossPay" runat="server" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" Width="100%"></asp:TextBox>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                    Retirement Date</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtRetirement" runat="server" CssClass="twitterStyleTextbox" onchange="return validatedate(this);" Width="100%"></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtRetirementCalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtRetirement">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <label>
                                                    Service Period :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtSerPeriod" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <label>
                                                    Issuing Authority</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtissuingAuthority" runat="server" CssClass="twitterStyleTextbox"  Width="100%"></asp:TextBox>
                                                 
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                    Issue Date</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtissueDate" runat="server" CssClass="twitterStyleTextbox" onchange="return validatedate(this);"  Width="100%"></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtissueDateCalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtissueDate">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <label>
                                                    Issuing Authority Address</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtissauthAddress" runat="server" CssClass="twitterStyleTextbox"  Width="100%"></asp:TextBox>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td> 
                                                    &nbsp;</td>
                                            </tr>
                                             <tr>
                                                <td >
                                                    <label>
                                                    Remarks</label></td>
                                                <td colspan="3" >
                                                    <asp:TextBox ID="txtsalaryRemarks" runat="server" CssClass="twitterStyleTextbox"  Width="100%"></asp:TextBox>
                                                     </td>
                                                 <td >
                                                     <asp:ImageButton ID="imgAddSalary" runat="server" ImageUrl="~/Images/AddBtn.png" OnClientClick="return validatecrop()" />
                                                 </td>
                                                 <td ></td>
                                                <td >&nbsp;</td>
                                                <td > 
                                                    &nbsp;</td>
                                            </tr>
                                             <tr>
                                                <td>&nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>&nbsp;</td>
                                                 <td>&nbsp;</td>
                                                 <td>&nbsp;</td>
                                                 <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td> 
                                                    &nbsp;</td>
                                            </tr>
                                             <tr>
                                                <td>&nbsp;</td>
                                                <td colspan="6">
                                                    &nbsp;</td>
                                                <td> &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="8">
                                                    <asp:GridView ID="Grid_Salary" runat="server" AutoGenerateColumns="False" CssClass="grid-viewNew" Width="100%">
                                                        <Columns>
                                                            <asp:BoundField DataField="CertiNo" HeaderText="Cert. No" />
                                                            <asp:BoundField DataField="FirmName" HeaderText="Firm Name" />
                                                            <asp:BoundField DataField="Branch" HeaderText="Branch" />
                                                            <asp:BoundField DataField="EmployeeName" HeaderText="Emp. Name" />
                                                             <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                             <asp:BoundField DataField="JoinDate" HeaderText="JoinDate" />
                                                             <asp:BoundField DataField="BasicSal" HeaderText="Basic Salary" />
                                                            <asp:CommandField ShowDeleteButton="True" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="DivGold"  runat="server" visible="false"  >
                                        
                                        <table width="100%">
                                            <tr>
                                                <td width="13%">
                                                    <label>
                                                    Gold Ornaments</label></td>
                                                <td width="13%"></td>
                                                <td width="2%"></td>
                                                <td width="13%"></td>
                                                <td width="13%"></td>
                                                <td width="2%"></td>
                                                <td width="13%"></td>
                                                <td width="13%"></td>
                                            </tr>
                                            <tr>
                                                <td width="13%">
                                                    <label>
                                                    Ornaments Type</label></td>
                                                <td width="13%">
                                                    <asp:DropDownList ID="CmbOrnamentType" runat="server" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="2%"></td>
                                                <td width="13%">
                                                    <label>
                                                    Count</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtCount" runat="server" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="2%"></td>
                                                <td width="13%">
                                                    <label>
                                                    Remarks</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtRemarks" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="13%">
                                                    <label>
                                                    Gross Weight</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtGrossWeight" runat="server" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="2%"></td>
                                                <td width="13%">
                                                    <label>
                                                    Net Weight</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtNetWeight" runat="server" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="2%"></td>
                                                <td width="13%">
                                                    <asp:ImageButton ID="ImgBtnOrnament" runat="server" ImageUrl="~/Images/AddBtn.png" />
                                                </td>
                                                <td width="13%"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="8">
                                                    <asp:GridView ID="DgvOrnament" runat="server" AutoGenerateColumns="False" CssClass="grid-viewNew" Width="100%">
                                                        <Columns>
                                                            <asp:BoundField DataField="OrnamentType" HeaderText="Ornament Type" Visible="False" />
                                                            <asp:BoundField DataField="OrnamentDtls" HeaderText="Ornament">
                                                            <HeaderStyle Width="35%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="GoldCount" HeaderText="Count">
                                                            <HeaderStyle Width="5%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                            <HeaderStyle Width="20%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NetWeight" HeaderText="Net weight">
                                                            <HeaderStyle Width="15%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="GrossWeight" HeaderText="Gross Weight">
                                                            <HeaderStyle Width="20%" />
                                                            </asp:BoundField>
                                                            <asp:CommandField ShowDeleteButton="True">
                                                            <HeaderStyle Width="5%" />
                                                            </asp:CommandField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="DivInsurance"  runat="server" visible="false"   >
                                       
                                        <table width="100%">
                                            <tr>
                                                <td width="13%">    
                                                    <label>
                                                    Insurance</label></td>
                                                <td width="13%"></td>
                                                <td width="2%"></td>
                                                <td width="13%"></td>
                                                <td width="13%"></td>
                                                <td width="2%"></td>
                                                <td width="13%"></td>
                                                <td width="13%"></td>
                                            </tr>
                                            <tr>
                                                <td width="13%">
                                                    <label>
                                                    Insurance Company :</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtInsuranceCompany" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="2%"></td>
                                                <td width="13%">
                                                    <label>
                                                    Policy Name :</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtPolicyName" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="2%"></td>
                                                <td width="13%">
                                                    <label>
                                                    PolicyNo :</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtPolicyNo" runat="server" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="13%">
                                                    <label>
                                                    Sum Assured</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtSumAssured" runat="server" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="2%"></td>
                                                <td width="13%">
                                                    <label>
                                                    Date Of Maturity</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtDateOfMaturity" runat="server" CssClass="twitterStyleTextbox" onchange="return validatedate(this);" Width="100%"></asp:TextBox>
                                                    <asp:CalendarExtender ID="TxtDateOfMaturity_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="TxtDateOfMaturity">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td width="2%"></td>
                                                <td width="13%">
                                                    <label>
                                                    Surrender Value</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtSurrenderValue" runat="server" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td width="13%">
                                                    &nbsp;</td>
                                                <td width="13%">
                                                    &nbsp;</td>
                                                <td width="2%"></td>
                                                <td width="13%">
                                                    &nbsp;</td>
                                                <td width="13%">
                                                    <asp:ImageButton ID="ImgBtnInsurance" runat="server" ImageUrl="~/Images/AddBtn.png" OnClientClick="return ValidateSaveinsurance()" />
                                                </td>
                                                <td width="2%"></td>
                                                <td width="13%"></td>
                                                <td width="13%"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="8">
                                                    <asp:GridView ID="DgvInsurance" runat="server" AutoGenerateColumns="False" CssClass="grid-viewNew" Width="100%">
                                                        <Columns>
                                                            <asp:BoundField DataField="InsuranceCompany" HeaderText="Insurance Company" />
                                                            <asp:BoundField DataField="PolicyName" HeaderText="Policy Name" />
                                                            <asp:BoundField DataField="PolicyNo" HeaderText="Policy No" />
                                                            <asp:BoundField DataField="SumAssured" HeaderText="Sum Assured" />
                                                            <asp:BoundField DataField="MaturityDate" HeaderText="Date Of maturity" />
                                                            <asp:BoundField DataField="SurrenderValue" HeaderText="Surrender Value" />
                                                            <asp:CommandField ShowDeleteButton="True" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="DivBond"  runat="server" visible="false" >
                                       
                                        <table width="100%">
                                            <tr>
                                                <td width="13%">
                                                    <label>
                                                    Other Bonds</label></td>
                                                <td width="13%"></td>
                                                <td width="2%"></td>
                                                <td width="13%"></td>
                                                <td width="13%"></td>
                                                <td width="2%"></td>
                                                <td width="13%"></td>
                                                <td width="13%"></td>
                                            </tr>
                                            <tr>
                                                <td width="13%">
                                                    <label>
                                                    Issued Post Office :</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtIssuedPostOffice" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="2%"></td>
                                                <td width="13%"><label>
                                                    Serial No :</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtSerialNo" runat="server" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="2%"></td>
                                                <td width="13%"><label>
                                                    Phase Value :</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtPhaseValue" runat="server" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="13%">
                                                    <label>
                                                    Due Date :</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtDueDate0" runat="server" CssClass="twitterStyleTextbox" onchange="return validatedate(this);" Width="100%"></asp:TextBox>
                                                    <asp:CalendarExtender ID="TxtDueDate0_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="TxtDueDate0">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td width="2%"></td>
                                                <td width="13%"> <label>
                                                    Total Phase Value :</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtTotalPhaseValue" runat="server" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="2%"></td>
                                                <td width="13%">
                                                    <asp:ImageButton ID="ImgBtnBond" runat="server" ImageUrl="~/Images/AddBtn.png" OnClientClick="return ValidateSaveBond()" />
                                                </td>
                                                <td width="13%"></td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="8">
                                                    <asp:GridView ID="DgvBond" runat="server" AutoGenerateColumns="False" CssClass="grid-viewNew" Width="100%">
                                                        <Columns>
                                                            <asp:BoundField DataField="IssuedPo" HeaderText="Issued Post Office" />
                                                            <asp:BoundField DataField="SerialNo" HeaderText="Serial No" />
                                                            <asp:BoundField DataField="PhaseValue" HeaderText="Phase Value" />
                                                            <asp:BoundField DataField="DueDate" HeaderText="Due Date" />
                                                            <asp:BoundField DataField="TotPhaseVal" HeaderText="Total Phase Value" />
                                                            <asp:CommandField ShowDeleteButton="True" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="DivVehicle"  runat="server" visible="false"     >
                                  
                                        <table width="100%">
                                            <tr>
                                                <td width="13%">
                                                    <label>
                                                    Vehicle</label></td>
                                                <td width="13%"></td>
                                                <td width="2%"></td>
                                                <td width="13%"></td>
                                                <td width="13%"></td>
                                                <td width="2%"></td>
                                                <td width="13%"></td>
                                                <td width="13%"></td>
                                            </tr>
                                            <tr>
                                                <td width="13%">
                                                    <label>
                                                    Type :</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtType" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="2%"></td>
                                                <td width="13%">
                                                    <label>
                                                    RTOffice :</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtRTOffice" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="2%"></td>
                                                <td width="13%">
                                                    <label>
                                                    Tax Details:</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtTextDetails" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="13%">
                                                    <label>
                                                    Region No :</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtRegionNo" runat="server" CssClass="twitterStyleTextbox"  Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="2%"></td>
                                                <td width="13%">
                                                    <label>
                                                    Lien Marked In RC :</label></td>
                                                <td width="13%">
                                                    <asp:CheckBox ID="ChkLienMarked" runat="server" AutoPostBack="True" />
                                                </td>
                                                <td width="2%"></td>
                                                <td width="13%">
                                                    <label>
                                                    Tax Date :</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtTextDate" runat="server" onchange="return validatedate(this);" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                    <asp:CalendarExtender ID="TxtTextDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="TxtTextDate">
                                                    </asp:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="13%">
                                                    <label>
                                                    Chassis No
                                                    </label>
                                                </td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtChassisNo" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="2%"></td>
                                                <td width="13%">
                                                    <label>
                                                    Lien No :</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtLienNo" runat="server" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="2%"></td>
                                                <td width="13%">
                                                    <label>
                                                    Insurance Details :</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtInsuranceDetails" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="13%">
                                                    <label>
                                                    Engine No :</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtEngineNo" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="2%"></td>
                                                <td width="13%">
                                                    <label>
                                                    Lien Date :</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="txtLienDate" runat="server" CssClass="twitterStyleTextbox" onchange="return validatedate(this);" Width="100%"></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtLienDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtLienDate">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td width="2%"></td>
                                                <td width="13%">
                                                    <label>
                                                    Insurance Date :</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtInsuranceDate0" runat="server" onchange="return validatedate(this);" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                    <asp:CalendarExtender ID="TxtInsuranceDate0_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="TxtInsuranceDate0">
                                                    </asp:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="13%">
                                                    <label>
                                                    Old/New :</label></td>
                                                <td width="13%"></td>
                                                <td width="2%"></td>
                                                <td width="13%">
                                                    <label>
                                                    Lien Released Letter Date :</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtLienReleased" runat="server" onchange="return validatedate(this);" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                    <asp:CalendarExtender ID="TxtLienReleased_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="TxtLienReleased">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td width="2%"></td>
                                                <td width="13%">
                                                    <label>
                                                    Insurance Value :</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtInsuranceValue" runat="server" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="13%">
                                                    <asp:RadioButton ID="RadOld" runat="server" AutoPostBack="True" Checked="True" GroupName="Main" Text="Old" Width="100%" />
                                                </td>
                                                <td width="13%">
                                                    <asp:RadioButton ID="RadNew" runat="server" AutoPostBack="True" GroupName="Main" Text="New" />
                                                </td>
                                                <td width="2%"></td>
                                                <td width="13%">
                                                    <label>
                                                    Brand :</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtBrand" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="2%"></td>
                                                <td width="13%">
                                                    <label>
                                                    Model :</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtModel" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="13%">
                                                    <label>
                                                    Key Submited :</label></td>
                                                <td width="13%">
                                                    <asp:CheckBox ID="ChkKeySubmited" runat="server" AutoPostBack="True" />
                                                </td>
                                                <td width="2%"></td>
                                                <td width="13%">
                                                    <label>
                                                    Key No :</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtKeyNo" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="2%"></td>
                                                <td width="13%">
                                                    <label>
                                                    Permit Scope :</label></td>
                                                <td width="13%">
                                                    <asp:TextBox ID="TxtPermitScope" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="13%">
                                                    <label>
                                                    Other Details :</label></td>
                                                <td colspan="3" style="width: 26%">
                                                    <asp:TextBox ID="TxtOtherDetails0" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="13%">
                                                    <asp:ImageButton ID="ImgBtnVehicle" runat="server" ImageUrl="~/Images/AddBtn.png" OnClientClick="return ValidateaddVehicle()" />
                                                </td>
                                                <td width="2%"></td>
                                                <td width="13%">&nbsp;</td>
                                                <td width="13%"></td>
                                            </tr>
                                            
                                           
                                           
                                            <tr>
                                                <td colspan="8">
                                                    <asp:GridView ID="DgvVehicle" runat="server" AutoGenerateColumns="False" CssClass="grid-viewNew" Width="100%">
                                                        <Columns>
                                                            <asp:BoundField DataField="VType" HeaderText="Vehicle Type" />
                                                            <asp:BoundField DataField="RegionNo" HeaderText="Region No" Visible="False" />
                                                            <asp:BoundField DataField="ChasisNo" HeaderText="Chasis No" />
                                                            <asp:BoundField DataField="EngineNo" HeaderText="Engine No" Visible="False" />
                                                            <asp:BoundField DataField="OldNew" HeaderText="Old/New" Visible="False" />
                                                            <asp:BoundField DataField="Brand" HeaderText="Brand" />
                                                            <asp:BoundField DataField="Model" HeaderText="Model" Visible="False" />
                                                            <asp:BoundField DataField="KeySub" HeaderText="Key Submitted" Visible="False" />
                                                            <asp:BoundField DataField="KeyNo" HeaderText="Key No" Visible="False" />
                                                            <asp:BoundField DataField="PermitScope" HeaderText="Permit Scope" Visible="False" />
                                                            <asp:BoundField DataField="RtOffice" HeaderText="Rt Office" Visible="False" />
                                                            <asp:BoundField DataField="LienMarked" HeaderText="Lien Marked In RC" />
                                                            <asp:BoundField DataField="LienNo" HeaderText="Lien No" />
                                                            <asp:BoundField DataField="LienDate" HeaderText="Lien Date" />
                                                            <asp:BoundField DataField="LienReleased" HeaderText="Lien Released Letter Date" Visible="False" />
                                                            <asp:BoundField DataField="TextDetails" HeaderText="Text Details" Visible="False" />
                                                            <asp:BoundField DataField="TaxDate" HeaderText="Tax Date" />
                                                            <asp:BoundField DataField="InsuranceDetails" HeaderText="Insurance Details" Visible="False" />
                                                            <asp:BoundField DataField="InsuranceDate" HeaderText="Insurance Date" />
                                                            <asp:BoundField DataField="InsuranceValue" HeaderText="Insurance Value" />
                                                            <asp:BoundField DataField="OtherDetails" HeaderText="Other Details" Visible="False" />
                                                            <asp:CommandField ShowDeleteButton="True" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="DivTermDeposit" runat="server" visible="false" >
                                       
                                        <table width="100%">
                                            <tr>
                                                <td width="13%">
                                                    <label>
                                                    Term Deposits</label></td>
                                                <td width="15%"></td>
                                                <td width="2%"></td>
                                                <td width="1%"></td>
                                                <td width="13%"></td>
                                                <td width="2%"></td>
                                                <td width="13%"></td>
                                                <td width="11%"></td>

                                            </tr>
                                            <tr>
                                                <td >
                                                    <asp:CheckBox ID="ChkOtherBankAc" runat="server" AutoPostBack="True" Text="Other Bank A/c" />
                                                </td>
                                                <td ></td>
                                                <td></td>
                                                <td>
                                                    <label>
                                                    Bank Name :</label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtSecBankName" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td>
                                                    <label>
                                                    Branch :</label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtSecTDBrName" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                    <asp:Label ID="lblbranch" runat="server" Text="Branch"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="CmbBranch1" runat="server" AutoPostBack="True" TabIndex="4" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td >
                                                    <label>
                                                    <asp:Label ID="lbl_AccType" runat="server" Text="Acc Type"></asp:Label>
                                                    </label>
                                                </td>
                                                <td >
                                                    <asp:TextBox ID="TxtAcType" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                                <td >&nbsp;</td>
                                                <td >
                                                    <label>
                                                    <asp:Label ID="Lbl_Certificate" runat="server" Text="Certificate.No"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TxtCertificateNo" runat="server" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                    <asp:Label ID="lbl_Scheme" runat="server" Text="Scheme"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DdlScheme" runat="server" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td >&nbsp;</td>
                                                <td>
                                                    <label>
                                                    A/c No :</label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtAcNo" runat="server" AutoPostBack="True" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td>
                                                    <label>
                                                    Customer No :</label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtCustomerNo" runat="server" CssClass="twitterStyleTextbox" onkeypress="return isFloatKey(event);" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                    Deposite Amt :</label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtDepositeAmt" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <label>
                                                    Amt Outstanding :</label></td>
                                                <td >
                                                    <asp:TextBox ID="TxtAmtOutstanding" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                                <td ></td>
                                                <td >
                                                    <label>
                                                    Due Date</label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtDueDate5" runat="server" CssClass="twitterStyleTextbox" onchange="return validatedate(this);" Width="100%"></asp:TextBox>
                                                    <asp:CalendarExtender ID="TxtDueDate5_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="TxtDueDate5">
                                                    </asp:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td >
                                                    <label>
                                                    Name :</label></td>
                                                <td >
                                                    <asp:TextBox ID="TxtSecTD_CustName" runat="server" CssClass="twitterStyleTextbox" Width="100%"></asp:TextBox>
                                                </td>
                                                <td ></td>
                                                <td >
                                                    <label>
                                                    Address :</label></td>
                                                <td colspan="3" >
                                                    <asp:TextBox ID="TxtTDAddress" runat="server" CssClass="twitterStyleTextbox" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                </td>
                                                <td >
                                                    <asp:ImageButton ID="ImgBtnTermDeposit" runat="server" ImageUrl="~/Images/AddBtn.png" OnClientClick="return ValidatetermDeposit()" />
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="8">
                                                    <asp:GridView ID="DgvDeposit" runat="server" AutoGenerateColumns="False" CssClass="grid-viewNew" Width="100%">
                                                        <Columns>
                                                            <asp:BoundField DataField="OtherBankAC" HeaderText="Other Bank A/C" Visible="False" />
                                                            <asp:BoundField DataField="Branch1" HeaderText="Branch" visible="false" />
                                                            <asp:BoundField DataField="BranchDtls1" HeaderText="Branch" />
                                                            <asp:BoundField DataField="Scheme" HeaderText="Scheme" visible="false" />
                                                            <asp:BoundField DataField="SchemeDtls" HeaderText="Scheme" />
                                                            <asp:BoundField DataField="AcNo" HeaderText="A/C No" />
                                                            <asp:BoundField DataField="CertificateNo" HeaderText="Certificate No" Visible="False" />
                                                            <asp:BoundField DataField="DepositAmt" HeaderText="Deposit Amount" />
                                                            <asp:BoundField DataField="AmtOutstanding" HeaderText="Amt Outstanding" />
                                                            <asp:BoundField DataField="DueDate" HeaderText="Due Date" />
                                                            <asp:BoundField DataField="BankName" HeaderText="Bank Name" Visible="False" />
                                                            <asp:BoundField DataField="Branch" HeaderText="Branch" />
                                                            <asp:BoundField DataField="AcType" HeaderText="A/C Type" Visible="False" />
                                                            <asp:BoundField DataField="CustomerNo" HeaderText="Customer No" />
                                                            <asp:BoundField DataField="Name" HeaderText="Name" Visible="False" />
                                                            <asp:BoundField DataField="Address" HeaderText="Address" Visible="False" />
                                                            <asp:CommandField ShowDeleteButton="True" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                               
                                    </td>
                            </tr>
                        </table>
                    
                    
</div>

                <%-- --------------------------------- --%>

<div id ="AddMem"  runat="server" visible="false"   class="gradientBoxesWithOuterShadows" style="width:100%;  " >
<table width ="100%">
     <tr>
         <td width ="2%">
            </td>
         <td width ="40%">
             <asp:HiddenField ID="HiddenSurCustid" runat="server" Value="0"/>
        </td>
         <td width ="56%">
             <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtSurAddMemAccid" 
                 runat="server" AutoPostBack="True" TabIndex="13" 
                 Visible="False" Width="95%"></asp:TextBox>
         </td>
         <td width ="2%">
            </td>
     </tr>
<tr>
<td> </td>
<td align="left">
    <label>Member Branch &amp; Scheme &amp; Member No</label></td>
<td>
    <table width="100%">
    <tr>
    <td width="30%">
      <asp:DropDownList ID="CmbAdMemBranch" runat="server" Width="100%" TabIndex="11">
                </asp:DropDownList></td>
    <td width="70%">
   <table width ="100%">
   <tr>
   <td>
   <asp:DropDownList ID="CmbSurMem" runat="server" Width="100%" TabIndex="12" 
                    AutoPostBack="True">
                </asp:DropDownList>
   </td>
   <td>
   


       <table width="100%">
           <tr>
               <td width="85%">
                   <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtSurMem" runat="server" 
                       AutoPostBack="True" TabIndex="13" 
                       Width="95%" Wrap="False"></asp:TextBox>
               </td>
               <td width="15%">
                   <asp:Button ID="Button1" runat="server"  style="background:#1a43c7; color:#FFF; padding-top:5px; "
                       Text="A/L" onclientclick="return imgLiab1_onclick();" />
                 
               </td>
           </tr>
       </table>
   


   </td>
   </tr>
   
   </table>
        
        </td>
    </tr>
    </table>
    </td>
<td>
   </td>

</tr>
     <tr>
         <td>
             </td>
         <td colspan="3">
             <label>
             Surety Details</label></td>
     </tr>
     <tr>
         <td>
         </td>
         <td colspan="3">
            <asp:GridView ID="DgvAdditionalMem" runat="server" AutoGenerateColumns="False"  CssClass="grid-viewNew"
                 DataKeyNames="CustID" TabIndex="17" Width="100%">
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
                      <asp:BoundField DataField="Dob" HeaderText="Dob" 
                DataFormatString="{0:dd/MM/yyyy}">
                     </asp:BoundField>
                     <asp:BoundField DataField="Age" HeaderText="Age">
 
                     </asp:BoundField>
                <asp:CommandField DeleteText="&lt;img src='./images/del.jpg'&gt;" 
                    ShowDeleteButton="True" DeleteImageUrl="~/Images/DeleteN.png" 
                         EditImageUrl="~/Images/DeleteN.png" 
                         NewImageUrl="~/Images/DeleteN.png" SelectImageUrl="~/Images/DeleteN.png">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
                     
                 </Columns>
                 <AlternatingRowStyle BackColor="#F5F4FF" />
             </asp:GridView>
         </td>
     </tr>
     <tr>
         <td>
             </td>
         <td>
              <asp:HiddenField ID="HiddenSurMemNo" runat="server" Visible="False" />
              </td>
         <td>
             </td>
         <td>
             </td>
     </tr>
    
 </table>  
     </div>  

                <%-- -------------- --%>
            </td>
            
        </tr>

        <tr>
            <td colspan="2">
                <div id="TrfrDiv" runat="server" class="gradientBoxesWithOuterShadows" style="margin-left:2%;margin-right:2%;margin-bottom:2%;margin-top:2%;" visible="false">
                    <legend>Quick Transfer</legend>
                    <table width="100%">
                        <tr>
                            <td width="2%"></td>
                            <td width="15%">
                                <label>
                                Transfer Branch</label></td>
                            <td width="15%">
                                <asp:DropDownList ID="CmbTrfrBr" runat="server" TabIndex="31" Width="100%">
                                </asp:DropDownList>
                            </td>
                            <td width="2%"></td>
                            <td width="16%">
                                <label>
                                Transfer Scheme</label></td>
                            <td width="16%">
                                <asp:DropDownList ID="CmbTrfrSch" runat="server" TabIndex="31" Width="100%">
                                </asp:DropDownList>
                            </td>
                            <td width="2%"></td>
                            <td width="15%">
                                <label>
                                Transfer Account No</label></td>
                            <td width="15%">
                                <asp:TextBox ID="TxtTrfrAcNo" runat="server" AutoPostBack="True" CssClass="twitterStyleTextbox" onkeypress="return textboxMultilineMaxNumber(this,18);" TabIndex="33" Width="96%"></asp:TextBox>
                            </td>
                            <td width="2%"></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <label>
                                Name</label></td>
                            <td>
                                <asp:TextBox ID="TxtTrfrAcHoldenName" runat="server" AutoPostBack="True" CssClass="twitterStyleTextbox" onkeypress="return textboxMultilineMaxNumber(this,200);" TabIndex="33" Width="96%"></asp:TextBox>
                            </td>
                            <td></td>
                            <td>
                                <label>
                                Address</label></td>
                            <td colspan="4">
                                <asp:TextBox ID="TxtTrfrAddress" runat="server" AutoPostBack="True" CssClass="twitterStyleTextbox" onkeypress="return textboxMultilineMaxNumber(this,200);" TabIndex="33" Width="98%"></asp:TextBox>
                            </td>
                            <td width="2%"></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <label>
                                Balance</label> </td>
                            <td>
                                <asp:TextBox ID="TxtTrfrBalAmt" runat="server" CssClass="twitterStyleTextbox" onkeypress="return textboxMultilineMaxNumber(this,18);" TabIndex="33" Width="96%"></asp:TextBox>
                            </td>
                            <td></td>
                            <td></td>
                            <td colspan="3"></td>
                            <td></td>
                            <td width="2%"></td>
                        </tr>
                    </table>
                </div>
            </td>
            <td class="auto-style1" >
                </td>
        </tr>

</table>    
      
       
    
       <div id="NomDiv" runat="server" visible="false">
            <table width="100%">
                <tr>
                    <td width="2%"></td>
                    <td width="15%">
                        <label>
                        Nominee Name</label></td>
                    <td width="49%">
                        <asp:TextBox ID="Txt_NomName" runat="server" CssClass="twitterStyleTextbox" TabIndex="30" Width="99%"></asp:TextBox>
                    </td>
                    <td width="2%"></td>
                    <td width="15%">
                        <label>
                        Relation</label></td>
                    <td width="15%">
                        <asp:DropDownList ID="CmbNomRel" runat="server" TabIndex="31" Width="100%">
                        </asp:DropDownList>
                    </td>
                    <td width="2%"></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <label>
                        Nominee Address</label></td>
                    <td>
                        <asp:TextBox ID="Txt_NomAdd" runat="server" CssClass="twitterStyleTextbox" TabIndex="32" Width="99%"></asp:TextBox>
                    </td>
                    <td></td>
                    <td>
                        <label>
                        Phone No</label></td>
                    <td>
                        <asp:TextBox ID="Txt_NomPh" runat="server" CssClass="twitterStyleTextbox" TabIndex="33" Width="96%"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
   

<table border="0" width="100%">
        <tr>
            <td width="2%">
            </td>
            <td width="96%" style="padding-top:40px;">
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
    </table>

	 </asp:Panel>
	 
	<table width="100%">
	    <tr>
	    <td width ="25%"></td>
	    <td width ="50%" align="center">
                <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" meta:resourcekey="CmdSaveResource1" TabIndex="33" />
           <%-- onclientclick="return Validatesave();"--%>
                <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/images/Close.png" 
                    meta:resourcekey="CmdClearResource1" TabIndex="34" />
                    	<asp:ImageButton ID="cmdDelete" runat="server" ImageUrl="~/images/DELETE.png" Visibl="false"
                meta:resourcekey="CmdSaveResource1" TabIndex="29" OnClientClick="return ValidateDELETE();"  />
        </td>
	    <td width ="25%"> &nbsp;</td>
	    </tr>
	    <tr>
            <td width="35%">
                <asp:HiddenField ID="HdnMemBrCode" runat="server" />
                <asp:HiddenField ID="HdnTrfrAccID" runat="server" />
            </td>
            <td width="30%">
                <asp:HiddenField ID="HdnMemType" runat="server" />
                <asp:HiddenField ID="HdnCustID" runat="server" />
                <asp:HiddenField ID="HdnPrdType" runat="server" />
                <asp:HiddenField ID="HdnEMIType" runat="server" />
            </td>
            <td width="35%">
                <asp:HiddenField ID="HdnMemNo" runat="server" />
            </td>
        </tr>
	</table>

          </div>
        </ContentTemplate>
   
    </asp:UpdatePanel> 
       
</asp:Content>


    


