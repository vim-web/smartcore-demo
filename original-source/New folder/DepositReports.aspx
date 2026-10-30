<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DepositReports.aspx.vb" Inherits="SMARTCORE_UI.DepositReports" MasterPageFile="~/SMARTCORE.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
   
    <style type="text/css">
        .style1
        {
            height: 58px;
        }
        .style2
        {
            height: 47px;
        }
        #progress
        {
            width: 202px;
        }
    </style>
    
       <script language="javascript" type="text/javascript">
     
  function validatedate(inputText)  
  {  
  var dateformat = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;  
  // Match the date format through regular expression  
  if(inputText.value.match(dateformat))  
  {  
//  document.form1.text1.focus();  
  //Test which seperator is used '/' or '-'  
  var opera1 = inputText.value.split('/');  
  var opera2 = inputText.value.split('-');  
  lopera1 = opera1.length;  
  lopera2 = opera2.length;  
  // Extract the string into month, date and year  
  if (lopera1>1)  
  {  
  var pdate = inputText.value.split('/');  
  }  
  else if (lopera2>1)  
  {  
  var pdate = inputText.value.split('-');  
  }  
  var dd = parseInt(pdate[0]);  
  var mm  = parseInt(pdate[1]);  
  var yy = parseInt(pdate[2]);  
  // Create list of days of a month [assume there is no leap year by default]  
  var ListofDays = [31,28,31,30,31,30,31,31,30,31,30,31];  
  if (mm==1 || mm>2)  
  {  
  if (dd>ListofDays[mm-1])  
  {
      alert('Invalid date format! Please enter a valid date!');
     
  return false;  
  }  
  }  
  if (mm==2)  
  {  
  var lyear = false;  
  if ( (!(yy % 4) && yy % 100) || !(yy % 400))   
  {  
  lyear = true;  
  }  
  if ((lyear==false) && (dd>=29))  
  {
      alert('Invalid date format! Please enter a valid date!');
     
  return false;  
  }  
  if ((lyear==true) && (dd>29))  
  {
      alert('Invalid date format! Please enter a valid date!');
     
  return false;  
  }   
  }   
  }   
  else  
  {   
      alert("Invalid date format! Please enter a valid date!");
     
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
<div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%; font-family: Tahoma" >                
    
    <legend>Deposit Reports</legend>

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
                &nbsp;&nbsp; 
                <asp:Label ID="LblDdl2" runat="server" Text="Scheme" ForeColor="#6699FF"></asp:Label>
            </td>
            <td > 
                <asp:DropDownList ID="DdlScheme" runat="server" Width="98%">
                </asp:DropDownList>
             </td>
            <td > </td>
        </tr>
       
        <tr>
            <td width="6%"> </td>
            <td width="18%"> 
                <asp:Label ID="Lbl_ReportOpt" runat="server" ForeColor="#6699FF" 
                    Text="Report Options"></asp:Label>
            </td>
            <td width="26%" colspan="2" style="width: 44%"> 
                <asp:DropDownList ID="DdlReportOptions" runat="server" AutoPostBack="True" 
                    Width="98%">
                </asp:DropDownList>
            </td>
            <td width="26%"> </td>
            <td width="6%"> </td>
        </tr>
        
        
       
         <tr>
            <td > </td>
            <td > 
                <asp:Label ID="LblDt1" runat="server" Text="From Date" ForeColor="#6699FF"></asp:Label>
             </td>
            <td > 
                <asp:TextBox ID="DtFrom" runat="server"  Width = "95%" TabIndex="2" OnChange="validatedate(this);"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="DtFrom"> </asp:CalendarExtender>      
             </td>
            <td > 
                &nbsp;&nbsp; 
                <asp:Label ID="LblDt2" runat="server" Text="To Date" ForeColor="#6699FF"></asp:Label>
            </td>
            <td > 
               <asp:TextBox ID="DtTo" runat="server"  Width = "95%" TabIndex="2" OnChange="validatedate(this);"
                    AutoCompleteType="Disabled"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="DtTo"> </asp:CalendarExtender>      
             </td>
            <td > </td>
        </tr>
        <tr>
            <td class="style1" > </td>
            <td class="style1" > <asp:Label ID="LblTxt1" runat="server" Text="Text" 
                    ForeColor="#6699FF"></asp:Label></td>
            <td class="style1" > <asp:TextBox ID="Txt1" runat="server"  Width = "70%" TabIndex="2"></asp:TextBox> 
           
           
                <asp:DropDownList ID="DdlPeriodFrom" runat="server" Width="25%">
                </asp:DropDownList>
           
           
            <td class="style1" > 
                &nbsp;&nbsp; 
                <asp:Label ID="LblTxt2" runat="server" Text="Text" ForeColor="#6699FF"></asp:Label>
            </td>
            <td class="style1" > 
                <asp:TextBox ID="Txt2" runat="server" TabIndex="2" Width="70%"></asp:TextBox>
                <asp:DropDownList ID="DdlPeriodTo" runat="server" Width="25%">
                </asp:DropDownList>
           
            </td>
            <td class="style1"> </td>
        </tr>
        <tr>
            <td > </td>
            <td > 
                <asp:Label ID="LblTxt3" runat="server" Text="Text" ForeColor="#6699FF"></asp:Label>
            </td>
            <td > 
                <asp:TextBox ID="Dt3" runat="server" TabIndex="2" Width="98%"></asp:TextBox>
                <asp:CalendarExtender ID="Dt3_CalendarExtender" runat="server" 
                    Format="dd/MM/yyyy" TargetControlID="Dt3">
                </asp:CalendarExtender>
            </td>
            <td > 
                &nbsp;&nbsp; 
                <asp:Label ID="LblTxt4" runat="server" Text="Text" ForeColor="#6699FF"></asp:Label>
            </td>
            <td > 
                <asp:TextBox ID="Dt4" runat="server" AutoCompleteType="Disabled" TabIndex="2" 
                    Width="98%"></asp:TextBox>
                <asp:CalendarExtender ID="Dt4_CalendarExtender" runat="server" 
                    Format="dd/MM/yyyy" TargetControlID="Dt4">
                </asp:CalendarExtender>
            </td>
            <td> </td>
        </tr>
        <tr>
            <td > </td>
            <td>
                  <label>Output To</label></td>
              <td>
                  <asp:DropDownList ID="CmbOutput" runat="server" Width="100%">
                  </asp:DropDownList>
              </td>

            </td>
            <td > 
                  &nbsp;</td>
            <td > </td>
            <td > </td>
            <td> </td>
        </tr>
        <tr>
            <td > </td>
            <td > </td>
            <td > </td>
            <td > </td>
            <td > </td>
            <td> </td>
        </tr>
        </table>
        <table width="100%">
        <tr>
            <td width="20%"> 
                
            </td>
            <td  width="20%"> </td>
             <td align="center" colspan="3">
                  <asp:ImageButton ID="CmdShow" runat="server" Height="25px" 
                      ImageUrl="~/Images/Show.png" />
                  &nbsp;<asp:ImageButton ID="CmdClose" runat="server" 
                      ImageUrl="~/Images/CloseBtn.png" />
              </td>
            <td width="20%"> </td>
        </tr>
           <tr>
            <td width="6%" class="style2"> </td>
            <td width="18%" class="style2"> </td>
            <td width="26%" class="style2"> 
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
            <td width="18%" class="style2"> &nbsp;</td>
            <td width="26%" class="style2"> </td>
            <td width="6%" class="style2"> </td>
        </tr>
    </table>
            </div>

   </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>  
      