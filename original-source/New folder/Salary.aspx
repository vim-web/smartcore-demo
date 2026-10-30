<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="Salary.aspx.vb" Inherits="SMARTCORE_UI.Salary" 
    title="Untitled Page" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
<script type="text/javascript">
    var safe = function(id) {
        return document.getElementById(id);
    }   
    function hide(id) {
    safe(id).style.display = 'none'; }

    function show(id)
    {  safe(id).style.display = 'block'; }
</script>
  <style type="text/css">
      .classhead{float:left; height:30px; padding-top:10px; background-color:Blue;border-Radius:8px 8px 0px 0px; color:#FFF;text-align:center; cursor:pointer; }
  </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> 
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
        </Scripts>
        </asp:ToolkitScriptManager>
        <div style="width:100%; margin:0 auto;">
        <div class="gradientBoxesWithOuterShadows" style="width:1000px; padding:20px; margin:0 auto;" >
       
        <div id="Menu" style="width:100%; height:40px; ">
        <div class="classhead" style="width:33%;" onclick="hide('Emp_Advance');hide('Emp_bonus');show('Leave');" >Employee Leave Surrender</div>
        <div class="classhead" style="width:33%;" onclick="hide('Leave');hide('Emp_Advance');show('Emp_bonus');" >Employee Bonus</div>
        <div class="classhead" style="width:32%;" onclick="hide('Leave');hide('Emp_bonus');show('Emp_Advance');" >Employee Advance</div>
        </div>

     <div style="width:100%; padding-top:20px;" id="Leave">         
    <legend>Leave Surrender</legend> 
    <table width="100%">
     <tr>
    <td width="24%" colspan="2" style="width: 47%">&nbsp;</td>
        <td style="width: 5%" width="4%">
            &nbsp;</td>
    <td width="20%" colspan="2" style="width: 44%">&nbsp;</td>
    <td width="2%"></td>
     </tr>
     
     <tr>
    <td width="24%">
        <label>
        </td>
    <td width="24%" colspan="2">
       <label>DATE</label>
         </td>
    <td width="24%">
        <asp:TextBox ID="TxtDate" runat="server" meta:resourcekey="TxtDobResource1" 
            OnChange="validatedate(this);" TabIndex="16" width="80%"></asp:TextBox>
        <asp:CalendarExtender ID="CalendarExtender" runat="server" Format="dd/MM/yyyy" 
            TargetControlID="TxtDate">
        </asp:CalendarExtender>
         </td>
    <td width="24%">
       
         <asp:ImageButton ID="CmdShow" runat="server" ImageUrl="~/Images/Show.png" 
             style="height: 23px" />
         </td>
    <td width="2%"></td>
     </tr>
     
    
     <tr>
    <td colspan="5">
                <asp:GridView ID="DgvSalary" runat="server" AutoGenerateColumns="False" 
                    onrowdatabound="DgvSalary_RowDataBound" TabIndex="32" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="EmpId" HeaderText="ID">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EmpName" HeaderText="NAME">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="30%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" />
                        </asp:BoundField>
                         <asp:BoundField DataField="GrossPay" HeaderText="GROSS PAY">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="20%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" />
                        </asp:BoundField>
                         <asp:BoundField DataField="LeaveBal" HeaderText="LEAVE BALANCE">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="20%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" />
                        </asp:BoundField>
                         
                        <asp:TemplateField runat="server" HeaderText="SURRENDER LEAVE" 
                            SortExpression="SurrenderLeave">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="20%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" />
                            <ItemTemplate>
                                <asp:TextBox ID="TxtBalance" runat="server" AutoPostBack="true" onTextChanged="TxtBalance_TextChanged" 
                                    BorderStyle="none" ForeColor="Blue" style="text-align:right" 
                                    Text='<%# Bind("SurrenderLeave") %>' Width="50%">
                        </asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                         <asp:BoundField DataField="SurrenderAmt" HeaderText="LEAVE AMT">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="20%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
    <td width="2%">
        &nbsp;</td>
     </tr>
     
    
        <tr>
    <td width="24%"></td>
    <td width="24%" colspan="2" align="right">
        <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
            meta:resourcekey="CmdSaveResource1" TabIndex="56" />
            </td>
    <td width="24%">
        <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/images/Close.png" 
            meta:resourcekey="CmdClearResource1" TabIndex="57" />
            </td>
    <td width="24%"></td>
    <td width="2%"></td>
        </tr>   
    </table>
    </div>  
    
           <div style="width:100%; padding-top:20px; display:none;" id="Emp_bonus">         
    <legend>Employee Bonus</legend> 
    <table width="100%">
     <tr>
    <td width="24%" colspan="2" style="width: 47%">&nbsp;</td>
        <td style="width: 5%" width="4%">
            &nbsp;</td>
    <td width="20%" colspan="2" style="width: 44%">&nbsp;</td>
    <td width="2%"></td>
     </tr>
     
     <tr>
    <td width="24%">
        <label>
        </td>
    <td width="24%" colspan="2">
       <label>DATE</label>
         </td>
    <td width="24%">
        <asp:TextBox ID="TextBonus" runat="server" meta:resourcekey="TxtDobResource1" 
            OnChange="validatedate(this);" TabIndex="16" width="80%"></asp:TextBox>
        <asp:CalendarExtender ID="Calendar" runat="server" Format="dd/MM/yyyy" 
            TargetControlID="TextBonus">
        </asp:CalendarExtender>
         </td>
    <td width="24%">
       
         <asp:ImageButton ID="ShowBonus" runat="server" ImageUrl="~/Images/Show.png" 
             style="height: 23px" />
         </td>
    <td width="2%"></td>
     </tr>
     
    
     <tr>
    <td colspan="5">
                <asp:GridView ID="DgvBonus" runat="server" AutoGenerateColumns="False" 
                    onrowdatabound="DgvBonus_RowDataBound" TabIndex="32" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="EmpId" HeaderText="ID">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EmpName" HeaderText="NAME">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="30%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" />
                        </asp:BoundField>
                         <asp:BoundField DataField="GrossPay" HeaderText="GROSS PAY">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="20%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" />
                        </asp:BoundField>
                         <asp:BoundField DataField="MaxBonus" HeaderText="MAXIMUM BONUS">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="20%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" />
                        </asp:BoundField>
                         
                        <asp:TemplateField runat="server" HeaderText="BONUS" 
                            SortExpression="SurrenderLeave">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="20%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" />
                            <ItemTemplate>
                                <asp:TextBox ID="TxtBonus" runat="server" AutoPostBack="true" onTextChanged="TxtBonus_TextChanged" 
                                    BorderStyle="none" ForeColor="Blue" style="text-align:right" 
                                    Text='<%# Bind("Bonus") %>' Width="50%">
                        </asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                    </Columns>
                </asp:GridView>
            </td>
    <td width="2%">
        &nbsp;</td>
     </tr>
     
    
        <tr>
    <td width="24%"></td>
    <td width="24%" colspan="2" align="right">
        <asp:ImageButton ID="Bonus_Save" runat="server" ImageUrl="~/images/save.png" 
            meta:resourcekey="CmdSaveResource1" TabIndex="56" />
            </td>
    <td width="24%">
        <asp:ImageButton ID="Bonus_Close" runat="server" ImageUrl="~/images/Close.png" 
            meta:resourcekey="CmdClearResource1" TabIndex="57" />
            </td>
    <td width="24%"></td>
    <td width="2%"></td>
        </tr>   
    </table>
    </div>     
       <div style="width:100%; padding-top:20px;display:none;" id="Emp_Advance">         
    <legend>Employee Advance</legend> 
    <table width="100%">
     <tr>
    <td width="24%" colspan="2" style="width: 47%">&nbsp;</td>
        <td style="width: 5%" width="4%">
            &nbsp;</td>
    <td width="20%" colspan="2" style="width: 44%">&nbsp;</td>
    <td width="2%"></td>
     </tr>
     
     <tr>
    <td width="24%">
        <label>
        </td>
    <td width="24%" colspan="2">
       <label>DATE</label>
         </td>
    <td width="24%">
        <asp:TextBox ID="TxtAdvancedt" runat="server" meta:resourcekey="TxtDobResource1" 
            OnChange="validatedate(this);" TabIndex="16" width="80%"></asp:TextBox>
        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
            TargetControlID="TxtAdvancedt">
        </asp:CalendarExtender>
         </td>
    <td width="24%">
       
         <asp:ImageButton ID="ShowAdvance" runat="server" ImageUrl="~/Images/Show.png" 
             style="height: 23px" />
         </td>
    <td width="2%"></td>
     </tr>
     
    
     <tr>
    <td colspan="5">
                <asp:GridView ID="DgvAdvance" runat="server" AutoGenerateColumns="False" 
                    onrowdatabound="DgvAdvance_RowDataBound" TabIndex="32" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="EmpId" HeaderText="ID">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="10%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EmpName" HeaderText="NAME">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="30%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" />
                        </asp:BoundField>
                         <asp:BoundField DataField="GrossPay" HeaderText="GROSS PAY">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="20%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" />
                        </asp:BoundField>
                         <asp:BoundField DataField="MaxAdvance" HeaderText="MAXIMUM ADVANCE">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="20%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" />
                        </asp:BoundField>
                         
                        <asp:TemplateField runat="server" HeaderText="ADVANCE" 
                            SortExpression="Advance">
                            <HeaderStyle BackColor="#00246F" BorderColor="#7A64E3" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Size="Medium" ForeColor="White" Width="20%" />
                            <ItemStyle BorderColor="#7A64E3" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Size="Medium" />
                            <ItemTemplate>
                                <asp:TextBox ID="TxtAdvance" runat="server" AutoPostBack="true" onTextChanged="TxtAdvance_TextChanged" 
                                    BorderStyle="none" ForeColor="Blue" style="text-align:right" 
                                    Text='<%# Bind("Advance") %>' Width="50%">
                        </asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                       
                    </Columns>
                </asp:GridView>
            </td>
    <td width="2%">
        &nbsp;</td>
     </tr>
     
    
        <tr>
    <td width="24%"></td>
    <td width="24%" colspan="2" align="right">
        <asp:ImageButton ID="AdvanceSave" runat="server" ImageUrl="~/images/save.png" 
            meta:resourcekey="CmdSaveResource1" TabIndex="56" />
            </td>
    <td width="24%">
        <asp:ImageButton ID="AdvanceClose" runat="server" ImageUrl="~/images/Close.png" 
            meta:resourcekey="CmdClearResource1" TabIndex="57" />
            </td>
    <td width="24%"></td>
    <td width="2%"></td>
        </tr>   
    </table>
    </div>         
             
              </div>
        </div>     
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
