<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="LoanSecLandAndBuilding.aspx.vb" Inherits="SMARTCORE_UI.LoanSecLandAndBuilding" 
    title="Untitled Page" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />  
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  
        EnablePageMethods="True" CombineScripts="True"> 
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
        </Scripts>
        </asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" height: 640px;width: 100%; margin-left:25%; margin-right:25%;>    
<table width="100%">
  <tr>
    <td width="2%"></td>
    <td width="16%">
           
            <asp:Label ID="Label3" runat="server" Text="Enter AccountNo :" Width ="100%" 
            style="font-weight: 700" Font-Bold="True" Font-Size="11pt"></asp:Label>
            </td>
    <td width="17%">
            <asp:ImageButton ID="ImgSrch" runat="server" Height="20px" 
                ImageUrl="~/images/Search.png" Width="20px" />                
                            </td>
    <td width="2%"></td>
    <td width="15%"></td>
    <td width="15%"></td>
    <td width="2%"></td>
    <td width="14%"></td>
    <td width="15%"></td>
    <td width="2%"></td>
    
  </tr>
  <tr>
    <td></td>
    <td>
        </td>
    <td>        <asp:TextBox ID="TxtAccountNo" runat="server" Width="100%" 
            AutoPostBack="True"></asp:TextBox>
                    </td>
    <td></td>
    <td>
        <asp:Label ID="lblName" runat="server" Text="" Width ="100%"></asp:Label></td>
    <td>        <asp:Label ID="lblSchName" runat="server" Width ="100%"></asp:Label></td></td>
    <td></td>
    <td>        <asp:Label ID="lblAccId" runat="server" Width ="100%" Visible="False"></asp:Label></td></td>
    <td>        <asp:Label ID="lblSchCode" runat="server" Width ="100%" Visible="False"></asp:Label></td>
    <td></td>
    
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td colspan =2>
        &nbsp;</td>
    <td>
                &nbsp;</td>
    <td>&nbsp;</td>
    <td>
        &nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>
        &nbsp;</td>
    <td>&nbsp;</td>
 
  </tr>
  <tr>
    <td></td>
    <td colspan =2>
        <asp:Label runat="server" Text="Deed Details" Width ="100%" 
            style="font-weight: 700" Font-Bold="True" Font-Size="11pt"></asp:Label></td>
    <td>
                </td>
    <td></td>
    <td>
        <asp:CheckBox ID="ChkTax" runat="server" Text="Tax Details" 
            style="font-weight: 700" AutoPostBack="True" Font-Bold="True" />
                    </td>
    <td></td>
    <td></td>
    <td>
        <asp:CheckBox ID="ChkInsurance" runat="server" Text="Insurance Details" 
            style="font-weight: 700" AutoPostBack="True" Font-Bold="True" />
                    </td>
  <td></td>
 <%--     <td></td>--%>
  </tr>
  <tr>
    <td></td>
    <td><label width ="100%">No. :</label></td>
    <td>
        <asp:TextBox ID="TxtDeedNo" runat="server" Width="100%"></asp:TextBox>
                    </td>
    <td></td>
     <td><label>No. :</label></td
    <td>
       <%-- <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>--%>
                    </td>
    <td>
        <asp:TextBox ID="TxtTaxNo" runat="server" Width="100%"></asp:TextBox>
      </td>
     <td></td
    <td>
      <%--  <asp:TextBox ID="TextBox15" runat="server"></asp:TextBox>--%>
                    </td>
    <td><label>No. :</label></td>
     <td>
        <asp:TextBox ID="TxtInsuranceNo" runat="server" Width="100%"></asp:TextBox>
        
                    </td>
    <td></td>
  </tr>
  <tr>
    <td></td>
    <td><label>Date. :</label></td>
    <td>
        <asp:TextBox ID="TxtDeedDate" runat="server" Width="100%"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                TargetControlID="TxtDeedDate">
            </asp:CalendarExtender>
                    </td>
    <td></td>
    <td><label>Date. :</label></td>
    <td>
        <asp:TextBox ID="TxtTaxDate" runat="server" Width="100%"></asp:TextBox>
                              <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" 
                TargetControlID="TxtTaxDate">
            </asp:CalendarExtender>
                    </td>
    <td></td>
   <td><label>Date. :</label></td>
    <td>
        <asp:TextBox ID="TxtInsuranceDate" runat="server" Width="100%"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                TargetControlID="TxtInsuranceDate">
            </asp:CalendarExtender>
                    </td>
    <td></td>
  </tr>
  <tr>
    <td></td>
    <td><label>Prior No. :</label></td>
    <td>
        <asp:TextBox ID="TxtPriorNo" runat="server" Width="100%"></asp:TextBox>
                    </td>
    <td></td>
    <td><label>Details. :</label></td>
    <td>
        <asp:TextBox ID="TxtTaxDetails" runat="server" Width="100%"></asp:TextBox>
                    </td>
    <td></td>
    <td><label>DueDate. :</label></td>
    <td>
                    <asp:TextBox ID="TxtDueDate" runat="server" Width="100%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="TxtDueDate">
                </asp:CalendarExtender>
    </td>
    <td></td>
  </tr>
    
        
        <tr>
            <td>

            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <label>
                Extend. :</label></td>
            <td>
                <asp:TextBox ID="TxtExtend" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
                <label>
                Recovery No :</label></td>
            <td>
                <asp:TextBox ID="TxtRecoveryNo" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <label>
                District. :</label></td>
            <td>
                <asp:TextBox ID="TxtDistrict" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
                <label>
                Other Details :</label>
            </td>
            <td>
                <asp:TextBox ID="TxtOtherDetails" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <label>
                Tehsil. :</label></td>
            <td>
                <asp:TextBox ID="TxtTehsil" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <label>
                Panchayath/Muncipality :</label></td>
            <td>
                <asp:TextBox ID="TxtPunchMun" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
                <asp:DropDownList ID="DdlModeOfOwnerdhip" runat="server" Visible="False" 
                    Width="100%">
                    <asp:ListItem>Single</asp:ListItem>
                    <asp:ListItem></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" style="font-weight: 700" 
                    Text="Ownership Details" Visible="False" Width="100%"></asp:Label>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <label>
                Village. :</label></td>
            <td>
                <asp:TextBox ID="TxtVillage" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <label>
                Sub Reg Office. :</label></td>
            <td>
                <asp:TextBox ID="TxtSubRegOffice" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <label>
                Market Value. :</label></td>
            <td>
                <asp:TextBox ID="TxtMarketValue" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
                <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                    meta:resourcekey="CmdSaveResource1" TabIndex="56" />
            </td>
            <td>
                <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/images/Close.png" 
                    meta:resourcekey="CmdClearResource1" PostBackUrl="~/Main.aspx" TabIndex="57" />
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    
</table>
</div> 
    </ContentTemplate> 
    </asp:UpdatePanel> 
</asp:Content>
