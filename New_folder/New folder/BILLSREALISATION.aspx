<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="BILLSREALISATION.aspx.vb" Inherits="SMARTCORE_UI.BILLSREALISATION"     title="BILLS REALISATION" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script type='text/javascript'>


    function ValidateSave() {


        var Ctl = document.getElementById('<%=TxtCollectedAmt.ClientID%>');
        if (Ctl.value == "") {
            alertify.error('Collected Amount  Not Selected');
            Ctl.focus();
            return false;
        }
        
        return true;
    }

</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
<div class="gradientBoxesWithOuterShadows" width: 100%; margin-left:25%; margin-right:25%; > 
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<legend>BILLS REALISATION</legend>


       <table width="100%" border="1">
       
           <tr>
               <td width="34%" colspan  ="3" >
               </td>

               <td width="15%">
               <label width="100%">
                   BILL TYPE</label>
               </td>
               <td >
                   <asp:DropDownList ID="cmbBillType" runat="server" Width="100%" 
                       AutoPostBack="True">
                       <asp:ListItem Value="I">INWARD BILL</asp:ListItem>
                       <asp:ListItem Value="O">OUTWARD BILL</asp:ListItem>
                   </asp:DropDownList>
               </td>
               <td width="34%"  colspan  ="3" >
               </td>

           </tr>
           
           
           </table>
<div id ="DivBillDetails" runat="server" visible="false">
<%--       <table width="100%" >
       
           <tr>
               <td width="34%" colspan  ="3" >
               </td>

               <td width="15%">
                   <label width="100%">    SELECT BILL NO</label>
               </td>
               <td >
                   <asp:DropDownList ID="CmbBillNo" runat="server" Width="100%" 
                       AutoPostBack="True">
                       <asp:ListItem Value="I">INWARD BILL</asp:ListItem>
                       <asp:ListItem Value="O">OUTWARD BILL</asp:ListItem>
                   </asp:DropDownList>
               </td>
               <td width="34%"  colspan  ="3" >
               </td>

           </tr>
           
           
           </table>--%>
           
       <table width="100%">
           <tr>
               <td width="3%">
                   &nbsp;</td>
               <td width="15%">
                                   &nbsp;</td>
               <td width="15%">
               </td>
               <td width="2%">
                   &nbsp;</td>
               <td width="15%">
                   &nbsp;</td>
               <td width="15%">
                   &nbsp;</td>
               <td width="2%">
                   &nbsp;</td>
                <td width="15%">
               </td>
                <td width="15%">
               </td>
               <td width="3%">
               </td>
           </tr>    
           <tr>
               <td width="2%">
                    </td>
               <td width="15%">
                                  <label width="100%">    SELECT BILL NO</label>
               </td>
               <td width="15%">
                                  <asp:DropDownList ID="CmbBillNo" runat="server" Width="100%" 
                       AutoPostBack="True"></asp:DropDownList> 
                                  
                                  <asp:Panel ID="Panel1" runat="server">
                                      <asp:TextBox ID="TxtBillNo" runat="server"></asp:TextBox>
                                  </asp:Panel>
                                  
                    </td>
               <td width="2%">
                    </td>
               <td width="15%">
                   <label width="100%">    BANK BC</label></td>
               <td >
                   <asp:TextBox ID="TxtBankBc" runat="server" Width="100%"></asp:TextBox>
               </td>
               <td width="2%">
                    </td>
                <td width="15%">
                    <label width="100%">    BILL STATUS</label></td>
                <td width="15%">
                   <asp:DropDownList ID="cmbStatus" runat="server" Width="100%" 
                       AutoPostBack="True">
                       <asp:ListItem Value="RETURNED">RETURNED</asp:ListItem>
                       <asp:ListItem Value="REALISED">REALISED</asp:ListItem>
                   </asp:DropDownList>
               </td>
               <td width="2%">
                    </td>
           </tr>    
           <tr>
               <td width="2%">
                    </td>
               <td width="15%">
                   <label width="100%">    INSTRUMENT TYPE</label></td>
               <td width="15%">
                   <asp:DropDownList ID="CmbInstType" runat="server" Width="100%">
                   </asp:DropDownList>
               </td>
               <td width="2%">
                    </td>
               <td width="15%">
                   <label width="100%">    BANK BC DATE</label></td>
               <td >
                   <asp:TextBox ID="TxtBankBcDate" runat="server" Width="100%"></asp:TextBox>
                   <asp:CalendarExtender ID="TxtBankBcDate_CalendarExtender" runat="server"  Format="dd/MM/yyyy"
                       TargetControlID="TxtBankBcDate">
                   </asp:CalendarExtender>
               </td>
               <td width="2%">
                    </td>
                <td width="15%">
                    <label width="100%">    COLL.REMARKS</label></td>
                <td width="15%">
                   <asp:TextBox ID="TxtRemarkStatus" runat="server" Width="100%"></asp:TextBox>
               </td>
               <td width="2%">
                    </td>
           </tr>    
           <tr>
               <td width="2%">
                    </td>
               <td width="15%">
                   <label width="100%">    INSTRUMENT  NO</label></td>
               <td width="15%">
                   <asp:TextBox ID="TxtInstNo" runat="server" Width="100%"></asp:TextBox>
               </td>
               <td width="2%">
                    </td>
               <td width="15%">
                   <label width="100%">    BILL AMOUNT</label></td>
               <td >
                   <asp:TextBox ID="TxtBillAmt" runat="server" Width="100%" AutoPostBack="True"></asp:TextBox>
               </td>
               <td width="2%">
                    </td>
                <td colspan=2 rowspan="6">
             <div id="DivCollDtails" runat ="server" >
      
                <table width="100%">
                <tr>
                <td width="50%">
                <label width="100%">    BILL COLLECTED AMT</label>
                </td>
                <td width="50%">
                 <asp:TextBox ID="TxtCollectedAmt" runat="server" Width="100%" AutoPostBack="True"></asp:TextBox>
                </td>
                
                </tr>
                <tr>
                <td>
                
                    <label width="100%">    COMMISSION AMT</label></td>
                <td>
                
                   <asp:TextBox ID="TxtCommAmt" runat="server" Width="100%" AutoPostBack="True"></asp:TextBox>
                
                </td>
                </tr>
                
                <tr>
                <td>
                
                    <label width="100%" visible="false">    OTHER BANK COMM</label></td>
                <td>
                
                   <asp:TextBox ID="TxtOtherbankCommAmt" runat="server" Width="100%" 
                        Visible="False" AutoPostBack="True"></asp:TextBox>
                
                </td>
                </tr>
                <tr>
                <td>
                
                <label width="100%">    POSTAGE</label></td>
                <td>
                
                   <asp:TextBox ID="TxtPostageAmt" runat="server" Width="100%" AutoPostBack="True"></asp:TextBox>
                
                </td>
                </tr>
                
                <tr>
                <td>
                
                <%--<label width="100%" visible="false">    SERVICE TAX    </label></td>--%>
                <td>
                
                   <asp:TextBox ID="TxtServiceTaxAmt" runat="server" Width="100%" Visible="False"></asp:TextBox>
                
                </td>
                </tr>
                </table>     
    </div>
                    </td>
                
               <td width="2%">
                    </td>
           </tr>    
           <tr>
               <td width="2%">
                    </td>
               <td width="15%">
                   <label width="100%">    INSTRUMENT DATE</label></td>
               <td width="15%">
                   <asp:TextBox ID="TxtInstDate" runat="server" Width="100%"></asp:TextBox>
                   <asp:CalendarExtender ID="TxtInstDate_CalendarExtender" runat="server"  Format="dd/MM/yyyy"
                       TargetControlID="TxtInstDate">
                   </asp:CalendarExtender>
               </td>
               <td width="2%"></td>
               <td width="15%">
                   <label width="100%">    COLLECTION BANK</label></td>
               <td width="15%">
                   <asp:DropDownList ID="CmbCollnBank" runat="server" Width="100%">
                   </asp:DropDownList>
               </td>   
                <td width="2%"></td>
                <td >
                 </td>
               
           </tr>    
           <tr>
               <td width="2%">
                    </td>
               <td width="15%">
                   <label width="100%">    DRAWER</label></td>
               <td width="15%">
                   <asp:TextBox ID="TxtDrawer" runat="server" Width="100%"></asp:TextBox>
               </td>
               <td width="2%">
                    </td>
               <td width="15%">
                   <label width="100%">    BEARER</label></td>
               <td width="15%">
                   <asp:TextBox ID="TxtBearer" runat="server" Width="100%"></asp:TextBox>
               </td>

               <td width="2%">
                    </td>
               <td width="2%">
                    </td>
           </tr>    
           <tr>
               <td width="2%">
                    </td>
               <td width="15%">
                   <label width="100%">    PAYEE BANK</label></td>
               <td width="15%">
                                      <asp:TextBox ID="TxtPayeeBank" runat="server" Width="100%"></asp:TextBox>
               
               </td>
               <td width="2%">
                    </td>
               <td width="15%">
                   <label width="100%">    PAYEE BRANCH</label></td>
               <td width="15%">
                   <asp:TextBox ID="TxtPayeeBranch" runat="server" Width="100%"></asp:TextBox>
               </td>

               <td width="2%">
                    </td>
                <td width="2%"></td>
           </tr>    
           <tr>
               <td width="2%">
                    </td>
               <td width="15%">
                   <label width="100%">    BILL SEND DATE</label></td>
               <td >
                   <asp:TextBox ID="TxtBillSendDate" runat="server" Width="100%"></asp:TextBox>
                   <asp:CalendarExtender ID="TxtBillSendDate_CalendarExtender" runat="server"  Format="dd/MM/yyyy"
                       TargetControlID="TxtBillSendDate">
                   </asp:CalendarExtender>
                                        </td>
               <td></td>
               <td width="15%">
                   <label width="100%">    REMARKS</label></td>
               <td width="15%">
                   <asp:TextBox ID="TxtRemark" runat="server" Width="100%"></asp:TextBox>
               </td>

               <td width="2%">
                    </td>
               <td width="2%">
                    </td>
           </tr>    
   
           
           </table>    
           
<table width="100%">
<tr>
<td width="3%">
</td>

<td >
    <asp:GridView ID="DgvBill" runat="server" AutoGenerateColumns="False" CssClass="grid-view" 
           Width="100%" AllowPaging="True" PageSize="3">
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
       <br />
    <asp:GridView ID="DgvTransaction" runat="server" AutoGenerateColumns="False" 
        TabIndex="32" Width="100%">
        <Columns>
            <asp:BoundField DataField="AccNo" HeaderText="AccNo" Visible="False" />
            <asp:BoundField DataField="AccCode" HeaderText="AccCode" Visible="False">
                <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                    BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                    Font-Size="Medium" />
            </asp:BoundField>
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
<td width="3%">
</td>
</tr>
</table>
<table width="100%">
        <tr>
            <td width="30%">
            </td>
            <td width="40%">
                <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                    meta:resourcekey="CmdSaveResource1" TabIndex="21"  
                    OnClientClick="return ValidateSave();"/>
                <asp:ImageButton ID="CmdClear0" runat="server" ImageUrl="~/Images/Clear.png" 
                    meta:resourcekey="CmdSaveResource1" TabIndex="22" />
                <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/images/Close.png" 
                    meta:resourcekey="CmdClearResource1" TabIndex="23" />
            </td>
            <td width="30%">
                &nbsp;</td>
        </tr>
    </table>
</td>
<td width="3%">
    &nbsp;</td>
    </tr>
    <tr>
<td width="3%">
    &nbsp;</td>
<td width="15%">
    &nbsp;</td> 
<td width="79%">

    &nbsp;</td>
<td width="3%">
    &nbsp;</td>
    </tr>
</table>    
           
           

        
</div>
</div> 

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
</asp:UpdatePanel> 
</asp:Content>
