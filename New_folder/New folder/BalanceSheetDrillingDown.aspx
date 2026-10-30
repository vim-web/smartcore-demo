<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="BalanceSheetDrillingDown.aspx.vb" Inherits="SMARTCORE_UI.BalanceSheetDrillingDown" 
    title="Untitled Page" %>
  
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
    <link href="CSS/ui-lightness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript' src="Scripts/Common.js"></script>
    <script src="Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script type='text/javascript' src="Scripts/jquery-ui-1.9.2.custom.min.js"></script>      




    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 32px;
        }
        .style3
        {
            height: 26px;
        }
    </style>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePageMethods="True" CombineScripts="True">
   
</asp:ToolkitScriptManager>
    <table class="style1" width="100%">
        <tr>
            <td>
                </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
            <div id="DivPara" runat="server"  width="100%">
            
                <table class="style1" width="100%" >
                    <tr>
                        <td width="25%">
                           </td>
                        <td  width="50%">
                         <div id="Para1" class="gradientBoxesWithOuterShadows" style="width: 80%; margin-left:10%; margin-right:30%; font-family: Tahoma"  >
                            <table class="style1" width="100%">
                                <tr>
                                    <td width="20%" class="style2">
                                        <legend>Drilling Down</legend></td>
                                    <td width="30%" align=" Left" class="style2">&nbsp;</td>
                                    <td width="30%" class="style2">
                                        &nbsp;</td>
                                    <td width="20%" class="style2">
                                        &nbsp;</td>
                                </tr>
                             
                                <tr>
                                    <td class="style2" width="20%">
                                    </td>
                                    <td align="Left" class="style2" width="30%">
                                        <label>
                                        Report Type</label></td>
                                    <td class="style2" width="30%">
                                        <asp:DropDownList ID="DrpType" runat="server" AutoPostBack="True" Width="90%">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style2" width="20%">
                                    </td>
                                </tr>
                             
                                <tr>
                                    <td width="20%">
                                        &nbsp;</td>
                                    <td align="Left" width="30%">
                                        <label>
                                        Branch</label> &nbsp;</td>
                                    <td width="30%">
                                        <asp:DropDownList ID="DrpBranch" runat="server" Width="90%">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="20%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                      <td width="20%" class="style3">
                                        </td>
                                    <td width="30%" align="Left" class="style3">
                                        <asp:Label ID="LblFrom" runat="server" Text="As On Date" ForeColor="#008080" Font-Size="90%"></asp:Label>
                                        &nbsp;</td>
                                    <td width="30%" align="Left" class="style3">
                                        <asp:TextBox ID="TxtAsOn" runat="server" Width="88%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                        TargetControlID="TxtAsOn">
                    </asp:CalendarExtender>
                                      </td>
                                    <td width="20%" class="style3">
                                        </td>
                                </tr>
                                <tr>
                                    <td width="20%">
                                        &nbsp;</td>
                                    <td align="left" width="30%">
                                        <asp:Label ID="LblToDt" runat="server" Text="To Date" ForeColor="#008080" Font-Size="90%" Visible="false"></asp:Label></td>
                                    <td width="30%">
                                        <asp:TextBox ID="TxtToDt" runat="server" Width="88%" Visible="false"></asp:TextBox>
                                         <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                        TargetControlID="TxtToDt">
                    </asp:CalendarExtender>
                                    </td>
                                    <td width="20%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="20%">
                                        &nbsp;</td>
                                    <td width="30%">
             <%--                                               <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                        AssociatedUpdatePanelID="UpdatePanel1">
                        <ProgressTemplate>
                            <div ID="blur">
                            </div>
                            <div ID="progress">
                                <img alt="progress" src="Images/progress.gif" /> Please Wait...
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>--%>
                                        &nbsp;</td>
                                    <td width="30%">
                                        &nbsp;</td>
                                    <td width="20%">
                                        &nbsp;</td>
                                </tr>
                                
                                <tr>
                                     <td width="20%">
                                        &nbsp;</td>
                                    <td width="30%" colspan="2" style="width: 60%">
                                        <table class="style1" width="100%">
                                            <tr>
                                                <td width="25%">
                         
                                                    &nbsp;</td>
                                                <td width="25%" align="right">
                                                    <asp:ImageButton ID="CmdShow" runat="server" Height="25px" 
                                                        ImageUrl="~/Images/Show.png" />
                                                </td>
                                                <td width="25%">
                                                    <asp:ImageButton ID="CmdClose" runat="server" 
                                                        ImageUrl="~/Images/CloseBtn.png" />
                                                </td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                     </td>
                                    <td width="20%">
                                        &nbsp;</td>
                                </tr>
                            </table>
                         </div>
                        </td>
                        <td width="25%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="25%">
                            &nbsp;</td>
                        <td width="50%"> 
                            &nbsp;</td>
                            <td width="25%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                         <td width="25%">
                            &nbsp;</td>
                        <td width="50%"> 
                            &nbsp;</td>
                            <td width="25%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                          <td width="25%">
                            &nbsp;</td>
                        <td width="50%"> 
                            &nbsp;</td>
                            <td width="25%">
                            &nbsp;</td>
                    </tr>
                </table>
            
            </div>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <div ID="DivBal" runat="server" visible="false">
                <table class="style1" width="100%">
                    <tr>
                        <td><legend align="right" width="50%">
                            <asp:Label ID="LblLiab" runat="server" Text="Liability"></asp:Label></legend>
                            </td>
                        <td><legend align="right" width="50%"> 
                            <asp:Label ID="LblAsset" runat="server" Text="Asset"></asp:Label></legend>
                            </td>
                    </tr>
                </table>
             
            </td>
        </tr>
        <tr>
            <td colspan="2">
               
                    <table border="0" width="100%">
                        <tr>
                            <td align="right" width="50%">
                                <asp:GridView ID="GvLiab" runat="server" align="Right" 
                                    AutoGenerateColumns="False" CssClass="grid-viewNew" 
                                    DataKeyNames="LIAB_CODE,LIAB_NAME,LIAB_PRTYPE" EmptyDataText="no data" 
                                    EnableCellClick="True" style="height:200px;overflow:auto" Width="98%" >
                                    <Columns>
                                    <asp:BoundField DataField="LIAB_PRTYPE" HeaderText="" Visible="True" />
                                        <asp:BoundField DataField="LIAB_CODE" HeaderText="Account Code" 
                                            Visible="true" />
                                        <asp:BoundField DataField="LIAB_NAME" HeaderText="Account Head" 
                                            Visible="true" />
                                        <asp:BoundField DataField="LIAB_AMT" HeaderText="Amount" Visible="true" />
                                       
                                        <asp:commandfield buttontype="Image" selectimageurl="~/Images/Plus1.png" 
                                            showselectbutton="true" />
                                             
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#F5F7FE" />
                                </asp:GridView>
                                &nbsp;</td>
                            <td align="left" width="50%">
                                <asp:GridView ID="GvAsset" runat="server" align="Left" 
                                    AutoGenerateColumns="False" CssClass="grid-viewNew"
                                    DataKeyNames="ASSET_CODE,ASSET_NAME,ASSET_PRTYPE" EnableCellClick="True" 
                                    style="height:200px;overflow:auto" Width="98%" >
                                    <Columns>
                                   <asp:BoundField DataField="ASSET_PRTYPE" HeaderText="" Visible="True" />
                                        <asp:BoundField DataField="ASSET_CODE" HeaderText="Account Code" 
                                            Visible="true" />
                                        <asp:BoundField DataField="ASSET_NAME" HeaderText="Account Head" 
                                            Visible="true" />
                                        <asp:BoundField DataField="ASSET_AMT" HeaderText="Amount" Visible="true" />
                                          
                                        <asp:commandfield buttontype="Image" selectimageurl="~/Images/Plus1.png" 
                                            showselectbutton="true" />
                                           
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#F5F7FE" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:HiddenField ID="HdnCashCode" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
            <div id="DivSubTotal"  runat="server"  visible="false" >
                <table class="style1" width="100%">
                    <tr>
                        <td width="25%">
                            &nbsp;</td>
                        <td width="50%" rowspan="2">
                 
                            <asp:GridView ID="GVSubTotal" runat="server" AutoGenerateColumns="False" 
                            CssClass="grid-viewNew" DataKeyNames="AccCode,AccName" Width="100%">
                               <Columns >
                                <asp:BoundField DataField="AccCode" HeaderText="Account Code" Visible="true" />
                                <asp:BoundField DataField="AccName" HeaderText="Account Head" Visible="true" />                               
                                <asp:BoundField DataField="BalAmnt" HeaderText="Balance" Visible="true" />
                                <asp:commandfield buttontype="Image" selectimageurl="~/Images/Plus1.png" 
                                    showselectbutton="true" />
                            </Columns>
                            <AlternatingRowStyle BackColor="#F5F7FE" />
                           
                            </asp:GridView>
                            
                        </td>
                        <td width="25%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                         <td width="25%">
                            &nbsp;</td>
                        <td width="25%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                         <td width="25%">
                            &nbsp;</td>
                        <td width="50%">
                            <table class="style1" width="100%">
                                <tr>
                                    <td width="25%">
                                        &nbsp;</td>
                                    <td width="25%">                                        
                                        &nbsp;</td>
                                    <td width="25%" align="left">
                                    <asp:Button ID="BtnBack5" runat="server" Text="Back" />
                                        &nbsp;</td>
                                    <td width="25%">
                                        &nbsp;</td>
                                </tr>
                            </table>
                         </td>
                        <td width="25%">
                            &nbsp;</td>
                    </tr>
                </table>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            <div id="DivYear"  runat="server"   visible="false" >
            <table width="100%">
            <tr>
            <td width="25%">
            
                &nbsp;</td>
            <td width="50%">
            <legend>Year Total</legend>
                &nbsp;</td>
            <td width="25%">
            
                &nbsp;</td>
            <td>
            
                &nbsp;</td>
            </tr>
                <tr>
                    <td width="25%">
                    </td>
                    <td width="50%">
                        <asp:GridView ID="GvYear" runat="server" AutoGenerateColumns="False" 
                            CssClass="grid-viewNew" Width="100%" DataKeyNames="AccCode,AccName,FinYear">
                            <Columns>
                                <asp:BoundField DataField="AccCode" HeaderText="Account Code" Visible="true" />
                                <asp:BoundField DataField="AccName" HeaderText="Account Head" Visible="true" />
                                <asp:BoundField DataField="FinYear" HeaderText="Financial Year" Visible="true" />
                                <asp:BoundField DataField="BalAmnt" HeaderText="Balance" Visible="true" />
                                <asp:commandfield buttontype="Image" selectimageurl="~/Images/Plus1.png" 
                                    showselectbutton="true" />
                            </Columns>
                            <AlternatingRowStyle BackColor="#F5F7FE" />
                        </asp:GridView>
                    </td>
                    <td width="25%">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="50%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="50%">
                        <table class="style1" width="100%">
                            <tr>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    <asp:Button ID="BtnBack1" runat="server" Text="Back" />
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                    <td width="25%">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            </div>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
            <div id="DivMonth"  runat="server" visible="false">
            <table width="100%">
            <tr>
            <td width="25%">&nbsp;</td>
            <td width="50%"> <legend>Month Total</legend>
                &nbsp;</td>
            <td width="25%">&nbsp;</td>
            </tr>
            
                <tr>
                    <td width="25%">
                    </td>
                    <td width="50%">
                        <asp:GridView ID="GvMonth" runat="server" AutoGenerateColumns="False" 
                            CssClass="grid-viewNew" DataKeyNames="AccCode,AccName,Month" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="AccCode" HeaderText="Account Code" Visible="true" />
                                <asp:BoundField DataField="AccName" HeaderText="Account Head" Visible="true" />
                                <asp:BoundField DataField="Month" HeaderText="TranMonth" Visible="true" />
                                <asp:BoundField DataField="OpenBal" HeaderText="Open Balance" Visible="true" />
                                <asp:BoundField DataField="Receipt" HeaderText="Receipt" Visible="true" />
                                <asp:BoundField DataField="Payment" HeaderText="Payment" Visible="true" />
                                <asp:BoundField DataField="BalAmnt" HeaderText="Balance" Visible="true" />
                                <asp:commandfield buttontype="Image" selectimageurl="~/Images/Plus1.png" 
                                    showselectbutton="true" />
                            </Columns>
                            <AlternatingRowStyle BackColor="#F5F7FE" />
                        </asp:GridView>
                    </td>
                    <td width="25%">
                    </td>
                </tr>
            
                <tr>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="50%">
                        <table class="style1" width="100%">
                            <tr>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td width="25%">
                                    <asp:Button ID="BtnBack2" runat="server" Text="Back" />
                                </td>
                                <td width="25%">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                    <td width="25%">
                        &nbsp;</td>
                </tr>
            
            </table>
            </div>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
            <div id="DivDaily"  runat="server" visible="false">
                <table class="style1" width="100%">
                    <tr>
                        <td width="25%">
                            &nbsp;</td>
                        <td width="25%"><legend>Day Total</legend>
                            &nbsp;</td>
                        <td width="25%">
                            &nbsp;</td>
                        <td width="25%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="25%">
                            &nbsp;</td>
                        <td colspan="2" width="50%">
                            <asp:GridView ID="GvDaily" runat="server" CssClass="grid-viewNew" AutoGenerateColumns="False" 
                            DataKeyNames="AccCode,AccName,Trdate" Width="100%">
                               <Columns>
                                <asp:BoundField DataField="AccCode" HeaderText="Account Code" Visible="true" />
                                <asp:BoundField DataField="AccName" HeaderText="Account Head" Visible="true" />
                                <asp:BoundField DataField="Trdate" HeaderText="Date" Visible="true" />
                                <asp:BoundField DataField="OpenBal" HeaderText="Open Balance" Visible="true" /> 
                                <asp:BoundField DataField="Recpt" HeaderText="Receipt" Visible="true" /> 
                                <asp:BoundField DataField="Payment" HeaderText="Payment" Visible="true" /> 
                                <asp:BoundField DataField="BalAmnt" HeaderText="Balance" Visible="true" /> 
                                <asp:commandfield buttontype="Image" selectimageurl="~/Images/Plus1.png" 
                                    showselectbutton="true" />
                            </Columns>
                            <AlternatingRowStyle BackColor="#F5F7FE" />
                            </asp:GridView>
                        </td>
                        <td width="25%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="25%">
                            &nbsp;</td>
                        <td colspan="2" width="50%">
                            <table class="style1" width="100%">
                                <tr>
                                    <td width="25%">
                                        &nbsp;</td>
                                    <td width="25%">
                                        &nbsp;</td>
                                    <td width="25%">
                                        <asp:Button ID="BtnBack3" runat="server" Text="Back" />
                                    </td>
                                    <td width="25%">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                        <td width="25%">
                            &nbsp;</td>
                    </tr>
                </table>
                </div>
            </td>
        </tr>
        
        <div id="DivDayBook"  runat="server" visible="false">
   
      <%--  <form id="form1" runat="server">--%>
        <table width="100%">
        
            <tr width="100%">
                <td width="25%">
                    &nbsp;</td>
                <td width="25%"><legend>DayBook</legend>
                    &nbsp;</td>
                <td width="25%">
                    &nbsp;</td>
                <td width="25%">
                    &nbsp;</td>
            </tr>
            <tr width="100%">
                <td colspan="4">
                
                    <table class="style1" width="100%">
                        <tr>
                            <td width="5%">
                                &nbsp;</td>
                            <td width="90%">
                            <div style="min-height:300px;max-height:350px;overflow:scroll;">
                           
                                <asp:GridView ID="GvDayBook" runat="server" Width="100%" CssClass="grid-viewNew" AutoGenerateColumns="False" >
                                       <Columns>
                                <asp:BoundField DataField="Br_Name" HeaderText="Branch" Visible="true" />
                                <asp:BoundField DataField="Tr_Date" HeaderText="Tran Date" Visible="true" />
                                <asp:BoundField DataField="Vr_No" HeaderText="Vou No" Visible="true" />
                                <asp:BoundField DataField="Remarks" HeaderText="Particulars" Visible="true" /> 
                                <asp:BoundField DataField="RctCash" HeaderText="Receipt Cash" Visible="true" /> 
                                <asp:BoundField DataField="RctTransfer" HeaderText="Receipt Transfer" Visible="true" /> 
                                <asp:BoundField DataField="PayCash" HeaderText="Payment Cash" Visible="true" /> 
                                <asp:BoundField DataField="PayTransfer" HeaderText="Payment Transfer" Visible="true" />                                 
                            </Columns>
                            <AlternatingRowStyle BackColor="#F5F7FE" />
                                </asp:GridView>
       
                                </div>
                            </td>
                            <td width="5%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="5%">
                                &nbsp;</td>
                            <td width="90%">
                                <table class="style1" width="100%">
                                    <tr>
                                        <td width="25%">
                                            &nbsp;</td>
                                        <td width="25%" align="right">
                                            <asp:Button ID="BtnExcel" runat="server" Text="Print" visible="false"/>
                                        </td>
                                        <td width="25%">
                                            <asp:Button ID="BtnBack4" runat="server" Text="Back" />
                                            &nbsp;</td>
                                        <td width="25%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                            <td width="5%">
                                &nbsp;</td>
                        </tr>
                    </table>
                  
                    </div>
                </td>
            </tr>
        
        </table>
        
    </table>
    </ContentTemplate>
    </asp:UpdatePanel> 
</asp:Content>
