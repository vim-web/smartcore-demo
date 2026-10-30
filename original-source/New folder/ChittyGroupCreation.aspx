<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ChittyGroupCreation.aspx.vb" Inherits="SMARTCORE_UI.ChittyGroupCreation" MasterPageFile="~/SMARTCORE.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
<script type='text/javascript' src="Scripts/Common.js"></script> 
<script type='text/javascript'>

    function ValidateSave() {
        var GroupCode = document.getElementById('<%=TxtGroupCode.ClientID%>');
        if (parseFloat(GroupCode.value) == 0 || GroupCode.value == "") {
            alertify.error('Group Code Not Entered, Please Check');
            GroupCode.focus();
            return false;
        }

        var GroupName = document.getElementById('<%=TxtGroupName.ClientID%>');
        if (GroupName.value == "") {
            alertify.error('Group Name Not Entered, Please Check');
            GroupName.focus();
            return false;
        }        

        var StartDate = document.getElementById('<%=TxtStartDate.ClientID%>');
        if (StartDate.value == "") {
            alertify.error('Start Date Not Entered, Please Check');
            StartDate.focus();
            return false;
        }        
    
        var Sala = document.getElementById('<%=TxtSala.ClientID%>');
        if (parseFloat(Sala.value) == 0 || Sala.value == "") {
            alertify.error('Sala Amount Not Entered, Please Check');
            Sala.focus();
            return false;
        }

        var MemberNo = document.getElementById('<%=TxtNoOfMember.ClientID%>');
        if (parseFloat(MemberNo.value) == 0 || MemberNo.value == "") {
            alertify.error('No of Members Not Entered, Please Check');
            MemberNo.focus();
            return false;
        }

        var NoofMonths = document.getElementById('<%=TxtNoOfMonths.ClientID%>');
        if (parseFloat(NoofMonths.value) == 0 || NoofMonths.value == "") {
            alertify.error('No of Months Not Entered, Please Check');
            NoofMonths.focus();
            return false;
        }        
        return true;
    }

</script>

<div class="gradientBoxesWithOuterShadows" width: 100%; margin-left:25%; margin-right:25%; > 
<legend>CHITTY GROUP CREATION</legend>               
<br />

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
     
    <table width="100%">
    <tr>
    <td width="2%"></td>
    <td width="24%">
    <label>Branch</label></td>
    <td width="24%">
        <table width="100%">
        <tr>
        <td width="30%">
            <asp:TextBox ID="TxtBranch" CssClass="twitterStyleTextbox" Width="90%" runat="server" AutoPostBack="True"></asp:TextBox>
        </td>
        <td width="70%">
        <asp:DropDownList ID="CmbBranch" Width="100%" runat="server" AutoPostBack="True">
        </asp:DropDownList>
        </td>
        </tr>
        </table>
        </td>
        <td width="2%"></td>
    <td width="23%">
        <label>
        No Of Members</label></td>
    <td width="23%">
        <asp:TextBox ID="TxtNoOfMember" runat="server" 
            onkeypress="return isNumberKey(event);" Width="97%" 
            CssClass="twitterStyleTextbox" MaxLength="3"></asp:TextBox>
        </td>
    <td width="2%"></td>
    </tr>
    <tr>
    <td></td>
    <td>
        <label>
        Scheme</label></td>
    <td>
    <table width="100%">
        <tr>
        <td width="30%">
            <asp:TextBox ID="TxtScheme" runat="server" AutoPostBack="True" Width="90%" CssClass="twitterStyleTextbox"></asp:TextBox>
        </td>
        <td width="70%">
            <asp:DropDownList ID="CmbScheme" runat="server" AutoPostBack="True" Width="100%" CssClass="twitterStyleTextbox">
            </asp:DropDownList>
        </td>
        </tr>
        </table>  
        </td>
        <td></td>
    <td>
        <label>
        No Of Months</label></td>
    <td>
        <asp:TextBox ID="TxtNoOfMonths" runat="server" Width="97%" 
            CssClass="twitterStyleTextbox" MaxLength="3"></asp:TextBox>
        </td>
    <td></td>
    </tr>
    <tr>
    <td>&nbsp;</td>
    <td>
        <label>
        Group Code</label></td>
    <td>
        <table width="100%">
        <tr>
        <td width="30%">
            <asp:TextBox ID="TxtGroupCode" runat="server" Width="90%" CssClass="twitterStyleTextbox"></asp:TextBox>
        </td>
        <td width="70%">
            <asp:TextBox ID="TxtGroupName" runat="server" Width="97%" CssClass="twitterStyleTextbox"></asp:TextBox>
        </td>
        </tr>
        </table>  
        </td>
        <td></td>
    <td>
        <label>
        Suspense Liability A/c</label></td>
    <td>
        <asp:TextBox ID="TxtSuspenseLiabAc" runat="server" Width="97%" 
            CssClass="twitterStyleTextbox" AutoPostBack="True"></asp:TextBox>
        </td>
    <td>&nbsp;</td>
    </tr>
    <tr>
    <td>&nbsp;</td>
    <td><label>Start Date</label></td>
    <td>
        <asp:TextBox ID="TxtStartDate" runat="server" Width="97%" CssClass="twitterStyleTextbox"></asp:TextBox>
        <asp:CalendarExtender ID="TxtStartDate_CalendarExtender" runat="server" TargetControlID="TxtStartDate" Format="dd/MM/yyyy" >
        </asp:CalendarExtender>
    </td>
    <td></td>
    <td><label> Bank Foreman Cust ID</label></td>
    <td>
        <asp:TextBox ID="TxtForemanCustID" runat="server" Width="97%" 
            CssClass="twitterStyleTextbox"></asp:TextBox>
    </td>
    <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td><label>Sala</label></td>
        <td><asp:TextBox ID="TxtSala" runat="server" onkeypress="return isNumberKey(event);" Width="97%" CssClass="twitterStyleTextbox"></asp:TextBox></td>
        <td></td>
        <td>
            <label>
            Commission %</label></td>
        <td>
            <asp:TextBox ID="TxtCommPer" runat="server" CssClass="twitterStyleTextbox" 
                MaxLength="10" onkeypress="return isNumberKey(event);" Width="97%"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Fine % Prized</label></td>
            <td>
                <asp:TextBox ID="TxtFinePerPrized" runat="server" 
                    CssClass="twitterStyleTextbox" onkeypress="return isNumberKey(event);" 
                    Width="97%"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Fine % Non Prized</label></td>
            <td>
                <asp:TextBox ID="TxtFinePerNonPrized" runat="server" 
                    CssClass="twitterStyleTextbox" onkeypress="return isNumberKey(event);" 
                    Width="97%"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Receipt Head</label></td>
            <td>
                <asp:DropDownList ID="CmbRctHead" runat="server" 
                    Width="100%">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Payment Head</label></td>
            <td>
                <asp:DropDownList ID="CmbPayHead" runat="server" 
                    Width="100%">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Discount Head</label></td>
            <td>
                <asp:DropDownList ID="CmbDisc" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Commission A/c</label></td>
            <td>
                <asp:DropDownList ID="CmbCommission" runat="server" 
                    Width="100%">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Forfeit Month</label></td>
            <td>
                <asp:TextBox ID="TxtForefeitMth" runat="server" CssClass="twitterStyleTextbox" 
                    onkeypress="return isNumberKey(event);" Width="97%"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Discount Forfeit&nbsp; Head</label></td>
            <td>
                <asp:DropDownList ID="CmbDiscFor" runat="server" 
                    Width="100%">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Foreman Head</label></td>
            <td>
                <asp:DropDownList ID="CmbForeman" runat="server" 
                    Width="100%">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Foreman Discount Head</label></td>
            <td>
                <asp:DropDownList ID="CmbFormanDisc" runat="server" 
                    Width="100%">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Admission Fee Head</label></td>
            <td>
                <asp:DropDownList ID="CmbAdmnFee" runat="server" 
                    Width="100%">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
            <td>
                <label>
                Interest Head</label></td>
            <td>
                <asp:DropDownList ID="CmbIntHead" runat="server" 
                    Width="100%">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        
    </table>
    <div id ="OthDiv" runat="server" visible="false">
    <table>
    </div>
       <table width = "100%">
       <tr>
           <td>
               &nbsp;</td>
           <td align="center">
               <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" OnClientClick="return ValidateSave();"/>
               &nbsp;<asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/Images/Clear.png" />
               &nbsp;<asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/images/Close.png" />
           </td>
           <td>
               &nbsp;</td>
           </tr>
        </tr>
           <tr>
               <td>
                   &nbsp;</td>
               <td align="center">
                   <asp:HiddenField ID="HdnSuspAccid" runat="server" />
               </td>
               <td>
                   &nbsp;</td>
           </tr>
        </table>
    </div>

</ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>
