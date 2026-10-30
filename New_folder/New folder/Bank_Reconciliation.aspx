<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="Bank_Reconciliation.aspx.vb" Inherits="SMARTCORE_UI.Bank_Reconciliation" 
    title="Bank Reconciliation" %>
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
function CheckOne(obj) {
    var grid = obj.parentNode.parentNode.parentNode;
    var inputs = grid.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].type == "checkbox") {
            if (obj.checked && inputs[i] != obj && inputs[i].checked) {
                inputs[i].checked = false;
            }
        }
    }
}  
</script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 210px;
        }
      
     .hidden
     {
         display:none;
     }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
        }     

    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%; font-family: Tahoma" >                
    
    <legend>Bank Reconciliation</legend>
        <table width="100%">
            <tr>
                <td width="25%">
                </td>
                <td width="20%">
                    <asp:Label ID="LblDdl2" runat="server" ForeColor="#6699FF" Text="Branch"></asp:Label>
                </td>
                <td width="20%">
                    <asp:DropDownList ID="DdlBranch" runat="server" Width="98%" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td width="25%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="25%">
                    &nbsp;</td>
                <td width="20%">
                    <asp:Label ID="Label3" runat="server" ForeColor="#6699FF" Text="Bank"></asp:Label>
                </td>
                <td width="20%">
                    <asp:DropDownList ID="DdlBank" runat="server" Width="98%" 
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
                    <asp:Label ID="LblRecon" runat="server" ForeColor="#6699FF" Text="Recon_Status"></asp:Label>
                </td>
                <td width="20%">
                    <asp:DropDownList ID="DrpReconStatus" runat="server" Width="98%" AutoPostBack="true">                       
                        <asp:ListItem Value="N">Non_Reconciled</asp:ListItem>
                         <asp:ListItem Value="Y">Reconciled</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="25%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="25%">
                    &nbsp;</td>
                <td width="20%">
                    <asp:Label ID="LblDt1" runat="server" Text="From Date" ForeColor="#6699FF"></asp:Label>
                </td>
                <td width="20%">
                <asp:TextBox ID="DtFrom" runat="server" TabIndex="2" Width="98%" OnChange="validatedate(this);"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                        TargetControlID="DtFrom">
                    </asp:CalendarExtender>
                    </td>
                <td width="25%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="LblDt2" runat="server" ForeColor="#6699FF" Text="To Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="DtTo" runat="server" TabIndex="2" Width="98%" OnChange="validatedate(this);"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                        TargetControlID="DtFrom">
                    </asp:CalendarExtender>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td colspan="2">
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
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td colspan="2">
                    <asp:FileUpload ID="FleBank" runat="server" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td align="center" colspan="6" style="width: 40%" width="20%">
                    &nbsp; &nbsp;<asp:ImageButton ID="CmdShow" runat="server" Height="25px" 
                        ImageUrl="~/Images/Show.png" />
                    &nbsp;<asp:ImageButton ID="CmdClose" runat="server" 
                        ImageUrl="~/Images/CloseBtn.png" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="6" style="width: 40%" width="20%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="6%">
                </td>
                <td width="18%">
                    &nbsp;</td>
                    
                <td width="26%">
                   
                    <asp:RadioButtonList ID="RdoReconcile" runat="server" AutoPostBack="True">
                        <asp:ListItem Value="A">Auto Reconcile</asp:ListItem>
                        <asp:ListItem Value="M">Manual Reconcile</asp:ListItem>
                    </asp:RadioButtonList>
                   
                </td>
                <td width="18%">
                    <asp:ImageButton ID="CmdReconcile" runat="server" Height="28px" 
                        ImageUrl="~/Images/Reconcile.png" />
                </td>
             
                <td width="26%">
                </td>
                <td width="6%">
                </td>
            </tr>
        </table>


            </div>

<div class="gradientBoxesWithOuterShadows" style="width: 96%; margin-left:2%; margin-right:2%; font-family: Tahoma" > 
 <table class="style1" width="100%">
  <tr>
        <td width="2%"></td>
            <td width="23%">
                <legend>Bank Passbook</legend></td>
            <td class="style2"  width="25%">
                &nbsp;</td>
            <td  width="25%">
                &nbsp;</td>
            <td width="23%">
                &nbsp;</td>
                 <td width="2%"></td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td colspan="4">
             <div style="min-height:300px;max-height:400px;overflow:scroll;">
                <asp:GridView ID="GvBankTran" Width="100%"  AutoGenerateColumns="False" HeaderStyle-BackColor="#2063b7" Font-Bold="false"  HeaderStyle-ForeColor="White"
                  runat="server" BackColor="#d9d9d9" ForeColor="#282c31" DataKeyNames="ID,BrCode,Acc_Code" >
                  <Columns>
                   <asp:BoundField DataField="ID" HeaderText="ID" >
                   <ItemStyle CssClass="hidden"/>
                   <HeaderStyle CssClass="hidden"/>
                   </asp:BoundField>
                   <asp:BoundField DataField="BrCode" HeaderText="BrCode" >
                   <ItemStyle CssClass="hidden"/>
                   <HeaderStyle CssClass="hidden"/>
                   </asp:BoundField>
                    <asp:BoundField DataField="Acc_Code"  HeaderText="Acc_Code" >   
                    <ItemStyle CssClass="hidden"/> 
                    <HeaderStyle CssClass="hidden"/>
                    </asp:BoundField>
                      <asp:TemplateField HeaderText="Reconcile" ItemStyle-HorizontalAlign="Center">
                         <ItemTemplate>
                         <asp:CheckBox ID="chkSelect"   runat="server" onclick="CheckOne(this)" />
                          </ItemTemplate>
                      </asp:TemplateField>             
                    <asp:BoundField DataField="Br_Name" HeaderText="Branch" >                        
                     </asp:BoundField>
                  <asp:BoundField DataField="Acc_Name" HeaderText="Bank Name">                        
                     </asp:BoundField>
 
                     <asp:BoundField DataField="Credit" HeaderText="Credit">
                        
                     </asp:BoundField>
                     <asp:BoundField DataField="Debit" HeaderText="Debit">
                        
                     </asp:BoundField>       
                     <asp:BoundField DataField="InstrNo" HeaderText="Instrument No">
                         
                     </asp:BoundField>
                     <asp:BoundField DataField="Tr_Date" HeaderText="Tran Date">
                     </asp:BoundField>
                     <asp:BoundField DataField="Recon_Status" HeaderText="Recon_Status">
                     <ItemStyle CssClass="hidden"/>
                     <HeaderStyle CssClass="hidden"/>
                     </asp:BoundField>
                  </Columns>
                </asp:GridView>
                </div>
            </td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="23%">
                &nbsp;</td>
            <td class="style2" width="25%">
                &nbsp;</td>
            <td width="25%">
                &nbsp;</td>
            <td width="23%">
                &nbsp;</td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="23%">
                &nbsp;</td>
            <td class="style2" width="25%">
                <asp:Label ID="LblBankCr" runat="server" ForeColor="Red" Text="Pending Credit"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="LblBankCr1" runat="server" ForeColor="Red" Text="0.00"></asp:Label>
            </td>
            <td width="25%">
                <asp:Label ID="LblBankDr" runat="server" ForeColor="Red" Text="Pending Debit"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="LblBankDr1" runat="server" ForeColor="Red" Text="0.00"></asp:Label>
            </td>
            <td width="23%">
                &nbsp;</td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="23%">
                &nbsp;</td>
            <td class="style2" width="25%">
                &nbsp;</td>
            <td width="25%">
                &nbsp;</td>
            <td width="23%">
                &nbsp;</td>
            <td width="2%">
                &nbsp;</td>
     </tr>
     <tr>
         <td width="2%">
             &nbsp;</td>
         <td width="23%">
             &nbsp;</td>
         <td class="style2" width="25%">
             &nbsp;</td>
         <td width="25%">
             &nbsp;</td>
         <td width="23%">
             &nbsp;</td>
         <td width="2%">
             &nbsp;</td>
     </tr>
        </table>
    <table class="style1" width="100%">
      
        <tr>
         <td width="2%">&nbsp;</td>
            <td colspan="4">
                <legend>Branch Passbook</legend></td>
                 <td width="2%">&nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
            </td>
            <td colspan="4">
                <div style="min-height:300px;max-height:400px;overflow:scroll;">
                    <asp:GridView ID="GvReconcil" runat="server" AutoGenerateColumns="False" 
                        BackColor="#d9d9d9" Font-Bold="false" ForeColor="#282c31" 
                        HeaderStyle-BackColor="#2063b7" HeaderStyle-ForeColor="White" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="Tran_ID" HeaderText="Tran_ID" >
                            <ItemStyle CssClass="hidden"/>
                            <HeaderStyle CssClass="hidden"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="Tran_No" HeaderText="Tran_No" >
                            <ItemStyle CssClass="hidden"/>
                            <HeaderStyle CssClass="hidden"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="Br_Code" HeaderText="Br_Code" >
                            <ItemStyle CssClass="hidden"/>
                            <HeaderStyle CssClass="hidden"/>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Reconcile" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" onclick="CheckOne(this)" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Br_Name" HeaderText="Branch" >
                            </asp:BoundField>
                             <asp:BoundField DataField="Acc_code" HeaderText="Acc_code" >
                             <ItemStyle CssClass="hidden"/>
                            <HeaderStyle CssClass="hidden"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="Acc_Name" HeaderText="Bank Name" >
                            </asp:BoundField>
                            <asp:BoundField DataField="Vr_No" HeaderText="Vou No" >
                            </asp:BoundField>
                            <asp:BoundField DataField="Credit" HeaderText="Credit" >
                            </asp:BoundField>
                            <asp:BoundField DataField="Debit" HeaderText="Debit" >
                            </asp:BoundField>
                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" >
                            </asp:BoundField>
                            <asp:BoundField DataField="Tr_Date" HeaderText="Tran Date" >
                            </asp:BoundField>
                            <asp:BoundField DataField="Recon_Status" HeaderText="Recon_Status" >
                            <ItemStyle CssClass="hidden"/>
                            <HeaderStyle CssClass="hidden"/>
                            </asp:BoundField>
                            <%--   <asp:TemplateField HeaderText="ReconcileDt">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtReconcileDt" AutoPostBack="true" OnTextChanged="TxtReconcileDt_TextChanged" runat="server" align="center" 
                                                        DataField="ReconcileDt"  
                                                        text='<%# Eval("ReconcileDt")%>' OnChange="validatedate(this);" width="75%"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
            <td width="2%">
            </td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td colspan="4">
                &nbsp;</td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="23%">
                &nbsp;</td>
            <td class="style2" width="25%">
             <asp:Label ID="LblTranCr" runat="server" ForeColor="Red" Text="Pending Credit"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="LblTranCr1" runat="server" ForeColor="Red" Text="0.00"></asp:Label>
                </td>
            <td width="25%">
             <asp:Label ID="LblTranDr" runat="server" ForeColor="Red" Text="Pending Debit"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="LblTranDr1" runat="server" ForeColor="Red" Text="0.00"></asp:Label>
                </td>
            <td width="23%">
                &nbsp;</td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="23%">
                &nbsp;</td>
            <td class="style2" width="25%">
                &nbsp;</td>
            <td width="25%">
                &nbsp;</td>
            <td width="23%">
                &nbsp;</td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="23%">
                &nbsp;</td>
            <td class="style2" width="25%">
                &nbsp;</td>
            <td width="25%">
                &nbsp;</td>
            <td width="23%">
                &nbsp;</td>
            <td width="2%">
                &nbsp;</td>
        </tr>
    </table>

</div>
   </ContentTemplate>
       <Triggers>
       <asp:PostBackTrigger ControlID="CmdShow" />
       </Triggers>
</asp:UpdatePanel> 

</asp:Content>  