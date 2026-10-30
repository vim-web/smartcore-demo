<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="LoanPIForm.aspx.vb" Inherits="SMARTCORE_UI.LoanPIForm"  title="Untitled Page" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type='text/javascript' src="Scripts/Common.js"></script>  
    <script type='text/javascript' src="Scripts/jquery-1.8.3.js"></script>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>


    </td>
</tr>


<script type='text/javascript'>

    function ValidateSave() {    

        var Ctl = document.getElementById('<%=TxtPifDate.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Date Not Entered, Please Check');
            Ctl.focus();
            return false;
        }


        var Ctl = document.getElementById('<%=TxtPropertyValue.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Amount Not Set, Please Check');
            Ctl.focus();
            return false;
        }




        return true;
    }

</script>



<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    

        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 80%; margin-left:10%; margin-right:10%;">
     
     <legend>PROPERTY INSPECTION DETAILS</legend>
 <table width="90%"  style="padding-left:10%">
<tr>
<td width="40%">
    &nbsp;</td>
<td width="56%">
    
        <asp:DropDownList ID="CmbLoanAppType" Width="100%" runat="server" 
            AutoPostBack="True" Visible="false">    

    </asp:DropDownList>
</td>
</tr>




<tr>
<td align="left">
    <label>
    Branch</label></td>
<td>
    
        <asp:DropDownList ID="CmbBranch" runat="server" AutoPostBack="True" 
            TabIndex="4" Width="100%">
        </asp:DropDownList>
    </td>
</tr>
<tr>
<td align="left">
    <label>
    Application No</label></td>
<td>

    <asp:DropDownList ID="CmbApplicationNo" runat="server" AutoPostBack="True" 
        Width="100%">
    </asp:DropDownList>
    
    </td>
</tr>
     <tr>
         <td align="left">
             <label>
             Loan Scheme</label></td>
         <td>
             <asp:DropDownList ID="CmbScheme" runat="server" AutoPostBack="True" 
                 TabIndex="5" Width="100%">
             </asp:DropDownList>
         </td>
     </tr>
<%--     <tr visible ="false" >
         <td align="left">
             <label visible =f>
             Member Number</label></td>
         <td>
             <asp:TextBox ID="TxtMemberno" runat="server" CssClass="twitterStyleTextbox" 
                 Enabled="False" ReadOnly="True" Width="100%"></asp:TextBox>
         </td>
     </tr>--%>
<tr>
<td>
    <label>
                Name </label>
                

    </td>
<td>
    <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtName" Width="100%" 
        runat="server" ReadOnly="True" Enabled="False"></asp:TextBox>
   </td>
</tr>
        <tr>
            <td>
      <label>
                Address </label>
                </td>
            <td>
            
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtAddress" Width="100%" 
                    runat="server" TextMode="MultiLine" 
                    ReadOnly="True" Enabled="False"></asp:TextBox>
                </td>
        </tr>
<%--<tr>
<td>
    <label>Amsam</label></td>
<td>
     
    <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtAmsam" runat="server" 
        Width="100%" ></asp:TextBox>
     
    </td>
</tr>
<tr visible="false" >
<td>
    <label>Desam</label></td>
<td>
    <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtDesam" runat="server" Width="100%"></asp:TextBox>
    </td>
</tr>
<tr visible="false" >
<td>
    <label>Director</label></td>
<td>
    <asp:DropDownList ID="CmbDirector" runat="server" Width="100%" TabIndex="9">
    </asp:DropDownList>
    </td>
</tr>--%>
   <tr>
            <td>
                <label>
                Loan Application Date</label></td>
            <td>
            
            <table width ="100%">
            <tr>
            <td width="30%">
                <asp:TextBox  CssClass="twitterStyleTextbox" ID = "TxtLoanDate" width ="100%" 
                    runat="server" ReadOnly="True" Enabled="False" ></asp:TextBox>
                
                </td>
            <td width="6%"> </td>
            <td width="24%">
            <label >  Loan Amount</label>
            </td>
            <td width="40%">
            <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtLoanAmount" runat="server" onkeypress="return validate(event)" 
        Width="100%" AutoPostBack="True" ReadOnly="True" Enabled ="false"></asp:TextBox>
            </td>
            </tr>
            </table>
            
            
            
             
            </td>
        </tr>

   <tr>
            <td>
                <label>
                Property Inspection Date</label></td>
            <td>
            
            <table width ="100%">
            <tr>
            <td width="30%">
               <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtPifDate" runat="server" Width="100%" TabIndex="10" 
                    ></asp:TextBox>
                <asp:CalendarExtender ID="TxtPifDate_CalendarExtender" runat="server" 
                    TargetControlID="TxtPifDate"  Format="dd/MM/yyyy">
                </asp:CalendarExtender>
            </td>
            <td width="6%"> </td>
            <td width="24%">
            <label >  Property Value</label>
            </td>
            <td width="40%">
                            <asp:TextBox  CssClass="twitterStyleTextbox" ID="TxtPropertyValue" runat="server"
        Width="100%" AutoPostBack="True" onkeypress="return isFloatKey(event);" ></asp:TextBox>
            </td>
            </tr>
            </table>
            
            
            
             
            </td>
        </tr>
   

    </div>
<tr>
<td>
</td>
<td>
    </td>
</tr>
     <tr>
         <td>
              <asp:HiddenField ID="HdnAccid" runat="server" />
              </td>
         <td>
              <asp:HiddenField ID="HdnCustID" runat="server" Visible="False" />
              </td>
     </tr>
     
     
       <tr>
         <td colspan ="2">
         
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
        &nbsp;</td>
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
    
                    
                        <label>
                        Legal Scrunity/Site Inspection/Property valuation Documents</label></td>
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
              <asp:ImageButton ID="BtnSave" runat="server" ImageUrl="~/Images/Save.png" 
                  onclientclick="return ValidateSave();" />
              <asp:ImageButton ID="ImageButton2" runat="server" 
                  ImageUrl="~/Images/Clear.png" />
              <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/Close.png" 
                  PostBackUrl="~/Main.aspx" />
              </td>
         <td>
              </td>
     </tr>
 </table>    
     </div>
          
         </td>
     </tr>
    <tr>
        <td>
        </td>
        <td>
        </td>
        <tr>
            <td align="center" colspan="2">
            </td>
        </tr>
        <td>
        </td>
        <td>
        </td>
    </tr>
    </ContentTemplate>
</asp:UpdatePanel> 


</asp:Content>
