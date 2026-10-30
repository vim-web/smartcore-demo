<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="AssetMaster.aspx.vb" Inherits="SMARTCORE_UI.AssetMaster" 
    title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
     <script type='text/javascript' src="Scripts/Common.js"></script> 
    <style type="text/css">
        .style1
        {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    b<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  
        EnablePageMethods="True" CombineScripts="True"> 
       
        </asp:ToolkitScriptManager>
    <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;margin-top:5%; " >                
        <legend>ASSET MASTER</legend> <br />
        <table width="100%">
        <tr>
        <td width="2%"></td>
        <td width="20%"><label>Asset Code</label></td>
        <td  width="24%">
            <asp:TextBox ID="TxtAssetCode" runat="server" Width="100%" onkeypress="return isFloatKey(event);" 
                CssClass="twitterStyleTextbox" AutoPostBack="True"></asp:TextBox>
            </td>
        <td  width="5%">
            <asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
                ImageUrl="~/images/Search.png" TabIndex="999" Width="23px" />
            </td>
        <td width="20%"><label>Asset Name</label></td>
        <td  width="27%">
            <asp:TextBox ID="TxtAssetName" runat="server" Width="100%" 
                CssClass="twitterStyleTextbox" TabIndex="1"></asp:TextBox></td>
        <td width="2%"></td>
        </tr>
        <tr>
        <td width="2%"></td>
        <td width="20%"><label>Asset Group</label></td>
        <td  width="27%"><asp:DropDownList ID="CmbAssetGroup" runat="server" Width="100%" 
                TabIndex="2"></asp:DropDownList></td>
        <td  width="2%"></td>
        <td width="20%"><label>Depriciation %</label></td>
        <td  width="27%">
            <asp:TextBox ID="TxtAssetDepPerc" runat="server" Width="100%" onkeypress="return isFloatKey(event);" 
                CssClass="twitterStyleTextbox" MaxLength="6" TabIndex="3"></asp:TextBox></td>
        <td width="2%"></td>
       
         <tr>
        <td width="2%"></td>
        <td width="20%"><label>Op Stock Qty</label></td>
        <td  width="27%">
            <asp:TextBox ID="TxtAssOpQty" runat="server" Width="100%" onkeypress="return isFloatKey(event);" 
                CssClass="twitterStyleTextbox" TabIndex="4"></asp:TextBox></td>
         <td  width="2%"></td>
         <td width="20%"><label>Value</label></td>
        <td  width="27%">
            <asp:TextBox ID="TxtAssetOpValue" runat="server" Width="100%" onkeypress="return isFloatKey(event);" 
                CssClass="twitterStyleTextbox" TabIndex="5"></asp:TextBox></td>
        <td width="2%"></td>
        </tr>
         <tr>
        <td width="2%"></td>
        <td width="20%"><label>Asset Head</label></td>
        <td  width="27%"><asp:DropDownList ID="CmbAssetHead" runat="server" Width="100%" 
                TabIndex="6"></asp:DropDownList></td>
        <td  width="2%"></td>
        <td width="20%"><label>Dep Head</label></td>
        <td  width="27%">
            <asp:DropDownList ID="CmbAssetDepHead" runat="server" Width="100%" TabIndex="7"></asp:DropDownList>
        </td>
        <td width="2%"></td>
        </tr>
         
        
            <tr>
                <td width="2%" class="style1">
                    </td>
                <td width="20%" class="style1">
                    <label>Open Date</label>&nbsp;</td>
                <td width="27%" class="style1">
                    <asp:TextBox ID="TxtOpenDate" runat="server" Width="98%"></asp:TextBox>
                      <asp:CalendarExtender ID="Txt_DemandDate_CalendarExtender" runat="server" 
                      Format="dd/MM/yyyy" TargetControlID="TxtOpenDate">
                  </asp:CalendarExtender>
                </td>
                <td width="2%" class="style1">
                    </td>
                <td width="20%" class="style1"><label>Asset Branch</label>
                    </td>
                <td width="27%" class="style1">
                    <asp:DropDownList ID="DrpBranch" runat="server" Width="98%" AutoPostBack="True">
                    </asp:DropDownList>
                    </td>
                <td width="2%" class="style1">
                    </td>
            </tr>
         
        
         <tr>
        <td width="2%"></td>
        <td width="20%"></td>
        <td  width="27%"></td>
         <td  width="2%"></td>
        <td width="20%"></td>
        <td  width="27%"></td>
        <td width="2%"></td>
        </tr>
        
        <tr>
            <td width="2%"></td>
            <td width="20%"> </td>
            <td colspan="3" style="width: 47%">
                <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                    meta:resourcekey="CmdSaveResource1" OnClientClick="return ValidateSave();" 
                    style="height: 33px" TabIndex="8" />
                <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" 
                    meta:resourcekey="CmdSaveResource1" TabIndex="9" />
                <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/images/Close.png" 
                    meta:resourcekey="CmdClearResource1"  TabIndex="10" style="height: 33px" />
           </td>
            <td width="27%"></td>
            <td width="2%"></td>
        </tr>
        
        </table>
        

</div>
</ContentTemplate>
</asp:UpdatePanel>

</asp:Content>

