<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="NotSubmitPanCard.aspx.vb" Inherits="SMARTCORE_UI.NotSubmitPanCard" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
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

                           return false;
                       }
                       if ((lyear == true) && (dd > 29)) {
                           alertify.error('Invalid date format! Please enter a valid date!');

                           return false;
                       }
                   }
               }
               else {
                   alertify.error("Invalid date format! Please enter a valid date!");

                   document.form1.text1.focus();
                   return false;
               }
           }

</script>
    <style type="text/css">
        .auto-style4 {
            width: 8%;
        }
        .auto-style5 {
            width: 28%;
        }
        .auto-style6 {
            width: 14%;
        }
        .auto-style7 {
            width: 14%;
            height: 21px;
        }
        .auto-style8 {
            width: 28%;
            height: 21px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 52%; margin-left:20%;float:left;" >                
    
    <legend>PanCard Details</legend>

    <table width="100%">
        <tr>
            <td class="auto-style7"><label>Quarterly Report</label></td>
            <td class="auto-style8"> 
                <asp:CheckBox ID="ChkQuarterly" runat="server" AutoPostBack="True" />
            </td>
        </tr>
        
         <tr>
             <td class="auto-style6">
                 <asp:Label ID="LblDdl1" runat="server" CssClass="lblCSS" Text="Branch"></asp:Label>
             </td>
            <td class="auto-style5" > 
                <asp:DropDownList ID="DdlBranch" runat="server" Width="45%">
                </asp:DropDownList>
             </td>
        </tr>
       
        
        
        
       
        <tr>
            <td class="auto-style6"><LABEL>Submit</LABEL>
                &nbsp;</td>
            <td class="auto-style5">
                <asp:DropDownList ID="DrpSubmit" runat="server" Width="45%">
                </asp:DropDownList>
            </td>
        </tr>
       
        
        
        
       
        <tr>
            <td class="auto-style6">
                <asp:Label ID="LblScheme" runat="server" Text="Scheme" CssClass="lblCSS"  Visible=false></asp:Label>
                &nbsp;</td>
            <td class="auto-style5">
                <asp:DropDownList ID="DdlScheme" runat="server" Width="45%" Visible=false>
                </asp:DropDownList>
            </td>
        </tr>
       
        
        
        
       
        <tr>
            <td class="auto-style6">
                <asp:Label ID="LblFromDt" runat="server" Text="From Date" Visible=False 
                    CssClass="lblCSS" ></asp:Label></td>
            <td class="auto-style5" > 
                <asp:TextBox ID="TxtFrom" runat="server" Width="45%" Visible=false></asp:TextBox>
                 <asp:CalendarExtender ID="TxtFrom_CalendarExtender" runat="server" 
              Format="dd/MM/yyyy" TargetControlID="TxtFrom">
          </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td class="auto-style6"><asp:Label ID="LblToDt" runat="server" Text="To Date" 
                    Visible=False CssClass="lblCSS" ></asp:Label>
                &nbsp;</td>
            <td class="auto-style5">
                <asp:TextBox ID="TxtTo" runat="server" Width="45%" Visible=false></asp:TextBox>
                  <asp:CalendarExtender ID="TxtTo_CalendarExtender1" runat="server" 
              Format="dd/MM/yyyy" TargetControlID="TxtTo">
          </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td class="auto-style6">
                &nbsp;</td>
            <td class="auto-style5">
                &nbsp;</td>
        </tr>
        </table>
        <table width="100%">
        <tr>
            <td class="auto-style4" align="center"> 
                
                &nbsp;</td>
             <td >
                             
                <asp:ImageButton ID="CmdShow" runat="server" Height="25px" 
                    ImageUrl="~/Images/Show.png" />
            
                  <asp:ImageButton ID="CmdClose" runat="server" ImageUrl="~/Images/CloseBtn.png" />
              </td>
        </tr>
        
           <tr>
            <td class="auto-style4"> 
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div id="blur">
                        </div>
                        <div id="progress">
                            <img alt="progress" src="Images/progress.gif" />
                            Please Wait...
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
               </td>
            <td width="18%"> </td>
        </tr>
        
    </table>
            </div>

   </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>  
      
      