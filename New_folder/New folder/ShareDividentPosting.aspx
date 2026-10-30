
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="ShareDividentPosting.aspx.vb" Inherits="SMARTCORE_UI.ShareDividentPosting" title="Share Divident Posting" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
    <script type='text/javascript' src="Scripts/Common.js"></script> 
        <script language="javascript" type="text/javascript">

        function validatedate(inputText) {
            var dateformat = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
            // Match the date format through regular expression  
            if (inputText.value.match(dateformat)) {
                //  document.form1.text1.focus();  
                //Test which seperator is used '/' or '-'  
                var opera1 = inputText.value.split('/');
                var opera2 = inputText.value.split('-');
                lopera1 = opera1.length;
                lopera2 = opera2.length;
                // Extract the string into month, date and year  
                if (lopera1 > 1) {
                    var pdate = inputText.value.split('/');
                }
                else if (lopera2 > 1) {
                    var pdate = inputText.value.split('-');
                }
                var dd = parseInt(pdate[0]);
                var mm = parseInt(pdate[1]);
                var yy = parseInt(pdate[2]);
                // Create list of days of a month [assume there is no leap year by default]  
                var ListofDays = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
                if (mm == 1 || mm > 2) {
                    if (dd > ListofDays[mm - 1]) {
                        alertify.error('Invalid date format! Please enter a valid date!');
                        inputText.focus();
                        return false;
                    }
                }
                if (mm == 2) {
                    var lyear = false;
                    if ((!(yy % 4) && yy % 100) || !(yy % 400)) {
                        lyear = true;
                    }
                    if ((lyear == false) && (dd >= 29)) {
                        alertify.error('Invalid date format! Please enter a valid date!');
                        inputText.focus();
                        return false;
                    }
                    if ((lyear == true) && (dd > 29)) {
                        alertify.error('Invalid date format! Please enter a valid date!');
                        inputText.focus();
                        return false;
                    }
                }
            }
            else {
                alertify.error("Invalid date format! Please enter a valid date!");
                inputText.focus();
                document.form1.text1.focus();
                return false;
            }
        }  

</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True">
    <Scripts>
            <asp:ScriptReference Path="~/Scripts/FixFocus.js" />
    </Scripts>
</asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 50%; margin-left:25%; margin-right:25%;margin-top:1%;">     
    <legend>DIVIDENT POSTING</legend> 
    <table width="100%">
        <tr>
            <td width="2%" height="100%"></td>
            <td width="20%" height="100%">
                <label>Branch</label>
            </td>
            <td width="2%" height="100%"></td>
            <td width="20%" height="100%">
                <asp:TextBox ID="TxtBrCode" runat="server" AutoPostBack="True" 
                    CssClass="twitterStyleTextbox" TabIndex="3" Width="100%"></asp:TextBox>
            </td>
            <td width="2%" height="100%"></td>
            <td width="52%" height="100%">
                <asp:DropDownList ID="CmbBranch" runat="server" TabIndex="2" Width="100%" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td width="2%" height="100%"></td>
        </tr>
        <tr>
            <td width="2%" height="100%"></td>
            <td width="20%" height="100%">
                <label>Scheme</label>
            </td>
            <td width="2%" height="100%"></td>
            <td width="20%" height="100%"> 
                <asp:TextBox ID="TxtSchCode" runat="server" AutoPostBack="True" 
                    CssClass="twitterStyleTextbox" TabIndex="3" Width="100%"></asp:TextBox>
            </td>
            <td width="2%" height="100%">
            <td width="52%" height="100%">
                <asp:DropDownList ID="CmbScheme" runat="server" AutoPostBack="True" 
                    TabIndex="3" Width="100%">
                </asp:DropDownList>   
            </td>
             <td width="2%" height="100%"></td>
        </tr>
        
        <tr>
            <td width="2%" height="100%"></td>
            <td width="20%" height="100%">
                <label>From Date</label>
            </td>
            <td width="2%" height="100%"></td>
            <td colspan=3>
                  <asp:TextBox ID="TxtFromDate" runat="server" Width="30%" AutoPostBack="True" OnChange="validatedate(this);"></asp:TextBox>
                  <asp:CalendarExtender ID="TxtFromDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                      TargetControlID="TxtFromDate">
                  </asp:CalendarExtender>

                  <asp:Label ID="LblDateDtls" runat="server" Text=""></asp:Label>

            </td>
            <td width="2%" height="100%"></td>
        </tr>
        
        <tr>
            <td width="2%" height="100%"></td>
            <td width="20%" height="100%">
                <label>Percent</label>
            </td>
            <td width="2%" height="100%"></td>
            <td colspan=3>
                  <asp:TextBox ID="TxtPercent" runat="server" Width="30%" onkeypress="return isFloatKey(event);"></asp:TextBox>
            </td>
            <td width="2%" height="100%"></td>

        </tr>
        
        <tr>
            <td width="2%" height="100%"></td>
            
            <td colspan=5 align="center">
                <asp:ImageButton ID="CmdProcess" runat="server" ImageUrl="~/Images/ProcessBtn.png"/>
                  &nbsp;<asp:ImageButton ID="CmdSave" runat="server" 
                    ImageUrl="~/Images/PostBtn.png" />
                  &nbsp;<asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/Close.png" />  
            </td>
            <td width="2%" height="100%"></td>
        </tr>
        
        <tr>
            <td></td>
            <td colspan="5"> 
                <asp:GridView ID="DgvDivPost" runat="server" AutoGenerateColumns="False" 
                    CssClass="grid-view" Width="100%" AllowPaging="True">
                    <Columns>
                        <asp:BoundField DataField="Mem_No" HeaderText="Member No" />
                        <asp:BoundField ConvertEmptyStringToNull="False" DataField="Admit_Date" 
                            DataFormatString="{0:dd/MM/yyyy}" HeaderText="Open Date" 
                            InsertVisible="False" />
                        <asp:BoundField DataField="Cust_Name" HeaderText="Cutomer Name" />
                        <asp:BoundField DataField="DivAmt" HeaderText="Div Amt " >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Share_Out" HeaderText="Share Balance" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Cust_ID" HeaderText="Customer ID" Visible="False"/>
                        <asp:BoundField DataField="Acc_ID" HeaderText="Acc_ID" Visible="False" />
                    </Columns>
                </asp:GridView>
            
            </td>
            <td></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                
                <asp:HiddenField ID="HdnTrDate" runat="server" />
                <asp:HiddenField ID="HdnToDate" runat="server" />
                
            </td>
        </tr>
    </table>
</div>
</ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>
