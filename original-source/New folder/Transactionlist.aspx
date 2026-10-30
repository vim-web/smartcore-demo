<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="Transactionlist.aspx.vb" Inherits="SMARTCORE_UI.Transactionlist" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
<link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />    
    <link href="CSS/ui-lightness/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript' src="Scripts/Common.js"></script>
    <script src="Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script type='text/javascript' src="Scripts/jquery-ui-1.9.2.custom.min.js"></script>      
    
    <script language="javascript" type="text/javascript">
            function fSplitValues(source, eventArgs) {
                var sStr;
                var extender = source.get_id();         //id of the source extender            
                if (document.getElementById("<%=TxtAccCode.ClientID%>").value != "") {
                    sStr = document.getElementById("<%=TxtAccCode.ClientID%>").value;
                    sStr = sStr.replace(/  /g, "")
                    sStr = sStr.replace("Code:", "")
                    sStr = sStr.replace(".", "")
                    if (sStr.indexOf("|") != -1) {
                        sStr = sStr.split("|");
                        document.getElementById("<%=TxtAccCode.ClientID%>").value = sStr[1];
                        document.getElementById("<%=TxtAccName.ClientID%>").value = sStr[0].replace(".", "");

                    }
                }
            }

    </script>
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
    <style type="text/css">
        .style1
        {
            height: 22px;
        }
    </style>
</asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
     <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%; font-family: Tahoma" >                
    
    <legend>Transaction List</legend>
    
     <table width="100%">
     <tr>
     <td width="2%"></td>
     <td class="style2"><label>Branch</label></td>
     <td colspan="3">
         &nbsp;<asp:DropDownList ID="CmbBranch" runat="server" Width="91%">
         </asp:DropDownList>
         </td>
     <td width="2%"></td>
     </tr>
                <tr>
             <td width="2%">
                 &nbsp;</td>
             <td class="style2">
                 <label>A/c Heads</label></td>
             
                            
             <td colspan="3">
                 <asp:TextBox ID="TxtAccCode" runat="server" AutoPostBack="True" 
                     CssClass="twitterStyleTextbox" Height="22px" TabIndex="1" Width="16%"></asp:TextBox>
                 <asp:autocompleteextender ID="AutoCompleteExtender1" runat="server" 
                     CompletionInterval="10" CompletionListCssClass="wordWheel listMain .box" 
                     CompletionListHighlightedItemCssClass="wordWheel itemsSelected" 
                     CompletionListItemCssClass="wordWheel itemsMain" CompletionSetCount="6" 
                     EnableCaching="true" MinimumPrefixLength="1" 
                     OnClientItemSelected="fSplitValues" ServiceMethod="GetCompletionList" 
                     TargetControlID="TxtAccCode" UseContextKey="True">
                 </asp:autocompleteextender>
                 <asp:TextBox ID="TxtAccName" runat="server" CssClass="twitterStyleTextbox" 
                     TabIndex="2" Height="22px" Width="78%"></asp:TextBox>
             </td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td  width ="15%">
                 <label>From Date</label></td>
             <td width = "20%">
                 <asp:TextBox ID="TxtFromDate" runat="server" Width="75%" OnChange="validatedate(this);" TabIndex="3"></asp:TextBox>
                 <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="TxtFromDate"> </asp:CalendarExtender> 
             </td>
             <td  width ="15%">
                 <label>
                 To Date</label></td>
             <td class="style4">
                 <asp:TextBox ID="TxtToDate" runat="server" Width="108px" OnChange="validatedate(this);" TabIndex="4"></asp:TextBox>
                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="TxtToDate"> </asp:CalendarExtender> 
             </td>
             <td width="20%">
                 &nbsp;</td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
         <tr>
             <td width="2%">
                 &nbsp;</td>
             <td>
                 <label>
                 Output To</label></td>
             <td colspan="3">
                
                 <asp:DropDownList ID="CmbOutput" runat="server" Width="93%" TabIndex="8">
                 </asp:DropDownList>
                
             </td>
             <td width="20%">
                 &nbsp;</td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
         
           <tr>
               <td width="2%" class="style1">
                   </td>
               <td class="style1">
                   &nbsp;<label>To Excel</label></td>
               <td colspan="3" class="style1">
                   <asp:CheckBox ID="ChkToExcl" runat="server" />
               </td>
               <td width="20%" class="style1">
                   </td>
               <td width="2%" class="style1">
                   </td>
         </tr>
         
           <tr>
             <td width="2%">
                 &nbsp;</td>
             <td class="style2">
                 
             <td class="style3" colspan="2">
              
  
                 <asp:ImageButton ID="CmdShow" runat="server" ImageUrl="~/Images/Show.png" />
                 <asp:ImageButton ID="CmdClear" runat="server" 
                     ImageUrl="~/Images/ClearBtn.png" />
                 <asp:ImageButton ID="CmdClose" runat="server" 
                     ImageUrl="~/Images/CloseBtn.png" />
              
  
             </td>
             <td class="style4">
                
                 <td width="2%">
                     &nbsp;</td>
         </tr>
          <tr>
             <td width="2%">
                 &nbsp;</td>
             <td class="style2">
                 
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div ID="blur"> </div>
                    <div ID="progress">
                        <img alt="progress" src="Images/progress.gif" /> Please Wait...
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
             <td class="style1">
                
             <td class="style4">
               
             </td>
             <td width="2%">
                 &nbsp;</td>
         </tr>
         
     </table>
     </div>
    </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>



