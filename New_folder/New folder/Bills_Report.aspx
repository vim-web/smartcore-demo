<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Bills_Report.aspx.vb" Inherits="SMARTCORE_UI.Bills_Report"  MasterPageFile="~/SMARTCORE.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
<script type='text/javascript' src="Scripts/Common.js"></script> 
<script type='text/javascript' src="Scripts/jquery-1.8.3.js"></script>  

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="gradientBoxesWithOuterShadows" style="width: 44%; margin-left:28%; margin-right:28%;">
        <legend>Bills Reports</legend>
        <table width="100%">
            <tr>
                <td width="2%"></td>
                <td width="40%"><label>Branch</label></td>
                <td width="76%">
                    <asp:DropDownList ID="CmbBranch" runat="server" Width="100%"></asp:DropDownList>
                </td>
                <td width="2%"></td>
            </tr>
            <tr>
                <td width="2%"></td>
                <td width="40%"><label>Report Name</label></td>
                <td width="76%">
                    <asp:DropDownList ID="CmbRptName" runat="server" Width="100%" AutoPostBack="True"></asp:DropDownList>
                </td>
                <td width="2%"></td>
            </tr>
            <tr>
                <td width="2%"></td>
                <td width="40%"><label>Bill Type</label></td>
                <td width="76%">
                    <asp:DropDownList ID="CmbBillType" runat="server" Width="100%"></asp:DropDownList>
                </td>
                <td width="2%"></td>
            </tr>
            <tr>
                <td width="2%"></td>
                <td width="40%"><label>From Date</label></td>
                <td width="76%">
                    <asp:TextBox ID="TxtFrDate" runat="server" Width="100%"></asp:TextBox>
                    <asp:CalendarExtender ID="TxtIntimationDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                      TargetControlID="TxtFrDate">
                    </asp:CalendarExtender>
                </td>
                <td width="2%"></td>
            </tr>
            <tr>
                <td width="2%"></td>
                <td width="40%"><label>To Date</label></td>
                <td width="76%">
                    <asp:TextBox ID="TxtToDate" runat="server" Width="100%"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                      TargetControlID="TxtToDate">
                    </asp:CalendarExtender>
                </td>
                <td width="2%"></td>
            </tr>
            <tr>
                <td width="2%"></td>
                <td width="40%"><label>Bank Name</label></td>
                <td width="76%"><asp:DropDownList ID="CmbBankName" runat="server" Width="100%" 
                        AutoPostBack="True"></asp:DropDownList></td>
                <td width="2%"></td>
            </tr>
            <tr>
                <td width="2%"> </td>
                <td width="40%">
                    &nbsp;</td>
                <td width="76%">
                    &nbsp;</td>
                <td width="2%"> </td>
            </tr>
            <tr>
                <td width="2%">
                     </td>
                <td colspan="2" >
                     
                     
                                          
                     <asp:GridView ID="DgvBills" runat="server" AutoGenerateColumns="False" 
                         CellPadding="2" DataKeyNames="Bill_ID" ForeColor="#333333" Width ="100%" 
                         AllowSorting="True">
                         <Columns>
                             <asp:TemplateField HeaderText="CheckAll">
                                 <HeaderTemplate>
                                     <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="true" 
                                         OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                 </HeaderTemplate>
                                 <ItemTemplate>
                                     <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" 
                                         OnCheckedChanged="chkSelect_CheckedChanged" />
                                 </ItemTemplate>
                             </asp:TemplateField>
                             <asp:BoundField DataField="Bill_No" HeaderText="Bill No" 
                                 SortExpression="Bill_No" Visible="false" />
                             <asp:TemplateField HeaderText="Acc No" SortExpression="Bill_No">
                                 <ItemTemplate>
                                     <asp:TextBox ID="Txtacc_No" runat="server" BorderStyle="none" ForeColor="Blue" 
                                         ReadOnly="true" Text='<%# Bind("Bill_No") %>'>
                        </asp:TextBox>
                                 </ItemTemplate>
                             </asp:TemplateField>
                             <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                 <ItemTemplate>
                                     <asp:TextBox ID="TxtCust_Name" runat="server" BorderStyle="none" 
                                         BorderWidth="0px" ForeColor="Blue" ReadOnly="true" 
                                         Text='<%# Bind("cust_Name") %>'>
                        </asp:TextBox>
                                 </ItemTemplate>
                             </asp:TemplateField>
                             <asp:TemplateField HeaderText="Bill Amount" SortExpression="Bill Amount">
                                 <ItemTemplate>
                                     <asp:TextBox ID="TxtOut" runat="server" BorderStyle="none" ForeColor="Blue" 
                                         ReadOnly="true" Text='<%# Bind("Bill_Amount") %>'>
                        </asp:TextBox>
                                 </ItemTemplate>
                             </asp:TemplateField>
                             <asp:TemplateField HeaderText="Bill ID" SortExpression="Bill Id" Visible ="false"  >
                                 <ItemTemplate >
                                     <asp:TextBox ID="TxtBillId" runat="server" BorderStyle="none" ForeColor="Blue" 
                                         ReadOnly="true" Text='<%# Bind("Bill_ID") %>'>
                        </asp:TextBox>
                                 </ItemTemplate>
                             </asp:TemplateField>

                             
                         </Columns>
                     </asp:GridView>
                     
                     
                                          
                     </td>
                <td width="2%">
                     </td>
            </tr>
            <tr>
                <td width="2%">
                </td>
                <td width="40%">
                    <Label>Output To</Label>
                </td>
                <td width="76%">
                    <asp:DropDownList ID="CmbOutput" runat="server" Width="100%">
                    </asp:DropDownList>
                </td>
                <td width="2%">
                </td>
            </tr>
            <tr>
                <td width="2%"></td>
                <td colspan="2" align="center">
                
                    <asp:ImageButton ID="CmdShow" runat="server" ImageUrl="~/Images/Show.png" 
                        OnClientClick="return ValidateSave();" Width="96px"/>
                    <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />
                
                </td>
                <td width="2%"></td>
            </tr>
            <tr>
                <td width="2%"></td>
                <td width="40%"></td>
                <td width="76%"></td>
                <td width="2%"></td>
            </tr>
        </table>
    </div>
    </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>
     
     
