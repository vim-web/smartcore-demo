<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="Entry_change.aspx.vb" Inherits="SMARTCORE_UI.Entry_change" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />   
    <script type='text/javascript' src="Scripts/Common.js"></script>  
    <script type='text/javascript' src="Scripts/jquery-1.8.3.js"></script>  
    <link href="CSS/ui-lightness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript' src="Scripts/jquery-ui-1.9.2.custom.min.js"></script> 
    <style type="text/css">
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
   <legend>Voucher Entry modification</legend>
    <div class="gradientBoxesWithOuterShadows" style="width: 70%; margin-left:15%;  margin-right:15%;  float:left; " >
        

        <table style="width:100%;">
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="LblBr" runat="server" ForeColor="#009933" Text="Branch"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DrpBr" runat="server" Width="187px" AutoPostBack = "true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="LblVr" runat="server" ForeColor="#009933" Text="Vou.No"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TxtVrno" runat="server" Width="183px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="LblTrDate" runat="server" ForeColor="#009933" 
                        Text="TransactionDate"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TxtTrdate" runat="server" Width="183px" ></asp:TextBox>
                    <asp:CalendarExtender ID="CalenderExtender" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="TxtTrdate"> </asp:CalendarExtender>  
                    <asp:ImageButton ID="CmdAdd" runat="server" Height="21px" 
                            ImageUrl="~/Images/Show.png" TabIndex="25" />
                </td>
            </tr>
            <tr>
                <td width="2%">
                  &nbsp;</td>
              <td colspan="3" style="width: 47%">
                  <asp:GridView ID="GvAccountDetails"  CssClass="grid-viewNew" AutoGenerateColumns="False"   Width="100%" runat="server" >
                   <Columns>
                <asp:BoundField DataField="Cust_Name" HeaderText="Name" >
                 </asp:BoundField> 
                <asp:BoundField DataField="ACC_No" HeaderText="AccountNo" >
                 </asp:BoundField> 
                <asp:BoundField DataField="Tr_Date" HeaderText="TransferDate" >
                 </asp:BoundField> 
                <asp:BoundField DataField="Vr_No" HeaderText="VoucherNo" >
                 </asp:BoundField> 
                <asp:BoundField DataField="Tran_Type" HeaderText="TranType" >                  
                 </asp:BoundField> 
                 <asp:BoundField DataField="Cash" HeaderText="Cash" >                  
                 </asp:BoundField> 
                 <asp:BoundField DataField="Transfer" HeaderText="Transfer" >                  
                 </asp:BoundField> 
                 <asp:BoundField DataField="Tran_id" HeaderText="TransactionId" >                  
                 </asp:BoundField> 
                  <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            </Columns>
                  </asp:GridView>
              </td>
              <td width="2%">
                  &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="LblChange" runat="server" ForeColor="#009933" Text="Change To"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DrpMode" runat="server" Width="187px" AutoPostBack = "True">
                     <asp:ListItem Text="Cash" Value="0" />
                     <asp:ListItem Text="Transfer" Value="1" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td colspan="3" style="width: 45%" width="30%">
                    <table class="style4" width="100%">
                        <tr>
                            <td width="30%">
                                &nbsp;</td>
                            <td width="70%">
                                <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                                    meta:resourcekey="CmdSaveResource1"
                                    TabIndex="29" />
                                <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/images/Close.png" 
                                    meta:resourcekey="CmdClearResource1" TabIndex="30" />
                                <asp:ImageButton ID="CmdClear0" runat="server" ImageUrl="~/Images/Clear.png" 
                                    meta:resourcekey="CmdSaveResource1" TabIndex="36" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            </tr>
        </table>
        

        </div>
    </ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>

