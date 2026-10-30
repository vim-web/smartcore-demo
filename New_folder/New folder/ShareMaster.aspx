<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" Inherits="SMARTCORE_UI.ShareMaster" CodeBehind="ShareMaster.aspx.vb"  title="SHARE MASTER" %>

 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
<script type='text/javascript' src="scripts/jquery_json.js"></script>
<script type='text/javascript' src="Scripts/Common.js"></script>  
<script type='text/javascript' src="Scripts/jquery-1.8.3.js"></script>  
<link href="CSS/ui-lightness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
<script type='text/javascript' src="Scripts/jquery-ui-1.9.2.custom.min.js"></script>      

<script type="text/javascript">
    $(function() {
        $('MasterAccDtls').attr("disabled", true);
    });

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
    function ValidateSave() {
        // Blocking Save Incase of Amount Not Entered
        var RctCash = document.getElementById('<%=LblRctCashTotal.ClientID%>').innerHTML;
        var RctTrans = document.getElementById('<%=LblRctTransferTotal.ClientID%>').innerHTML;
        var PayCash = document.getElementById('<%=LblPayCashTotal.ClientID%>').innerHTML;
        var PayTrans = document.getElementById('<%=LblPayTransferTotal.ClientID%>').innerHTML;
        var ShareNo = document.getElementById('<%=Txt_ShareNo.ClientID%>').value;
        // Not Numeric
        if (parseFloat(ShareNo) == 0) {
            alertify.error('Share Nos Not Entered, Please Check');
            document.getElementById('<%=Txt_ShareNo.ClientID%>').focus();
            return false;
        }

        if (RctCash == "") {
            alertify.error('Amount Not Entered, Please Check');
            return false;
        }
        
        if (parseFloat(RctCash) + parseFloat(RctTrans) + parseFloat(PayCash) + parseFloat(PayTrans) == 0 ) {
            alertify.error('Amount Not Entered, Please Check');
            return false;
        }

        return true;
    }

</script>
   
<%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>
    <script type = "text/javascript" src = "http://www.aspsnippets.com/scripts/ScrollableGridPlugin.js?22_12"></script>
    <script type = "text/javascript">
    
    // To Bind the function to the textbox for moving the cursor from one textbox to another  
            $(document).ready(function () {  
                $("input:text:first").focus();  
                $("input:text").bind("keydown", function (e) {  
                    if (e.which == 13) {  
                        e.preventDefault();  
                        var nextIndex = $('input:text').index(this) + 1;  
                        if ($('input:text')[nextIndex] != null)  
                            $('input:text')[nextIndex].focus();  
                    }  
                });  
            });  
            
          </script>--%>


    <style type="text/css">
        .style1
        {
            width: 110px;
        }
        .style2
        {
            width: 744px;
        }
    </style>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

    <div style="width: 98%; margin-left:1%; margin-right:1%;margin-top:1%;" >  
    
      <asp:Panel ID="PnlAuthorise" runat="server">
                              
    <legend>SHARE ACCOUNT</legend>                                                   
   <br />
    <div id="MasterAccDtls" >
    <table width="100%" border="0" style="font-family: Tahoma">
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
                <asp:DropDownList ID="CmbScheme" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
        <td></td>
        <td><label>No of Operators</label></td>
        <td>
                <asp:TextBox ID="TxtNoOfOperator" runat="server" Width = "98%" TabIndex="999"></asp:TextBox>
            </td>
        <td></td>
      </tr>
    </table>
    </div>
        <table border="0" width="100%">
                        <tr>
                            <td width="2%">
                            </td>
                            <td width="96%"  style="padding:30px 0px 30px 0px">
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
                            </td>
                            <td width="2%">
                            </td>
                        </tr>
                    </table>
        
        <table  width="100%">
        
         <tr>
        <td width="2%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
        <td width="2%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
        <td width="2%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
        <td width="15%">&nbsp;</td>
        <td width="2%">&nbsp;</td>
        </tr>
      
            <tr>
            <td></td>
                <td >
                    <asp:Label ID="Lbl_AddmissionDate" runat="server" Text="Admission Date"  CssClass="lblCSS" 
                        ></asp:Label>
                    
                </td>
                <td >
                    <asp:TextBox ID="Txt_AdmissionDate" runat="server"  
                       Width="96%" CssClass="twitterStyleTextbox" ></asp:TextBox>

                </td>
                
                <td></td>
                <td >
                    <asp:Label ID="Lbl_ReslNo" runat="server" CssClass="lblCSS" 
                         Text="Resl No"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="Txt_ReslNo" runat="server" CssClass="twitterStyleTextbox" 
                        TabIndex="1" Width="96%"></asp:TextBox>
                </td>
                <td></td>
                <td >
                
                     <asp:Label ID="Lbl_ReslDate" runat="server" CssClass="lblCSS" 
                          Text="Resl Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Txt_ReslDate" runat="server" CssClass="twitterStyleTextbox" 
                        TabIndex="4" Width="96%"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                        TargetControlID="Txt_ReslDate">
                    </asp:CalendarExtender>
                </td>
           <td> 
               &nbsp;</td>
            </tr>
            <tr>
            <td></td>
                <td >
                    <asp:Label ID="Lbl_IdNumber" runat="server" CssClass="lblCSS" 
                        Text="ID Number"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="Txt_IdNumber" runat="server" CssClass="twitterStyleTextbox" 
                        TabIndex="3" Width="96%"></asp:TextBox>
                </td>
                <td></td>
                <td >
                     <asp:Label ID="Lbl_Amsom" runat="server" CssClass="lblCSS"   Text="Amsom"></asp:Label>
                </td>
                <td >
               
                    <asp:DropDownList ID="CmbAmsom" runat="server" Width="100%">
                    </asp:DropDownList>
               
                </td>
                <td></td>
                <td >
                    <asp:Label ID="Lbl_Desham" runat="server"  CssClass="lblCSS"
                        Text="Desham"></asp:Label>
                </td>
                <td>
                     <asp:DropDownList ID="CmbDesam" runat="server" Width="100%"> </asp:DropDownList>
                </td>
            <td></td>
            </tr>            
            <tr>
                <td></td>
                <td>
                      <asp:Label ID="Lbl_ShareNo" runat="server"  CssClass="lblCSS" Text="No of Share" ></asp:Label>
                </td> 
                <td >
                <asp:TextBox ID="Txt_ShareNo" runat="server"   
                        Width="96%" CssClass="twitterStyleTextbox" AutoPostBack="True" 
                        TabIndex="2"></asp:TextBox>
                </td>
                <td></td>
                <td >
                    <asp:Label ID="Lbl_ShareAmt" runat="server" CssClass="lblCSS" Text="Share Amount"  ></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="Txt_ShareAmt" runat="server"  onkeypress="return isFloatKey(event);"
                         Width="96%" CssClass="twitterStyleTextbox" AutoPostBack="True"></asp:TextBox>
                </td>
                <td></td>
                <td >
                     <asp:Label ID="Lbl_NomNo" runat="server" CssClass="lblCSS"    Text="Nominee"></asp:Label>
                </td>
                <td>
                      <asp:DropDownList ID="CmbNomReq" runat="server" AutoPostBack="True" 
                          TabIndex="24" Width="100%">
                      </asp:DropDownList>
                </td>
                <td></td>
            </tr>
            </table>
        <table width = "100%">         
            <tr>
            <td width="2%">&nbsp;</td>
            <td width="15%">
                <asp:Label ID="Lbl_CashTransfer" runat="server" CssClass="lblCSS"  
                     Text="Cash/Transfer"></asp:Label>
                </td>
            <td width="15%">
                <asp:DropDownList ID="CmbCashTransfer" runat="server" AutoPostBack="True" 
                    Width="100%">
                </asp:DropDownList>
                </td>
            <td width="2%">&nbsp;</td>
            <td width="15%">
                <asp:Label ID="Lbl_TockenNo" runat="server" CssClass="lblCSS" 
                     Text="Tocken No"></asp:Label>
                </td>
            <td width="15%">
                <asp:TextBox ID="Txt_TockenNo" runat="server" CssClass="twitterStyleTextbox" 
                    Width="96%"></asp:TextBox>
                </td>
            <td width="2%">&nbsp;</td>
            <td width="15%"><label>Batch No</label></td>
            <td width="15%">
                <asp:DropDownList ID="CmbBatchNo" runat="server" AutoPostBack="True" 
                    TabIndex="1" Width="80%" Enabled="False">
                </asp:DropDownList>
                <asp:ImageButton ID="ImgBatch" runat="server"  
                    ImageUrl="~/Images/Node.png" ImageAlign="Middle" Enabled="False" 
                    OnClientClick="return imgBatchDtls_onclick();"/>
            </td>
            <td width="2%">&nbsp;</td>
            </tr>            
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td>
                    <asp:Label ID="LblCredit" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="LblDebit" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                </td>
                <td></td>
            </tr>
        </table>
     
    <div id ="NomDiv" runat="server" visible="false">
            <table width = "100%">
	            <tr>
		            <td width = "2%" > </td>
		            <td width = "15%"> <label>Nominee Name</label></td>
		            <td width = "49%"> 
                        <asp:TextBox  CssClass="twitterStyleTextbox" ID="Txt_NomName" runat="server"  Width="99%" TabIndex="30"></asp:TextBox></td>
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

   
     <table border="0" width="100%">
         <tr>
             <td width="2%"></td>
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
                 <td width="2%"></td>
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
    
     </asp:Panel>
     
    <asp:HiddenField ID="HdnShareValue" runat="Server" Value="" />
    <table width= "100%">
        <tr>
           
            <td width = "50%" align="right">
                <asp:ImageButton ID="CmdSave" runat="server" Height="32px" 
                    ImageUrl="~/images/save.png" Width="96px" style="margin-left: 312px" 
                    TabIndex="8" OnClientClick="return ValidateSave();"/> </td>           
            <td  width = "50%" align="left">                
                 <asp:ImageButton ID="CmdClose" runat="server" Height="32px" 
                    ImageUrl="~/images/Close.png" Width="95px" /> </td>
                    <asp:ImageButton ID="cmdDelete" runat="server" ImageUrl="~/images/DELETE.png" Visibl="false"
                meta:resourcekey="CmdSaveResource1" TabIndex="29" OnClientClick="return ValidateDELETE();"  />
        </tr>
    </table>
    </div>
    
</ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>


    
    
    
    
    
    
    
    
    




    
    
        
    
       
        
          
          
     
    
    
        
    
         
     
   