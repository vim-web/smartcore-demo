<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="ShareStatement.aspx.vb" Inherits="SMARTCORE_UI.ShareStatement" 
    title="Share Statements " %>
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

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" CombineScripts="True"> </asp:ToolkitScriptManager>
<div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%; margin-top:10% ;font-family: Tahoma" >                
    
    <legend> Share Statements</legend>

    <table width="100%">
        <tr>
            <td width="6%"> </td>
            <td width="18%"> </td>
            <td width="26%"> </td>
            <td width="18%"> </td>
            <td width="26%"> </td>
            <td width="6%"> </td>
        </tr>
        
         <tr>
            <td > </td>
            <td > 
                <asp:Label ID="LblDdl1" runat="server" Text="Branch" ForeColor="#6699FF"></asp:Label>
             </td>
            <td > 
                <asp:DropDownList ID="DdlBranch" runat="server" Width="98%">
                </asp:DropDownList>
             </td>
            <td > 
                <asp:Label ID="LblDt2" runat="server" ForeColor="#6699FF" Text="Report Type"></asp:Label>
             </td>
            <td > 
                <asp:DropDownList ID="DdlRptType" runat="server" CausesValidation="True" 
                    Width="98%">
                    <asp:ListItem>TRANSACTION UNDER  MEMBERSHIP</asp:ListItem>
                    <%--<asp:ListItem>LOAN COVERAGE</asp:ListItem>--%>
                </asp:DropDownList>
             </td>
            <td > </td>
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
        </tr>
       
        
        
        
       
         <tr>
            <td > </td>
            <td > 
                <asp:Label ID="LblDt1" runat="server" Text="Date From" ForeColor="#6699FF"></asp:Label>
             </td>
            <td > 
                <asp:TextBox ID="DtFrom" runat="server"  Width = "95%" TabIndex="2" OnChange="validatedate(this);"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="DtFrom"> </asp:CalendarExtender>      
             </td>
            <td > 
                <asp:Label ID="LblDt3" runat="server" ForeColor="#6699FF" Text="Date To"></asp:Label>
             </td>
            <td > 
               <asp:TextBox ID="DtTo" runat="server"  Width = "98%" TabIndex="2" OnChange="validatedate(this);"
                    AutoCompleteType="Disabled"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="DtTo"> </asp:CalendarExtender>      
             </td>
            <td > </td>
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
        </tr>
        
        <tr>
            <td > </td>
            <td > </td>
            <td > 
            
            <table width="100%">
            <tr>
            <td width ="50%">
                <asp:ImageButton ID="CmdShow" runat="server" Height="25px" 
                    ImageUrl="~/Images/Show.png" />
            </td>
            <td width ="50%">
                <asp:ImageButton ID="CmdClose" runat="server" 
                    ImageUrl="~/Images/CloseBtn.png" />
            </td>
            </tr>
            </table>
          


            </td>
            <td >  </td>
            <td > </td>
            <td> </td>
        </tr>
        </table>
        <table width="100%">
        <tr>
            <td width="20%"> 
                
            </td>
            <td  width="20%">  </td>
             <td align="center" colspan="3">
                     </td>
            <td width="20%"> </td>
        </tr>
        
           <tr>
            <td width="6%"> </td>
            <td width="18%"> </td>
            <td width="26%"> 
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                    AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div ID="blur">
                        </div>
                        <div ID="progress">
                            <img alt="progress" src="Images/progress.gif" /> Please Wait...
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
               </td>
            <td width="18%"> </td>
            <td width="26%"> </td>
            <td width="6%"> </td>
        </tr>
        
    </table>
            </div>

   </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>  
      
      