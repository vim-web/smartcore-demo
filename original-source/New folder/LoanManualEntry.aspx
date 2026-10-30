<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="LoanManualEntry.aspx.vb" Inherits="SMARTCORE_UI.LoanManualEntry" 
    title="Collection Entry Form" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    

        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:15%; margin-right:15%;">
          <legend>Collection Entry Form</legend>
              <table width="100%">
              <tr>
              <td width="2%"></td>
              <td width="48%"><label>Branch</label></td>
              <td width="48%">
                  <asp:DropDownList ID="CmbBranch" runat="server" Width="100%" 
                      AutoPostBack="True">
                  </asp:DropDownList>
                  </td>
              <td width="2%"></td>
              </tr>
                  <tr>
                      <td width="2%">
                           </td>
                      <td width="48%"><label>Scheme</label>
                           </td>
                      <td width="48%">
                          <asp:TextBox ID="TxtScheme" runat="server" Width="25%"></asp:TextBox>
                           <asp:DropDownList ID="CmbScheme" runat="server" Width="70%" 
                              AutoPostBack="True">
                          </asp:DropDownList>
                      </td>
                      <td width="2%">
                           </td>
                  </tr>
                  <tr>
                      <td width="2%">
                           </td>
                      <td width="48%">
                          <label>Sangam Details</label></td>
                      <td width="48%">
                          <asp:DropDownList ID="CmbSangam" runat="server" Width="100%">
                          </asp:DropDownList>
                      </td>
                      <td width="2%">
                           </td>
                  </tr>
                  <tr>
                      <td width="2%">
                           </td>
                      <td width="48%">
                          <label>
                          Amount</label></td>
                      <td width="100%">
                          <asp:TextBox ID="TxtAmt" runat="server" Width="75%"></asp:TextBox>
                          <asp:ImageButton ID="CmdShow" runat="server" ImageUrl="~/Images/Show.png" />
                      </td>
                      <td width="2%">
                           </td>
                  </tr>
                  <tr>
                      <td width="2%">
                           </td>
                      <td align="center" colspan="2" style="width: 96%" width="48%">
                           </td>
                      <td width="2%">
                           </td>
                  </tr>
                  <tr>
                      <td width="2%">
                           </td>
                      <td align="center" colspan="2" style="width: 96%" width="48%">
                      
<asp:GridView ID="DgvSangamDetails" runat="server" AutoGenerateColumns="False" 
                              CellPadding="2" DataKeyNames="Acc_No" ForeColor="#333333">
                              <Columns>
<%--                                  <asp:TemplateField HeaderText="CheckAll">
                                      <HeaderTemplate>
                                          <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="true" 
                                              OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                      </HeaderTemplate>
                                      <ItemTemplate>
                                          <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" 
                                              OnCheckedChanged="chkSelect_CheckedChanged" />
                                      </ItemTemplate>
                                  </asp:TemplateField>--%>
                                  <asp:BoundField DataField="Acc_No" HeaderText="Account No" 
                                      SortExpression="Acc_No" Visible="false" />
                                  <asp:TemplateField HeaderText="Acc No" SortExpression="Outstanding">
                                      <ItemTemplate>
                                          <asp:TextBox ID="Txtacc_No" runat="server" BorderStyle="none" ForeColor="Blue" 
                                              ReadOnly="true" Text='<%# Bind("Acc_No") %>' Width ='90%'>
                        </asp:TextBox>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                      <ItemTemplate>
                                          <asp:TextBox ID="TxtCust_Name" runat="server" BorderStyle="none" 
                                              BorderWidth="0px" ForeColor="Blue" ReadOnly="true" 
                                              Text='<%# Bind("cust_Name") %>' Width ='90%'>
                        </asp:TextBox>
                                      </ItemTemplate>
                                  </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Amount" SortExpression="Amount">
                                      <ItemTemplate>
                                          <asp:TextBox ID="TxtAmount" runat="server" BorderStyle="none" ForeColor="Blue" 
                                              Text='<%# Bind("Amount") %>' AutoPostBack ="true" Width ='90%'>
                        </asp:TextBox>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="ACC_ID" SortExpression="ACC_ID" Visible ="false" >
                                      <ItemTemplate>
                                          <asp:TextBox ID="TxtAccID" runat="server" BorderStyle="none" ForeColor="Blue" 
                                              ReadOnly="true" Text='<%# Bind("ACC_ID") %>' Visible ="false" >
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
                           <Div id ="Divtotal" runat ="server"  visible ="false" >
                      <td align="center" colspan="2" >
                      <asp:Label ID ="Label1total" runat ="server" 
                              Text =" Total Collected Amount     : "  Font-Bold ="true" Font-Size ="Large"  ></asp:Label>
                      <asp:Label ID ="LblTotal" runat ="server" Font-Bold ="true" Font-Size ="Large"  > </asp:Label>
                      </Div>
                           </td>
                      <td width="2%">
                           </td>
                  </tr>
                  <tr>
                      <td width="2%">
                           </td>
                      <td align="center" colspan="2" style="width: 96%" width="48%">
                      <table width ="100%">
                      <tr>
                      <td width ="60%">
                      </td>

                      <td width ="20%">
                          <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                              meta:resourcekey="CmdSaveResource1" onclientclick="return Save();" 
                              TabIndex="34" />
                      </td>
                      
                      <td width ="20%">
                          <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/images/Close.png" 
                              meta:resourcekey="CmdClearResource1" TabIndex="35" />
                      </td>
                      </tr>
                      </table>
                      
                      
                      </td>
                      <td width="2%">
                           </td>
                  </tr>
                  <tr>
                      <td width="2%">
                           </td>
                      <td align="center" colspan="2" style="width: 96%" width="48%">
                           </td>
                      <td width="2%">
                           </td>
                  </tr>
              </table>
          </legend>
              </div>
    </tr>
   </ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>