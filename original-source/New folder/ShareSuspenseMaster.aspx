<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="ShareSuspenseMaster.aspx.vb" Inherits="SMARTCORE_UI.ShareSuspenseMaster" 
    title="SHARE SUSPENSE MASTER" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
<script type='text/javascript' src="scripts/jquery_json.js"></script>
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


    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

    <div class="gradientBoxesWithOuterShadows" style="width: 98%; margin-left:1%; margin-right:1%;margin-top:1%;" >                          
    <legend>SHARE SUSPENSE MASTER</legend>                                                   
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
        <td>
            <label>
            Scheme</label></td>
        <td width="27%">
                <asp:DropDownList ID="CmbScheme" runat="server" Width="100%">
                </asp:DropDownList>
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
                    <asp:Label ID="Lbl_AddmissionDate" runat="server" Text="Admission Date" Font-Names="Tahoma" 
                        Font-Size="Medium" ForeColor="#6666FF"></asp:Label>
                    
                </td>
                <td >
                    <asp:TextBox ID="Txt_AdmissionDate" runat="server"   
                       Width="96%" CssClass="twitterStyleTextbox" ></asp:TextBox>

                </td>
                
                <td></td>
                <td >
                    <asp:Label ID="Lbl_ShareNo" runat="server" Font-Names="Tahoma" 
                        Font-Size="Medium" ForeColor="#6666FF" Text="No of Share"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="Txt_ShareNo" runat="server" AutoPostBack="True" 
                        CssClass="twitterStyleTextbox" TabIndex="2" Width="96%"></asp:TextBox>
                </td>
                <td></td>
                <td >
                
                     <asp:Label ID="Lbl_ShareAmt" runat="server" Font-Names="Tahoma" 
                         Font-Size="Medium" ForeColor="#6666FF" Text="Share Amount"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Txt_ShareAmt" runat="server" AutoPostBack="True" 
                        CssClass="twitterStyleTextbox" Width="96%"></asp:TextBox>
                </td>
           <td> 
               &nbsp;</td>
            </tr>
                        
            <tr>
                <td></td>
                <td >
                      <asp:Label ID="Lbl_CashTransfer" runat="server" Font-Names="Tahoma" 
                          Font-Size="Medium" ForeColor="#6666FF" Text="Cash/Transfer"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList ID="CmbCashTransfer" runat="server" AutoPostBack="True" 
                        Width="100%">
                    </asp:DropDownList>
                </td>
                <td></td>
                <td >
                    <asp:Label ID="Lbl_TockenNo" runat="server" Font-Names="Tahoma" 
                        Font-Size="Medium" ForeColor="#6666FF" Text="Tocken No"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="Txt_TockenNo" runat="server" CssClass="twitterStyleTextbox" 
                        Width="96%"></asp:TextBox>
                </td>
                <td></td>
                <td >
                     <asp:Label ID="Lbl_NomNo" runat="server" Font-Names="Tahoma" Font-Size="Medium" 
                         ForeColor="#6666FF" Text="Nominee"></asp:Label>
                </td>
                <td>
                      <asp:DropDownList ID="CmbNomReq" runat="server" AutoPostBack="True" 
                          TabIndex="24" Width="100%">
                      </asp:DropDownList>
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td><label>Batch No</label></td>
                <td>
                    <asp:DropDownList ID="CmbBatchNo" runat="server" AutoPostBack="True" 
                        TabIndex="1" Width="80%" Enabled="False">
                    </asp:DropDownList>
                    <asp:ImageButton ID="ImgBatch" runat="server"  
                        ImageUrl="~/Images/Node.png" ImageAlign=Middle Enabled="False" 
                        OnClientClick="return imgBatchDtls_onclick();"/>
                </td>
                <td></td>
                <td>
                    <asp:Label ID="LblCredit" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="LblDebit" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                </td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            </table>
        <table width = "100%">         
                        
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
        </tr>
    </table>
    </div>
    
</ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>


    
    
    
    
    
    
    
    
    




    
    
        
    
       
        
          
          
     
    
    
        
    
         