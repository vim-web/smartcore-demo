<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="IBTranListRpt.aspx.vb" Inherits="SMARTCORE_UI.IBTranListRpt" 
    title="Inter Branch Transaction List" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <link href="CSS/PageStyle.css" rel="stylesheet" type="text/css" />  
<style type="text/css">
.TextColor {
	color: #03F;
	font-weight: bold;
	font-size:small;
}
.TextColorNormal {
	color: #03F;
	font-weight:normal;
}
.TextColorRed{
	color:Red;
	font-weight: bold;
}

.ButtonStyle {
	color: #FFF;
	background-color: #039;
	text-align: center;
	display: inline;
        }
.CalendarCSS  
{  
    background-color:#3366CC;  
    color:Snow;  
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

   <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%; font-family: Tahoma" >                
    <legend>Inter Branch Transaction List</legend>   
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
                <asp:Label ID="LblDdl1" runat="server" Text="Branch"></asp:Label>
             </td>
            <td > 
                <asp:DropDownList ID="DdlBranch" runat="server" Width="98%">
                </asp:DropDownList>
             </td>
            <td > 
                <asp:Label ID="LblDdl2" runat="server" Text="Scheme"></asp:Label>
            </td>
            <td > 
                <asp:DropDownList ID="DdlScheme" runat="server" Width="98%">
                </asp:DropDownList>
             </td>
            <td > </td>
        </tr>
        
         <tr>
            <td > </td>
            <td > 
                <asp:Label ID="LblDt1" runat="server" Text="From Date"></asp:Label>
             </td>
            <td > 
                <asp:TextBox ID="DtFrom" runat="server"  Width = "95%" TabIndex="2" OnChange="validatedate(this);"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="DtFrom"> </asp:CalendarExtender>      
             </td>
            <td > 
                <asp:Label ID="LblDdl3" runat="server" Text="To Date"></asp:Label>
             </td>
            <td > 
                <asp:TextBox ID="DtTo" runat="server" OnChange="validatedate(this);" 
                    TabIndex="2" Width="98%"></asp:TextBox>
                <asp:CalendarExtender ID="DtTo_CalendarExtender" runat="server" 
                    Format="dd/MM/yyyy" TargetControlID="DtTo">
                </asp:CalendarExtender>
             </td>
            <td > </td>
        </tr>
        <tr>
            <td  > </td>
            <td  > 
                <asp:Label ID="LblDt2" runat="server" Text="Report Type" Visible="False"></asp:Label>
            </td>
            <td  >  
           
           
                
                &nbsp;<td  > 
                    <asp:RadioButton ID="rbtnDetailed" runat="server" AutoPostBack="True" 
                        Checked="True" OnCheckedChanged="rbtnDetailed_CheckedChanged" 
                        Text="Detailed" Visible="False" />
                </td>
            <td  > 
                <asp:RadioButton ID="rbtnSummary" runat="server" AutoPostBack="True" 
                    OnCheckedChanged="rbtnSummary_CheckedChanged" Text="Summary" Visible="false"  />
                </td>
            <td > </td>
        </tr>
        
        <tr>
            </td>
            <td > </td>
            <td> 
                &nbsp;</td>
            <td > 
                  &nbsp;</td>
            <td > </td>
            <td > </td>
            <td> </td>
        </tr>
        <tr>
            <td > </td>
            <td > </td>
            <td colspan="2" > 
                <asp:ImageButton ID="ImgProcess" runat="server" Height="25px" 
                    ImageUrl="~/Images/ProcessBtn.png" Visible="False" />
                &nbsp;
                <asp:ImageButton ID="CmdShow" runat="server" Height="25px" 
                    ImageUrl="~/Images/Show.png" />
                &nbsp;<asp:ImageButton ID="CmdClose" runat="server" 
                    ImageUrl="~/Images/CloseBtn.png" />
            </td>
            <td > </td>
            <td> </td>
        </tr>
        </table>
        <table width="100%">
        <tr>
            <td width="20%"> 
                <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
                    EnablePageMethods="true">
                </asp:ToolkitScriptManager>
            </td>
            <td  width="20%"> <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                    AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div ID="blur">
                        </div>
                        <div ID="progress">
                            <%--<asp:Label ID="Lbl_Status" runat="server" Text="Status"></asp:Label>--%>
                            <img alt="progress" src="Images/progress.gif" /> Please Wait...
                            
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress> </td>
            <td  colspan="3">  
                &nbsp;</td>
            <td width="20%"> </td>
        </tr>
    </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    <style type="text/css">


*
{
padding: 0px;
            margin-left: 0px;
            margin-right: 0px;
            margin-top: 0px;
        }

    </style>
</asp:Content>


