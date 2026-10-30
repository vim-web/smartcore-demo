<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="ChequeRpt.aspx.vb" Inherits="SMARTCORE_UI.ChequeRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
 
     <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;margin-top:5%; font-family: Tahoma" >                
    
    <legend>Cheque Reports</legend>  
    <table width="100%">
    <tr>
    <td width="2%"></td>
    <td width="24%" rowspan="5">
    <asp:Panel ID="Panel2" runat="server" BackColor="#E6F2FF" ForeColor="White">
                <asp:RadioButton ID="Opt_ChequeStock" runat="server" AutoPostBack="True" 
                    Checked="True" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" 
                    ForeColor="White" GroupName="RptType" Text="Cheque Stock" />
                <br />
                <asp:RadioButton ID="Opt_ChequeIssue" runat="server" AutoPostBack="True" 
                    Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" ForeColor="White" 
                    GroupName="RptType" Text="Cheque Issue" />
                <br />
                <asp:RadioButton ID="Opt_ChqReturn" runat="server" AutoPostBack="True" 
                    Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" ForeColor="White" 
                    GroupName="RptType" Text="Cheque Return" />
                <br />
                <asp:RadioButton ID="Opt_ChqStopPayment" runat="server" AutoPostBack="True" 
                    Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" ForeColor="White" 
                    GroupName="RptType" Text="Cheque Stop Payment" />
                <br />
                 </asp:Panel>
    </td>
    <td width="24%"></td>
    <td width="24%"></td>
    <td width="24%"></td>
    <td width="2%"></td>
    </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="24%">
                <asp:Label ID="Lbl_Branch" runat="server" Font-Names="Tahoma" 
                    Font-Size="Medium" ForeColor="#6666FF" Text="Branch"></asp:Label>
            </td>
            <td width="24%">
                <asp:DropDownList ID="Ddl_Branch" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
            <td width="24%">
                &nbsp;</td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="24%">
                <asp:Label ID="Lbl_Date" runat="server" Font-Names="Tahoma" Font-Size="Medium" 
                    ForeColor="#6666FF" Text="Date"></asp:Label>
            </td>
            <td width="24%">
                <asp:TextBox ID="Txt_Date" runat="server" BackColor="White" 
                    CssClass="twitterStyleTextbox" TabIndex="4" Width="97%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="Txt_Date">
                </asp:CalendarExtender>
            </td>
            <td width="24%">
                &nbsp;</td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="24%">
                <asp:Label ID="Lbl_DateTo" runat="server" Font-Names="Tahoma" 
                    Font-Size="Medium" ForeColor="#6666FF" Visible="false" Text="Date"></asp:Label>
            </td>
            <td width="24%">
                <asp:TextBox ID="Txt_DateTo" runat="server" BackColor="White" 
                    CssClass="twitterStyleTextbox" TabIndex="4" Visible="false" Width="97%"></asp:TextBox>
                <asp:CalendarExtender ID="Txt_DateTo_CalendarExtender" runat="server" 
                    Format="dd/MM/yyyy" TargetControlID="Txt_DateTo">
                </asp:CalendarExtender>
            </td>
            <td width="24%">
                &nbsp;</td>
            <td width="2%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td width="24%">
                <asp:Label ID="Lbl_Date0" runat="server" Font-Names="Tahoma" Font-Size="Medium" 
                    ForeColor="#6666FF" Text="Output To"></asp:Label>
            </td>
            <td width="24%">
                <asp:DropDownList ID="CmbOutput" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
            <td width="24%">
                &nbsp;</td>
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
        <tr>
            <td width="2%">
                &nbsp;</td>
            <td colspan="4">
                &nbsp;
                <table width="100%">
     
         <tr>
             <td width="40%">
                     <asp:Panel ID="PnlPage" runat="server" Visible="false">
                            <asp:Label ID="Lbl_AccHead0" runat="server" Font-Names="Tahoma" 
                                Font-Size="Medium" ForeColor="#6666FF" Text="Start Page" Visible="False"></asp:Label>
                            <asp:TextBox ID="Txt_DateFrom0" runat="server" BackColor="White" 
                                CssClass="twitterStyleTextbox" TabIndex="4" Width="20%"></asp:TextBox>
                        </asp:Panel>
                 &nbsp;</td>
            <td width="40%">
                      <asp:ImageButton ID="ImageButton1" runat="server" Height="32px" 
                          ImageUrl="~/Images/Show.png" Width="96px" />
                      <asp:ImageButton ID="ImageButton2" runat="server" Height="32px" 
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
