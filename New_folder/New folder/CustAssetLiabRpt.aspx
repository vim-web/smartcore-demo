<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMARTCORE.Master" CodeBehind="CustAssetLiabRpt.aspx.vb" Inherits="SMARTCORE_UI.CustAssetLiabRpt" %>
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

 <div class="gradientBoxesWithOuterShadows" style="width: 60%; margin-left:20%; margin-right:20%;">
 <legend>Customer Asset Liability Report</legend>                
<br />

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<table width="100%">
    <tr>
        <td width="2%">
        </td>
        <td width="48%" colspan="3" style="width: 96%">
            <table width ="100%">
                <tr>
                <td width ="33%">
                        <asp:RadioButton ID="Opt_LiabilityRegister" runat="server" 
                                Text="Liability Register" Checked="True" GroupName="Liability" />
                </td>
                <td width ="33%">
                            <asp:RadioButton ID="Opt_LiabilityCertificate" runat="server" 
                                Text="Liability Certificate" GroupName="Liability" />
                </td>

                <td width ="34%">

                                            <asp:RadioButton ID="Opt_NonLiabilityCertificate" runat="server" 
                                Text="Non Liability Certificate" GroupName="Liability" />
                </td>
                </tr>

            </table>
        </td>
        <td width="2%">
        </td>
    </tr>
    <tr>
        <td>
            </td>
        <td>
            <label>Branch</label></td>
        <td colspan="2">
            <asp:DropDownList ID="CmbBranch" runat="server" Width="100%" 
                AutoPostBack="True">
            </asp:DropDownList>
            </td>
        <td>
            </td>
    </tr>
    <%--<tr>
        <td>
            &nbsp;</td>
        <td>
            <label>Membership Type</label></td>
        <td colspan="2">
            <asp:DropDownList ID="CmbMembershipType" runat="server" Width="100%">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <label>Member No</label></td>
        <td colspan="2">
            <asp:TextBox ID="TxtMemberNo" runat="server" Width="100%" AutoPostBack="True"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>--%>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <label>Customer ID</label></td>
        <td colspan="2">
            <asp:TextBox ID="TxtCustId" runat="server" Width="80%" AutoPostBack="True"></asp:TextBox>
            <asp:ImageButton ID="ImgSrch" runat="server" Height="20px" ImageUrl="~/images/Search.png" Width="23px"  />
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <label>Customer Name</label></td>
        <td colspan="2">
            <asp:TextBox ID="TxtCustName" runat="server" Width="100%"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <label>Address</label></td>
        <td colspan="2">
            <asp:TextBox ID="TxtAddress" runat="server" Width="100%" TextMode="MultiLine"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <label>As On Date</label></td>
        <td colspan="2">
            <asp:TextBox ID="Txt_DueDate" runat="server" Height="22px" Width="87%" OnChange="validatedate(this);"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                TargetControlID="Txt_DueDate">
            </asp:CalendarExtender>
            <asp:Image ID="ImgDoa" runat="server" BorderStyle="Outset" Height="24px" 
                ImageUrl="~/Images/Cal.jpg" meta:resourcekey="ImgDoaResource1" Width="17px" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td align="center" colspan="3">
            <asp:RadioButton ID="RadAsset" runat="server" GroupName="Asset" Text="Asset" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:RadioButton ID="RadLiability" runat="server" GroupName="Asset" 
                Text="Liability" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:RadioButton ID="RadBoth" runat="server" GroupName="Asset" Text="Both" 
                Checked="True" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td align="center">
            <asp:CheckBox ID="ChkIncludeClosed" runat="server" Text="Include Closed" />
        </td>
        <td align="center">
            <asp:CheckBox ID="ChkClosedOnly" runat="server" Text="Closed Only" />
        </td>
        <td align="center">
            <asp:CheckBox ID="ChkPrintPreview" runat="server" Text="Print Preview" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td align="center" colspan="3">
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
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td align="center" colspan="3">
            <asp:ImageButton ID="CmdShow" runat="server" ImageUrl="~/Images/Show.png" 
                style="margin-left: 0px" CausesValidation="False" />
            &nbsp;<asp:ImageButton ID="CmdClear" runat="server" 
                ImageUrl="~/Images/ClearBtn.png" />
            &nbsp;<asp:ImageButton ID="CmdClose" runat="server" 
                ImageUrl="~/Images/CloseBtn.png" style="height: 23px" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
</table>

</div>
</ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>