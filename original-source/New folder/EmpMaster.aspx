<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="EmpMaster.aspx.vb" Inherits="SMARTCORE_UI.EmpMaster" 
    title="Employee Master" %>
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
<legend>EMPLOYEE DETAILS</legend>  
<br />
<table width = "100%">
<tr>
	<td width = "2%"></td>
	<td width = "23%">
                                                    <label>
                                                    Employee ID</label></td>
	<td width = "24%">
                                                                <asp:TextBox ID="TxtID" runat="server"
                                                                    CssClass="twitterStyleTextbox" TabIndex="6" Width="79%" 
                                                                    AutoPostBack="True"></asp:TextBox>
                                                                <asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
                                                                    ImageUrl="~/images/Search.png" Width="23px" />
                                                            </td>
	<td width = "2%"></td>
	<td width = "23%">
                                                    <label>
                                                    Joining Date</label></td>
	<td width = "24%">
                                                                <asp:TextBox ID="TxtDoj" runat="server"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="6" Width="99%"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
            Format="dd/MM/yyyy" TargetControlID="TxtDoj">
        </asp:CalendarExtender>
                                                            </td>
	<td width = "2%"></td>
</Tr>
<Tr>
	<td width = "2%"></td>
	<td width = "23%">
                                                    <label>
                                                    Employee Name</label></td>
	<td width = "24%">
                                                                <asp:TextBox ID="TxtName" runat="server"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="6" Width="99%"></asp:TextBox>
                                                            </td>
	<td width = "2%"></td>
	<td width = "23%">
                                                    <label>
                                                    Qualification</label></td>
	<td width = "24%">
            <asp:DropDownList ID="CmbQualification" runat="server" Width="100%" 
                meta:resourcekey="CmbOccupationResource1" TabIndex="12">
            </asp:DropDownList>
        </td>
	<td width = "2%"></td>
</Tr>
<Tr>
	<td width = "2%"></td>
	<td width = "23%">
                                                    <label>
                                                    Relative Name</label></td>
	<td width = "24%">
                                                                <asp:TextBox ID="TxtRelName" runat="server"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="6" Width="99%"></asp:TextBox>
                                                            </td>
	<td width = "2%"></td>
	<td width = "23%">
                                                    <label>
                                                    Branch</label></td>
	<td width = "24%">
            <asp:DropDownList ID="CmbBranch" runat="server" Width="100%" 
                meta:resourcekey="CmbOccupationResource1" TabIndex="12">
            </asp:DropDownList>
        </td>
	<td width = "2%"></td>
</Tr>
<Tr>
	<td width = "2%"></td>
	<td width = "23%">
                                                    <label>
                                                    Employee Address</label></td>
	<td width = "24%">
                                                                <asp:TextBox ID="TxtAdd1" runat="server"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="6" Width="99%"></asp:TextBox>
                                                            </td>
	<td width = "2%"></td>
	<td width = "23%">
        <label>
                                                    Designation</label></td>
	<td width = "24%">
            <asp:DropDownList ID="CmbDesign" runat="server" Width="100%" 
                meta:resourcekey="CmbOccupationResource1" TabIndex="12">
            </asp:DropDownList>
                                                            </td>
	<td width = "2%"></td>
</Tr>
<Tr>
	<td width = "2%"></td>
	<td width = "23%"></td>
	<td width = "24%">
                                                                <asp:TextBox ID="TxtAdd2" runat="server"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="6" Width="99%"></asp:TextBox>
                                                            </td>
	<td width = "2%"></td>
	<td width = "23%">
        <label>
                                                    Fixation Date</label></td>
	<td width = "24%">
                                                                <asp:TextBox ID="TxtFixDate" runat="server"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="6" Width="99%"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender3" runat="server" 
            Format="dd/MM/yyyy" TargetControlID="TxtFixDate">
        </asp:CalendarExtender>
                                                            </td>
	<td width = "2%"></td>
</Tr>
<Tr>
	<td width = "2%"></td>
	<td width = "23%">
                                                    </td>
	<td width = "24%">
                                                                <asp:TextBox ID="TxtAdd3" runat="server"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="6" Width="99%"></asp:TextBox>
                                                            </td>
	<td width = "2%"></td>
	<td width = "23%">
                                                    <label>
                                                    Fixation Amount</label></td>
	<td width = "24%">
                                                                <asp:TextBox ID="TxtFixAmt" runat="server"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="6" Width="99%"></asp:TextBox>
                                                            </td>
	<td width = "2%"></td>
</Tr>
<Tr>
	<td width = "2%"></td>
	<td width = "23%">
                                                    <label>
                                                    Phone No</label></td>
	<td width = "24%">
                                                                <asp:TextBox ID="TxtPhoneNo" runat="server"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="6" Width="99%" MaxLength="12" onkeypress="return isFloatKey(event);"></asp:TextBox>
                                                            </td>
	<td width = "2%"></td>
	<td width = "23%">
                                                    <label>
                                                    Salary Account 
                                                    </label>
                                                    
    </td>
	<td width = "24%">
                                                                <asp:TextBox ID="TxtSalAcc" runat="server"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="6" Width="79%" AutoPostBack="True"></asp:TextBox>
                                                                <asp:ImageButton ID="ImgSLAcc" runat="server" Height="20px" Visible=false 
                                                                    ImageUrl="~/images/Search.png" Width="23px" />
                                                            </td>
	<td width = "2%">&nbsp;</td>
</Tr>
<Tr>
	<td width = "2%"></td>
	<td width = "23%">
                                                    <label>
                                                    Mobile No</label></td>
	<td width = "24%">
                                                                <asp:TextBox ID="TxtMblNo" runat="server"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="6" Width="99%" MaxLength="12" onkeypress="return isFloatKey(event);"></asp:TextBox>
    </td>
	<td width = "2%"></td>
	<td width = "23%"><label>Account Holder </label>
    </td>
	<td width = "24%">
                                                                <asp:TextBox ID="TxtAccName" runat="server"
                                                                    
            CssClass="twitterStyleTextbox" TabIndex="6" Width="99%"></asp:TextBox>
                                                            </td>
	<td width = "2%"></td>
</Tr>
<Tr>
	<td width = "2%"></td>
	<td width = "23%">
        <label>
                                                    Date of Birth &amp; Age</label></td>
	<td width = "24%">
        <asp:TextBox ID="TxtDob" runat="server" onblur="CalculateAge(this)"
        CssClass="twitterStyleTextbox" TabIndex="6" Width="55%"></asp:TextBox>
        <asp:CalendarExtender ID="CalendarExtender2" runat="server" 
            Format="dd/MM/yyyy" TargetControlID="TxtDob">
        </asp:CalendarExtender>

        <asp:TextBox ID="TxtAge" runat="server"
        CssClass="twitterStyleTextbox" TabIndex="6" Width="22%"></asp:TextBox>                                                           </td>
	<td width = "2%"></td>
	<td width = "23%">
                                                    <label>
                                                    Status</label></td>
	<td width = "24%">
            <asp:DropDownList ID="CmbStatus" runat="server" Width="100%" 
                meta:resourcekey="CmbOccupationResource1" TabIndex="12">
            </asp:DropDownList>
        </td>
	<td width = "2%"></td>
</Tr>
<Tr>
	<td width = "2%"></td>
	<td width = "23%"><label> Employee Category</label>
        </td>
	<td width = "24%">  <asp:DropDownList ID="CmbStafftype" runat="server" Width="100%" 
                meta:resourcekey="CmbStafftype" TabIndex="12" Visible ="True" >
            </asp:DropDownList> </td>
	<td width = "2%"></td>
	<td width = "23%"></td>
	<td width = "24%"></td>
	<td width = "2%"></td>
</Tr>
<Tr>
	<td width = "2%"></td>
	<td width="23%">
                    <asp:HiddenField ID="HdnAccid" runat="server" />
                </td>
	<td width = "24%"></td>
    <td width="2%">
        </td>
    <td width="23%">
        </td>
    <td width="24%">
        </td>
    <td width="2%">
        </td>
</tr>
<Tr>
	<td width = "2%"></td>
	<td colspan="5" align  ="center">
                    <asp:ImageButton ID="CmdSave" runat="server" ImageUrl="~/images/save.png" 
                        meta:resourcekey="CmdSaveResource1" TabIndex="56"  />
                    <asp:ImageButton ID="CmdClear" runat="server" ImageUrl="~/images/Close.png" 
                        meta:resourcekey="CmdClearResource1" TabIndex="57" />
                </td>
	<td width = "2%"></td>
</tr>
</table>
</div>
        </ContentTemplate>
        </asp:UpdatePanel> 

</asp:Content>
