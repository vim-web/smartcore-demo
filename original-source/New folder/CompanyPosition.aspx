<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="CompanyPosition.aspx.vb" Inherits="SMARTCORE_UI.CompanyPosition" 
    title="Company Position" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>   
    <asp:Content ID="Content4" runat="server" contentplaceholderid="head">
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />        
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 23px;
        }
    </style>
</asp:Content>--%>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style> 
         




</asp:Content>
    <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server"  
        EnablePageMethods="True" CombineScripts="True"> 
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
        </Scripts>
        </asp:ToolkitScriptManager>
      <%--<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True">
        </asp:ToolkitScriptManager>--%>
        <div style="width: 98%; margin-left:1%; margin-right:1%;margin-top:1%;">
<div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%;  float:left; ">


    <table  width="100%">
        <tr>
            <td width="25%">
                <legend>Company Position</legend></td>
            <td width="25%">
                &nbsp;</td>
            <td width="25%">
                &nbsp;</td>
            <td width="25%">
                &nbsp;</td>
        </tr>
        <tr>
            <td >
                </td>
            <td >
                <label>Branch</label></td>
            <td >
                <asp:DropDownList ID="Ddl_Branch" runat="server" Width="98%">
                </asp:DropDownList>
            </td>
            <td >
                </td>
        </tr>
        <tr>
            <td>
                </td>
            <td>
               <label>As On Date</label></td>
            <td>
                <asp:TextBox ID="TxtAson" runat="server" Width="96%"></asp:TextBox>
                 <asp:CalendarExtender ID="TxtFromDate_CalendarExtender" Format="dd/MM/yyyy"  runat="server" 
                     TargetControlID="TxtAson">
                 </asp:CalendarExtender>                
                </td>
                 
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                 <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                
                    <div id="progress">
                        <img alt="progress" src="Images/progress.gif" /> Please Wait...
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress></td>
            <td colspan="2">
                <table width="100%">
                    <tr>
                        <td width="25%">
                            <asp:ImageButton ID="BtnPro" runat="server" 
                                ImageUrl="~/Images/ProcessBtn.png" Height="24px"   />
                        </td>
                        <td width="25%">
                           <asp:ImageButton ID="CmdShow" runat="server" ImageUrl="~/Images/Show.png" />
                 </td>
                        <td width="25%">
                          <asp:ImageButton ID="CmdClose" runat="server" 
                     ImageUrl="~/Images/CloseBtn.png" /> </td>
                        <td width="25%">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>


</div>
            <asp:Panel ID="Panel1" runat="server">
            
                <table class="style1" width="100%">
                    <tr>
                        <td width="2%">
                            &nbsp;</td>
                        <td width="45%">
                            &nbsp;</td>
                        <td width="6%">
                            &nbsp;</td>
                        <td width="45%">
                            &nbsp;</td>
                        <td width="2%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="2%">
                            &nbsp;</td>
                        <td width="45%">
                            <legend>Deposit Outstand</legend>
                        </td>
                        <td width="6%">
                            &nbsp;</td>
                        <td width="45%">
                            <legend>Loan Outstand</legend>
                        </td>
                        <td width="2%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                       <td width="2%">
                            &nbsp;</td>
                        <td width="45%">
                        <div></div>
                            <asp:Panel ID="Panel2" runat="server" CssClass="gradientBoxesWithOuterShadows" Width="100%">
                                <asp:GridView ID="GvDeposit"  AutoGenerateColumns="False" CssClass="grid-viewNew"  runat="server" Width="100%">
                                 <Columns >
                                <asp:BoundField DataField="Scheme" HeaderText="Scheme" Visible="true" />
                                <asp:BoundField DataField="BalNo" HeaderText="Bal No" Visible="true" />                               
                                <asp:BoundField DataField="BalAmnt" HeaderText="Balance" Visible="true" />    
                                 <asp:BoundField DataField="Percentage" HeaderText="Percentage" Visible="true" />                            
                            </Columns>
                            <AlternatingRowStyle BackColor="#F5F7FE" />
                                </asp:GridView>
                            </asp:Panel>
                            </td>
                        <td width="6%">
                            &nbsp;</td>
                        <td width="45%">
                            <asp:Panel ID="Panel3" runat="server" CssClass="gradientBoxesWithOuterShadows" Width="100%">
                              <asp:GridView ID="GvLoan"  AutoGenerateColumns="False" CssClass="grid-viewNew"  runat="server" Width="100%">
                                 <Columns >
                                <asp:BoundField DataField="Scheme" HeaderText="Scheme" Visible="true" />
                                <asp:BoundField DataField="BalNo" HeaderText="Bal No" Visible="true" />                               
                                <asp:BoundField DataField="BalAmnt" HeaderText="Balance" Visible="true" />    
                                 <asp:BoundField DataField="Percentage" HeaderText="Percentage" Visible="true" />                            
                            </Columns>
                            <AlternatingRowStyle BackColor="#F5F7FE" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                        <td width="2%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                          <td width="2%">
                            &nbsp;</td>
                        <td width="45%">
                            &nbsp;</td>
                        <td width="6%">
                            &nbsp;</td>
                        <td width="45%">
                            &nbsp;</td>
                        <td width="2%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                         <td width="2%">
                            &nbsp;</td>
                        <td width="45%">
                            <Legend>Share</Legend></td>
                        <td width="6%">
                            &nbsp;</td>
                        <td width="45%">
                            <legend>Suspense</legend></td>
                        <td width="2%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="2%">
                            &nbsp;</td>
                        <td width="45%">
                       
                            <asp:Panel ID="Panel4" runat="server" CssClass="gradientBoxesWithOuterShadows" Width="100%">
                                <asp:GridView ID="GvShare" AutoGenerateColumns="False" CssClass="grid-viewNew"  runat="server" Width="100%">
                                  <Columns >
                                <asp:BoundField DataField="Scheme" HeaderText="Scheme" Visible="true" />
                                <asp:BoundField DataField="BalNo" HeaderText="Bal No" Visible="true" />                               
                                <asp:BoundField DataField="BalAmnt" HeaderText="Balance" Visible="true" />    
                                 <asp:BoundField DataField="Percentage" HeaderText="Percentage" Visible="true" />                            
                            </Columns>
                            <AlternatingRowStyle BackColor="#F5F7FE" />
                                </asp:GridView>
                            </asp:Panel>
                           
                        </td>
                        <td width="6%">
                            &nbsp;</td>
                        <td width="45%">
                               <asp:Panel ID="Panel5" runat="server" CssClass="gradientBoxesWithOuterShadows" Width="100%">
                                <asp:GridView ID="GvSuspense" AutoGenerateColumns="False" CssClass="grid-viewNew"  runat="server" Width="100%">
                                  <Columns >
                                <asp:BoundField DataField="Scheme" HeaderText="Scheme" Visible="true" />
                                <asp:BoundField DataField="BalNo" HeaderText="Bal No" Visible="true" />                               
                                <asp:BoundField DataField="BalAmnt" HeaderText="Balance" Visible="true" />    
                                 <asp:BoundField DataField="Percentage" HeaderText="Percentage" Visible="true" />                            
                            </Columns>
                            <AlternatingRowStyle BackColor="#F5F7FE" />
                                </asp:GridView>
                            </asp:Panel>
                            </td>
                        <td width="2%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="2%">
                            &nbsp;</td>
                        <td width="45%">
                            &nbsp;</td>
                        <td width="6%">
                            &nbsp;</td>
                        <td width="45%">
                            &nbsp;</td>
                        <td width="2%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="2%">
                            &nbsp;</td>
                        <td width="45%">
                            <legend>Other Details</legend></td>
                        <td width="6%">
                            &nbsp;</td>
                        <td width="45%">
                            &nbsp;</td>
                        <td width="2%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="2%">
                            &nbsp;</td>
                        <td width="45%">
                        
                              <asp:Panel ID="Panel6" runat="server" CssClass="gradientBoxesWithOuterShadows" Width="100%">
                                <asp:GridView ID="GV_Other" AutoGenerateColumns="False" CssClass="grid-viewNew"  runat="server" Width="100%">
                                  <Columns >
                                <asp:BoundField DataField="Scheme" HeaderText="Scheme" Visible="true" />
                                <asp:BoundField DataField="BalNo" HeaderText="Bal No" Visible="true" />                               
                                <asp:BoundField DataField="BalAmnt" HeaderText="Balance" Visible="true" />    
                                 <asp:BoundField DataField="Percentage" HeaderText="Percentage" Visible="true" />                            
                            </Columns>
                            <AlternatingRowStyle BackColor="#F5F7FE" />
                                </asp:GridView>
                            </asp:Panel>
                        
                        
                            &nbsp;</td>
                        <td width="6%">
                            &nbsp;</td>
                        <td width="45%">
                            &nbsp;</td>
                        <td width="2%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="2%">
                            &nbsp;</td>
                        <td width="45%">
                            &nbsp;</td>
                        <td width="6%">
                            &nbsp;</td>
                        <td width="45%">
                            &nbsp;</td>
                        <td width="2%">
                            &nbsp;</td>
                    </tr>
                </table>
            
            </asp:Panel>
</div>
</ContentTemplate>
</asp:UpdatePanel>

</asp:Content>


