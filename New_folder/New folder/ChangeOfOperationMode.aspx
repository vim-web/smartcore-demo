<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="ChangeOfOperationMode.aspx.vb" Inherits="SMARTCORE_UI.ChangeOfOerationMode" %>
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
     <div class="gradientBoxesWithOuterShadows" style="width: 80%; margin-left:10%; margin-right:10%;">
          <legend>Change Operation Mode</legend>
          <table width="100%">

          <tr>
          <td width="2%">&nbsp;</td>
          <td width="47%">
              &nbsp;</td>
         
          <td width="2%">&nbsp;</td>
          <td width="47%">
             </td>
              <td width="2%">
                  &nbsp;</td>
              <tr>
                  <td width="2%">
                  </td>
                  <td width="47%">
                    <table width="100%">
                    <tr>
                    <td width="33%">
                    <label>
                    Branch</label></td>
                    <td width="67%">
                    <asp:TextBox ID="TxtBranch" runat="server" AutoPostBack="True" Width="25%"></asp:TextBox>
                    &nbsp;<asp:DropDownList ID="CmbBranch" runat="server" AutoPostBack="True" 
                    Width="68%">
                    </asp:DropDownList>
                    </td>
                    </tr>
                    </table>
                      <table width="100%">
                          <tr>
                              <td width="33%">
                                  <label>
                                  Effective Date</label></td>
                              <td width="33%">
                                  <asp:TextBox ID="TxtEffDate" runat="server" Width="100%"></asp:TextBox>
                                  <asp:CalendarExtender ID="TxtEffDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                                      TargetControlID="TxtEffDate">
                                  </asp:CalendarExtender>
                              </td>
                              <td width="33%">
                              </td>
                          </tr>
                          <tr>
                              <td colspan="3">
                                  <label style="text-decoration: underline">
                                  Present Mode Of Operation</label></td>
                          </tr>
                          <tr>
                              <td>
                                  <label>
                                  Scheme</label></td>
                              <td colspan="2">
                                  <asp:TextBox ID="TxtScheme" runat="server" AutoPostBack="True" Width="25%"></asp:TextBox>
                                  &nbsp;<asp:DropDownList ID="CmbScheme" runat="server" AutoPostBack="True" 
                                      Width="65%">
                                  </asp:DropDownList>
                              </td>
                          </tr>
                          <tr>
                              <td>
                                  <label>
                                  Account No</label></td>
                              <td colspan="2">
                                  <asp:TextBox ID="TxtSchCode" runat="server" AutoPostBack="True" Width="25%"></asp:TextBox>
                                  &nbsp;<asp:TextBox ID="TxtAccNo" runat="server" AutoPostBack="True" Width="60%"></asp:TextBox></td>
                          </tr>
                          <tr>
                              <td>
                                  <label>
                                  Operation Mode</label></td>
                              <td colspan="2">
                                  <asp:DropDownList ID="CmbOprnMode" runat="server" Width="100%">
                                  </asp:DropDownList>
                              </td>
                          </tr>
                          <tr>
                              <td>
                                  <label>
                                  Customer No</label></td>
                              <td colspan="2">
                                  <asp:TextBox ID="TxtCustNo" runat="server" Width="100%"></asp:TextBox>
                              </td>
                          </tr>
                          <tr>
                              <td>
                                  <label>
                                  Name</label></td>
                              <td colspan="2">
                                  <asp:TextBox ID="TxtName" runat="server" Width="100%"></asp:TextBox>
                              </td>
                          </tr>
                          <tr>
                              <td>
                                  <label>
                                  Address</label></td>
                              <td>
                                  <asp:TextBox ID="TxtAddress" runat="server" TextMode="MultiLine" Width="200%"></asp:TextBox>
                              </td>
                          </tr>
                          <tr>
                              <td class="style1" colspan="3">
                                  <asp:GridView ID="DgvPresentMode" runat="server" Width="100%" CssClass="grid-view"
                                      AutoGenerateColumns="False">
                                      <Columns>
                                          <asp:BoundField DataField="Cust_Name" HeaderText="Customer Name" />
                                          <asp:BoundField DataField="Cust_Id" HeaderText="Customer Id" />
                                      </Columns>
                                  </asp:GridView>
                              </td>
                          </tr>
                          <tr>
                              <td class="style1" colspan="3">
                                  &nbsp;</td>
                          </tr>
                          <tr>
                              <td class="style1" colspan="3">
                                  &nbsp;</td>
                          </tr>
                          <tr>
                              <td colspan="3">
                                  <asp:CheckBox ID="CheckBox1" runat="server" Text="Operative Customer" />
                                  &nbsp;
                                  <asp:CheckBox ID="CheckBox2" runat="server" Text="Non Operative Customer" />
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
                      </table>
                  </td>
                  <td width="2%">
                  </td>
                  <td width="47%">
                      <div class="gradientBoxesWithOuterShadows">
                          <table width="100%">
                              <tr>
                                  <td colspan="2">
                                      <label style="text-decoration: underline">
                                      New Mode Of Operation</label></td>
                              </tr>
                              <tr>
                                  <td>
                                      <label>
                                      Scheme</label></td>
                                  <td>
                                      <asp:TextBox ID="TxtNewScheme" runat="server" AutoPostBack="True" Width="25%"></asp:TextBox>
                                      &nbsp;<asp:DropDownList ID="CmbNewScheme" runat="server" AutoPostBack="True" 
                                          Width="68%">
                                      </asp:DropDownList>
                                  </td>
                              </tr>
                              <tr>
                                  <td>
                                      <label>
                                      Account No</label></td>
                                  <td>
                                      <asp:TextBox ID="TxtNewSchCode" runat="server" AutoPostBack="True" Width="25%"></asp:TextBox>
                                      &nbsp;<asp:TextBox ID="TxtNewAccNo" runat="server" AutoPostBack="True" Width="66%"></asp:TextBox></td>
                              </tr>
                              <tr>
                                  <td>
                                      <label>
                                      Operation Mode</label></td>
                                  <td>
                                      <asp:DropDownList ID="CmbNewOprnMode" runat="server" Width="95%">
                                      </asp:DropDownList>
                                  </td>
                              </tr>
                              <tr>
                                  <td>
                                      <label>
                                      No Of Operators</label></td>
                                  <td>
                                      <asp:TextBox ID="TxtNoOfCustomer" runat="server" Width="95%"></asp:TextBox>
                                  </td>
                              </tr>
                              <tr>
                                  <td>
                                      <label>
                                      Customer No</label></td>
                                  <td>
                                      <asp:TextBox ID="TxtNewCustNo" runat="server" Width="70%" AutoPostBack="True"></asp:TextBox>
                                      &nbsp;<asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
                                          ImageUrl="~/images/Search.png" Width="23px" />
                                  </td>
                              </tr>
                              <tr>
                                  <td>
                                      <label>
                                      Name</label></td>
                                  <td>
                                      <asp:TextBox ID="TxtNewName" runat="server" Width="95%"></asp:TextBox>
                                  </td>
                              </tr>
                              <tr>
                                  <td>
                                      <label>
                                      Address</label></td>
                                  <td>
                                      <asp:TextBox ID="TxtNewAddress" runat="server" TextMode="MultiLine" 
                                          Width="95%"></asp:TextBox>
                                  </td>
                              </tr>
                              <tr>
                                  <td>
                                      <label>Relation Type</label></td>
                                  <td>
                                      <asp:DropDownList ID="CmbRelntype" runat="server" Width="95%">
                                      </asp:DropDownList>
                                  </td>
                              </tr>
                              <tr>
                                  <td align="left" colspan="2">
                                      &nbsp;
                                      <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                                          <asp:ListItem>Main</asp:ListItem>
                                          <asp:ListItem>Authorised Operator</asp:ListItem>
                                      </asp:CheckBoxList>
                                  </td>
                              </tr>
                              <tr>
                                  <td>
                                      &nbsp;</td>
                                  <td align="right">
                                      &nbsp;
                                      <asp:ImageButton ID="CmdAdd" runat="server" ImageUrl="~/Images/AddBtn.png" />
                                  </td>
                              </tr>
                              <tr>
                                  <td colspan="2">
                                      <asp:GridView ID="DgvNewMode" runat="server" Width="100%" CssClass="grid-view"
                                          AutoGenerateColumns="False">
                                          <Columns>
                                              <asp:BoundField DataField="CustName" HeaderText="Customer Name" />
                                              <asp:BoundField DataField="CID" HeaderText="Customer Id" />
                                              <asp:BoundField DataField="Relation" HeaderText="Relation" Visible="False" />
                                              <asp:BoundField DataField="AuthOpratr" HeaderText="Authorised Operator" />
                                              <asp:BoundField DataField="RelnType" HeaderText="Relation Type" 
                                                  Visible="False" />
                                              <asp:CommandField ShowDeleteButton="True" />
                                          </Columns>
                                      </asp:GridView>
                                  </td>
                              </tr>
                              <tr>
                                  <td colspan="2">
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  <td align="center" colspan="2">
                                      &nbsp;&nbsp;</td>
                              </tr>
                          </table>
                      </div>
                  </td>
                  <td>
                  </td>
              </tr>
          </div>
         </tr>          
          <tr>
              <td width="2%">
                  </td>
              <tr>
                  <td width="2%">
                      &nbsp;</td>
                  <td align="center" colspan="3" style="width: 94%">
                      <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/Images/Save.png" />
                      <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" />
                      <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />
                  </td>
                  <td width="2%">
                      &nbsp;</td>
              </tr>
              <tr>
                  <td width="2%">
                      &nbsp;</td>
                  <td align="center" colspan="3" style="width: 94%">
                      <asp:HiddenField ID="HdnAccId" runat="server" /></td>
                  <td width="2%">
                      &nbsp;</td>
              </tr>
              </table>
              </div>
    </tr>
   </ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>

