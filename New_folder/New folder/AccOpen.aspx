<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="AccOpen.aspx.vb" Inherits="SMARTCORE_UI.AccOpen" 
    title="Account Open" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />
     <script type='text/javascript' src="Scripts/Common.js"></script>  
     <script type="text/javascript" src="Scripts/jquery_json.js"></script>
     
    <script type="text/javascript">
        $(function() {
            var Ctl = document.getElementById('<%=TxtSchCode.ClientID%>');
            Ctl.focus();
        });
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

<script type="text/javascript">
    function onlyAlphabets(event) {
        var Key = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
        if (Key == 13 || (Key >= 48 && Key <= 57)) {
            alertify.error("Numbers Not Allowed");
            return false;
        }
        else return true;
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
    
     <style type="text/css">
         .auto-style1 {
             width: 20%;
         }
         .style1
         {
             width: 100%;
         }
     </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePageMethods="True" CombineScripts="True">
    <Scripts>
            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
    </Scripts>
</asp:ToolkitScriptManager>


<div class="gradientBoxesWithOuterShadows" style="width: 90%; margin-left:5%; margin-right:5%; margin-top:2%; font-family: Tahoma" > 
    <legend>ACCOUNT OPEN</legend>
    <div style="width:100%; float:left;"  >            
        <table width="100%" >
      <tr>
        <td width="15%">&nbsp;</td>
        <td width="30%">&nbsp;</td>
        <td width="5%">&nbsp;</td>
        <td width="18%">&nbsp;</td>
        <td width="32%">&nbsp;</td>
      </tr>
        <tr>
        <td><label>Branch</label></td>
        <td>
            <asp:DropDownList ID="CmbBranch" runat="server" Width="100%">
            </asp:DropDownList>
        </td>
        <td></td>                            
        <td><label>Mode of Operation</label></td>
        <td>
            <asp:DropDownList ID="CmbOpMode" runat="server" Width="100%" 
                AutoPostBack="True" TabIndex="1">
            </asp:DropDownList>
        </td>
      </tr>
            <tr>
        <td><label>Section </label></td>
        <td>
        
            <asp:DropDownList ID="Ddl_Section" runat="server" AutoPostBack="True" 
                TabIndex="1" Width="100%">
            </asp:DropDownList>
        
        </td>
        <td></td>
        <td>
            <asp:Label ID="LblNoofOperator" CssClass="lblCSS" runat="server" Text="No of Operators" 
                ></asp:Label>
        <td>
            <asp:TextBox ID="TxtNoOfOperator" runat="server" Width = "96%"  
                onkeypress="return isFloatKey(event);" TabIndex="3" 
                CssClass="twitterStyleTextbox" MaxLength="3"></asp:TextBox>
        </td>
        </tr>
      <tr>
     
        <td colspan="2">
        <div id="DivLoanType" runat="server" visible=false>
            <table class="style1" width="100%">
                <tr>
                    <td width="33%">
                        <label>Loan Type</label></td>
                    <td width="67%">
                        <asp:DropDownList ID="DrpLoanType" Width="100%" runat="server" 
                            AutoPostBack="True">
            </asp:DropDownList></td>
                </tr>
            </table>
            </div>
          </td>

<%--<label>Loan Type</label>--%>
<%--  <asp:DropDownList ID="DrpLoanType" Width="100%" runat="server">
            </asp:DropDownList>--%>
        <td></td>
        <td><label>Customer / Member</label></td>
        <td>
            <asp:DropDownList ID="CmbCustMem" runat="server" Width="100%" 
                AutoPostBack="True" TabIndex="5" class="select">
            </asp:DropDownList>
        </td>
      </tr>
            <tr>
                <td>
                    <label>Scheme </label></td>
                <td>
                    <table width = "100%">
                <tr>
                    <td width ="18%">
                        <asp:TextBox ID="TxtSchCode" runat="server" Width = "100%" TabIndex="3" 
                            CssClass="twitterStyleTextbox" AutoPostBack="True"></asp:TextBox>
                    </td>         
                    <td width ="2%"></td>           
                    <td width ="80%">
                        <asp:DropDownList ID="CmbScheme" runat="server" Width="100%" 
                            AutoPostBack="True" TabIndex="1">
                        </asp:DropDownList>                    
                    </td>
                </tr>
            </table></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
      <tr>
        <td><label>Privelage</label></td>
        <td>
            <asp:DropDownList ID="CmbPrivelage" runat="server" TabIndex="5" Width="100%">
            </asp:DropDownList>
        </td>
        <td></td>
        <td></td>
        <td>
             
        </td>

      </tr>
    </table>
<div id="DivLoanApp" runat="server" visible="true" >
        <table width="100%">
          <tr>
            
            <td width="15%">
                <asp:Label ID="Label1" runat="server" Text="Loan Application Type" 
                   CssClass="lblCSS" ></asp:Label>
            <td width="30%">
                <asp:DropDownList ID="CmbLoanAppType" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
            
            <td width="5%"></td>
            <td width="18%">
                <asp:Label ID="Label2" runat="server"  CssClass="lblCSS"
                    Text="Loan Application Number"></asp:Label>
                <td width="32%">
                    <asp:TextBox ID="TxtApplnNo" runat="server" AutoPostBack="true" placeholder="Application NO"
                        CssClass="twitterStyleTextbox" Style="text-transform: uppercase" TabIndex="10" 
                        Width="100%"></asp:TextBox>
                </td>
            
          </tr>
        </table>
    </div>
    
    <div id="MemberIDDiv" runat="server" visible="false" >
        <table width="100%">
          <tr>
            <td width="15%">
                <asp:Label ID="LblMember" runat="server" Text="Member Branch &amp; Scheme" 
                   CssClass="lblCSS" ></asp:Label>
            <td width="15%">
                <asp:DropDownList ID="CmbMemBranch" runat="server" Width="100%" TabIndex="6" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td width="15%">
                <asp:DropDownList ID="CmbMemType" runat="server" Width="100%" TabIndex="7" 
                    AutoPostBack="True">
                </asp:DropDownList>
              </td>
            <td width="5%">&nbsp;</td>
            <td width="18%">
                <asp:Label ID="LblMemNo" runat="server" Text="Member No" CssClass="lblCSS" ></asp:Label>
            <td width="32%">
                <asp:TextBox ID="TxtMemberNo" runat="server" Width="80%" AutoPostBack="True" 
                    TabIndex="8" CssClass="twitterStyleTextbox" MaxLength="18"></asp:TextBox>
                <asp:ImageButton ID="ImgMemSearch" runat="server" Height="24px" 
                    ImageUrl="~/images/Search.png" Width="23px" />
              </td>
            </tr>
        </table>
    </div>
    
        
    
    
    <div id="CustomerIDDiv" runat="server">
        <table width="100%">
          <tr> 
            <td width="15%"><label>Customer ID</label></td>
            <td  width="30%">
                <asp:TextBox ID="TxtCustID" runat="server" Width = "80%" 
                    AutoPostBack="True" TabIndex="9" CssClass="twitterStyleTextbox"></asp:TextBox>
                <asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
                    ImageUrl="~/images/Search.png" Width="23px" />
              </td>
            <td width="5%">&nbsp;</td>
            <td width="18%">
                <label>
                Customer Type</label>&nbsp;</td>
            <td width="32%">
                <asp:DropDownList ID="CmbCustomerType" runat="server" TabIndex="17" 
                    Width="100%">
                </asp:DropDownList>
              </td>
          </tr>
        </table>
    </div>

<table width="100%">
  <tr>
    <td width="15%" >
        <label>
        Customer Name</label></td>
    <td  width="30%">
        <asp:TextBox ID="TxtCustName" runat="server" TabIndex="10" Width="97%" Style="text-transform: uppercase" CssClass="twitterStyleTextbox"  MaxLength="200"></asp:TextBox>
    </td>
    <td  width="5%"></td>
    <td  width="18%">
        <label>
        Customer Category</label></td>
    <td width="32%" >
        <asp:DropDownList ID="CmbCustomerCatg" runat="server" TabIndex="18" 
            Width="100%">
        </asp:DropDownList>
    </td>
  </tr>
  <tr>
    <td>
        <label>
        Relative Name</label></td>
    <td>
       
                    <asp:DropDownList ID="CmbRelType" runat="server" 
                        meta:resourcekey="CmbRelTypeResource1" TabIndex="11" Width="15%">
                    </asp:DropDownList>
                               
                
                    <asp:TextBox ID="TxtRelName" runat="server" onkeypress="return onlyAlphabets(event);" 
                        meta:resourcekey="TxtRelNameResource1" TabIndex="12" Width="78%" Style=" margin-left:3%; text-transform: uppercase" CssClass="twitterStyleTextbox"></asp:TextBox>
               
    </td>
    <td></td>
    <td><label>Date of Birth</label></td>
    <td>
        <table width="100%">
            <tr>
                <td width ="60%">
                    <asp:TextBox ID="TxtDob" placeholder="DD/MM/YYYY" runat="server" meta:resourcekey="TxtDobResource1" 
                        TabIndex="19" Width="95%"  onblur="CalculateAge(this)" CssClass="twitterStyleTextbox"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                        TargetControlID="TxtDob">
                    </asp:CalendarExtender>
                </td>
                <td width ="20%"><label>Age</label></td>
                <td width ="20%">
                    <asp:TextBox ID="TxtAge" runat="server" meta:resourcekey="TxtAgeResource1" onkeypress="return onlyNumbers(event)"
                        onblur="return CheckMinor();" TabIndex="20" Width="89%" CssClass="twitterStyleTextbox" ></asp:TextBox>
                </td>
            </tr>
        </table>
    </td>
  </tr>
  <tr>
    <td>
        <label>
        House Name</label></td>
    <td>
        <asp:TextBox ID="TxtPresHouseName" runat="server" TabIndex="13" Width="97%" Style="text-transform: uppercase" CssClass="twitterStyleTextbox" onkeypress="return textboxMultilineMaxNumber(this,100);"></asp:TextBox>
      </td>
    <td></td>
    <td>
        <label>
        Country</label></td>
    <td>
        <asp:TextBox ID="TxtPresCountry" runat="server" TabIndex="21" Width="97%" Style="text-transform: uppercase" CssClass="twitterStyleTextbox" onkeypress="return textboxMultilineMaxNumber(this,50);"></asp:TextBox>
      </td>
  </tr>
  <tr>
    <td>
        <label>
        Place</label></td>
    <td> 
        <asp:TextBox ID="TxtPresPlace" runat="server" TabIndex="14" Width="97%" Style="text-transform: uppercase" CssClass="twitterStyleTextbox" onkeypress="return textboxMultilineMaxNumber(this,50);"></asp:TextBox>
      </td>
    <td></td>
    <td>
        <label>
        Pin Code</label></td>
    <td valign=middle>
        <asp:TextBox ID="TxtPresPin" runat="server" TabIndex="22" Width="97%" Style="text-transform: uppercase" CssClass="twitterStyleTextbox" onkeypress="return textboxMultilineMaxNumber(this,10);"></asp:TextBox>
      </td>
  </tr>
  <tr>
    <td >
        <label>
        District</label></td>
    <td >
        <asp:TextBox ID="TxtPresDist" runat="server" TabIndex="15" Width="97%" Style="text-transform: uppercase" CssClass="twitterStyleTextbox" onkeypress="return textboxMultilineMaxNumber(this,50);"></asp:TextBox>
      </td>
    <td>&nbsp;</td>
    <td >
        <label>
        Mobile No</label></td>
    <td >
        <asp:TextBox ID="TxtMblNo" runat="server" TabIndex="23" Width="97%" Style="text-transform: uppercase" CssClass="twitterStyleTextbox" onkeypress="return textboxMultilineMaxNumber(this,15);"></asp:TextBox>
      </td>
  </tr>
    <tr>
        <td >
            <label>
            State</label></td>
        <td>
            <asp:TextBox ID="TxtPresState" runat="server" TabIndex="16" Width="97%" Style="text-transform: uppercase" CssClass="twitterStyleTextbox" onkeypress="return textboxMultilineMaxNumber(this,50);"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
        <td >
            <label>
            Land Phone</label></td>
        <td >
            <table width="100%">
                <tr>
                    <td width="40%">
                        <asp:TextBox ID="TxtLandPhone" runat="server" TabIndex="24" Width="97%" Style="text-transform: uppercase" CssClass="twitterStyleTextbox" onkeypress="return textboxMultilineMaxNumber(this,15);" ></asp:TextBox>
                    </td>
                    
                    <td width="20%">
                        <asp:ImageButton ID="CmdAdd" runat="server" Height="21px" ImageUrl="~/Images/AddBtn.png" TabIndex="25" />
                    </td>
                    <td width="39%">
                        <asp:ImageButton ID="Img_Edit" runat="server" ImageUrl="~/Images/Add.png" />
                        <asp:ImageButton ID="CmdCustClear" runat="server" Height="21px" ImageUrl="~/Images/ClearBtn.png" TabIndex="26" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>    
   
</table>
        <asp:GridView ID="DgvCustomer" runat="server" AutoGenerateColumns="False" 
            CssClass="grid-viewNew" DataKeyNames="CustID" TabIndex="30" Width="100%">
            <Columns>
                <asp:BoundField DataField="MemDtls" HeaderText="Member Details" />
                <asp:BoundField DataField="MemBrCode" HeaderText="Member BrCode" 
                    Visible="False" />
                <asp:BoundField DataField="MemSchCode" HeaderText="MemSchCode" 
                    Visible="False" />
                <asp:BoundField DataField="MemNo" HeaderText="Member No" Visible="False" />
                <asp:BoundField DataField="CustID" HeaderText="Customer ID" />
                <asp:BoundField DataField="CustName" DataFormatString="{0:F2}" 
                    HeaderText="Name" />
                <asp:BoundField DataField="Relation" HeaderText="Relation" />
                <asp:BoundField DataField="CustAdd" HeaderText="Address" />
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
                <asp:BoundField DataField="Dob" HeaderText="Dob" />
                <asp:BoundField DataField="Age" HeaderText="Age" />
                <asp:CommandField DeleteText="&lt;img src='./images/del.jpg'&gt;" 
                    ShowDeleteButton="True">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
            </Columns>
            <AlternatingRowStyle BackColor="#F5F4FF" />
        </asp:GridView>
<table width="100%">
  <tr>
    <td>
        &nbsp;</td>
  </tr>
</table>    
<table width="100%">
<tr>
    <td width = "35%"></td>
    <td width = "30%" align=center>
        <asp:ImageButton ID="ImgNext" runat="server" Height="21px" 
        ImageUrl="~/Images/NextBtn.png" Width="84px" TabIndex="28" />     
&nbsp;&nbsp;
        <asp:ImageButton ID="ImgNext0" runat="server" Height="21px" 
            ImageUrl="~/Images/CloseBtn.png" Width="84px" TabIndex="29" />

    </td>
    <td width = "35%"></td>
</tr>
        
</table>
        </div>   
</div>

</ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>
