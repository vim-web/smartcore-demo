<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="ChittyPaymentList.aspx.vb" Inherits="SMARTCORE_UI.ChittyPaymentList" 
    title="CHITTY PAYMENTY LIST" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
    <script type='text/javascript' src="Scripts/Common.js"></script> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True">
    <Scripts>
            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
    </Scripts>
</asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 50%; margin-left:25%; margin-right:25%;margin-top:1%;">     
    <legend>CHITTY PAYMENTY LIST</legend> 
    <table width="100%">
        <tr>
            <td width="2%" height="100%"></td>
            <td width="20%" height="100%">
                <label>Branch</label>
            </td>
            <td width="2%" height="100%"></td>
            <td width="52%" height="100%" colspan="3">
                <asp:DropDownList ID="CmbBranch" runat="server" TabIndex="2" Width="100%" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td width="2%" height="100%"></td>
        </tr>
        <tr>
            <td width="2%" height="100%"></td>
            <td width="20%" height="100%">
                <label>Scheme</label>
            </td>
            <td width="2%" height="100%">
            <td width="52%" height="100%" colspan="3">
                <asp:DropDownList ID="CmbScheme" runat="server" AutoPostBack="True" 
                    TabIndex="3" Width="100%">
                </asp:DropDownList>   
            </td>
             <td width="2%" height="100%"></td>
        </tr>

        <tr>
            <td width="2%" height="100%"></td>
            <td width="20%" height="100%">
                <label>Group</label>
            </td>
            <td width="2%" height="100%"></td>
            <td width="52%" height="100%" colspan="3">
                <asp:DropDownList ID="CmbChittyGrp" runat="server" AutoPostBack="True" 
                    TabIndex="3" Width="100%" AppendDataBoundItems="True">
                </asp:DropDownList>   
            </td>
             <td width="2%" height="100%"></td>
        </tr>

        
        <tr>
            <td width="2%" height="100%"></td>
            <td width="20%" height="100%">
                <label>Date</label>
            </td>
            <td width="2%" height="100%"></td>
            <td colspan=3>
                  <asp:TextBox ID="TxtFromDate" runat="server" Width="30%"></asp:TextBox>
                  <asp:CalendarExtender ID="TxtFromDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                      TargetControlID="TxtFromDate">
                  </asp:CalendarExtender>

            </td>
            <td width="2%" height="100%"></td>
        </tr>
        
        <tr>
            <td width="2%" height="100%"></td>
            <td width="20%" height="100%">
                &nbsp;</td>
            <td width="2%" height="100%"></td>
            <td colspan=3>
                  &nbsp;</td>
            <td width="2%" height="100%"></td>

        </tr>
        <tr>
            <td width="2%" height="100%"></td>
            
            <td colspan=5 align="center">
                <asp:ImageButton ID="CmdReport" runat="server" 
                    ImageUrl="~/Images/Show.png" Width="89px"/>
                  &nbsp;&nbsp;<asp:ImageButton ID="CmdClose" runat="server" 
                    ImageUrl="~/Images/Close.png" PostBackUrl="~/Main.aspx" />  
            </td>
            <td width="2%" height="100%"></td>
        </tr>
        
        <tr>
            <td></td>
            <td colspan="5"> 
            
            </td>
            <td></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                
                <asp:HiddenField ID="HdnTrDate" runat="server" />
                
            </td>
        </tr>
    </table>
    
     <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
         AssociatedUpdatePanelID="UpdatePanel1">
         <ProgressTemplate>
             <div ID="blur">
                 <asp:Panel ID="Panel1" runat="server">
                 </asp:Panel>
             </div>
             <div ID="progress">
                 <img alt="progress" src="Images/progress.gif" /> Please Wait...
             </div>
         </ProgressTemplate>
     </asp:UpdateProgress>
    
</div>
</ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>
