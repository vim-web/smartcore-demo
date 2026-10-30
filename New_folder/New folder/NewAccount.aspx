<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NewAccount.aspx.vb" Inherits="SMARTCORE_UI.NewAccOpen"  enableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

 
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Print Preview</title>
    <script src="Scripts/Print.js" type="text/javascript"></script> 
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />
   <script type='text/javascript' src="scripts/jquery_json.js"></script>  
<script type = "text/javascript" >
   function preventBack(){window.history.forward();}
    setTimeout("preventBack()", 0);
    window.onunload=function(){null};
</script>
    <script language="javascript" type="text/javascript">
// <!CDATA[
        window.location.hash = "no-back-button";
        window.location.hash = "Again-No-back-button"; //again because google chrome don't insert first hash into history
        window.onhashchange = function () { window.location.hash = "no-back-button"; }

        function FD_Print() {

            var DrpPrn = document.getElementById('<%=DrpPrinters.ClientID%>');
            var Prn = document.getElementById('<%=HdnFDValue.ClientID%>');
            var ClPrinter1 = DrpPrn.options[DrpPrn.selectedIndex].text;


            //               var DrpObj = document.getElementById('DrpPrinters');
            //               var SelPrn = DrpObj.options[DrpObj.selectedIndex].text;



            //DrpPrn.options.add('jai');
            // xyz();

            // ClPrinter= GetPrintersNew(DrpPrn);

            printNew(Prn.value, ClPrinter1);


        }








function Button1_onclick() {

    var DrpPrn = document.getElementById('<%=DrpPrinters.ClientID%>');
    var Prn = document.getElementById('<%=HiddenField1.ClientID%>');
    var ClPrinter1 = DrpPrn.options[DrpPrn.selectedIndex].text;


    //               var DrpObj = document.getElementById('DrpPrinters');
    //               var SelPrn = DrpObj.options[DrpObj.selectedIndex].text;



    //DrpPrn.options.add('jai');
    // xyz();

    // ClPrinter= GetPrintersNew(DrpPrn);

    printNew(Prn.value, ClPrinter1);


}

function Button2_onclick() {

}

function Button2_onclick() {

}



    </script>


</head>
<body onload="disableBackButton();">
    <form id="form1" runat="server">
    <div class="gradientBoxesWithOuterShadows" style="width: 80%; margin-left:10%; margin-right:10%; margin-top:10%; margin-bottom:10%" >
    <div id ="divacdtls" runat="server">
    <legend>NEW ACCOUNT DETAILS</legend><br />
<%--    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  EnablePageMethods="True" CombineScripts="True"> 
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
        </Scripts>
        </asp:ToolkitScriptManager>--%>

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
                <asp:TextBox ID="TxtNoOfOperator" runat="server" Width = "100%" TabIndex="999"></asp:TextBox>
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
                        <asp:CommandField DeleteText="&lt;img src='./images/del.jpg'&gt;" 
                            ShowDeleteButton="True">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:CommandField>
                    </Columns>
                    <AlternatingRowStyle BackColor="#F5F4FF" />
                </asp:GridView>
            
            </td>
            <td width="2%"></td>
        </tr>
    </table>
    </div>
    &nbsp;<table width = "100%">
    <tr >
        <td width = "25%" height = "20px"></td>
        <td width = "25%"></td>
        <td width = "25%"></td>
        <td width = "25%"></td>
    </tr>
    <tr>
        <td>
            <%--<input id="CmdBond" type="button" value="Bond" runat="server" 
                
                style="font-family: 'Courier New', Courier, monospace; font-size: medium; color: #FFFFFF; background-color: #0066FF; height: 26px; font-weight: bold;"   
                 onclick="FD_Print();" visible="False" />--%></td>
        <%--<td align="right"><label>New Account No : </label></td>--%>
        <td colspan="2" align="center"  >
            
            <asp:Label ID="LblAccNo" runat="server" Font-Bold="True" Font-Size="Larger"  Text="ssssssssssssss"
                ForeColor="#3333CC"></asp:Label>
                
        </td>
    </tr>
    <tr>
        <td height = "20px">
       <asp:HiddenField ID="HiddenField1" runat="server" />
    <%--<applet archive="dist/jzebra.jar" code="jzebra.PrintApplet.class" height="25" 
           name="jZebra" width="25">
           <param name="printer" value="zebra" />
<!-- <param name="sleep" value="200"> -->
       </applet>--%>
            
            
            </td>
        <td>
        
  <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Larger"  Text ="Select The Printer " Width ="100%" 
                ForeColor="#3333CC"  Visible="false" ></asp:Label>
            <asp:HiddenField ID="hdnPrintBond" runat="server" />
        </td>
        <td>
        <asp:DropDownList ID="DrpPrinters" runat="server"  Width ="100%"   Visible="false" >
                    </asp:DropDownList>
            <asp:HiddenField ID="hdnTransID" runat="server" />
        </td>
        <td>

<div id="DivMiltipleAccount" runat ="server"  visible ="false" >
<%--    <asp:CheckBoxList ID="ChkAccNo" runat="server" AutoPostBack="True" 
        onselectedindexchanged="ChkAccNo_SelectedIndexChanged">
    </asp:CheckBoxList>--%>
    <asp:RadioButtonList ID="ChkAccNo" runat="server" AutoPostBack="True" >
    </asp:RadioButtonList>
    
    
    
</div>



</td>
    </tr>
    <tr>
        <td>
            <asp:HiddenField ID="hdnSecName" runat="server" />
        </td>
        <td align="right">
            &nbsp;</td>
        <td style="margin-left: 120px">
<%--            <input id="Button1" type="button" value="Print" 
            runat="server" 
                style="font-family: 'Courier New', Courier, monospace; font-size: medium; color: #FFFFFF; background-color: #0066FF; height: 26px; font-weight: bold;"   
                 onclick="return Button1_onclick()" />--%>

                <asp:Button ID="ButtonPrint"  BackColor="#0066FF" Font-Bold="True" 
                ForeColor="White" Text="Print" style="margin-left: 0px" Width="73px" 
                Font-Names="Courier New" runat="server"/>
                 
                 <asp:Button ID="CmdClose" 
                runat="server" BackColor="#0066FF" Font-Bold="True" 
                ForeColor="White" Height="26px" Text="Close" Width="74px" 
                Font-Names="Courier New" />
                
                <asp:Button ID="CmdBond0"  BackColor="#0066FF" Font-Bold="True" 
                ForeColor="White" Text="Bond" style="margin-left: 0px" Width="73px" 
                Font-Names="Courier New" visible="false" runat="server"/>
        </td>
        <td>
                
                &nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:HiddenField ID="hdnSchCode" runat="server" />
        </td>
        <td>
            <asp:HiddenField ID="hdnDRCR" runat="server" />
            <asp:HiddenField ID="hdnTRANCATG" runat="server" />
        </td>
        <td>
            <asp:Button ID="CmdPrint" runat="server" BackColor="#6666FF" Font-Bold="True" 
                ForeColor="White" Text="Voucher" Visible="False" />
            <asp:HiddenField ID="hdnSection" runat="server" />
  
            </td>
        <td>
       <asp:HiddenField ID="HdnFDValue" runat="server" />
        </td>
    </tr>
</table>
</div>
    
    
    
    
    
    

    </form>
</body>