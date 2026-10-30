<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="NEFT_PaymentApproval.aspx.vb" Inherits="SMARTCORE_UI.NEFT_PaymentApproval" title="NEFT Approval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
<script language="javascript" type="text/javascript">
function validatedate(inputText)  
  {  
  var dateformat = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;  
  // Match the date format through regular expression  
  if(inputText.value.match(dateformat))  
  {  
  //document.form1.text1.focus();  
  //Test which seperator is used '/' or '-'  
  var opera1 = inputText.value.split('/');  
  var opera2 = inputText.value.split('-');  
  lopera1 = opera1.length;  
  lopera2 = opera2.length;  
  //Extract the string into month, date and year  
  if (lopera1>1)  
  {  
  var pdate = inputText.value.split('/');  
  }  
  else if (lopera2>1)  
  {  
  var pdate = inputText.value.split('-');  
  }  
  var dd = parseInt(pdate[0]);  
  var mm  = parseInt(pdate[1]);  
  var yy = parseInt(pdate[2]);  
  // Create list of days of a month [assume there is no leap year by default]  
  var ListofDays = [31,28,31,30,31,30,31,31,30,31,30,31];  
  if (mm==1 || mm>2)  
  {  
  if (dd>ListofDays[mm-1])  
  {
  alertify.error('Invalid date format! Please enter a valid date!')     
  return false;  
  }  
  }  
  if (mm==2)  
  {  
  var lyear = false;  
  if ( (!(yy % 4) && yy % 100) || !(yy % 400))   
  {  
  lyear = true;  
  }  
  if ((lyear==false) && (dd>=29))  
  {
  alertify.error('Invalid date format! Please enter a valid date!');     
  return false;  
  }  
  if ((lyear==true) && (dd>29))  
  {
  alertify.error('Invalid date format! Please enter a valid date!');     
  return false;  
  }   
  }   
  }   
  else  
  {
  alertify.error("Invalid date format! Please enter a valid date!");     
  document.form1.text1.focus();  
  return false;  
  }  
  }  
</script>
<style type="text/css">
    .style2
    {
        width: 100%;
    }
    .hidden
    {
        display:none;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%; font-family: Tahoma" >             
    <legend>NEFT Payment</legend>
        <table width="100%">
            <tr>
                <td width="25%">
                    &nbsp;</td>
                <td width="20%">
                    <asp:Label ID="LblRegn" runat="server" ForeColor="#6699FF" Text="Region"></asp:Label>
                </td>
                <td width="20%">
                    <asp:DropDownList ID="Drpregn" runat="server" AutoPostBack="true" Width="98%">
                    </asp:DropDownList>
                </td>
                <td width="25%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="25%">
                    &nbsp;</td>
                <td width="20%">
                      <asp:Label ID="LblArea" runat="server" ForeColor="#6699FF" Text="Area"></asp:Label></td>
                <td width="20%">
                   <asp:DropDownList ID="DrpArea" AutoPostBack="true" Width="98%" runat="server">
                    </asp:DropDownList></td>
                <td width="25%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="25%">
                </td>
                <td width="20%">
                    <asp:Label ID="LblDdl2" runat="server" ForeColor="#6699FF" Text="Branch"></asp:Label>
                </td>
                <td width="20%">
                    <asp:DropDownList ID="DdlBranch" runat="server" Width="98%">
                    </asp:DropDownList>
                </td>
                <td width="25%">
                    &nbsp;</td>
            </tr>
                <tr>
                <td width="25%">
                </td>
                <td width="20%">
                    <asp:Label ID="Label1" runat="server" ForeColor="#6699FF" Text="TrDate"></asp:Label>
                </td>
                <td width="20%">
                    <asp:TextBox ID="TxtTrDate" Width="97%" runat="server"></asp:TextBox>
                </td>
                <td width="25%">
                    &nbsp;</td>
            </tr>
         
            <tr>
                <td width="25%">
                    &nbsp;</td>
                <td width="20%">
                    <asp:Label ID="Label2" runat="server" ForeColor="#6699FF" Text="Bank Name"></asp:Label>
                </td>
                <td width="20%">
                    <asp:DropDownList ID="Drp_BankScheme" Width="98%" runat="server" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td width="25%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="25%">
                    &nbsp;</td>
                <td width="20%">
                    <asp:Label ID="Label3" runat="server" ForeColor="#6699FF" Text="Acc_No"></asp:Label>
                </td>
                <td width="20%">
                    <asp:DropDownList ID="DrpBankAccNo" Width="98%" runat="server">
                    </asp:DropDownList>
                </td>
                <td width="25%">
                    &nbsp;</td>
            </tr>
         
            <tr>
                <td>
                    &nbsp;</td>
                <td colspan="2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <table width="100%">
            <tr>            
                <td align="center" colspan="6" style="width: 40%" width="20%">
                 &nbsp;<asp:ImageButton ID="CmdShow" runat="server" Height="25px" 
                        ImageUrl="~/Images/Show.png" />
                    &nbsp;<asp:ImageButton ID="CmdClose" runat="server" 
                        ImageUrl="~/Images/CloseBtn.png" />
                </td>
            </tr>
            <tr>
                <td width="6%">
                </td>
                <td width="18%">
                    &nbsp;</td>
                <td width="26%">
       
                    &nbsp;</td>
                <td width="18%">
                </td>
                <td width="26%">
                    &nbsp;</td>
                <td width="6%">
                </td>
            </tr>
        </table>
           </div>
<div class="gradientBoxesWithOuterShadows" style="width: 80%; margin-left:10%; margin-right:10%; font-family: Tahoma" >   
    <table class="style2" width="100%">
        <tr>
            <td width="5%">
                &nbsp;</td>
            <td width="90%">
                &nbsp;</td>
            <td width="5%">
                &nbsp;</td>
        </tr>
        <div id="PostDiv" runat="server">
        <tr>
            <td width="5%">
                &nbsp;</td>
            <td width="90%" align="right">
                <asp:CheckBox ID="ChkAll" runat="server" AutoPostBack="true" Text="ALL" />
            </td>
            <td width="5%">
                &nbsp;</td>
        </tr>
             <tr>
            <td width="5%">
                &nbsp;</td>
            <td width="90%">
                    <div style="width: 100%; height: 400px; overflow: scroll">
                <asp:GridView  ID="Gv_PostInt" runat="server" AutoGenerateColumns="False" 
                            CssClass="grid-viewNew"  ShowFooter="True" 
                            Width="100%" DataKeyNames="Tran_No,Acc_Id,Br_Code,Cust_Id,Sch_Code,Acc_No,Accrued,Tds">
                            <Columns>
                              <asp:BoundField DataField="Tran_No" HeaderText="Tran_No"  >
                                     <ItemStyle CssClass="hidden"/>
                                     <HeaderStyle CssClass="hidden"/>
                              </asp:BoundField>
                              <asp:BoundField DataField="Acc_Id" HeaderText="Acc_Id"  >
                              <ItemStyle CssClass="hidden"/>
                                     <HeaderStyle CssClass="hidden"/>
                                     </asp:BoundField>
                                     <asp:BoundField DataField="Br_Code" HeaderText="Br_Code">
                                     <ItemStyle CssClass="hidden"/>
                                     <HeaderStyle CssClass="hidden"/>
                                     </asp:BoundField>
                                     <asp:BoundField DataField="Br_Name" HeaderText="Branch"  >
                                     </asp:BoundField>
                                     <asp:BoundField DataField="Cust_Id" HeaderText="Cust_Id"  >
                                     </asp:BoundField>
                                     <asp:BoundField DataField="Cust_Name" HeaderText="Cust_Name"  >
                                     </asp:BoundField>
                                     <asp:BoundField DataField="Sch_Code" HeaderText="Sch_Code"  >
                                     <ItemStyle CssClass="hidden"/>
                                     <HeaderStyle CssClass="hidden"/>
                                     </asp:BoundField>
                                     <asp:BoundField DataField="Sch_Name" HeaderText="Scheme"  >
                                     </asp:BoundField>
                                     <asp:BoundField DataField="Acc_No" HeaderText="Acc_No"  >
                                     </asp:BoundField>   
                                     <asp:BoundField DataField="Outstand" HeaderText="Dep_Amount">
                                     </asp:BoundField>
                                     <asp:BoundField DataField="Accrued" HeaderText="IntAmnt">
                                     </asp:BoundField>
                                     <asp:BoundField DataField="Tds" HeaderText="TDS"  >
                                     </asp:BoundField>
                                     <asp:BoundField DataField="PayableDr" HeaderText="PayableDr"  >
                                     </asp:BoundField>
                                     <asp:TemplateField HeaderText="Bank">
                                <ItemTemplate>
                                <asp:CheckBox ID="ChkSelect" runat="server" Checked='<%# Eval("IsSelect") %>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            </Columns>
                </asp:GridView>
                </div>
                </td>
            <td width="5%">
                &nbsp;</td>
        </tr>
        </div>
        <div id="DivBank" runat="server">
        </div>
        <tr>
            <td>
                &nbsp;</td>
            <td align="center">
                     <asp:ImageButton ID="BtnApprove" runat="server" ImageUrl="~/Images/Approve.png" 
           meta:resourcekey="CmdSaveResource1" TabIndex="36" />
       <asp:ImageButton ID="ImageButton3" runat="server" 
           ImageUrl="~/Images/Reject.png" meta:resourcekey="CmdSaveResource1" 
           TabIndex="36" />
           </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</div>
   </ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>  