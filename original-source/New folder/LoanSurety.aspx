<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="LoanSurety.aspx.vb" Inherits="SMARTCORE_UI.LoanSurety" 
    title="Untitled Page" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
    <link href="CSS/ui-lightness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript' src="Scripts/Common.js"></script>
   <script src="Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script type='text/javascript' src="Scripts/jquery-ui-1.9.2.custom.min.js"></script>     
    <script type="text/javascript">
    
      $(function() {
        });
        function imgLiab_onclick() {        
        
            $('.gradientBoxesWithOuterShadows').append('<div id="resultdiv" title="Customer Asset Liablities" visible="false" >');
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
        function ValidateCheckLoanAppNo()
        {
            var Ctl = document.getElementById('<%=TxtLoanNo.ClientID%>');
            if (Ctl.value == "") {
                alertify.error('Please Enter Loan Number');
                Ctl.focus();
                return false;
            }

        }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                        <Scripts>
                            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
                        </Scripts>
        </asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 70%; margin-left:15%; margin-right:15%;">
        <legend>Loan Surety Details</legend>
        <table width="100%">
        <tr>
        <td width="2%"></td>
        <td width="48%"><label>Branch</label></td>
        <td width="48%">
    
        <asp:DropDownList ID="CmbBranch" runat="server" AutoPostBack="True" 
        TabIndex="4" Width="100%">
    </asp:DropDownList>
            </td>
          <td width="2%"></td>
        </tr>
         <tr>
        <td width="2%"></td>
        <td width="48%"><label>Scheme</label></td>
        <td width="48%">
             <asp:DropDownList ID="CmbScheme" runat="server" AutoPostBack="True" 
                 TabIndex="5" Width="100%">
                <%-- <asp:ListItem Value="94">NAST</asp:ListItem>--%>
             </asp:DropDownList>
             </td>
          <td width="2%"></td>
        </tr>
         <tr>
        <td width="2%"></td>
        <td width="48%"><label>Loan No</label></td>
        <td width="48%">
    <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtLoanNo" runat="server" 
        AutoPostBack="True" TabIndex="1" 
        Width="100%"></asp:TextBox>
             </td>
          <td width="2%"></td>
        </tr>
         <tr>
        <td width="2%">&nbsp;</td>
        <td width="48%"><label>Member No</label></td>
        <td width="48%">
             <asp:TextBox ID="TxtMemberno" runat="server" AutoPostBack="True" 
                 CssClass="twitterStyleTextbox" Enabled="False" TabIndex="8" Width="100%"></asp:TextBox>
             </td>
          <td width="2%">&nbsp;</td>
        </tr>
            <tr>
                <td width="2%">
                </td>
                <td width="48%">
                    <label>
                    Name</label></td>
                <td width="48%">
                    <asp:TextBox ID="TxtName" runat="server" CssClass="twitterStyleTextbox" 
                        Enabled="False" ReadOnly="True" Width="100%"></asp:TextBox>
                </td>
                <td width="2%">
                </td>
            </tr>
         <tr>
        <td width="2%"></td>
        <td width="48%"><label>Address</label></td>
        <td width="48%">
             <asp:TextBox ID="TxtAddress" runat="server" CssClass="twitterStyleTextbox" 
                 Enabled="False" ReadOnly="True" TextMode="MultiLine" Width="100%"></asp:TextBox>
             </td>
          <td width="2%"></td>
        </tr>
         <tr>
        <td width="2%"></td>
        <td width="48%">
            <asp:HiddenField ID="SurMemAccid" runat="server" />
            <asp:HiddenField ID="HdnAccid" runat="server" />
             </td>
        <td width="48%">
            <asp:HiddenField ID="HdnCustID" runat="server" />
             </td>
          <td width="2%"></td>
        </tr>
         <tr>
        <td width="2%"></td>
        <td width="48%" colspan="2" style="width: 96%">
        <%--<div id="Surety" runat="server" visible="false">--%>
            <table width="100%">
                <tr>
                    <td width="2%">
                    </td>
                    <td width="40%">
                        <asp:HiddenField ID="HdnSurCustID" runat="server" />
                    </td>
                    <td width="56%">
                        <asp:TextBox ID="TxtSurAccID" runat="server" AutoPostBack="True" 
                            CssClass="twitterStyleTextbox" TabIndex="13" Visible="false" Width="95%"></asp:TextBox>
                    </td>
                    <td width="2%">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
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
                                                <asp:DropDownList ID="CmbSurMemType" runat="server" AutoPostBack="True" 
                                                    TabIndex="12" Width="100%">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <table width="100%">
                                                    <tr>
                                                        <td width="85%">
                                                            <asp:TextBox ID="TxtSurMemNo" runat="server" AutoPostBack="True" 
                                                                CssClass="twitterStyleTextbox" TabIndex="13" Width="95%"></asp:TextBox>
                                                        </td>
                                                        <td width="15%">
                                                            <asp:Button ID="Button2" runat="server" 
                                                                onclientclick="return imgLiab_onclick();" Text="A/L" />
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
                        <asp:ImageButton ID="ImgBtnAddLand" runat="server" 
                            ImageUrl="~/Images/AddBtn.png" />
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
                                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                        BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="15%" />
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
                                        BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="8%" />
                                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                        Font-Size="Small" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CustName" DataFormatString="{0:F2}" 
                                    HeaderText="Name">
                                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                        BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="17%" />
                                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                        Font-Size="Small" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Relation" HeaderText="Relation">
                                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                        BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="20%" />
                                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                        Font-Size="Small" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CustAdd" HeaderText="Address">
                                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                        BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="30%" />
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
                                <asp:BoundField DataField="Dob" DataFormatString="{0:dd/MM/yyyy}" 
                                    HeaderText="Dob">
                                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                        BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="6%" />
                                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                        Font-Size="Small" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Age" HeaderText="Age">
                                    <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                        BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="4%" />
                                    <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                        Font-Size="Small" />
                                </asp:BoundField>
                                <asp:CommandField DeleteImageUrl="~/Images/DeleteN.png" 
                                    DeleteText="&lt;img src='./images/del.jpg'&gt;" 
                                    EditImageUrl="~/Images/DeleteN.png" NewImageUrl="~/Images/DeleteN.png" 
                                    SelectImageUrl="~/Images/DeleteN.png" ShowDeleteButton="True">
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
                   
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
          <%--  </div> --%>
             </td>
          <td width="2%"></td>
        </tr>
        <table width="100%">
<tr>
<td width="2%"></td>
<td width="96%" align="center">
    <asp:ImageButton ID="BtnSave" runat="server" ImageUrl="~/Images/Save.png" 
        OnClientClick="return  ValidateCheckLoanAppNo()" />
    <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" />
    <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" 
        PostBackUrl="~/Main.aspx" />
    </td>
<td width="2%"></td>
</tr>
</table>

        </table>
        
</div>

</ContentTemplate>
</asp:UpdatePanel> 


  
</asp:Content>


