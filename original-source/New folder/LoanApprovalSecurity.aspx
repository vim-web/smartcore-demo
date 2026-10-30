<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="LoanApprovalSecurity.aspx.vb" Inherits="SMARTCORE_UI.LoanApprovalSecurity" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
    <link href="CSS/ui-lightness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript' src="Scripts/Common.js"></script>
   <script src="Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script type='text/javascript' src="Scripts/jquery-ui-1.9.2.custom.min.js"></script>     
    <script type="text/javascript">
    
      $(function() {
        });
        function imgLiab_onclick() {        
        
            $('.gradientBoxesWithOuterShadows1').append('<div id="resultdiv" title="Customer Asset Liablities" visible="false" >');
            $("#resultdiv").load("AssetLiab.aspx?CustID=" + document.getElementById('<%=HdnSurCustID.ClientID%>').value, {}, function(data) {
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
//    open: function(event, ui) {$(this).dialog('open'); }
        }             
    </script>
    <script type="text/javascript">
    
      $(function() {
        });
        function imgLiab_onclick() {        
        
            $('.gradientBoxesWithOuterShadows1').append('<div id="resultdiv" title="Customer Asset Liablities" visible="false" >');
            $("#resultdiv").load("AssetLiab.aspx?CustID=" + document.getElementById('<%=HdnSurCustID.ClientID%>').value, {}, function(data) {
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
//    open: function(event, ui) {$(this).dialog('open'); }
        }             
    </script>
    <script type="text/javascript">
    
      $(function() {
        });
        function imgLiab1_onclick() {        
        
            $('.gradientBoxesWithOuterShadows2').append('<div id="resultdiv" title="Customer Asset Liablities" visible="false" >');
            $("#resultdiv").load("AssetLiab.aspx?CustID=" + document.getElementById('<%=HiddenSurCustid.ClientID%>').value, {}, function(data) {
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
//    open: function(event, ui) {$(this).dialog('open'); }
        }             
    </script>
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">   
   <ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True">
    <Scripts>
            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
    </Scripts>
</asp:ToolkitScriptManager>
<script type="text/javascript">
    function Chk_Validate() {
        if (document.getElementById('<%=ChkLandBuilding.ClientID%>').checked == true) {
            document.getElementById('DivLandBuilding').style.display = 'inline';
        }
        else {
            document.getElementById('DivLandBuilding').style.display = 'none';
        }

        if (document.getElementById('<%=ChkCrops.ClientID%>').checked == true) {
            document.getElementById('DivCrops').style.display = 'inline';
        }
        else {
            document.getElementById('DivCrops').style.display = 'none';
        }
        if (document.getElementById('<%=ChkGoldOrnaments.ClientID%>').checked == true) {
            document.getElementById('DivGold').style.display = 'inline';
        }
        else {
            document.getElementById('DivGold').style.display = 'none';
        }
        if (document.getElementById('<%=ChkInsur.ClientID%>').checked == true) {
            document.getElementById('DivInsurance').style.display = 'inline';
        }
        else {
            document.getElementById('DivInsurance').style.display = 'none';
        }
        if (document.getElementById('<%=ChkBond.ClientID%>').checked == true) {
            document.getElementById('DivBond').style.display = 'inline';
        }
        else {
            document.getElementById('DivBond').style.display = 'none';
        }
        if (document.getElementById('<%=ChkVehicle.ClientID%>').checked == true) {
            document.getElementById('DivVehicle').style.display = 'inline';
        }
        else {
            document.getElementById('DivVehicle').style.display = 'none';
        }
        if (document.getElementById('<%=ChkTermDeposit.ClientID%>').checked == true) {
            document.getElementById('DivTermDeposit').style.display = 'inline';
        }
        else {
            document.getElementById('DivTermDeposit').style.display = 'none';
        }
    }
</script>   
    <script type="text/javascript">
     
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
             var Ctl = document.getElementById('<%=TxtDueDate.ClientID%>');
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
                 alert('Please Enter RTO Office');
                 Ctl.focus();
                 return false;
             }
           
            
             var Ctl = document.getElementById('<%=TxtTaxDetails.ClientID%>');
             if (Ctl.value == "") {
                 alert('Please Enter vehicle TaxDetails');
                 Ctl.focus();
                 return false;
             }
             var Ctl = document.getElementById('<%=TxtInsuranceDetails.ClientID%>');
             if (Ctl.value == "") {
                 alert('Please Enter vehicle InsuranceDetails');
                 Ctl.focus();
                 return false;
             }
             var Ctl = document.getElementById('<%=TxtInsuranceValue.ClientID%>');
             if (Ctl.value == "") {
                 alert('Please Enter vehicle InsuranceValue');
                 Ctl.focus();
                 return false;
             }
             var Ctl = document.getElementById('<%=TxtOtherDetails0.ClientID%>');
             if (Ctl.value == "") {
                 alert('Please Enter OtherDetails');
                 Ctl.focus();
                 return false;
             }
         }


         function ValidatetermDeposit() {           
        var Ctl = document.getElementById('<%=TxtDepositeAmt.ClientID%>');
             if (Ctl.value == "") {
                 alert('Please Enter DepositeAmt');
                 Ctl.focus();
                 return false;
             }
             var Ctl = document.getElementById('<%=TxtAmtOutstanding.ClientID%>');
             if (Ctl.value == "") {
                 alert('Please Enter AmtOutstanding');
                 Ctl.focus();
                 return false;
             }
             var Ctl = document.getElementById('<%=TextBox1.ClientID%>');
             if (Ctl.value == "") {
                 alert('Please Enter Section Branch Name');
                 Ctl.focus();
                 return false;
             }
             
             var Ctl = document.getElementById('<%=TxtCustomerNo.ClientID%>');
             if (Ctl.value == "") {
                 alert('Please Enter Customer No');
                 Ctl.focus();
                 return false;
             }
             var Ctl = document.getElementById('<%=TextBox2.ClientID%>');
             if (Ctl.value == "") {
                 alert('Please Enter Name');
                 Ctl.focus();
                 return false;
             }
             var Ctl = document.getElementById('<%=TxtAddress1.ClientID%>');
             if (Ctl.value == "") {
                 alert('Please Enter Address');
                 Ctl.focus();
                 return false;
             }
         }

         function ValidateLand() {

             var Ctl = document.getElementById('<%=TxtDeedNo.ClientID%>');
          if (Ctl.value == "") {
              alert('Please Enter Deed No');
              Ctl.focus();
              return false;
          }
          var Ctl = document.getElementById('<%=TxtDeedDate.ClientID%>');
         if (Ctl.value == "") {
             alert('Please Enter Deed Date');
             Ctl.focus();
             return false;
         }
         var Ctl = document.getElementById('<%=TxtPriorNo.ClientID%>');
        if (Ctl.value == "") {
            alert('Please Enter Prior No');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtExtend.ClientID%>');
        if (Ctl.value == "") {
            alert('Please Enter Due Extend');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtDistrict.ClientID%>');
        if (Ctl.value == "") {
            alert('Please Enter District');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtTehsil.ClientID%>');
        if (Ctl.value == "") {
            alert('Please Enter Tehsil');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtPunchMun.ClientID%>');
        if (Ctl.value == "") {
            alert('Please Enter Panchayath');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtVillage.ClientID%>');
        if (Ctl.value == "") {
            alert('Please Enter Village');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtSubRegOffice.ClientID%>');
        if (Ctl.value == "") {
            alert('Please Enter SubRegOffice');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtMarketValue.ClientID%>');
        if (Ctl.value == "") {
            alert('Please Enter MarketValue');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtRecoveryNo.ClientID%>');
        if (Ctl.value == "") {
            alert('Please Enter RecoveryNo');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtOtherDetails.ClientID%>');
        if (Ctl.value == "") {
            alert('Please Enter OtherDetails');
            Ctl.focus();
            return false;
        }

    }

    function ValidateSaveinsurance() {

        var Ctl = document.getElementById('<%=TxtInsuranceCompany.ClientID%>');
        if (Ctl.value == "") {
            alert('Please Enter InsuranceCompany');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtPolicyName.ClientID%>');
             if (Ctl.value == "") {
                 alert('Please Enter Policy Name');
                 Ctl.focus();
                 return false;
             }
             var Ctl = document.getElementById('<%=TxtPolicyNo.ClientID%>');
             if (Ctl.value == "") {
                 alert('Please Enter Policy No');
                 Ctl.focus();
                 return false;
             }
             var Ctl = document.getElementById('<%=TxtSumAssured.ClientID%>');
             if (Ctl.value == "") {
                 alert('Please Enter SumAssured');
                 Ctl.focus();
                 return false;
             }
             var Ctl = document.getElementById('<%=TxtSurrenderValue.ClientID%>');
             if (Ctl.value == "") {
                 alert('Please Enter Surrender Value');
                 Ctl.focus();
                 return false;
             }
         }

         function validatecrop() {
             var Ctl = document.getElementById('<%=TxtCropName.ClientID%>');
        if (Ctl.value == "") {
            alert('Please Enter Crop Name');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtNoOfTrees.ClientID%>');
        if (Ctl.value == "") {
            alert('Please Enter NoOfTrees');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtValue.ClientID%>');
        if (Ctl.value == "") {
            alert('Please Enter Value');
            Ctl.focus();
            return false;
        }

    }
</script> 

     <div class="gradientBoxesWithOuterShadows" style="width: 85%; margin-left:10%; margin-right:10%;">
     <legend>Loan Application</legend>
 <table width="100%">
<tr>
<td width="2%" ></td>
<td width="40%" >
<label>Loan Applciation Type</label></td>

<td width="56%" >

        <asp:DropDownList ID="CmbLoanAppType" Width="100%" runat="server" AutoPostBack ="true">    

    </asp:DropDownList>
</td>
<td width="2%" >
    </td>
</tr>
<tr>
<td> </td>
<td align="left">
    <label>Branch</label></td>
<td>
    <table width="100%">
    <tr>
    <td width="100%">
        <asp:DropDownList ID="CmbBranch" Width="100%" runat="server" AutoPostBack="True" 
            TabIndex="4">
    </asp:DropDownList>
    
        &nbsp;</td>

    </tr>
    </table>
    </td>
<td> </td>
</tr>
<tr>
<td></td>
<td>
<label>Application Form No</label></td>
<td>
    <asp:DropDownList ID="CmbLoanNo" runat="server" AutoPostBack="True" 
        Width="100%">
    </asp:DropDownList>
    </td>
<td></td>
</tr>





<tr>
<td> </td>
<td align="left">
    <label>Loan Scheme</label></td>
<td>
    <table width="100%">
    <tr>
    <td width="100%">
        <asp:DropDownList ID="CmbScheme" Width="100%" runat="server" AutoPostBack="True" 
            TabIndex="5">
    </asp:DropDownList>
        &nbsp;</td>


    </td>
    </tr>
    </table>
    
    </td>
<td> </td>
</tr>
<tr>
<td> </td>
<td align="left">
    <label>Member No</label></td>
<td>
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtMemberno" runat="server" 
                AutoPostBack="True" TabIndex="8" 
            Width="100%" Enabled="False"></asp:TextBox>
    </td>
<td>
    <asp:HiddenField ID="HdnAccid" runat="server" />
</td>
</tr>
<tr>
<td> </td>
<td>
    <label>
                Name </label>
                

    </td>
<td>
    <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtName" Width="100%" 
        runat="server" ReadOnly="True" Enabled="False"></asp:TextBox>
   </td>
<td> 
    <asp:HiddenField ID="HdnCustID" runat="server"  />
    </td>
</tr>
        <tr>
            <td>
                 </td>
            <td>
      <label>
                Address </label>
                </td>
            <td>
            
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtAddress" Width="100%" 
                    runat="server" TextMode="MultiLine" 
                    ReadOnly="True" Enabled="False"></asp:TextBox>
                </td>
            <td>
                 </td>
        </tr>
<tr>
<td> </td>
<td>
    <label>Amsam</label></td>
<td>
     
    <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtAmsam" runat="server" 
        Width="100%" Enabled="False"></asp:TextBox>
     
    </td>
<td> </td>
</tr>
<tr>
<td> </td>
<td>
    <label>Desam</label></td>
<td>
    <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtDesam" runat="server" 
        Width="100%" Enabled="False"></asp:TextBox>
    </td>
<td> 
    <asp:HiddenField ID="SurMemAccid" runat="server"  />
    </td>
</tr>

<tr>
<td> </td>
<td>
    <label>Director</label></td>
<td>
    <asp:DropDownList ID="CmbDirector" runat="server" Width="100%" TabIndex="9" 
        Enabled="False">
    </asp:DropDownList>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    </td>
<td> </td>
</tr>
     <tr>
         <td>
             &nbsp;</td>
         <td>
             &nbsp;</td>
         <td>
             &nbsp;</td>
         <td>
             <asp:HiddenField ID="SurMemAccid0" runat="server" />
         </td>
     </tr>
   
        
        

  <tr>
            <td>
                 </td>
            <td>
                <label>
                Advocate</label></td>
            <td>
            
                <asp:DropDownList ID="CmbAdvocate" runat="server" TabIndex="9" Width="100%" 
                    Enabled="False">
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
                Loan Application Date</label></td>
            <td>
            
            <table width ="100%">
            <tr>
            <td width="30%">
                <asp:TextBox  CssClass="twitterStyleTextbox" ID = "TxtLoanDate" width ="100%" 
                    runat="server" Enabled="False" ></asp:TextBox>
                
                </td>
            <td width="6%"> </td>
            <td width="24%">
            <label >  Loan Amount</label>
            </td>
            <td width="40%">
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtLoanAmount" runat="server" onkeypress="return validate(event)" 
        Width="100%" AutoPostBack="True" Enabled="False"></asp:TextBox>
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
            <td>
                <label>
                Property Inspection Date</label></td>
            <td>
            
            <table width ="100%">
            <tr>
            <td width="30%">
               <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtPifDate" runat="server" 
                    Width="100%" TabIndex="10" 
                    ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:CalendarExtender ID="TxtPifDate_CalendarExtender" runat="server" 
                    TargetControlID="TxtPifDate"  Format="dd/MM/yyyy">
                </asp:CalendarExtender>
            </td>
            <td width="6%"> </td>
            <td width="24%">
            <label >  Property Value</label>
            </td>
            <td width="40%">
                            <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtPropertyValue" 
                                runat="server" onkeypress="return validate(event)" 
        Width="100%" AutoPostBack="True" ReadOnly="True" Enabled="False"></asp:TextBox>
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
            <td>
                <label>
                Record Inspection Date</label></td>
            <td>
            
            <table width ="100%">
            <tr>
            <td width="30%">
               <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtRiDate" runat="server" 
                    Width="100%" TabIndex="10" Enabled="False"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                    TargetControlID="TxtRiDate"  Format="dd/MM/yyyy">
                </asp:CalendarExtender>
            </td>
            <td width="6%"> </td>
            <td width="24%">
            
                <label>Record Inspection Amt</label></td>
            <td width="40%">
                            <asp:TextBox  CssClass="twitterStyleTextbox" ID="txtRiAmt" Visible="false"  runat="server" onkeypress="return validate(event)" 
        Width="100%" AutoPostBack="True" Enabled="False" ></asp:TextBox>
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
            <td>
               <label>
             SELECT SECURITY/SURETY</label></td>
            <td>
            
            <table width ="100%">
            <tr>
            <td width="20%">
                <asp:CheckBox ID="ChkSur" runat="server" AutoPostBack="True" Font-Bold="True" 
                    Text="SURETY" />
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" 
                    TargetControlID="TxtRiDate"  Format="dd/MM/yyyy">
                </asp:CalendarExtender>
            </td>
            
            <td width="24%">
             <asp:CheckBox ID="ChkSec" runat="server" AutoPostBack="True" Font-Bold="True" 
                                Text="SECURITY" />
            </td>
            <td width="20%">
                            <asp:CheckBox ID="Chkaddmem" runat="server" AutoPostBack="True" Font-Bold="True" 
                                Text="ADL MEMBER" />
            </td>
            </tr>
            </table>        
            
            
             
            </td>
            <td>
            </td>
        </tr>

</table>
<div id ="DivSur" runat ="server"   class="gradientBoxesWithOuterShadows1" style="width: 90%; margin-left:5%; margin-right:5%;" visible ="false" >
<table width ="100%">
     <tr>
         <td width ="2%">
            </td>
         <td width ="40%">
             <asp:HiddenField ID="HdnSurCustID" runat="server" Value="0"/>
        </td>
         <td width ="56%">
             <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtSurAccID" runat="server" AutoPostBack="True" TabIndex="13" 
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
      <asp:DropDownList ID="CmbSurBranch" runat="server" Width="100%" TabIndex="11">
                </asp:DropDownList></td>
    <td width="70%">
   <table width ="100%">
   <tr>
   <td>
   <asp:DropDownList ID="CmbSurMemType" runat="server" Width="100%" TabIndex="12" 
                    AutoPostBack="True">
                </asp:DropDownList>
   </td>
   <td>
   


       <table width="100%">
           <tr>
               <td width="85%">
                   <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtSurMemNo" runat="server" AutoPostBack="True" TabIndex="13" 
                       Width="95%"></asp:TextBox>
               </td>
               <td width="15%">
                   <asp:Button ID="imgLiab" runat="server" 
                       Text="A/L" onclientclick="return imgLiab_onclick();" />
                   <%--<asp:ImageButton ID="ImgLiab" runat="server" ImageUrl="~/Images/TrImg9.png"
                    OnClientClick="window.open('AssetLiab.aspx?CustID=<%HdnSurCustID.ClientID%>', 'height=50,width=100,status=yes,toolbar=yes,menubar=yes,location=yes');return false;" />
               --%>
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
            <asp:GridView ID="DgvCustomer" runat="server" AutoGenerateColumns="False" 
                 DataKeyNames="CustID" TabIndex="17" Width="100%">
                 <Columns>
                     <asp:BoundField DataField="MemDtls" HeaderText="Member Details">
 <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="15%" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" />
                     </asp:BoundField>
                     <asp:BoundField DataField="MemBrCode" HeaderText="Member BrCode" 
                         Visible="False" />
                     <asp:BoundField DataField="MemSchCode" HeaderText="MemSchCode" 
                         Visible="False" />
                     <asp:BoundField DataField="MemNo" HeaderText="Member No" Visible="False">
 <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" />
                     </asp:BoundField>
                     <asp:BoundField DataField="CustID" HeaderText="Customer ID">
 <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="8%" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" HorizontalAlign="Center" />
                     </asp:BoundField>
                     <asp:BoundField DataField="CustName" DataFormatString="{0:F2}" 
                         HeaderText="Name">
 <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="17%" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" HorizontalAlign="Left" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Relation" HeaderText="Relation">
 <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="20%" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" />
                     </asp:BoundField>
                     <asp:BoundField DataField="CustAdd" HeaderText="Address">
 <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="30%" />
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
                      <asp:BoundField DataField="Dob" HeaderText="Dob" 
                DataFormatString="{0:dd/MM/yyyy}">
                     
                     
 <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="6%" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Age" HeaderText="Age">
 <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="4%" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" />
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
              <asp:HiddenField ID="HdnSurMemNo" runat="server" Visible="False" />
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
            </td>
         <td>
         </td>
         <td>
         </td>
     </tr>
 </table>    
     </div>

<div id ="AddMem" runat ="server"   class="gradientBoxesWithOuterShadows2" style="width: 90%; margin-left:5%; margin-right:5%;" visible ="false" >
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
                   <asp:Button ID="Button1" runat="server" 
                       Text="A/L" onclientclick="return imgLiab1_onclick();" />
                   <%--<asp:ImageButton ID="ImgLiab" runat="server" ImageUrl="~/Images/TrImg9.png"
                    OnClientClick="window.open('AssetLiab.aspx?CustID=<%HdnSurCustID.ClientID%>', 'height=50,width=100,status=yes,toolbar=yes,menubar=yes,location=yes');return false;" />
               --%>
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
            <asp:GridView ID="DgvAdditionalMem" runat="server" AutoGenerateColumns="False" 
                 DataKeyNames="CustID" TabIndex="17" Width="100%">
                 <Columns>
                     <asp:BoundField DataField="MemDtls" HeaderText="Member Details">
 <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="15%" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" />
                     </asp:BoundField>
                     <asp:BoundField DataField="MemBrCode" HeaderText="Member BrCode" 
                         Visible="False" />
                     <asp:BoundField DataField="MemSchCode" HeaderText="MemSchCode" 
                         Visible="False" />
                     <asp:BoundField DataField="MemNo" HeaderText="Member No" Visible="False">
 <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" />
                     </asp:BoundField>
                     <asp:BoundField DataField="CustID" HeaderText="Customer ID">
 <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="8%" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" HorizontalAlign="Center" />
                     </asp:BoundField>
                     <asp:BoundField DataField="CustName" DataFormatString="{0:F2}" 
                         HeaderText="Name">
 <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="17%" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" HorizontalAlign="Left" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Relation" HeaderText="Relation">
 <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="20%" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" />
                     </asp:BoundField>
                     <asp:BoundField DataField="CustAdd" HeaderText="Address">
 <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="30%" />
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
                      <asp:BoundField DataField="Dob" HeaderText="Dob" 
                DataFormatString="{0:dd/MM/yyyy}">
                     
                     
 <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="6%" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Age" HeaderText="Age">
 <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="4%" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" />
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
 </table>    
     </div>
  

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

  

<div id ="DivOtherSuerties" visible ="false" runat ="server"  class="gradientBoxesWithOuterShadows" style="width: 90%; margin-left:5%; margin-right:5%;">
            <table width ="100%">
            <tr>
            <td width="2%">&nbsp;</td>
            <td width="40%" colspan="2" style="width: 96%">
                &nbsp;</td>
            <td width="2%">&nbsp;</td>
            </tr>
               
                <tr>
                    <td width="2%">
                    </td>
                    <td colspan="2" >

                      <table width="100%">
                        <tr>
                        <td width="2%"></td>
                        <td width="20%">
                            <asp:CheckBox ID="ChkLandBuilding" runat="server"  Text="Land And Building" AutoPostBack="True" />
                            </td>
                        <td width="8%">
                            <asp:CheckBox ID="ChkCrops" runat="server" Text="Crops" AutoPostBack="True" />
                            </td>
                        <td width="17%">
                            <asp:CheckBox ID="ChkGoldOrnaments" runat="server" Text="Gold Ornaments" AutoPostBack="True"
                                />
                            </td>
                        <td width="11%">
                            <asp:CheckBox ID="ChkInsur" runat="server" Text="Insurance" AutoPostBack="True"
                               />
                            </td>
                        <td width="14%">
                            <asp:CheckBox ID="ChkBond" runat="server" Text="Other Bonds" AutoPostBack="True"
                               />
                            </td>
                        <td width="14%">
                            <asp:CheckBox ID="ChkVehicle" runat="server" Text="Vehicle" AutoPostBack="True"
                               />
                            </td>
                        <td width="14%">
                            <asp:CheckBox ID="ChkTermDeposit" runat="server" Text="Term Deposit" AutoPostBack="True"
                                />
                            </td>
                        <td width="2%"></td>
                            </tr>
                            </table> 

                        </td>
                    <td width="2%">
                    </td>
                </tr>
                                <tr>
                    <td width="2%">
                    </td>
                    <td colspan="2" >

                      <table width="100%">
                        <tr>
                        <td width="2%"></td>
                        <td width="28%">
                           
                            </td>

                        <td width="28%">
                           
                            </td>
                           
                     
                        <td width="28%">
                           
                            </td>
                        <td width="14%">
                           
                            </td>
                        <td width="2%"></td>
                            </tr>
                            </table> 

                        </td>
                    <td width="2%">
                    </td>
                </tr>       
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                <tr>
                    <td width="2%">
                        &nbsp;</td>
                    <td colspan="2" >
                       
                       


    <br />                  
    <div id="DivLandBuilding"  runat ="server"  visible="false"  >

       <br />  
       <br />
 
        <hr width="100%" />

    <table width="100%">
    <tr>
    <td width="13%">
        <label>
        Deed Details</label></td>
    <td width="13%"></td>
    <td width="2%"></td>
    <td width="13%">
        <asp:CheckBox ID="ChkTax" runat="server" Text="Tax Details" 
            AutoPostBack="True" />
        </td>
    <td width="13%"></td>
    <td width="2%"></td>
     <td width="13%">
         <asp:CheckBox ID="ChkInsurance" runat="server" Text="Insurance Details" 
             AutoPostBack="True" />
        </td>
    <td width="13%"></td>
    </tr>
    <tr>
    <td>
        <label>
        No. :</label></td>
    <td>
        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtDeedNo" runat="server" Width="100%" onkeypress="return isFloatKey(event);"></asp:TextBox>
        </td>
    <td></td>
    <td>
        <label>
        No. :</label></td>
    <td>
        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtTaxNo" runat="server" Width="100%" onkeypress="return isFloatKey(event);"></asp:TextBox>
        </td>
    <td></td>
    <td>
        <label>
        No. :</label></td>
    <td>
        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtInsuranceNo" runat="server" Width="100%" onkeypress="return isFloatKey(event);"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td>
        <label>
        Date. :</label></td>
    <td>
        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtDeedDate" runat="server" Width="100%"></asp:TextBox>
        <asp:CalendarExtender ID="TxtDeedDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
            TargetControlID="TxtDeedDate">
        </asp:CalendarExtender>
        </td>
    <td></td>
    <td>
        <label>
        Date. :</label></td>
    <td>
        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtTaxDate" runat="server" Width="100%"></asp:TextBox>
        <asp:CalendarExtender ID="TxtTaxDate_CalendarExtender" runat="server" Format="dd/MM/yyyy"
            TargetControlID="TxtTaxDate">
        </asp:CalendarExtender>
        </td>
    <td></td>
    <td>
        <label>
        Date. :</label></td>
    <td>
        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtInsuranceDate" runat="server" Width="100%"></asp:TextBox>
        <asp:CalendarExtender ID="TxtInsuranceDate_CalendarExtender" runat="server" Format="dd/MM/yyyy"
            TargetControlID="TxtInsuranceDate">
        </asp:CalendarExtender>
        </td>
    </tr>
    <tr>
    <td>
        <label>
        Prior No. :</label></td>
    <td>
        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtPriorNo" runat="server" Width="100%" onkeypress="return isFloatKey(event);"></asp:TextBox>
        </td>
    <td>&nbsp;</td>
    <td>
        <label>
        Details. :</label></td>
    <td>
        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtTaxDetails" runat="server" Width="100%"></asp:TextBox>
        </td>
    <td>&nbsp;</td>
    <td>
        <label>
        DueDate. :</label></td>
    <td>
        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtDueDate" runat="server" Width="100%"></asp:TextBox>
        <asp:CalendarExtender ID="TxtDueDate_CalendarExtender" runat="server" Format="dd/MM/yyyy"
            TargetControlID="TxtDueDate">
        </asp:CalendarExtender>
        </td>
    </tr>
    <tr>
    <td>
        <label>
        Extend. :</label></td>
    <td>
        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtExtend" runat="server" Width="100%"></asp:TextBox>
        </td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    </tr>
    <tr>
    <td>
        <label>
        District. :</label></td>
    <td>
        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtDistrict" runat="server" Width="100%"></asp:TextBox>
        </td>
    <td>&nbsp;</td>
    <td>
        <label>
        Owner Details :</label></td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td><label>SurveyNo. :</label></td>
    <td>
        <asp:TextBox ID="TxtSurveyno" runat="server" CssClass="twitterStyleTextbox" 
            onkeypress="return isFloatKey(event);" Width="100%"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td>
        <label>
        Tehsil. :</label></td>
    <td>
        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtTehsil" runat="server" Width="100%"></asp:TextBox>
        </td>
    <td>&nbsp;</td>
    <td>
        <label>
        Mode Of Ownership :</label></td>
    <td>
        <asp:DropDownList ID="CmbModeOfOwnerdhip" runat="server" Width="100%">
            <asp:ListItem>Single</asp:ListItem>
            <asp:ListItem>Joint</asp:ListItem>
        </asp:DropDownList>
        </td>
    <td>&nbsp;</td>
    <td><label>ResurveyNo. :</label></td>
    <td>
        <asp:TextBox ID="TxtResurveyNo" runat="server" CssClass="twitterStyleTextbox" 
            onkeypress="return isFloatKey(event);" Width="100%"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td>
       <label>
        Panchayath/Muncipality
        </label>
       </td>
    <td>
        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtPunchMun" runat="server" Width="100%"></asp:TextBox>
        </td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td >&nbsp;</td>
    <td ><label> DocumentNo. :</label> </td>
    <td>
        <asp:TextBox ID="TxtDocNo" runat="server" CssClass="twitterStyleTextbox" 
            onkeypress="return isFloatKey(event);" Width="100%"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td >
        <label>
        Village. :</label></td>
    <td >
        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtVillage" runat="server" Width="100%"></asp:TextBox>
        </td>
    <td >&nbsp;</td>
    <td >&nbsp;</td>
    <td >&nbsp;</td>
    <td >&nbsp;</td>
    <td >&nbsp;</td>
    <td >&nbsp;</td>
    </tr>
    <tr>
    <td >
        <label>
        Sub Reg Office. :</label></td>
    <td >
        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtSubRegOffice" runat="server" Width="100%"></asp:TextBox>
        </td>
    <td >&nbsp;</td>
    <td >
        <label>
        Recovery No :</label></td>
    <td >
        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtRecoveryNo" runat="server" Width="100%" onkeypress="return isFloatKey(event);"></asp:TextBox>
        </td>
    <td >&nbsp;</td>
    <td >&nbsp;</td>
    <td >&nbsp;</td>
    </tr>
    <tr>
    <td >
        <label>
        Market Value. :</label></td>
    <td >
        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtMarketValue" runat="server" Width="100%"  onkeypress="return isFloatKey(event);"></asp:TextBox>
        </td>
    <td >&nbsp;</td>
    <td >
        <label>
        Other Details :</label></td>
    <td >
        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtOtherDetails" runat="server" Width="100%"></asp:TextBox>
        </td>
    <td >&nbsp;</td>
    <td >
        <asp:ImageButton ID="ImgBtnAddLand" runat="server" 
            ImageUrl="~/Images/AddBtn.png" OnClientClick="return ValidateLand();" />
        </td>
    <td >&nbsp;</td>
    </tr>
        <tr>
            <td colspan="8">
                <asp:GridView ID="DgvLand" runat="server" AutoGenerateColumns="False" CssClass="grid-view"
                    Width="100%">
                    <Columns>
                        <asp:BoundField DataField="Tax" HeaderText="Tax" Visible="False" />
                        <asp:BoundField DataField="Insurance" HeaderText="Insurance" Visible="False" />
                        <asp:BoundField DataField="DeedNo" HeaderText="DeedNo" />
                        <asp:BoundField DataField="DeedDate" HeaderText="Deed Date" Visible="False" />
                        <asp:BoundField DataField="PriorNo" HeaderText="Prior No" Visible="False" />
                        <asp:BoundField DataField="Extend" HeaderText="Extend" Visible="False" />
                        <asp:BoundField DataField="District" HeaderText="District" Visible="False" />
                        <asp:BoundField DataField="Tehsil" HeaderText="Tehsil" Visible="False" />
                        <asp:BoundField DataField="Panchayath" HeaderText="Panchayath" 
                            Visible="False" />
                        <asp:BoundField DataField="Village" HeaderText="Village" Visible="False" />
                        <asp:BoundField DataField="SubRegOffice" HeaderText="Sub Reg Office" 
                            Visible="False" />
                        <asp:BoundField DataField="MarketValue" HeaderText="Market Value" 
                            Visible="False" />
                        <asp:BoundField DataField="TaxNo" HeaderText="Tax No" />
                        <asp:BoundField DataField="TaxDate" HeaderText="Tax Date" />
                        <asp:BoundField DataField="TaxDetails" HeaderText="Tax Details" 
                            Visible="False" />
                        <asp:BoundField DataField="ModeOfOwnership" HeaderText="Mode Of Ownership" 
                            Visible="False" />
                        <asp:BoundField DataField="RecoveryNo" HeaderText="Recovery No" />
                        <asp:BoundField DataField="OtherDetails" HeaderText="Other Details" 
                            Visible="False" />
                        <asp:BoundField DataField="InsuranceNo" HeaderText="Insurance No" 
                            Visible="False" />
                        <asp:BoundField DataField="InsuranceDate" HeaderText="Insurance Date" />
                        <asp:BoundField DataField="DueDate" HeaderText="Due Date" />
                         <asp:BoundField DataField="SurveyNo" HeaderText="SurveyNo" Visible="False" />
                         <asp:BoundField DataField="ResurveyNo" HeaderText="ResurveyNo" Visible="False" />
                          <asp:BoundField DataField="DocNo" HeaderText="DocNo" Visible="False" />
                        <asp:CommandField ShowDeleteButton="True" />
                        
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    </div>


        <div id="DivCrops"   runat ="server"   visible="false"  >
       <br />
       <br />  
       <br />
 
        <hr />
        <table width="100%">
        <tr>
        <td width="13%"></td>
        <td width="13%"></td>
        <td width="2%"></td>
        <td width="13%"></td>
        <td width="13%"></td>
        <td width="2%"></td>
        <td width="13%"></td>
        <td width="13%"></td>
        </tr>
        <tr>
        <td>
            <label>
            Crop Name :</label></td>
        <td>
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtCropName" runat="server" Width="100%"></asp:TextBox>
            </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
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
            <label>
            No Of Trees :</label></td>
        <td>
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtNoOfTrees"  onkeypress="return isFloatKey(event);" runat="server" Width="100%"></asp:TextBox>
            </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
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
            <label>
            Value :</label></td>
        <td>
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtValue" runat="server" Width="100%" onkeypress="return isFloatKey(event);"></asp:TextBox>
            </td>
        <td>&nbsp;</td>
        <td>
            <asp:ImageButton ID="ImgBtnCropAdd" runat="server" 
                ImageUrl="~/Images/AddBtn.png" OnClientClick="return validatecrop();" />
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
            <tr>
                <td colspan="8">
                    <asp:GridView ID="DgvCrop" runat="server" AutoGenerateColumns="False" CssClass="grid-view"
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="CropName" HeaderText="Crop Name" />
                            <asp:BoundField DataField="NoOfTrees" HeaderText="No Of Trees" />
                            <asp:BoundField DataField="Value" HeaderText="Value" />
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        </div>

        <div id="DivGold" runat ="server"  visible="false"  >
               <br />
       <br />  
       <br />
                <hr />
            <table width="100%">
                <tr>
                    <td width="13%">
                        <label>
                        Gold Ornaments</label></td>
                    <td width="13%">
                    </td>
                    <td width="2%">
                    </td>
                    <td width="13%">
                    </td>
                    <td width="13%">
                    </td>
                    <td width="2%">
                    </td>
                    <td width="13%">
                    </td>
                    <td width="13%">
                    </td>
                </tr>
                <tr>
                    <td width="13%">
                        <label>
                        Ornaments Type</label></td>
                    <td width="13%">
                        <asp:DropDownList ID="CmbOrnamentType" runat="server" Width="100%">
                        </asp:DropDownList>
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="13%">
                        <label>
                        Count</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtCount"  onkeypress="return isFloatKey(event);" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="13%">
                        <label>
                        Remarks</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtRemarks" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="13%">
                        <label>
                        Net Weight</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtNetWeight"  onkeypress="return isFloatKey(event);" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="13%">
                        <label>
                        Gross Weight</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtGrossWeight"  onkeypress="return isFloatKey(event);" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        <asp:ImageButton ID="ImgBtnOrnament" runat="server" 
                            ImageUrl="~/Images/AddBtn.png" />
                    </td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:GridView ID="DgvOrnament" runat="server" AutoGenerateColumns="False" CssClass="grid-view"
                            Width="100%">
                            <Columns>

                                 <asp:BoundField DataField="OrnamentType" HeaderText="Ornament Type" 
                                    Visible="False" />
                                <asp:BoundField DataField="OrnamentDtls" HeaderText="Ornament" />
                                    
                                <asp:BoundField DataField="GoldCount" HeaderText="Count" />
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                <asp:BoundField DataField="NetWeight" HeaderText="Net weight" />
                                <asp:BoundField DataField="GrossWeight" HeaderText="Gross Weight" />
                                <asp:CommandField ShowDeleteButton="True" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>

       
       <div id="DivInsurance" runat ="server"  visible="false"  >
              <br />
       <br />  
       <br />
               <hr />
       <table width="100%">
       <tr>
       <td width="13%">
                       <label>
                       Insurance</label></td>
                    <td width="13%">
                    </td>
                    <td width="2%">
                    </td>
                    <td width="13%">
                    </td>
                    <td width="13%">
                    </td>
                    <td width="2%">
                    </td>
                    <td width="13%">
                    </td>
                    <td width="13%">
                    </td>
       </tr>
       <tr>
       <td width="13%">
                       <label>
                       Insurance Company :</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtInsuranceCompany" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
       </tr>
       <tr>
       <td width="13%">
                       <label>
                       Policy Name :</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtPolicyName" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
       </tr>
       <tr>
       <td width="13%">
                       <label>
                       PolicyNo :</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtPolicyNo" runat="server" Width="100%" onkeypress="return isFloatKey(event);"></asp:TextBox>
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
       </tr>
       <tr>
       <td width="13%">
                       <label>
                       Sum Assured</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtSumAssured" runat="server" Width="100%" onkeypress="return isFloatKey(event);"></asp:TextBox>
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
       </tr>
       <tr>
       <td width="13%">
                       <label>
                       Date Of Maturity</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtDateOfMaturity" runat="server" Width="100%"></asp:TextBox>
                        <asp:CalendarExtender ID="TxtDateOfMaturity_CalendarExtender" runat="server"  Format="dd/MM/yyyy"
                            TargetControlID="TxtDateOfMaturity">
                        </asp:CalendarExtender>
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
       </tr>
       <tr>
       <td width="13%">
                       <label>
                       Surrender Value</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtSurrenderValue" runat="server" Width="100%" onkeypress="return isFloatKey(event);"></asp:TextBox>
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        <asp:ImageButton ID="ImgBtnInsurance" runat="server" 
                            ImageUrl="~/Images/AddBtn.png" OnClientClick="return ValidateSaveinsurance();" />
           </td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
       </tr>
           <tr>
               <td colspan="8">
                   <asp:GridView ID="DgvInsurance" runat="server" AutoGenerateColumns="False" CssClass="grid-view" 
                       Width="100%">
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


        <div id="DivBond" runat ="server"  visible="false"  >
               <br />
       <br />  
       <br />
                <hr />
        <table width="100%">
        <tr>
        <td width="13%">
                       <label>
                       Other Bonds</label></td>
                    <td width="13%">
                    </td>
                    <td width="2%">
                    </td>
                    <td width="13%">
                    </td>
                    <td width="13%">
                    </td>
                    <td width="2%">
                    </td>
                    <td width="13%">
                    </td>
                    <td width="13%">
                    </td>
        </tr>
        <tr>
        <td width="13%">
                       <label>
                       Issued Post Office :</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtIssuedPostOffice" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
        </tr>
        <tr>
        <td width="13%">
                       <label>
                       Serial No :</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtSerialNo" runat="server" Width="100%" onkeypress="return isFloatKey(event);"></asp:TextBox>
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
        </tr>
        <tr>
        <td width="13%">
                       <label>
                       Phase Value :</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtPhaseValue" runat="server" Width="100%" onkeypress="return isFloatKey(event);"></asp:TextBox>
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
        </tr>
        <tr>
        <td width="13%">
                       <label>
                       Due Date :</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtDueDate0" runat="server" Width="100%"></asp:TextBox>
                        <asp:CalendarExtender ID="TxtDueDate0_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                            TargetControlID="TxtDueDate0">
                        </asp:CalendarExtender>
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
        </tr>
        <tr>
        <td width="13%">
                       <label>
                       Total Phase Value :</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtTotalPhaseValue" runat="server" Width="100%" onkeypress="return isFloatKey(event);"></asp:TextBox>
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        <asp:ImageButton ID="ImgBtnBond" runat="server" 
                            ImageUrl="~/Images/AddBtn.png" OnClientClick="return ValidateSaveBond();" />
            </td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
        </tr>
            <tr>
                <td colspan="8">
                    <asp:GridView ID="DgvBond" runat="server" AutoGenerateColumns="False" CssClass="grid-view"
                        Width="100%">
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

        <div id="DivVehicle" runat ="server"  visible="false"  >
               <br />
       <br />  
       <br />
                <hr />
        <table width="100%">
        <tr>
        <td width="13%">
                       <label>
                       Vehicle</label></td>
                    <td width="13%">
                        
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
        </tr>
        <tr>
        <td width="13%">
                       <label>
                       Type :</label></td>
                    <td width="13%">
                        
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtType" runat="server" Width="100%"></asp:TextBox>
                        
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        <label>
                        RTOffice :</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtRTOffice" runat="server" Width="100%"></asp:TextBox>
            </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
        </tr>
        <tr>
        <td width="13%">
                       <label>
                       Region No :</label></td>
                    <td width="13%">
                        
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtRegionNo" runat="server" Width="100%" onkeypress="return isFloatKey(event);"></asp:TextBox>
                        
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        <label>
                        Lien Marked In RC :</label></td>
                    <td width="13%">
                        <asp:CheckBox ID="ChkLienMarked" runat="server" AutoPostBack="True" />
            </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
        </tr>
        <tr>
        <td width="13%">
                       <label>
                       Chassis No
                       </label>
                       </td>
                    <td width="13%">
                        
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtChassisNo" runat="server" Width="100%"></asp:TextBox>
                        
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        <label>
                        Lien No :</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtLienNo" runat="server" Width="100%" onkeypress="return isFloatKey(event);"></asp:TextBox>
            </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
        </tr>
        <tr>
        <td width="13%">
                       <label>
                       Engine No :</label></td>
                    <td width="13%">
                        
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtEngineNo" runat="server" Width="100%"></asp:TextBox>
                        
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        <label>
                        Lien Date :</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="txtLienDate" runat="server" Width="100%"></asp:TextBox>
                        <asp:CalendarExtender ID="txtLienDate_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                            TargetControlID="txtLienDate">
                        </asp:CalendarExtender>
            </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
        </tr>
        <tr>
        <td width="13%">
                       <label>
                       Old/New :</label></td>
                    <td width="13%">
                        
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        <label>
                        Lien Released Letter Date :</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtLienReleased" runat="server" Width="100%"></asp:TextBox>
                        <asp:CalendarExtender ID="TxtLienReleased_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                            TargetControlID="TxtLienReleased">
                        </asp:CalendarExtender>
            </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
        </tr>
        <tr>
        <td width="13%">
                       <asp:RadioButton ID="RadOld" runat="server" Text="Old" Width="100%" 
                           AutoPostBack="True" Checked="True" GroupName="Main" />
                       </td>
                    <td width="13%">
                        
                        <asp:RadioButton ID="RadNew" runat="server" Text="New" AutoPostBack="True" 
                            GroupName="Main" />
                        
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        <label>
                        Tax Details:</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtTextDetails" runat="server" Width="100%"></asp:TextBox>
            </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
        </tr>
        <tr>
        <td width="13%">
                       <label>
                       Brand :</label></td>
                    <td width="13%">
                        
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtBrand" runat="server" Width="100%"></asp:TextBox>
                        
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        <label>
                        Tax Date :</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtTextDate" runat="server" Width="100%"></asp:TextBox>
                        <asp:CalendarExtender ID="TxtTextDate_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                            TargetControlID="TxtTextDate">
                        </asp:CalendarExtender>
            </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
        </tr>
        <tr>
        <td width="13%">
                       <label>
                       Model :</label></td>
                    <td width="13%">
                        
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtModel" runat="server" Width="100%"></asp:TextBox>
                        
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        <label>
                        Insurance Details :</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtInsuranceDetails" runat="server" Width="100%"></asp:TextBox>
            </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
        </tr>
        <tr>
        <td width="13%">
                       <label>
                       Key Submited :</label></td>
                    <td width="13%">
                        
                        <asp:CheckBox ID="ChkKeySubmited" runat="server" AutoPostBack="True" />
                        
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        <label>
                        Insurance Date :</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtInsuranceDate0" runat="server" Width="100%"></asp:TextBox>
                        <asp:CalendarExtender ID="TxtInsuranceDate0_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                            TargetControlID="TxtInsuranceDate0">
                        </asp:CalendarExtender>
            </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
        </tr>
        <tr>
        <td width="13%">
                       <label>
                       Key No :</label></td>
                    <td width="13%">
                        
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtKeyNo" runat="server" Width="100%"></asp:TextBox>
                        
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        <label>
                        Insurance Value :</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtInsuranceValue" runat="server" Width="100%" onkeypress="return isFloatKey(event);"></asp:TextBox>
            </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
        </tr>
        <tr>
        <td width="13%">
                       <label>
                       Permit Scope :</label></td>
                    <td width="13%">
                        
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtPermitScope" runat="server" Width="100%"></asp:TextBox>
                        
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        <label>
                        Other Details :</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtOtherDetails0" runat="server" Width="100%"></asp:TextBox>
            </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        <asp:ImageButton ID="ImgBtnVehicle" runat="server" 
                            ImageUrl="~/Images/AddBtn.png" OnClientClick="return ValidateaddVehicle();" />
            </td>
                    <td width="13%">
                        &nbsp;</td>
        </tr>
            <tr>
                <td colspan="8">
                    <asp:DropDownList ID="DdlScheme" runat="server" Width="100%">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                    <asp:GridView ID="DgvVehicle" runat="server" AutoGenerateColumns="False" CssClass="grid-view"
                        Width="100%">
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
                            <asp:BoundField DataField="PermitScope" HeaderText="Permit Scope" 
                                Visible="False" />
                            <asp:BoundField DataField="RtOffice" HeaderText="Rt Office" Visible="False" />
                            <asp:BoundField DataField="LienMarked" HeaderText="Lien Marked In RC" />
                            <asp:BoundField DataField="LienNo" HeaderText="Lien No" />
                            <asp:BoundField DataField="LienDate" HeaderText="Lien Date" />
                            <asp:BoundField DataField="LienReleased" HeaderText="Lien Released Letter Date" 
                                Visible="False" />
                            <asp:BoundField DataField="TextDetails" HeaderText="Text Details" 
                                Visible="False" />
                            <asp:BoundField DataField="TaxDate" HeaderText="Tax Date" />
                            <asp:BoundField DataField="InsuranceDetails" HeaderText="Insurance Details" 
                                Visible="False" />
                            <asp:BoundField DataField="InsuranceDate" HeaderText="Insurance Date" />
                            <asp:BoundField DataField="InsuranceValue" HeaderText="Insurance Value" />
                            <asp:BoundField DataField="OtherDetails" HeaderText="Other Details" 
                                Visible="False" />
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        </div>
        

        <div id="DivTermDeposit" runat ="server"  visible="false"  >
               <br />
       <br />  
       <br />
                <hr />
        <table width="100%">
        <tr>
        <td width="13%">
                       <label>
                       Term Deposits</label></td>
                    <td width="13%">
                        
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
        </tr>
        <tr>
        <td width="13%">
                       <asp:CheckBox ID="ChkOtherBankAc" runat="server" Text="Other Bank A/c" AutoPostBack="True" />
                       </td>
                    <td width="13%">
                        
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        <label>
                        Bank Name :</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtBankName" runat="server" Width="100%"></asp:TextBox>
            </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
        </tr>
            <tr>
                <td width="13%">
                    <label>
                    <asp:Label ID="lblbranch" runat="server" Text="Branch"></asp:Label>
                    </label>
                </td>
                <td width="13%">
                    <asp:DropDownList ID="CmbBranch1" runat="server" AutoPostBack="True" TabIndex="4" Width="100%">
                    </asp:DropDownList>
                </td>
                <td width="2%">&nbsp;</td>
                <td width="13%">&nbsp;</td>
                <td width="13%">&nbsp;</td>
                <td width="2%">&nbsp;</td>
                <td width="13%">&nbsp;</td>
                <td width="13%">&nbsp;</td>
            </tr>
        <tr>
        <td width="13%">
                       <label>
                       Scheme</label></td>
                    <td width="13%">
                        
                        <asp:DropDownList ID="DdlScheme0" runat="server" Width="100%">
                        </asp:DropDownList>
            </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        <label>
                        Branch :</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TextBox1" runat="server" Width="100%"></asp:TextBox>
            </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
        </tr>
        <tr>
        <td width="13%">
                       <label>
                       A/c No :</label></td>
                    <td width="13%">
                        
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtAcNo" runat="server" Width="100%" onkeypress="return isFloatKey(event);" AutoPostBack="True"></asp:TextBox>
                        
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        <label>
                        A/c Type :</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtAcType" runat="server" Width="100%"></asp:TextBox>
            </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
        </tr>
        <tr>
        <td width="13%">
                       <label>
                       Certificate No</label></td>
                    <td width="13%">
                        
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtCertificateNo" runat="server" Width="100%" onkeypress="return isFloatKey(event);"></asp:TextBox>
                        
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
        </tr>
        <tr>
        <td width="13%">
                       <label>
                       Deposite Amt :</label></td>
                    <td width="13%">
                        
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtDepositeAmt" runat="server" Width="100%"></asp:TextBox>
                        
                    </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        <label>
                        Customer No :</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtCustomerNo" runat="server" Width="100%" onkeypress="return isFloatKey(event);"></asp:TextBox>
            </td>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
                    <td width="13%">
                        &nbsp;</td>
        </tr>
        <tr>
        <td width="13%" >
                       <label>
                       Amt Outstanding :</label></td>
                    <td width="13%" >
                        
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtAmtOutstanding" runat="server" Width="100%" onkeypress="return isFloatKey(event);"></asp:TextBox>
                        
                    </td>
                    <td width="2%" >
                        </td>
                    <td width="13%" >
                        <label>
                        Name :</label></td>
                    <td width="13%" >
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TextBox2" runat="server" Width="100%"></asp:TextBox>
            </td>
                    <td width="2%" >
                        </td>
                    <td width="13%" >
                        </td>
                    <td width="13%" >
                        </td>
        </tr>
        <tr>
        <td width="13%">
                       <label>
                       Due Date</label></td>
                    <td width="13%">
                        
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtDueDate5" runat="server" Width="100%"></asp:TextBox>
                        
                        <asp:CalendarExtender ID="TxtDueDate5_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                            TargetControlID="TxtDueDate5">
                        </asp:CalendarExtender>
                        
                    </td>
                    <td width="2%">
                        </td>
                    <td width="13%">
                        <label>
                        Address :</label></td>
                    <td width="13%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtAddress1" runat="server" Width="100%" TextMode="MultiLine"></asp:TextBox>
            </td>
                    <td width="2%">
                        </td>
                    <td width="13%">
                        <asp:ImageButton ID="ImgBtnTermDeposit" runat="server" 
                            ImageUrl="~/Images/AddBtn.png" OnClientClick="return  ValidatetermDeposit();" />
            </td>
                    <td width="13%">
                        </td>
        </tr>
            <tr>
                <td colspan="8">
                    <asp:GridView ID="DgvDeposit" runat="server" AutoGenerateColumns="False" CssClass="grid-view" 
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="OtherBankAC" HeaderText="Other Bank A/C" 
                                Visible="False" />
                             <asp:BoundField DataField="Branch1" HeaderText="Branch" visible="false"/>
                             <asp:BoundField DataField="BranchDtls1" HeaderText="Branch" />
                           <asp:BoundField DataField="Scheme" HeaderText="Scheme" visible="false"/>
                             <asp:BoundField DataField="SchemeDtls" HeaderText="Scheme" />
                            <asp:BoundField DataField="AcNo" HeaderText="A/C No" />
                            <asp:BoundField DataField="CertificateNo" HeaderText="Certificate No" 
                                Visible="False" />
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
                    <td width="2%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="2%">
                        &nbsp;</td>
                    <td width="40%">
                        &nbsp;</td>
                    <td width="56%">
                        &nbsp;</td>
                    <td width="2%">
                        &nbsp;</td>
                </tr>
               
            </table>

</div>





<table width="100%">



     <tr>
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
<td align="center" colspan="2">
    <asp:ImageButton ID="BtnSave" runat="server" ImageUrl="~/Images/Save.png" OnClientClick="return  ValidateCheckLoanAppNo();"/>
    &nbsp;<asp:ImageButton ID="Clear" runat="server" 
        ImageUrl="~/Images/Clear.png" />
    &nbsp;<asp:ImageButton ID="close" runat="server" 
        ImageUrl="~/Images/Close.png" PostBackUrl="~/Main.aspx" />
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td> </td>
<td>
     </td>
<td>
    &nbsp;</td>
<td> </td>
</tr>
</table>
    </div>
   </ContentTemplate>
</asp:UpdatePanel> 


  
</asp:Content>

