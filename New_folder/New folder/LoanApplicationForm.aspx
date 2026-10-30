<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LoanApplicationForm.aspx.vb" Inherits="SMARTCORE_UI.LoanApplicationForm" MasterPageFile="~/SMARTCORE.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <LINK REL=Stylesheet HREF= "CSS/PageStyle.css" TYPE="text/css" MEDIA=screen>    
    
        <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
    <link href="CSS/ui-lightness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript' src="Scripts/Common.js"></script>
    <script type='text/javascript' src="Scripts/jquery-1.8.3.js"></script>  
    <script type='text/javascript' src="Scripts/jquery-ui-1.9.2.custom.min.js"></script>  
    <asp:UpdatePanel ID="UpdatePanel1" runat=   "server">

<ContentTemplate>

<script type='text/javascript'>

    function ValidateSave() {

        var Ctl = document.getElementById('<%=TxtFormFee.ClientID%>');
            if (Ctl.value == "") {
                alertify.error('Form Fee Not Set, Please Check');
                Ctl.focus();
                return false;
            }



        return true;
    }

</script>


  <script type="text/javascript">
      function imgLiab_onclick() {

          $('.gradientBoxesWithOuterShadows').append('<div id="resultdiv" title="Customer Asset Liablities" visible="false" >');
          $("#resultdiv").load("AssetLiab.aspx?CustID=" + document.getElementById('<%=SurMemAccid.ClientID%>').value, {}, function(data) {
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
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    

        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 70%; margin-left:10%; margin-right:10%;">
     <legend>Loan Application Form</legend>
    <table width="100%">
<tr>
<td width="2%"></td>
<td width="38%">
<label visible ="false"  runat ="server"  >Form No</label></td>
<td width="48%">
    <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtFormNo" Width="100%" runat="server" Visible="False"></asp:TextBox>
    </td>
<td width="12%"></td>
</tr>

<tr>
<td>&nbsp;</td>
<td align="left">
    <label>Branch</label></td>
<td>
    <table width="100%">
    <tr>
    <td width="30%">
    <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtBranch" Width="95%" runat="server" AutoPostBack="True"></asp:TextBox>
    </td>
    <td width="70%">
    <asp:DropDownList ID="CmbBranch" Width="100%" runat="server" AutoPostBack="True">
    </asp:DropDownList>
    </td>
    </tr>
    </table>
    </td>
    <td>
   

        <asp:HiddenField ID="HdnAccid" runat="server" />
   

    </td>
</tr>
<tr>
<td>&nbsp;</td>
<td align="left">
    <label>Customer ID</label></td>
<td>
<%--    <table width="100%">
    <tr>
    <td width="30%">
        &nbsp;</td>
    <td width="70%">
   <table width ="100%">
   <tr>
   <td>
       &nbsp;</td>
   <td>
   <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtMemNo" runat="server" 
                    Width="100%" AutoPostBack="True"></asp:TextBox>
   </td>
   </tr>
   
   </table>
        
        </td>
    </tr>
    </table>--%>
    <asp:TextBox ID="TxtCustId" runat="server" Width="90%" AutoPostBack="True"></asp:TextBox>
    <asp:ImageButton ID="ImgSrch" runat="server" Height="20px" 
        ImageUrl="~/images/Search.png" Width="23px" />
    </td>
<td>
    <table width="100%">
        <tr>
            <td width="15%">
                <asp:Button ID="Button2" runat="server"  Visible ="false"
                    onclientclick="return imgLiab_onclick();" Text="A/L" />
            </td>
            <td width="85%">
            </td>
        </tr>
    </table>
    </td>
</tr>
<tr>
<td>&nbsp;</td>
<td>
    <label>
                Name </label>
                

    </td>
<td>
    <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtName" Width="100%" runat="server"></asp:TextBox>
   </td>
<td>
<asp:HiddenField ID="HddAccCode" runat="server" />
</td>
</tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
      <label>
                Address </label>
                </td>
            <td>
            
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtAddress" Width="100%" runat="server" TextMode="MultiLine"></asp:TextBox>
                </td>
            <td>
        
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <label>
                Issue Date</label></td>
            <td>
                <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtIssueDate" runat="server" Width="100%" Enabled="False" 
                    ReadOnly="True"></asp:TextBox>
                <asp:CalendarExtender ID="TxtIssueDate_CalendarExtender" runat="server" 
                    TargetControlID="TxtIssueDate">
                </asp:CalendarExtender>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Form Fee</label></td>
            <td>
                <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtFormFee" runat="server" onkeypress="return isFloatKey(event);" 
                    Width="100%" AutoPostBack="True"  ></asp:TextBox>
            </td>
            <td>
                <asp:HiddenField ID="SurMemAccid" runat="server" />
            </td>
        </tr>
<tr>
<td>&nbsp;</td>
<td>
    <label>Remarks</label></td>
<td>
    <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtRemarks" Width="100%" runat="server"></asp:TextBox>
    </td>
<td>&nbsp;</td>
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
        </tr>
        
        
           <tr>
            <td colspan="4">
            <%--<asp:GridView ID="DgvTransaction" runat="server" AutoGenerateColumns="False" 
                TabIndex="32" Width="100%">
                <Columns>
                    <asp:BoundField DataField="AccNo" HeaderText="AccNo" Visible="False">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AccCode" HeaderText="AccCode" Visible="False" />
                    <asp:BoundField DataField="AccHead" HeaderText="Particulars">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="30%" 
                            Font-Names="Times New Roman" HorizontalAlign="Left" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" ForeColor="#333399" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RctCash" HeaderText="Receipt Cash" 
                        DataFormatString="{0:F2}">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="15%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" HorizontalAlign="Right" ForeColor="#333399" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RctTransfer" HeaderText="Receipt Transfer" 
                        DataFormatString="{0:F2}">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" HorizontalAlign="Right" ForeColor="#333399" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PayCash" HeaderText="Payment Cash" 
                        DataFormatString="{0:F2}">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" HorizontalAlign="Right" ForeColor="#333399" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PayTransfer" HeaderText="Payment Transfer" 
                        DataFormatString="{0:F2}">
                        <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                        <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="Medium" HorizontalAlign="Right" ForeColor="#333399" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Seq_No" HeaderText="Seq_No" Visible="False" />
                </Columns>
                <AlternatingRowStyle BackColor="#F5F7FE" />
            </asp:GridView>--%>
            
            
              </td>
        </tr>
        
           <tr>
            <td colspan="4">

           <table width="100%" border="0">
            <tr>                  
                <td width="40%" scope="col"  align="right">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#0033CC" 
                        Text="TOTAL" Visible="False"></asp:Label>
                </td>
                <td width="20%" scope="col"  align="right">
                    <asp:Label ID="LblRctCashTotal" runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>
                </td>
                <td width="14%" scope="col"  align="right">
                    <asp:Label ID="LblRctTransferTotal" runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>
                </td>
                <td width="14%" scope="col"  align="right">
                    <asp:Label ID="LblPayCashTotal" runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>
                </td>
                <td width="12%" scope="col"  align="right">
                    <asp:Label ID="LblPayTransferTotal" runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>
                </td>
            </tr>
        </table>
            
              </td>
        </tr>
        <tr>
            <td>
                </td>
            <td>
                 &nbsp;</td></td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
<tr>
<td>&nbsp;</td>
<td align="center" colspan="2">
    <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" 
        style="height: 40px" onclientclick="return ValidateSave();" />
    &nbsp;<asp:ImageButton ID="CmdClear" runat="server" 
        ImageUrl="~/Images/Clear.png" />
    &nbsp;<asp:ImageButton ID="CmdClose" runat="server" 
        ImageUrl="~/Images/Close.png" PostBackUrl="~/Main.aspx" />
    </td>
<td>&nbsp;</td>
</tr>
     
</table>
    </div>
    </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>