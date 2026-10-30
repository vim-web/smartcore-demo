<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="InterestDueList.aspx.vb" Inherits="SMARTCORE_UI.InterestDueList" 
    title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;  margin-top:10%; font-family: Tahoma" >                
    
    
    <legend>Demand List</legend>
    <table width="100%">
            <tr>
                <td width="25%">
                    &nbsp;</td>
                <td width="20%">
                    <asp:Label ID="LblType" runat="server" ForeColor="#6699FF" Text="Other Loan"></asp:Label>
                </td>
                <td width="20%">
                    <asp:CheckBox ID="ChkOther" runat="server" AutoPostBack="True" />
                </td>
                <td width="25%">
                    &nbsp;</td>
            </tr>
        <tr>
                <td width="25%">
                </td>
                <td width="20%">
                    <asp:Label ID="Label2" runat="server" ForeColor="#6699FF" Text="Region"></asp:Label>
                </td>
                <td width="20%">
                    <asp:DropDownList ID="DrpRegn" runat="server" Width="98%" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td width="25%">
                </td>
            </tr>
        <tr>
                <td width="25%">
                </td>
                <td width="20%">
                    <asp:Label ID="Label3" runat="server" ForeColor="#6699FF" Text="Area"></asp:Label>
                </td>
                <td width="20%">
                    <asp:DropDownList ID="DrpArea" runat="server" Width="98%" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td width="25%">
                </td>
            </tr>

            <tr>
                <td width="25%">
                </td>
                <td width="20%">
                    <asp:Label ID="LblBranch" runat="server" ForeColor="#6699FF" Text="Branch"></asp:Label>
                </td>
                <td width="20%">
                    <asp:DropDownList ID="DdlBranch" runat="server" Width="98%">
                    </asp:DropDownList>
                </td>
                <td width="25%">
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="LblScheme" runat="server" ForeColor="#6699FF" Text="Scheme"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DdlScheme" runat="server" Width="98%">
                                           </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="LblSection" runat="server" ForeColor="#6699FF" Text="Section"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="Ddlsection" runat="server" Width="98%">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="Label1" runat="server" ForeColor="#6699FF" Text="From Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Dtfrom" runat="server" Width="98%"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                        TargetControlID="Dtfrom">
                    </asp:CalendarExtender>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="LblDt1" runat="server" ForeColor="#6699FF" Text="To Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="DtTo" runat="server" TabIndex="2" Width="98%"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                        TargetControlID="DtTo">
                    </asp:CalendarExtender>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td align="center" colspan="6" style="width: 40%" width="20%">
                    &nbsp; &nbsp;<asp:ImageButton ID="CmdShow" runat="server" Height="25px" 
                        ImageUrl="~/Images/Show.png" />
                    &nbsp;<asp:ImageButton ID="CmdClose" runat="server" 
                        ImageUrl="~/Images/CloseBtn.png" />
                </td>
            </tr>
            <tr>
                <td width="6%">
                </td>
                <td width="18%">
                    <asp:Label ID="Lbl_Status" runat="server" Text=" "></asp:Label>
                </td>
                <td width="26%">
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
                <td width="18%">
                </td>
                <td width="26%">
                </td>
                <td width="6%">
                </td>
            </tr>
        </table>


            </div>

   </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content> 
