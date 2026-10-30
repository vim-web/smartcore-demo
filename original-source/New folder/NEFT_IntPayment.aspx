<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="NEFT_IntPayment.aspx.vb" Inherits="SMARTCORE_UI.NEFT_IntPayment" 
    title="NEFT Process" %>
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
//  document.form1.text1.focus();  
  //Test which seperator is used '/' or '-'  
  var opera1 = inputText.value.split('/');  
  var opera2 = inputText.value.split('-');  
  lopera1 = opera1.length;  
  lopera2 = opera2.length;  
  // Extract the string into month, date and year  
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
      alertify.error('Invalid date format! Please enter a valid date!');
     
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
        .style1
        {
            height: 33px;
        }
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
                      <asp:Label ID="LblRpt" runat="server" ForeColor="#6699FF" Text="Process Type"></asp:Label></td>
                <td width="20%">
                    <asp:DropDownList ID="DrpRptType" runat="server" Width="98%" 
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
                    <asp:Label ID="LblRegn" runat="server" ForeColor="#6699FF" Text="Region"></asp:Label>
                </td>
                <td width="20%">
                    <asp:DropDownList ID="Drpregn" runat="server" AutoPostBack="true" Width="98%">
                    </asp:DropDownList>
                </td>
                <td width="25%">
                    <asp:Timer ID="Timer1" runat="server" Interval="10000">
                    </asp:Timer>
                </td>
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
                <td width="25%" class="style1">
                    </td>
                <td width="20%" class="style1">
                    <asp:Label ID="Label3" runat="server" ForeColor="#6699FF" Text="Section"></asp:Label>
                </td>
                <td width="20%" class="style1">
                    <asp:DropDownList ID="Ddlsection" runat="server" Width="98%" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td width="25%" class="style1">
                    </td>
            </tr>
            <tr>
                <td width="25%">
                    &nbsp;</td>
                <td width="20%">
                    <asp:Label ID="Label1" runat="server" Text="Scheme" ForeColor="#6699FF"></asp:Label>
                    &nbsp;</td>
                <td width="20%">
                    <asp:DropDownList ID="DdlScheme" runat="server" Width="98%" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td width="25%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                               <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                        AssociatedUpdatePanelID="UpdatePanel1">
                        <ProgressTemplate>
                            <div ID="blur">
                            </div>
                            <div ID="progress">
                                <img alt="progress" src="Images/progress.gif" /> Please Wait...
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    </td>
                <td>
                    <asp:Label ID="Label2" runat="server" ForeColor="#6699FF" Text="As On Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="DtTo" runat="server" width="98%" ></asp:TextBox>
                     <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                        TargetControlID="DtTo">
                    </asp:CalendarExtender>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <div id="DivbankAccount" runat="server" visible="false">
            <tr>
                <td>
                    </td>
                <td>
                    <asp:Label ID="Label6" runat="server" ForeColor="#6699FF" Text="Bank Branch"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DrpBankBranch" Width="98%" runat="server" 
                        AutoPostBack="True">
                    </asp:DropDownList>                   
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    </td>
                <td>
                    <asp:Label ID="Label4" runat="server" ForeColor="#6699FF" Text="Bank"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DrpBank" Width="98%" runat="server" AutoPostBack="True">
                    </asp:DropDownList>                   
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="Label7" runat="server" ForeColor="#6699FF" Text="Bank AccNo"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DrpBankAccNo" Width="98%" runat="server" 
                        AutoPostBack="True">
                    </asp:DropDownList> 
                </td>
                <td>
                    <asp:HiddenField ID="HdnBankAccId" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    </td>
                <td>
                    <asp:Label ID="Label5" runat="server" ForeColor="#6699FF" Text="Remarks"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TxtRemarks" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
           </div>
            <tr>
                <td>
                    &nbsp;</td>
                <td colspan="2">
                    <asp:Label ID="lblProgress" runat="server" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <table width="100%">
            <tr>
            
                <td align="center" colspan="6" style="width: 40%" width="20%">
                    <asp:CheckBox ID="ChkPost" runat="server" Text="Interest Post" 
                        AutoPostBack="True" />
                <asp:ImageButton ID="BtnPro" runat="server" ImageUrl="~/Images/ProcessBtn.png" Height="25px" />
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
                    <asp:ImageButton ID="BtnPost" runat="server" Height="25px" 
                        ImageUrl="~/Images/PostBtn.png" />
                </td>
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
                            Width="100%" DataKeyNames="Acc_Id,Br_Code,Cust_Id,Sch_Code,Acc_No,Accrued,Tds">
                            <Columns>
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
                                     <asp:BoundField DataField="Outsanding" HeaderText="Dep_Amount"  >
                                     </asp:BoundField>
                                       <asp:BoundField DataField="Accrued" HeaderText="IntAmnt"  >
                                     </asp:BoundField>
                                         <asp:BoundField DataField="Tds" HeaderText="TDS"  >
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
        <tr>
            <td>
                &nbsp;</td>
            <td>
            <div style="width: 100%; height: 400px; overflow: scroll">
                <asp:GridView  ID="GvDeposit" runat="server" AutoGenerateColumns="False" 
                            CssClass="grid-viewNew"  ShowFooter="True" 
                            Width="100%" DataKeyNames="Acc_Id">
                            <Columns>
                              <asp:BoundField DataField="Acc_Id" HeaderText="Acc_Id"  >
                                   <ItemStyle CssClass="hidden"/>
                                    <HeaderStyle CssClass="hidden"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Br_Name" HeaderText="Branch"  >
                                     </asp:BoundField>
                                     <asp:BoundField DataField="Cust_Id" HeaderText="Cust_Id"  >
                                     </asp:BoundField>
                                       <asp:BoundField DataField="Cust_Name" HeaderText="Cust_Name"  >
                                     </asp:BoundField>
                                     <asp:BoundField DataField="Sch_Name" HeaderText="Scheme"  >
                                     </asp:BoundField>
                                      <asp:BoundField DataField="Acc_No" HeaderText="Acc_No"  >
                                     </asp:BoundField>
                                     <asp:BoundField DataField="Address" HeaderText="Address"  >
                                     </asp:BoundField>
                                     <asp:BoundField DataField="Eff_Date" HeaderText="Eff_Date"  >
                                     </asp:BoundField>
                                     <asp:BoundField DataField="DueDate" HeaderText="DueDate"  >
                                     </asp:BoundField>
                                     <asp:BoundField DataField="Dep_Amount" HeaderText="Dep_Amount"  >
                                     </asp:BoundField>
                                     <asp:BoundField DataField="TDS_Status" HeaderText="TDS_Status"  >
                                     </asp:BoundField>
                                         <asp:BoundField DataField="SubDate" HeaderText="TDS_SubDate"  >
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
            <td>
                &nbsp;</td>
        </tr>
        </div>
        <tr>
            <td>
                &nbsp;</td>
            <td align="center">
                <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                    meta:resourcekey="CmdSaveResource1" 
                    OnClientClick="return ValidateSave1() &amp;&amp; ValidateSave();" 
                    TabIndex="35" />
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