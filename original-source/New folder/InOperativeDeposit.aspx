<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="InOperativeDeposit.aspx.vb" Inherits="SMARTCORE_UI.InOperativeDeposit" 
    title="InOperative List" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    

        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
 <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%; font-family: Tahoma" >                

     <table width="100%">
        <tr>
            <td width="2%">
            </td>
            <td width="48%">
                <legend>INOPERATIVE / FREEZED ACCOUNT LIST</legend></td>
            <td width="48%"> 
                &nbsp;</td>
            <td width="2%">
            </td>
        </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td>
                 <label>
                 Branch</label></td>
             <td width="45%">
                 <asp:DropDownList ID="CmbBranch" runat="server" Width="100%">
                 </asp:DropDownList>
             </td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td>
                 <label>
                 Section</label></td>
             <td width="45%">
                 <asp:DropDownList ID="CmbSection" runat="server" AutoPostBack="True" 
                     Width="100%">
                     <asp:ListItem Value="1">DEPOSIT</asp:ListItem>
                     <asp:ListItem Value="2">LOAN</asp:ListItem>

                 </asp:DropDownList>
             </td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td>
                 <label>
                 Scheme</label></td>
             <td width="45%">
                 <label>
                 <asp:DropDownList ID="CmbScheme" runat="server" Width="100%">
                 </asp:DropDownList>
                 </label>
             </td>
             <td width="2%">
                 `</td>
         </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td>
                 <label>
                 From Date</label></td>
             <td width="45%">
                 <asp:TextBox ID="TxtFromDate" runat="server" Width="100%"></asp:TextBox>
                 <asp:CalendarExtender ID="TxtFromDate_CalendarExtender" runat="server"  Format="dd/MM/yyyy" 
                     TargetControlID="TxtFromDate">
                 </asp:CalendarExtender>
             </td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td>
                 <label>
                 To Date</label></td>
             <td width="45%">
                 <asp:TextBox ID="TxtTodate" runat="server" Width="100%"></asp:TextBox>
                 <asp:CalendarExtender ID="TxtTodate_CalendarExtender" runat="server"    Format="dd/MM/yyyy" 
                     TargetControlID="TxtTodate">
                 </asp:CalendarExtender>
             </td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td>
                 <label>
                 Report Type</label></td>
             <td width="45%">
                 <asp:DropDownList ID="CmbType" runat="server" AutoPostBack="True" Width="100%">
                     <asp:ListItem>INOPERATIVE LIST</asp:ListItem>
                     <asp:ListItem>FREEZED LIST</asp:ListItem>
                 </asp:DropDownList>
             </td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td width="48%">
                 &nbsp;</td>
             <td width="48%">
                 &nbsp;</td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td width="48%">
                 &nbsp;</td>
             <td width="48%">
                 <asp:ImageButton ID="CmdShow" runat="server" ImageUrl="~/Images/Show.png" />
                 <asp:ImageButton ID="CmdClose" runat="server" 
                     ImageUrl="~/Images/CloseBtn.png" />
             </td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td width="48%">
                 &nbsp;</td>
             <td width="48%">
                 &nbsp;</td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td align="center" colspan="2" style="width: 96%" width="48%">
                 &nbsp;</td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
         
           <tr>
            <td width="2%">
            </td>
            <td width="48%">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                    AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div ID="blur">
                        </div>
                        <div ID="progress">
                            <img alt="progress" src="Images/progress.gif" /> Please Wait...
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
              </td> 
            <td width="48%"> 
                &nbsp;</td>
            <td width="2%">
            </td>
        </tr>
        </table>
     </div>
    </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>
