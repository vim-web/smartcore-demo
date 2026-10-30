<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="InterestRateMaster.aspx.vb" Inherits="SMARTCORE_UI.InterestRateMaster" 
    title="InterestRate Master" %>
         <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
<script type='text/javascript' src="Scripts/Common.js"></script>

 
   <script type="text/javascript">
       function validatedate(inputText) {
           var dateformat = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
           // Match the date format through regular expression  
           if (inputText.value.match(dateformat)) {
               //  document.form1.text1.focus();  
               //Test which seperator is used '/' or '-'  
               var opera1 = inputText.value.split('/');
               var opera2 = inputText.value.split('-');
               lopera1 = opera1.length;
               lopera2 = opera2.length;
               // Extract the string into month, date and year  
               if (lopera1 > 1) {
                   var pdate = inputText.value.split('/');
               }
               else if (lopera2 > 1) {
                   var pdate = inputText.value.split('-');
               }
               var dd = parseInt(pdate[0]);
               var mm = parseInt(pdate[1]);
               var yy = parseInt(pdate[2]);
               // Create list of days of a month [assume there is no leap year by default]  
               var ListofDays = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
               if (mm == 1 || mm > 2) {
                   if (dd > ListofDays[mm - 1]) {
                       alert('Invalid date format! Please enter a valid date!');

                       return false;
                   }
               }
               if (mm == 2) {
                   var lyear = false;
                   if ((!(yy % 4) && yy % 100) || !(yy % 400)) {
                       lyear = true;
                   }
                   if ((lyear == false) && (dd >= 29)) {
                       alert('Invalid date format! Please enter a valid date!');

                       return false;
                   }
                   if ((lyear == true) && (dd > 29)) {
                       alert('Invalid date format! Please enter a valid date!');

                       return false;
                   }
               }
           }
           else {
               alert("Invalid date format! Please enter a valid date!");

               document.form1.text1.focus();
               return false;
           }
       }

       function ValidateSave() {
           var Ctl = document.getElementById('<%=TxtCircularDate.ClientID%>');
           if (Ctl.value == "") {
               alert('Enter Circular Date');
               return false;
           }
           var Ctl = document.getElementById('<%=TxtCirNo.ClientID%>');
           if (Ctl.value == "") {
               alert('Enter Circular No');
               return false;
           }
           var Ctl = document.getElementById('<%=TxtEffectiveDate.ClientID%>');
           if (Ctl.value == "") {
               alert('Enter Effective Date');
               return false;
           }
       }
   </script>

 <style type="text/css">
   .HiddenCol{
       
       display:none;

   }                
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True">
    <Scripts>
            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
    </Scripts>
</asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 70%; margin-left:15%; margin-right:15%;" > 
<br />
<table width="100%">

     <tr>
         <td width="5%" ></td>
        <td width="20%" ></td>
        <td width="20%" >
            </td>
        <td width="5%" ></td>
        <td width="20%" ></td>
        <td width="20%" >
            </td>
        <td width="10%" ></td>
     </tr>
          <tr>
              <td width="5%"></td>
              <td width="20%">
                  <label>
                  Scheme</label></td>
              <td width="20%">
                  <asp:DropDownList ID="DrpScheme" runat="server" AutoPostBack="True" 
                                     TabIndex="2" Width="100%">
                  </asp:DropDownList>
              </td>
              <td width="5%"></td>
              <td width="20%">
                  <label>
                  Circular No</label></td>
              <td width="20%">
                  <asp:TextBox ID="TxtCirNo" runat="server" CssClass="twitterStyleTextbox" 
                     MaxLength="10" onkeypress="return isFloatKey(event);" 
                      TabIndex="4" Width="97%"></asp:TextBox>
              </td>
              <td width="10%"></td>
        </tr>
          <tr>
         <td width="5%"></td>
        <td width="20%"><label> Circular Date </label></td>
        <td width="20%">
            <asp:TextBox ID="TxtCircularDate" runat="server" 
        OnChange="validatedate(this);" Width="97%"  CssClass="twitterStyleTextbox" ></asp:TextBox>
    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
        TargetControlID="TxtCircularDate">
    </asp:CalendarExtender>
           
              </td>
        <td width="5%"></td>
        <td width="20%">
            <label>
            Effective Date</label></td>
        <td width="20%">
            <asp:TextBox ID="TxtEffectiveDate" runat="server" 
                CssClass="twitterStyleTextbox" OnChange="validatedate(this);" Width="97%"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                TargetControlID="TxtEffectiveDate">
            </asp:CalendarExtender>
              </td>
        <td width="10%"></td>
     </tr>
          <tr>
         <td width="5%" ></td>
        <td width="20%" >
            <label>
            Period Type</label></td>
        <td width="20%" >
            <asp:DropDownList ID="DrpPeriodType" runat="server"
                TabIndex="7" 
                Width="100%">
            </asp:DropDownList>
           
              </td>
        <td width="5%" ></td>
        <td width="20%" >
            <label>
            Interest For</label></td>
        <td width="10%" >
            <asp:DropDownList ID="DrpInterestFor" runat="server" TabIndex="8" 
                Width="100%" AutoPostBack="True">
            </asp:DropDownList>
              </td>
        <td width="5%" ></td>
     </tr>

              <tr>
         <td width="5%" ></td>
        <td width="20%" >
            <label>
            Pay Mode</label></td>
        <td width="20%" >
            <asp:DropDownList ID="CmbRenewIntPayMode" runat="server"
                TabIndex="7" 
                Width="100%">
            </asp:DropDownList>
           
              </td>
        <td width="5%" ></td>
        <td width="20%" >
            <label>
         </label></td>
        <td width="10%" >
           
              </td>
        <td width="5%" ></td>
     </tr>
         <tr>
             <td width="5%"></td>
             <td width="20%">&nbsp;</td>
             <td width="20%">
                 &nbsp;</td>
             <td width="5%"></td>
             <td width="20%">&nbsp;</td>
             <td width="20%">
                 &nbsp;</td>
             <td width="10%"></td>
        </tr>
          <tr>
         <td width="5%"></td>
        <td width="20%"></td>
        <td width="20%">
            </td>
        <td width="5%"></td>
        <td width="20%"></td>
        <td width="20%">
            </td>
        <td width="10%"></td>
     </tr>
          <tr>
         <td width="5%"></td>
        <td width="20%"></td>
        <td width="20%">
            </td>
        <td width="5%"></td>
        <td width="20%"></td>
        <td width="20%">
            </td>
        <td width="10%">
            </td>
     </tr>
        <tr>
            <td width="5%"></td>
            <td colspan="8">
                <table width="100%">
                    <tr>
                        <td colspan="7">
                            <asp:GridView ID="GVGoldIntRate" runat="server" 
                              
                                PageSize="3" Width="100%" 
                                AutoGenerateColumns="False" ShowFooter="True"   >
                                <RowStyle CssClass="normal" />
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="PeriodStart" HeaderStyle-Width="15%">
                                   <ItemTemplate>
                                     <asp:Label ID="Label2" runat="server"  Text='<%# Bind("PeriodStart") %>' > </asp:Label>
                                  </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="TxtPeriodFrom" runat="server" CssClass="twitterStyleTextbox" 
                                               onkeypress="return isFloatKey(event);"  style="text-align:right"
                                                 TabIndex="9" Width="90%"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PeriodEnd" HeaderStyle-Width="15%">
                                       <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("PeriodEnd") %>'></asp:Label>
                                       </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="TxtPeriodTo" runat="server" CssClass="twitterStyleTextbox" 
                                               onkeypress="return isFloatKey(event);"  style="text-align:right"
                                                 TabIndex="10" Width="90%"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="InterestRate" HeaderStyle-Width="15%">
                                       <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("InterestRate") %>'></asp:Label>
                                       </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="TxtIntRate" runat="server" CssClass="twitterStyleTextbox" 
                                               onkeypress="return isFloatKey(event);" style="text-align:right" 
                                                TabIndex="11" Width="90%"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AmountUpto" HeaderStyle-Width="15%">
                                        <FooterTemplate>
                                            <asp:TextBox ID="TxtAmtUpto" runat="server" CssClass="twitterStyleTextbox" 
                                               MaxLength="13" onkeypress="return isFloatKey(event);" 
                                                style="text-align:right" TabIndex="13" Width="90%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("AmountUpto") %>'></asp:Label>
                                       </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Staff" HeaderStyle-Width="15%">
                                      <FooterTemplate>
                                            <asp:TextBox ID="TxtStaff" runat="server" CssClass="twitterStyleTextbox" 
                                               MaxLength="13" onkeypress="return isFloatKey(event);" 
                                                style="text-align:right" TabIndex="13" Width="90%"></asp:TextBox>
                                        </FooterTemplate>
                                       <ItemTemplate>
                                            <asp:Label ID="Label8" runat="server" Text='<%# Bind("Staff") %>'></asp:Label>
                                       </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ExStaff" HeaderStyle-Width="15%">
                                      <FooterTemplate>
                                            <asp:TextBox ID="TxtExStaff" runat="server" CssClass="twitterStyleTextbox" 
                                               MaxLength="13" onkeypress="return isFloatKey(event);" 
                                                style="text-align:right" TabIndex="13" Width="90%"></asp:TextBox>
                                        </FooterTemplate>
                                       <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("ExStaff") %>'></asp:Label>
                                       </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Senior Citizen" HeaderStyle-Width="15%">
                                      <FooterTemplate>
                                            <asp:TextBox ID="TxtSenCitizen" runat="server" CssClass="twitterStyleTextbox" 
                                               MaxLength="13" onkeypress="return isFloatKey(event);" 
                                                style="text-align:right" TabIndex="13" Width="90%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label10" runat="server" Text='<%# Bind("SenCitizen") %>'></asp:Label>
                                       </ItemTemplate>
                                    </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Pay Mode" HeaderStyle-Width="15%">
                                      <FooterTemplate>
                                            <asp:TextBox ID="TxtPayMode" runat="server" CssClass="twitterStyleTextbox" 
                                               MaxLength="13" onkeypress="return isFloatKey(event);" 
                                                style="text-align:left" TabIndex="13" Width="90%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                           <asp:Label ID="Label5" runat="server" Text='<%# Bind("PayMode")%>' ></asp:Label>
                                       </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderStyle-Width="15%">
                                         <FooterTemplate>
                                             <asp:ImageButton ID="ImgBtnAdd" runat="server" Height="23px" 
                                                 ImageUrl="~/Images/AddBtn.png" onclick="ImgBtnAdd_Click" 
                                                 onclientclick="return ValidateSave()" TabIndex="15" Width="90%" />
                                         </FooterTemplate>
                                         <ItemTemplate>
                                            <asp:ImageButton ID="ImgDel" runat="server" OnClientClick="return confirm('Remove the details')" CommandName="Delete"  ImageUrl="~/Images/del.jpg" Width="15px" />
                                         </ItemTemplate>
                                       
                                    </asp:TemplateField>
                                         <asp:TemplateField  ControlStyle-CssClass="HiddenCol" ItemStyle-CssClass="HiddenCol" HeaderStyle-CssClass="HiddenCol" FooterStyle-CssClass="HiddenCol">
                                      <FooterTemplate >
                                            <asp:TextBox ID="TxtPayModeId" runat="server" CssClass="twitterStyleTextbox" 
                                               MaxLength="13" onkeypress="return isFloatKey(event);" 
                                                style="text-align:left" TabIndex="13" Width="90%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate  >
                                           <asp:Label ID="Lblpaymode" runat="server" Text='<%# Bind("PayModeId")%>'  ></asp:Label>
                                       </ItemTemplate>
                                    </asp:TemplateField>

                                   
                                </Columns>
                                 <HeaderStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" 
                                                BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                                                Width="15%" />
                                           
                                  <FooterStyle BackColor="#00246F"  BorderColor="#7A64E3" BorderStyle="Solid" 
                                                BorderWidth="1px" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" 
                                                Width="15%" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                             
                                <AlternatingRowStyle CssClass="alternate" />
                            </asp:GridView>
                        </td>
                        <td width="5%"></td>
                    </tr>
                    <tr>
                        <td width="16%">
                            &nbsp;</td>
                        <td width="16%">
                            &nbsp;</td>
                        <td width="16%">
                            &nbsp;</td>
                        <td width="16%">
                            &nbsp;</td>
                        <td width="16%">
                            &nbsp;</td>
                        <td width="15%">

                            &nbsp;</td>
                        <td width="5%">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="5%" ></td>
            <td width="20%" ></td>
            <td align="center" colspan="3">
                <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                    meta:resourcekey="CmdSaveResource1" OnClientClick="return ValidateSave();" 
                    TabIndex="56" />
                <asp:ImageButton ID="CmdClear0" runat="server" ImageUrl="~/Images/Clear.png" 
                    meta:resourcekey="CmdSaveResource1" TabIndex="36" />
                <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/images/Close.png" 
                    meta:resourcekey="CmdClearResource1" PostBackUrl="~/Main.aspx" 
                    style="margin-top: 0px" TabIndex="57" />
            </td>
            <td width="20%" ></td>
            <td width="10%" ></td>
        </tr>
                      
</table>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

