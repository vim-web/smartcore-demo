<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="LoanOdrpt.aspx.vb" Inherits="SMARTCORE_UI.LoanOdrpt" %>
  <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
 <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;margin-top:5%" > 
    <legend>Loan OD Transaction For Month</legend>

    <table width="100%">
        <tr>
            <td width="2%"> </td>
            <td width="48%"> </td>
            <td width="48%"> </td>
            <td width="2%"> </td>           
        </tr>
        
         <tr>
            <td width="2%"> </td>
            <td width="48%"> 
                <asp:Label ID="LblDdl1" runat="server" Text="Branch" ForeColor="#6699FF"></asp:Label>
             </td>
            <td width="48%"> 
                <asp:DropDownList ID="DdlBranch" runat="server" Width="98%">
                </asp:DropDownList>
             </td>
            <td  width="2%"> 
               </td>
           
        </tr>     
        
        
        
       
         <tr>
            <td width="2%" class="auto-style1"> </td>
            <td width="48%" class="auto-style1"> 
                <asp:Label ID="FromDate" runat="server" Text="From Date" ForeColor="#6699FF"></asp:Label>
             </td>
            <td width="48%" class="auto-style1"> 
                <asp:TextBox ID="Txt_Fromdate" runat="server" Width="100%"></asp:TextBox>
                <asp:CalendarExtender ID="Txt_Fromdate_CalendarExtender" runat="server" Enabled="True" TargetControlID="Txt_Fromdate">
                </asp:CalendarExtender>
             </td>
            
            <td width="2%" class="auto-style1"> </td>
        </tr>
         <tr>
            <td width="2%"> </td>
            <td width="48%"> 
                <asp:Label ID="ToDate" runat="server" Text="To Date" ForeColor="#6699FF"></asp:Label>
             </td>
            <td width="48%"> 
                <asp:TextBox ID="Txt_Todate" runat="server" Width="100%"></asp:TextBox>
                <asp:CalendarExtender ID="Txt_Todate_CalendarExtender" runat="server" Enabled="True" TargetControlID="Txt_Todate">
                </asp:CalendarExtender>
             </td>
            
            <td width="2%"> </td>
        </tr>
         <tr>
            <td width="2%"> </td>
            <td width="48%"> </td>
            <td width="48%"> </td>
            <td width="2%"> </td>           
        </tr>
        
         <tr>
             <td width="2%"></td>
             <td align="right" width="48%">
                 <asp:ImageButton ID="CmdShow" runat="server" Height="25px" ImageUrl="~/Images/Show.png" />
             </td>
             <td width="48%">
                 <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/CloseBtn.png" />
             </td>
             <td width="2%"></td>

        </table>
        <table width="100%">     
        
           <tr>
            <td width="25%"> </td>
            <td width="50%">  <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                    AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div ID="blur">
                        </div>
                        <div ID="progress">
                            <img alt="progress" src="Images/progress.gif" /> Please Wait...
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress></td>
            <td width="25%">              
               </td>
          
        </tr>
        
    </table>
            </div>

   </ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>
