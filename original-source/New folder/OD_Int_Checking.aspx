<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="OD_Int_Checking.aspx.vb" Inherits="SMARTCORE_UI.OD_Int_Checking" 
    title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" /> 
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
      alertify.error('Invalid date format! Please enter a valid date!');
     
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
      alertify.error('Invalid date format! Please enter a valid date!');
     
  return false;  
  }  
  if ((lyear==true) && (dd>29))  
  {
      alertify.error('Invalid date format! Please enter a valid date!');
     
  return false;  
  }   
  }   
  }   
  else  
  {
      alertify.error("Invalid date format! Please enter a valid date!");
     
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
    
    <legend>OD Calculation</legend>
        <table width="100%">
            <tr>
                <td width="25%">
                </td>
                <td width="20%">
                    <asp:Label ID="LblDdl2" runat="server" ForeColor="#6699FF" Text="Branch"></asp:Label>
                </td>
                <td width="20%">
                    <asp:DropDownList ID="DdlBranch" runat="server" Width="98%">
                    </asp:DropDownList>
                </td>
                <td width="25%">
                    &nbsp;</td>
            </tr>

            <tr>
                <td width="25%">
                    &nbsp;</td>
                <td width="20%">
                    <asp:Label ID="Label1" runat="server" Text="Scheme" ForeColor="#6699FF"></asp:Label>
                    &nbsp;</td>
                <td width="20%">
                    <asp:DropDownList ID="DdlScheme" runat="server" Width="98%" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td width="25%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="25%">
                    &nbsp;</td>
                <td width="20%">
                    <asp:Label ID="LblAccNo" runat="server" ForeColor="#6699FF" Text="AccNo"></asp:Label>
                </td>
                <td width="20%">
                    <asp:TextBox ID="TxtAccNo" runat="server" Width="98%" AutoPostBack="True"></asp:TextBox>
                </td>
                <td width="25%">
                    <asp:ImageButton ID="ImgSrch" runat="server" Height="24px" 
                        ImageUrl="~/images/Search.png" Visible="false" TabIndex="999" Width="23px" />
                </td>
            </tr>
            <tr>
                <td width="25%">
                    &nbsp;</td>
                <td width="20%">
                    <asp:Label ID="Label3" runat="server" ForeColor="#6699FF" Text="Name"></asp:Label></td>
                <td width="20%">
                    <asp:TextBox ID="TxtName" width="98%" runat="server"></asp:TextBox>
                     </td>
                <td width="25%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="Label2" runat="server" ForeColor="#6699FF" Text="As On Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="DtTo" runat="server" width="98%" ></asp:TextBox>
                     <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                        TargetControlID="DtTo">
                    </asp:CalendarExtender>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="AccId" runat="server" />
                </td>
                <td colspan="2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <table width="100%">
            <tr>
            
                <td align="center" colspan="6" style="width: 40%" width="20%">
                    &nbsp; &nbsp;<asp:ImageButton ID="CmdShow" runat="server" Height="25px" 
                        ImageUrl="~/Images/Show.png" />
                    &nbsp;<asp:ImageButton ID="CmdClose" runat="server" 
                        ImageUrl="~/Images/CloseBtn.png" />
                </td>
            </tr>
            <tr>
                <td width="6%">
                </td>
                <td width="18%">
                    &nbsp;</td>
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
                <td width="18%">
                </td>
                <td width="26%">
                </td>
                <td width="6%">
                </td>
            </tr>
        </table>


            </div>

   </ContentTemplate>
</asp:UpdatePanel> 

</asp:Content>  
