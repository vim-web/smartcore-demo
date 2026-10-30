<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="LoanActionTaken.aspx.vb" Inherits="SMARTCORE_UI.LoanActionTaken" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <style type="text/css">
        .style1
        {
            width: 24%;
        }
    </style>
   
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    &nbsp;&nbsp;

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
<script type='text/javascript' src="Scripts/Common.js"></script>
 
        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                        <Scripts>
                            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
                        </Scripts>
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 80%; margin-left:10%; margin-right:10%;">
          <legend>Loan Action Taken</legend>
          <table width="100%">
          <tr>
          <td width="2%"></td>
          <td width="18%"><label>Branch</label></td>
          <td width="10%">
              <asp:TextBox ID="TxtBranch" runat="server" Width="97%" AutoPostBack="True" 
                  Height="21px"></asp:TextBox>
           </td>
           <td class="style1">
              &nbsp;<asp:DropDownList ID="CmbBranch" runat="server" Width="95%" 
                  AutoPostBack="True">
              </asp:DropDownList>
              </td>
          <td width="2%"></td>
          <td width="18%">
              <label>
              Customer Name</label></td>
          <td width="24%">
              <asp:TextBox ID="TxtCustName" runat="server" Width="100%" TabIndex="500"></asp:TextBox>
              </td>
          <td width="2%"></td>
          </tr>
              <tr>
                  <td>
                      </td>
                  <td>
                      <label>Scheme</label></td>
                  <td>
                      <asp:TextBox ID="TxtScheme" runat="server" Width="100%" AutoPostBack="True" 
                          TabIndex="1"></asp:TextBox>
                  </td>
                  <td class="style1">
                      &nbsp;<asp:DropDownList ID="CmbScheme" runat="server" Width="95%" 
                          AutoPostBack="True" TabIndex="1">
                      </asp:DropDownList>
                      </td>
                  <td>
                      </td>
                  <td colspan="2" rowspan="3">
                      <label>
                      Address<asp:TextBox ID="TxtAddress" runat="server" Height="60px" 
                          TextMode="MultiLine" Width="100%" TabIndex="500"></asp:TextBox></label></td>
                  <td>
                      </td>
              </tr>
              <tr>
                  <td>
                      &nbsp;</td>
                  <td>
                      <label>Account No</label></td>
                  <td>
                      <asp:TextBox ID="TxtShortCode" runat="server" Width="95%" AutoPostBack="True" 
                          TabIndex="2"></asp:TextBox>
                  </td>
                  <td class="style1">
                      <asp:TextBox ID="TxtAccNo" runat="server" Width="85%" AutoPostBack="True" 
                          TabIndex="3"></asp:TextBox>
                      <asp:ImageButton ID="ImgSrch" runat="server" Height="20px" 
                          ImageUrl="~/images/Search.png" Width="23px" />
                  </td>
                  <td>
                      &nbsp;</td>
                  <td rowspan="4" valign="top">
                      &nbsp;</td>
                  <td>
                      &nbsp;</td>
              </tr>
              <tr>
                  <td>
                      &nbsp;</td>
                  <td>
                      <label>
                      Customer Id</label></td>
                  <td colspan="2">
                      <asp:TextBox ID="TxtCid" runat="server" Width="95 %" TabIndex="4"></asp:TextBox>
                  </td>

                  <td>
                      &nbsp;</td>
              </tr>
                         
              
              <tr>
                  <td>
                      &nbsp;</td>
                  <td colspan="6">
                      <asp:GridView ID="DgvJointAc" runat="server" AutoGenerateColumns="False" 
                          TabIndex="500" Width="100%" AllowPaging="True" PageSize="5">
                          <Columns>
                              <asp:BoundField DataField="Reg_Date" HeaderText="Date" 
                                  DataFormatString="{0:dd/MM/yyyy}">
                              <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                  BorderWidth="1px" Font-Names="Times New Roman" Font-Size="Medium" 
                                  ForeColor="White" HorizontalAlign="Left" Width="20%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="Pkc_Description" HeaderText="Action Taken">
                              <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                  BorderWidth="1px" Font-Names="Times New Roman" Font-Size="Medium" 
                                  ForeColor="White" HorizontalAlign="Left" Width="30%" />
                              <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                  Font-Size="Medium" ForeColor="#333399" />
                              </asp:BoundField>
                              
                              <asp:BoundField DataField="GLHead" HeaderText="GL Head">
                              <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                  BorderWidth="1px" Font-Names="Times New Roman" Font-Size="Medium" 
                                  ForeColor="White" HorizontalAlign="Left" Width="35%" />
                              <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                  Font-Size="Medium" ForeColor="#333399" />
                              </asp:BoundField>
                              
                              <asp:BoundField DataField="ChargeAmount" HeaderText="Amount">
                              <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                  BorderWidth="1px" Font-Names="Times New Roman" Font-Size="Medium" 
                                  ForeColor="White" HorizontalAlign="Right" Width="30%" />
                              <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                  Font-Size="Medium" ForeColor="#333399" HorizontalAlign="Right" />
                              </asp:BoundField>

                          </Columns>
                          <AlternatingRowStyle BackColor="#F5F7FE" />
                      </asp:GridView>
                  </td>
                  <td>
                      &nbsp;</td>
              </tr>
              <tr>
                  <td>
                      &nbsp;</td>
                  <td>
                      <label>
                      Registration Date</label></td>
                  <td colspan="2">
                      <asp:TextBox ID="TxtRegnDate" runat="server" Width="100%" TabIndex="5"></asp:TextBox>
                  </td>
                  <td>
                      &nbsp;</td>
                  <td>
                      <label>
                      Action Type</label></td>
                  <td>
                      <asp:DropDownList ID="CmbActionType" runat="server" Width="100%" TabIndex="6">
                      </asp:DropDownList>
                  </td>
                  <td>
                      &nbsp;</td>
              </tr>
              <tr>
                  <td>
                      &nbsp;</td>
                  <td>
                      <label>
                      Settlement Amount</label></td>
                  <td colspan="2">
                      <asp:TextBox ID="TxtSettlementAmt" runat="server" Width="100%" TabIndex="7"></asp:TextBox>
                  </td>
                  <td>
                      &nbsp;</td>
                  <td>
                      <label>
                      Register No</label></td>
                  <td>
                      <asp:TextBox ID="TxtRegNo" runat="server" Width="100%" TabIndex="8"></asp:TextBox>
                  </td>
                  <td>
                      &nbsp;</td>
              </tr>
              <tr>
                  <td>
                      &nbsp;</td>
                  <td colspan="2">
                      <asp:CheckBox ID="ChkStopInterest" runat="server"
                          Text="Stop Interest Calculation" AutoPostBack="True" TabIndex="500" />
                  </td>
                  <td class="style1">
                      &nbsp;</td>
                  <td>
                      &nbsp;</td>
                  <td>
                      <label>Balance</label></td>
                  <td>
                      <asp:TextBox ID="TxtBalance" runat="server" Width="100%" TabIndex="10"></asp:TextBox>
                  </td>
                  <td>
                      &nbsp;</td>
              </tr>
              <tr>
                  <td >
                      </td>
                  <td >
                      <label>
                      Interest Date</label></td>
                  <td colspan="2">
                      <asp:TextBox ID="TxtDate" runat="server" Width="100%" TabIndex="9"></asp:TextBox>
                      <asp:CalendarExtender ID="TxtDate_CalendarExtender" runat="server" 
                          Format="dd/MM/yyyy" TargetControlID="TxtDate">
                      </asp:CalendarExtender>
                  </td>
                  <td >
                      </td>
                  <td >
                      <label>Remarks</label></td>
                  <td >
                      <asp:TextBox ID="TxtRemarks" runat="server" Width="100%" TabIndex="1"></asp:TextBox>
                  </td>
                  <td >
                      </td>
              </tr>
            
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <label>
                        Res.No</label></td>
                    <td colspan="2">
                        <asp:TextBox ID="TxtResNo" runat="server" Width="100%" TabIndex="12"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        <label>
                        Res.Date</label></td>
                    <td>
                        <asp:TextBox ID="TxtResDate" runat="server" BorderStyle="None" Width="100%"></asp:TextBox>
                        <asp:CalendarExtender ID="TxtResDate_CalendarExtender" runat="server" 
                            Format="dd/MM/yyyy" TargetControlID="TxtResDate">
                        </asp:CalendarExtender>
                    </td>
                    <td>
                        &nbsp;</td>
              </tr>
            
                <tr>
                  <td>
                      &nbsp;</td>
                  <td>
                      <label>
                      GL Head</label></td>
                  <td colspan="2">
                      <asp:DropDownList ID="CmbGLHead" runat="server" Width="100%" TabIndex="13">
                      </asp:DropDownList>
                    </td>

                  <td>
                      &nbsp;</td>
                  <td>
                      <label>
                      Charge Amount</label></td>
                  <td>
                      <asp:TextBox ID="TxtChargeAmt0" runat="server" Width="55%" TabIndex="14"></asp:TextBox>
                      &nbsp;<asp:ImageButton ID="ImgAdd" runat="server" 
                          ImageUrl="~/Images/AddBtn.png" TabIndex="10" />
                    </td>
                  <td>
                      &nbsp;</td>
              </tr>

              <tr>
                  <td>
                      &nbsp;</td>
                  <td colspan="6">
                      <asp:GridView ID="DgvGl" runat="server" Width="100%" CssClass="grid-view" 
                          AutoGenerateColumns="False" TabIndex="500">
                          <Columns>
                              <asp:BoundField DataFormatString="Acc_Code" HeaderText="Code" Visible="False" />
                              <asp:BoundField DataField="GlHead" HeaderText="G L Head" />
                              <asp:BoundField DataField="ChargeAmount" HeaderText="Charge Amount" >
                                  <HeaderStyle HorizontalAlign="Right" />
                                  <ItemStyle HorizontalAlign="Right" />
                              </asp:BoundField>
                          </Columns>
                      </asp:GridView>
                  </td>
                  <td>
                      &nbsp;</td>
              </tr>
              <tr>
                  <td colspan="2">
                      &nbsp;</td>
                  <td align="center" colspan="5">
                      <asp:ImageButton ID="CmdSave" runat="server" 
                          ImageUrl="~/Images/Save.png" TabIndex="11" />
                      &nbsp;<asp:ImageButton ID="CmdClear" runat="server" 
                          ImageUrl="~/Images/Clear.png" TabIndex="12" />
                      &nbsp;<asp:ImageButton ID="CmdClose" runat="server" 
                          ImageUrl="~/Images/Close.png" TabIndex="13" PostBackUrl="~/Main.aspx" />
                  </td>
                  <td>
                      &nbsp;</td>
              </tr>
              <tr>
                  <td>
                      &nbsp;</td>
                  <td align="center" colspan="5">
                  <asp:HiddenField ID="Hdnglcode" runat="server" />
                  <asp:HiddenField ID="HdnAccId" runat="server" /></td>
                  <td>
                      &nbsp;</td>
              </tr>
          </table>
           </div>
    </tr>
   </ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>