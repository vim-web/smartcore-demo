<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="Goldopen.aspx.vb" Inherits="SMARTCORE_UI.Goldopen" title="GOLD LOAN" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/ui-lightness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript' src="Scripts/Common.js"></script>
    <script type='text/javascript' src="Scripts/jquery-1.8.3.js"></script>  
    <script type='text/javascript' src="Scripts/jquery-ui-1.9.2.custom.min.js"></script>      
    <script type="text/javascript">
        var safe = function (id)
        { return document.getElementById(id); }
        function show(id)
        {
            safe(id).style.display = "block";
        }
        function hide(id)
        {
            safe(id).style.display = "none";
        }
        $(function ()
        {
            var Ctl = document.getElementById('<%=TxtOrnCode.ClientID%>');
            Ctl.focus();
        });        
        function ValidateDELETE()
        {
            var r = confirm("Do You Want to Delete ? ");
            if (r == true)
            {
                return true;
            }
            else
            {
                return false;
            }
            return true;
        }
        function imgLiab_onclick()
        {
            $('.gradientBoxesWithOuterShadows').append('<div id="resultdiv" title="GOLD Liablities" visible="false" >');
            var custID = document.getElementById("<%=HdnCustID.ClientID%>").value;
            $("#resultdiv").load("AssetLiab.aspx?CustID=" + custID + "&FilterOpt=GO", {}, function (data)
            {
                $("#resultdiv").show();
            });
            $("#resultdiv").dialog({
                autoOpen: false,
                height: 600,
                width: 1200,
                beforeClose: function(event, ui) { $('#resultdiv').remove(); },
                modal: true
            });
            $('#resultdiv').dialog('open');
        }
    </script>    
    <style type="text/css">
    label 
    {
	    color: #03F;
    }
    </style>
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
.TitleBar 
    {
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
    .style1
    {
        height: 26px;
    }    
    .style2
    {
        width: 100%;
    }        
    .py-2 {
        width: 788px;
    }        
    .py-3 {
        margin-left: 224px;
    }        
    </style>
<script language="javascript" type="text/javascript">        
function fSplitValues(source, eventArgs) {
var sStr;
var extender = source.get_id();         //id of the source extender            
    if (document.getElementById("ctl00_ContentPlaceHolder1_TxtOrnCode").value != "") {          
        sStr = document.getElementById("ctl00_ContentPlaceHolder1_TxtOrnCode").value;
        sStr = sStr.replace(/  /g,"")
        sStr = sStr.replace("Code:","")
        sStr = sStr.replace(".","")
        if (sStr.indexOf("|") != -1) {                
            sStr = sStr.split("|");
            document.getElementById("ctl00_ContentPlaceHolder1_TxtOrnCode").value = sStr[1];
            document.getElementById("ctl00_ContentPlaceHolder1_TxtOrnName").value = sStr[0].replace(".","");                        
            document.getElementById("ctl00_ContentPlaceHolder1_TxtOrnDetails").focus();
        }
    }
}

        function ValidateSave() {
            // Validate Appraiser
            objDDl = document.getElementById('<%=CmbAppraiser.ClientID%>');
            var selval = objDDl.options[objDDl.selectedIndex].value;

            // Not Numeric
           
            objDDl = document.getElementById('ctl00_ContentPlaceHolder1_Hid_ODPStatus');
            if (objDDl.value == "N") {
                alertify.error("Please Verify Mobile NO");
                return false;
            }
            var r = confirm("Do You Want to Save ? ");
            if (r == true)
            {
                return true;
            }
            else
            {
                return false ;
            }  
            return true;
        }
</script>
<script language="javascript" type="text/javascript">
    function onlyNumbers(event) {
        var charCode = (event.which) ? event.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            alertify.error("Enter Only Numbers");
            return false;
        }
        else
            return true;
    }
    </script>
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
<script type="text/javascript">
Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
function BeginRequestHandler(sender, args)
{
    args.get_postBackElement().disabled = true;
}
</script>
    <div id="cover"></div> 
    <div class="popup" id="VerifyMob" style="width:200px;height:100px; margin-left:10%; margin-top:200px; ">
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
<div style="width: 100%; float:left; " >                
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
                <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtNoOfOperator" onkeypress="return onlyNumbers(event)" runat="server" Width = "97%" TabIndex="999" MaxLength="3"></asp:TextBox>
            </td>
        <td></td>
      </tr>
    </table>
    <table width="100%" border="0">
        <tr>
            <td width="2%"></td>
            <td width="96%">
                <asp:GridView ID="DgvCustomer" runat="server" AutoGenerateColumns="False"  CssClass="grid-viewNew"
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
                <p><hr />
                    <p>
                    </p>
                    <asp:GridView ID="DgvOrnaments" runat="server" AutoGenerateColumns="False" 
                        CssClass="grid-viewNew" DataKeyNames="CODE" Font-Names="Tahoma" 
                        Font-Size="Medium" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="Code" HeaderText="Code" />
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="Details" HeaderText="Details" />
                            <asp:BoundField DataField="Purity" HeaderText="Purity">
                                <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                    BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="15%" />
                                <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                    Font-Size="Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Count" HeaderText="Count" />
                            <asp:BoundField DataField="GrossWt" HeaderText="GrossWt" />
                            <asp:BoundField DataField="NetWt" HeaderText="NetWt" />
                            <asp:BoundField DataField="EligibleAmt" HeaderText="EligibleAmnt" />
                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                            <asp:CommandField DeleteText="&lt;img src='./images/del.jpg'&gt;" 
                                ShowDeleteButton="True">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                        </Columns>
                    </asp:GridView>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                </p>
                
            </td>
            <td width="2%"></td>
        </tr>
    </table>
    <table border="0" width="100%">
        <tr>
            <td width="2%">&nbsp;</td>
            <td width="96%">                
            <table border="0" width="100%">
                <tr style="font-family: Tahoma; font-size: medium">
                    <td width="10%"><label>Code</label></td>
                    <td width="30%"><label>Ornament Name</label></td>
                    <td width="25%">
                    <table width ="100%">
                    <tr>
                    <td Width="40%"><label>Details</label></td>
                    <td Width="30%"><label>Purity</label></td>
                    <td Width="30%"><label>Count</label></td>
                    </tr>
                    
                    </table>

                    </td>
                   
                    <td width="10%"><label>Gross Weight</label></td>
                    <td width="10%"><label>Net Weight</label></td>
                    <td width="15%"><label>Remarks</label></td>
                </tr>
                <tr>
                    <td width="10%" style="margin-left: 40px">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtOrnCode" runat="server" Width="96%" AutoPostBack="True"></asp:TextBox>
                        <asp:autocompleteextender ID="AutoCompleteExtender1" runat="server" 
                            TargetControlID="TxtOrnCode" MinimumPrefixLength="1" CompletionInterval="10" 
                            EnableCaching="true" CompletionSetCount="6"
                            UseContextKey="True" ServiceMethod="GetCompletionList" 
                            OnClientItemSelected="fSplitValues" 
                            CompletionListCssClass="wordWheel listMain .box" 
                            CompletionListItemCssClass="wordWheel itemsMain" 
                            CompletionListHighlightedItemCssClass="wordWheel itemsSelected">
                        </asp:autocompleteextender>
                    </td>
                    
                    
                    
                    
                    
                    <td width="30%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtOrnName" runat="server" Width="96%" TabIndex="1"></asp:TextBox></td>
                        
                    <td width="25%">
                    <table width ="100%">
                    <tr>
                    <td Width="40%"><asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtOrnDetails" runat="server" Width="96%" TabIndex="2"></asp:TextBox></td>
                    <td Width="30%"><asp:DropDownList ID="CmbPurity" runat="server" Width="100%" TabIndex="3">
            </asp:DropDownList></td>
                    <td Width="30%"><asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtCount" runat="server" Width="96%" 
                            onkeypress="return isFloatKey(event);" style="text-align:right" TabIndex="3" ></asp:TextBox>
                    </td>
                    </tr>
                    
                    </table>

                    </td>                
                        
                        
<%--                    <td width="15%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtOrnDetails" runat="server" Width="96%" TabIndex="2"></asp:TextBox></td>
                    <td width="10%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtCount" runat="server" Width="96%" 
                            onkeypress="return isFloatKey(event);" style="text-align:right" TabIndex="3" ></asp:TextBox></td>--%>
                    <td width="10%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtGrossWt" runat="server" Width="96%" 
                            onkeypress="return isFloatKey(event);" style="text-align:right" 
                            TabIndex="4" ></asp:TextBox></td>
                    <td width="10%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtNetWt" runat="server" Width="96%" 
                            onkeypress="return isFloatKey(event);" style="text-align:right" 
                            TabIndex="5"></asp:TextBox></td>
                    <td width="15%">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtRemarks" runat="server" Width="96%" TabIndex="6"></asp:TextBox></td>
                </tr>
                <%--<tr>
                    <td style="margin-left: 40px" width="10%">&nbsp;</td>
                    <td width="30%"> <asp:FileUpload ID="FileUpload4" runat="server" CssClass="tb8" /></td>
                    <td width="25%">&nbsp;</td>
                    <td width="10%">&nbsp;</td>
                    <td width="10%">&nbsp;</td>
                    <td width="15%">&nbsp;</td>
                </tr>--%>
                <%--<tr>
                    <td style="margin-left: 40px" width="10%">&nbsp;</td>
                    <td width="30%"> <asp:Image ID="ImgPhoto" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                            Height="150px" ImageUrl="~/Images/no_photo.jpg" TabIndex="56" Width="130px" />
                        <asp:Button ID="btn_Upload" runat="server" CssClass="btn" Height="31px" OnClientClick="return CheckFile()"
                            OnClick="btn_Upload_Click" Text="Upload" /></td>
                    <td width="25%">&nbsp;</td>
                    <td width="10%">&nbsp;</td>
                    <td width="10%">&nbsp;</td>
                    <td width="15%">&nbsp;</td>
                </tr>--%>
            </table>
            </td>
            <td width="2%" valign="bottom">
                
                <asp:Button ID="CmdOrnAdd" runat="server" BackColor="#9999FF" TabIndex="7" 
                    Text="+" UseSubmitBehavior="False" Width="100%" />
                
            </td>
        </tr>
    </table>
     <div  class="col-lg-12 py-2">
     <p style="color:blue; font-size:16px">Image Upload</p>
         <div></div>
          <table style="width: 780px" >
              <tr>
                  <td></td> <td></td>
                  <td> <label >Browse Weight </label> </td>
                  <td> <label >Browse Ornament </label> </td>
              </tr>
              <tr>
                   <td></td> <td></td>
                  <td>
<asp:FileUpload ID="FileUpload4" runat="server" CssClass="tb8" />
                  </td>

                 <td>
<asp:FileUpload ID="FileUpload5" runat="server" CssClass="tb8" />
                  </td>    </tr>
              <tr>
                   <td></td> <td></td>
                  <td>
<asp:Image ID="ImgPhoto" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                            Height="150px" ImageUrl="~/Images/no_photo.jpg" TabIndex="56" Width="130px" />
                  </td>
                  <td>
<asp:Image ID="ImgPhoto2" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                            Height="150px" ImageUrl="~/Images/no_photo.jpg" TabIndex="56" Width="130px" />
                  </td>
                 
                      <%--  <td>

                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="grid-viewNew" DataKeyNames="Id" Font-Names="Tahoma" Width="100%">
                              <Columns>
                                  <asp:CommandField SelectText="View" ShowSelectButton="True" />
                                  <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                                 <%-- <asp:BoundField DataField="txt_DocDetails" HeaderText="Proof Type" />
                                  <asp:BoundField DataField="Doc_Typ" HeaderText="Document Type" visible="false" />
                                  <asp:BoundField DataField="Doc_Name" HeaderText="Document Name" />
                                  <asp:BoundField DataField="FileType" HeaderText="FileType" visible="false" />
                                  <asp:BoundField DataField="FileData" HeaderText="File Data" visible="false" />
                                  <asp:BoundField DataField="FilePath" HeaderText="FilePath" visible="false" />
                                  <asp:CommandField SelectText="Delete" ShowDeleteButton="True">
                                  <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" Width="5%" />
                                  </asp:CommandField>
                              </Columns>
                          </asp:GridView>
                      </td>--%>
              </tr>
              <tr>
                   <td></td> <td></td>
                  <td>
       <asp:Button ID="btn_Upload" runat="server" CssClass="btn" Height="31px" OnClientClick="return CheckFile()"
                            OnClick="btn_Upload_Click" Text="Upload" />
</td>
                  <td>
       <asp:Button ID="btn_Upload2" runat="server" CssClass="btn" Height="31px" OnClientClick="return CheckFile()"
                            OnClick="btn_Upload2_Click" Text="Upload" />
</td>
              </tr>
          </table>
         
                     
         
        
     </div>


<table width="100%" border="0" style="font-family: Tahoma; font-size: medium">

  <tr>
    <td width="2%" class="style1"></td>
    <td width="13%" class="style1"><label>Loan Date</label></td>
    <td width="17%" class="style1">
        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtLoanDt" runat="server" Width="96%" TabIndex="8"></asp:TextBox>
    </td>
    <td width="2%" class="style1"></td>
    <td width="13%" class="style1"><label>No of Months</label></td>
    <td width="18%" class="style1">
        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtPrd" runat="server" Width="96%" 
            TabIndex="9" AutoPostBack="True"></asp:TextBox></td>
    <td width="2%" class="style1"></td>
    <td width="13%" class="style1"><label>Loan Due Date</label></td>
    <td width="18%" class="style1">
        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtDueDate" runat="server" 
            Width="96%" ForeColor="#FF3300" Font-Bold="True" TabIndex="10" ></asp:TextBox></td>
    <td width="2%" class="style1"></td>
  </tr>
  <tr>
    <td width="2%" class="style1"></td>
    <td width="13%" class="style1"><label>Total Count</label></td>
    <td width="17%" class="style1">
        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtTotalCount" runat="server" Width="96%" 
            onkeypress="return isFloatKey(event);" style="text-align:right" TabIndex="11" ></asp:TextBox>
    </td>
    <td width="2%" class="style1"></td>
    <td width="13%" class="style1"><label>Total Gross Weight</label></td>
    <td width="18%" class="style1">
        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtTotalGrossWt" runat="server" Width="96%"
            onkeypress="return isFloatKey(event);" style="text-align:right" 
            TabIndex="12" ></asp:TextBox>
    </td>
    <td width="2%" class="style1"></td>
    <td width="13%" class="style1"><label>Total Net Weight</label></td>
    <td width="18%" class="style1">
        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtTotalNetWt" runat="server" Width="96%" 
            onkeypress="return isFloatKey(event);" style="text-align:right" 
            TabIndex="13" AutoPostBack="True" ></asp:TextBox>
    </td>
    <td width="2%" class="style1"></td>
  </tr>

  <tr>
    <td width="2%" class="style1"></td>
    <td width="13%" class="style1"><label>Market Rate</label></td>
    <td width="17%" class="style1">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtMarketRate" runat="server" Width="96%" TabIndex="14" onkeypress="return isFloatKey(event);" style="text-align:right" ></asp:TextBox>
                    </td>
    <td width="2%" class="style1"></td>
    <td width="13%" class="style1"><label>Market Price</label></td>
    <td width="18%" class="style1">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtMarketPrice" runat="server" Width="96%" TabIndex="15" onkeypress="return isFloatKey(event);" style="text-align:right" ></asp:TextBox>
                    </td>
    <td width="2%" class="style1"></td>
    <td width="13%" class="style1"><label>Bank Rate</label></td>
    <td width="18%" class="style1">
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtBankRate" runat="server"  Width="96%" TabIndex="16" onkeypress="return isFloatKey(event);" style="text-align:right" ></asp:TextBox>
                    </td>
    <td width="2%" class="style1"></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td><label>Eligible Amt</label></td>
    <td>
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtEligibleAmt" runat="server"  Width="96%" TabIndex="17" onkeypress="return isFloatKey(event);" style="text-align:right" ></asp:TextBox>
                    </td>
    <td>&nbsp;</td>
    <td><label>Appraiser Value</label></td>
    <td>
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtAppraiserAmt" runat="server"  Width="96%" TabIndex="18" onkeypress="return isFloatKey(event);" style="text-align:right" ></asp:TextBox>
                    </td>
    <td>&nbsp;</td>
    <td><label>Sanctioned Amt</label></td>
    <td>
<asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtSanctionedAmt" runat="server"  Width="96%" 
    ForeColor="#FF3300" AutoPostBack="True" Font-Bold="True" TabIndex="19" onkeypress="return isFloatKey(event);" style="text-align:right" ></asp:TextBox>
                    </td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td>
        <label> 
        Transfer Amt</label></td>
    <td>
            <asp:TextBox ID="TxtTransfer" runat="server" AutoPostBack="True" 
                CssClass="twitterStyleTextbox" Font-Bold="True" ForeColor="#FF3300" 
                onkeypress="return isFloatKey(event);" style="text-align:right" TabIndex="19" 
                Width="96%"></asp:TextBox>
        </td>
    <td>&nbsp;</td>
    <td><label>Sanctioned by</label></td>
    <td>
            <asp:DropDownList ID="CmbSanctionedBy" runat="server" Width="100%" 
                TabIndex="21">
            </asp:DropDownList>
        </td>
    <td>&nbsp;</td>
    <td><label>Interest Rate</label></td>
    <td>
        <table width="100%">
        <tr>
            <td  width="50%">
                <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtIntRate" runat="server" ForeColor="#FF3300" 
                     Width="90%" Font-Bold="True" TabIndex="22" onkeypress="return isFloatKey(event);" style="text-align:right" ></asp:TextBox></td>
            <td  width="50%">
                <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtPIntRate" runat="server" ForeColor="#FF3300" 
                     Width="90%" Font-Bold="True" TabIndex="23" onkeypress="return isFloatKey(event);" style="text-align:right" ></asp:TextBox></td>
        </tr>
        </table>
    </td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td></td>
    <td><label>Purpose</label></td>
    <td>
            <asp:DropDownList ID="CmbPurpose" runat="server" Width="100%" 
                AutoPostBack="True" TabIndex="24">
            </asp:DropDownList>
        </td>
    <td></td>
    <td><label>Sub Purpose</label></td>
    <td>
            <asp:DropDownList ID="CmbSubPurpose" runat="server" Width="100%" TabIndex="25">
            </asp:DropDownList>
        </td>
    <td></td>
    <td><label>Priority</label></td>
    <td>
            <asp:DropDownList ID="CmbPriority" runat="server" Width="100%" TabIndex="26">
            </asp:DropDownList>
        </td>
    <td></td>
  </tr>
    <tr>
        <td class="style1">
            &nbsp;</td>
        <td class="style1">
            <label>
            Appraiser</label></td>
        <td class="style1">
            <asp:DropDownList ID="CmbAppraiser" runat="server" TabIndex="20" Width="100%">
            </asp:DropDownList>
        </td>
        <td class="style1">
            &nbsp;</td>
        <td class="style1">
            <label>
            Survey No.</label></td>
        <td class="style1">
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtSurveyNo" runat="server" Width="96%" TabIndex="28"></asp:TextBox>
        </td>
        <td class="style1">
            &nbsp;</td>
        <td class="style1">
            <label>Nominee</label></td>
        <td class="style1">
            <asp:DropDownList ID="CmbNomReq" runat="server" AutoPostBack="True" TabIndex="24" Width="100%">
            </asp:DropDownList>
        </td>
        <td class="style1">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style1">&nbsp;</td>
        <td class="style1">
            <label>
            Mobile No.</label></td>
        <td class="style1">
            <asp:TextBox ID="TxtMobileNo" runat="server" CssClass="twitterStyleTextbox" TabIndex="28" Width="96%"></asp:TextBox>
        </td>
        <td class="style1">&nbsp;</td>
        <td class="style1">
            <div style="float:left;" id="div_Verify"><asp:Button ID="But_verfiy" runat="server" Text="Verify"   style="width:50px; height:20px;" /></div> 
            <img src="Images/verify.jpg" style=" float:left; display:none;" id="imgVerify" />
           <img src="Images/Verifyerror.jpg" style=" float:left;display:none;" id="imgverifyError" />
        </td>
        <td class="style1">
            <asp:CheckBox ID="ChkBank" runat="server" Text="Bank Details" visible="false"
                AutoPostBack="True" />
        </td>
        <td class="style1">&nbsp;</td>
        <td class="style1">
            <label>
            Cash/Transfer</label></td>
        <td class="style1">
            <asp:DropDownList ID="CmbCashTransfer" runat="server" AutoPostBack="True" 
                TabIndex="27" Width="100%">
            </asp:DropDownList>
        </td>
        <td class="style1">&nbsp;</td>
    </tr>
    <tr>
        <td class="style1">
            &nbsp;</td>
        <td class="style1">
            <label>IRR Rate</label></td>
        <td class="style1">
            <asp:TextBox ID="TxtIRR" Enabled="false" Width="100%" runat="server"></asp:TextBox></td>
        <td class="style1">
            &nbsp;</td>
        <td class="style1">
           <label>Advance EMI</label></td>
        <td class="style1">
            <asp:TextBox ID="TxtAdvEmi" Enabled="false" Width="100%" runat="server"></asp:TextBox></td>
        <td class="style1">
            </td>
        <td class="style1">
            <asp:ImageButton ID="btnEMI" runat="server" Enabled="True" 
                ImageUrl="~/Images/TrImgEMI.png" TabIndex="999" Visible="true" />
        </td>
        <td class="style1">
            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                Text="Processing Fee Change to Cash" />
        </td>
        <td class="style1">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style1">
            &nbsp;</td>
        <td class="style1">
            <label>EMI Amnt</label></td>
        <td class="style1">
            <asp:TextBox ID="TxtInstAmnt" Enabled="false" Width="100%" runat="server"></asp:TextBox></td>
        <td class="style1">
            &nbsp;</td>
        <td class="style1">
            <label>Canvassed By</label></td>
        <td class="style1">
            <asp:DropDownList ID="CmbCanvassedBy" runat="server"  Width="100%">
            </asp:DropDownList>
        </td>
        <td class="style1">
            &nbsp;</td>
        <td class="style1">
            &nbsp;</td>
        <td class="style1">
            &nbsp;</td>
        <td class="style1">
            &nbsp;</td>
    </tr>
    <caption>
        <br />
     
    </caption>

</table>
    <br />
    
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
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </div>
    <br />
     <div id="DivBankDetail" runat="server" visible="false">
   
       
   
     
   
       
   
         <table class="style2" width="100%">
             <tr>
                 <td width="25">
                     &nbsp;</td>
                 <td width="50%">
                     &nbsp;</td>
                 <td width="25%">
                     &nbsp;</td>
             </tr>
             <tr>
                  <td width="25">
                     &nbsp;</td>
                 <td width="50%" rowspan="2">
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
                  <td width="25">
                     &nbsp;</td>
                 <td width="25%">
                     &nbsp;</td>
             </tr>
             <tr>
                 <td width="25">
                     &nbsp;</td>
                 <td width="50%">
                     &nbsp;</td>
                 <td width="25%">
                     &nbsp;</td>
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
            </td>
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
    </table>
<br />
</asp:Panel>
	<br/>   
	<table width="100%">
	<tr>
	<td width ="35%">
        <asp:ImageButton ID="ImgGoldLiab" runat="server" ImageUrl="~/Images/Liab.png" OnClientClick="return imgLiab_onclick();" 
            TabIndex="58" />
        &nbsp;<asp:ImageButton ID="ImgDetails" runat="server" ImageUrl="~/Images/TrImg5.png" 
            OnClientClick="return ImgDetails_onclick();" TabIndex="999" />
        </td>
	<td width ="30%">
            <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                meta:resourcekey="CmdSaveResource1" TabIndex="29" OnClientClick="return ValidateSave();"  />
            <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" 
                meta:resourcekey="CmdSaveResource1" TabIndex="29" />
            <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/images/Close.png" 
                meta:resourcekey="CmdClearResource1" TabIndex="30" />
                	<asp:ImageButton ID="cmdDelete" runat="server" ImageUrl="~/images/DELETE.png" Visibl="false"
                meta:resourcekey="CmdSaveResource1" TabIndex="29" OnClientClick="return ValidateDELETE();"  />
    </td>
	<td width = "35%">
        <asp:HiddenField ID="HdnPrdType" runat="server" />
        </td>
	</tr>
	    <tr>
            <td width="35%">
                <asp:HiddenField ID="HdnMemBrCode" runat="server" />
            </td>
            <td width="30%">
                <asp:HiddenField ID="HdnMemType" runat="server" />
                <asp:HiddenField ID="HdnCustID" runat="server" />
                <asp:HiddenField ID="ImageID" runat="server" Value="N"/>
                <asp:HiddenField ID="ImageID2" runat="server" Value="N"/>
            </td>
            <td width="35%">
                <asp:HiddenField ID="HdnMemNo" runat="server" />
            </td>
        </tr>
	</table>
</div>
</ContentTemplate>
        <Triggers>
       <asp:PostBackTrigger ControlID="btn_Upload" />
       <asp:PostBackTrigger ControlID="btn_Upload2" />
       </Triggers>
</asp:UpdatePanel>
</asp:Content>
