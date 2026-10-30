<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="LoanApproval.aspx.vb" Inherits="SMARTCORE_UI.LoanApproval" 
    title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    
        <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
    <link href="CSS/ui-lightness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript' src="Scripts/Common.js"></script>
    <script type='text/javascript' src="Scripts/jquery-1.8.3.js"></script>  
    <script type='text/javascript' src="Scripts/jquery-ui-1.9.2.custom.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<script type='text/javascript'>

    function ValidateSave() {      



        var Ctl = document.getElementById('<%=CmbLoanStatus.ClientID%>');
        var LoanStatus = Ctl.options[Ctl.selectedIndex].text.toUpperCase();

        if (LoanStatus == 'APPROVED') {
            var Ctl = document.getElementById('<%=TxtResNo.ClientID%>');
            if (Ctl.value == "") {
                alertify.error('Res. No Not Entered, Please Check');
                Ctl.focus();
                return false;
            }

            
            var Ctl = document.getElementById('<%=TxtResDate.ClientID%>');
            if (Ctl.value == "") {
                alertify.error('Res. Date Not Entered, Please Check');
                Ctl.focus();
                return false;
            }
            var Ctl = document.getElementById('<%=TxtLoanSanAmount.ClientID%>');
            if (Ctl.value == "") {
                alertify.error('Sanction Amount Set, Please Check');
                Ctl.focus();
                return false;
            }   
            
        }
        else {
            var Ctl = document.getElementById('<%=TxtReason.ClientID%>');
            if (Ctl.value == "") {
                alertify.error('Reason Not Entered, Please Check');
                Ctl.focus();
                return false;
            }
        }



        return true;
    }

</script>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    

        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 70%; margin-left:10%;">
          <legend>LOAN APPROVAL / REJECTION  FORM</legend>
         
 <table width="100%">
<tr>
<td width="2%"></td>
<td width="40%">
<label>Loan Applciation Type</label></td>
<td width="56%">
<%--    <table width="100%">
    <tr>
    <td width="30%"> 
         </td>
    <td width="70%">
    
    </td>
    </tr>
    </table> --%>
    
        <asp:DropDownList ID="CmbLoanAppType" Width="100%" runat="server" 
        AutoPostBack="True">    

    </asp:DropDownList>
</td>
<td width="2%"></td>
</tr>

<tr>
<td> </td>
<td align="left">
    <label>Branch</label></td>
<td>
 <%--   <table width="100%">
    <tr>
    <td width="30%">
         </td>
    <td width="70%">

    </td>
    </tr>
    </table>--%>
    
        <asp:DropDownList ID="CmbBranch" Width="100%" runat="server" AutoPostBack="True" 
            TabIndex="4">
    </asp:DropDownList>
    </td>
<td> </td>
</tr>

<tr>
<td></td>
<td>
<label>Application No</label></td>
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
 <%--   <table width="100%">
    <tr>
    <td width="30%">
         </td>
    <td width="70%">
    
    </td>
    </tr>
    </table>--%>
    <asp:DropDownList ID="CmbScheme" Width="100%" runat="server" AutoPostBack="True" 
            TabIndex="5">
    </asp:DropDownList>
    
    </td>
<td> </td>
</tr>
<tr>
<td> </td>
<td align="left">
    <label>Member Number</label></td>
<td>
<asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtMemberno" Width="100%" 
        runat="server" ReadOnly="True" Enabled="False"></asp:TextBox>

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
    <asp:HiddenField ID="HdnCustID" runat="server" Visible="False" />
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
     
    <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtAmsam" runat="server" Width="100%"></asp:TextBox>
     
    </td>
<td> </td>
</tr>
<tr>
<td> </td>
<td>
    <label>Desam</label></td>
<td>
    <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtDesam" runat="server" Width="100%"></asp:TextBox>
    </td>
<td> </td>
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

<%--<tr>
<td> </td>
<td>
     </td>
<td>
     </td>
<td> </td>
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
   <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtSurMemNo" runat="server" 
                    Width="100%" AutoPostBack="True" TabIndex="13"></asp:TextBox>
   </td>
   </tr>
   
   </table>
        
        </td>
    </tr>
    </table>
    </td>
<td>
    <asp:HiddenField ID="HdnSurCustID" runat="server" />
</td>
</tr>
     <tr>
         <td>
              </td>
         <td align="left">
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
         <label>Surety Details</label>
              <asp:HiddenField ID="HdnSurMemNo" runat="server" Visible="False" />
              </td>
         <td>
         
         <asp:GridView ID="DgvCustomer" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="CustID" TabIndex="17" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="MemDtls" HeaderText="Member Details">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
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
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CustID" HeaderText="Customer ID">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" Width="8%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CustName" DataFormatString="{0:F2}" 
                            HeaderText="Name">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                                Width="17%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Relation" HeaderText="Relation">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                                Width="20%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CustAdd" HeaderText="Address">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
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
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                                Width="6%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Age" HeaderText="Age">
                            <HeaderStyle BackColor="#9DA9F9" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                                Width="4%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Small" />
                        </asp:BoundField>
                        <asp:CommandField DeleteText="&lt;img src='./images/del.jpg'&gt;" 
                            ShowDeleteButton="True">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:CommandField>
                    </Columns>
                    <AlternatingRowStyle BackColor="#F5F4FF" />
                </asp:GridView>
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
     <tr>
<td> </td>
<td>
    </td>
<td>
    
    </td>
<td> </td>
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
<td> </td>
<td colspan="2">
    <div>
    <table width="100%">
    <tr>
    <td width="50%">
         </td>
    <td width="50%">
        <table width="100%">
        <tr>
        <td width="30%">
             </td>
        <td width="70%">
             </td>
        </tr>
        </table>   
        </td>
    </tr>
    <tr>
    <td>
        <label>Schee</label></td>
    <td>
           <table width="100%">
        <tr>
        <td width="30%">
             </td>
        <td width="70%">
             </td>
        </tr>
        </table>
        </td>
    </tr>
    <tr>
    <td>
        <label>Account No</label></td>
    <td>
         <table width="100%">
        <tr>
        <td width="30%">
             </td>
        <td width="70%">
             </td>
        </tr>
        </table>   
        </td>
    </tr>
    <tr>
    <td class="style1">
        <label>Add</label></td>
    <td class="style1">
        <asp:ImageButton ID="CmdAdd" runat="server" />
        </td>
    </tr>
    </table>--%>
    </div>
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
                &nbsp;</td>
            <td width="40%">
                            <asp:TextBox  CssClass="twitterStyleTextbox" ID="txtRiAmt" runat="server" onkeypress="return validate(event)" 
        Width="100%" AutoPostBack="True" Enabled="False" Visible="False" ></asp:TextBox>
            </td>
            </tr>
            </table>
            
            
            
             
            </td>
            <td>
            </td>
        </tr>
        
<tr>
<td> </td>
<td>
    <label >Select Loan Status</label></td>
<td>
    <asp:DropDownList ID="CmbLoanStatus" runat="server" Width="100%" TabIndex="9" 
        AutoPostBack="True">
        <asp:ListItem>APPROVED</asp:ListItem>
        <asp:ListItem>REJECTED</asp:ListItem>
    </asp:DropDownList>
    <asp:HiddenField ID="HiddenField2" runat="server" />
    </td>
<td> </td>
</tr>
     <tr>
         <td>
              </td>
         <td>
            <label id ="LblResno" runat="server"  >Enter Res.No</label></td></td>
         <td>
         <table width ="100%">
         <tr>
         <td width="30%">
           <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtResNo" runat="server" Width="100%"></asp:TextBox>
         </td>
             <td width="6%">
         </td>
                 <td width="24%">
                 
                 <label id ="LblResDate" runat="server"  >Res.Date</label>
                    </td>
                 <td width="40%">
                 
                         <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtResDate" runat="server" Width="100%" TabIndex="10"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" 
                    TargetControlID="TxtResDate"  Format="dd/MM/yyyy">
                </asp:CalendarExtender>       
                 
                 
         </td>
         </tr>
         </table>
         
           
         </td>
         <td>
              </td>
     </tr>
     <tr>
         <td>
             &nbsp;</td>
         <td>
          <label id ="LblSanAmt" runat="server"  >Loan Sanction Amount</label>
       </td>
         <td>
             <asp:TextBox ID="TxtLoanSanAmount" runat="server" 
                 CssClass="twitterStyleTextbox" TabIndex="10" Width="50%"  
                 onkeypress="return isFloatKey(event);"></asp:TextBox>

         </td>
         <td>
             &nbsp;</td>
    </tr>
     <tr>
         <td>
              </td>
         <td>
             <label id ="LblReason" runat="server"  >Enter Reason For Rejection</label></td>
         <td>
             <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtReason" runat="server" TabIndex="10" Width="100%"></asp:TextBox>
         </td>
         <td>
              </td>
    </tr>
     <tr>
<td> </td>
<td align="center" colspan="2">
     </td>
<td> </td>
</tr>
    <tr>
        <td>
             </td>
        <td align="center" colspan="2">
            <asp:ImageButton ID="BtnSave" runat="server" ImageUrl="~/Images/Save.png" 
                style="width: 100px" OnClientClick="return ValidateSave();"/>
             <asp:ImageButton ID="ImageButton2" runat="server" 
                ImageUrl="~/Images/Clear.png" />
             <asp:ImageButton ID="ImageButton3" runat="server" 
                ImageUrl="~/Images/Close.png" PostBackUrl="~/Main.aspx" />
        </td>
        <td>
             </td>
    </tr>
<tr>
<td> </td>
<td>
     </td>
<td>
    </td>
<td> </td>
</tr>
</table>
    </div>
   </ContentTemplate>
</asp:UpdatePanel> 


</asp:Content>
