<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="DayEndStatus.aspx.vb" Inherits="SMARTCORE_UI.DayEndStatus" 
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
    
    
    <legend> Branch Day End Status</legend>
    <table width="100%">
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
            <tr>
                <td width="6%">
                    &nbsp;</td>
                <td colspan="4">
                    <asp:GridView ID="Grid_DayEndrpt" runat="server" AutoGenerateColumns="False" 
                        CssClass="grid-viewNew" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="Slno" HeaderText="Slno" />
                            <asp:BoundField DataField="Br_Code" HeaderText="Br_Code" />
                             <asp:BoundField DataField="Branch_Name" HeaderText="Branch" />
                            <asp:BoundField DataField="Trans_Date" DataFormatString="{0:MM/dd/yyyy}" 
                                HeaderText="Trans Date" />
                        </Columns>
                    </asp:GridView>
                </td>
                <td width="6%">
                    &nbsp;</td>
            </tr>
        </table>


            </div>

   </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>  
