<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="ChittyActionTakerRpt.aspx.vb" Inherits="SMARTCORE_UI.ChittyActionTakerRpt" 
    title="CHITTY ACTION TAKEN REPORT" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
 
     <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;margin-top:5%; font-family: Tahoma" >                
    
    <legend>Chitty Action Taken Report</legend>  
    <table width="100%">
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="48%">
                <asp:Label ID="Lbl_Branch" runat="server" Font-Names="Tahoma" 
                    Font-Size="Medium" ForeColor="#6666FF" Text="Branch"></asp:Label>
            </td>
            <td width="48%">
                <asp:DropDownList ID="Cmb_Branch" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="24%">
                <asp:Label ID="Lbl_Scheme" runat="server" Font-Names="Tahoma" 
                    Font-Size="Medium" ForeColor="#6666FF" Text="Scheme"></asp:Label>
            </td>
            <td width="24%">
                <asp:DropDownList ID="Cmb_Scheme" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="24%">
                <asp:Label ID="Lbl_Action" runat="server" Font-Names="Tahoma" 
                    Font-Size="Medium" ForeColor="#6666FF" Text="Action Type"></asp:Label>
            </td>
            <td width="24%">
                <asp:DropDownList ID="CmbActionType" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="24%">
                <asp:Label ID="Lbl_Date" runat="server" Font-Names="Tahoma" Font-Size="Medium" 
                    ForeColor="#6666FF" Text="From Date"></asp:Label>
            </td>
            <td width="24%">
                <asp:TextBox ID="Txt_FrDate" runat="server" BackColor="White" 
                    CssClass="twitterStyleTextbox" TabIndex="4" Width="97%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="Txt_FrDate">
                </asp:CalendarExtender>
            </td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="24%">
                <asp:Label ID="Lbl_DateTo" runat="server" Font-Names="Tahoma" 
                    Font-Size="Medium" ForeColor="#6666FF"  Text="To Date"></asp:Label>
            </td>
            <td width="24%">
                <asp:TextBox ID="Txt_ToDate" runat="server" BackColor="White" 
                    CssClass="twitterStyleTextbox" TabIndex="4" Width="97%"></asp:TextBox>
                <asp:CalendarExtender ID="Txt_ToDate_CalendarExtender" runat="server" 
                    Format="dd/MM/yyyy" TargetControlID="Txt_ToDate">
                </asp:CalendarExtender>
            </td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="24%">
                <asp:Label ID="Lbl_GLHead" runat="server" Font-Names="Tahoma" 
                    Font-Size="Medium" ForeColor="#6666FF" Text="GL Head"></asp:Label>
            </td>
            <td width="24%">
                <asp:DropDownList ID="Cmb_GLHead" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td colspan="4">
                &nbsp;
                <table width="100%">
     
         <tr>
             <td width="20%">
                 &nbsp;</td>
            <td width="60%">
                      <asp:ImageButton ID="Cmd_Show" runat="server" Height="32px" 
                          ImageUrl="~/Images/Show.png" Width="96px" />
                      <asp:ImageButton ID="Cmd_Close" runat="server" Height="32px" 
                          ImageUrl="~/images/Close.png" Width="96px" />
             </td>
               
                    <td width="20%">
                 
                     
                
                 
                     
             </td>
           
         </tr>
     
    
  
     </table>
                </td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="24%">
                &nbsp;</td>
            <td width="24%">
                &nbsp;</td>
            <td width="24%">
                &nbsp;</td>
            <td width="24%">
                &nbsp;</td>
            <td width="2%">
                &nbsp;</td>
        </tr>
    </table>

  
</div> 
</ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>

