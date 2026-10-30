<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BillsDll.aspx.vb" Inherits="SMARTCORE_UI.Bills_Dll" MasterPageFile="~/SMARTCORE.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<script type='text/javascript'>


    function ValidateSave() {


        var Ctl = document.getElementById('<%=TxtInstDate.ClientID%>');
        if (Ctl.value == "") {
            alert('InstDate  Not Selected');
            Ctl.focus();
            return false;
        }
        
        var Ctl = document.getElementById('<%=TxtInstDate.ClientID%>');
        if (Ctl.value == "") {
            alert('InstDate  Not Selected');
            Ctl.focus();
            return false;
        }


        var Ctl = document.getElementById('<%=TxtBillAmt.ClientID%>');
        if (Ctl.value == "") {
            alert('Amount  Not Selected');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtDate.ClientID%>');
        if (Ctl.value == "") {
            alert('Date  Not Selected');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtInstNo.ClientID%>');
        if (Ctl.value == "") {
            alert('InstNo  Not Selected');
            Ctl.focus();
            return false;
        }
        
        var Ctl = document.getElementById('<%=TxtBankBc.ClientID%>');
        if (Ctl.value == "") {
            alert('Bank BC  Not Entered');
            Ctl.focus();
            return false;
        }
        var Ctl = document.getElementById('<%=TxtBankBcDate.ClientID%>');
        if (Ctl.value == "") {
            alert('Bank BC Date Not Selected');
            Ctl.focus();
            return false;
        }        

        var Ctl = document.getElementById('<%=TxtAmount.ClientID%>');
        if (Ctl.value == "") {
            alert('Amount  Not Selected');
            Ctl.focus();
            return false;
        }
        


        return true;
    }

</script>


<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
<LINK REL=Stylesheet HREF= "CSS/MasterStyle.css" TYPE="text/css" MEDIA=screen>
<div class="gradientBoxesWithOuterShadows" width: 100%; margin-left:25%; margin-right:25%; > 
               
<br />

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
     <legend>Bills</legend>
       <table width="100%">
           <tr>
               <td width="2%">
               </td>
               <td width="15%">
               </td>
               <td width="15%">
               </td>
               <td width="2%">
               </td>
               <td width="15%">
               </td>
               <td >
               </td>
               <td width="2%">
               </td>
                <td width="15%">
               </td>
                <td width="15%">
               </td>
               <td width="2%">
               </td>
           </tr>
           <tr>
               <td width="2%">
                    </td>
               <td width="15%">
                    </td>
               <td width="15%">
                    </td>
               <td width="2%">
                    </td>
               <td colspan="4">
                   <asp:RadioButtonList ID="RdoBillType" runat="server" Width ="100%" 
                       RepeatDirection="Horizontal">
                       <asp:ListItem Selected="True" Value="O">Outward Bills</asp:ListItem>
                       <asp:ListItem Value="I">Inward Bills</asp:ListItem>
<%--                       <asp:ListItem Value="D" >Bill Discounting</asp:ListItem>--%>
                   </asp:RadioButtonList>
               </td>
               <td width="15%">
                    </td>
               <td width="2%">
                    </td>
           </tr>
           <tr>
               <td width="2%">
                    </td>
               <td width="15%">
                    </td>
               <td width="15%">
                    </td>
               <td width="2%">
                    </td>
               <td width="15%">
                    </td>
               <td  >
                    </td>
               <td width="2%">
                    </td>
               <td width="15%">
                    <asp:HiddenField ID="HdnCustName" runat="server" Visible="False" />
                    </td>
               <td width="15%">
                    </td>
               <td width="2%">
                    </td>
           </tr>
           <tr>
               <td>
                    </td>
               <td>
                   <label>
                   Bill Type</label></td>
               <td>
                   <asp:DropDownList ID="cmbBillType" runat="server" Width="100%" 
                       AutoPostBack="True">
                   </asp:DropDownList>
               </td>
               <td>
                    </td>
               <td>
                   <label>
                   Date</label></td>
               <td  >
                   <asp:TextBox ID="TxtDate" runat="server" Width="100%"></asp:TextBox>
                   <asp:CalendarExtender ID="TxtDate_CalendarExtender" runat="server"  Format="dd/MM/yyyy"
                       TargetControlID="TxtDate">
                   </asp:CalendarExtender>
               </td>
               <td>
                    </td>
               <td>
                   <label>
                   Branch</label></td>
               <td>
                   <asp:TextBox ID="TxtBranch" runat="server" AutoPostBack="True" Width="25%"></asp:TextBox>
                    <asp:DropDownList ID="CmbBranch" runat="server" Width="67%" 
                       AutoPostBack="True">
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
                   Inst Type</label></td>
               <td>
                   <asp:DropDownList ID="CmbInstType" runat="server" Width="100%">
                   </asp:DropDownList>
               </td>
               <td>
                    </td>
               <td>
                   <label>
                   Inst No</label></td>
               <td  >
                   <asp:TextBox ID="TxtInstNo" runat="server" Width="100%"></asp:TextBox>
               </td>
               <td>
                    </td>
               <td>
                   <label>
                   Scheme</label></td>
               <td>
                   <asp:TextBox ID="TxtScheme" runat="server" AutoPostBack="True" Width="25%"></asp:TextBox>
                    <asp:DropDownList ID="CmbScheme" runat="server" Width="67%" 
                       AutoPostBack="True">
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
                   Inst Date</label></td>
               <td>
                   <asp:TextBox ID="TxtInstDate" runat="server" Width="100%"></asp:TextBox>
                   <asp:CalendarExtender ID="TxtInstDate_CalendarExtender" runat="server"  Format="dd/MM/yyyy"
                       TargetControlID="TxtInstDate">
                   </asp:CalendarExtender>
               </td>
               <td>
                    </td>
               <td>
                   <label>
                   Bank Bc</label></td>
               <td  >
                   <asp:TextBox ID="TxtBankBc" runat="server" Width="100%"></asp:TextBox>
               </td>
               <td>
                    </td>
               <td>
                   <label>
                   Account No</label></td>
               <td>
                   <table width="100%">
                        <tr>
                            <td width ="20%">                     
                               <asp:TextBox ID="TxtShortCode" runat="server" AutoPostBack="True" TabIndex="4" Width="100%"  CssClass="twitterStyleTextbox"></asp:TextBox>
                            </td>
                            <td width ="2%"></td>
                            <td width ="67%">
                                <asp:TextBox ID="TxtAccNo" runat="server" AutoPostBack="True" TabIndex="4" Width="99%"  CssClass="twitterStyleTextbox"></asp:TextBox>
                            </td>
                            <td width ="2%"></td>
                            <td width ="8%" align="right">
                                    <asp:ImageButton ID="ImgSrch" runat="server" Height="24px" ImageUrl="~/images/Search.png" Width="23px" />
                            </td>
                        </tr>
                    </table>
               </td>
               <td>
                   <asp:Button ID="BtnAdd" runat="server" Text="+" />
                   </td>
           </tr>
           <tr>
               <td>
                    </td>
               <td>
                   <label>
                   Bill Amt</label></td>
               <td>
                   <asp:TextBox ID="TxtBillAmt" runat="server" Width="100%" AutoPostBack="True"></asp:TextBox>
               </td>
               <td>
                    </td>
               <td>
                   <label>
                   Bank Bc Date</label></td>
               <td  >
                   <asp:TextBox ID="TxtBankBcDate" runat="server" Width="100%"></asp:TextBox>
                   <asp:CalendarExtender ID="TxtBankBcDate_CalendarExtender" runat="server"  Format="dd/MM/yyyy"
                       TargetControlID="TxtBankBcDate">
                   </asp:CalendarExtender>
               </td>
               <td>
                    </td>
               <td>
                   <label>
                   Account Detls</label></td>
               <td>
                   <asp:TextBox ID="TxtAccDetls" runat="server" Width="100%" Rows="3" 
                       TextMode="MultiLine"></asp:TextBox>
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
               <td>
                    </td>
               <td  >
                    </td>
               <td>
                    </td>
               <td>
                   <label>
                   Amount</label></td>
               <td>
                   <asp:TextBox ID="TxtAmount" runat="server" Width="100%"></asp:TextBox>
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
               <td>
                    </td>
               <td >
                    </td>
               <td>
                    </td>
               <td>
                   <label>
                   Remarks</label></td>
               <td>
                   <asp:TextBox ID="TxtRemarks" runat="server" Width="100%"></asp:TextBox>
               </td>
               <td>
                    </td>
           </tr>
           <tr>
               <td>
                    </td>
               <td colspan="5">
               <div id ="BankDtailsDiv" runat="server" visible="false">
                   <table width="100%" >
                   
                       <tr>
                           <td width="20%">
                               <label>
                               Bank Code</label></td>
                           <td width="53%">
                               <asp:TextBox ID="TxtBankCode" runat="server" Width="100%"></asp:TextBox>
                           </td>
                           <td width ="27%">
                           </td>
                       </tr>
                       <tr>
                           <td>
                               <label>
                               Bank Name</label></td>
                           <td>
                               <asp:DropDownList ID="CmbBankName" runat="server" Width="100%">
                               </asp:DropDownList>
                           </td>
                           <td>
                           </td>
                       </tr>
                       <tr>
                           <td>
                               <label>
                               Bank Branch</label>  </td>
                           <td>
                               <asp:DropDownList ID="CmbBankBranch" runat="server" Width="100%">
                               </asp:DropDownList>
                           </td>
                           <td>
                           </td>
                       </tr>
                   </table>
                   </div>
                   </td>
               <td>
                    </td>
               <td colspan="3">
                   <asp:GridView ID="DgvBill" runat="server" AutoGenerateColumns="False" 
                       Width="100%">
                       <Columns>
                           <asp:BoundField DataField="Bill_ID" HeaderText="Bill_ID" Visible="False" />
                           <asp:BoundField DataField="TrACC_ID" HeaderText="TrACC_ID" Visible="False" />
                           <asp:BoundField DataField="Name" HeaderText="Name" Visible="True" />
                           <asp:BoundField DataField="Tran_Type" HeaderText="Tran_Type" Visible="False" />
                           <asp:BoundField DataField="Amount" HeaderText="Amount" />
                           <asp:BoundField DataField="Narration" HeaderText="Narration" />
                         <%--  <asp:BoundField DataField="Sl_No" HeaderText="Sl_No" Visible="False" />--%>
                       </Columns>
                   </asp:GridView>
               </td>
               <td>
                    </td>
           </tr>
           <tr>
               <td>
                    </td>
               <td colspan="0">
                   <label>
                   Payee Bank</label>  </td>
               <td colspan="3">
                                      <asp:TextBox ID="TxtPayeeBank" runat="server" Width="100%"></asp:TextBox>
               
               </td>
               <td>
                    </td>
               <td>
                    </td>
               <td>
                    </td>
           </tr>
           <tr>
               <td>
                    </td>
               <td colspan="0">
                   <label>
                   Payee Branch</label></td>
               <td colspan="3">
                   <asp:TextBox ID="TxtPayeeBranch" runat="server" Width="100%"></asp:TextBox>
               </td>
               <td>
                    </td>
               <td>
                    </td>
               <td>
                    </td>
           </tr>
           <tr>
               <td>
                    </td>
               <td colspan="0">
                   <label>
                   Collection Bank</label>   </td>
               <td colspan="3">
                   <asp:DropDownList ID="CmbCollnBank" runat="server" Width="100%">
                   </asp:DropDownList>
               </td>
               <td>
                    </td>
               <td>
                    </td>
               <td>
                    </td>
           </tr>
           <tr>
               <td>
                    </td>
               <td colspan="0">
                   <label>
                   Drawer</label>   </td>
               <td colspan="3">
                   <asp:TextBox ID="TxtDrawer" runat="server" Width="100%"></asp:TextBox>
               </td>
               <td>
                    </td>
               <td>
                    </td>
               <td>
                    </td>
           </tr>
           <tr>
               <td>
                    </td>
               <td colspan="0">
                   <label>
                   Bearer</label>   </td>
               <td colspan="3">
                   <asp:TextBox ID="TxtBearer" runat="server" Width="100%"></asp:TextBox>
               </td>
               <td>
                    </td>
               <td>
                    </td>
               <td>
                    </td>
           </tr>
           <tr>
               <td>
                    </td>
               <td colspan="0">
                   <label>
                   Remarks</label></td>
               <td colspan="3">
                   <asp:TextBox ID="TxtRemark" runat="server" Width="100%"></asp:TextBox>
               </td>
               <td>
                    </td>
               <td>
                    </td>
               <td>
                    </td>
           </tr>
           <tr>
               <td>
                    </td>
               <td align="center" colspan="8">
                   <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" OnClientClick="return ValidateSave();"  />
                    <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" />
                    <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />
               </td>
           </tr>
       </table>
   </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>
