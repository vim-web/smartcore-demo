<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="PaySettings.aspx.vb" Inherits="SMARTCORE_UI.PaySettings" 
    title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
     <script type='text/javascript' src="Scripts/Common.js"></script>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  
        EnablePageMethods="True" CombineScripts="True"> 
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
        </Scripts>
        </asp:ToolkitScriptManager>
    <div class="gradientBoxesWithOuterShadows" style="width: 80%; margin-left:10%; margin-right:10%;margin-top:5%; font-family: Tahoma" >                
        <legend>PAY SETTINGS</legend> <br />
<table width = "100%">
<tr>
	<td width = "2%"></td>
	<td width = "23%">
        <label>Employee ID</label>
    </td>
	<td width = "24%">
        <asp:TextBox ID="TxtID" runat="server" CssClass="twitterStyleTextbox" 
            Width="69%" AutoPostBack="True"></asp:TextBox>
        <asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
            ImageUrl="~/images/Search.png" Width="23px" />
    </td>
	<td width = "2%"></td>
	<td width = "23%">
        <label> Name</label>
    </td>
	<td width = "24%">
        <asp:TextBox ID="TxtName" runat="server" CssClass="twitterStyleTextbox" 
            TabIndex="1" Width="99%"></asp:TextBox>
    </td>
	<td width = "2%"></td>
</tr>
    <tr>
        <td colspan="7">
    </tr>
    <tr>
        <td width="2%">
            </td>
        <td colspan="5">
            <asp:GridView ID="DgvPaySetting" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="PAYCODE" TabIndex="30" Width="100%" CssClass="grid-view">
                <Columns>
                    <asp:BoundField DataField="PayCode" HeaderText="PAYCODE" Visible="False" />
                    <asp:BoundField DataField="PayHead" HeaderText="Pay Head">
                        <HeaderStyle Width="20%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="EmpAmt" HeaderText="EmpAmt" 
                        DataFormatString="{0:n2}">
                        <HeaderStyle Width="10%" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="EmpPer" HeaderText="Emp%" DataFormatString="{0:n2}">
                        <HeaderStyle Width="10%" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="BankAmt" HeaderText="BankAmt" 
                        DataFormatString="{0:n2}" >
                        <HeaderStyle Width="10%" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="BankPer" HeaderText="Bank%" 
                        DataFormatString="{0:n2}">
                        <HeaderStyle Width="10%" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AdnlAmt" HeaderText="Adnl Amt" 
                        DataFormatString="{0:n2}">
                        <HeaderStyle Width="10%" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AccNo" HeaderText="Acc No">
                        <HeaderStyle Width="15%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                        <HeaderStyle Width="15%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AccID" HeaderText="AccID" Visible="False" />
                    <asp:CommandField DeleteText="&lt;img src='./images/del.jpg'&gt;" 
                        ShowDeleteButton="True">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:CommandField>
                </Columns>
                <AlternatingRowStyle BackColor="#F5F4FF" />
            </asp:GridView>
        </td>
        <td width="2%">
            </td>
    </tr>
    <tr>
        <td colspan="7">
    </tr>

    </table>
    <table width ="100%">
    <tr>
        <td width = "2%"></td>
	    <td width = "36%">
            <label>
            Pay Head</label> </td>
	    <td width = "12%">
            <label>
            Employee Amt</label></td>
	    <td width = "12%">
            <label>
            Employee %</label></td>
	    <td width = "12%">
            <label>
            Bank Amt</label></td>
	    <td width = "12%">
            <label>
            Bank %</label></td>
	    <td width = "12%">
            <label>
            Adnl Amount</label></td>
	    <td width = "2%"></td>
    </tr>
        <tr>
            <td width="2%">
                </td>
            <td width="36%">
                <asp:DropDownList ID="CmbPayHead" runat="server" 
                    meta:resourcekey="CmbOccupationResource1" TabIndex="12" Width="100%">
                </asp:DropDownList>
            </td>
            <td width="12%">
                <asp:TextBox ID="TxtEmpAmt" runat="server" CssClass="twitterStyleTextbox" 
                    TabIndex="6" Width="95%" onkeypress="return isFloatKey(event);"  style="text-align:right"  ></asp:TextBox>
            </td>
            <td width="12%">
                <asp:TextBox ID="TxtEmpPer" runat="server" CssClass="twitterStyleTextbox" 
                    TabIndex="6" Width="95%" onkeypress="return isFloatKey(event);"  style="text-align:right"  ></asp:TextBox>
            </td>
            <td width="12%">
                <asp:TextBox ID="TxtBankAmt" runat="server" CssClass="twitterStyleTextbox" 
                    TabIndex="6" Width="95%" onkeypress="return isFloatKey(event);"  style="text-align:right"  ></asp:TextBox>
            </td>
            <td width="12%">
                <asp:TextBox ID="TxtBankPer" runat="server" CssClass="twitterStyleTextbox" 
                    TabIndex="6" Width="95%" onkeypress="return isFloatKey(event);"  style="text-align:right"  ></asp:TextBox>
            </td>
            <td width="12%">
                <asp:TextBox ID="TxtAdnlAmt" runat="server" CssClass="twitterStyleTextbox" 
                    TabIndex="6" Width="95%" onkeypress="return isFloatKey(event);"  style="text-align:right"  ></asp:TextBox>
            </td>
            <td width="2%">
                </td>
        </tr>
    </table>
    
    <table width ="100%">
    <tr>
        <td width="2%">
            </td>
        <td width="23%">
            <label>
            Account No</label></td>
        <td width="24%">
            <label>
            Account Holder</label></td>
        <td width="2%">
            </td>
        <td width="23%"><label>Remarks</label>
            </td>
        <td width="24%">
            </td>
        <td width="2%">
            </td>
    </tr>
        <tr>
            <td width="2%">
                </td>
            <td width="23%">
                <asp:TextBox ID="TxtAccNo" runat="server" AutoPostBack="True" 
                    CssClass="twitterStyleTextbox" TabIndex="6" Width="95%"></asp:TextBox>
            </td>
            <td width="24%">
                <asp:TextBox ID="TxtAccName" runat="server" CssClass="twitterStyleTextbox" 
                    TabIndex="6" Width="99%"></asp:TextBox>
            </td>
            <td width="2%">
                </td>
            <td width="23%">
                <asp:TextBox ID="TxtRemarks" runat="server" CssClass="twitterStyleTextbox" 
                    TabIndex="6" Width="99%"></asp:TextBox>
                </td>
            <td width="24%">
                <asp:ImageButton ID="CmdAdd" runat="server" Height="21px" 
                    ImageUrl="~/Images/AddBtn.png" TabIndex="25" />
                <asp:ImageButton ID="CmdClear" runat="server" Height="21px" 
                    ImageUrl="~/Images/ClearBtn.png" TabIndex="26" />
                </td>
            <td width="2%">
                </td>
        </tr>
    <tr>
        <td width="2%">
            </td>
        <td width="23%">
            </td>
        <td colspan="3" style="width: 47%">
            </td>
        <td width="24%" align="center">
            &nbsp;</td>
        <td width="2%">
            </td>
    </tr>
    <tr>
        <td width="2%">
            </td>
        <td colspan="5" align="center">
            <asp:HiddenField ID="HdnAccid" runat="server" />
        </td>
        <td width="2%">
            </td>
    </tr>
        <tr>
            <td width="2%">
                </td>
            <td align="center" colspan="5">
                <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                    meta:resourcekey="CmdSaveResource1" TabIndex="56" />
                <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/images/Close.png" 
                    meta:resourcekey="CmdClearResource1" TabIndex="57" />
                </td>
            <td width="2%">
                </td>
        </tr>
</table>
</div>
</ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>
