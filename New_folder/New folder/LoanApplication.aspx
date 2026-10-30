<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LoanApplication.aspx.vb" Inherits="SMARTCORE_UI.LoanApplication" MasterPageFile="~/SMARTCORE.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
  
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   
<ContentTemplate>

<script type='text/javascript'>

    function ValidateSave() {



        var Ctl = document.getElementById('<%=TxtLoanAmt.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Amount Not Set, Please Check');
            Ctl.focus();
            return true;
        }

       return true;
    }

</script>

  <script type="text/javascript">
      function imgLiab_onclick() {
          
          $('.gradientBoxesWithOuterShadows').append('<div id="resultdiv" title="Customer Asset Liablities" visible="false" >');
          $("#resultdiv").load("AssetLiab.aspx?CustID=" + document.getElementById('<%=HdnSurCustID.ClientID%>').value, {}, function(data) {
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
     <legend>Loan Application</legend>
 <table width="100%">
<tr>
<td width="2%"></td>
<td width="40%">
     </td>
<td width="56%">
<%--    <table width="100%">
    <tr>
    <td width="30%"> 
         </td>
    <td width="70%">
    
    </td>
    </tr>
    </table> --%>
    
        </td>
<td width="2%" colspan="2">
     </td>
</tr>
<tr>
<td> </td>
<td align="left">
    <label>Branch</label></td>
<td>
    <table width="100%">
    <tr>
    <td width="30%">
    <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtBranch" Width="95%" 
            runat="server" TabIndex="2" AutoPostBack="True"></asp:TextBox>
    </td>
    <td width="70%">
    <asp:DropDownList ID="CmbBranch" Width="100%" runat="server" AutoPostBack="True" 
            TabIndex="3">
    </asp:DropDownList>
    </td>
    </tr>
    </table>
    </td>
<td colspan="2"> </td>
</tr>





<tr>
<td>  </td>
<td align="left">
    <label>
    Application Form No</label></td>
<td>
    <asp:DropDownList ID="Cmb_LoanAppNo" runat="server" AutoPostBack="True" 
        Width="100%">
    </asp:DropDownList>
    
    <asp:Panel ID="Panel1" runat="server">
        <asp:TextBox ID="TxtApplNo" runat="server" Width="100%"></asp:TextBox>
    </asp:Panel>
    
    </td>
<td colspan="2">  </td>
</tr>
     <tr>
         <td>
         </td>
         <td align="left">
             <label>
             Loan Scheme</label></td>
         <td>
             <table width="100%">
                 <tr>
                     <td width="30%">
                         <asp:TextBox ID="TxtSchCode" runat="server" AutoCompleteType="Disabled" 
                             CssClass="twitterStyleTextbox" TabIndex="5" Width="95%" 
                             AutoPostBack="True"></asp:TextBox>
                     </td>
                     <td width="70%">
                         <asp:DropDownList ID="CmbScheme" runat="server" AutoPostBack="True" 
                             TabIndex="5" Width="100%">
                         </asp:DropDownList>
                     </td>
                 </tr>
             </table>
         </td>
         <td colspan="2">
         </td>
     </tr>
     <tr>
         <td>
         </td>
         <td align="left">
              </td>
         <td>
             <table width="100%">
                 <tr>
                     <td width="30%">
                          </td>
                     <td width="70%">
                         <table width="100%">
                             <tr>
                                 <td>
                                      </td>
                                 <td>
                                      </td>
                             </tr>
                         </table>
                     </td>
                 </tr>
             </table>
         </td>
         <td colspan="2">
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
<td colspan="2"> 
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
            <td colspan="2">
                 </td>
        </tr>






     <tr>
         <td>
              </td>
         <td>
              </td>
         <td>
              </td>
         <td colspan="2">
              </td>
     </tr>
   <tr>
            <td>
            </td>
            <td>
                <label>
                Application Date</label></td>
            <td>
            
            <table width ="100%">
            <tr>
            <td width="30%">
               <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtIssueDate" runat="server" Width="100%" TabIndex="10" 
                    Enabled="False"></asp:TextBox>
                <asp:CalendarExtender ID="TxtIssueDate_CalendarExtender" runat="server" 
                    TargetControlID="TxtIssueDate"  Format="dd/MM/yyyy">
                </asp:CalendarExtender>
            </td>
            <td width="6%"> </td>
            <td width="24%">
            <label >  Loan Amount</label>
            </td>
            <td width="40%">
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtLoanAmt" runat="server" 
        Width="100%" AutoPostBack="True" onkeypress="return isFloatKey(event);"></asp:TextBox>
            </td>
            </tr>
            </table>
            
            
            
             
            </td>
            <td colspan="2">
            </td>
        </tr>



     <tr>
         <td>
              </td>
         <td>
             <asp:CheckBox ID="ChkRenewal" runat="server" Text="LoanRenewal" 
                 Visible="False" />
             <asp:CheckBox ID="Chkaddmem" runat="server" AutoPostBack="True" 
                 Text="Adl Mem" Visible="False" />
         </td>
         <td>
             <asp:CheckBox ID="ChkSur" runat="server" AutoPostBack="True" 
                 Text="Add Guarantor Details" />
         </td>
         <td colspan="2">
              </td>
     </tr>
     <tr>
         <td>
              </td>
         <td>
              </td>
         <td>
              </td>
         <td colspan="2">
              </td>
     </tr>



</table>
<div id ="DivSur" runat ="server"  visible ="false"  > 
<table width ="100%">
     <tr>
         <td width ="2%">
              </td>
         <td width ="40%">
             <asp:HiddenField ID="HdnSurCustID" runat="server" />
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
    <label>Select  Guarantor Custstomer ID</label></td>
<td>
    <table width="100%">
    <tr>
    <td width="30%">
        <asp:TextBox ID="TxtCustId" runat="server" AutoPostBack="True" Width="90%"></asp:TextBox>
        </td>
    <td width="70%">
   <table width ="100%">
   <tr>
   <td>
       <asp:ImageButton ID="ImgSrch" runat="server" Height="20px" 
           ImageUrl="~/images/Search.png" Width="23px" />
       </td>
   <td>
   


       <table width="100%">
           <tr>
               <td width="85%">
                    </td>
               <td width="15%">
                   <asp:Button ID="Button2" runat="server" Visible="false"
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
     </td>

</tr>
     <tr>
         <td>
               </td>
         <td colspan="3">
             <label>
             <label>
             ====&gt;&gt;</label>Suerty Details</label></td>
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
 </table>    
     </div>
 
 <div id ="DivFieldEnq" runat ="server"  visible ="true"  > 
<table width ="100%">
     <tr>
         <td width ="2%">
              </td>
         <td width ="40%">
             <asp:HiddenField ID="HiddenField1" runat="server" />
        </td>
         <td width ="56%">
             <asp:TextBox  CssClass="twitterStyleTextbox" ID="TextBox1" runat="server" AutoPostBack="True" TabIndex="13" 
                 Visible="False" Width="95%"></asp:TextBox>
         </td>
         <td width ="2%">
              </td>
     </tr>
<tr>
<td> </td>
<td align="left">
    <label>
    <label>
    ====&gt;&gt;</label>Field Enquiry Reports</label></td>
<td>
    <table width="100%">
    <tr>
    <td width="30%">
         </td>
    <td width="70%">
   <table width ="100%">
   <tr>
   <td>
        </td>
   <td>
   


       <table width="100%">
           <tr>
               <td width="85%">
                    </td>
               <td width="15%">
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
         
            <table width ="100%">
                <tr>
                    <td width ="18%">
                    
                        <label>Score Points</label>
                    </td>
                    <td width ="28%">
                    
        
                    
                        <asp:DropDownList ID="CmbScore" runat="server" TabIndex="20" Width="100%">
                        </asp:DropDownList>
                    
        
                    
                    </td>
                    <td width ="18%">
                        <label>Opinion</label>
                    </td>
                    <td width ="28%">
                        <asp:DropDownList ID="CmbOpinion" Width="100%" runat="server"   TabIndex="20">
                       
                        </asp:DropDownList>
                    </td>
                    <td width ="28%">

                        <asp:Button ID="BtnEnqAdd" runat="server" 
                              Text="Add" Visible="True" />

                    </td>
                    
                </tr>

            </table>
         
             </td>
     </tr>
     <tr>
         <td>
         </td>
         <td colspan="3">
         

              <asp:GridView ID="DgvField" runat="server" AutoGenerateColumns="False" 
                 DataKeyNames="EnqID" TabIndex="17" Width="100%">
                 <Columns>
                     <asp:BoundField DataField="LANO" HeaderText="LANO" Visible ="false">
 <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White"  />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" />
                     </asp:BoundField>
                     <asp:BoundField DataField="EnqID" HeaderText="EnqID" 
                         Visible="False" />
    
                     <asp:BoundField DataField="EnqIDDesc" HeaderText="Score Points"   >
 <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="45%" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" />
                     </asp:BoundField>
                     <asp:BoundField DataField="EnqRateID" HeaderText="EnqRateID" Visible="False">
 <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" />
                     </asp:BoundField>
                     <asp:BoundField DataField="EnqRateDesc" HeaderText="Opinion">
 <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="45%" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" HorizontalAlign="Center" />
                     </asp:BoundField>


      
                     <asp:BoundField DataField="EnqValue" HeaderText="EnqValue" Visible ="false">
 <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
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
              <asp:HiddenField ID="HiddenField2" runat="server" Visible="False" />
              </td>
         <td>
              </td>
         <td>
              </td>
     </tr>
 </table>    
     </div>
 

 <div id ="Div1" runat ="server"  visible ="true"  > 
<table width ="100%">
     <tr>
         <td width ="2%">
              </td>
         <td width ="40%">
             &nbsp;</td>
         <td width ="56%">
             &nbsp;</td>
         <td width ="2%">
              </td>
     </tr>
<tr>
<td> </td>
<td align="left">

   <label>====&gt;&gt;Documentation Details</label></td>
<td>
    <table width="100%">
    <tr>
    <td width="30%">
         </td>
    <td width="70%">
   <table width ="100%">
   <tr>
   <td>
        </td>
   <td>
   


       <table width="100%">
           <tr>
               <td width="85%">
                    </td>
               <td width="15%">
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
         
            <table width ="100%">
                <tr>
                    <td width ="18%">
                    
                        <label>Select  Documents</label>
                    </td>
                    <td colspan="2" width ="66%">
                    
        
                    
                        <asp:DropDownList ID="CmbDocument" runat="server" TabIndex="20" Width="100%">
                        </asp:DropDownList>
                    
        
                    
                    </td>
                    <td width ="8%">

                    </td>
                    <td width ="28%">

                        <asp:Button ID="BtnDocAdd" runat="server" 
                              Text="Add" Visible="True" />

                    </td>
                    
                </tr>
                
                
             <tr>
                    <td width ="18%">
                    
                        
                    </td>
                    <td colspan="2" width ="66%">
    
                    
                    </td>
                    <td width ="8%">

                    </td>
                    <td width ="28%">


                    </td>
                    
                </tr>

            </table>
         
             </td>
     </tr>
     <tr>
         <td>
         </td>
         <td colspan="3">
         

              <asp:GridView ID="DgvDoc" runat="server" AutoGenerateColumns="False" 
                 DataKeyNames="DocID" TabIndex="17" Width="100%">
                 <Columns>
                     <asp:BoundField DataField="LANO" HeaderText="LANO" Visible ="false">
 <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White"  />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" />
                     </asp:BoundField>
                     <asp:BoundField DataField="DocID" HeaderText="EnqID" 
                         Visible="False" />
    
                     <asp:BoundField DataField="DocDesc" HeaderText="Document Details"   >
 <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="90%" />
                         <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                             Font-Size="Small" />
                     </asp:BoundField>



      
                     <asp:BoundField DataField="EnqValue" HeaderText="EnqValue" Visible ="false">
 <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
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
              <asp:HiddenField ID="HiddenField4" runat="server" Visible="False" />
              </td>
         <td>
              </td>
         <td>
              </td>
     </tr>
 </table>    
     </div>
 
 
 
 
 
 
  <div id="DivTran" runat ="server"  visible ="false"  >     

     <table width="100%">
    <tr>
         <td width ="2%">
              </td>
         <td width ="40%">
        </td>
         <td width ="56%">
              </td>
         <td width ="2%">
              </td>
     </tr>
     
         <tr>
             <td width="2%">
                  </td>
             <td width="40%">
                  </td>
             <td width="56%">
                  </td>
             <td width="2%">
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
         <td colspan="2">
              <asp:GridView ID="DgvTransaction" runat="server" AutoGenerateColumns="False" 
                  TabIndex="32" Width="100%">
                  <Columns>
                      <asp:BoundField DataField="AccNo" HeaderText="AccNo" Visible="False">
                          <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                          <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                              Font-Size="Medium" />
                      </asp:BoundField>
                      <asp:BoundField DataField="AccCode" HeaderText="AccCode" Visible="False" />
                      <asp:BoundField DataField="AccHead" HeaderText="Particulars">
                          <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                              BorderWidth="1px" Font-Names="Times New Roman" Font-Size="Medium" 
                              ForeColor="White" HorizontalAlign="Left" Width="30%" />
                          <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                              Font-Size="Medium" ForeColor="#333399" />
                      </asp:BoundField>
                      <asp:BoundField DataField="RctCash" DataFormatString="{0:F2}" 
                          HeaderText="Receipt Cash">
                          <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                              BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="15%" />
                          <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                              Font-Size="Medium" ForeColor="#333399" HorizontalAlign="Right" />
                      </asp:BoundField>
                      <asp:BoundField DataField="RctTransfer" DataFormatString="{0:F2}" 
                          HeaderText="Receipt Transfer">
                          <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                              BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                          <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                              Font-Size="Medium" ForeColor="#333399" HorizontalAlign="Right" />
                      </asp:BoundField>
                      <asp:BoundField DataField="PayCash" DataFormatString="{0:F2}" 
                          HeaderText="Payment Cash">
                          <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                              BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                          <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                              Font-Size="Medium" ForeColor="#333399" HorizontalAlign="Right" />
                      </asp:BoundField>
                      <asp:BoundField DataField="PayTransfer" DataFormatString="{0:F2}" 
                          HeaderText="Payment Transfer">
                          <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                              BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                          <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                              Font-Size="Medium" ForeColor="#333399" HorizontalAlign="Right" />
                      </asp:BoundField>
                      <asp:BoundField DataField="Seq_No" HeaderText="Seq_No" Visible="False" />
                  </Columns>
                  <AlternatingRowStyle BackColor="#F5F7FE" />
              </asp:GridView>
              </td>
          
          <td></td>
     </tr>
<tr>
<td> </td>
<td colspan="2">

    <%--<table width="100%">
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
<td> </td>
</tr>
<tr>
<td> </td>
<td>
    
    </td>
<td>
     </td>
<td></td>
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
             Aditional Member</label></td>
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
<table width="100%">



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
<td align="center" colspan="2">
    <asp:ImageButton ID="BtnSave" runat="server" ImageUrl="~/Images/Save.png" OnClientClick="return ValidateSave();"/>
     <asp:ImageButton ID="ImageButton2" runat="server" 
        ImageUrl="~/Images/Clear.png" />
     <asp:ImageButton ID="ImageButton3" runat="server" 
        ImageUrl="~/Images/Close.png" PostBackUrl="~/Main.aspx" />
    </td>
<td> </td>
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
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">

    <LINK REL=Stylesheet HREF= "CSS/MasterStyle.css" TYPE="text/css" MEDIA=screen>    
    
        <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
    <link href="CSS/ui-lightness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript' src="Scripts/Common.js"></script>
    <script type='text/javascript' src="Scripts/jquery-1.8.3.js"></script>  
    <script type='text/javascript' src="Scripts/jquery-ui-1.9.2.custom.min.js"></script>   
    </asp:Content>

