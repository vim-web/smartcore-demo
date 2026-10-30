<%@Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="TransFrmXL.aspx.vb" Inherits="SMARTCORE_UI.TransFrmXL"%>
<%@Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">   

          <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpMain" runat="server">
    <ContentTemplate>

            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
            </Scripts>
            </asp:ToolkitScriptManager>
    <div class="gradientBoxesWithOuterShadows" style="width: 77%; margin-left:10%;  float:left; " >                  
    <legend>Excel Transactions</legend>
    <table style="width: 93%;">
        <tr>
            <td class="style1" colspan="7">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="LblFile" runat="server" Text="Select File"></asp:Label>
            </td>
            <td class="style4" colspan="6">
                <asp:FileUpload ID="FleFile" runat="server" Width="436px" />
                &nbsp;</td>
            <td class="style4" rowspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style4" ALIGN="right">
                <asp:ImageButton ID="CmdUpload" runat="server" height="24px" 
                    ImageUrl="~/Images/Uploadbtn.jpg" OnClientClick="showProgressforGenerate()" 
                    Width="69px" />
                <asp:ImageButton ID="Btn_Save" runat="server" BackColor="#009999" 
                    ImageUrl="~/images/save.png" OnClientClick="return ValidateShares();" 
                    Text="SAVE" Width="69px" />
                <asp:ImageButton ID="But_Clear" runat="server" BackColor="#009999" 
                    ImageUrl="~/images/Close.png" Text="CLOSE" Width="72px" />
            </td>
            <td ALIGN="right" class="style4">
                &nbsp;</td>
            <td ALIGN="right" class="style4">
                &nbsp;</td>
            <td ALIGN="right" class="style4">
                &nbsp;</td>
            <td ALIGN="right" class="style4">
                &nbsp;</td>
            <td ALIGN="right" class="style4">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="7">
                <div style="overflow: scroll; height: 344px; width: 946px;">
                <asp:GridView ID="GVTransDetails" runat="server" CssClass="grid-view111New"
                    EnableModelValidation="True" Height="105px" Width="920px">
                    <HeaderStyle BackColor="#2063b7" ForeColor="White" />
                </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td class="style1" colspan="7">
                &nbsp;</td>
        </tr>
    </table>
    </div>
    </ContentTemplate>
             <Triggers>
       <asp:PostBackTrigger ControlID="CmdUpload" />
       </Triggers>
    </asp:UpdatePanel> 
</asp:Content>
