<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ChittyAuctionList.aspx.vb" MasterPageFile="~/SMARTCORE.Master" Inherits="SMARTCORE_UI.ChittyAuctionList" %>

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
    <legend>CHITTY AUCTION LIST</legend> 
    <table width="100%">
        <tr>
            <td width="2%" height="100%"></td>
            <td width="20%" height="100%">
                <label>Branch</label>
            </td>
            <td width="2%" height="100%"></td>
            <td width="52%" height="100%" colspan="3">
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
            <td width="2%" height="100%">
            <td width="52%" height="100%" colspan="3">
                <asp:DropDownList ID="CmbScheme" runat="server" AutoPostBack="True" 
                    TabIndex="3" Width="100%">
                </asp:DropDownList>   
            </td>
             <td width="2%" height="100%"></td>
        </tr>

        <tr>
            <td width="2%" height="100%"></td>
            <td width="20%" height="100%">
                <label>Group</label>
            </td>
            <td width="2%" height="100%"></td>
            <td width="52%" height="100%" colspan="3">
                <asp:DropDownList ID="CmbChittyGrp" runat="server" AutoPostBack="True" 
                    TabIndex="3" Width="100%" AppendDataBoundItems="True">
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
                  <asp:TextBox ID="TxtFromDate" runat="server" Width="30%" OnChange="validatedate(this);"></asp:TextBox>
                  <asp:CalendarExtender ID="TxtFromDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                      TargetControlID="TxtFromDate">
                  </asp:CalendarExtender>

            </td>
            <td width="2%" height="100%"></td>
        </tr>
        
        <tr>
            <td width="2%" height="100%"></td>
            <td width="20%" height="100%">
                <label>To Date</label>
            </td>
            <td width="2%" height="100%"></td>
            <td colspan=3>
                  <asp:TextBox ID="TxtToDate" runat="server" Width="30%" OnChange="validatedate(this);"></asp:TextBox>
                  <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                      TargetControlID="TxtToDate">
                  </asp:CalendarExtender>
            </td>
            <td width="2%" height="100%"></td>

        </tr>
        <tr>
            <td width="2%" height="100%"></td>
            
            <td colspan=5 align="center">
                <asp:ImageButton ID="CmdReport" runat="server" 
                    ImageUrl="~/Images/Show.png" Width="89px"/>
                  &nbsp;&nbsp;<asp:ImageButton ID="CmdClose" runat="server" 
                    ImageUrl="~/Images/Close.png" PostBackUrl="~/Main.aspx" />  
            </td>
            <td width="2%" height="100%"></td>
        </tr>
        
        <tr>
            <td></td>
            <td colspan="5"> 
            
            </td>
            <td></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                
                <asp:HiddenField ID="HdnTrDate" runat="server" />
                
            </td>
        </tr>
    </table>
    
     <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
         AssociatedUpdatePanelID="UpdatePanel1">
         <ProgressTemplate>
             <div ID="blur">
                 <asp:Panel ID="Panel1" runat="server">
                 </asp:Panel>
             </div>
             <div ID="progress">
                 <img alt="progress" src="Images/progress.gif" /> Please Wait...
             </div>
         </ProgressTemplate>
     </asp:UpdateProgress>
    
</div>
</ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>
